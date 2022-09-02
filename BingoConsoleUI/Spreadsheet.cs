using Bingo;
using ClosedXML.Excel;

namespace BingoConsoleUI;

internal class Spreadsheet
{
    public Dictionary<int, string> IncorrectGuessAmount { get; set; }

    public Spreadsheet(Game game, string filepath)
    {
        using var workbook = new XLWorkbook(filepath);
        var worksheet = workbook.Worksheet(1);

        var invalidGuessers = new Dictionary<int, string>();

        short currentRow = 1;
        while (!worksheet.Cell(currentRow, 1).IsEmpty())
        {

            var name = worksheet.Cell(currentRow, 1).GetString().Trim();

            var guess = Utilities.StringFormat(worksheet.Cell(currentRow, 2).GetString());

            var guessCheck = Game.CheckValidGuessAmount(guess, game.Config);

            if (!guessCheck)
            {
                invalidGuessers.Add(currentRow, name);
            }
            else
            {
                game.Players.Add(new Player(name, guess));
            }

            currentRow++;
        }

        IncorrectGuessAmount = invalidGuessers;
    }
}
