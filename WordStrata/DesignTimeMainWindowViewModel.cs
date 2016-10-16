using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata
{
    class DesignTimeMainWindowViewModel : IMainWindowViewModel
    {
        public List<TileViewModel> GuiTiles
        {
            get
            {
                var GameBoard = Solve.BoardGenerator.generateWeightedBoard(5, 5);
                var guiTiles = new List<TileViewModel>();

                foreach (var tile in GameBoard.Tiles)
                {
                    guiTiles.Add(new TileViewModel(tile, 0));
                }
                return guiTiles;
            }
        }

        public string UserWord { get; set; } = "ABIOGENETICALLY";
    }
}
