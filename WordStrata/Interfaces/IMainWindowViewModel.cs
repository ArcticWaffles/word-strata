using System.Collections.Generic;
using Core;
using System;
using System.Collections.ObjectModel;

namespace WordStrata
{
    public interface IMainWindowViewModel
    {
        ObservableCollection<Tile> ThePath { get; set; }

        Board GameBoard { get; }

        HashSet<String> Dictionary { get; }

        String UserWord { get; }
    }
}