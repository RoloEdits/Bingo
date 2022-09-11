namespace Bingo.Library;

public interface ICard : IGrid
{
    public byte Columns { get; init; }
    public byte Rows { get; init; }
    public short TotalSquares { get; init; }
    public byte BaseSquareValue { get; init; }
    public int RowValueOffset { get; init; }
    public byte BonusColumns { get; init; }
    public byte BonusMultiplier { get; init; }
    public char BonusSkipChar { get; init; }
}
