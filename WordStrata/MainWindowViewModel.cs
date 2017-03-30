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

        /// <summary> Number of rows on the gameboard. </summary>
        public int Rows
        {
            get { return gameModel.GameBoard.Rows; }
        }

        /// <summary> Number of columns on the gameboard. </summary>
        public int Columns
        {
            get { return gameModel.GameBoard.Columns; }
        }

        /// <summary> Determines a word's validity. </summary>
        public HashSet<String> Dictionary
        {
            get { return gameModel.Dictionary; }
        }

        /// <summary> Word the user is building. </summary>
        private string userWord = "";
        public string UserWord
        {
            get { return userWord; }

            set
            {
                if (value == userWord) return;
                userWord = value;
                OnPropertyChanged("UserWord");
            }
        }

        //Current path of clicked tiles
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

        private Snake currentSnake = new Snake();
        public Snake CurrentSnake
        {
            get { return currentSnake; }
            set { if (currentSnake == value) return; currentSnake = value; OnPropertyChanged("CurrentSnake"); }
        }

        // User selects a tile
        public void AddTile(Tile theTile)
        {
            ThePath.Add(theTile);
        }

        // User deselects a tile
        public void RemoveTile(Tile theTile)
        {
            ThePath.Remove(theTile);
        }

        // Checks user-submitted word against the dictionary.
        public bool CheckWord()
        {
            return Dictionary.Contains(UserWord);
        }

        // Clears UserWord, thereby clearing the textblock and the gameboard.
        public void ClearWord()
        {
            ThePath.Clear();
        }

        public void BuildSnake()
        {
            if (ThePath.Any())
            {
                var theSnake = new Snake();
                // Add points
                foreach (var tile in ThePath)
                {
                    var endCoordsX = 100 * ((double)tile.Coords.Y / Columns + .5 / Columns);
                    var endCoordsY = 100 * ((double)tile.Coords.X / Rows + .5 / Rows);
                    theSnake.Points.Add(new Point(endCoordsX, endCoordsY));
                }
                CurrentSnake = theSnake;
            }
            else CurrentSnake = new Snake(); // ThePath is empty
        }

        /// <summary>
        /// Turns used tiles to holes and clears word.
        /// </summary>
        public void FinishTurn()
        {
            GameBoard.ConvertTilesToHoles(ThePath.ToList());
            ClearWord();
        }

        public bool WordsRemain()
        {
            return Solver.AnyWordExistsOnBoard(Dictionary, GameBoard);
        }

        public bool EnableSubmit
        {
            get
            {
                return !ThePath.Any();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UserWord = Solver.GetLetters(ThePath.ToList());
            BuildSnake();
            OnPropertyChanged("ThePath");
        }

    }
}

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one