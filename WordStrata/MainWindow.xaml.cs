﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using Core;

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

        // User selects a tile
        private void TileToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton theSender = (ToggleButton)sender;
            Tile senderTile = (Tile)theSender.DataContext;
            viewModel.AddTile(senderTile);
        }

        // User deselects a tile
        private void TileToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton theSender = (ToggleButton)sender;
            Tile senderTile = (Tile)theSender.DataContext;
            viewModel.RemoveTile(senderTile);
        }

        // User submits a word
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // If more than one path exists
            if (viewModel.ThePath.Count > 1)
            {
                // Prompt user
                (Resources["PickAPath"] as Storyboard).Begin();
                // Freeze tile clicking (modify isClickable converter to disallow all when ___(snakesEnabled) OR UserPaths is empty so it works anyway?
                // Enable snake clicking and click handler? Or color-based picker.
                // Upon clicking a snake, clear all UserPaths but the one, enable tile clicking (auto), and call SubmitButton_Click again
                // Return
            }

            // If the word is valid
            else if (viewModel.CheckWord())
            {
                // WordFeedback.Foreground = Brushes.Green;
                (Resources["AcceptWord"] as Storyboard).Begin();
                viewModel.FinishTurn();

                // If no more words remain on the board
                if(!viewModel.WordsRemain())
                {
                    (Resources["NoMoreWords"] as Storyboard).Begin();
                }

            }
            else // Word is not valid
            {
                //WordFeedback.Foreground = Brushes.Red;
                (Resources["RejectWord"] as Storyboard).Begin();
            }
        }

        // User clears the word box
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearWord();
        }
    }
}

