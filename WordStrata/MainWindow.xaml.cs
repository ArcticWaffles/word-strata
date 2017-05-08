﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using Core;
using System.Windows.Data;

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
            if (theSender.DataContext == BindingOperations.DisconnectedSource) return;
            Tile senderTile = (Tile)theSender.DataContext;
            viewModel.ThePath.Add(senderTile);
        }

        // User deselects a tile
        private void TileToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton theSender = (ToggleButton)sender;
            if (theSender.DataContext == BindingOperations.DisconnectedSource) return;
            Tile senderTile = (Tile)theSender.DataContext;
            viewModel.ThePath.Remove(senderTile);
        }

        // User submits a word
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // If the word is valid
            if (viewModel.CheckWord())
            {
                (Resources["AcceptWord"] as Storyboard).Begin();
                viewModel.FinishTurn();

                // If no more words remain on the board
                if(!viewModel.WordsRemain())
                {
                    (Resources["NoMoreWords"] as Storyboard).Begin();
                    // TODO: Disable all tiles
                }
            }
            // TODO - remove RejectWord animation. Scenario shouldn't be possible since word must be valid in order to submit.
            else // Word is not valid
            {
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

