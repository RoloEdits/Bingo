namespace Bingo.Domain.Models;

using Bingo.Domain;

public interface ICard
{
    public byte Columns { get; init; }
    public byte Rows { get; init; }
    public byte TotalSquares { get; init; }
    public byte BaseSquareValue { get; init; }
    public int RowValueOffset { get; init; }
    public byte BonusColumns { get; init; }
    public byte BonusMultiplier { get; init; }
    public char BonusSkipChar { get; init; }
    public List<string> RowLabels { get; init; }
    public List<string> SquareLabels { get; init; }

}
