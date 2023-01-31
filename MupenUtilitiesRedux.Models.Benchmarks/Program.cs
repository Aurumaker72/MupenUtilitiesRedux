using System.Diagnostics;
using BenchmarkDotNet.Configs;
using MupenUtilitiesRedux.Models.Serializers;

namespace MupenUtilitiesRedux.Models.Benchmarks;

public class Config : ManualConfig
{
    public Config()
    {
        _ = WithOptions(ConfigOptions.Default).WithOptions(ConfigOptions.DisableOptimizationsValidator);
    }
}

internal class Program
{
    public static void Main()
    {
        const int n = 100000;


        var data = File.ReadAllBytes("movie.m64");

        var stopwatch = Stopwatch.StartNew();
        for (var i = 0; i < n; i++) _ = new ReflectionMovieSerializer().Deserialize(data);
        stopwatch.Stop();

        var avg = stopwatch.Elapsed.TotalMilliseconds / n;

        Console.WriteLine(
            $"Time elapsed: {stopwatch.Elapsed.TotalMilliseconds}ms\nIterations: {n}\nAverage: {avg:N2}ms");

        //var summary = BenchmarkRunner.Run<DeserializationBenchmark>();
    }
}