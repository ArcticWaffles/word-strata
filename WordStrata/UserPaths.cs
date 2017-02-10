using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;
using Core;
using Solve;

namespace WordStrata
{
    /// <summary>
    /// Keeps track of tile path(s) the player has selected, either by clicking or typing. May be more than one path if the board has multiple occurrences.
    /// </summary>
    public class UserPaths : List<List<Tile>>
    {
        /// <summary> Last tile(s) in the <see cref="UserPaths"/> path(s). Used for determining which tiles on the board are clickable. </summary>
        public List<Tile> CurrentTiles
        {
            get
            {
                var allCurrentTiles = new List<Tile>();
                foreach (var list in this)
                {
                    allCurrentTiles.Add(list.Last());
                }
                return allCurrentTiles; 
            }
        }

        internal bool Contains(Tile tile)
        {
            foreach (var list in this)
            {
                if (list.Contains(tile))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool IsEmpty()
        {
            bool isEmpty = true;
            foreach (var list in this)
            {
                if (!(list.Count == 0))
                {
                    isEmpty = false;
                }
            }
            return isEmpty;
        }

    }

}
