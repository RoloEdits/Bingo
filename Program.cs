using ClosedXML.Excel;

namespace tog_bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Prompt User for which settings to load
            Console.WriteLine("Load Default Values: Press 1\nEnter Custom Values: Press 2\n\n");
            Console.Write("Enter option: ");
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
            
            // Starting row for While loop.
            short currentRow = 1;
            //  While the current rows cell is not empty
            while (!workSheet.Cell(currentRow, 1).IsEmpty())
            {
                var nameData = workSheet.Cell(currentRow, 1).GetString();// Parrse column 1 for Name.
                var guessData = StringFormat(workSheet.Cell(currentRow, 2).GetString());//Parse Column 2 for Guess.

                char[] guessChars = guessData.ToCharArray(); //Turns player guess into char array.

                short columnElement = Settings.columns; // Holds the elemment position of each final column of that row.
                int currentElementHolder = 0; // Holds current absolute element postion.

                int score = 0; // Starting score
                short sqaureValue = Settings.baseSquareValue; // Resets the next rows base sqaure to Settings.Base.

                // Loops through rows.
                for (short rowCounter = 0; rowCounter < Settings.rows; rowCounter++)
                {
                    // currentElement is the progress of what sqaure is being worked on. This value is not relative to the row, but is in the context of all elements.
                    // columnElement is the current rows final column, setting up each row loop to end on that rows final column, fucntionally blocking out each rows length.
                    for (int currentElement = currentElementHolder; currentElement < columnElement; currentElement++)
                    {
                        // Checks if the sqaure being worked on lines up with the bonus sqaure of that row.
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
                    // Setting new base value for the next rows loop.
                    sqaureValue += Settings.rowValueOffset;
                    // Calculates the next rows final column in terms of absolute position.
                    columnElement += Settings.columns;
                }

                
                players.Add(new Player(nameData, guessData, score));//Add Current Player to List.
                players.Sort((lower, higher) => higher.Score.CompareTo(lower.Score));//Sort List.

                currentRow++;// Goes to next row in spreadsheet.
            }
            // After all players have been added, writes each out to file.
            using TextWriter writer = new StreamWriter(Settings.fileName);
            foreach (var player in players)
            {
                writer.WriteLine($"{player.Name} {player.Score}");
            }
            
            Console.Clear();
            Console.WriteLine($"Finished calulating {players.Count} Bingo guesses.\nYou can press any key to exit...");
            Console.ReadKey();

        }

        // Formats string inputs by removing spaces, new lines, and makes all charachters uppercase.
        static string StringFormat(string formatedString)
        {
            return formatedString.Replace(" ", "").Replace("\r\n", "").Replace("\n", "").ToUpper();
        }

        // Checks how to handle settings.
        static void SettingLoad(string selection)
        {
            // Selected Default Values.
            if (selection == "1")
            {
                Settings.columns = 4;
                Settings.rows = 3;
                Settings.bonusColumns = 1;
                Settings.rowValueOffset = 20;
                Settings.bonusMultiplier = 2;
                Settings.bonusSkipChar = '.';
                Settings.baseSquareValue = 10;

                Console.WriteLine("Default Settings Loaded");

                Console.Write("Please Enter Path: ");
                Settings.path = Console.ReadLine();
                Settings.fileName = Path.ChangeExtension(Settings.path, "md");

                Console.Write("Please Enter Key: ");
                Settings.key = StringFormat(Console.ReadLine());
            }
            // Selected Custom Values.
            else if (selection == "2")
            {
                Console.Write("Please Enter Path: ");
                Settings.path = Console.ReadLine();

                Settings.fileName = Path.ChangeExtension(Settings.path, "md");

                Console.Write("Please Enter Key: ");
                Settings.key = StringFormat(Console.ReadLine());

                Console.Write("Please Enter Column Amount(Default: 4): ");
                Settings.columns = Int16.Parse(StringFormat(Console.ReadLine()));

                Console.Write("Please Enter Row Amount(Default: 3): ");
                Settings.rows = Int16.Parse(StringFormat(Console.ReadLine()));

                Console.Write("Please Enter Bonus Column Amount(Default: 1): ");
                Settings.bonusColumns = Int16.Parse(StringFormat(Console.ReadLine()));

                Console.Write("Please Enter Offset Amount For Rows(Default: 20): ");
                Settings.rowValueOffset = Int16.Parse(StringFormat(Console.ReadLine()));

                Console.Write("Please Enter Multiplier For Bonus(Default: 2): ");
                Settings.bonusMultiplier = Int16.Parse(StringFormat(Console.ReadLine()));

                Settings.bonusSkipChar = '.';

                Console.Write("Please Enter Starting Sqaure(Default: 10): ");
                Settings.baseSquareValue = Int16.Parse(StringFormat(Console.ReadLine()));
            }
        }
    }
}
