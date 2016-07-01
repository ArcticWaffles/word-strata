using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Tile : GridSquare
    {
        readonly char letter;
        public char Letter
        {
            get { return letter; }
        }

        public Tile(Coordinates coordsIn, char letterIn) : base(coordsIn)
        {
            letter = letterIn;
        }

    }
}
