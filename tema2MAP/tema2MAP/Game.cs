using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema2MAP
{
    internal class Game
    {
        private bool _firstRound { get; set; }
        private bool _secondRound { get; set; }
        private bool _thirdRound { get; set; }

        public static string[] listaCuvinte = { "MASINA", "TELEFON", "AVION" };
        private string? _word { get; set; }
        private int _roundsWon { get; set; }

        public Game() {}
        public Game(string word)
        {
            _firstRound = false;
            _secondRound = false;
            _thirdRound = false;
            _roundsWon = 0;
            _word = word;
        }
        public bool isWon()
        {
            if (_roundsWon == 3 && _firstRound && _secondRound && _thirdRound)
                return true;
            return false;
        }



    }
}
