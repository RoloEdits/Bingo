namespace Bingo.Domain.Models;

public static class Label
{
    public static readonly char[] Rows = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

    public static readonly string[,] Squares =
    {
        { "A1", "A2", "A3", "A4", "A5", "A6", "A7" },
        { "B1", "B2", "B3", "B4", "B5", "B6", "B7" },
        { "C1", "C2", "C3", "C4", "C5", "C6", "C7" },
        { "D1", "D2", "D3", "D4", "D5", "D6", "D7" },
        { "E1", "E2", "E3", "E4", "E5", "E6", "E7" },
        { "F1", "F2", "F3", "F4", "F5", "F6", "F7" },
        { "G1", "G2", "G3", "G4", "G5", "G6", "G7" }
    };

    public static Square[,] SquareGenerator(byte rows, byte columns, byte bonusColumns)
    {
        var result = new Square[rows, columns];

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                var isBonus = (column + bonusColumns) >= columns;
                result[row, column] = new Square($"{Rows[row]}{column + 1}", isBonus);
            }
        }

        return result;
    }
}