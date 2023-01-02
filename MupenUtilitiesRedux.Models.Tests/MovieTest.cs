namespace MupenUtilitiesRedux.Models.Tests;

public class MovieTest
{
	[Theory]
	[InlineData("Files/super-mario-64-120-star-tas.m64")]
	public void Test_Reserializing_Movie_Produces_Equal_Output(string path)
	{
		var movieA = MovieFactory.FromBytes(File.ReadAllBytes(path));
		var movieBytesA = MovieFactory.ToBytes(movieA);

		var movieB = MovieFactory.FromBytes(movieBytesA);
		var movieBytesB = MovieFactory.ToBytes(movieB);

		Assert.True(movieBytesA.SequenceEqual(movieBytesB));
	}
}