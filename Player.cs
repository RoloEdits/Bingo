using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    internal class Player
    {
        public string Name { get; }
        public List<char> Guess { get; }
        public int Score { get; set; }
        public bool AllSame { get; }
        // Holds correct Guesses players have made.
        public static List<int> CorrectGuesses = new();

        public Player(string name, string guess, int score)
        {
            Name = name;
            Guess = guess.ToList();
            Score = score;
            // Checks if player guess is all the same.
            AllSame = Guess.All(check => check.Equals(Guess[0]));
        }
    }
}
