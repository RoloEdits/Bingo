using ClosedXML.Excel;

namespace tog_bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Prompt User for which settings to load
            Console.WriteLine("Load Default Settings: 1\nEnter Custom Settings: 2\n\n");
            Console.Write("Enter Option: ");
            // Pass answer to function
            string Options = StringFormat(Console.ReadLine());
            Console.Clear();
            SettingLoad(Options);

            // Convert key to character array
            char[] keyChars = StringFormat(Settings.key).ToCharArray();
            // Make players list of type Player.
            var players = new List<Player>();

            // Import spreadsheet and look to the first Worksheet
            using var workbook = new XLWorkbook(Settings.path);
            var workSheet = workbook.Worksheet(1);

            // Starting spreadsheet row for While loop.
            short currentRow = 1;
            //  While the current rows cell is not empty
            while (!workSheet.Cell(currentRow, 1).IsEmpty())
            {
                var nameData = workSheet.Cell(currentRow, 1).GetString();// Parse Column 1 for Name.
                var guessData = StringFormat(workSheet.Cell(currentRow, 2).GetString());//Parse Column 2 for Guess.

                char[] guessChars = guessData.ToCharArray(); //Turns player guess into char array.

                short columnElement = Settings.columns; // Holds the current position of each final column sqaure of that row.
                int currentElementHolder = 0; // Holds current absolute element postion.

                int score = 0; // Starting score
                short sqaureValue = Settings.baseSquareValue; // Resets the value for next player loop.

                // Loops through rows.
                for (short rowCounter = 0; rowCounter < Settings.rows; rowCounter++)
                {
                    // currentElement is the progress of what sqaure is being worked on. This value is not relative to the row, but is in the context of all elements.
                    // columnElement is the current rows final column sqaure, setting up each row loop to end on that rows final sqaure, fucntionally blocking out each rows length.
                    for (int currentElement = currentElementHolder; currentElement < columnElement; currentElement++)
                    {
                        // Checks if the sqaure being worked on lines up with the bonus sqaures of that row.
                        if ((currentElement + Settings.bonusColumns) >= columnElement)
                            // Checks if the sqaure was skipped by checking if the skip charachter was used.
                            if (guessChars[currentElement] == Settings.bonusSkipChar)
                            {
                                score += 0; // If skipped, add 0 to score.
                            }
                            // Checks if the guess matches.
                            else if (guessChars[currentElement] == keyChars[currentElement])
                            {
                                // If it matches, it will take the base sqare value of the current row and multiply it by the bonus multiplier and add it to the score. e.g 10 * 2.
                                score += (sqaureValue * Settings.bonusMultiplier);
                            }
                            else
                            {
                                // If it doesnt match then it will subtract by the calculated amount.
                                score -= (sqaureValue * Settings.bonusMultiplier);
                            }
                        // Checks if the current guess matches with the key for that sqaure.
                        else if (guessChars[currentElement] == keyChars[currentElement])
                        {
                            // If matches adds current row value to score.
                            score += sqaureValue;
                        }
                        else
                        {
                            // If it doesn't match, it will subract the rows' value from score. 
                            score -= sqaureValue;
                        }

                        // This is to keep track of the current absolute element position and not have it reset to 0 in the next loop.
                        currentElementHolder = currentElement + 1;

                    }
                    // Sets new base sqaure value for the next row.
                    sqaureValue += Settings.rowValueOffset;
                    // Calculates the next rows final column in terms of absolute position.
                    columnElement += Settings.columns;
                }


                players.Add(new Player(nameData, guessData, score));//Add Current Player to List.
                players.Sort((lower, higher) => higher.Score.CompareTo(lower.Score));//Sort List so highest Score is on top.

                currentRow++;// Goes to next row in spreadsheet.
            }
            // After all players have been added, writes each out to file.
            using TextWriter writer = new StreamWriter(Settings.fileName);
            foreach (var player in players)
            {
                writer.WriteLine($"{player.Name} {player.Score}");
            }
            //Clears Console and writes to inform user of how many guesses have been checked and calulated.
            Console.Clear();
            Console.WriteLine($"Finished Checking Guesses, and Calculated Scores For {players.Count} Participants.\nYou can press any key to exit...");
            Console.ReadKey();// Pressing ends program and closes application.

        }

        // Formats string inputs by removing spaces, new lines,, returns, and makes all characters uppercase.
        static string StringFormat(string formatedString)
        {
            return formatedString.Replace(" ", "").Replace("\r\n", "").Replace("\n", "").ToUpper();
        }

        // Checks how user wants to handle settings.
        static void SettingLoad(string selection)
        {
            // If '1' Set These Default Values.
            if (selection == "1")
            {
                Settings.columns = 4;
                Settings.rows = 3;
                Settings.bonusColumns = 1;
                Settings.rowValueOffset = 20;
                Settings.bonusMultiplier = 2;
                Settings.bonusSkipChar = '.'; // Which single character will be recognized as player having skipped the optional sqaures.
                Settings.baseSquareValue = 10;

                Console.WriteLine("Default Settings Loaded");

                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.path = Console.ReadLine();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.fileName = Path.ChangeExtension(Settings.path, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.key = StringFormat(Console.ReadLine());
            }
            // Asks user to input custom values.
            else if (selection == "2")
            {
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.path = Console.ReadLine();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.fileName = Path.ChangeExtension(Settings.path, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.key = StringFormat(Console.ReadLine());
                // The x-axis column amount
                Console.Write("Please Enter Column Amount(Default: 4): ");
                Settings.columns = Int16.Parse(StringFormat(Console.ReadLine()));
                // The y-axis row amount
                Console.Write("Please Enter Row Amount(Default: 3): ");
                Settings.rows = Int16.Parse(StringFormat(Console.ReadLine()));
                // Of the given grid size, how many end columns are optional sqaures. The Purple Sqaures.
                Console.Write("Please Enter How Many Columns Will Be Optional(Default: 1): ");
                Settings.bonusColumns = Int16.Parse(StringFormat(Console.ReadLine()));
                // The offset amount to shift each rows values from the previous row. Example: first row is a 10 value, entering 20 here means next row is now worth 30.
                Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row(Default: 20): ");
                Settings.rowValueOffset = Int16.Parse(StringFormat(Console.ReadLine()));
                // How much the bonus squares are compared to that rows base sqaure value. If a rows sqaure value is 10, a 2 multiplier will mean that rows bonus sqaures are worth 20.
                Console.Write("Please Enter Multiplier For Optional Column/s(Default: 2): ");
                Settings.bonusMultiplier = Int16.Parse(StringFormat(Console.ReadLine()));
                // Which single character will be recognized as player having skipped the optional sqaures.
                Settings.bonusSkipChar = '.';
                // This sets the value for the normal sqaures in the very first row.
                Console.Write("Please Enter The Starting Rows Bingo Sqaure Value(Default: 10): ");
                Settings.baseSquareValue = Int16.Parse(StringFormat(Console.ReadLine()));
            }
        }
    }
}
