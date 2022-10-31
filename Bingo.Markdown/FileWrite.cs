using System.Text;
using Bingo.Core;
using Bingo.Domain;
using Bingo.Domain.Models;
using DocumentFormat.OpenXml.Drawing;
using Path = System.IO.Path;

namespace Bingo.Markdown;

public static class FileWrite
{
    public static void WriteToFile(Game game, string path)
    {
        var fileName = GetFileName(path);

        using TextWriter writer = new StreamWriter(fileName);
        var playersOrdered = game.Players
            .OrderByDescending(player => player.Score)
            .ThenBy(player => player.Name).ToList();

        var table = new Table(game.Card.Columns, game.Card.Rows, game.Card.BonusColumns);

        writer.WriteLine(table.Create<char>("Key", game.Key));

        var percentages = game.Stats.CorrectGuessesPercentage;
        writer.WriteLine(table.Create("Stats", percentages.ListTo2DArray(game.Card.Rows, game.Card.Columns)));

        writer.WriteLine(BuildScoreTable(playersOrdered));

    }

    private static string GetFileName(string filepath)
    {
        return Path.ChangeExtension(filepath, "md");
    }

    private static string BuildScoreTable(List<IPlayer> players)
    {
        var builder = new StringBuilder();

        builder.AppendLine("| Names | Scores |");
        builder.AppendLine("|---|---|");

        foreach (var player in players!)
        {
            // Replaces all pipes with an escaped version so that the Markdown Table wont be broken.
            builder.AppendLine($"| {player.Name.Replace("|", "\\|")} | {player.Score} |");
        }

        return builder.ToString();
    }
}
