using System.Text;

namespace MupenUtilitiesRedux.Models.Enums;

/// <summary>
///     An <see langword="enum" /> listing text character encodings which respectively map to a <see langword="static" />
///     <see cref="Encoding" /> under the <see cref="Encoding" /> class
/// </summary>
internal enum StringEncodings
{
    Invalid,
    Utf8,
    Ascii
}

/// <summary>
///     An extension <see langword="class" /> providing functionality for mapping <see cref="StringEncodings" /> to
///     <see cref="Encoding" />
/// </summary>
internal static class StringEncodingsExtensions
{
	/// <summary>
	///     Maps <paramref name="stringEncodings" /> to the respective <see cref="Encoding" />
	/// </summary>
	/// <param name="stringEncodings">The <see cref="StringEncodings" /> to map</param>
	/// <returns>
	///     If the <paramref name="stringEncodings" /> can be mapped to a <see cref="Encoding" />, the respective
	///     <see cref="Encoding" />
	///     <para>
	///         Otherwise, <see langword="null" />
	///     </para>
	/// </returns>
	internal static Encoding? ToEncoding(this StringEncodings stringEncodings)
    {
        return stringEncodings switch
        {
            StringEncodings.Utf8 => Encoding.UTF8,
            StringEncodings.Ascii => Encoding.ASCII,
            _ => null
        };
    }
}