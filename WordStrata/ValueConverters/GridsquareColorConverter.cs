using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Core;
using System.Windows.Media;

namespace WordStrata
{
    internal class GridsquareColorConverter : IMultiValueConverter
    {
        // Makes "deeper" tiles darker so user can tell layers apart. Accommodates up to 5 layers.
        // Temporary solution until some kind of 3D visual layering effect can be implemented.

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var levelNumber = (int)values[0];
            Level level = Levels.GameLevels[levelNumber - 1];
            var gridsquare = values[1] as Gridsquare;
            Brush holeColor = new SolidColorBrush(level.HoleColor);
            if (gridsquare is Hole) return holeColor;
            else
            {
                double opacityLevel = 0;
                switch (gridsquare.Coords.Z)
                {
                    case 0:
                        opacityLevel = 1;
                        break;

                    case 1:
                        opacityLevel = .8;
                        break;

                    case 2:
                        opacityLevel = .6;
                        break;

                    case 3:
                        opacityLevel = .4;
                        break;

                    case 4:
                        opacityLevel = .2;
                        break;

                    default:
                        opacityLevel = 0;
                        break;
                }
                Brush tileColor = new SolidColorBrush(level.BaseColor)
                {
                    Opacity = opacityLevel
                };
                return tileColor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}