namespace Bingo.Core;

public class CardBuilder
{
    private int? _columns;
    private int? _rows;
    private int? _baseSquareValue;
    private int? _rowOffset;
    private int? _bonusColumns;
    private int _bonusMultiplier = 1;
    private char _bonusSkipChar = 'P';

    public CardBuilder AddColumns(int columns)
    {
        _columns = columns;
        return this;
    }

    public CardBuilder AddRows(int rows)
    {
        _rows = rows;
        return this;
    }

    public CardBuilder AddBaseSquareValue(int baseSquareValue)
    {
        _baseSquareValue = baseSquareValue;
        return this;
    }

    public CardBuilder AddRowOffset(int rowOffset)
    {
        _rowOffset = rowOffset;
        return this;
    }

    public CardBuilder AddBonusColumns(int bonusColumns)
    {
        _bonusColumns = bonusColumns;
        return this;
    }

    public CardBuilder AddBonusMultiplier(int bonusMultiplier)
    {
        _bonusMultiplier = bonusMultiplier;
        return this;
    }

    public CardBuilder AddBonusSkipChar(string bonusSkipChar)
    {
        _bonusSkipChar = bonusSkipChar.ToUpper()[0];
        return this;
    }

    public Card Build()
    {
        if (_columns is null || _rows is null || _baseSquareValue is null || _rowOffset is null ||
            _bonusColumns is null)
        {
            throw new InvalidOperationException("Not all fields to build a bingo card were given");
        }

        return new Card(
            (int)_columns,
            (int)_rows,
            (int)_baseSquareValue,
            (int)_rowOffset,
            (int)_bonusColumns,
            _bonusMultiplier,
            _bonusSkipChar
        );
    }
}
