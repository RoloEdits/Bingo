namespace Bingo;

public class Player
{
    public string Name { get; init; }
    public string Guess { get; init; }
    public int Score { get; set; }
    public bool AllSameGuess { get; init; }

    public Player(string name, string guess, int score = 0)
    {
        Name = name;
        Guess = guess;
        Score = score;
        AllSameGuess = Guess.All(check => check.Equals(Guess[0]));
    }
}
