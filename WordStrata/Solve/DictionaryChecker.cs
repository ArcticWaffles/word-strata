using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
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
