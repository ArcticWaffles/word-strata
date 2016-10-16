using System.Collections.Generic;

namespace WordStrata
{
    public interface IMainWindowViewModel
    {
        List<TileViewModel> GuiTiles { get; }
        string UserWord { get; set; }
    }
}