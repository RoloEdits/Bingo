using Bingo.Domain.Errors;
using Bingo.Domain.ValueObjects;

namespace Bingo.Domain.Tests;

public class GuessTests
{
    [Fact]
    void ShouldBe_AllSameGuess_True()
    {
        var guess = new Guess("YYYYYYYYY", 3, 3);

        Assert.True(guess.IsAllSame);
    }

     [Fact]
    void ShouldBe_ValidAmount_TooMany()
    {
        Assert.ThrowsAny<InvalidSquareAmountException>( () => { var guess = new Guess("YYYYYYYYYY", 3, 3);});
    }

    [Fact]
    void ShouldBeValidAmount_NotEnough()
    {
        Assert.ThrowsAny<InvalidSquareAmountException>( () => { var guess = new Guess("YYYYYYY", 3, 3);});
    }

    [Fact]
    void ShouldBe_OnlyHaveThreeMaxUniqueChars()
    {
        Assert.ThrowsAny<InvalidUniqueCharAmountException>( () => { var guess = new Guess("NYNPYNMMY", 3, 3);});
    }

}
