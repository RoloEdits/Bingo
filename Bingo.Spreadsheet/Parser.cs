using Bingo.Domain;
using Bingo.Domain.Errors;
using ClosedXML.Excel;

namespace Bingo.Spreadsheet;

public static class Parser
{
    public static HashSet<SpreadsheetData> Parse(string path)
    {
        try
        {
            using var wb = new XLWorkbook(path);
        }
        catch (Exception)
        {
            throw new CannotOpenFileException("Couldn't open file! Make sure it's not open by another program.");
        }

        using var workbook = new XLWorkbook(path);
        var worksheet = workbook.Worksheet(1);

        var count = 0;
        var row = 1;
        while (!worksheet.Cell(row, 1).IsEmpty())
        {
            count++;
            row++;
        }

        if (count == 0)
        {
            throw new NoPlayersException("No players found!");
        }

        var players = new HashSet<SpreadsheetData>(count);

        ushort currentRow = 1;
        while (currentRow != row)
        {
            var name = worksheet.Cell(currentRow, 1).GetString().Trim();
            var guess = worksheet.Cell(currentRow, 2).GetString().StringFormat();

            if (!players.Add(new SpreadsheetData(currentRow, name, guess)))
            {
                throw new GuessedMoreThanOnceException($"{name} on row {currentRow} was input more than once, please check to make sure the correct guess is used.");
            }

            currentRow++;
        }

        return players;
    }
}
