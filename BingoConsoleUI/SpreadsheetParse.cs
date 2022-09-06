using Bingo;
using ClosedXML.Excel;

namespace BingoConsoleUI;

internal class SpreadsheetParse
{
    public Dictionary<int, string> InvalidGuesses { get; set; }
    public string FilePath { get; init; }

    public SpreadsheetParse(string filepath)
    {
        FilePath = filepath;
        InvalidGuesses = new Dictionary<int, string>();
    }

    public List<Player> GetPlayers(Format format)
    {
        using var workbook = new XLWorkbook(FilePath);
        var worksheet = workbook.Worksheet(1);

        InvalidGuesses = new Dictionary<int, string>();

        var players = new List<Player>();

        short currentRow = 1;
        while (!worksheet.Cell(currentRow, 1).IsEmpty())
        {
            var name = worksheet.Cell(currentRow, 1).GetString().Trim();

            var guess = Utilities.StringFormat(worksheet.Cell(currentRow, 2).GetString());

            var guessCheck = Game.CheckValidGuessAmount(guess, format);

            if (!guessCheck)
            {
                InvalidGuesses.Add(currentRow, name);
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
            Utilities.AsciiTitle();
            Console.WriteLine($"Detected: Players with incorrect amount of guesses!");
            foreach (var incorrectGuesser in InvalidGuesses)
            {
                var (row, name) = incorrectGuesser;
                Console.WriteLine($"Row:{row} Name:{name}");
            }
            Console.Write("Please resolve issue and try again.");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
            Environment.Exit(5);
        }

        return players;
    }

    public Dictionary<int, string> GetInvalidGuesses()
    {
        return InvalidGuesses;
    }
}
