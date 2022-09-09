using Bingo;

namespace BingoConsoleUI;

internal static class Prompt
{
    public static Format Format()
    {
        var option = GetConfigOptionFromUser();

        Console.Clear();
        Ascii.Title();

        if (option == "1")
        {
            Console.WriteLine("Loaded Default Configuration....");
            return new Format(4, 3, 10, 20, 1, 2);
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

            return new Format(columns, rows, baseSquareValue, rowValueOffset, bonusColumns, bonusMultiplier);
        }
    }

    public static string Path() => GetFilePath();
    public static string Key(Format config) => GetKey(config.TotalSquares);

    // Helper functions.
    private static string GetKey(in int squares)
    {
        Console.Write("Please Enter Answer Key: ");
        var key = Console.ReadLine()?.StringFormat();
        while (key?.Length != (squares))
        {
            Console.Write($"Invalid key. Make sure you enter for {squares} squares, you entered {key?.Length}: ");
            key = Console.ReadLine()?.StringFormat();
        }

        return key;
    }

    private static string GetFilePath()
    {
        Console.Write("Please Enter File Path: ");
        var path = Console.ReadLine()?.Trim();

        while (!File.Exists(path) || System.IO.Path.GetExtension(path) != ".xlsx")
        {
            if (!File.Exists(path))
            {
                Console.Write("File does not exist. Please enter correct file path: ");
                path = Console.ReadLine()?.Trim();
            }
            else if (System.IO.Path.GetExtension(path) != ".xlsx")
            {
                Console.Write("Invalid file type. Please use a file type of '.xlsx': ");
                path = Console.ReadLine()?.Trim();
            }
        }

        return path;
    }

    private static string GetConfigOptionFromUser()
    {
        Console.Clear();
        Ascii.Title();
        Console.WriteLine("Load Default Settings: 1");
        Console.WriteLine("Enter Custom Settings: 2");
        Console.Write(Environment.NewLine);
        Console.Write(Environment.NewLine);
        Console.Write("Enter Option: ");

        var selection = Console.ReadLine()?.StringFormat();

        while (selection != "1" && selection != "2")
        {
            Console.Write($"You entered {selection}, must be either 1 or 2: ");

            selection = Console.ReadLine()?.StringFormat();
        }

        return selection;
    }

    private static byte GetColumnAmount()
    {
        Console.Write("Please Enter Column Amount( Default: 4 ): ");
        var columnsPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out var columns);
        while (columns <= 0 || !columnsPass || columns > 64)
        {
            Console.Write("Invalid amount. Please enter a number from 1 to 64: ");
            columnsPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out columns);
        }

        return columns;
    }

    private static byte GetRowAmount()
    {
        Console.Write("Please Enter Row Amount( Default: 3 ): ");
        var rowPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out var rows);
        while (rows <= 0 || !rowPass || rows > 64)
        {
            Console.Write("Invalid amount. Please enter a number from 1 to 64: ");
            rowPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out rows);
        }

        return rows;
    }

    private static byte GetBonusColumnAmount(byte columns)
    {
        Console.Write("Please Enter How Many Columns Will Be Optional( Default: 1 ): ");
        var bonusColumnsPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out var bonus);

        while (!(bonus <= columns && bonusColumnsPass))
        {
            Console.Write($"Invalid amount. Please enter a number from 0 to {columns}: ");
            bonusColumnsPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out bonus);
        }

        return bonus;
    }

    private static byte GetBaseSquareValue()
    {
        Console.Write("Please Enter The Starting Rows' Square Value( Default: 10 ): ");
        var baseSquarePass = byte.TryParse(Console.ReadLine()?.StringFormat(), out var value);

        while (!(value > 0 && baseSquarePass))
        {
            Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
            baseSquarePass = byte.TryParse(Console.ReadLine()?.StringFormat(), out value);
        }

        return value;
    }

    private static int GetRowValueOffset()
    {
        Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row( Default: 20 ): ");
        var rowOffsetPass = int.TryParse(Console.ReadLine()?.StringFormat(), out var offset);

        while (!(offset >= 0 && rowOffsetPass))
        {
            Console.Write($"Invalid amount. Please enter a number from {int.MinValue} to {int.MaxValue}: ");
            rowOffsetPass = int.TryParse(Console.ReadLine()?.StringFormat(), out offset);
        }

        return offset;
    }

    private static byte GetBonusMultiplier()
    {
        Console.Write("Please Enter Score Multiplier For Optional Column/s( Default: 2 ): ");
        var bonusMultiplierPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out var multiplier);

        while (!(multiplier > 0 && bonusMultiplierPass))
        {
            Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
            bonusMultiplierPass = byte.TryParse(Console.ReadLine()?.StringFormat(), out multiplier);
        }

        return multiplier;
    }
}
