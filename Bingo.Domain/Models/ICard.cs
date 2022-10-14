namespace Bingo.Domain.Models;

using Bingo.Domain;

public interface ICard
{
    public byte Columns { get; }
    public byte Rows { get; }
    public byte TotalSquares { get; }
    public byte BaseSquareValue { get; }
    public int RowValueOffset { get; }
    public byte BonusColumns { get; }
    public byte BonusMultiplier { get;}
    public char BonusSkipChar { get; }
    public char[] RowLabels { get;}
    public Square[,] SquareLabels { get; }
}
