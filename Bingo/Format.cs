namespace Bingo;

public record Format
{
    public byte Columns { get; init; }
    public byte Rows { get; init; }
    public byte BaseSquareValue { get; init; }
    public byte RowValueOffset { get; init; }
    public byte BonusColumns { get; init; }
    public byte BonusMultiplier { get; init; }
    public char BonusSkipChar { get; init; }

    public Format()
    {

    }

    public Format(byte columns, byte rows, byte baseSqaurevalue, byte rowOffsetValue, byte bonusColumns, byte bonusMultiplier)
    {
        Columns = columns;
        Rows = rows;
        BaseSquareValue = baseSqaurevalue;
        RowValueOffset = rowOffsetValue;
        BonusColumns = bonusColumns;
        BonusMultiplier = bonusMultiplier;

        BonusSkipChar = 'P';
    }
}