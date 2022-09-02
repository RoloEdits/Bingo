using Bingo;
using System.Runtime.InteropServices;

namespace BingoConsoleUI;

internal class Program
{
    private static void Main()
    {
        // Set console name and text color.
        Console.Title = "Tower of God Bingo Solver";
        Console.ForegroundColor = ConsoleColor.Red;

        // Checks if the OS is windows. If it is, sets console window height.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WindowHeight = 45;
        }

        var config = Prompt.Config();
        var key = Prompt.Key(config);
        var path = Prompt.Path();

        var game = new Game(config, key);
        var spreadsheet = new Spreadsheet(game, path);

        if (spreadsheet.IncorrectGuessAmount.Count > 0)
        {
            Console.Clear();
            Utilities.AsciiTitle();
            Console.WriteLine($"Detected: Players with incorrect amount of guesses!");
            foreach (var incorrectGuesser in spreadsheet.IncorrectGuessAmount)
            {
                var (row, name) = incorrectGuesser;
                Console.WriteLine($"Row:{row} Name:{name}");
            }
            Console.Write("Please resolve issue and try again.");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
            Environment.Exit(5);
        }

        game.Play();

        FileWrite.WriteToFile(game, path);

        Console.Clear();
        Utilities.AsciiTitle();
        Console.WriteLine($"Successfully Finished Going Through {game.Players.Count} Players' Guesses in {game.Timer}ms.");
        Console.Write("Press any key to exit...");
        Console.ReadKey();
        Console.WriteLine(Environment.NewLine);
        Console.ResetColor();
        Environment.Exit(0);

    }
}