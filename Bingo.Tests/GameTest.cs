using Bingo.Library;

namespace Bingo.Tests;

public sealed class GameTest
{
    [Fact]
    public void CalculatePlayerScoreTest_Max450()
    {
        const string key = "YYYYYYYYYYYY";
        const string guess = "YYYYYYYYYYYY";

        var card = new Card(4, 3, 10, 20, 1, 2);
        var game = new Game(key, card);

        var result = game.CalculatePlayerScore(guess, true);

        Assert.Equal(450, result);
    }

    [Fact]
    public void CalculatePlayerScoreTest_Min450()
    {
        const string key = "YYYYYYYYYYYY";
        const string guess = "NNNNNNNNNNNN";

        var card = new Card(4, 3, 10, 20, 1, 2);
        var game = new Game(key, card);

        var result = game.CalculatePlayerScore(guess, true);

        Assert.Equal(-450, result);
    }
}