using Bingo.Domain.Errors;

namespace Bingo.Domain.ValueObjects;

public sealed record Guess
{
    private readonly char[,] _guess;
    private readonly byte _rows;
    private readonly byte _columns;

    public readonly bool IsAllSame;
    public readonly byte Length;
    public char this[int row, int column] => _guess[row, column];

    public Guess(string guess, byte rows, byte columns)
    {
        IsValidGuess(guess, rows, columns);

        _guess = Utilities.SpanTo2DArray<char>(guess, rows, columns);
        _rows = rows;
        _columns = columns;
        Length = (byte)guess.Length;

        IsAllSame = CheckIsAllSame(guess);
    }

    public static implicit operator char[,](Guess guess)
    {
        var converted = new char[guess._rows, guess._columns];

        for (var row = 0; row < guess._rows; row++)
        {
            for (var column = 0; column < guess._columns; column++)
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
};
