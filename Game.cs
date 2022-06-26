using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    internal class Game
    {
        // List of characters to hold the Key.
        public static List<char>? Key;
        public string PlayerName { get; set; }
        public short Row { get; set; }
        public int GuessLength { get; set; }

        public Game(string playerName, int guessLength, short row)
        {
            PlayerName = playerName;
            GuessLength = guessLength;
            Row = row;
        }
        public static int PlayerScore(List<char> guess, string name, bool allSame)
        {
            int score = 0;


            // Holds the current position of each final column square of that row.
            short finalColumn = Settings.Columns;
            // Holds current absolute element postion.
            int currentIndexHolder = 0;

            // Resets the value for next player loop.
            short squareValue = Settings.BaseSquareValue;
            // Loops through rows.
            for (short rowCounter = 0; rowCounter < Settings.Rows; rowCounter++)
            {
                // currentIndex is the progress of what square is being worked on. This value is not relative to the row, but is in the context of all elements.
                // finalColumn is the current rows final column square, setting up the end point of the loop.
                for (int currentIndex = currentIndexHolder; currentIndex < finalColumn; currentIndex++)
                {
                    // Checks if the square being worked on lines up with the bonus squares of that row.
                    if ((currentIndex + Settings.BonusColumns) >= finalColumn)
                    {
                        // Checks if the square was skipped by checking if the skip charachter was used.
                        if (guess[currentIndex] != Settings.BonusSkipChar)
                        {
                            // Checks if the guess matches.
                            if (guess[currentIndex] == Game.Key![currentIndex])
                            {
                                // If it matches, it will take the base sqare value of the current row and multiply it by the bonus multiplier and add it to the score. e.g 10 * 2.
                                score += (squareValue * Settings.BonusMultiplier);
                                // Checks if current player guessed the same answer for all the guess. If they did, exclude from stats.
                                if (!allSame)
                                {
                                    Player.CorrectGuesses.Add(currentIndex);
                                }
                            }
                            else
                            {
                                // If it doesnt match then it will subtract by the calculated amount.
                                score -= (squareValue * Settings.BonusMultiplier);
                            }
                        }
                    }
                    // Checks if the current guess matches with the key for that square.
                    else if (guess[currentIndex] == Game.Key![currentIndex])
                    {
                        // If matches adds current row value to score.
                        score += squareValue;
                        // Checks if current player guessed the same answer for all the guess. If they did, exclude from stats.
                        if (!allSame)
                        {
                            Player.CorrectGuesses.Add(currentIndex);
                        }
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

            // Returns final score for that player.
            return score;
        }
    
        public static List<Game> WrongAmountGuessedFor(List<Game> game)
        {
            // All correct guessers are removed leaving only those with an incorrect amount. 
            game.RemoveAll(x => x.GuessLength == (Settings.Columns * Settings.Rows));
            return game;
        }
    }
}
