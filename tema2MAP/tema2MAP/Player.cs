using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tema2MAP
{
    internal class Player
    {
        private string? _name { get; set; }
        private int _indexImage { get; set; }
        private List<Game>? _gamesPlayed { get; set; }

        public Player() {}
        public Player(string name, int indexImage)
        {
            _name = name;
            _indexImage = indexImage;
            _gamesPlayed = new List<Game>();
        }


    }
}
