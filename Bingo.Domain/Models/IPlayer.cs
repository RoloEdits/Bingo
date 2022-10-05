namespace Bingo.Domain.Models;

public interface IPlayer
{
    public string Name { get; }
    public char[,] Guess { get; }
    public long Score { get; set; }
    public bool IsAllSameGuess { get; }
    public Dictionary<string, short> ResultPerSquare { get; }
}
