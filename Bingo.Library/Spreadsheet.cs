using ClosedXML.Excel;

namespace Bingo.Library;

internal static class Spreadsheet
{
    public static (List<Player>, int) Parse()
    {
        // TODO - Error handling for if the file is already open. Will hold on error and  prompt to hit a key when the file is closed.
        try
        {
            using var workbook = new XLWorkbook(Game.Path);
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
                throw new Exception("No players found");
            }

            var players = new List<Player>(playerCount);

            short currentRow = 1;
            while (!worksheet.Cell(currentRow, 1).IsEmpty())
            {
                var name = worksheet.Cell(currentRow, 1).GetString().Trim();
                var guess = worksheet.Cell(currentRow, 2).GetString().StringFormat();

                players.Add(new Player(name, guess));

                currentRow++;
            }

            return (players, playerCount);
        }
        catch
        {
            throw new Exception("Couldn't open file");
        }
    }
}