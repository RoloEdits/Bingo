using ClosedXML.Excel;

namespace Bingo
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
            Settings.SettingsLoad(Options);

            // Checks if the key is large enough to answer for the bingo.
            if (Settings.Key.Length < (Settings.Columns * Settings.Rows))
            {
                Console.Clear();
                Utilities.AsciiTitle();
                Console.WriteLine($"Error Occured: You input an answer for only {Settings.Key.Length} total squares, must have enough for {Settings.Columns * Settings.Rows} total squares.");
                Console.Write("Press any key to exit...");
                Console.ReadKey();
                Console.ResetColor();
                Environment.Exit(1);
            }

            // Make players list of type Player.
            var players = new List<Player>();

            // Import spreadsheet and look to the first Worksheet.
            using var workbook = new XLWorkbook(Settings.Path);
            var workSheet = workbook.Worksheet(1);

            // Starting spreadsheet row for While loop.
            short currentRow = 1;
            //  While the current rows cell is not empty
            while (!workSheet.Cell(currentRow, 1).IsEmpty())
            {
                // Parse Column 1 for Name.
                string nameData = workSheet.Cell(currentRow, 1).GetString();
                //Parse Column 2 for Guess.
                string guessData = Utilities.StringFormat(workSheet.Cell(currentRow, 2).GetString());

                //Turns player guess into char array.
                char[] guessChars = guessData.ToCharArray();

                // Checks if current player has enough squares guessed. 
                if (guessChars.Length >= (Settings.Columns * Settings.Rows))
                {
                    // Holds the current position of each final column square of that row.
                    short finalColumn = Settings.Columns;
                    // Holds current absolute element postion.
                    int currentIndexHolder = 0;

                    // Starting score.
                    int score = 0;

                    // Resets the value for next player loop.
                    short squareValue = Settings.BaseSquareValue;
                    // Loops through rows.
                    for (short rowCounter = 0; rowCounter < Settings.Rows; rowCounter++)
                    {
                        // currentElement is the progress of what square is being worked on. This value is not relative to the row, but is in the context of all elements.
                        // columnElement is the current rows final column square, setting up each row loop to end on that rows final square, fucntionally blocking out each rows length.
                        for (int currentIndex = currentIndexHolder; currentIndex < finalColumn; currentIndex++)
                        {
                            // Checks if the square being worked on lines up with the bonus squares of that row.
                            if ((currentIndex + Settings.BonusColumns) >= finalColumn)
                            {
                                // Checks if the square was skipped by checking if the skip charachter was used.
                                if (guessChars[currentIndex] != Settings.BonusSkipChar)
                                {
                                    // Checks if the guess matches.
                                    if (guessChars[currentIndex] == Settings.Key[currentIndex])
                                    {
                                        // If it matches, it will take the base sqare value of the current row and multiply it by the bonus multiplier and add it to the score. e.g 10 * 2.
                                        score += (squareValue * Settings.BonusMultiplier);
                                        Player.CorrectGuesses.Add(currentIndex);
                                    }
                                    else
                                    {
                                        // If it doesnt match then it will subtract by the calculated amount.
                                        score -= (squareValue * Settings.BonusMultiplier);
                                    }
                                }
                            }

                            // Checks if the current guess matches with the key for that square.
                            else if (guessChars[currentIndex] == Settings.Key[currentIndex])
                            {
                                // If matches adds current row value to score.
                                score += squareValue;
                                Player.CorrectGuesses.Add(currentIndex);
                            }
                            else
                            {
                                // If it doesn't match, it will subract the rows' value from score. 
                                score -= squareValue;
                            }

                            // This is to keep track of the current absolute element position and not have it reset to 0 in the next loop.
                            currentIndexHolder = currentIndex + 1;
                        }
                        // Sets new base square value for the next row.
                        squareValue += Settings.RowValueOffset;
                        // Calculates the next rows final column in terms of absolute position.
                        finalColumn += Settings.Columns;
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
                    Console.WriteLine($"Error Occured: On Row {currentRow} '{nameData}' has guessed for only {guessChars.Length} squares, needs to be for {Settings.Columns * Settings.Rows} squares.");
                    Console.Write("Press any key to exit...");
                    Console.ReadKey();
                    Console.ResetColor();
                    Environment.Exit(1);
                }
            }

            // After all players have been added, writes each out to file.
            using (TextWriter writer = new StreamWriter(Settings.FileName!))
            {
                //Sort List so highest Score is on top.
                players.Sort((lower, higher) => higher.Score.CompareTo(lower.Score));
                // Writes out the correct answer key to a table.
                writer.Write(Table.AnswerTable());
                writer.WriteLine();

                // Writes out table for the amount of correct guesses per sqaure.
                writer.Write(Table.PercentageTable());
                writer.WriteLine();

                // Writes out table for players and their scores.
                writer.WriteLine("| Names | Scores |");
                writer.WriteLine("|---|---|");
                foreach (Player player in players)
                {
                    writer.WriteLine($"| {player.Name.Replace("|", "\\|")} | {player.Score} |");
                }
            }
            // Successful run of application
            Console.Clear();
            Utilities.AsciiTitle();
            Console.WriteLine($"Successfully Finished Going Through {Player.PlayerCount} Players' Guesses.");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}