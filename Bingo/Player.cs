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
        AllSameGuess = Guess.All(check => check.Equals(Guess[0]));
    }
}
