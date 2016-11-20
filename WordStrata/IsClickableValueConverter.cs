using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WordStrata
{
    class IsClickableValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var userSelections = values[0] as Collection<TileViewModel>;
            var currentTile = values[1] as TileViewModel;
            var tile = values[2] as TileViewModel;

            return TileIsClickable(tile, currentTile, userSelections);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        // A tile is clickable if any of the following are true:
        // 1. It neighbors the current tile and is not already selected
        // 2. It is the current tile (user can click it to backtrack)
        // 3. No tiles on the board are selected (UserSelections is null or empty)
        protected bool TileIsClickable(TileViewModel tileVM, TileViewModel currentTile, Collection<TileViewModel> userSelections)
        {
            return (AreNeighbors(currentTile, tileVM) && (userSelections.Contains(tileVM) == false)
                    || tileVM == currentTile || userSelections.Count == 0 || userSelections == null);
        }

        private bool AreNeighbors(TileViewModel tile1, TileViewModel tile2)
        {
            if(tile1 == null || tile2 == null)
            {
                return false;
            }

            var x1 = tile1.TheTile.Coords.X;
            var y1 = tile1.TheTile.Coords.Y;

            var x2 = tile2.TheTile.Coords.X;
            var y2 = tile2.TheTile.Coords.Y;

            // Tiles are neighbors if the x coordinates are equal and the y
            // coordinates differ by one, or vice-versa.
            if (x1 == x2 && (Math.Abs(y1 - y2) == 1))
            {
                return true;
            }
            else if (y1 == y2 && (Math.Abs(x1 - x2) == 1))
            {
                return true;
            }
            else return false;
        }

    }
}
