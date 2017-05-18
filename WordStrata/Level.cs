using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WordStrata
{
    public class Level
    {
        public Level(bool[,,] tileLocations, Color baseColor, Color holeColor)
        {
            TileLocations = tileLocations;
            BaseColor = baseColor;
            HoleColor = holeColor;
        }

        public bool[,,] TileLocations { get; }

        public System.Drawing.Image BackgroundImage { get; }

        public Color BaseColor { get; }

        public Color HoleColor { get; }
    }
}