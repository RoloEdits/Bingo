using ClosedXML.Excel;
using Bingo.Domain.Errors;
using Bingo.Domain;

namespace Bingo.Spreadsheet.Parser.Excel;

public static class Spreadsheet
{
    public static Dictionary<string, string> Parse(string path)
    {
        // TODO: Might break this up to just get a name and guess return and move player validation out
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

        var playerCount = 0;
        var row = 1;
        while (!worksheet.Cell(row, 1).IsEmpty())
        {
            playerCount++;
            row++;
        }

        if (playerCount == 0)
        {
            throw new NoPlayersException("No players found!");
        }

        var players = new Dictionary<string, string>(playerCount);

        short currentRow = 1;
        while (!worksheet.Cell(currentRow, 1).IsEmpty())
        {
            var name = worksheet.Cell(currentRow, 1).GetString().Trim();
            var guess = worksheet.Cell(currentRow, 2).GetString().StringFormat();

            if (!players.TryAdd(name, guess))
            {
                throw new GuessedMoreThanOnceException($"Players can only guess once! {name} guess multiple times!");
            }

            currentRow++;
        }

        return players;
    }
}