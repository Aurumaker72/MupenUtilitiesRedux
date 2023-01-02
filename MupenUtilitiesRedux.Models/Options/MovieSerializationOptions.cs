namespace MupenUtilitiesRedux.Models.Options;

/// <summary>
///     A <see langword="struct" /> which contains options for the <see cref="Movie" /> serialization
/// </summary>
public struct MovieSerializationOptions
{
	public MovieSerializationOptions()
	{
	}

	/// <summary>
	///     The value with which empty space inside the <see cref="Movie" /> should be filled
	/// </summary>
	/// <remarks>
	///     It is recommended to keep this at <c>0</c>, as it can cause undefined behaviour in Mupen64
	/// </remarks>
	public byte EmptyValue { internal get; init; } = 0;
}