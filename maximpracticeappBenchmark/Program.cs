using BenchmarkDotNet.Running;
namespace maximpracticeappBenchmark;
public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<MapBenchmark>();
    }
}