﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    class GridSquare
    {
        private Coordinates coords;
        public Coordinates Coords
        {
            get { return coords; }
            private set { coords = value; }
        }

        public GridSquare(Coordinates coordsIn)
        {
            Coords = coordsIn;
        }
    }
}
