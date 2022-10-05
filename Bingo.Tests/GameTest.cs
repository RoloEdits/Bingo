namespace Bingo.Library.Tests;

public sealed class GameTest
{
    [Fact]
    public void CalculatePlayerScoreTest_Max450()
    {
        const string key = "YYYYYYYYYYYY";
        const string guess = "YYYYYYYYYYYY";

        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings()
        {
            WillLogStats = true,
            WillCountAllSameGuessersInStats = true
        };

        var player = new Dictionary<string, string>() { { "Rolo", guess } };

        var game = new Game(key, card, settings, player);

        game.CalculateScore(game.Players[0]);

        var result = game.Players[0].Score;

        Assert.Equal(450, result);
    }

    [Fact]
    public void CalculatePlayerScoreTest_Min450()
    {
        const string key = "YYYYYYYYYYYY";
        const string guess = "NNNNNNNNNNNN";

        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings()
        {
            WillLogStats = true,
            WillCountAllSameGuessersInStats = true
        };

        var player = new Dictionary<string, string>() { { "Rolo", guess } };

        var game = new Game(key, card, settings, player);

        game.CalculateScore(game.Players[0]);

        var result = game.Players[0].Score;

        Assert.Equal(-450, result);
    }

    [Fact]
    public void CalculatePlayerScoreTest_ShouldEqual10_WhenOnlyFreeSpaceIsGiven()
    {
        const string key = "YYYYYYYYYYYYYYYYYYYYYYYYY";
        const string guess = "PPPPPPPPPPPPYPPPPPPPPPPPP";

        var card = new Card(5, 5, 10, 0, 5, 1);
        var settings = new Settings()
        {
            WillLogStats = true,
            WillCountAllSameGuessersInStats = true
        };

        var player = new Dictionary<string, string>() { { "Rolo", guess } };

        var game = new Game(key, card, settings, player);

        game.CalculateScore(game.Players[0]);

        var result = game.Players[0].Score;

        Assert.Equal(10, result);
    }
}
