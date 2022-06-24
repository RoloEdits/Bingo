using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bingo
{
    internal class Settings
    {
        public static string? filePath;
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
            // Checks that user input either 1 or 2, and only one of those two options. Will reprompt if not 1 or 2.
            while (selection != "1" && selection != "2")
            {
                Console.Write($"You entered {selection}, must be either 1 or 2:");

                selection = Utilities.StringFormat(Console.ReadLine());
            }
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
                Console.Clear();
                Utilities.AsciiTitle();
                Console.WriteLine("Default Settings Loaded...");
                Console.WriteLine();
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.filePath = Console.ReadLine()?.Trim();
                // Checks if file exists. If it doesn't, it will reprompt until you give a valid file. Checks if filetype is valid.
                while (!File.Exists(Settings.filePath) || Path.GetExtension(Settings.filePath) != ".xlsx")
                {
                    // If file doesnt exist, promt that it doesnt and ask again.
                    if (!File.Exists(Settings.filePath))
                    {
                        Console.Write("File does not exist. Please enter correct file path: ");
                        Settings.filePath = Console.ReadLine()?.Trim();
                    }
                    // If file does exist, but isnt correct type, prompt that filetype is invlaid.
                    else if (Path.GetExtension(Settings.filePath) != ".xlsx")
                    {
                        Console.Write("Invalid filetype. Please use a filetype of '.xlsx': ");
                        Settings.filePath = Console.ReadLine()?.Trim();
                    }
                }
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.FileName = System.IO.Path.ChangeExtension(Settings.filePath, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Game.Key = Utilities.StringFormat(Console.ReadLine()).ToList();
                // Checks if the amount entered is 12, and that it has only Y and N inputs. 
                while ( Game.Key.Count != 12 || Game.Key.Any(key => key != 'Y' && key != 'N')  )
                {
                    Console.Write("Invalid key. Make sure you enter for 12 squares, and only Y or N: ");
                    Game.Key = Utilities.StringFormat(Console.ReadLine()).ToList();
                }
            }
            // Asks user to input custom values.
            else if (selection == "2")
            {
                // ASCII Title Art
                Console.Clear();
                Utilities.AsciiTitle();

                // The x-axis column amount
                Console.Write("Please Enter Column Amount( Default: 4 ): ");
                Settings.Columns = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Checks if the amount entered is able to contruct a valid grid with at least 1 column.
                while (!(Settings.Columns > 0 && Settings.Columns < 256))
                {
                    Console.Write("Invalid amount. Please enter a number from 1 to 255: ");
                    Settings.Columns = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                }
                // The y-axis row amount
                Console.Write("Please Enter Row Amount( Default: 3 ): ");
                Settings.Rows = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Checks if the amount entered is able to contruct a valid grid with at least 1 row.
                while (!(Settings.Rows > 0 && Settings.Rows < 27))
                {
                    Console.Write("Invalid amount. Please enter a number from 1 to 26: ");
                    Settings.Rows = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                }
                // Of the given grid size, how many end columns are optional squares. The Purple Squares.
                Console.Write("Please Enter How Many Columns Will Be Optional( Default: 1 ): ");
                Settings.BonusColumns = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Checks that amount entered is at least 0, and not larger than the amount of columns in the grid.
                while (!(Settings.BonusColumns >= 0 && Settings.BonusColumns <= Settings.Columns))
                {
                    Console.Write($"Invalid amount. Please enter a number from 0 to {Settings.Columns}: ");
                    Settings.BonusColumns = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                }
                // This sets the value for the normal squares in the very first row.
                Console.Write("Please Enter The Starting Rows' Square Value( Default: 10 ): ");
                Settings.BaseSquareValue = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Checks that amount entered is at least 1, and lower than 256.
                while (!(Settings.BaseSquareValue > 0 && Settings.BaseSquareValue < 256))
                {
                    Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
                    Settings.BaseSquareValue = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                }
                // The offset amount to shift each rows values from the previous row. Example: first row is a 10 value, entering 20 here means next row is now worth 30.
                Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row( Default: 20 ): ");
                Settings.RowValueOffset = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Checks that the value to go up by is at least 0, and lower than 256. If you enter 0 all rows would be worth the same amount.
                while (!(Settings.RowValueOffset >= 0 && Settings.RowValueOffset < 256))
                {
                    Console.Write($"Invalid amount. Please enter a number from 0 to 255: ");
                    Settings.RowValueOffset = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                }
                // How much the bonus squares are compared to that rows base square value. If a rows square value is 10, a 2 multiplier will mean that rows bonus squares are worth 20.
                Console.Write("Please Enter Score Multiplier For Optional Column/s( Default: 2 ): ");
                Settings.BonusMultiplier = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                // Checks that the amounnt entered is at least 1, and less than 256. If you enter 1, bonus squares would be worth the same as the other squares of that row.
                while(!(Settings.BonusMultiplier > 0 && Settings.BonusMultiplier < 256))
                {
                    Console.Write($"Invalid amount. Please enter a number from 1 to 255: ");
                    Settings.BonusMultiplier = Byte.Parse(Utilities.StringFormat(Console.ReadLine()));
                }
                // Which single character will be recognized as player having skipped the optional squares.
                Settings.BonusSkipChar = 'P';

                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.filePath = Console.ReadLine()?.Trim();
                // Checks if file exists. If it doesn't, it will reprompt until you give a valid file. Checks if filetype is valid.
                while (!File.Exists(Settings.filePath) || Path.GetExtension(Settings.filePath) != ".xlsx")
                {
                    // If file doesnt exist, promt that it doesnt and ask again.
                    if (!File.Exists(Settings.filePath))
                    {
                        Console.Write("File does not exist. Please enter correct file path: ");
                        Settings.filePath = Console.ReadLine()?.Trim();
                    }
                    // If file does exist, but isnt correct type, prompt that filetype is invlaid.
                    else if (Path.GetExtension(Settings.filePath) != ".xlsx")
                    {
                        Console.Write("Invalid filetype. Please use a filetype of '.xlsx': ");
                        Settings.filePath = Console.ReadLine()?.Trim();
                    }
                }
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.FileName = System.IO.Path.ChangeExtension(Settings.filePath, "md");

                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Game.Key = Utilities.StringFormat(Console.ReadLine()).ToList();
                // Checks if the amount entered is the same as the total amount of squares, and that it has only Y and N inputs. 
                while ( Game.Key.Count != (Settings.Rows * Settings.Columns) || Game.Key.Any(key => key != 'Y' && key != 'N') )
                {
                    Console.Write($"Invalid key. Make sure you enter for {Settings.Rows * Settings.Columns} squares, and only Y or N: ");
                    Game.Key = Utilities.StringFormat(Console.ReadLine()).ToList();
                }
            }
        }
    }
}
