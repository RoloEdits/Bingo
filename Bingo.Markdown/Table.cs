using System.Runtime.CompilerServices;
using System.Text;
using Bingo.Domain;

namespace Bingo.Markdown;

public sealed class Table
{
    public byte Columns { get; }
    public byte Rows { get; }
    public byte TotalSquares { get; }
    private short BonusColumns { get; }

    public Table(byte columns, byte rows, byte bonus)
    {
        Columns = columns;
        Rows = rows;
        TotalSquares = (byte)(Columns * Rows);
        BonusColumns = (short)(Columns - bonus);
    }
    public string Create<T>(string corner, T[,] data)
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
    private static void WriteRows<T>(Table table, StringBuilder rows, T[,] data)
    {
        var labels = Label.Rows(table.Rows);

        for (var row = 0; row < table.Rows; row++)
        {
            // Row label.
            rows.Append($"| **{labels[row]}** |");
            // Row data.
            for (var column = 0; column < table.Columns; column++)
            {
                if (data is double[,] percentage)
                {
                    rows.Append($" {percentage[row, column].ToString("P2")} |");
                }
                else
                {
                    rows.Append($" {data[row, column]} |");
                }
            }
            rows.Append(Environment.NewLine);
        }
    }
}
