using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bingo;

namespace BingoConsoleUI;
internal static class GameExtentions
{
    public static void End(this Game game)
    {
        Console.Clear();
        Utilities.AsciiTitle();
        Console.WriteLine($"Successfully Finished Going Through {game.Players.Count} Players' Guesses in {game.ScoreCalculationTime}ms.");
        Console.Write("Press any key to exit...");
        Console.ReadKey();
        Console.WriteLine(Environment.NewLine);
        Console.ResetColor();
        Environment.Exit(0);
    }
}
