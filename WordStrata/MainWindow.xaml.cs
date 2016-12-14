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
using System.Windows.Media.Animation;
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
            DataContext = viewModel;
        }


        private void TileToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton theSender = (ToggleButton)sender;
            Tile senderTile = (Tile)theSender.DataContext;

            // User clicks a tile
            if (theSender.IsChecked == true)
            {
                viewModel.ClickTile(senderTile);
            }

            // User unclicks a tile
            else
            {
                viewModel.UnclickTile(senderTile);
            }

        }

        // User submits a word
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // If the word is valid
            if (viewModel.CheckWord())
            {
                WordFeedback.Text = "Word Accepted!";
                WordFeedback.Foreground = Brushes.Green;
                (Resources["AcceptWord"] as Storyboard).Begin();
                viewModel.ClearWord();
                // TODO: Delete used tiles
            }
            else // Word is not valid
            {
                WordFeedback.Foreground = Brushes.Red;
                WordFeedback.Text = "Word Not Accepted!";
                (Resources["RejectWord"] as Storyboard).Begin();
            }
        }

        // User clears the word box
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearWord();
        }

        private void Word_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = viewModel.CheckLetter(e.Key);
        }
    }
}

//TODO: Turning used letters into holes
//TODO: Combine unit test classes, convert old ones to NUnit
//TODO: Error handling
//TODO: Use solver to make sure moves remain
//TODO: "Submit" button - implement results
//TODO: Clean up and commenting
//TODO: Try /// comments?
//TODO: Allow user to type the word
