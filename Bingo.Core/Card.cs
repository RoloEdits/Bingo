using System.Buffers;
using Bingo.Domain;
using Bingo.Domain.Models;

namespace Bingo.Core;

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

    public Card(byte columns, byte rows, byte baseSquareValue, int rowOffsetValue, byte bonusColumns, byte bonusMultiplier = 1, char bonusSkipChar = 'P')
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
