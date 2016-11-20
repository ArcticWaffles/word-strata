using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata.Solve;


namespace WordStrata
{
    public class TileViewModel
    {
        public TileViewModel(Tile tile)
        {
            TheTile = tile;
        }

        public Tile TheTile { get; }


    }
}
