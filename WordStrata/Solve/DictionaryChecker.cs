using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    /// <summary>
    /// Used by Solver to check the validity of a word by
    /// checking it against the dictionary. Verifies whether any
    /// valid word remains on the board and stops after
    /// finding one match.
    /// </summary>
    public class DictionaryChecker : Checker
    {
        public DictionaryChecker(HashSet<string> theDictionary)
        {
            dictionary = theDictionary;
            StartDepth = 0;
            // Max word length to check is the length of the longest word in the dictionary.
            EndDepth = theDictionary.Max(x => x.Length);
        }

        private HashSet<String> dictionary;


        public override void Check(List<Tile> path)
        {
            string word = Solver.GetLetters(path);
            Result = dictionary.Contains(word);
            // Stop if a match is found; otherwise continue.
            ShallContinue = !Result;
        }

        public bool Result { get; set; }
    }
}
// TODO: Would it be any more efficient to change max word length function to find
// longest word in dictionary that doesn't have a "subword" in it?
