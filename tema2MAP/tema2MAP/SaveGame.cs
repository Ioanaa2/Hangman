using System;
using System.Collections.Generic;

namespace tema2MAP
{
    public class SavedGame
    {
        public string PlayerName { get; set; }
        public string Category { get; set; }

        public int Level { get; set; }
        public int Streak { get; set; }
        public int WrongGuesses { get; set; }
        public int TimeLeft { get; set; }

        public string Word { get; set; }
        public string DisplayWord { get; set; }

        public List<char> UsedLetters { get; set; }
    }
}