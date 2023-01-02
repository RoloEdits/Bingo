

namespace Bingo.Core.Tests;

public sealed class GameTest
{
    [Fact]
    public void CalculatePlayerScoreTest_Max450()
    {
        const string key = "YYYYYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(3)
            .AddColumns(4)
            .AddBaseSquareValue(10)
            .AddRowOffset(20)
            .AddBonusColumns(1)
            .AddBonusMultiplier(2)
            .Build();

        var settings = new Settings(true, false);
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", key) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        // Assert
        Assert.Equal(450, game.Players[0].Score);
    }

    [Fact]
    public void CalculatePlayerScoreTest_Min450()
    {
        const string key = "YYYYYYYYYYYY";
        const string guess = "NNNNNNNNNNNN";

        var card = new CardBuilder()
            .AddRows(3)
            .AddColumns(4)
            .AddBaseSquareValue(10)
            .AddRowOffset(20)
            .AddBonusColumns(1)
            .AddBonusMultiplier(2)
            .Build();

        var settings = new Settings(true, false);
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", guess ) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        Assert.Equal(-450, game.Players[0].Score);
    }

    [Fact]
    public void CalculatePlayerScoreTest_ShouldEqual10_WhenOnlyFreeSpaceIsGiven()
    {
        const string key = "YYYYYYYYYYYYYYYYYYYYYYYYY";
        const string guess = "PPPPPPPPPPPPYPPPPPPPPPPPP";

        var card = new CardBuilder()
            .AddRows(5)
            .AddColumns(5)
            .AddBaseSquareValue(10)
            .AddRowOffset(0)
            .AddBonusColumns(0)
            .Build();

        var settings = new Settings(true, true);
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", guess ) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        Assert.Equal(10, game.Players[0].Score);
    }
}
