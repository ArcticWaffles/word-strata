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

    }
}

//TODO: Make GUI tile class with color?
//TODO: Make word textblock bigger, bolder, margin, etc
//TODO: Enforce adjacent tiles
//TODO: Encorporate dictionary, actual board
//TODO: Weigh letters so it isn't truly random
//TODO: in XAML under itemscontrol, set item source
//TODO: Any other whiteboard stuff I've missed