using MupenUtilitiesRedux.Models.Exceptions;
using MupenUtilitiesRedux.Models.Options;

namespace MupenUtilitiesRedux.Models.Interfaces;

/// <summary>
///     The default <see langword="interface" /> for a service that serializes and de-serializes <see cref="Movie"/>s
/// </summary>
public interface IMovieSerializer
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
    public byte[] Serialize(Movie movie, MovieSerializationOptions? movieSerializationOptions = null);

    /// <summary>
    ///     Creates a <see cref="Movie" /> from a <see cref="ReadOnlySpan{T}" /> of <see cref="byte" />s
    /// </summary>
    /// <param name="bytes">The raw data to construct the <see cref="Movie" /> from</param>
    /// <param name="movieDeserializationOptions">
    ///     The options to be considered when constructing the <see cref="Movie" />
    /// </param>
    /// <returns>The created <see cref="Movie" /></returns>
    public Movie Deserialize(ReadOnlySpan<byte> bytes,
        MovieDeserializationOptions? movieDeserializationOptions = null);
}