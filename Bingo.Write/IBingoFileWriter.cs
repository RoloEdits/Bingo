using Bingo.Core;

namespace Bingo.Write;

public interface IBingoFileWriter
{
    static abstract void Write(Game game, string path);
}
