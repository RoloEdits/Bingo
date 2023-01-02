using System.Text;
using Bingo.Domain.Models;

namespace Bingo.Write;

public sealed class Table
{
    private int Columns { get; }
    private int Rows { get; }
    private int TotalSquares { get; }
    private int BonusColumns { get; }

    public Table(int columns, int rows, int bonus)
    {
        Columns = columns;
        Rows = rows;
        TotalSquares = Columns * Rows;
        BonusColumns = Columns - bonus;
    }

    public string Create<T>(string cornerLabel, T[,] data)
    {
        var builder = new StringBuilder();

        BuildHeader(builder, cornerLabel);
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
