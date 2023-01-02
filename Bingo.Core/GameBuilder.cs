using Bingo.Domain.Errors;
using Bingo.Spreadsheet;

namespace Bingo.Core;

public class GameBuilder
{
    private HashSet<SpreadsheetData>? _players;
    private Card? _card;
    private string? _keyString;
    private Settings? _settings;

    public GameBuilder AddKey(string key)
    {
        _keyString = key;
        return this;
    }

    public GameBuilder AddCard(in Card card)
    {
        _card = card;
        return this;
    }

    public GameBuilder AddSettings(in Settings settings)
    {
        _settings = settings;
        return this;
    }

    public GameBuilder AddPlayers( in HashSet<SpreadsheetData> players )
    {
        _players = players;
        return this;
    }

    public Game Build()
    {
        if (_keyString is null || _card is null || _settings is null || _players is null)
        {
            throw new InvalidOperationException("Not all fields to build a game were given");
        }

        return new Game(_keyString, _card, _settings, _players);
    }

}
