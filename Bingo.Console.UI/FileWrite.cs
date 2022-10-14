using Bingo.Domain;
using Bingo.Library;
using Bingo.Markdown;

namespace Bingo.Console.UI;

internal static class FileWrite
{
    public static void WriteToFile(Game game, string path)
    {
        var fileName = GetFileName(path);

        using TextWriter writer = new StreamWriter(fileName);
        var playersOrdered = game?.Players
            ?.OrderByDescending(player => player.Score)
            ?.ThenBy(player => player.Name).ToList();

        if (game is not null && playersOrdered is not null)
        {
            var table = new Table(game.Card.Columns, game.Card.Rows, game.Card.BonusColumns);

            writer.Write(table.Create<char>("Key", game.Key));
            writer.WriteLine();

            if (game.Settings.WillLogStats)
            {
                var percentages = game.Stats.PerSquareCorrectGuessesDouble;
                writer.Write(table.Create("Stats", percentages.ListTo2DArray(game.Card.Rows, game.Card.Columns)));
                writer.WriteLine();
            }

            writer.WriteLine("| Names | Scores |");
            writer.WriteLine("|---|---|");
            foreach (var player in playersOrdered)
            {
                // Replaces all pipes with an escaped version so that the Markdown Table wont be broken.
                writer.WriteLine($"| {player.Name.Replace("|", "\\|")} | {player.Score} |");
            }
        }
    }

    private static string GetFileName(string filepath)
    {
        return Path.ChangeExtension(filepath, "md");
    }
}
