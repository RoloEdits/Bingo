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

			string color;

			// If color is from a built in theme from OnlyOffice, this can fail for some reason.
			// Currently the only moment that this issue can pop-up is when manually changing the color.
			try
			{
				var red = worksheet.Cell(currentRow, 1).Style.Font.FontColor.Color.R;
				var green = worksheet.Cell(currentRow, 1).Style.Font.FontColor.Color.G;
				var blue = worksheet.Cell(currentRow, 1).Style.Font.FontColor.Color.B;

				color = $"rgb({red}, {green}, {blue})";
			}
			catch
			{
				color = $"rgb({255}, {255}, {255})";
			}

			if (!players.Add(new SpreadsheetData(currentRow, name, color, guess)))
			{
				throw new GuessedMoreThanOnceException($"{name} on row {currentRow} was input more than once, please check to make sure the correct guess is used.");
			}

			currentRow++;
		}

		return players;
	}
}
