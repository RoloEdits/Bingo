using ClosedXML.Excel;

namespace Tog.Bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console name and text color.
            Console.Title = "Tower of God Bingo Solver";
            Console.ForegroundColor = ConsoleColor.Red;
            // ASCII Title Art.
            Utilities.AsciiTitle();
            // Prompt User for which settings to load.
            Console.WriteLine("Load Default Settings: 1");
            Console.WriteLine("Enter Custom Settings: 2");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Enter Option: ");
            // Pass answer to function.
            string Options = Utilities.StringFormat(Console.ReadLine());
            Console.Clear();
            Settings.SettingLoad(Options);

            // Convert key to character array.
            char[] keyChars = Utilities.StringFormat(Settings.key).ToCharArray();
            // Make players list of type Player.
            var players = new List<Player>();
            var logCorrectSquares = new List<int>();
            // Checks if the key is large enough to answer for the bingo.
            if (keyChars.Length < (Settings.columns * Settings.rows))
            {
                Console.Clear();
                Utilities.AsciiTitle();
                Console.WriteLine($"Error Occured: You input an answer for only {keyChars.Length} total squares, must have enough for {Settings.columns * Settings.rows} total squares.");
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.ResetColor();
                Environment.Exit(1);
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
                var guessData = Utilities.StringFormat(workSheet.Cell(currentRow, 2).GetString());

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
                            {
                                // Checks if the square was skipped by checking if the skip charachter was used.
                                if (guessChars[currentElement] != Settings.bonusSkipChar)
                                {
                                    // Checks if the guess matches.
                                    if (guessChars[currentElement] == keyChars[currentElement])
                                    {
                                        // If it matches, it will take the base sqare value of the current row and multiply it by the bonus multiplier and add it to the score. e.g 10 * 2.
                                        score += (squareValue * Settings.bonusMultiplier);
                                        logCorrectSquares.Add(currentElement);
                                    }
                                    else
                                    {
                                        // If it doesnt match then it will subtract by the calculated amount.
                                        score -= (squareValue * Settings.bonusMultiplier);
                                    }

                                }
                                
                            }

                            // Checks if the current guess matches with the key for that square.
                            else if (guessChars[currentElement] == keyChars[currentElement])
                            {
                                // If matches adds current row value to score.
                                score += squareValue;
                                logCorrectSquares.Add(currentElement);
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
                    
                    // Goes to next row in spreadsheet.
                    currentRow++;
                }
                else
                {
                    // If they dont have enough squares guessed, the program will end and inform user of where the culprit is in the spreadsheet.
                    Console.Clear();
                    Utilities.AsciiTitle();
                    Console.WriteLine($"Error Occured: On Row {currentRow} '{nameData}' has guessed for only {guessChars.Length} squares, needs to be for {Settings.columns * Settings.rows} squares.");
                    Console.Write("Press any key to exit...");
                    Console.ReadKey();
                    Console.ResetColor();
                    Environment.Exit(1);
                }
            }

            // After all players have been added, writes each out to file.
            using (TextWriter writer = new StreamWriter(Settings.fileName!))
            {
                //Sort List so highest Score is on top.
                players.Sort((lower, higher) => higher.Score.CompareTo(lower.Score));

                writer.WriteLine("| Names | Scores |");
                writer.WriteLine("|---|---|");
                foreach (var player in players)
                {
                    writer.WriteLine($"| {player.Name.Replace("|", "\\|")} | {player.Score} |");
                }
                writer.WriteLine("---");

                var playerCount = players.Count;
                string table = Table.Write(logCorrectSquares, Settings.rows, Settings.columns, Settings.bonusColumns, playerCount);

                writer.Write(table);
                
            }

            // Successful run of application
            Console.Clear();
            Utilities.AsciiTitle();
            Console.WriteLine($"Successfully Finished Going Through {players.Count} Players' Guesses.");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}
