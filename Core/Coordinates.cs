using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Cartesian coordinates.
    /// </summary>
    public class Coordinates
    {
        public int X;
        public int Y;
        public int Z;

        public Coordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}

// TODO: Should Coordinates remain its own class or just make properties on Gridsquare?