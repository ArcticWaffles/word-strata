using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Solve;
using System.Collections.ObjectModel;

namespace WordStrata
{
    /// <summary>
    /// For development use only. Provides a simple version of the GUI that is visible in the designer.
    /// </summary>
    class DesignTimeMainWindowViewModel : IMainWindowViewModel
    {
        public Board GameBoard { get; } = BoardGenerator.generateRectangularBoard(5, 5, 1);

        public ObservableCollection<Tile> ThePath { get; set; } = new TilePath();

        public string UserWord { get; } = "ABIOGENETICALLY";

        public HashSet<String> Dictionary { get; }
    }
}
