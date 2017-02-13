using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Solve
{
    /// <summary>
    /// Checks the validity of a word by comparing it to a dictionary. Used by
    /// Solver to check whether any valid word remains on the board; stops after
    /// finding one match.
    /// </summary>
    class DictionaryChecker : Checker
    {
        internal DictionaryChecker(HashSet<string> theDictionary)
        {
            dictionary = theDictionary;
            StartDepth = 0;
            // Max word length to check is the length of the longest word in the dictionary.
            EndDepth = theDictionary.Max(x => x.Length);
        }


        private HashSet<String> dictionary;


        internal override void Check(List<Tile> path)
        {
            string word = Solver.GetLetters(path);
            Result = dictionary.Contains(word);
            // Stop if a match is found; otherwise continue.
            ShallContinue = !Result;
        }

        /// <summary> Indicated whether the word is found in the dictionary. </summary>
        internal bool Result { get; set; }
    }
}
// TODO: Would it be any more efficient to change max word length function to find
// longest word in dictionary that doesn't have a "subword" in it?
