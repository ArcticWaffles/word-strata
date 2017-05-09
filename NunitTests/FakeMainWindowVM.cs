using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using WordStrata;

namespace NUnitTests
{
    class FakeMainWindowVM : IMainWindowViewModel
    {
        public FakeMainWindowVM(HashSet<String> dictionary)
        {
            Dictionary = dictionary;
        }

        public TilePath ThePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Board GameBoard => throw new NotImplementedException();

        public HashSet<string> Dictionary { get; }

        public string UserWord { get; set; }

        public bool EnableSubmit
        {
            get
            {
                return Dictionary.Contains(UserWord, StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}
