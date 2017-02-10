﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Core;

namespace WordStrata
{
    class IsClickableValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var paths = values[0] as UserPaths;
            var gridsquare = values[1] as GridSquare;
            var currentTiles = paths.CurrentTiles;

            if (gridsquare is Hole) return false;
            if (paths.Count == 0 || paths.IsEmpty() || paths == null) return true;
            
            foreach (var tile in currentTiles)
            {
                if (TileIsClickable(gridsquare as Tile, tile, paths)) return true;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        // A tile is clickable if any of the following are true:
        // 1. It neighbors a current tile and is not already selected
        // 2. It is a current tile (user can click it to backtrack)
        // 3. No tiles on the board are selected (Paths is null or empty)
        private bool TileIsClickable(Tile tile, Tile currentTile, UserPaths paths)
        {
            return (AreNeighbors(currentTile, tile) && (paths.Contains(tile) == false)) ||
                tile == currentTile; 
        }

        private bool AreNeighbors(Tile tile1, Tile tile2)
        {
            if(tile1 == null || tile2 == null)
            {
                return false;
            }

            var x1 = tile1.Coords.X;
            var y1 = tile1.Coords.Y;

            var x2 = tile2.Coords.X;
            var y2 = tile2.Coords.Y;

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
