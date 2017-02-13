using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Building blocks of the gameboard.
    /// </summary>
    public class Tile : GridSquare
    {
        public Tile(Coordinates coordsIn, char letterIn) : base(coordsIn)
        {
            Letter = letterIn;
        }

        public char Letter { get; }
    }
}
