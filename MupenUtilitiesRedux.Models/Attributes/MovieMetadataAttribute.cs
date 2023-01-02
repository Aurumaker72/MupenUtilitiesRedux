namespace MupenUtilitiesRedux.Models.Attributes;

/// <summary>
///     An <see cref="Attribute" /> which stores data related to memory regions in a <see cref="Movie" />
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
internal sealed class MovieMetadataAttribute : Attribute
{
	/// <summary>
	///     The end of the <see cref="Movie" />'s header as a <see cref="byte" /> index into its raw data
	/// </summary>
	public int EndOfHeader { get; init; }
}