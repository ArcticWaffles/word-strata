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
        public override Board GameBoard { get; } = BoardGenerator.generateWeightedBoard(5, 5);

        public override UserTileSelections UserSelections { get; set; } = new UserTileSelections();

        //public string UserWord { get; set; } = "ABIOGENETICALLY";
    }
}
