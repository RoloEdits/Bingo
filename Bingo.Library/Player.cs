namespace Bingo.Library;

public sealed class Player : IPlayer
{
    public string Name { get; init; }
    public string Guess { get; init; }
    public long Score { get; set; }
    public bool IsAllSameGuess { get; init; }

    public Player(string name, string guess)
    {
        Name = name;
        Guess = guess;
        IsAllSameGuess = IsAllSame(Guess);

        static bool IsAllSame(ReadOnlySpan<char> guess)
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