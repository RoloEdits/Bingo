using BenchmarkDotNet.Attributes;
using Bingo.Core;
using Bingo.Spreadsheet;

namespace Bingo.Benchmarks;

[MemoryDiagnoser]
[RankColumn]
public class GameBenchmark
{
    [Benchmark]
    public void CalculateScoreBenchmarkFor3X3()
    {
        const string input = "YYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(3)
            .AddColumns(3)
            .AddBaseSquareValue(10)
            .AddRowOffset(0)
            .AddBonusColumns(0)
            .Build();

        var settings = new Settings(true, false);

        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", input) };

        var game = new GameBuilder()
            .AddKey(input)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor5X5()
    {
        const string input = "YYYYYYYYYYYYYYYYYYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(5)
            .AddColumns(5)
            .AddBaseSquareValue(10)
            .AddRowOffset(0)
            .AddBonusColumns(0)
            .Build();

        var settings = new Settings(true, false);

        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", input) };

        var game = new GameBuilder()
            .AddKey(input)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();
    }

    [Benchmark]
    public void CalculateScoreBenchmarkFor7X7()
    {
        const string input = "YYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(7)
            .AddColumns(7)
            .AddBaseSquareValue(10)
            .AddRowOffset(0)
            .AddBonusColumns(0)
            .Build();

        var settings = new Settings(true, false);

        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", input) };

        var game = new GameBuilder()
            .AddKey(input)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();
    }
}
