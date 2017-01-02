using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    /// <summary>
    /// Used by the Solver to check a word against a dictionary or
    /// another word.
    /// </summary>
    public abstract class Checker
    {
        public bool ShallContinue { get; set; }

        public int StartDepth { get; set; }

        public int EndDepth { get; set; }

        public abstract void Check(List<Tile> path);

    }
}

// TODO: Make checker classes private?
