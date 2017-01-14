using System.Collections.Generic;
using Core;

namespace WordStrata
{
    public abstract class MainWindowViewModelBase
    {
        public abstract UserTileSelections UserSelections { get; set; }
        public abstract Board GameBoard { get; }
    }
}