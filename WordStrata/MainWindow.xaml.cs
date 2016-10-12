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
using WordStrata.Solve;


namespace WordStrata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel dataContext;

        public MainWindow()
        {
            InitializeComponent();
            dataContext = (MainWindowViewModel)DataContext;
        }


        private void TileToggleButton_Click(object sender, RoutedEventArgs e)
        {

            ToggleButton theSender = (ToggleButton)sender;
            TileViewModel senderTile = (TileViewModel)theSender.DataContext;

            // User clicks a tile: The letter is added to UserWord. The tile is added to the UserSelections list and its properties are updated.
            if (theSender.IsChecked == true)
            {
                dataContext.UserWord += theSender.Content;
                dataContext.UserSelections.Add(senderTile);
                senderTile.IsClicked = true;
                senderTile.IsCurrentTile = true;
            }

            // User unclicks a tile: The letter is removed from UserWord. The tile is removed from the UserSelections list and its properties are updated.
            else
            {
                dataContext.UserWord = dataContext.UserWord.Remove(dataContext.UserWord.Length - 1);
                dataContext.UserSelections.Remove(senderTile);
                senderTile.IsClicked = false;
                senderTile.IsCurrentTile = false;
            }

            // The last tile in the UserSelections list becomes the new CurrentTile
            if (dataContext.UserSelections != null && dataContext.UserSelections.Count > 0)
            {
                dataContext.CurrentGuiTile = dataContext.UserSelections.Last();
                dataContext.CurrentGuiTile.IsCurrentTile = true;
            }
            else
            {
                dataContext.CurrentGuiTile = null;
            }

            // IsClickable and IsCurrentTile property is updated for all tiles
            foreach (var guiTile in dataContext.GuiTiles)
            {
                if(guiTile != dataContext.CurrentGuiTile)
                {
                    guiTile.IsCurrentTile = false;
                }
                guiTile.IsClickable = guiTile.determineClickability(dataContext.CurrentGuiTile, dataContext.UserSelections, dataContext.GameBoard);
            }

        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if(dataContext.Dictionary.Contains(dataContext.UserWord))
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Not a word!");
            }
        }
    }
}

//TODO: Incorporate dictionary (Solver)
//TODO: Turning used letters into holes
//TODO: VM unit tests?
//TODO: Error handling
//TODO: Should user selection list be Observable Collection?
//TODO: "Submit" button, test user word against dictionary
//TODO: More comments
//TODO: Try /// comments?
//TODO: install comment flow thingie
