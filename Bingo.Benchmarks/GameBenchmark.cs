using BenchmarkDotNet.Attributes;
using Bingo.Core;

namespace Bingo.Benchmarks;

[MemoryDiagnoser]
[RankColumn]
public class GameBenchmark
{
    [Benchmark]
    public void CalculateScoreBenchmarkFor3X3()
    {
        const string input = "YYYYYYYYY";
        var card = new Card(3, 3, 10, 0, 0, 1);
        var player = new Dictionary<string, string> { { "Rolo", input } };
        var game = new Game(input, card, new Settings());

        game.AddPlayers(player);
        game.Play();
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor5X5()
    {
        const string input = "YYYYYYYYYYYYYYYYYYYYYYYYY";
        var card = new Card(5, 5, 10, 0, 0, 1);
        var player = new Dictionary<string, string> { { "Rolo", input } };
        var game = new Game(input, card, new Settings());

        game.AddPlayers(player);
        game.Play();
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor7X7()
    {
        const string input = "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";
        var card = new Card(7, 7, 10, 0, 0, 1);
        var player = new Dictionary<string, string> { { "Rolo", input } };
        var game = new Game(input, card, new Settings());

        game.AddPlayers(player);
        game.Play();
    }
}
