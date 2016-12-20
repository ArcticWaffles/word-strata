using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    // StringChecker is used by Solver to check a word on the board against a
    // provided string. It is used in the process of verifying that a
    // user-entered word exists on the board as a valid path. Since the word may
    // be formed by more than one combination of tiles, StringChecker looks for
    // all matching tile paths (ShallContinue is always true) and saves each as
    // a list.
    public class StringChecker : WordChecker
    {
        public StringChecker(string theMasterWord)
        {
            masterWord = theMasterWord;
            ShallContinue = true;
        }

        private string masterWord;

        public List<List<Tile>> Result { get; set; } = new List<List<Tile>>();

        public override void Check(List<Tile> path)
        {
            string word = GetLetters(path);
            if (word.Equals(masterWord, StringComparison.OrdinalIgnoreCase))
            {
                Result.Add(path);
            }
        }
    }
}
