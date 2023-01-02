using System.Text;
using Bingo.Core;
using Bingo.Domain;
using Path = System.IO.Path;

namespace Bingo.Write;

public sealed class Markdown : IBingoFileWriter
{
    public static void Write(Game game, string path)
    {
        var fileName = Path.ChangeExtension(path, "md");

        using TextWriter writer = new StreamWriter(fileName);
        var playersOrdered = game.Players
            .OrderByDescending(player => player.Score)
            .ThenBy(player => player.Name).ToList();

        var table = new Table(game.Card.Columns, game.Card.Rows, game.Card.BonusColumns);

        writer.WriteLine(table.Create<char>("Key", game.Key));

        var percentages = game.Stats.CorrectGuessesPercentage;
        writer.WriteLine(table.Create("Stats", percentages));

        writer.WriteLine(BuildScoreTable(playersOrdered));

    }

    private static string BuildScoreTable(List<Player> players)
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
