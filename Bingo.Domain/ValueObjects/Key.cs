using System.Collections;
using System.Net.Http.Headers;
using System.Transactions;
using System.Xml;
using Bingo.Domain.Errors;

namespace Bingo.Domain.ValueObjects;

public sealed record Key : IEnumerable<char>
{
    private readonly char[,] _key;

    public readonly byte Rows;
    public readonly byte Columns;
    public readonly int Length;
    public char this[int row, int column] => _key[row, column];

    public Key(string key, byte rows, byte columns)
    {
        IsValidKey(key, rows, columns);

        _key = Utilities.SpanTo2DArray<char>(key, rows, columns);
        Rows = rows;
        Columns = columns;
        Length = Rows * Columns;
    }

    public static implicit operator char[,](Key key)
    {
        var converted = new char[key.Rows, key.Columns];

        for (var row = 0; row < key.Rows; row++)
        {
            for (var column = 0; column < key.Columns; column++)
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

    public IEnumerator<char> GetEnumerator()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                yield return _key[row, column];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
