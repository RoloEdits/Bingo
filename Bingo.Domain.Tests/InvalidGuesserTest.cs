namespace Bingo.Domain.Tests;


public class InvalidGuesserTest
{
    [Theory]
    [InlineData("Rolo", 12)]
    public void InvalidGuesser(string name, int guessAmount)
    {
        var test = new InvalidGuesser(1, name, guessAmount);

        Assert.Equal(name, test.Name);
        Assert.Equal(guessAmount, test.GuessAmount);
    }
}
