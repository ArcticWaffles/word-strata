﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata.Solve;

namespace WordStrata
{
    public interface IGameModel
    {
        Board GameBoard { get; }

        HashSet<String> Dictionary { get; }

    }
}
