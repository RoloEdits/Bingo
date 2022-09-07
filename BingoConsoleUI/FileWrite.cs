using Bingo;
using Markdown;

namespace BingoConsoleUI;

internal class FileWrite
{
    public static void WriteToFile(Game game, string filepath)
    {
        var fileName = GetFileName(filepath);

        using TextWriter writer = new StreamWriter(fileName);
        var playerScore = game.Players
            .OrderByDescending(player => player.Score)
            .ThenBy(player => player.Name).ToList();

        var dynamicTable = new Table(game.Format.Columns, game.Format.Rows, game.Format.BonusColumns);

        var percentages = Game.GetPercentageOfCorrectGuesses(game);
        var key = game.Key.ToList();

        writer.Write(Table.CreateDynamic("Key", key, dynamicTable));
        writer.WriteLine();

        writer.Write(Table.CreateDynamic("Stats", percentages, dynamicTable));
        writer.WriteLine();

        writer.WriteLine("| Names | Scores |");
        writer.WriteLine("|---|---|");
        foreach (var player in playerScore)
        {
            // Replaces all pipes with an escaped version so that the Markdown Table wont be broken.
            writer.WriteLine($"| {player.Name.Replace("|", "\\|")} | {player.Score.ToString()} |");
        }
    }
    private static string GetFileName(string filepath)
    {
        return Path.ChangeExtension(filepath, "md");
    }

}
