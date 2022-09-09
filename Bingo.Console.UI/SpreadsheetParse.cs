using Bingo.Library;
using ClosedXML.Excel;

namespace Bingo.Console.UI;

internal class SpreadsheetParse
{
    private List<InvalidGuesser> InvalidGuesses { get; set; }
    private string FilePath { get; init; }

    public SpreadsheetParse(string filepath)
    {
        FilePath = filepath;
        InvalidGuesses = new List<InvalidGuesser>();
    }

    public List<Player> GetPlayers(Card format)
    {
        using var workbook = new XLWorkbook(FilePath);
        var worksheet = workbook.Worksheet(1);

        var players = new List<Player>();

        short currentRow = 1;
        while (!worksheet.Cell(currentRow, 1).IsEmpty())
        {
            var name = worksheet.Cell(currentRow, 1).GetString().Trim();

            var guess = worksheet.Cell(currentRow, 2).GetString().StringFormat();

            var guessCheck = Game.CheckValidGuessAmount(guess, format);

            if (!guessCheck)
            {
                InvalidGuesses.Add(new InvalidGuesser(currentRow, name, guess.Length));
            }
            else
            {
                players.Add(new Player(name, guess));
            }

            currentRow++;
        }

        if (InvalidGuesses.Count > 0)
        {
            System.Console.Clear();
            Ascii.Title();
            System.Console.WriteLine($"Detected: Players with incorrect amount of guesses!");
            System.Console.WriteLine($"Make sure each player has guessed for {format.TotalSquares} squares.");
            foreach (var incorrectGuesser in InvalidGuesses)
            {
                System.Console.WriteLine(
                    $"'{incorrectGuesser.Name}' in row {incorrectGuesser.Row} guessed for {incorrectGuesser.GuessAmount} squares");
            }

            System.Console.WriteLine("Please resolve issue and try again.");
            System.Console.Write("Press Enter to exit....");
            System.Console.ReadKey(true);
            System.Console.WriteLine(Environment.NewLine);
            System.Console.ResetColor();
            Environment.Exit(5);
        }

        return players;
    }
}
