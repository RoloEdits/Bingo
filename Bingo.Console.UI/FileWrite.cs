using Bingo.Library;
using Bingo.Markdown;

namespace Bingo.Console.UI;

internal static class FileWrite
{
    public static void WriteToFile(Game game)
    {
        var fileName = GetFileName(Game.Path!);

        using TextWriter writer = new StreamWriter(fileName);
        var playersOrdered = game.Players
            .OrderByDescending(player => player.Score)
            .ThenBy(player => player.Name).ToList();

        var table = new Table(game.Card.Columns, game.Card.Rows, game.Card.BonusColumns);

        var percentages = game.Stats.CorrectGuessesPerSquareAsPercentageString;
        var key = game.Key.ToList();

        writer.Write(table.CreateDynamic("Key", key));
        writer.WriteLine();

        if (percentages.Count > 0)
        {
            writer.Write(table.CreateDynamic("Stats", percentages));
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

    private static string GetFileName(string filepath)
    {
        return Path.ChangeExtension(filepath, "md");
    }
}
