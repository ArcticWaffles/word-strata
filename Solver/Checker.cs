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
    abstract class Checker
    {
        /// <summary> Determines whether the search should keep going. </summary>
        internal bool ShallContinue { get; set; }

        /// <summary> Minimum size of word to check (inclusive, index-based, e.g. zero checks one-letter words) </summary>
        internal int StartDepth { get; set; }

        /// <summary> Maximum size of word to check (exclusive) </summary>
        internal int EndDepth { get; set; }

        /// <summary> Tile list whose letters are used as the string for comparison.</summary>
        internal abstract void Check(TilePath path);
    }
}

