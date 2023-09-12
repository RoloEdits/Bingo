using System.Text;
using Bingo.Core;

namespace Bingo.Write;

public sealed class Html : IBingoFileWriter
{

    public static void Write(Game game, string path)
    {
        var fileName = Path.ChangeExtension(path, "html");
        using TextWriter writer = new StreamWriter(fileName);

        var playersOrdered = game.Players
            .OrderByDescending(player => player.Score)
            .ThenBy(player => player.Name).ToList();

        writer.Write(ToHtml(playersOrdered));
    }

    private static string ToHtml(List<Player> players)
    {
        var html = new StringBuilder();

        html.Append("""
<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8" />
    <title>title</title>
    <script src="https://cdn.tailwindcss.com"></script>
</head>

<body class="max-w-md">
    <div class="relative">
        <table class="w-full text-4xl text-left text-zinc-50">
            <thead class="text-xl uppercase bg-zinc-700 text-zinc-50">
                <tr>
                    <th scope="col" class="px-6 py-3">
                        Name
                    </th>
                    <th scope="col" class="px-6 py-3">
                        Score
                    </th>
                </tr>
            </thead>
""");

        html.Append("""<tbody class="bg-zinc-800 text-white">""");


        foreach (var player in players)
        {
            html.Append($"""
<tr class=" border-b border-zinc-700">
                    <th scope="row" class="px-6 py-4 font-medium whitespace-nowrap" style="color: {player.Color};">
                        {player.Name}
                    </th>
                    <td class="px-6 py-4 text-white">
                        {player.Score}
                    </td>
                </tr>
""");
        }

        html.Append("</tbody>");

        html.Append("""
        </table>
    </div>
</body>
</html>
""");

        return html.ToString();
    }
}
