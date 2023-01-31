using BenchmarkDotNet.Attributes;
using MupenUtilitiesRedux.Models.Serializers;

namespace MupenUtilitiesRedux.Models.Benchmarks;

public class DeserializationBenchmark
{
    private byte[]? _data;

    [GlobalSetup]
    public void Setup()
    {
        _data = File.ReadAllBytes("movie.m64");
    }

    [Benchmark]
    public Movie Deserialize()
    {
        return new ReflectionMovieSerializer().Deserialize(_data);
    }
}