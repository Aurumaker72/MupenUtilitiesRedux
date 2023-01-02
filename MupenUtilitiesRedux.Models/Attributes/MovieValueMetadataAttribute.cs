using MupenUtilitiesRedux.Models.Enums;

namespace MupenUtilitiesRedux.Models.Attributes;

/// <summary>
///     An <see cref="Attribute" /> which stores data related to (de-)serialization of fields in a <see cref="Movie" />
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
internal sealed class MovieValueMetadataAttribute : Attribute
{
	/// <summary>
	///     The value's offset relative to <c>0x00000000</c>
	/// </summary>
	public int Offset { get; init; } = -1;

	/// <summary>
	///     <para>The optional <see cref="byte" /> length of the value</para>
	///     If this value is <see langword="null" />, the length will be automatically determined. This property is only
	///     applicable to the <see cref="string" /> type
	/// </summary>
	public int Length { get; init; } = -1;

	/// <summary>
	///     The optional <see cref="StringEncodings" /> of the <see cref="string" /> value
	/// </summary>
	public StringEncodings StringEncoding { get; init; } = StringEncodings.Invalid;

	/// <summary>
	///     Whether this value is deserializable
	/// </summary>
	public bool IsDeserializable { get; init; } = true;
}