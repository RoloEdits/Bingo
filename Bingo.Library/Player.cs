using Bingo.Domain.Models;
using Bingo.Domain.ValueObjects;

namespace Bingo.Library;

public sealed class Player : IPlayer
{
    public string Name { get; }
    public Guess Guess { get; }
    public long Score { get; set; }
    public Dictionary<string, Result> ResultPerSquare { get; }

    public Player(string name, Guess guess)
    {
        Name = name;
        Guess = guess;
        ResultPerSquare = new Dictionary<string, Result>(guess.Length);
    }
}
