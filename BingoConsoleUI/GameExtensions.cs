using Bingo;

namespace BingoConsoleUI;
internal static class GameExtensions
{
    public static void End(this Game game)
    {
        Console.Clear();
        Ascii.Title();
        Console.WriteLine($"Successfully finished scoring {game.Format.TotalSquares} Squares for {game.Players.Count} Players' in {game.ScoreCalculationTime} milliseconds.");
        Console.Write("Press any key to exit...");
        Console.ReadKey(true);
        Console.Write(Environment.NewLine);
        Console.ResetColor();
        Environment.Exit(0);
    }
}
