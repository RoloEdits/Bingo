using Bingo.Domain;
using Bingo.Write;

namespace Bingo.Markdown.Tests;

public class TableTest
{
    [Fact]
    public void TableShouldMatch()
    {
        // Arrange
        var table = new Table(3, 3, 0);
        var list = new List<char>(){ 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y' }.ListTo2DArray(3, 3);
        const string expected = @"|  | 1 | 2 | 3 |
 :---: | :---: | :---: | :---: |
| **A** | Y | Y | Y |
| **B** | Y | Y | Y |
| **C** | Y | Y | Y |
";
        // Act
        var construct = table.Create<char>("", list);

        // Assert
        Assert.Equal(expected, construct);
    }
}
