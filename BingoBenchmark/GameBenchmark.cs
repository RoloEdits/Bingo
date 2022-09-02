using BenchmarkDotNet.Attributes;
using Bingo;

namespace BingoBenchmark;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class GameBenchmark
{
    private const string key = "YYYYYYYYYYYY";
    private static readonly Config config = new Config();
    private static readonly Game game = new Game(config, key);

    [Benchmark]
    public void CalculateScoreBenchmark()
    {
        Game.CalculatePlayerScore(game.Key, "YNYNYNYNYNYN", false, game);
    }

}
