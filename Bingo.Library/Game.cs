using Bingo.Domain.Errors;
using Bingo.Domain.Models;
using Bingo.Domain.ValueObjects;
using Bingo.Domain;

namespace Bingo.Core;

public sealed class Game
{
    public List<IPlayer> Players { get;}
    public ICard Card { get; }
    public Key Key { get; }
    public ISettings Settings { get; }
    public Stats Stats { get; }

    public Game(string key, ICard card, ISettings settings, IDictionary<string, string> players)
    {
        Card = card;
        Settings = settings;
        Players = new List<IPlayer>(players.Count);
        Key = new Key(key, Card.Rows, Card.Columns);

        var invalidGuessers = new List<InvalidGuesser>();
        foreach (var player in players)
        {
            try
            {
                Players.Add(new Player(player.Key, new Guess(player.Value, Card.Rows, Card.Columns)));
            }
            catch (InvalidSquareAmountException ex)
            {
                invalidGuessers.Add(new InvalidGuesser(player.Key, player.Value.Length));
            }
            // TODO: Add rest of the Exceptions
        }

        if (invalidGuessers.Count > 0)
        {
            // TODO: Implement a way to warn bingo manager of all the bad guessers in a way that is UI agnostic.
        }

        Stats = new Stats(Card, Players.Count);
        Stats.MaxScorePossible = Stats.GetMaxScore(Key, Key, Card);
    }

    public void  Play()
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

    public void CalculateScore(IPlayer player)
    {
        long score = 0;
        int squareValue = Card.BaseSquareValue;

        for (short row = 0; row < Card.Rows; row++)
        {
            for (var column = 0; column < Card.Columns; column++)
            {
                var isBonusColumn = (column + Card.BonusColumns) >= Card.Columns;

                if (isBonusColumn)
                {
                    var isNotSkipChar = player.Guess[row, column] != Card.BonusSkipChar;

                    if (isNotSkipChar)
                    {
                        if (player.Guess[row, column] == Key[row, column])
                        {
                            score += (squareValue * Card.BonusMultiplier);
                            if (Settings.WillLogStats)
                            {
                                CollectResult(Card.SquareLabels[row, column].Label, Result.Correct);
                            }
                        }
                        else
                        {
                            score -= (squareValue * Card.BonusMultiplier);
                            if (Settings.WillLogStats)
                            {
                                CollectResult(Card.SquareLabels[row, column].Label, Result.Incorrect);
                            }
                        }
                    }
                    else
                    {
                        if (Settings.WillLogStats)
                        {
                            CollectResult(Card.SquareLabels[row, column].Label, Result.Skipped);
                        }
                    }
                }
                else if (player.Guess[row, column] == Key[row, column])
                {
                    score += squareValue;
                    if (Settings.WillLogStats)
                    {
                        CollectResult(Card.SquareLabels[row, column].Label, Result.Correct);
                    }
                }
                else
                {
                    score -= squareValue;
                    if (Settings.WillLogStats)
                    {
                        CollectResult(Card.SquareLabels[row, column].Label, Result.Incorrect);
                    }
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
