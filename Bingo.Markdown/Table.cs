using System.Text;
using Bingo.Domain.Models;

namespace Bingo.Markdown;

public sealed class Table
{
    private byte Columns { get; }
    private byte Rows { get; }
    private byte TotalSquares { get; }
    private byte BonusColumns { get; }

    public Table(byte columns, byte rows, byte bonus)
    {
        Columns = columns;
        Rows = rows;
        TotalSquares = (byte)(Columns * Rows);
        BonusColumns = (byte)(Columns - bonus);
    }

    public string Create<T>(string corner, T[,] data)
    {
        var builder = new StringBuilder();

        BuildHeader(builder, corner);
        BuildDivider(builder);
        BuildRows(builder, data);

        return builder.ToString();
    }

    private void BuildHeader(StringBuilder builder, string corner)
    {
        builder.Append($"| {corner} |");

        for (var headerColumn = 0; headerColumn < Columns; headerColumn++)
        {
            if (headerColumn >= BonusColumns && BonusColumns != 0)
            {
                builder.Append(" Bonus |");
            }
            else
            {
                builder.Append($" {headerColumn + 1} |");
            }
        }

        builder.Append(Environment.NewLine);
    }

    private void BuildDivider(StringBuilder builder)
    {
        builder.Append(" :---: |");

        for (var i = 0; i < Columns; i++)
        {
            builder.Append(" :---: |");
        }

        builder.Append(Environment.NewLine);
    }

    private void BuildRows<T>(StringBuilder builder, T[,] data)
    {
        for (var row = 0; row < Rows; row++)
        {
            // Row label.
            builder.Append($"| **{Label.Rows[row]}** |");
            // Row data.
            for (var column = 0; column < Columns; column++)
            {
                if (data is double[,] percentage)
                {
                    builder.Append($" {percentage[row, column].ToString("P2")} |");
                }
                else
                {
                    builder.Append($" {data[row, column]} |");
                }
            }

            builder.Append(Environment.NewLine);
        }
    }
}
