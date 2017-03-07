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
        public Snake(double location, Path path, SolidColorBrush color)
        {
            LocationOnTile = location;
            Path = path;
            SnakeColor = color;
        }

        public PointCollection Points { get; set; } = new PointCollection();

        public SolidColorBrush SnakeColor { get; set; }

        public double LocationOnTile { get; }

        public Path Path { get; }

        public static List<Color> makeUniqueColors(int listSize)
        {
            List<Color> colorList = new List<Color>();
            Byte r, g, b;
            Color color = new Color();
            for(int i = 0; i < listSize; i++)
            {
                //var random = new Random();
                //r = (byte)((i + 1) * (random.Next()) % 255);
                //g = (byte)((i + 1) * (random.Next()) % 255);
                //b = (byte)((i + 1) * (random.Next()) % 255);
                r = (byte)(60 * (i + 1) % 255);
                g = (byte)(110 * (i + 2) % 255);
                b = (byte)(180 * (i + 3) % 255);

                color = Color.FromRgb(r, g, b);
                colorList.Add(color);
            }
            return colorList;
        }
    }
 
}
