namespace Bingo.Core.Tests;

public sealed class StatsTest
{
    [Fact]
    public void MaxScoreShouldBe450()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings();
        var player = new Dictionary<string, string>() { { "Rolo", input } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(450, game.Stats.MaxScorePossible);
    }

    [Fact]
    public void PlayerCountShouldBeOne()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings();
        var player = new Dictionary<string, string>() { { "Rolo", input } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(1, game.Stats.PlayerCount);
    }

    [Fact]
    public void PlayerScores_ShouldEqual_1Count_450Score()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings();
        var player = new Dictionary<string, string>() { { "Rolo", input } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(450, game.Stats.PlayerScores[0]);
        Assert.Single(game.Stats.PlayerScores);
    }

    [Fact]
    public void PlayerScoreFreq_ShouldEqual_1CountOf450Score()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings();
        var player = new Dictionary<string, string>() { { "Rolo", input } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        Assert.Equal(1L, game.Stats.PlayerScoreFrequency[450]);
        Assert.Equal(1L, game.Stats.FullScoreFrequency[450]);
    }

    [Fact]
    public void PercentageShouldBe100Percent()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings(){ WillCountAllSameGuessersInStats = true};
        var player = new Dictionary<string, string>() { { "Rolo", input } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        foreach (var percentage in game.Stats.CorrectGuessesPercentage)
        {
            Assert.Equal(1.0, percentage);
            Assert.Equal("100.00%", $"{percentage:P2}");
        }
    }

    [Fact]
    public void PercentageShouldBe0Percent()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings(){ WillCountAllSameGuessersInStats = true};
        var player = new Dictionary<string, string>() { { "Rolo", "NNNNNNNNNNNN" } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        foreach (var percentage in game.Stats.CorrectGuessesPercentage)
        {
            Assert.Equal(0.0, percentage);
            Assert.Equal("0.00%", $"{percentage:P2}");
        }
    }

    [Fact]
    public void PercentageShouldBe50Percent()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 1, 2);
        var settings = new Settings(){ WillCountAllSameGuessersInStats = true};
        var player = new Dictionary<string, string>() { { "Rolo", "NNNNNNNNNNNN" }, {"Jon", input}};

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
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
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 4, 2);
        var settings = new Settings(){ WillCountAllSameGuessersInStats = true};
        var player = new Dictionary<string, string>() { { "Rolo", "PPPPPPPPPPPP" } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        foreach (var data in game.Stats.Skipped)
        {
            var skipped = data.Value.Count;

            Assert.Equal(1, skipped);
        }
    }

    [Fact]
    public void ShouldBeAllWrong()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 4, 2);
        var settings = new Settings(){ WillCountAllSameGuessersInStats = true};
        var player = new Dictionary<string, string>() { { "Rolo", "NNNNNNNNNNNN" } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        foreach (var data in game.Stats.IncorrectGuesses)
        {
            var wrong = data.Value.Count;

            Assert.Equal(1, wrong);
        }
    }

    [Fact]
    public void ShouldBeAllRight()
    {
        // Arrange
        const string input = "YYYYYYYYYYYY";
        var card = new Card(4, 3, 10, 20, 4, 2);
        var settings = new Settings(){ WillCountAllSameGuessersInStats = true};
        var player = new Dictionary<string, string>() { { "Rolo", input } };

        var game = new Game(input, card, settings);

        // Act
        game.AddPlayers(player);
        game.Play();

        // Assert
        foreach (var data in game.Stats.CorrectGuesses)
        {
            var right = data.Value.Count;

            Assert.Equal(1, right);
        }
    }
}
