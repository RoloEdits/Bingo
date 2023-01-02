using Bingo.Domain.Models;

namespace Bingo.Core;

public sealed class Stats
{
    public double ScoreCalculationTime { get; set; }
    public int PlayerCount { get; set; }
    public long MaxScorePossible { get; init; }
    public Dictionary<string, List<string>> CorrectGuesses { get; }
    public Dictionary<string, List<string>> IncorrectGuesses { get; }
    public Dictionary<string, List<string>> Skipped { get; }
    public double[,] CorrectGuessesPercentage { get; }
    public int[,] CorrectGuessersPerSquare { get; }
    public Dictionary<long, uint> PlayerScoreFrequency { get; }
    public List<long> PlayerScores { get; }

    public Stats(Card card, Settings settings)
    {
        CorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        CorrectGuessesPercentage = new double[card.Rows, card.Columns];
        IncorrectGuesses = new Dictionary<string, List<string>>(card.TotalSquares);
        Skipped = new Dictionary<string, List<string>>(card.Rows * card.BonusColumns);
        ScoreCalculationTime = 0.0;
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

        // Pre-populate CorrectGuessersPerSquare dimensions
        CorrectGuessersPerSquare = new int[card.Rows, card.Columns];
    }

    internal void AggregateResults(Game game)
    {
        FilterResults(game.Players);

        GetScoreFrequency(game.Players);

        GetPlayerScores(game.Players);

        // Must be after FilterResults method
        GetCorrectGuessesPercentage();

        GetCorrectGuessersPerSquare();

    }
    private void GetPlayerScores(List<Player> players)
    {
        foreach (var player in players)
        {
            PlayerScores.Add(player.Score);
        }
    }
    private void GetScoreFrequency(IEnumerable<Player> players)
    {
        foreach (var score in players.Select(player => player.Score))
        {
            if (PlayerScoreFrequency.TryGetValue(score, out var value))
            {
                value++;
            }
            else
            {
                PlayerScoreFrequency.Add(score, 1);
            }
        }
    }
    private void FilterResults(List<Player> players)
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
                }
            }
        }
    }
    private void GetCorrectGuessesPercentage()
    {
        foreach (var square in CorrectGuesses.OrderBy(label => label.Key))
        {

            var list = IterateSquaresForCorrectGuessers();
            var count = 0;
            for (var i = 0; i < CorrectGuessersPerSquare.GetLength(0); i++)
            {
                for (var j = 0; j < CorrectGuessersPerSquare.GetLength(1); j++)
                {
                    CorrectGuessesPercentage[i, j] = (double)list[count] / PlayerCount;
                    count++;
                }
            }
        }
    }
    private void GetCorrectGuessersPerSquare()
    {
        var list = IterateSquaresForCorrectGuessers();
        var count = 0;
        for (var i = 0; i < CorrectGuessersPerSquare.GetLength(0); i++)
        {
            for (var j = 0; j < CorrectGuessersPerSquare.GetLength(1); j++)
            {
                CorrectGuessersPerSquare[i, j] = list[count];
                count++;
            }
        }
    }
    public static long GetMaxScore(Card card)
    {
        long score = 0;
        var squareValue = card.BaseSquareValue;

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
    private List<int> IterateSquaresForCorrectGuessers()
    {
        return CorrectGuesses.OrderBy(label => label.Key).Select(square => square.Value.Count).ToList();
    }

}
