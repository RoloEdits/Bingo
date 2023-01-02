using Bingo.Domain.Models;

namespace Bingo.Core;

public sealed record Card
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

	public Card(int columns, int rows, int baseSquareValue, int rowOffsetValue, int bonusColumns, int bonusMultiplier = 1, char bonusSkipChar = 'P')
	{
		Columns = columns;
		Rows = rows;
		BaseSquareValue = baseSquareValue;
		RowValueOffset = rowOffsetValue;
		BonusColumns = bonusColumns;
		BonusMultiplier = BonusColumns == 0 ? (byte)1 : bonusMultiplier;
		TotalSquares = (byte)(columns * rows);
		BonusSkipChar = bonusSkipChar;
		RowLabels = Label.Rows[..rows];
		SquareLabels = Label.SquareGenerator(rows, columns, bonusColumns);
	}
}
