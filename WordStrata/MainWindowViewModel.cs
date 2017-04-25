using Core;
using Solve;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WordStrata
{
    public class MainWindowViewModel : MainWindowViewModelBase, INotifyPropertyChanged
    {
        public MainWindowViewModel(GameModel theGameModel)
        {
            gameModel = theGameModel;
            ThePath.CollectionChanged += OnCollectionChanged;
        }

        private GameModel gameModel;

        public override Board GameBoard
        {
            get { return gameModel.GameBoard; }
        }

        public int Rows
        {
            get { return gameModel.GameBoard.Rows; }
        }

        public int Columns
        {
            get { return gameModel.GameBoard.Columns; }
        }

        /// <summary> Used for determining a word's validity. </summary>
        public HashSet<String> Dictionary
        {
            get { return gameModel.Dictionary; }
        }

        /// <summary> Word the user is building. </summary>
        public string UserWord
        {
            get
            {
                return Solver.GetLetters(ThePath.ToList());
            }
        }

        /// <summary> Current path of clicked tiles. </summary>
        private TilePath thePath = new TilePath();
        public override TilePath ThePath
        {
            get { return thePath; }
            set
            {
                if (value == thePath) return;
                thePath = value;
                OnPropertyChanged("ThePath");
            }
        }

        /// <summary> Visually traces the current TilePath. </summary>
        public Snake CurrentSnake
        {
            get
            {
                Snake currentSnake = new Snake();
                if (ThePath.Any())
                {
                    // Translates built-in tile coordinates to GUI coordinates
                    foreach (var tile in ThePath)
                    {
                        var guiCoordsX = 100 * ((double)tile.Coords.Y / Columns + .5 / Columns);
                        var guiCoordsY = 100 * ((double)tile.Coords.X / Rows + .5 / Rows);
                        currentSnake.Points.Add(new Point(guiCoordsX, guiCoordsY));
                    }
                }
                return currentSnake;
            }
        }

        /// <summary> Checks user word against the dictionary. </summary>
        public bool CheckWord()
        {
            return Dictionary.Contains(UserWord);
        }

        /// <summary> Clears UserWord, thereby clearing the textblock and the gameboard. </summary>
        public void ClearWord()
        {
            ThePath.Clear();
        }

        /// <summary> Turns used tiles to holes and clears word. </summary>
        public void FinishTurn()
        {
            GameBoard.ConvertTilesToHoles(ThePath.ToList());
            ClearWord();
        }

        /// <summary> Determines if any valid words remain on the board. </summary>
        public bool WordsRemain()
        {
            return Solver.AnyWordExistsOnBoard(Dictionary, GameBoard);
        }

        /// <summary> Is true only if a valid word is selected. </summary>
        public bool EnableSubmit
        {
            get
            {
                return (CheckWord());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ThePath");
            OnPropertyChanged("UserWord");
            OnPropertyChanged("CurrentSnake");
            OnPropertyChanged("EnableSubmit");
        }
    }
}

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one