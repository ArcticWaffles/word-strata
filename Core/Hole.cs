﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Location on the board that doesn't contain a tile. 
    /// </summary>
    public class Hole : GridSquare
    {
        public Hole(Coordinates coordsIn) : base(coordsIn)
        {}
    }
}
