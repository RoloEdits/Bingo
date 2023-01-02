using System.Runtime.CompilerServices;
using Bingo.Core;
using System.Runtime.InteropServices;
using Bingo.Domain.Errors;
using Bingo.Write;
using Bingo.Spreadsheet;

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
            var players = Parser.Parse(path);

            var game = new GameBuilder()
                .AddKey(key)
                .AddCard(card)
                .AddSettings(settings)
                .AddPlayers(players)
                .Build();

            game.Play();

            Markdown.Write(game, path);
            Json.Write(game, path);

            Prompt.End(game);
        }
        catch (InvalidGuessersException invalidGuessers)
        {
            if (invalidGuessers?.InvalidGuessers != null)
                Prompt.InvalidGuessers(invalidGuessers.InvalidGuessers, card.TotalSquares);
        }
        catch (CannotOpenFileException ex)
        {
            Prompt.CannotOpenFile(ex);
        }
        catch (NoPlayersException ex)
        {
            Prompt.NoPlayers(ex);
        }
        catch (GuessedMoreThanOnceException ex)
        {
            Prompt.MultipleGuessesOfSamePlayer(ex);
        }
    }
}
