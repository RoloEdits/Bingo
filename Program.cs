using ClosedXML.Excel;

namespace tog_bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console name and text color.
            Console.Title = "Tower of God Bingo Solver";
            Console.ForegroundColor = ConsoleColor.Red;
            // ASCII Title Art.
            AsciiTitle();
            // Prompt User for which settings to load.
            Console.WriteLine("Load Default Settings: 1\nEnter Custom Settings: 2\n\n\n\n");
            Console.Write("Enter Option: ");
            // Pass answer to function.
            string Options = StringFormat(Console.ReadLine());
            Console.Clear();
            SettingLoad(Options);

            // Convert key to character array.
            char[] keyChars = StringFormat(Settings.key).ToCharArray();
            // Make players list of type Player.
            var players = new List<Player>();

            // Checks if the key is large enough to answer for the bingo.
            if (keyChars.Length < (Settings.columns * Settings.rows))
            {
                Console.Clear();
                AsciiTitle();
                Console.WriteLine($"Error Occured: You input an answer for only {keyChars.Length} total squares, must have enough for {Settings.columns * Settings.rows} total squares.");
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.ResetColor();
                Environment.Exit(0);
            }

            // Import spreadsheet and look to the first Worksheet.
            using var workbook = new XLWorkbook(Settings.path);
            var workSheet = workbook.Worksheet(1);

            // Starting spreadsheet row for While loop.
            short currentRow = 1;
            //  While the current rows cell is not empty
            while (!workSheet.Cell(currentRow, 1).IsEmpty())
            {
                // Parse Column 1 for Name.
                var nameData = workSheet.Cell(currentRow, 1).GetString();
                //Parse Column 2 for Guess.
                var guessData = StringFormat(workSheet.Cell(currentRow, 2).GetString());

                //Turns player guess into char array.
                char[] guessChars = guessData.ToCharArray();

                // Holds the current position of each final column square of that row.
                short columnElement = Settings.columns;
                // Holds current absolute element postion.
                int currentElementHolder = 0;

                // Starting score.
                int score = 0;
                // Resets the value for next player loop.
                short squareValue = Settings.baseSquareValue;

                // Checks if current player has enough squares gussed. 
                if (guessChars.Length >= (Settings.columns * Settings.rows))
                {
                    // Loops through rows.
                    for (short rowCounter = 0; rowCounter < Settings.rows; rowCounter++)
                    {
                        // currentElement is the progress of what square is being worked on. This value is not relative to the row, but is in the context of all elements.
                        // columnElement is the current rows final column square, setting up each row loop to end on that rows final square, fucntionally blocking out each rows length.
                        for (int currentElement = currentElementHolder; currentElement < columnElement; currentElement++)
                        {
                            // Checks if the square being worked on lines up with the bonus squares of that row.
                            if ((currentElement + Settings.bonusColumns) >= columnElement)
                                // Checks if the square was skipped by checking if the skip charachter was used.
                                if (guessChars[currentElement] == Settings.bonusSkipChar)
                                {
                                    // If skipped, add 0 to score.
                                    score += 0;
                                }
                                // Checks if the guess matches.
                                else if (guessChars[currentElement] == keyChars[currentElement])
                                {
                                    // If it matches, it will take the base sqare value of the current row and multiply it by the bonus multiplier and add it to the score. e.g 10 * 2.
                                    score += (squareValue * Settings.bonusMultiplier);
                                }
                                else
                                {
                                    // If it doesnt match then it will subtract by the calculated amount.
                                    score -= (squareValue * Settings.bonusMultiplier);
                                }
                            // Checks if the current guess matches with the key for that square.
                            else if (guessChars[currentElement] == keyChars[currentElement])
                            {
                                // If matches adds current row value to score.
                                score += squareValue;
                            }
                            else
                            {
                                // If it doesn't match, it will subract the rows' value from score. 
                                score -= squareValue;
                            }

                            // This is to keep track of the current absolute element position and not have it reset to 0 in the next loop.
                            currentElementHolder = currentElement + 1;

                        }
                        // Sets new base square value for the next row.
                        squareValue += Settings.rowValueOffset;
                        // Calculates the next rows final column in terms of absolute position.
                        columnElement += Settings.columns;
                    }

                    //Add Current Player to List.
                    players.Add(new Player(nameData, guessData, score));
                    //Sort List so highest Score is on top.
                    players.Sort((lower, higher) => higher.Score.CompareTo(lower.Score));

                    // Goes to next row in spreadsheet.
                    currentRow++;

                    
                }
                else
                {
                    // If they dont have enough squares guessed, the program will end and inform user of where the culprit is in the spreadsheet.
                    Console.Clear();
                    AsciiTitle();
                    Console.WriteLine($"Error Occured: On Row {currentRow} '{nameData}' has guessed for only {guessChars.Length} squares, needs to be for {Settings.columns * Settings.rows} squares.");
                    Console.Write("Press any key to exit...");
                    Console.ReadKey();
                    Console.ResetColor();
                    Environment.Exit(0);
                }
            }

            // After all players have been added, writes each out to file.
            using TextWriter writer = new StreamWriter(Settings.fileName);
            foreach (var player in players)
            {
                writer.WriteLine($"{player.Name} {player.Score}");
            }

            // Successful run of application
            Console.Clear();
            AsciiTitle();
            Console.WriteLine($"Successfully Finished Going Through {players.Count} Players' Guesses.");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine("\n");
            Console.ResetColor();
        }

        // Formats string inputs by removing spaces, new lines,, returns, and makes all characters uppercase.
        static string StringFormat(string? formatedString)
        {
            // Redundant NULL input handling to allow graceful exit.
            if (formatedString == null)
            {
                Console.Clear();
                AsciiTitle();
                Console.WriteLine("Error: Invalid Input. Press any key to exit program...");
                Console.ReadKey();
                Console.ResetColor();
                Environment.Exit(0);
            }
            return formatedString = formatedString.Replace(" ", "").Replace("\r\n", "").Replace("\n", "").ToUpper();
        }

        // Holder for the Ascii Art Title
        static void AsciiTitle()
        {
            Console.WriteLine(@"    ___       ___       ___       ___       ___            ___       ___            ___       ___       ___   
   /\  \     /\  \     /\__\     /\  \     /\  \          /\  \     /\  \          /\  \     /\  \     /\  \  
   \:\  \   /::\  \   /:/\__\   /::\  \   /::\  \        /::\  \   /::\  \        /::\  \   /::\  \   /::\  \ 
   /::\__\ /:/\:\__\ /:/:/\__\ /::\:\__\ /::\:\__\      /:/\:\__\ /::\:\__\      /:/\:\__\ /:/\:\__\ /:/\:\__\
  /:/\/__/ \:\/:/  / \::/:/  / \:\:\/  / \;:::/  /      \:\/:/  / \/\:\/__/      \:\:\/__/ \:\/:/  / \:\/:/  /
  \/__/     \::/  /   \::/  /   \:\/  /   |:\/__/        \::/  /     \/__/        \::/  /   \::/  /   \::/  / 
             \/__/     \/__/     \/__/     \|__|          \/__/                    \/__/     \/__/     \/__/  
                                  ___       ___       ___       ___       ___                      
                                 /\  \     /\  \     /\__\     /\  \     /\  \ 
                                /::\  \   _\:\  \   /:| _|_   /::\  \   /::\  \ 
                               /::\:\__\ /\/::\__\ /::|/\__\ /:/\:\__\ /:/\:\__\ 
                               \:\::/  / \::/\/__/ \/|::/  / \:\:\/__/ \:\/:/  /
                                \::/  /   \:\__\     |:/  /   \::/  /   \::/  /
                                 \/__/     \/__/     \/__/     \/__/     \/__/");
            Console.WriteLine("\n\n\n\n");
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
                Settings.bonusSkipChar = 'P'; // Which single character will be recognized as player having skipped the optional squares.
                Settings.baseSquareValue = 10;

                // ASCII Title Art
                AsciiTitle();
                Console.WriteLine("Default Settings Loaded...\n");
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.path = Console.ReadLine().Trim();
                // Sets file output name to be in the same place and have the same name as the entered path, but changes extension. md is for Markdown.
                Settings.fileName = Path.ChangeExtension(Settings.path, "md");
                // Enter known correct answer key
                Console.Write("Please Enter Answer Key: ");
                Settings.key = StringFormat(Console.ReadLine());
            }
            // Asks user to input custom values.
            else if (selection == "2")
            {
                // ASCII Title Art
                AsciiTitle();
                // Ask user for absolute file path.
                Console.Write("Please Enter File Path: ");
                Settings.path = Console.ReadLine().Trim();
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
                // Of the given grid size, how many end columns are optional squares. The Purple Squares.
                Console.Write("Please Enter How Many Columns Will Be Optional(Default: 1): ");
                Settings.bonusColumns = Int16.Parse(StringFormat(Console.ReadLine()));
                // The offset amount to shift each rows values from the previous row. Example: first row is a 10 value, entering 20 here means next row is now worth 30.
                Console.Write("Please Enter How Much The Next Row Will Go Up In Value From Current Row(Default: 20): ");
                Settings.rowValueOffset = Int16.Parse(StringFormat(Console.ReadLine()));
                // How much the bonus squares are compared to that rows base square value. If a rows square value is 10, a 2 multiplier will mean that rows bonus squares are worth 20.
                Console.Write("Please Enter Score Multiplier For Optional Column/s(Default: 2): ");
                Settings.bonusMultiplier = Int16.Parse(StringFormat(Console.ReadLine()));
                // Which single character will be recognized as player having skipped the optional squares.
                Settings.bonusSkipChar = 'P';
                // This sets the value for the normal squares in the very first row.
                Console.Write("Please Enter The Starting Rows' Square Value(Default: 10): ");
                Settings.baseSquareValue = Int16.Parse(StringFormat(Console.ReadLine()));
            }
        }
    }
}
