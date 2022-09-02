using System.Diagnostics;

namespace Bingo;

public class Game
{
    public List<Player> Players { get; set; }
    public Config Config { get; init; }
    public string Key { get; init; }
    public Dictionary<int, uint> CorrectGuesses { get; set; }
    public long Timer { get; set; }

    public Game(Config config, string key)
    {
        Players = new List<Player>();
        Config = config;
        Key = key;
        CorrectGuesses = new Dictionary<int, uint>();
    }
    public void Play()
    {
        var game = this;
        var watch = Stopwatch.StartNew();
        watch.Start();

        foreach (var player in game.Players)
        {
            player.Score = CalculatePlayerScore(game.Key, player.Guess, player.AllSameGuess, game);
        }
        watch.Stop();

        game.Timer = watch.ElapsedMilliseconds;
    }
    public static int CalculatePlayerScore(ReadOnlySpan<char> key, ReadOnlySpan<char> guess, bool same, Game game)
    {
        var score = 0;

        short finalColumnOfRow = game.Config.Columns;
        var holder = 0;
        short squareValue = game.Config.BaseSquareValue;

        for (short rowCounter = 0; rowCounter < game.Config.Rows; rowCounter++)
        {
            for (var current = holder; current < finalColumnOfRow; current++)
            {
                if ((current + game.Config.BonusColumns) >= finalColumnOfRow && guess[current] != game.Config.BonusSkipChar)
                {
                    if (guess[current] == key[current])
                    {
                        score += (squareValue * game.Config.BonusMultiplier);
                        AddCorrectGuesses(current, same, game);
                    }
                    else
                    {
                        score -= (squareValue * game.Config.BonusMultiplier);
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
            squareValue += game.Config.RowValueOffset;
            finalColumnOfRow += game.Config.Columns;
        }
        return score;
    }
    public static bool CheckValidGuessAmount(ReadOnlySpan<char> guess, Config config)
    {
        return (guess.Length == (config.Columns * config.Rows));
    }
    public static void AddCorrectGuesses(int key, bool same, Game game)
    {
        if (!same)
        {
            if (!game.CorrectGuesses.ContainsKey(key))
            {
                game.CorrectGuesses.Add(key, 1);
            }
            else
            {
                game.CorrectGuesses[key]++;
            }
        }
    }
    public static List<string> GetPercentageOfCorrectGuesses(Game game)
    {
        var correctGuesses = game.CorrectGuesses;
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