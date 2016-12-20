using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    // WordChecker is used by the Solver to check a word against a dictionary or
    // another word. It is the parent class of DictionaryChecker and
    // StringChecker.
    public abstract class WordChecker
    {
        public bool ShallContinue { get; set; }

        public int StartDepth { get; set; }

        public int EndDepth { get; set; }

        public abstract void Check(List<Tile> path);

        public string GetLetters(List<Tile> path)
        {
            string word = "";
            foreach (var tile in path)
            {
                word += tile.Letter;
            }
            return word;
        }

    }
}
