using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata;
using Core;

namespace NUnitTests
{
    public class FakeTilePath : ObservableCollection<Tile>
    {
        public FakeTilePath(string word)
        {
            foreach (char c in word)
            {
                Add(new Tile(new Coordinates(0, 0, 0), c));
            }
        }
    }
}