using Core;
using System;
using System.Globalization;
using System.Windows.Data;

namespace WordStrata
{
    public class StrokeValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var board = values[0] as Board;
            var paths = values[1] as UserPaths;
            double thickness = 100 / (double)(Math.Max(board.Rows, board.Columns) * (paths.Count + 1));
            return thickness;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}