using Bingo;
using ClosedXML.Excel;

namespace BingoConsoleUI;

internal class SpreadsheetParse
{
    public List<InvalidGuesser> InvalidGuesses { get; set; }
    public string FilePath { get; init; }

    public SpreadsheetParse(string filepath)
    {
        FilePath = filepath;
        InvalidGuesses = new List<InvalidGuesser>();
    }

    public List<Player> GetPlayers(Format format)
    {
        using var workbook = new XLWorkbook(FilePath);
        var worksheet = workbook.Worksheet(1);

        var players = new List<Player>();

        short currentRow = 1;
        while (!worksheet.Cell(currentRow, 1).IsEmpty())
        {
            var name = worksheet.Cell(currentRow, 1).GetString().Trim();

            var guess = Utilities.StringFormat(worksheet.Cell(currentRow, 2).GetString());

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
            Console.Clear();
            Ascii.Title();
            Console.WriteLine($"Detected: Players with incorrect amount of guesses!");
            Console.WriteLine($"Make sure each player has guessed for {format.TotalSquares.ToString()} squares.");
            foreach (var incorrectGuesser in InvalidGuesses)
            {
                Console.WriteLine($"'{incorrectGuesser.Name}' in row {incorrectGuesser.Row.ToString()} guessed for {incorrectGuesser.GuessAmount.ToString()} squares");
            }
            Console.WriteLine("Please resolve issue and try again.");
            Console.Write("Press Enter to exit....");
            Console.ReadKey(true);
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
            Environment.Exit(5);
        }

        return players;
    }
}