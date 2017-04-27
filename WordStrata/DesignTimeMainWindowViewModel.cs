using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Solve;

namespace WordStrata
{
    class DesignTimeMainWindowViewModel : MainWindowViewModelBase
    {
        public override Board GameBoard { get; } = BoardGenerator.generateWeightedBoard(5, 5, 1);

        public override TilePath ThePath { get; set; } = new TilePath();

        //public string UserWord { get; set; } = "ABIOGENETICALLY";
    }
}
