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
    public class Snake
    {
        public PointCollection Points { get; set; } = new PointCollection();

        public SolidColorBrush SnakeColor { get; set; } = Brushes.DarkSeaGreen; // TODO: Make this a trigger relating to word validity
    }
 
}
