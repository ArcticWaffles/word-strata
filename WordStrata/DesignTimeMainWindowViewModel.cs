using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Solve;

namespace WordStrata
{
    class DesignTimeMainWindowViewModel : IMainWindowViewModel
    {
        public Board GameBoard { get; } = BoardGenerator.generateRectangularBoard(5, 5, 1);

        public TilePath ThePath { get; set; } = new TilePath();

        public string UserWord { get; } = "ABIOGENETICALLY";

        public HashSet<String> Dictionary { get; }
    }
}
