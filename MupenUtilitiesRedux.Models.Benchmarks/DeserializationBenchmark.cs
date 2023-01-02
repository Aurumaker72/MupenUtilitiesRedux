using BenchmarkDotNet.Attributes;

namespace MupenUtilitiesRedux.Models.Benchmarks;

public class DeserializationBenchmark
{
	private byte[]? data;

	[GlobalSetup]
	public void Setup()
	{
		data = File.ReadAllBytes("movie.m64");
	}

	[Benchmark]
	public Movie Deserialize()
	{
		return MovieFactory.FromBytes(data);
	}
}