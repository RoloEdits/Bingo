using Bingo.Domain;
using Bingo.Domain.Errors;
using Bingo.Domain.Models;
using Bingo.Domain.ValueObjects;
using Bingo.Spreadsheet;
using Newtonsoft.Json;

namespace Bingo.Core;

public sealed class Game
{
	public List<Player> Players { get; private set; }
	public Card Card { get; }
	public Key Key { get; }
	public Settings Settings { get; }
	public Stats Stats { get; }

	internal Game(string key, Card card, Settings settings, HashSet<SpreadsheetData> players)
	{
		Card = card;
		Settings = settings;
		Stats = new Stats(Card, Settings)
		{
			MaxScorePossible = Stats.GetMaxScore(Card)
		};

		Players = new List<Player>(players.Count);
		Stats.PlayerCount = players.Count;

		var invalidGuessers = new List<InvalidGuesser>();
		foreach (var player in players)
		{
			try
			{
				Players.Add(new Player(player.Name, new Guess(player.Guess, (byte)Card.Rows, (byte)Card.Columns)));
			}
			catch (InvalidSquareAmountException)
			{
				invalidGuessers.Add(new InvalidGuesser(player.Row, player.Name, player.Guess.Length));
			}
		}

		if (invalidGuessers.Count > 0)
		{
			throw new InvalidGuessersException(invalidGuessers);
		}

		Key = new Key(key.StringFormat(), (byte)Card.Rows, (byte)Card.Columns);
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
		var squareValue = Card.BaseSquareValue;

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

	public string ToJson()
	{
		var settings = new JsonSerializerSettings { Formatting = Formatting.Indented};
		var json = JsonConvert.SerializeObject(this, settings);
		return json;
	}
}
