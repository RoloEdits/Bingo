namespace Bingo;

public class Player
{
    public string Name { get; init; }
    public string Guess { get; init; }
    public int Score { get; set; }
    public bool AllSameGuess { get; init; }
    public Player(string name, string guess)
    {
        Name = name;
        Guess = guess;
        AllSameGuess = IsAllSame(Guess);

        static bool IsAllSame(ReadOnlySpan<char> guess)
        {
            for (int i = 1; i < guess.Length; i++)
            {
                if (guess[0] == guess[i])
                {
                    continue;
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
