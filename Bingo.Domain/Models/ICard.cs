namespace Bingo.Domain.Models;

using Bingo.Domain;

public interface ICard
{
	public int Columns { get; }
	public int Rows { get; }
	public int TotalSquares { get; }
	public int BaseSquareValue { get; }
	public int RowValueOffset { get; }
	public int BonusColumns { get; }
	public int BonusMultiplier { get; }
	public char BonusSkipChar { get; }
	public char[] RowLabels { get; }
	public Square[,] SquareLabels { get; }
}