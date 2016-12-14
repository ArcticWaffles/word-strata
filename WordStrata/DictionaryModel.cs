using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata
{
    public static class DictionaryModel
    {
        public static HashSet<string> GetDictionary()
        {
            var list = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("sowpods.txt");
            while ((line = file.ReadLine()) != null)
            {
                list.Add(line);
            }
            file.Close();
            return list;
            //TODO: file error handling?
            //TODO: check dictionary for longest word to set max word length in solver?
        }
    }
}
