namespace Bingo.Library;

public sealed class Stats
{
    // TODO - better handle stats so that when user decides to not track they dont get implemented and waste memory.
    public double ScoreCalculationTime { get; set; }
    public IDictionary<int, uint>? CorrectGuessesPerSquare { get; init; }
    public List<double>? CorrectGuessesPerSquareDouble { get; init; }

    public List<string>? CorrectGuessesPerSquareAsPercentageString => DoubleAsPercentage();
    public int PlayerCount;

    public void GetCorrectGuessesPerSquarePercentage()
    {
        if (CorrectGuessesPerSquare is null || CorrectGuessesPerSquareDouble is null) return;

        foreach (var guess in CorrectGuessesPerSquare)
        {
            var (_, count) = guess;

            if (count == 0)
            {
                CorrectGuessesPerSquareDouble.Add(0.0);
            }
            else
            {
                var percentage = ((double)count / PlayerCount);

                CorrectGuessesPerSquareDouble.Add(percentage);
            }
        }
    }

    private List<string> DoubleAsPercentage()
    {
        if (CorrectGuessesPerSquareDouble is null) return new List<string>(0);

        var result = new List<string>(CorrectGuessesPerSquareDouble.Count);

        foreach (var percentage in CorrectGuessesPerSquareDouble)
        {
            result.Add(percentage.ToString("P2"));
        }

        return result;
    }
}
