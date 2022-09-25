using System.Text;
using Bingo.Domain;

namespace Bingo.Markdown;

public sealed class Table
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

        Build(table, builder, corner);
        WriteDivider(table, builder);
        WriteRows(table, builder, data);

        return builder.ToString();
    }

    private static void Build(Table table, StringBuilder header, string corner)
    {
        header.Append($"| {corner} |");

        for (var headerColumn = 0; headerColumn < table.Columns; headerColumn++)
        {
            if (headerColumn >= table.BonusColumns && table.BonusColumns != 0)
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

    private static void WriteDivider(Table table, StringBuilder divider)
    {
        divider.Append(" :---: |");

        for (var i = 0; i < table.Columns; i++)
        {
            divider.Append(" :---: |");
        }

        divider.Append(Environment.NewLine);
    }

    private static void WriteRows<T>(Table table, StringBuilder rows, IReadOnlyList<T> data)
    {
        var absolute = 0;
        // That rows final column square
        short finalColumn = table.Columns;

        var labels = Label.Rows(table.Rows);

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
}