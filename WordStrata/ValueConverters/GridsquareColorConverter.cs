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
    class GridsquareColorConverter : IValueConverter
    {
        // Makes "deeper" tiles darker so user can tell layers apart. Accommodates up to 5 layers.
        // Temporary solution until some kind of 3D visual layering effect can be implemented.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridsquare = value as Gridsquare;
            Color holeColor = (Color)ColorConverter.ConvertFromString("#FF45779e");
            if (gridsquare is Hole) return holeColor.ToString();
            else
            {
                Color baseColor = (Color)ColorConverter.ConvertFromString("#FF5a9cce");
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
                baseColor.A = (byte)(opacityLevel * 255);
                return baseColor.ToString();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
