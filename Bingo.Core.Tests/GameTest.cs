

namespace Bingo.Core.Tests;

public sealed class GameTest
{
    [Fact]
    public void CalculatePlayerScoreTest_Max450()
    {
        // Arrange
        var input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings(true, false);
        var player = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", input) };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(450, game.Players[0].Score);
    }

    [Fact]
    public void CalculatePlayerScoreTest_Min450()
    {
        //Arrange
        const string key = "YYYYYYYYYYYY";
        const string guess = "NNNNNNNNNNNN";

        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings(true, false);
        var player = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", guess ) };
        var game = new Game(key, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(-450, game.Players[0].Score);
    }

    [Fact]
    public void CalculatePlayerScoreTest_ShouldEqual10_WhenOnlyFreeSpaceIsGiven()
    {
        // Arrange
        const string key = "YYYYYYYYYYYYYYYYYYYYYYYYY";
        const string guess = "PPPPPPPPPPPPYPPPPPPPPPPPP";

        var card = new Card(5, 5, 10, 0, 0);
        var settings = new Settings(true, false);
        var player = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", guess ) };
        var game = new Game(key, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(10, game.Players[0].Score);
    }
}
