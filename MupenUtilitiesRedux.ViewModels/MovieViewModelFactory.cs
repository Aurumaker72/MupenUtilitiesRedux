using MupenUtilitiesRedux.Models;
using MupenUtilitiesRedux.Services;

namespace MupenUtilitiesRedux.ViewModels;

/// <summary>
///     A <see langword="static" /> <see langword="class" /> providing functionality for converting
///     <see cref="MovieViewModel" />s to and from raw data
/// </summary>
public static class MovieViewModelFactory
{
	/// <summary>
	///     Converts a <see cref="MovieViewModel" /> into a <see cref="byte" />[]
	/// </summary>
	/// <param name="movieViewModel">The <see cref="MovieViewModel" /> to be converted</param>
	/// <returns>A <see cref="byte" />[] representation of the <see cref="MovieViewModel" /></returns>
	public static byte[] ToBytes(MovieViewModel movieViewModel)
	{
		return MovieFactory.ToBytes(movieViewModel.Movie);
	}

	/// <summary>
	///     Creates a <see cref="MovieViewModel" /> from a <see cref="ReadOnlySpan{T}" /> of <see cref="byte" />s
	/// </summary>
	/// <param name="bytes">The raw data to construct the <see cref="MovieViewModel" /> from</param>
	/// <returns>The created <see cref="MovieViewModel" /></returns>
	public static MovieViewModel FromBytes(ReadOnlySpan<byte> bytes, ITimerService timerService)
	{
		var movie = MovieFactory.FromBytes(bytes);
		return new MovieViewModel(movie, timerService);
	}
}