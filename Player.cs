namespace Tog.Bingo
{
    internal class Player
    {
        public string Name { get; }
        public string Guess { get; }
        public int Score { get; }

        public Player(string name, string guess, int score)
        {
            Name = name;
            Guess = guess;
            Score = score;
        }
    }
}
