using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
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
