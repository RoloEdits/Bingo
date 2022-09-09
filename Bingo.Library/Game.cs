using System.Collections;

namespace Bingo.Library;

public class Game
{
    public List<Player> Players { get; init; }
    public Card Card { get; init; }
    public string Key { get; init; }
    private Dictionary<int, uint> CorrectGuessesPerSquare { get; set; }
    public double ScoreCalculationTime { get; private set; }

    public Game(List<Player> players, Card card, string key)
    {
        Players = players;
        Card = card;
        Key = key;

        var initializeDict = new Dictionary<int, uint>(Card.TotalSquares);
        for (var i = 0; i < Card.TotalSquares; i++)
        {
            initializeDict.TryAdd(i, 0);
        }

        CorrectGuessesPerSquare = initializeDict;
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

        var columns = game.Card.Columns;
        var rows = game.Card.Rows;
        var bonusAmount = game.Card.BonusColumns;
        var bonusChar = game.Card.BonusSkipChar;
        short finalColumnOfRow = game.Card.Columns;
        long squareValue = game.Card.BaseSquareValue;
        var bonusMultiplier = game.Card.BonusMultiplier;
        var rowOffset = game.Card.RowValueOffset;

        var holder = 0;

        for (short rowCounter = 0; rowCounter < rows; rowCounter++)
        {
            for (var current = holder; current < finalColumnOfRow; current++)
            {
                var isBonus = (current + bonusAmount) >= finalColumnOfRow;
                var isNotSkipChar = guess[current] != bonusChar;

                if (isBonus && isNotSkipChar)
                {
                    if (guess[current] == key[current])
                    {
                        score += (squareValue * bonusMultiplier);
                        AddCorrectGuesses(current, same, game.CorrectGuessesPerSquare);
                    }
                    else
                    {
                        score -= (squareValue * bonusMultiplier);
                    }
                }
                else if (guess[current] == key[current])
                {
                    score += squareValue;
                    AddCorrectGuesses(current, same, game.CorrectGuessesPerSquare);
                }
                else
                {
                    score -= squareValue;
                }

                holder = current + 1;
            }

            squareValue += rowOffset;
            finalColumnOfRow += columns;
        }

        return score;

        void AddCorrectGuesses(int square, bool isSame, Dictionary<int, uint> correctGuesses)
        {
            // 'if (!isSame)' Decides whether to include or exclude all same guessers from the stats
            if (!isSame)
            {
                correctGuesses[square]++;
            }
        }
    }

    public static bool CheckValidGuessAmount(ReadOnlySpan<char> guess, Card config)
    {
        return (guess.Length == (config.Columns * config.Rows));
    }

    public static List<string> GetPercentageOfCorrectGuesses(Game game)
    {
        var correctGuesses = game.CorrectGuessesPerSquare;
        var percentages = new List<string>();

        const string zero = "0.00%";

        foreach (var guess in correctGuesses)
        {
            var (_, count) = guess;

            if (count == 0)
            {
                percentages.Add(zero);
            }
            else
            {
                var percentage = ((double)count / game.Players.Count).ToString("P2");

                percentages.Add(percentage);
            }
        }

        return percentages;
    }
}
