using Bingo.Domain.ValueObjects;

namespace Bingo.Domain.Models;

public interface IPlayer
{
    public string Name { get; }
    public Guess Guess { get; }
    public long Score { get; set; }
    public Dictionary<string, Result> ResultPerSquare { get; }
}