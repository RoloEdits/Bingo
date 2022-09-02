using Bingo;

namespace BingoConsoleUI;
internal class Prompt
{
    public static Config Config()
    {
        var option = GetConfigOptionFromUser();
        var config = new Config();

        Console.Clear();
        Utilities.AsciiTitle();

        if (option == "1")
        {
            Console.WriteLine("Loaded Default Configuration....");
        }
        else
        {
            config.Columns = GetColumnAmount();
            config.Rows = GetRowAmount();
            config.BaseSquareValue = GetBaseSquareValue();
            config.RowValueOffset = GetRowValueOffset();
            config.BonusColumns = GetBonusColumnAmount(config.Columns);
            if (config.BonusColumns != 0)
            {
                config.BonusMultiplier = GetBonusMultiplier();
            }
        }
        return config;
    }
    public static string Path() => GetFilePath();
    public static string Key(Config config) => GetKey(config.Rows * config.Columns);

    // Helper functions.
    private static string GetKey(int squares)
    {
        Console.Write("Please Enter Answer Key: ");
        var key = Console.ReadLine()?.StringFormat();
        while (key?.Length != (squares))
        {
            Console.Write($"Invalid key. Make sure you enter for {squares} squares: ");
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
                Console.Write("Invalid filetype. Please use a filetype of '.xlsx': ");
                path = Console.ReadLine()?.Trim();
            }
        }
        return path;
    }
    private static string GetConfigOptionFromUser()
    {
        Console.Clear();
        Utilities.AsciiTitle();

        Console.WriteLine("Load Default Settings: 1");
        Console.WriteLine("Enter Custom Settings: 2");
        Console.WriteLine();
        Console.WriteLine();
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
        var columnsPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out var columns);
        while (!(columns > 0 && columnsPass))
        {
            Console.Write("Invalid amount. Please enter a number from 1 to 255: ");
            columnsPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out columns);
        }
        return columns;
    }
    private static byte GetRowAmount()
    {
        Console.Write("Please Enter Row Amount( Default: 3 ): ");
        var rowPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out var rows);
        while (!(rows > 0 && rowPass))
        {
            Console.Write("Invalid amount. Please enter a number from 1 to 255: ");
            rowPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out rows);
        }
        return rows;
    }
    private static byte GetBonusColumnAmount(byte columns)
    {
        Console.Write("Please Enter How Many Columns Will Be Optional( Default: 1 ): ");
        var bonusColumnsPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out var bonus);

        while (!(bonus >= 0 && bonus <= columns && bonusColumnsPass))
        {
            Console.Write($"Invalid amount. Please enter a number from 0 to {columns}: ");
            bonusColumnsPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out bonus);
        }
        return bonus;
    }
    private static byte GetBaseSquareValue()
    {
        Console.Write("Please Enter The Starting Rows' Square Value( Default: 10 ): ");
        var baseSquarePass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out var value);

        while (!(value > 0 && baseSquarePass))
        {
            Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
            baseSquarePass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out value);
        }
        return value;
    }
    private static byte GetRowValueOffset()
    {
        Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row( Default: 20 ): ");
        var rowOffsetPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out var offset);

        while (!(offset >= 0 && rowOffsetPass))
        {
            Console.Write($"Invalid amount. Please enter a number from 0 to 255: ");
            rowOffsetPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out offset);
        }
        return offset;
    }
    private static byte GetBonusMultiplier()
    {
        Console.Write("Please Enter Score Multiplier For Optional Column/s( Default: 2 ): ");
        var bonusMultiplierPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out var multiplier);

        while (!(multiplier > 0 && bonusMultiplierPass))
        {
            Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
            bonusMultiplierPass = Byte.TryParse(Console.ReadLine()?.StringFormat(), out multiplier);
        }
        return multiplier;
    }
}
