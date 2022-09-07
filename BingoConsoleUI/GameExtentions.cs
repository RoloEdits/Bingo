using Bingo;

namespace BingoConsoleUI;
internal static class GameExtentions
{
    public static void End(this Game game)
    {
        Console.Clear();
        Ascii.Title();
        Console.WriteLine($"Successfully Finished Going Through {game.Players.Count.ToString()} Players' Guesses in {game.ScoreCalculationTime.ToString()}ms.");
        Console.Write("Press any key to exit...");
        Console.ReadKey(true);
        Console.WriteLine(Environment.NewLine);
        Console.ResetColor();
        Environment.Exit(0);
    }
}
