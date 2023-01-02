using System.Text;

namespace MupenUtilitiesRedux.Services.Abstractions;

/// <summary>
///     An extension <see langword="class" /> which provides wrapper functions for <see cref="IFile" />
/// </summary>
public static class FileExtensions
{
	/// <summary>
	///     Reads an <see cref="IFile" />'s into a <see cref="byte" /> buffer and returns it
	/// </summary>
	/// <param name="file">The <see cref="IFile" /> to be read</param>
	/// <returns>The <paramref name="file" />'s contents as <see cref="byte" />s</returns>
	public static async Task<byte[]?> ReadAllBytes(this IFile file)
	{
		var stream = await file.OpenStreamForReadAsync();

		var fileProperties = await file.GetPropertiesAsync();
		var buffer = new byte[fileProperties.Size];

		await using (stream)
		{
			// FIXME: verify that enough bytes were read
			var read = stream.Read(buffer, 0, buffer.Length);
			if (read != buffer.Length) return null;
		}

		return buffer;
	}


	/// <summary>
	///     Reads an <see cref="IFile" />'s into a <see cref="string" /> and returns it
	/// </summary>
	/// <param name="file">The <see cref="IFile" /> to be read</param>
	/// <returns>The <paramref name="file" />'s contents as a <see cref="string" /></returns>
	public static async Task<string?> ReadAllText(this IFile file, Encoding encoding)
	{
		var bytes = await ReadAllBytes(file);

		if (bytes != null) return encoding.GetString(bytes);

		return null;
	}
}