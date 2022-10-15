using Bingo.Domain;
using Bingo.Domain.Models;

namespace Bingo.Library;

public sealed class Stats
{
    // TODO - better handle stats so that when user decides to not track they dont get implemented and waste memory.
    public double ScoreCalculationTime { get; set; }
    private int PlayerCount { get; }
    public Dictionary<string, List<string>> CorrectGuesses { get; }
    public Dictionary<string, List<string>> IncorrectGuesses { get; }
    public Dictionary<string, List<string>> SkippedBonus { get; }
    public List<double> CorrectGuessesPercentage { get; }

    public Stats(ICard card, int playerCount)
    {
        CorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        CorrectGuessesPercentage = new List<double>(card.TotalSquares);
        IncorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        SkippedBonus = new Dictionary<string, List<string>>(card.Rows * card.BonusColumns);
        ScoreCalculationTime = 0.0;
        PlayerCount = playerCount;

        foreach (var square in card.SquareLabels)
        {
            CorrectGuesses.Add(square.Label, new List<string>());
            IncorrectGuesses.Add(square.Label, new List<string>());
            if (square.IsBonus)
            {
                SkippedBonus.Add(square.Label, new List<string>());
            }
        }
    }

    // TODO: Temp solution, once there is properly implemented option delete.
    private void GetCorrectGuessesPercentage()
    {
        foreach (var square in CorrectGuesses.OrderBy(label => label.Key))
        {
            CorrectGuessesPercentage.Add((double)square.Value.Count / PlayerCount);
        }
    }

    public void AggregateResults(List<IPlayer> players)
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

        // TODO: Find proper place to call.
        GetCorrectGuessesPercentage();
    }
}
