using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Solve
{
    /// <summary>
    /// Used by the Solver to check a word against a dictionary or another word.
    /// </summary>
    public abstract class Checker
    {
        /// <summary> Determines whether the search should keep going. </summary>
        public bool ShallContinue { get; set; }

        /// <summary> Minimum size of word to check (inclusive, index-based, e.g. zero checks one-letter words) </summary>
        public int StartDepth { get; set; }

        /// <summary> Maximum size of word to check (exclusive) </summary>
        public int EndDepth { get; set; }

        public abstract void Check(List<Tile> path);
    }
}

// TODO: Make checker classes private?
