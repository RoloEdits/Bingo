using BenchmarkDotNet.Attributes;
using Bingo.Library;

namespace Bingo.Benchmarks;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class GameBenchmark
{
    // 4x3
    private const string Key4x3 = "YYYYYYNYYYYY";
    private static readonly Card Cardard4x3 = new Card(4, 3, 10, 20, 1, 2);
    private static readonly Game Game4x3 = new Game(Key4x3, Cardard4x3);

    // 7x5
    private const string Key7x5 = "YYYYYYYYYYYYYYYNYYYYYYYYYYYYYYYYYYY";
    private static readonly Card Format7x5 = new Card(7, 5, 10, 20, 2, 2);
    private static readonly Game Game7x5 = new Game(Key4x3, Format7x5);

    // 10x10
    private const string Key10x10 = "NYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";
    private static readonly Card Card10x10 = new Card(10, 10, 10, 20, 3, 2);
    private static readonly Game Game10x10 = new Game( Key10x10, Card10x10);

    [Benchmark]
    public void CalculateScoreBenchmarkFor4x3()
    {
        Game4x3.CalculatePlayerScore(Key4x3,false);
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor7x5()
    {
        Game7x5.CalculatePlayerScore(Key7x5, false);
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor10x10()
    {
        Game10x10.CalculatePlayerScore(Key10x10, false);
    }
}
