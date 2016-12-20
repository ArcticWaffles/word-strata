using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    // DictionaryChecker is used by Solver to check the validity of a word by
    // checking it against the dictionary. Its purpose is to verify whether any
    // valid word remains on the board, so it has a bool result and stops after
    // finding one match.
    public class DictionaryChecker : WordChecker
    {
        public DictionaryChecker(HashSet<string> theDictionary)
        {
            dictionary = theDictionary;
        }

        private HashSet<String> dictionary;


        public override void Check(List<Tile> path)
        {
            string word = GetLetters(path);
            Result = dictionary.Contains(word);
            ShallContinue = !Result;
        }

        public bool Result { get; set; }
    }
}
