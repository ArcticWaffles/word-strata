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

namespace Core
{
    /// <summary>
    /// List of tiles the player has selected.
    /// </summary>
    public class TilePath : List<Tile>
    {
        public TilePath()
        {
        }

        public TilePath(IEnumerable<Tile> collection) : base(collection)
        {
        }

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
