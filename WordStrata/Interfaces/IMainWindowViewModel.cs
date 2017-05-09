using System.Collections.Generic;
using Core;
using System;

namespace WordStrata
{
    public interface IMainWindowViewModel
    {
        TilePath ThePath { get; set; }

        Board GameBoard { get; }

        HashSet<String> Dictionary { get; }

        String UserWord { get; }
    }
}