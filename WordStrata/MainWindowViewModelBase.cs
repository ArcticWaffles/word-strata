using System.Collections.Generic;
using Core;

namespace WordStrata
{
    public abstract class MainWindowViewModelBase
    {
        public abstract TilePath ThePath { get; set; }
        public abstract Board GameBoard { get; }
    }
}