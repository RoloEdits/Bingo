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

        var result = game.CalculatePlayerScore(game.Players[0]);

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

        var result = game.CalculatePlayerScore(game.Players[0]);

        Assert.Equal(-450, result);
    }
}