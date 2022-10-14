using BenchmarkDotNet.Attributes;
using Bingo.Domain.ValueObjects;
using Bingo.Library;

namespace Bingo.Benchmarks;

[MemoryDiagnoser]
[RankColumn]
public class GameBenchmark
{
    private static readonly Settings Settings = new()
    {
        WillLogStats = false,
        WillCountAllSameGuessersInStats = false
    };

    // 3x3
    private const string Key3X3 = "YYYYYYYYY";
    private static Dictionary<string, string> Players3X3 => new() { { "Rolo", Key3X3 } };
    private static readonly Card Card3X3 = new(3, 3, 10, 0, 0, 1);

    private static readonly Guess _guess = new Guess(Key3X3, Card3X3.Rows, Card3X3.Columns);

    private static List<Player> PlayersList3X3 => new()
    {
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
        new Player("Rolo", _guess),
    };

    private static readonly Game Game3X3 = new(Key3X3, Card3X3, Settings, Players3X3);

    // 5x5
    private const string Key5X5 = "YYYYYYYYYYYYYYYYYYYYYYYYY";
    private static Dictionary<string, string> Players5X5 => new() { { "Rolo", Key5X5 } };
    private static readonly Card Card5X5 = new(5, 5, 10, 0, 0, 0);
    private static readonly Game Game5X5 = new(Key5X5, Card5X5, Settings, Players5X5);

    // 7x7
    private const string Key7X7 =
        "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";
    private static Dictionary<string, string> Players7X7 => new() { { "Rolo", Key7X7 } };
    private static readonly Card Card7X7 = new(7, 7, 10, 0, 0, 0);
    private static readonly Game Game7X7 = new(Key7X7, Card7X7, Settings, Players7X7);

    [Benchmark]
    public void CalculateScoreBenchmarkFor3X3()
    {
        foreach (var player in PlayersList3X3)
        {
            Game3X3.CalculateScore(player);
        }

    }

    // [Benchmark]
    public void CalculateScoreBenchmarkFor5X5()
    {
        Game5X5.CalculateScore(Game5X5.Players[0]);
    }

    // [Benchmark]
    public void CalculateScoreBenchmarkFor7X7()
    {
        Game7X7.CalculateScore(Game7X7.Players[0]);
    }

    [Benchmark]
    public async Task CalculateScoreBenchmarkFor3X3Async()
    {
        var task = new List<Task>();
        foreach (var player in PlayersList3X3)
        {
            task.Add(Task.Run(() => Game3X3.CalculateScore(player)));
        }

        await Task.WhenAll(task);
    }
}
