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
        MainWindowViewModel viewModel;
        GameModel gameModel;

        public MainWindow()
        {
            InitializeComponent();
            HashSet<string> dictionary = DictionaryModel.GetDictionary();
            gameModel = new GameModel(dictionary);
            viewModel = new MainWindowViewModel(gameModel);
            this.DataContext = viewModel;
        }


        //private void TileToggleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ToggleButton theSender = (ToggleButton)sender;
        //    TileViewModel senderTile = (TileViewModel)theSender.DataContext;

        //    // User clicks a tile
        //    if (theSender.IsChecked == true)
        //    {
        //        viewModel.ClickTile(senderTile);
        //    }

        //    // User unclicks a tile
        //    else
        //    {
        //        viewModel.UnclickTile(senderTile);
        //    }

        //    viewModel.DetermineClickability();

        //}

        // User submits a word
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // If the word is valid
            if (viewModel.CheckWord())
            {
                Word.BorderBrush = Brushes.Green;
                WordFeedback.Visibility = Visibility.Visible; 
                WordFeedback.Foreground = Brushes.Green;
                WordFeedback.Text = "Word Accepted!";
                // Make this a trigger in XAML?
                // Delete used tiles
                // Clear user word and user selections
                // Turn text box green and write success message for a couple seconds
                // in general: have clear word button. Backspace too?
            }
            else // Word is not valid
            {
                Word.BorderBrush = Brushes.Red;
                WordFeedback.Visibility = Visibility.Visible;
                WordFeedback.Foreground = Brushes.Red;
                WordFeedback.Text = "Word Not Accepted!";
                // Turn text box red and write failure message for a couple seconds
                // can use clear word button to remove word

            }
        }

        // User clears the word box
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearWord();
            //viewModel.DetermineClickability();
        }
    }
}

//TODO: Turning used letters into holes
//TODO: VM unit tests?
//TODO: Error handling
//TODO: Use solver to make sure moves remain
//TODO: "Submit" button - implement results
//TODO: More comments, use comment reflower
//TODO: Try /// comments?
//TODO: Make it so user can type the word
