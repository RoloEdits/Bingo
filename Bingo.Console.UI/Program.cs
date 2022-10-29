using System.Runtime.CompilerServices;
using Bingo.Core;
using System.Runtime.InteropServices;
using Bingo.Domain.Errors;
using Bingo.Markdown;

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

        var (card, path, key, settings) = Prompt.Input();
        var game = new Game(key, card, settings);

        Dictionary<string, string>? players = null;
        try
        {
            players = Spreadsheet.Parser.Excel.Spreadsheet.Parse(path);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case CannotOpenFileException:
                    Prompt.CannotOpenFile(exception);
                    break;
                case NoPlayersException:
                    Prompt.NoPlayers(exception);
                    break;
                case GuessedMoreThanOnceException:
                    Prompt.MultipleGuessesOfSamePlayer(exception);
                    break;
            }
        }

        var badGuessers = game.AddPlayers(players!);

        if (badGuessers.Count > 0)
        {
            Prompt.InvalidGuessers(badGuessers, game);
        }

        game.Play();

        FileWrite.WriteToFile(game, path);

        Prompt.End(game);
    }
}