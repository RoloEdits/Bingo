using Bingo.Library;

namespace Bingo.Console.UI;

internal static class GameExtensions
{
    public static void End(this Game game)
    {
        System.Console.Clear();
        Ascii.Title();
        System.Console.WriteLine(
            $"Successfully finished scoring {game.Card.TotalSquares} Squares for {game.Players.Count} Players in {game.ScoreCalculationTime} milliseconds.");
        System.Console.Write("Press any key to exit...");
        System.Console.ReadKey(true);
        System.Console.Write(Environment.NewLine);
        System.Console.ResetColor();
        Environment.Exit(0);
    }
}