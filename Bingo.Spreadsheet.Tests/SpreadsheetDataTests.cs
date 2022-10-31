namespace Bingo.Spreadsheet.Tests;

public class UnitTest1
{
    [Fact]
    public void HashSetDuplicationProtection()
    {
        // Arrange
        var data = new SpreadsheetData(1, "Rolo", "YYYYYYYYY");
        var set = new HashSet<SpreadsheetData>(2);

        // Act
        set.Add(data);

        // Assert
        Assert.False(set.Add(data));
        Assert.Single(set);
    }

    [Fact]
    public void HashSetShouldHaveTwo()
    {
        // Arrange
        var rolo = new SpreadsheetData(1, "Rolo", "YYYYYYYYY");
        var rick = new SpreadsheetData(2, "Rick", "YYYYYYYYY");
        var set = new HashSet<SpreadsheetData>(2);

        // Act
        set.Add(rolo);
        set.Add(rick);

        // Assert
        Assert.Equal(2, set.Count);
    }
}
