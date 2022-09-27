namespace Bingo.Domain.Tests;

public class LabelTest
{
    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void RowLabelCreating_ShouldStopAtPassedValue(byte rows)
    {
        var result = Label.Rows(rows);

        Assert.Equal(rows, result.Count);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void RowLabelCreating_ShouldContainExpectedValues(byte rows)
    {
        var match = new List<string>() { "A", "B", "C", "D", "E", "F", "G" };
        var result = Label.Rows(rows);

        for (int i = 0; i < rows; i++)
        {
            Assert.Equal(match[i], result[i]);
        }
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    [InlineData(5, 5)]
    [InlineData(6, 6)]
    [InlineData(7, 7)]
    public void SquareLabelCreating_ShouldStopAtPassedValue(byte rows, byte columns)
    {
        var result = Label.Squares(rows, columns);

        Assert.Equal(rows * columns, result.Count);
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 4)]
    [InlineData(5, 5)]
    [InlineData(6, 6)]
    [InlineData(7, 7)]
    public void SquareLabelCreating_ShouldContainExpectedValues(byte rows, byte columns)
    {
        var match = new List<List<string>>()
        {
            new List<string>{"A1", "A2","A3","A4","A5","A6","A7"},
            new List<string>{"B1", "B2","B3","B4","B5","B6","B7"},
            new List<string>{"C1", "C2","C3","C4","C5","C6","C7"},
            new List<string>{"D1", "D2","D3","D4","D5","D6","D7"},
            new List<string>{"E1", "E2","E3","E4","E5","E6","E7"},
            new List<string>{"F1", "F2","F3","F4","F5","F6","F7"},
            new List<string>{"G1", "G2","G3","G4","G5","G6","G7"}
        };

        var result = Label.Squares(rows, columns);

        var count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Assert.Equal(match[i][j], result[count]);
                count++;
            }
        }
    }
}