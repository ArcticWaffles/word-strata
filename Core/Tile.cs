using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Tile : GridSquare
    {
        public char Letter { get; }

        public int Depth { get; } = 0;

        public Tile(Coordinates coordsIn, char letterIn) : base(coordsIn)
        {
            Letter = letterIn;
        }

    }
}
