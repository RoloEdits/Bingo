namespace Bingo.Domain;

public static class Label
{
    public static List<string> Rows(byte rows)
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

    public static List<string> Squares(byte rows, byte columns)
    {
        var rowLabels = Rows(rows);
        var result = new List<string>(rows * columns);

        foreach (var rowLabel in rowLabels)
        {
            for (var i = 0; i < columns; i++)
            {
                result.Add($"{rowLabel}{(i + 1).ToString()}");
            }
        }

        return result;
    }
}