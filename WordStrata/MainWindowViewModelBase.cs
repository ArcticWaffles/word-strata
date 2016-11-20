using System.Collections.Generic;
using WordStrata.Solve;

namespace WordStrata
{
    public abstract class MainWindowViewModelBase
    {
        public abstract UserTileSelections UserSelections { get; set; }
        public abstract Board GameBoard { get; }
    }
}