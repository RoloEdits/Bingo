using Bingo.Domain.Errors;
using Bingo.Domain.Models;

namespace Bingo.Library;

public sealed class Game
{
    public List<IPlayer> Players { get; set; }
    public ICard Card { get; set; }
    public string Key { get; set; }
    public ISettings Settings { get; set; }
    public Stats Stats { get; set; }

    public Game(string key, ICard card, ISettings settings, IDictionary<string, string> players)
    {
        Key = key;
        Card = card;
        Settings = settings;
        Players = new();

        foreach (var player in players)
        {
            Players.Add(new Player(player.Key, player.Value));
        }

        var invalidGuessers = CheckForInvalidGuessers();
        if (invalidGuessers.Count > 0)
        {
            throw new InvalidUserGuessAmountException("Invalid Guesses!", invalidGuessers);
        }

        Stats = new Stats(Card.TotalSquares, Players.Count);

        for (var i = 0; i < Card.TotalSquares; i++)
        {
            Stats.CorrectGuessesPerSquare.Add(Card.SquareLabels[i], 0);
        }
    }

    public void Play()
    {
        var start = DateTime.UtcNow;

        foreach (var player in Players)
        {
            player.Score = CalculatePlayerScore(player);
        }

        var spent = DateTime.UtcNow - start;

        Stats.ScoreCalculationTime = (double)spent.Ticks / 10_000;
        Stats.GetCorrectGuessesPerSquarePercentage();
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

    public long CalculatePlayerScore(IPlayer player)
    {
        long score = 0;

        var columns = Card.Columns;
        var rows = Card.Rows;
        var bonusAmount = Card.BonusColumns;
        var bonusChar = Card.BonusSkipChar;
        short finalColumnOfRow = Card.Columns;
        long squareValue = Card.BaseSquareValue;
        var bonusMultiplier = Card.BonusMultiplier;
        var rowOffset = Card.RowValueOffset;

        var holder = 0;

        /* Data structure is effectively a Parallel Array.
         * An index to index check is implemented for the key and player guess.
         * Offers both simpler implementation compared to KV pairs or 2D jagged arrays and better or same performance.
         */
        for (short rowCounter = 0; rowCounter < rows; rowCounter++)
        {
            for (var current = holder; current < finalColumnOfRow; current++)
            {
                var isBonus = (current + bonusAmount) >= finalColumnOfRow;
                var isNotSkipChar = player.Guess[current] != bonusChar;

                if (isBonus)
                {
                    if (isNotSkipChar)
                    {
                        if (player.Guess[current] == Key[current])
                        {
                            score += (squareValue * bonusMultiplier);
                            if (Settings.WillLogStats) AddCorrectGuess(Card.SquareLabels[current]);
                            player.ResultPerSquare.Add(new PlayerPerSquareResult(Card.SquareLabels[current], 1));

                        }
                        else
                        {
                            score -= (squareValue * bonusMultiplier);
                            player.ResultPerSquare.Add(new PlayerPerSquareResult(Card.SquareLabels[current], -1));
                        }
                    }
                    else
                    {
                        player.ResultPerSquare.Add(new PlayerPerSquareResult(Card.SquareLabels[current], 0));
                        // TODO: For player, make result 0 as they skipped a bonus.
                    }

                }
                else if (player.Guess[current] == Key[current])
                {
                    score += squareValue;
                    player.ResultPerSquare.Add(new PlayerPerSquareResult(Card.SquareLabels[current], 1));
                    if (Settings.WillLogStats) AddCorrectGuess(Card.SquareLabels[current]);
                }
                else
                {
                    score -= squareValue;
                    player.ResultPerSquare.Add(new PlayerPerSquareResult(Card.SquareLabels[current], -1));
                }

                holder = current + 1;
            }

            squareValue += rowOffset;
            finalColumnOfRow += columns;
        }

        return score;

        void AddCorrectGuess(string square)
        {
            if (Stats.CorrectGuessesPerSquare is null) return;

            if (Settings.WillCountAllSameGuessersInStats)
            {
                Stats.CorrectGuessesPerSquare[square]++;
                return;
            }

            if (!player.IsAllSameGuess)
            {
                Stats.CorrectGuessesPerSquare[square]++;
            }
        }
    }
}
