using System.Text;
using Bingo.Library;

namespace Bingo.Markdown;

public class Table : IGrid
{
    public byte Columns { get; init; }
    public byte Rows { get; init; }
    public byte TotalSquares { get; init; }
    private short BonusColumns { get; }

    public Table(byte columns, byte rows, byte bonus)
    {
        Columns = columns;
        Rows = rows;
        TotalSquares = (byte)(Columns * Rows);
        BonusColumns = (short)(Columns - bonus);
    }

    public string CreateDynamic<T>(string corner, List<T> data)
    {
        var table = this;
        var builder = new StringBuilder();

        WriteHeader(table, builder, corner);
        WriteDivider(table, builder);
        WriteRows(table, builder, data);

        return builder.ToString();
    }

    // TODO - Add check for if bonus columns == total columns. If so then dont add the bonus label.
    private static void WriteHeader(Table table, StringBuilder header, string corner)
    {
        header.Append($"| {corner} |");

        for (var headerColumn = 0; headerColumn < table.Columns; headerColumn++)
        {
            if (headerColumn >= table.BonusColumns)
            {
                header.Append(" Bonus |");
            }
            else
            {
                header.Append($" {headerColumn + 1} |");
            }
        }

        header.Append(Environment.NewLine);
    }

    private static void WriteDivider(IGrid table, StringBuilder divider)
    {
        divider.Append(" :---: |");

        for (var i = 0; i < table.Columns; i++)
        {
            divider.Append(" :---: |");
        }

        divider.Append(Environment.NewLine);
    }

    private static void WriteRows<T>(IGrid table, StringBuilder rows, IReadOnlyList<T> data)
    {
        var absolute = 0;
        // That rows final column square
        short finalColumn = table.Columns;

        var labels = GetLabel(table.Rows);

        for (var row = 0; row < table.Rows; row++)
        {
            // Writes out row label.
            rows.Append($"| **{labels[row]}** |");
            // Writes out each row.
            for (var current = absolute; current < finalColumn; current++)
            {
                // Writes out each square's value.
                rows.Append($" {data[current]} |");

                absolute = current + 1;
            }

            finalColumn += table.Columns;
            // Goes to next row.
            rows.Append(Environment.NewLine);
        }
    }

    private static List<string> GetLabel(byte rows)
    {
        var label = new List<string>();
        if (rows < 27)
        {
            for (var ones = 'A'; ones <= 'Z'; ones++)
            {
                label.Add($"{ones}");
            }

            return label;
        }

        for (var ones = 'A'; ones <= 'Z'; ones++)
        {
            label.Add($"{ones}");
        }

        for (var tens = 'A'; tens <= 'Z'; tens++)
        {
            for (var ones = 'A'; ones <= 'Z'; ones++)
            {
                label.Add($"{tens}{ones}");
                if (label.Count >= 64)
                {
                    return label;
                }
            }
        }

        return label;
    }
}
