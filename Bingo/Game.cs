namespace Bingo;

public class Game
{
    public List<Player> Players { get; init; }
    public Format Format { get; init; }
    public string Key { get; init; }
    private Dictionary<int, uint> CorrectGuessesPerSquare { get; set; }
    public double ScoreCalculationTime { get; private set; }

    public Game(List<Player> players, Format format, string key)
    {
        Players = players;
        Format = format;
        Key = key;
        CorrectGuessesPerSquare = new Dictionary<int, uint>();
    }
    public void Play()
    {
        var game = this;
        var start = DateTime.UtcNow;

        foreach (var player in game.Players)
        {
            player.Score = CalculatePlayerScore(
                game.Key,
                player.Guess,
                player.AllSameGuess,
                game);
        }
        var spent = DateTime.UtcNow - start;

        game.ScoreCalculationTime = (double)spent.Ticks / 10_000;
    }
    public static long CalculatePlayerScore(ReadOnlySpan<char> key, ReadOnlySpan<char> guess, bool same, Game game)
    {
        long score = 0;

        short finalColumnOfRow = game.Format.Columns;
        var holder = 0;
        long squareValue = game.Format.BaseSquareValue;

        for (short rowCounter = 0; rowCounter < game.Format.Rows; rowCounter++)
        {
            for (var current = holder; current < finalColumnOfRow; current++)
            {
                var isBonus = (current + game.Format.BonusColumns) >= finalColumnOfRow;
                var isNotSkipChar = guess[current] != game.Format.BonusSkipChar;

                if ( isBonus && isNotSkipChar)
                {
                    if (guess[current] == key[current])
                    {
                        score += (squareValue * game.Format.BonusMultiplier);
                        AddCorrectGuesses(current, same, game);
                    }
                    else
                    {
                        score -= (squareValue * game.Format.BonusMultiplier);
                    }
                }
                else if (guess[current] == key[current])
                {
                    score += squareValue;
                    AddCorrectGuesses(current, same, game);
                }
                else
                {
                    score -= squareValue;
                }
                holder = current + 1;
            }
            squareValue += game.Format.RowValueOffset;
            finalColumnOfRow += game.Format.Columns;
        }
        return score;
    }
    public static bool CheckValidGuessAmount(ReadOnlySpan<char> guess, Format config)
    {
        return (guess.Length == (config.Columns * config.Rows));
    }

    private static void AddCorrectGuesses(int key, bool same, Game game)
    {
        if (!same)
        {
            if (game.CorrectGuessesPerSquare.ContainsKey(key))
            {
                game.CorrectGuessesPerSquare[key]++;
            }
            else
            {
                game.CorrectGuessesPerSquare.Add(key, 1);
            }
        }
    }
    public static List<string> GetPercentageOfCorrectGuesses(Game game)
    {
        var correctGuesses = game.CorrectGuessesPerSquare;
        var percentages = new List<string>();

        foreach (var guess in correctGuesses)
        {
            var (_, count) = guess;

            var percentage = ((double)count / game.Players.Count).ToString("P2");

            percentages.Add(percentage);
        }
        return percentages;
    }
}
