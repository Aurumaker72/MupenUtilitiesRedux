using System.Buffers.Binary;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using MupenUtilitiesRedux.Models.Attributes;
using MupenUtilitiesRedux.Models.Enums;
using MupenUtilitiesRedux.Models.Exceptions;
using MupenUtilitiesRedux.Models.Options;

namespace MupenUtilitiesRedux.Models;

/// <summary>
///     A <see langword="static" /> <see langword="class" /> providing functionality for converting <see cref="Movie" />s
///     to and from raw data
/// </summary>
public static class MovieFactory
{
	/// <summary>
	///     Converts a <see cref="Movie" /> into a <see cref="byte" />[]
	/// </summary>
	/// <param name="movie">The <see cref="Movie" /> to be converted</param>
	/// <param name="movieSerializationOptions">
	///     The options to be considered when converting the <see cref="Movie" /> into raw
	///     data
	/// </param>
	/// <returns>A <see cref="byte" />[] representation of the <see cref="Movie" /></returns>
	/// <exception cref="MovieSerializationException"></exception>
	public static byte[] ToBytes(Movie movie, MovieSerializationOptions? movieSerializationOptions = null)
	{
		movieSerializationOptions ??= new MovieSerializationOptions();

		// compute the total samples by summing all controllers' sample counts
		var totalSamples = 0;
		for (var i = 0; i < Movie.MaxControllers; i++)
		{
			var controllerSamples = movie.Controllers[i].Samples?.Count;
			if (controllerSamples.HasValue) totalSamples += controllerSamples.Value;
		}

		// allocate the maximum amount of bytes
		// this also sets up a safeguard for this method to fail via ArrayIndexOutOfRangeException
		var bytes = new byte[typeof(Movie).GetCustomAttribute<MovieMetadataAttribute>()!.EndOfHeader +
		                     totalSamples * sizeof(int)];

		// fill bytes with bogus value so we know if something didnt get filled in
		bytes = Enumerable.Repeat(movieSerializationOptions.Value.EmptyValue, bytes.Length).ToArray();

		foreach (var property in typeof(Movie).GetProperties(BindingFlags.Instance |
		                                                     BindingFlags.DeclaredOnly |
		                                                     BindingFlags.NonPublic | BindingFlags.Public))
		{
			if (property.GetCustomAttribute(typeof(MovieValueMetadataAttribute)) is not MovieValueMetadataAttribute
			    movieValueMetadataAttribute)
			{
				Debug.Print($"Skipped property without {nameof(MovieValueMetadataAttribute)}");
				continue;
			}

			var val = property.GetValue(movie);

			switch (val)
			{
				case null:
					throw new MovieSerializationException($"A {nameof(Movie)} property's value was null");
				case byte byteVal:
					bytes[movieValueMetadataAttribute.Offset] = byteVal;
					break;
				case int intVal:
					BinaryPrimitives.WriteInt32LittleEndian(bytes.AsSpan(movieValueMetadataAttribute.Offset), intVal);
					break;
				case uint uintVal:
					BinaryPrimitives.WriteUInt32LittleEndian(bytes.AsSpan(movieValueMetadataAttribute.Offset), uintVal);
					break;
				case ushort ushortVal:
					BinaryPrimitives.WriteUInt16LittleEndian(bytes.AsSpan(movieValueMetadataAttribute.Offset),
						ushortVal);
					break;
				case short shortVal:
					BinaryPrimitives.WriteInt16LittleEndian(bytes.AsSpan(movieValueMetadataAttribute.Offset), shortVal);
					break;
				case string stringVal:

					if (movieValueMetadataAttribute.StringEncoding == StringEncodings.Invalid)
						throw new MovieSerializationException($"Expected {nameof(StringEncodings)}");

					var stringBytes = movieValueMetadataAttribute.StringEncoding.ToEncoding()
						.GetBytes(stringVal);

					Array.Copy(stringBytes, 0, bytes, movieValueMetadataAttribute.Offset, stringBytes.Length);

					break;
				default:
					throw new MovieSerializationException($"Can't resolve type \"{val.GetType()}\"");
			}
		}

		// try to find the end index of header
		var movieMetadataAttribute = typeof(Movie).GetCustomAttribute<MovieMetadataAttribute>();

		if (movieMetadataAttribute == null)
			throw new MovieSerializationException($"{nameof(Movie)} has no {nameof(MovieMetadataAttribute)}");

		var endOfHeader = movieMetadataAttribute.EndOfHeader;

		// get the amount of samples
		int? samples = null;
		foreach (var controller in movie.Controllers)
			if (controller.IsPresent)
				samples = controller.Samples.Count;

		if (samples == null) throw new MovieSerializationException("There are no controllers connected");


		// store flatten all controller inputs for connected controllers
		// TODO: O(1) implementation somehow
		List<uint> flattenedSamples = new();

		// aggregate...
		for (var i = 0; i < samples; i++)
		for (var j = 0; j < Movie.MaxControllers; j++)
			if (movie.Controllers[j].IsPresent)
				flattenedSamples.Add(movie.Controllers[j].Samples[i].Raw);


		// convert the flattened samples into bytes
		var flattenedSamplesAsBytes =
			MemoryMarshal.Cast<uint, byte>(CollectionsMarshal.AsSpan(flattenedSamples))
				.ToArray();

		Array.Copy(flattenedSamplesAsBytes, 0, bytes, endOfHeader, flattenedSamplesAsBytes.Length);

		return bytes;
	}

	/// <summary>
	///     Creates a <see cref="Movie" /> from a <see cref="ReadOnlySpan{T}" /> of <see cref="byte" />s
	/// </summary>
	/// <param name="bytes">The raw data to construct the <see cref="Movie" /> from</param>
	/// <param name="movieDeserializationOptions">
	///     The options to be considered when constructing the <see cref="Movie" />
	/// </param>
	/// <returns>The created <see cref="Movie" /></returns>
	/// <exception cref="MovieDeserializationException"></exception>
	/// <exception cref="UnreachableException"></exception>
	public static Movie FromBytes(ReadOnlySpan<byte> bytes,
		MovieDeserializationOptions? movieDeserializationOptions = null)
	{
		movieDeserializationOptions ??= new MovieDeserializationOptions();

		Movie movie = new();

		// loop through all properties on Movie
		foreach (var property in typeof(Movie).GetProperties(BindingFlags.Instance |
		                                                     BindingFlags.DeclaredOnly |
		                                                     BindingFlags.NonPublic | BindingFlags.Public))
		{
			if (property.GetCustomAttribute(typeof(MovieValueMetadataAttribute)) is not MovieValueMetadataAttribute
			    movieValueMetadataAttribute)
			{
				Debug.Print($"Skipping property without {nameof(MovieValueMetadataAttribute)}...");
				continue;
			}

			if (!movieValueMetadataAttribute.IsDeserializable)
			{
				Debug.Print("Skipping non-deserializable property...");
				continue;
			}

			var propertyType = property.PropertyType;

			string GetStringFromSlice(StringEncodings stringEncodings, ReadOnlySpan<byte> bytes)
			{
				var str = stringEncodings.ToEncoding().GetString(bytes);

				if (movieDeserializationOptions.Value.SimplifyNullTerminators)
				{
					// we need to unfuck the horrible null terminators

					var firstNullTerminator = str.IndexOf((char)0x00);

					if (firstNullTerminator != -1) str = str[..firstNullTerminator];
				}

				return str;
			}

			// computed value of type = propertyType from raw data in computedValue
			// TODO: there has to be a native API for this
			var computedValue = propertyType == typeof(byte)
				? bytes[movieValueMetadataAttribute.Offset]
				: propertyType == typeof(ushort)
					? MemoryMarshal.Read<ushort>(bytes.Slice(movieValueMetadataAttribute.Offset, sizeof(ushort)))
					: propertyType == typeof(uint)
						? MemoryMarshal.Read<uint>(bytes.Slice(movieValueMetadataAttribute.Offset, sizeof(uint)))
						: propertyType == typeof(int)
							? MemoryMarshal.Read<int>(bytes.Slice(movieValueMetadataAttribute.Offset, sizeof(int)))
							: propertyType == typeof(string)
								? (object)GetStringFromSlice(movieValueMetadataAttribute.StringEncoding, bytes.Slice(
									movieValueMetadataAttribute.Offset,
									movieValueMetadataAttribute.Length))
								: throw new MovieDeserializationException($"Can't resolve type \"{propertyType}\"");

			if (computedValue == null)
				throw new UnreachableException(
					$"ComputedValue was null, but no {nameof(MovieDeserializationException)} was thrown");

			property.SetValue(movie, computedValue);
		}

		// construct and add controllers
		movie.controllers = new Controller[Movie.MaxControllers];
		for (var i = 0; i < movie.controllers.Length; i++)
		{
			movie.controllers[i] = new Controller(movie, i);
			if (movie.controllers[i].IsPresent) movie.controllers[i].Samples = new List<Sample>();
		}

		// try to find the end index of header
		var movieMetadataAttribute = typeof(Movie).GetCustomAttribute<MovieMetadataAttribute>();

		if (movieMetadataAttribute == null)
			throw new MovieDeserializationException($"{nameof(Movie)} has no {nameof(MovieMetadataAttribute)}");

		var endOfHeader = movieMetadataAttribute.EndOfHeader;

		var rawSamples = MemoryMarshal.Cast<byte, uint>(bytes[endOfHeader..]);

		if (rawSamples.Length == 0) throw new MovieDeserializationException($"{nameof(Movie)} has no samples?");

		// TODO: optimize to be O(1)
		var sampleIndex = 0;
		while (sampleIndex < rawSamples.Length)
			for (var j = 0; j < Movie.MaxControllers; j++)
				if (movie.Controllers[j].IsPresent)
				{
					movie.Controllers[j].Samples.Add(new Sample(rawSamples[sampleIndex]));
					sampleIndex++;
				}

		//for (int n = 0; n < Movie.MaxControllers; n++)
		//{
		//	if (movie.Controllers[n].IsPresent)
		//	{
		//		movie.Controllers[n].Samples = RefEnumerable<uint>.DangerousCreate(ref MemoryMarshal.GetReference(rawSamples), rawSamples.Length / (n + 2), (n + 2)).ToArray().ToList().ConvertAll(x => new Sample(x));
		//	}
		//}

		return movie;
	}
}