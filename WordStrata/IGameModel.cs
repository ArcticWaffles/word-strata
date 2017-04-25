using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WordStrata
{
    public interface IGameModel
    {
        Board GameBoard { get; }

        HashSet<String> Dictionary { get; }
    }
}