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

        var card = Prompt.Format();
        Game.Path = Prompt.Path();
        var key = Prompt.Key(card);

        var game = new Game(key, card);

        game.Play();


    }


}
