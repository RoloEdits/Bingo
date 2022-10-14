using System.Buffers;
using Bingo.Domain;
using Bingo.Domain.Models;

namespace Bingo.Library;

public sealed class Card : ICard
{
    public byte Columns { get; }
    public byte Rows { get; }
    public byte TotalSquares { get; }
    public byte BaseSquareValue { get; }
    public int RowValueOffset { get; }
    public byte BonusColumns { get; }
    public byte BonusMultiplier { get; }
    public char BonusSkipChar { get; }
    public char[] RowLabels { get; }
    public Square[,] SquareLabels { get; }

    public Card(byte columns, byte rows, byte baseSquareValue, int rowOffsetValue, byte bonusColumns, byte bonusMultiplier, char bonusSkipChar = 'P')
    {
        // TODO: Add defaults so that when there is no bonus there is still the option to skip squares. Just that when there is a bonus, you can only skip on those squares.
        Columns = columns;
        Rows = rows;
        BaseSquareValue = baseSquareValue;
        RowValueOffset = rowOffsetValue;
        BonusColumns = bonusColumns;
        BonusMultiplier = bonusMultiplier;
        TotalSquares = (byte)(columns * rows);
        BonusSkipChar = bonusSkipChar;
        RowLabels = Label.Rows[..rows];
        SquareLabels = Label.SquareGenerator(rows, columns, bonusColumns);
    }
}
