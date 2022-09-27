namespace Bingo.Domain;

public static class Label
{
    public static List<string> Rows(byte rows)
    {
        var label = new List<string>(rows);

        for (var letter = 'A'; letter <= 'Z'; letter++)
        {
            if (label.Count != rows)
            {
                label.Add($"{letter}");
            }
        }
        return label;
    }

    public static List<string> Squares(byte rows, byte columns)
    {
        var rowLabels = Rows(rows);
        var result = new List<string>(rows * columns);

        foreach (var rowLabel in rowLabels)
        {
            for (var i = 0; i < columns; i++)
            {
                result.Add($"{rowLabel}{i + 1}");
            }
        }

        return result;
    }
}
