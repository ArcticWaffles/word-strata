using System.Collections.Generic;
using WordStrata.Solve;

namespace WordStrata
{
    public abstract class MainWindowViewModelBase
    {
        public abstract List<TileViewModel> GuiTiles { get; }
        public abstract UserTileSelections UserSelections { get; set; }
        public abstract Board GameBoard { get; }

        //protected bool TileIsChecked(TileViewModel tileVM)
        //{
        //    return UserSelections.Selections.Contains(tileVM);
        //}

        //protected void ClickTile(TileViewModel tileVM, bool isChecked)
        //{
        //    if(isChecked)
        //    {
        //        UserSelections.Selections.Add(tileVM);
        //    }
        //    else
        //    {
        //        UserSelections.Selections.Remove(tileVM);
        //    }
        //}
    }
}