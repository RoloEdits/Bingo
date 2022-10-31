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

    [Fact]
    public void ObjectsShouldBeEqual()
    {
        // Arrange
        var data1 = new SpreadsheetData(1, "Rolo", "YYYYYYYYY");
        var data2 = new SpreadsheetData(1, "Rolo", "YYYYYYYYY");
        var data3 = new SpreadsheetData(2, "Rolo", "NNNNNNNNN");
        var data4 = new SpreadsheetData(2, "Rick", "NNNNNNNNN");
        var set = new HashSet<SpreadsheetData>(2);

        // Assert
        Assert.Equal(data1, data2);
        Assert.Equal(data1, data3);
        Assert.NotEqual(data1, data4);
    }
}
