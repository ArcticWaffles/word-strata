using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// A specific location on the board (instantiated either as a tile or a hole).
    /// </summary>
    public abstract class Gridsquare
    {
        public Gridsquare(Coordinates coordsIn)
        {
            Coords = coordsIn;
        }

        public Coordinates Coords { get; set; }
    }
}
