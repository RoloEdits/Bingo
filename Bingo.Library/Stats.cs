namespace Bingo.Library;

public class Stats
{
    public double ScoreCalculationTime { get; set; }
    public IDictionary<int, uint> CorrectGuessesPerSquare { get; set; }
    public List<double> CorrectGuessesPerSquarePercentage { get; set; }
    public int PlayerCount { get; set; }

    public void GetCorrectGuessesPerSquarePercentage()
    {
        foreach (var guess in CorrectGuessesPerSquare)
        {
            var (_, count) = guess;

            if (count == 0)
            {
                CorrectGuessesPerSquarePercentage.Add(0.0);
            }
            else
            {
                var percentage = ((double)count / PlayerCount);

                CorrectGuessesPerSquarePercentage.Add(percentage);
            }
        }
    }
}
