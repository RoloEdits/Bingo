namespace Tog.Bingo
{
    internal class Player
    {
        public string Name { get; }
        public string Guess { get; }
        public int Score { get; }

        public static int PlayerCount = 0;

        public static List<int> CorrectGuesses = new();

        public Player(string name, string guess, int score)
        {
            Name = name;
            Guess = guess;
            Score = score;

            PlayerCount++;
        }
        public int PlayerScore(char[] Key, char[] guess)
        {
            return Score;
        }
    }
}
