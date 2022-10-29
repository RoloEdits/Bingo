using Bingo.Domain.Errors;
using Bingo.Domain.ValueObjects;

namespace Bingo.Domain.Tests;

public class KeyTests
{
    [Fact]
    void ValidIndex()
    {
        var input = "YYYYYYYYY";
        var key = new Key(input, 3, 3);

        var count = 0;

        foreach (var square in key)
        {
            Assert.Equal(input[count], square);
            count++;
        }

        Assert.Equal(key.Length, count);
    }

     [Fact]
    void ShouldBe_ValidAmount_TooMany()
    {
        Assert.ThrowsAny<InvalidSquareAmountException>( () => { var key = new Key("YYYYYYYYYY", 3, 3);});
    }

    [Fact]
    void ShouldBeValidAmount_NotEnough()
    {
        Assert.ThrowsAny<InvalidSquareAmountException>( () => { var key = new Key("YYYYYYY", 3, 3);});
    }

    [Fact]
    void ShouldBe_OnlyHaveTwoMaxUniqueChars()
    {
        Assert.ThrowsAny<InvalidUniqueCharAmountException>( () => { var key = new Key("NYNPYNMMY", 3, 3);});
    }
}
