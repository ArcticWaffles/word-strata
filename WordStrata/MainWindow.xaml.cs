using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordStrata.Solver;

namespace WordStrata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Tile> tiles = new List<Tile>();
            Random letterGenerator = new Random();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    tiles.Add(new Tile(new Coordinates(x, y), Convert.ToChar(letterGenerator.Next(65, 91))));
                }
            }

            boardGrid.ItemsSource = tiles;

        }


        private void TileToggleButton_Click(object sender, RoutedEventArgs e)
        {

            ToggleButton theSender = (ToggleButton)sender;

            if(theSender.IsChecked == true)
            {
                string startingText = theWord.Text;
                string endingText = String.Concat(startingText, theSender.Content);
                theWord.Text = endingText;
            }
            else
            {
                theWord.Text = theWord.Text.Remove(theWord.Text.Length - 1);
            }

        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
