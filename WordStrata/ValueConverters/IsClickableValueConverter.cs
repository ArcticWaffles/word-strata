using Core;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WordStrata
{
    /// <summary>
    /// Determines if a tile is clickable (enabled). 
    /// </summary>
    internal class IsClickableValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var path = values[0] as TilePath;
            var gridsquare = values[1] as Gridsquare;
            var currentTile = path.CurrentTile;

            if (gridsquare is Hole) return false;
            else return (TileIsClickable(gridsquare as Tile, currentTile, path));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        // A tile is clickable if any of the following are true:
        // 1. No tiles on the board are selected (ThePath is null or empty)
        // 2. It neighbors the current tile and is not already selected
        // 3. It is the current tile (user can click it to backtrack)
        private bool TileIsClickable(Tile tile, Tile currentTile, TilePath path)
        {
            if (!path.Any() || path == null) return true;
            else
            {
                return (AreNeighbors(currentTile, tile) && (path.Contains(tile) == false) ||
                tile == currentTile);
            }
        }

        private bool AreNeighbors(Tile tile1, Tile tile2)
        {
            if (tile1 == null || tile2 == null)
            {
                return false;
            }

            var x1 = tile1.Coords.X;
            var y1 = tile1.Coords.Y;

            var x2 = tile2.Coords.X;
            var y2 = tile2.Coords.Y;

            var vertical = (y1 == y2 && (Math.Abs(x1 - x2) == 1));
            var horizontal = (x1 == x2 && (Math.Abs(y1 - y2) == 1));
            var diagonal = Math.Abs(x1 - x2) == 1 && Math.Abs(y1 - y2) == 1;

            return vertical || horizontal || diagonal;
        }
    }
}