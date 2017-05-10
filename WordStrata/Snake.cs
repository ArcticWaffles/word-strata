using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Core;
using System.Windows;

namespace WordStrata
{
    /// <summary>
    /// Colored line tracing the selected path of tiles in the user interface.
    /// </summary>
    public class Snake
    {
        public Snake(TilePath tiles, Board gameboard)
        {
            var rows = gameboard.Rows;
            var columns = gameboard.Columns;
            HorizontalStretch = columns * 100;
            VerticalStretch = rows * 100;
            if (tiles.Any())
            {
                // Translates built-in tile coordinates to GUI coordinates
                foreach (var tile in tiles)
                {
                    var guiCoordsX = HorizontalStretch * ((double)tile.Coords.Y / columns + .5 / columns);
                    var guiCoordsY = VerticalStretch * ((double)tile.Coords.X / rows + .5 / rows);
                    Points.Add(new Point(guiCoordsX, guiCoordsY));
                }
            }
        }

        public PointCollection Points { get; set; } = new PointCollection();

        public SolidColorBrush SnakeColor { get; set; } = Brushes.Green;

        public int HorizontalStretch { get; }

        public int VerticalStretch { get; }
    }
}