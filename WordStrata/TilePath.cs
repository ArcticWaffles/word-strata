using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;
using System.Windows;
using Core;

namespace WordStrata
{
    /// <summary>
    /// List of tiles the player has selected.
    /// </summary>
    public class TilePath : ObservableCollection<Tile>
    {
        /// <summary> Last tile in the list. Used for determining which tiles on the board are clickable. </summary>
        public Tile CurrentTile
        {
            get
            {
                if (Count == 0)
                {
                    return null;
                }
                else
                {
                    return this.Last();
                }
            }
        }
    }
}
