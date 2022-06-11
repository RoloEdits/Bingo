using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    internal class Table
    {
        public static string PercentageTable(int playerCount)
        {
            // Holder for table construct.
            string fullTable = "";
            var columns = Settings.Columns;
            var rows = Settings.Rows;
            var bonus = Settings.BonusColumns;
            var gridColumns = columns + 1;
            var bonusColumns = columns - bonus;
            var squaresAmount = columns * rows;

            // Holds the amount of times an sqaure was chosen as correct.
            var finalCount = new List<int>();
            // Holds final percentages.
            var percentages = new List<double>();

            // Counts how many correct guesses there were for each sqaure.
            for (int i = 0; i < squaresAmount; i++)
            {
                var summedCount = Player.CorrectGuesses.Count(square => square == i);
                finalCount.Add(summedCount);
            }
            // Calculates the percentages for each sqaure.
            foreach (var sqaure in finalCount)
            {
                double percentage = ((double)sqaure / playerCount);
                percentages.Add(percentage);
            }
            // Sets first row label to 'A'.
            char rowLabel = 'A';
            int absoluteIndex = 0;
            int endRowSquare = columns;

            // Writes out Header row.
            for (int headerColumn = 0; headerColumn < gridColumns; headerColumn++)
            {
                if (headerColumn == 0)
                {
                    fullTable += "| Stats |";
                }
                else if (headerColumn >= bonusColumns + 1)
                {
                    fullTable += " Bonus |";
                }
                else
                {
                    fullTable += $" {headerColumn} |";
                }
            }
            //Goes to next line.
            fullTable += Environment.NewLine;
            // Writes out dividing row.
            for (int i = 0; i < gridColumns; i++)
            {
                fullTable += $" :---: |";
            }
            // Goes to next line.
            fullTable += Environment.NewLine;

            // Writes out data.
            for (int row = 0; row < rows; row++)
            {
                // Writes out row label.
                fullTable += $"| **{rowLabel}** |";
                // Iterates to next letter in alphabet for next rows label.
                rowLabel++;
                // Writes out each square.
                for (int currentIndex = absoluteIndex; currentIndex < endRowSquare; currentIndex++)
                {
                    // Writes out each square's value.
                    fullTable += $" {percentages[currentIndex].ToString("P2")} |";

                    absoluteIndex = currentIndex + 1;
                }
                endRowSquare += columns;
                // Goes to next row.
                fullTable += Environment.NewLine;
            }
            return fullTable;
        }

        public static string AnswerTable()
        {
            // Holder for table construct.
            string fullTable = "";
            var key = Game.Key;
            var columns = Settings.Columns;
            var rows = Settings.Rows;
            var bonus = Settings.BonusColumns;
            var gridColumns = columns + 1;
            var bonusColumns = columns - bonus;

            // Sets first row label to 'A'.
            char rowLabel = 'A';
            int absoluteIndex = 0;
            int endRowSquare = columns;

            // Writes out Header row.
            for (int headerColumn = 0; headerColumn < gridColumns; headerColumn++)
            {
                if (headerColumn == 0)
                {
                    fullTable += "| Key |";
                }
                else if (headerColumn >= bonusColumns + 1)
                {
                    fullTable += " Bonus |";
                }
                else
                {
                    fullTable += $" {headerColumn} |";
                }
            }
            //Goes to next line.
            fullTable += Environment.NewLine;
            // Writes out dividing row.
            for (int i = 0; i < gridColumns; i++)
            {
                fullTable += $" :---: |";
            }
            // Goes to next line.
            fullTable += Environment.NewLine;

            // Writes out data.
            for (int row = 0; row < rows; row++)
            {
                // Writes out row label.
                fullTable += $"| **{rowLabel}** |";
                // Iterates to next letter in alphabet for next rows label.
                rowLabel++;
                // Writes out each square.
                for (int currentIndex = absoluteIndex; currentIndex < endRowSquare; currentIndex++)
                {
                    // Writes out each square's value.
                    fullTable += $"  {key![currentIndex]}  |";

                    absoluteIndex = currentIndex + 1;
                }
                endRowSquare += columns;
                // Goes to next row.
                fullTable += Environment.NewLine;
            }
            return fullTable;
        }
    }
}
