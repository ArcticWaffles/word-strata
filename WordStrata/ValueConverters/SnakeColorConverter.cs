using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WordStrata
{
    class SnakeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isValidWord = (bool)value;
            if (isValidWord) return Brushes.Green;
            else return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

// [TestCase(dictionaryA, board2x2)]
// [TestCase(dictionaryA, board3x3)]
// [TestCase(dictionaryB, board3x3)]
// public void AnyWordExistsOnBoard_WordExists_ResultIsTrue(HashSet<string> dictionary, Board gameboard)
//{
//    var result = Solver.AnyWordExistsOnBoard(dictionary, gameboard);
//    Assert.That(result, Is.True);
//}