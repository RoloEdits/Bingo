namespace Tog.Bingo
{
    internal class Settings
    {
        public static string? Path;
        public static char[]? Key;
        public static short Columns;
        public static short Rows;
        public static short BonusColumns;
        public static short RowValueOffset;
        public static short BonusMultiplier;
        public static char BonusSkipChar;
        public static short BaseSquareValue;
        public static string? FileName = String.Empty;

        // Checks how user wants to handle settings.
        public static void SettingsLoad(string selection)
        {
            // If '1' Set These Default Values.
            if (selection == "1")
            {
                Settings.Columns = 4;
                Settings.Rows = 3;
                Settings.BonusColumns = 1;
                Settings.RowValueOffset = 20;
                Settings.BonusMultiplier = 2;
                Settings.BonusSkipChar = 'P'; // Which single character will be recognized as player having skipped the optional squares.
                Settings.BaseSquareValue = 10;

                // ASCII Title Art
                Utilities.AsciiTitle();
                Console.WriteLine("Default Settings Loaded...");
                Console.WriteLine();
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.Path = Console.ReadLine()?.Trim();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.FileName = System.IO.Path.ChangeExtension(Settings.Path, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.Key = Utilities.StringFormat(Console.ReadLine()).ToCharArray();
            }
            // Asks user to input custom values.
            else if (selection == "2")
            {
                // ASCII Title Art
                Utilities.AsciiTitle();
                // The x-axis column amount
                Console.Write("Please Enter Column Amount(Default: 4): ");
                Settings.Columns = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // The y-axis row amount
                Console.Write("Please Enter Row Amount(Default: 3): ");
                Settings.Rows = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Of the given grid size, how many end columns are optional squares. The Purple Squares.
                Console.Write("Please Enter How Many Columns Will Be Optional(Default: 1): ");
                Settings.BonusColumns = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // The offset amount to shift each rows values from the previous row. Example: first row is a 10 value, entering 20 here means next row is now worth 30.
                Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row(Default: 20): ");
                Settings.RowValueOffset = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // How much the bonus squares are compared to that rows base square value. If a rows square value is 10, a 2 multiplier will mean that rows bonus squares are worth 20.
                Console.Write("Please Enter Score Multiplier For Optional Column/s(Default: 2): ");
                Settings.BonusMultiplier = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Which single character will be recognized as player having skipped the optional squares.
                Settings.BonusSkipChar = 'P';
                // This sets the value for the normal squares in the very first row.
                Console.Write("Please Enter The Starting Rows' Square Value(Default: 10): ");
                Settings.BaseSquareValue = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.Path = Console.ReadLine()?.Trim();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.FileName = System.IO.Path.ChangeExtension(Settings.Path, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.Key = Utilities.StringFormat(Console.ReadLine()).ToCharArray();
            }
            // If its not 1 or 2 prompt user and exit program.
            else
            {
                Console.Clear();
                Utilities.AsciiTitle();
                Console.WriteLine($"Invalid entry. Must enter either a 1 or 2.");
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.ResetColor();
                Environment.Exit(1);
            }
        }
    }
}
