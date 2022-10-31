using Bingo.Domain.Errors;
using Bingo.Domain.Models;
using Bingo.Domain.ValueObjects;
using Bingo.Spreadsheet;

namespace Bingo.Core;

public sealed class Game
{
    public List<IPlayer> Players { get; private set; }
    public ICard Card { get; }
    public Key Key { get; }
    private ISettings Settings { get; }
    public Stats Stats { get; }

    public Game(string key, ICard card, ISettings settings)
    {
        Card = card;
        Settings = settings;
        Stats = new Stats(Card, Settings)
        {
            MaxScorePossible = Stats.GetMaxScore(Card)
        };

        Key = new Key(key, Card.Rows, Card.Columns);
    }

    public List<InvalidGuesser> AddPlayers(in HashSet<SpreadsheetData> players)
    {
        Stats.PlayerCount = players.Count;

        Players = new List<IPlayer>(players.Count);

        var invalidGuessers = new List<InvalidGuesser>();
        foreach (var player in players)
        {
            try
            {
                Players.Add(new Player(player.Name, new Guess(player.Guess, Card.Rows, Card.Columns)));
            }
            catch (InvalidSquareAmountException)
            {
                invalidGuessers.Add(new InvalidGuesser(player.Row,player.Name, player.Guess.Length));
            }
        }

        return invalidGuessers;
    }

    public void Play()
    {
        var start = DateTime.UtcNow;

        foreach (var player in Players)
        {
            CalculateScore(player);
        }

        var spent = DateTime.UtcNow - start;

        Stats.ScoreCalculationTime = (double)spent.Ticks / 10_000;
        Stats.AggregateResults(this);
    }

    private void CalculateScore(IPlayer player)
    {
        long score = 0;
        int squareValue = Card.BaseSquareValue;

        for (short row = 0; row < Card.Rows; row++)
        {
            for (var column = 0; column < Card.Columns; column++)
            {
                // If game has no bonus columns and they allow skipping, then this is always set to true. This allows the same structure to be used for scoring.
                // Only the bonus path is taken and the multiplier is always 1.
                var isBonusColumn = (column + Card.BonusColumns) >= Card.Columns || (Settings.AllowSkippingWhenThereIsNoBonus && Card.BonusMultiplier == 1);

                if (isBonusColumn)
                {
                    var isNotSkipChar = player.Guess[row, column] != Card.BonusSkipChar;

                    if (isNotSkipChar)
                    {
                        if (player.Guess[row, column] == Key[row, column])
                        {
                            score += (squareValue * Card.BonusMultiplier);
                            CollectResult(Card.SquareLabels[row, column].Label, Result.Correct);
                        }
                        else
                        {
                            score -= (squareValue * Card.BonusMultiplier);
                            CollectResult(Card.SquareLabels[row, column].Label, Result.Incorrect);
                        }
                    }
                    else
                    {
                        CollectResult(Card.SquareLabels[row, column].Label, Result.Skipped);
                    }
                }
                else if (player.Guess[row, column] == Key[row, column])
                {
                    score += squareValue;
                    CollectResult(Card.SquareLabels[row, column].Label, Result.Correct);
                }
                else
                {
                    score -= squareValue;
                    CollectResult(Card.SquareLabels[row, column].Label, Result.Incorrect);
                }
            }

            if (Card.RowValueOffset != 0)
            {
                squareValue += Card.RowValueOffset;
            }
        }

        player.Score = score;

        void CollectResult(string square, Result result)
        {
            if (Settings.WillCountAllSameGuessersInStats)
            {
                player.ResultPerSquare.Add(square, result);
                return;
            }

            if (!player.Guess.IsAllSame)
            {
                player.ResultPerSquare.Add(square, result);
            }
        }
    }
}
