namespace Bingo.Library;

public interface IPlayer
{
    public string Name { get; init; }
    public string Guess { get; init; }
    public long Score { get; set; }
    public bool IsAllSameGuess { get; init; }
}