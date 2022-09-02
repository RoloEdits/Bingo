namespace Bingo;

public class Config
{
    public byte Columns { get; set; } = 4;
    public byte Rows { get; set; } = 3;
    public byte BaseSquareValue { get; set; } = 10;
    public byte RowValueOffset { get; set; } = 20;
    public byte BonusColumns { get; set; } = 1;
    public byte BonusMultiplier { get; set; } = 2;
    public char BonusSkipChar { get; set; } = 'P';

    public Config()
    {

    }

    public Config(byte columns, byte rows, byte baseSqaurevalue, byte rowOffsetValue, byte bonusColumns, byte bonusMultiplier)
    {
        Columns = columns;
        Rows = rows;
        BaseSquareValue = baseSqaurevalue;
        RowValueOffset = rowOffsetValue;
        BonusColumns = bonusColumns;
        BonusMultiplier = bonusMultiplier;
    }
}