namespace Bingo.Library;

public interface IGame
{
    public List<Player> Players { get; set; }
    public ICard Card { get; init; }
    public string Key { get; init; }

    public long CalculatePlayerScore(string guess, bool isAllSameGuess);
}
