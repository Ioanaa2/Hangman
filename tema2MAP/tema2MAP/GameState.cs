using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema2MAP
{
    public class GameState
    {
        public string PlayerName { get; set; } = "";
        public string SelectedCategory { get; set; } = "";
        public string Word { get; set; } = "";
        public List<string> UsedLetters { get; set; } = new();
        public int WrongGuesses { get; set; }
        public int TimeLeft { get; set; }
        public int Level { get; set; }
        public int Streak { get; set; }
    }
}
