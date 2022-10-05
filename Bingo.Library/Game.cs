using Bingo.Domain;
using Bingo.Domain.Errors;
using Bingo.Domain.Models;

namespace Bingo.Library;

public sealed class Game
{
    public List<IPlayer> Players { get;}
    public ICard Card { get; }
    public char[,] Key { get; }
    public ISettings Settings { get; }
    public Stats Stats { get; }

    public Game(string key, ICard card, ISettings settings, IDictionary<string, string> players)
    {
        Card = card;
        Settings = settings;
        Players = new List<IPlayer>(players.Count);
        Key = Utilities.SpanTo2DArray<char>(key, Card.Rows, Card.Columns);

        foreach (var player in players)
        {
            Players.Add(new Player(player.Key, player.Value, Card.Rows, Card.Columns));
        }

        var invalidGuessers = CheckForInvalidGuessers();
        if (invalidGuessers.Count > 0)
        {
            throw new InvalidUserGuessAmountException("Invalid Guesses!", invalidGuessers);
        }

        Stats = new Stats(Card, Players.Count);
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
        Stats.AggregateResults(Players);
    }

    // TODO: Move this to somewhere else
    private List<InvalidGuesser> CheckForInvalidGuessers()
    {
        var invalidGuessers = new List<InvalidGuesser>();
        foreach (var player in Players)
        {
            if (player.Guess.Length != (Card.Columns * Card.Rows))
            {
                invalidGuessers.Add(new InvalidGuesser(player.Name, player.Guess.Length));
            }
        }

        return invalidGuessers;
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
                                CollectResult(Card.SquareLabels[row, column].Label, 1);
                            }
                        }
                        else
                        {
                            score -= (squareValue * Card.BonusMultiplier);
                            if (Settings.WillLogStats)
                            {
                                CollectResult(Card.SquareLabels[row, column].Label, -1);
                            }
                        }
                    }
                    else
                    {
                        if (Settings.WillLogStats)
                        {
                            CollectResult(Card.SquareLabels[row, column].Label, 0);
                        }
                    }
                }
                else if (player.Guess[row, column] == Key[row, column])
                {
                    score += squareValue;
                    if (Settings.WillLogStats)
                    {
                        CollectResult(Card.SquareLabels[row, column].Label, 1);
                    }
                }
                else
                {
                    score -= squareValue;
                    if (Settings.WillLogStats)
                    {
                        CollectResult(Card.SquareLabels[row, column].Label, -1);
                    }
                }
            }
            squareValue += Card.RowValueOffset;
        }

        player.Score = score;

        void CollectResult(string square, short result)
        {
            if (Settings.WillCountAllSameGuessersInStats)
            {
                player.ResultPerSquare.Add(square, result);
                return;
            }

            if (!player.IsAllSameGuess)
            {
                player.ResultPerSquare.Add(square, result);
            }
        }
    }
}
