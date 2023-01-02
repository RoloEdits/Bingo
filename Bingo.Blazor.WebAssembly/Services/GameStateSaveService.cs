using Bingo.Core;

namespace Bingo.Blazor.WebAssembly.Services;

public sealed class GameStateSaveService
{
    public Game Game { get; private set; }

    public string Url { get; }

    public Guid Id { get; }

    public DateTime DateTime { get; }

    public GameStateSaveService()
    {
        Id = Guid.NewGuid();
        Url = $"{Id}";
        DateTime = DateTime.Now;
    }

    public void SaveGameState(Game game)
    {
        Game = game;
    }
}
