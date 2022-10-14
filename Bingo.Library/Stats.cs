using Bingo.Domain;
using Bingo.Domain.Models;

namespace Bingo.Library;

public sealed class Stats
{
    // TODO - better handle stats so that when user decides to not track they dont get implemented and waste memory.
    public double ScoreCalculationTime { get; set; }
    public Dictionary<string, List<string>> PerSquareCorrectGuesses { get; }
    public Dictionary<string, List<string>> PerSquareIncorrectGuesses { get; }
    public Dictionary<string, List<string>> SkippedBonus { get; }
    public List<double> PerSquareCorrectGuessesDouble { get; }
    private int PlayerCount { get; }

    public Stats(ICard card, int playerCount)
    {
        PerSquareCorrectGuessesDouble = new List<double>(card.TotalSquares);
        PerSquareCorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        PerSquareIncorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        SkippedBonus = new Dictionary<string, List<string>>(card.Rows * card.BonusColumns);
        ScoreCalculationTime = 0.0;
        PlayerCount = playerCount;

        foreach (var square in card.SquareLabels)
        {
            PerSquareCorrectGuesses.Add(square.Label, new List<string>());
            PerSquareIncorrectGuesses.Add(square.Label, new List<string>());
            if (square.IsBonus)
            {
                SkippedBonus.Add(square.Label, new List<string>());
            }
        }
    }

    // TODO: Temp solution, once there is properly implemented option delete.
    private void GetPerSquareCorrectGuessesPercentage()
    {
        foreach (var square in PerSquareCorrectGuesses.OrderBy(label => label.Key))
        {
            PerSquareCorrectGuessesDouble.Add((double)square.Value.Count / PlayerCount);
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
                        PerSquareCorrectGuesses[result.Key].Add(player.Name);
                        break;
                    case Result.Incorrect:
                        PerSquareIncorrectGuesses[result.Key].Add(player.Name);
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
        GetPerSquareCorrectGuessesPercentage();
    }
}
