using Bingo.Core;

namespace Bingo.Write;

public sealed class Json : IBingoFileWriter
{
    public static void Write(Game game, string path)
    {
        var fileName = Path.ChangeExtension(path, "json");

        using TextWriter writer = new StreamWriter(fileName);

        writer.Write(game.ToJson());
    }
}
