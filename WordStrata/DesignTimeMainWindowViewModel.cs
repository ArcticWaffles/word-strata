using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata.Solve;

namespace WordStrata
{
    class DesignTimeMainWindowViewModel : MainWindowViewModelBase
    {
        public override List<TileViewModel> GuiTiles
        {
            get
            {
                var GameBoard = Solve.BoardGenerator.generateWeightedBoard(5, 5);
                var guiTiles = new List<TileViewModel>();

                foreach (var tile in GameBoard.Tiles)
                {
                    guiTiles.Add(new TileViewModel(tile, ClickTile, TileIsChecked, TileIsClickable));
                }
                return guiTiles;
            }
        }

        //meaningless for now
        public override Board GameBoard { get; }

        public override UserTileSelections UserSelections { get; set; } = new UserTileSelections();

        //public string UserWord { get; set; } = "ABIOGENETICALLY";
    }
}
