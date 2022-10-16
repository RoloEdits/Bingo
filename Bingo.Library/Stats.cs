using System.Resources;
using Bingo.Domain;
using Bingo.Domain.Models;
using Bingo.Domain.ValueObjects;

namespace Bingo.Library;

public sealed class Stats
{
    // TODO - better handle stats so that when user decides to not track they dont get implemented and waste memory.
    public double ScoreCalculationTime { get; set; }
    private int PlayerCount { get; }
    public long MaxScorePossible { get; set; }
    public Dictionary<string, List<string>> CorrectGuesses { get; }
    public Dictionary<string, List<string>> IncorrectGuesses { get; }
    public Dictionary<string, List<string>> SkippedBonus { get; }
    public List<double> CorrectGuessesPercentage { get; }
    // TODO: Might not need this, but will have to be sure how I want to consume it
    public Dictionary<long, uint> FullScoreFrequency { get; }
    public Dictionary<long, uint> PlayerScoreFrequency { get; }

    public Stats(ICard card, int playerCount)
    {
        CorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        CorrectGuessesPercentage = new List<double>(card.TotalSquares);
        IncorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        SkippedBonus = new Dictionary<string, List<string>>(card.Rows * card.BonusColumns);
        ScoreCalculationTime = 0.0;
        PlayerCount = playerCount;
        FullScoreFrequency = new Dictionary<long, uint>();
        PlayerScoreFrequency = new Dictionary<long, uint>();

        // Pre-populate labels and new List
        foreach (var square in card.SquareLabels)
        {
            CorrectGuesses.Add(square.Label, new List<string>());
            IncorrectGuesses.Add(square.Label, new List<string>());
            if (square.IsBonus)
            {
                SkippedBonus.Add(square.Label, new List<string>());
            }
        }

        // Pre-populate ScoreFrequency dictionary with every score bin
        for (var score = -MaxScorePossible; score <= MaxScorePossible; score++)
        {
            FullScoreFrequency.Add(score, 0);
        }
    }

    public void AggregateResults(Game game)
    {
        FilterResults(game.Players);

        GetScoreFrequency(game.Players);

        // Must be after FilterResults method
        GetCorrectGuessesPercentage();
    }

    private void GetScoreFrequency(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            FullScoreFrequency[player.Score]++;

            if (PlayerScoreFrequency.ContainsKey(player.Score))
            {
                PlayerScoreFrequency[player.Score]++;
            }
            else
            {
                PlayerScoreFrequency.Add(player.Score, 1);
            }
        }
    }

    private void FilterResults(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            foreach (var result in player.ResultPerSquare)
            {
                switch (result.Value)
                {
                    case Result.Correct:
                        CorrectGuesses[result.Key].Add(player.Name);
                        break;
                    case Result.Incorrect:
                        IncorrectGuesses[result.Key].Add(player.Name);
                        break;
                    case Result.Skipped:
                        SkippedBonus[result.Key].Add(player.Name);
                        break;
                    // TODO: Make custom exception
                    default: throw new Exception("Error processing player results");
                }
            }
        }
    }

    private void GetCorrectGuessesPercentage()
    {
        foreach (var square in CorrectGuesses.OrderBy(label => label.Key))
        {
            CorrectGuessesPercentage.Add((double)square.Value.Count / PlayerCount);
        }
    }

    public static long GetMaxScore(Key key, Key guess, ICard card)
    {
        return CalculateMaxScore(key, guess, card);
    }

    private static long CalculateMaxScore(Key key, Key guess, ICard card)
    {
        long score = 0;
        int squareValue = card.BaseSquareValue;

        for (short row = 0; row < card.Rows; row++)
        {
            for (var column = 0; column < card.Columns; column++)
            {
                var isBonusColumn = (column + card.BonusColumns) >= card.Columns;

                if (isBonusColumn)
                {
                    if (guess[row, column] == key[row, column])
                    {
                        score += (squareValue * card.BonusMultiplier);
                    }
                    else
                    {
                        score -= (squareValue * card.BonusMultiplier);
                    }
                }
                else if (guess[row, column] == key[row, column])
                {
                    score += squareValue;
                }
                else
                {
                    score -= squareValue;
                }
            }

            if (card.RowValueOffset != 0)
            {
                squareValue += card.RowValueOffset;
            }
        }

        return score;
    }
}
