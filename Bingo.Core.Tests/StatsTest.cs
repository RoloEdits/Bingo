namespace Bingo.Core.Tests;

public sealed class StatsTest
{
    [Fact]
    public void MaxScoreShouldBe450()
    {
        // Arrange
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
        Assert.Equal(450, game.Stats.MaxScorePossible);
    }

    [Fact]
    public void PlayerCountShouldBeOne()
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

        Assert.Equal(1, game.Stats.PlayerCount);
    }

    [Fact]
    public void PlayerScores_ShouldEqual_1Count_450Score()
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

        Assert.Equal(450, game.Stats.PlayerScores[0]);
        Assert.Single(game.Stats.PlayerScores);
    }

    [Fact]
    public void PlayerScoreFreq_ShouldEqual_1CountOf450Score()
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

        Assert.Equal(1L, game.Stats.PlayerScoreFrequency[450]);
    }

    [Fact]
    public void PercentageShouldBe100Percent()
    {
        // Arrange
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


        foreach (var percentage in game.Stats.CorrectGuessesPercentage)
        {
            Assert.Equal(1.0, percentage);
            Assert.Equal("100.00%", $"{percentage:P2}");
        }
    }

    [Fact]
    public void PercentageShouldBe0Percent()
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
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", "NNNNNNNNNNNN" ) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        foreach (var percentage in game.Stats.CorrectGuessesPercentage)
        {
            Assert.Equal(0.0, percentage);
            Assert.Equal("0.00%", $"{percentage:P2}");
        }
    }

    [Fact]
    public void PercentageShouldBe50Percent()
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
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", "NNNNNNNNNNNN" ), new SpreadsheetData(2, "Jon", key) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        // Assert
        foreach (var percentage in game.Stats.CorrectGuessesPercentage)
        {
            Assert.Equal(0.5, percentage);
            Assert.Equal("50.00%", $"{percentage:P2}");
        }
    }

    [Fact]
    public void ShouldBeAllSkips()
    {
        const string key = "YYYYYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(3)
            .AddColumns(4)
            .AddBaseSquareValue(10)
            .AddRowOffset(20)
            .AddBonusColumns(4)
            .AddBonusMultiplier(2)
            .Build();

        var settings = new Settings(true, false);
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", "PPPPPPPPPPPP" ) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        foreach (var data in game.Stats.Skipped)
        {
            var skipped = data.Value.Count;

            Assert.Equal(1, skipped);
        }
    }

    [Fact]
    public void ShouldBeAllWrong()
    {
        const string key = "YYYYYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(3)
            .AddColumns(4)
            .AddBaseSquareValue(10)
            .AddRowOffset(20)
            .AddBonusColumns(4)
            .AddBonusMultiplier(2)
            .Build();

        var settings = new Settings(true, false);
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", "NNNNNNNNNNNN" ) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        foreach (var data in game.Stats.IncorrectGuesses)
        {
            var wrong = data.Value.Count;

            Assert.Equal(1, wrong);
        }
    }

    [Fact]
    public void ShouldBeAllRight()
    {
        const string key = "YYYYYYYYYYYY";

        var card = new CardBuilder()
            .AddRows(3)
            .AddColumns(4)
            .AddBaseSquareValue(10)
            .AddRowOffset(20)
            .AddBonusColumns(4)
            .AddBonusMultiplier(2)
            .Build();

        var settings = new Settings(true, false);
        var players = new HashSet<SpreadsheetData>() { new SpreadsheetData(1, "Rolo", key ) };

        var game = new GameBuilder()
            .AddKey(key)
            .AddCard(card)
            .AddSettings(settings)
            .AddPlayers(players)
            .Build();

        game.Play();

        foreach (var data in game.Stats.CorrectGuesses)
        {
            var right = data.Value.Count;

            Assert.Equal(1, right);
        }
    }
}
