﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Core;

namespace WordStrata
{
    /// <summary>
    /// Converts a gridsquare to a char.
    /// </summary>
    internal class GridsquareContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridsquare = value as Gridsquare;
            if (gridsquare is Tile)
                return (gridsquare as Tile).Letter;
            else return " ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}