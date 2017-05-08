using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Core;

namespace WordStrata
{
    class IsCheckedValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var paths = values[0] as TilePath;
            var gridsquare = values[1] as Gridsquare;
            if (gridsquare is Tile)
                return paths.Contains(gridsquare as Tile);
            else return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}