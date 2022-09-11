namespace Bingo.Library;

public class Game : IGame
{
    // TODO - See about making a game settings class for things like whether user wants to track stats, or to include all same guessers
    public List<Player> Players { get; set; }
    public ICard Card { get; init; }
    public string Key { get; init; }
    public List<InvalidGuesser> InvalidGuesses { get; } = new();

    // Settings
    public static string Path;
    private bool WillLogStats { get; }
    private bool WillCountAllSameGuessInStats { get; }

    // Stats information
    public Stats Stats { get; }

    // TODO - Sure up Player validation so that compiler knows it cant be null
    public Game(string key, ICard card, bool willLogStats = true, bool willCountAllSameGuessInStats = false)
    {
        Key = key;
        Card = card;
        WillLogStats = willLogStats;
        WillCountAllSameGuessInStats = willCountAllSameGuessInStats;

        if (WillLogStats)
        {
            Stats = new Stats
            {
                CorrectGuessesPerSquarePercentage = new List<double>(Card.TotalSquares),
                CorrectGuessesPerSquare = new Dictionary<int, uint>(Card.TotalSquares),
                ScoreCalculationTime = 0.0
            };

            for (var i = 0; i < Card.TotalSquares; i++)
            {
                Stats.CorrectGuessesPerSquare.Add(i, 0);
            }
        }
    }

    public void Play()
    {
        var (players, count) = Spreadsheet.Parse();

        Players = players;
        Stats.PlayerCount = count;

        ValidatePlayersGuessAmount();

        var start = DateTime.UtcNow;

        foreach (var player in Players)
        {
            player.Score = CalculatePlayerScore(player.Guess, player.IsAllSameGuess);
        }

        var spent = DateTime.UtcNow - start;

        Stats.ScoreCalculationTime = (double)spent.Ticks / 10_000;
        Stats.GetCorrectGuessesPerSquarePercentage();
    }

    private void ValidatePlayersGuessAmount()
    {
        for (var row = 0; row < Players.Count; row++)
        {
            var player = Players[row];
            if (player.Guess.Length != (Card.Columns * Card.Rows))
            {
                InvalidGuesses.Add(new InvalidGuesser(row, player.Name, player.Guess.Length));
            }
        }
    }

    public long CalculatePlayerScore(string guess, bool isAllSameGuess)
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
                var isNotSkipChar = guess[current] != bonusChar;

                if (isBonus && isNotSkipChar)
                {
                    if (guess[current] == Key[current])
                    {
                        score += (squareValue * bonusMultiplier);
                        if (WillLogStats) AddCorrectGuess(current);
                    }
                    else
                    {
                        score -= (squareValue * bonusMultiplier);
                    }
                }
                else if (guess[current] == Key[current])
                {
                    score += squareValue;
                    if (WillLogStats) AddCorrectGuess(current);
                }
                else
                {
                    score -= squareValue;
                }

                holder = current + 1;
            }

            squareValue += rowOffset;
            finalColumnOfRow += columns;
        }

        return score;

        void AddCorrectGuess(int square)
        {
            if (WillCountAllSameGuessInStats)
            {
                Stats.CorrectGuessesPerSquare[square]++;
                return;
            }

            if (!isAllSameGuess)
            {
                Stats.CorrectGuessesPerSquare[square]++;
            }
        }
    }
}
