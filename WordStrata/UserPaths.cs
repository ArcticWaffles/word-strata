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
using System.Collections;
using System.Windows;

namespace WordStrata
{
    /// <summary>
    /// Keeps track of tile path(s) the player has selected, either by clicking or typing. May be more than one path if the board has multiple occurrences.
    /// </summary>
    public class UserPaths : List<Path>
    {
        /// <summary> Last tile(s) in the <see cref="UserPaths"/> path(s). Used for determining which tiles on the board are clickable. </summary>
        public Path CurrentTiles
        {
            get
            {
                var allCurrentTiles = new Path();
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
                if (list.Any())
                {
                    isEmpty = false;
                }
            }
            return isEmpty;
        }

        internal class PathComparison : IComparer<Path>
        {
            public int Compare(Path listA, Path listB)
            {
                // Find the "average point" of each list.
                Point pointA = new Point();
                Point pointB = new Point();
                pointA.X = listA.Average(t => t.Coords.X);
                pointA.Y = listA.Average(t => t.Coords.Y);
                pointB.X = listB.Average(t => t.Coords.X);
                pointB.Y = listB.Average(t => t.Coords.Y);

                // TODO: improve algorithm to reflect notes
                if (pointA == pointB)
                {
                    return 0;
                }
                else if (pointA.Y > pointB.Y) return 1;
                else if (pointA.Y == pointB.Y && pointA.X < pointB.X) return 1;
                else return -1;
            }
        }

        public List<SortedSet<Path>> GroupPaths()
        {
            var groupsOfPaths = new List<SortedSet<Path>>();
            var pathsCopy = new List<Path>(this);
            while (pathsCopy.Count > 1)
            {
                // Find busiest tile
                var allTiles = new Path();
                foreach (var path in pathsCopy)
                {
                    foreach (var tile in path)
                    {
                        allTiles.Add(tile);
                    }
                }
                var mostFrequentTile = (from t in allTiles
                                        group t by t into grp
                                        orderby grp.Count() descending
                                        select grp.Key).First();
                var intersectingPaths = new SortedSet<Path>(new PathComparison());
                foreach (var path in pathsCopy)
                {
                    if (path.Contains(mostFrequentTile))
                    {
                        intersectingPaths.Add(path);
                    }
                }
                groupsOfPaths.Add(intersectingPaths);
                foreach (var path in intersectingPaths)
                {
                    pathsCopy.Remove(path);
                }
            }
            return groupsOfPaths;
        }
    }

}
