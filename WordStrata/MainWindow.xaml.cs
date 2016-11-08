﻿using System;
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


        private void TileToggleButton_Click(object sender, RoutedEventArgs e)
        {

            ToggleButton theSender = (ToggleButton)sender;
            TileViewModel senderTile = (TileViewModel)theSender.DataContext;

            // User clicks a tile
            if (theSender.IsChecked == true)
            {
                viewModel.ClickTile(senderTile);
            }
            //{
            //    viewModel.UserWord += theSender.Content;
            //    viewModel.UserSelections.Add(senderTile);
            //    senderTile.IsClicked = true;
            //    senderTile.IsCurrentTile = true;
            //}

            // User unclicks a tile
            else
            {
                viewModel.UnclickTile(senderTile);
                //viewModel.UserWord = viewModel.UserWord.Remove(viewModel.UserWord.Length - 1);
                //viewModel.UserSelections.Remove(senderTile);
                //senderTile.IsClicked = false;
                //senderTile.IsCurrentTile = false;
            }

            // The last tile in the UserSelections list becomes the new CurrentTile.
            // TODO: Binding?
            //if (viewModel.UserSelections != null && viewModel.UserSelections.Count > 0)
            //{
            //    viewModel.CurrentGuiTile = viewModel.UserSelections.Last();
            //    viewModel.CurrentGuiTile.IsCurrentTile = true;
            //}
            //else
            //{
            //    viewModel.CurrentGuiTile = null;
            //}

            // IsClickable and IsCurrentTile property is updated for all tiles.
            // TODO: Binding?
            foreach (var guiTile in viewModel.GuiTiles)
            {
                guiTile.IsClickable = guiTile.determineClickability(viewModel.CurrentGuiTile, viewModel.UserSelections, viewModel.GameBoard);
            }

        }

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
            Word.Text = "";
            viewModel.UserSelections.Clear();
            // TODO: redo clickability so it is updated automatically when user selections are cleared.
        }
    }
}

//TODO: Turning used letters into holes
//TODO: VM unit tests?
//TODO: Error handling
//TODO: Use solver to make sure moves remain
//TODO: Should user selection list be Observable Collection?
//TODO: "Submit" button - implement results
//TODO: More comments, use commentreflower
//TODO: Try /// comments?
//TODO: Make it so user can type the word
