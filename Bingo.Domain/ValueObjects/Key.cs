using System.Net.Http.Headers;
using System.Transactions;
using System.Xml;
using Bingo.Domain.Errors;

namespace Bingo.Domain.ValueObjects;

public sealed record Key
{
    private readonly char[,] _key;

    private readonly byte _rows;
    private readonly byte _columns;
    public char this[int row, int column] => _key[row, column];

    public Key(string key, byte rows, byte columns)
    {
        IsValidKey(key, rows, columns);

        _key = Utilities.SpanTo2DArray<char>(key, rows, columns);
        _rows = rows;
        _columns = columns;
    }

    public static implicit operator char[,](Key key)
    {
        var converted = new char[key._rows, key._columns];

        for (var row = 0; row < key._rows; row++)
        {
            for (var column = 0; column < key._columns; column++)
            {
                converted[row, column] = key[row, column];
            }
        }

        return converted;
    }

    private static void IsValidKey(string key, byte rows, byte columns)
    {
        KeyIsValidAmount(key, rows, columns);
        KeyHasValidAmountOfUniqueChars(key);
    }

    private static void KeyIsValidAmount(string key, byte rows, byte columns)
    {
        if (key.Length != rows * columns)
        {
            throw new InvalidSquareAmountException();
        }
    }

    private static void KeyHasValidAmountOfUniqueChars(string key)
    {
        char uniqueOne = key[0];
        char? uniqueTwo = null;

        foreach (var square in key)
        {
            if (uniqueTwo is not null)
            {
                if (square != uniqueOne && square != uniqueTwo)
                {
                    throw new InvalidUniqueCharAmountException();
                }
            }

            if (uniqueTwo is null)
            {
                if (square != uniqueOne)
                {
                    uniqueTwo = square;
                }
            }
        }
    }
}
