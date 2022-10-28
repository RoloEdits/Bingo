using Bingo.Core;
using System.Runtime.InteropServices;
using Bingo.Domain.Errors;
using Bingo.Spreadsheet.Parser.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

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

        try
        {
            var players = Spreadsheet.Parser.Excel.Spreadsheet.Parse(path);

            var game = new Game(key, card, settings, players);

            game.Play();

            FileWrite.WriteToFile(game, path);

            Prompt.End(game);
        }
        catch (Exception exception)
        {
            System.Console.WriteLine(exception);
            throw;
        }



    }
}
