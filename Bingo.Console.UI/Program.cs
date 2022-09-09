using Bingo.Library;
using System.Runtime.InteropServices;

namespace Bingo.Console.UI;

internal static class Program
{
    private static void Main()
    {
        // Set console name and text color.
        System.Console.Title = "Tower of God Bingo Solver";
        System.Console.ForegroundColor = ConsoleColor.Red;

        // Checks if the OS is windows. If it is, sets console window height.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            System.Console.WindowHeight = 32;
        }

        var format = Prompt.Format();
        var path = Prompt.Path();
        var key = Prompt.Key(format);

        var spreadsheet = new SpreadsheetParse(path);
        var players = spreadsheet.GetPlayers(format);

        var game = new Game(players, format, key);

        game.Play();

        FileWrite.WriteToFile(game, path);

        game.End();
    }
}
