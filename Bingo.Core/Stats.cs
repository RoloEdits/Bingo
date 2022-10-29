using System.Resources;
using Bingo.Domain;
using Bingo.Domain.Models;
using Bingo.Domain.ValueObjects;

namespace Bingo.Core;

public sealed class Stats
{
    public double ScoreCalculationTime { get; set; }
    public int PlayerCount { get; set; }
    public long MaxScorePossible { get; set; }
    public Dictionary<string, List<string>> CorrectGuesses { get; }
    public Dictionary<string, List<string>> IncorrectGuesses { get; }
    public Dictionary<string, List<string>> Skipped { get; }
    public List<double> CorrectGuessesPercentage { get; }
    public Dictionary<long, uint> FullScoreFrequency { get; }
    public Dictionary<long, uint> PlayerScoreFrequency { get; }
    public List<long> PlayerScores { get; }

    public Stats(ICard card, ISettings settings)
    {
        CorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        CorrectGuessesPercentage = new List<double>(card.TotalSquares);
        IncorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        Skipped = new Dictionary<string, List<string>>(card.Rows * card.BonusColumns);
        ScoreCalculationTime = 0.0;
        FullScoreFrequency = new Dictionary<long, uint>();
        PlayerScoreFrequency = new Dictionary<long, uint>();
        PlayerScores = new List<long>();

        // Pre-populate labels and new List
        foreach (var square in card.SquareLabels)
        {
            CorrectGuesses.Add(square.Label, new List<string>());
            IncorrectGuesses.Add(square.Label, new List<string>());

            if (settings.AllowSkippingWhenThereIsNoBonus && card.BonusColumns == 0)
            {
                Skipped.Add(square.Label, new List<string>());
            }
            else
            {
                if (square.IsBonus)
                {
                    Skipped.Add(square.Label, new List<string>());
                }
            }
        }

        // Pre-populate ScoreFrequency dictionary with every score bin
        for (var score = -MaxScorePossible; score <= MaxScorePossible; score++)
        {
            FullScoreFrequency.Add(score, 0);
        }
    }

    internal void AggregateResults(Game game)
    {
        FilterResults(game.Players);

        GetScoreFrequency(game.Players);

        GetPlayerScores(game.Players);

        // Must be after FilterResults method
        GetCorrectGuessesPercentage();
    }

    private void GetPlayerScores(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            PlayerScores.Add(player.Score);
        }
    }

    private void GetScoreFrequency(List<IPlayer> players)
    {
        foreach (var player in players)
        {
            if (FullScoreFrequency.ContainsKey(player.Score))
            {
                FullScoreFrequency[player.Score]++;
            }
            else
            {
                FullScoreFrequency.Add(player.Score, 1);
            }


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
                        Skipped[result.Key].Add(player.Name);
                        break;
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

    public static long GetMaxScore(ICard card)
    {
        return CalculateMaxScore(card);
    }

    private static long CalculateMaxScore(ICard card)
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
                    score += (squareValue * card.BonusMultiplier);
                }
                else
                {
                    score += squareValue;
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
