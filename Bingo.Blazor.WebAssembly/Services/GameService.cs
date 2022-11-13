using System.Runtime.InteropServices.JavaScript;
using Bingo.Core;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Bingo.Blazor.WebAssembly.Services;

public sealed class GameService : IGameService
{
    public Game? Game { get; private set; }

    public string Url { get; }

    public Guid Id { get; }

    public DateTime DateTime { get; }


    public GameService()
    {
        Id = new Guid();
        Url = $"{Id}";
        DateTime = DateTime.Now;
    }

    public void AddGameReference(Game game)
    {
        Game = game;
    }
}
