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

        var format = Prompt.Format();
        var key = Prompt.Key(format);
        var path = Prompt.Path();

        var spreadsheet = new Spreadsheet(path);
        var players = spreadsheet.GetPlayers(format);

        var game = new Game(players, format, key);
        game.Play();

        FileWrite.WriteToFile(game, path);

        game.End();

    }
}