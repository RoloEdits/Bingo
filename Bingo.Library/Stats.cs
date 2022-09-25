namespace Bingo.Library;

public sealed class Stats
{
    // TODO - better handle stats so that when user decides to not track they dont get implemented and waste memory.
    public double ScoreCalculationTime { get; set; }
    // TODO: cull these to just the correct guess Dict and the double. Change the first int to be the square label.
    public IDictionary<string, uint> CorrectGuessesPerSquare { get; set; }
    public List<double> CorrectGuessesPerSquareDouble { get; set; }
    public List<string> CorrectGuessesPerSquareAsPercentageString => DoubleAsPercentage();
    public int PlayerCount { get; init; }

    public Stats(int amount, int playerCount)
    {
        CorrectGuessesPerSquareDouble = new List<double>(amount);
        CorrectGuessesPerSquare = new Dictionary<string, uint>(amount);
        ScoreCalculationTime = 0.0;
        PlayerCount = playerCount;
    }

    public void GetCorrectGuessesPerSquarePercentage()
    {
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
    // TODO: find away to get rid of this
    private List<string> DoubleAsPercentage()
    {
        var result = new List<string>(CorrectGuessesPerSquareDouble.Count);

        foreach (var percentage in CorrectGuessesPerSquareDouble)
        {
            result.Add(percentage.ToString("P2"));
        }

        return result;
    }
}
