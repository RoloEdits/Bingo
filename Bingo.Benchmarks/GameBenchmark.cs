using BenchmarkDotNet.Attributes;
using Bingo.Library;

namespace Bingo.Benchmarks;

[MemoryDiagnoser]
[RankColumn]
public class GameBenchmark
{
    // 4x3
    private const string Key4X3 = "YYYYYYNYYYYY";
    private static readonly Card Card4X3 = new Card(4, 3, 10, 20, 1, 2);
    private static readonly Game Game4X3 = new Game(Key4X3, Card4X3, false);

    // 7x5
    private const string Key7X5 = "YYYYYYYYYYYYYYYNYYYYYYYYYYYYYYYYYYY";
    private static readonly Card Format7X5 = new Card(7, 5, 10, 20, 2, 2);
    private static readonly Game Game7X5 = new Game(Key7X5, Format7X5, false);

    // 10x10
    private const string Key10X10 =
        "NYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";

    private static readonly Card Card10X10 = new Card(10, 10, 10, 20, 3, 2);
    private static readonly Game Game10X10 = new Game(Key10X10, Card10X10, false);

    [Benchmark]
    public void CalculateScoreBenchmarkFor4X3()
    {
        Game4X3.CalculatePlayerScore(Key4X3, false);
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor7X5()
    {
        Game7X5.CalculatePlayerScore(Key7X5, false);
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor10X10()
    {
        Game10X10.CalculatePlayerScore(Key10X10, false);
    }
}
