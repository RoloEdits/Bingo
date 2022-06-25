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
            byte rowLabel = 0;
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
                fullTable += $"| **{RowLabel(rowLabel)}** |";
                // Iterates to next letter in alphabet for next rows label.
                rowLabel++;
                // Writes out each square.
                for (int currentIndex = absoluteIndex; currentIndex < endRowSquare; currentIndex++)
                {
                    // Writes out each square's value.
                    fullTable += $" {percentages[currentIndex]:P2} |";

                    absoluteIndex = currentIndex + 1;
                }
                endRowSquare += columns;
                // Goes to next row.
                fullTable += Environment.NewLine;
            }
            // Returns fully build table as a string.
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
            byte rowLabel = 0;
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
                fullTable += $"| **{RowLabel(rowLabel)}** |";
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
            // Returns fully build table as a string.
            return fullTable;
        }
    
        private static string RowLabel(byte totalCount)
        {
            // Full alphabet to use as a base.
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // Initiating the return label.
            string label = string.Empty;
            byte counter;
            // All of this effectivly blocks out a base-26 number system. Enough to cover the full 255 range of a byte.
            if (totalCount < alphabet.Length)
            {
                char singleChar = alphabet[totalCount];
                label = singleChar.ToString();
            }
            else if (totalCount < alphabet.Length * 2)
            {
                // From here on, counter will always be set to a range from 0-25
                counter = (byte)(totalCount - alphabet.Length);
                // A
                char singleCharA = alphabet[0];
                // A - Z
                char singleCharB = alphabet[counter];
                // Concatenate the two charracters and store in label for return. AA, AB, AC, etc.
                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 3)
            {
                counter = (byte)(totalCount - alphabet.Length * 2);
                // B
                char singleCharA = alphabet[1];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 4)
            {
                counter = (byte)(totalCount - alphabet.Length * 3);
                // C
                char singleCharA = alphabet[2];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 5)
            {
                counter = (byte)(totalCount - alphabet.Length * 4);
                // D
                char singleCharA = alphabet[3];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 6)
            {
                counter = (byte)(totalCount - alphabet.Length * 5);
                // E
                char singleCharA = alphabet[4];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 7)
            {
                counter = (byte)(totalCount - alphabet.Length * 6);
                // F
                char singleCharA = alphabet[5];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 8)
            {
                counter = (byte)(totalCount - alphabet.Length * 7);
                // G
                char singleCharA = alphabet[6];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 9)
            {
                counter = (byte)(totalCount - alphabet.Length * 8);
                // H
                char singleCharA = alphabet[7];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            else if (totalCount < alphabet.Length * 10)
            {
                counter = (byte)(totalCount - alphabet.Length * 9);
                // I
                char singleCharA = alphabet[8];
                char singleCharB = alphabet[counter];

                label = singleCharA + singleCharB.ToString();
            }
            return label;
        }
    }
}
