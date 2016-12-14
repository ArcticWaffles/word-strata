using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
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
