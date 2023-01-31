using MupenUtilitiesRedux.Models.Serializers;

namespace MupenUtilitiesRedux.Models.Tests;

public class MovieTest
{
	[Theory]
	[InlineData("Files/super-mario-64-120-star-tas.m64")]
	public void Test_Reserializing_Movie_Produces_Equal_Output(string path)
	{
		var reflectionMovieSerializer = new ReflectionMovieSerializer();
		
		var movieA = reflectionMovieSerializer.Deserialize(File.ReadAllBytes(path));
		var movieBytesA = reflectionMovieSerializer.Serialize(movieA);

		var movieB = reflectionMovieSerializer.Deserialize(movieBytesA);
		var movieBytesB = reflectionMovieSerializer.Serialize(movieB);

		Assert.True(movieBytesA.SequenceEqual(movieBytesB));
	}
}