using Bingo.Library;
using System.Runtime.InteropServices;

namespace Bingo.Console.UI;

internal static class Program
{
    private static void Main()
    {
        System.Console.Title = "Tower of God Bingo Solver";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            System.Console.WindowHeight = 32;
        }

        var (card, path, key, stats, allGuesses) = Prompt.Input();
        Game.Path = path;
        var game = new Game(key, card, stats, allGuesses);

        var thereAreInvalidGuesses = game.Play();
        if (thereAreInvalidGuesses)
        {
            Prompt.InvalidGuessers(game);
        }

        FileWrite.WriteToFile(game);

        Prompt.End(game);
    }
}