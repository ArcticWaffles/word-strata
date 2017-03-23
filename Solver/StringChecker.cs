using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Solve
{ 
    /// <summary>
    /// Used by Solver to check whether a provided string exists on the board as a valid path. 
    /// Since the word may be formed by more than one combination of tiles,
    /// StringChecker looks for and returns all matching tile paths.
    /// </summary>
    class StringChecker : Checker
    {
        /// <param name="theMasterWord"> Word to be searched for on the board. </param>
        internal StringChecker(string theMasterWord)
        {
            masterWord = theMasterWord;
            // ShallContinue is always true because StringChecker searches for all possible matches.
            ShallContinue = true;
            if (theMasterWord.Length <= 0) StartDepth = 0;
            else StartDepth = theMasterWord.Length - 1;
            EndDepth = theMasterWord.Length;
        }


        /// <summary> Word to be searched for on the board. </summary>
        private string masterWord;


        /// <summary> Contains all matching tile paths found on the board. </summary>
        internal List<TilePath> Result { get; set; } = new List<TilePath>();


        /// <summary> Checks <see cref="masterWord"/> against a tile path's letters. </summary>
        internal override void Check(TilePath path)
        {
            string word = Solver.GetLetters(path);
            if (word.Equals(masterWord, StringComparison.OrdinalIgnoreCase))
            {
                Result.Add(path);
            }
        }
    }
}
    