using Bingo.Domain.Models;

namespace Bingo.Domain;

public static class Label
{
    public static List<string> Rows(byte rows)
    {
        var label = new List<string>(rows);

        for (var letter = 'A'; letter <= 'Z'; letter++)
        {
            if (label.Count != rows)
            {
                label.Add($"{letter}");
            }
        }
        return label;
    }
    public static Square[,] Squares(byte rows, byte columns, byte bonusColumns)
    {
        var rowLabels = Rows(rows);
        var result = new Square[rows, columns];

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                var isBonus = (column + bonusColumns) >= columns;
                result[row, column] = new Square($"{rowLabels[row]}{column + 1}", isBonus);
            }
        }
        return result;
    }
}
