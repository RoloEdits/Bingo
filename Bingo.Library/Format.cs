namespace Bingo.Library;

public class Grid
{
    public byte Columns { get; init; }
    public byte Rows { get; init; }
    public byte BaseSquareValue { get; init; }
    public int RowValueOffset { get; init; }
    public byte BonusColumns { get; init; }
    public byte BonusMultiplier { get; init; }
    public char BonusSkipChar { get; init; }
    public short TotalSquares { get; init; }

    public Grid(byte columns, byte rows, byte baseSquareValue, int rowOffsetValue, byte bonusColumns,
        byte bonusMultiplier)
    {
        Columns = columns;
        Rows = rows;
        BaseSquareValue = baseSquareValue;
        RowValueOffset = rowOffsetValue;
        BonusColumns = bonusColumns;
        BonusMultiplier = bonusMultiplier;
        TotalSquares = (short)(columns * rows);
        BonusSkipChar = 'P';
    }
}
