using System.Collections;
using Bingo.Domain.Errors;

namespace Bingo.Domain.ValueObjects;

public sealed record Guess : IEnumerable<char>
{
    private readonly char[,] _guess;
    public readonly byte Rows;
    public readonly byte Columns;

    public readonly bool IsAllSame;
    public readonly byte Length;
    public char this[int row, int column] => _guess[row, column];

    public Guess(string guess, byte rows, byte columns)
    {
        IsValidGuess(guess, rows, columns);

        _guess = Utilities.SpanTo2DArray<char>(guess, rows, columns);
        Rows = rows;
        Columns = columns;
        Length = (byte)guess.Length;

        IsAllSame = CheckIsAllSame(guess);
    }

    public static implicit operator char[,](Guess guess)
    {
        var converted = new char[guess.Rows, guess.Columns];

        for (var row = 0; row < guess.Rows; row++)
        {
            for (var column = 0; column < guess.Columns; column++)
            {
                converted[row, column] = guess[row, column];
            }
        }

        return converted;
    }

    private static void IsValidGuess(string guess, byte rows, byte columns)
    {
        GuessIsValidAmount(guess, rows, columns);
        GuessHasValidAmountOfUniqueChars(guess);
    }

    private static bool CheckIsAllSame(string guess)
    {
        foreach (var square in guess)
        {
            if (guess[0] != square)
            {
                return false;
            }
        }

        return true;
    }

    private static void GuessIsValidAmount(string guess, byte rows, byte columns)
    {
        if (guess.Length != rows * columns)
        {
            throw new InvalidSquareAmountException();
        }
    }

    private static void GuessHasValidAmountOfUniqueChars(string guess)
    {
        char uniqueOne = guess[0];
        char? uniqueTwo = null;
        char? uniqueThree = null;

        for (var square = 1; square < guess.Length; square++)
        {
            if (uniqueThree is not null)
            {
                if (guess[square] != uniqueOne && guess[square] != uniqueTwo && guess[square] != uniqueThree)
                {
                    throw new InvalidUniqueCharAmountException();
                }
            }

            if (uniqueTwo is not null && uniqueThree is null)
            {
                if (guess[square] != uniqueOne && guess[square] != uniqueTwo)
                {
                    uniqueThree = guess[square];
                }
            }

            if (uniqueTwo is null)
            {
                if (guess[square] != uniqueOne)
                {
                    uniqueTwo = guess[square];
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
                yield return _guess[row, column];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
};