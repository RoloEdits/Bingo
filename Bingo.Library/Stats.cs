using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace Bingo.Library;

public class Stats
{
    public double ScoreCalculationTime { get; set; }
    public IDictionary<int, uint> CorrectGuessesPerSquare { get; set; }
    public List<double> CorrectGuessesPerSquareDouble { get; set; }

    public List<string> CorrectGuessesPerSquareAsPercentageString => DoubleAsPercentage();
    public int PlayerCount { get; set; }

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
