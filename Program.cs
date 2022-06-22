using ClosedXML.Excel;
using System.Runtime.InteropServices;

namespace Bingo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creates timer.
            var watch = new System.Diagnostics.Stopwatch();

            // Set console name and text color.
            Console.Title = "Tower of God Bingo Solver";
            Console.ForegroundColor = ConsoleColor.Red;
            // Checks if tthe OS is windows. If it is, sets console window height.
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WindowHeight = 35;
            }

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
            string options = Utilities.StringFormat(Console.ReadLine());
            Settings.SettingsLoad(options);
            Console.Clear();

            // Starts timer.
            watch.Start();

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
                // Starting score.
                int score = 0;

                //Add Current Player to List.
                players.Add(new Player(nameData, guessData, score));

                // Goes to next row in spreadsheet.
                currentRow++;
            }
            // Goes through each player and calculates score.
            foreach (var player in players)
            {
                player.Score = Game.PlayerScore(player.Guess, player.Name, player.AllYes, player.AllNo);
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
                writer.Write(Table.PercentageTable(players.Count));
                writer.WriteLine();

                // Writes out table for players and their scores.
                writer.WriteLine("| Names | Scores |");
                writer.WriteLine("|---|---|");
                foreach (Player player in players)
                {
                    writer.WriteLine($"| {player.Name.Replace("|", "\\|")} | {player.Score} |");
                }
            }
            // Ends timer.
            watch.Stop();
            // Successful run of application
            Console.Clear();
            Utilities.AsciiTitle();
            Console.WriteLine($"Successfully Finished Going Through {players.Count} Players' Guesses in {watch.ElapsedMilliseconds}ms.");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}