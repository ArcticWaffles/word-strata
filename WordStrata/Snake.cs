using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Core;

namespace WordStrata
{
    public class Snake
    {
        public Snake(double location, Path path)
        {
            LocationOnTile = location;
            Path = path;
        }

        public PointCollection Points { get; set; }

        public SolidColorBrush Color { get; set; }

        public double LocationOnTile { get; }

        public Path Path { get; }
    }
}
