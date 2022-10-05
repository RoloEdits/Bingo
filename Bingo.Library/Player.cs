using Bingo.Domain;
using Bingo.Domain.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace Bingo.Library;

public sealed class Player : IPlayer
{
    public string Name { get; init; }
    public char[,] Guess { get; }
    public long Score { get; set; }
    public bool IsAllSameGuess { get; init; }
    public Dictionary<string, short> ResultPerSquare { get; }

    public Player(string name, string guess, byte rows, byte columns)
    {
        Name = name;
        Guess = Utilities.SpanTo2DArray<char>(guess, rows, columns);
        IsAllSameGuess = IsAllSame();
        ResultPerSquare = new Dictionary<string, short>(guess.Length);

        bool IsAllSame()
        {
            for (var i = 1; i < guess.Length; i++)
            {
                if (guess[0] == guess[i])
                {
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
