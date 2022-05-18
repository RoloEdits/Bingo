using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tog_bingo
{
    internal class Player
    {
        public string Name { get; set; }
        public string Guess { get; set; }
        public int Score { get; set; }

        public Player(string name, string guess, int score)
        {
            Name = name;
            Guess = guess;
            Score = score;
        }
    }
}
