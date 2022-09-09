using Bingo.Library;

namespace Bingo.Console.UI;

internal static class Prompt
{
    public static Card Format()
    {
        var option = GetConfigOptionFromUser();

        System.Console.Clear();
        Ascii.Title();

        if (option == "1")
        {
            System.Console.WriteLine("Loaded Default Configuration....");
            return new Card(4, 3, 10, 20, 1, 2);
        }
        else
        {
            var columns = GetColumnAmount();
            var rows = GetRowAmount();
            var baseSquareValue = GetBaseSquareValue();
            var rowValueOffset = GetRowValueOffset();
            var bonusColumns = GetBonusColumnAmount(columns);

            byte bonusMultiplier = 0;
            if (bonusColumns != 0)
            {
                bonusMultiplier = GetBonusMultiplier();
            }

            return new Card(columns, rows, baseSquareValue, rowValueOffset, bonusColumns, bonusMultiplier);
        }
    }

    public static string Path() => GetFilePath();
    public static string Key(Card config) => GetKey(config.TotalSquares);

    // Helper functions.
    private static string GetKey(in int squares)
    {
        System.Console.Write("Please Enter Answer Key: ");
        var key = System.Console.ReadLine()?.StringFormat();
        while (key?.Length != (squares))
        {
            System.Console.Write($"Invalid key. Make sure you enter for {squares} squares, you entered {key?.Length}: ");
            key = System.Console.ReadLine()?.StringFormat();
        }

        return key;
    }

    private static string GetFilePath()
    {
        System.Console.Write("Please Enter File Path: ");
        var path = System.Console.ReadLine()?.Trim();

        while (!File.Exists(path) || System.IO.Path.GetExtension(path) != ".xlsx")
        {
            if (!File.Exists(path))
            {
                System.Console.Write("File does not exist. Please enter correct file path: ");
                path = System.Console.ReadLine()?.Trim();
            }
            else if (System.IO.Path.GetExtension(path) != ".xlsx")
            {
                System.Console.Write("Invalid file type. Please use a file type of '.xlsx': ");
                path = System.Console.ReadLine()?.Trim();
            }
        }

        return path;
    }

    private static string GetConfigOptionFromUser()
    {
        System.Console.Clear();
        Ascii.Title();
        System.Console.WriteLine("Load Default Settings: 1");
        System.Console.WriteLine("Enter Custom Settings: 2");
        System.Console.Write(Environment.NewLine);
        System.Console.Write(Environment.NewLine);
        System.Console.Write("Enter Option: ");

        var selection = System.Console.ReadLine()?.StringFormat();

        while (selection != "1" && selection != "2")
        {
            System.Console.Write($"You entered {selection}, must be either 1 or 2: ");

            selection = System.Console.ReadLine()?.StringFormat();
        }

        return selection;
    }

    private static byte GetColumnAmount()
    {
        System.Console.Write("Please Enter Column Amount( Default: 4 ): ");
        var columnsPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out var columns);
        while (columns <= 0 || !columnsPass || columns > 64)
        {
            System.Console.Write("Invalid amount. Please enter a number from 1 to 64: ");
            columnsPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out columns);
        }

        return columns;
    }

    private static byte GetRowAmount()
    {
        System.Console.Write("Please Enter Row Amount( Default: 3 ): ");
        var rowPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out var rows);
        while (rows <= 0 || !rowPass || rows > 64)
        {
            System.Console.Write("Invalid amount. Please enter a number from 1 to 64: ");
            rowPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out rows);
        }

        return rows;
    }

    private static byte GetBonusColumnAmount(byte columns)
    {
        System.Console.Write("Please Enter How Many Columns Will Be Optional( Default: 1 ): ");
        var bonusColumnsPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out var bonus);

        while (!(bonus <= columns && bonusColumnsPass))
        {
            System.Console.Write($"Invalid amount. Please enter a number from 0 to {columns}: ");
            bonusColumnsPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out bonus);
        }

        return bonus;
    }

    private static byte GetBaseSquareValue()
    {
        System.Console.Write("Please Enter The Starting Rows' Square Value( Default: 10 ): ");
        var baseSquarePass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out var value);

        while (!(value > 0 && baseSquarePass))
        {
            System.Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
            baseSquarePass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out value);
        }

        return value;
    }

    private static int GetRowValueOffset()
    {
        System.Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row( Default: 20 ): ");
        var rowOffsetPass = int.TryParse(System.Console.ReadLine()?.StringFormat(), out var offset);

        while (!(offset >= 0 && rowOffsetPass))
        {
            System.Console.Write($"Invalid amount. Please enter a number from {int.MinValue} to {int.MaxValue}: ");
            rowOffsetPass = int.TryParse(System.Console.ReadLine()?.StringFormat(), out offset);
        }

        return offset;
    }

    private static byte GetBonusMultiplier()
    {
        System.Console.Write("Please Enter Score Multiplier For Optional Column/s( Default: 2 ): ");
        var bonusMultiplierPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out var multiplier);

        while (!(multiplier > 0 && bonusMultiplierPass))
        {
            System.Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
            bonusMultiplierPass = byte.TryParse(System.Console.ReadLine()?.StringFormat(), out multiplier);
        }

        return multiplier;
    }
}
