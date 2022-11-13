using Bingo.Core;

namespace Bingo.Blazor.WebAssembly.Services;

public interface IGameService
{
    public Game? Game { get; }

    public string Url { get; }

    public Guid Id { get; }

    public DateTime DateTime { get; }

    public void AddGameReference(Game game);
}
