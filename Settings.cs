namespace Tog.Bingo
{
    internal class Settings
    {
        public static string? path = String.Empty;
        public static string? key = String.Empty;
        public static short columns;
        public static short rows;
        public static short bonusColumns;
        public static short rowValueOffset;
        public static short bonusMultiplier;
        public static char bonusSkipChar;
        public static short baseSquareValue;
        public static string? fileName = String.Empty;

        // Checks how user wants to handle settings.
        public static void SettingLoad(string selection)
        {
            // If '1' Set These Default Values.
            if (selection == "1")
            {
                Settings.columns = 4;
                Settings.rows = 3;
                Settings.bonusColumns = 1;
                Settings.rowValueOffset = 20;
                Settings.bonusMultiplier = 2;
                Settings.bonusSkipChar = 'P'; // Which single character will be recognized as player having skipped the optional squares.
                Settings.baseSquareValue = 10;

                // ASCII Title Art
                Utilities.AsciiTitle();
                Console.WriteLine("Default Settings Loaded...");
                Console.WriteLine();
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.path = Console.ReadLine()?.Trim();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.fileName = Path.ChangeExtension(Settings.path, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.key = Utilities.StringFormat(Console.ReadLine());
            }
            // Asks user to input custom values.
            else if (selection == "2")
            {
                // ASCII Title Art
                Utilities.AsciiTitle();
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.path = Console.ReadLine()?.Trim();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.fileName = Path.ChangeExtension(Settings.path, "md");
                // The x-axis column amount
                Console.Write("Please Enter Column Amount(Default: 4): ");
                Settings.columns = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // The y-axis row amount
                Console.Write("Please Enter Row Amount(Default: 3): ");
                Settings.rows = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Of the given grid size, how many end columns are optional squares. The Purple Squares.
                Console.Write("Please Enter How Many Columns Will Be Optional(Default: 1): ");
                Settings.bonusColumns = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // The offset amount to shift each rows values from the previous row. Example: first row is a 10 value, entering 20 here means next row is now worth 30.
                Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row(Default: 20): ");
                Settings.rowValueOffset = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // How much the bonus squares are compared to that rows base square value. If a rows square value is 10, a 2 multiplier will mean that rows bonus squares are worth 20.
                Console.Write("Please Enter Score Multiplier For Optional Column/s(Default: 2): ");
                Settings.bonusMultiplier = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Which single character will be recognized as player having skipped the optional squares.
                Settings.bonusSkipChar = 'P';
                // This sets the value for the normal squares in the very first row.
                Console.Write("Please Enter The Starting Rows' Square Value(Default: 10): ");
                Settings.baseSquareValue = Int16.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.key = Utilities.StringFormat(Console.ReadLine());
            }
        }
    }


}
