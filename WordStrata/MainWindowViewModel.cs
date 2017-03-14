using Core;
using Solve;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WordStrata
{
    public class MainWindowViewModel : MainWindowViewModelBase, INotifyPropertyChanged
    {
        public MainWindowViewModel(GameModel theGameModel)
        {
            gameModel = theGameModel;
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

        public HashSet<String> Dictionary
        {
            get { return gameModel.Dictionary; }
        }

        /// <summary> Word the user is building. </summary>
        private string userWord = "";
        public string UserWord
        {
            get
            {
                return userWord;
            }

            set
            {
                userWord = value;
                var paths = new UserPaths();
                var lists = Solver.SpecificWordExistsOnBoard(userWord, GameBoard);
                foreach (var list in lists)
                {
                    paths.Add(list);
                }
                Paths = paths;
                CreateSnakes();
                OnPropertyChanged("UserWord");
            }
        }

        //Current valid tile paths based on UserWord
        private UserPaths paths = new UserPaths();
        public override UserPaths Paths
        {
            get { return paths; }
            set
            {
                if (value == paths) return;
                paths = value;
                OnPropertyChanged("");
            }
        }

        private List<Snake> snakes = new List<Snake>();
        public List<Snake> Snakes
        {
            get { return snakes; }
            set { if (snakes == value) return; snakes = value; OnPropertyChanged("Snakes"); }
        }

        //User clicks a tile
        public void ClickTile(Tile theTile)
        {
            UserWord += (theTile.Letter);
        }

        // User unclicks a tile
        public void UnclickTile(Tile theTile)
        {
            if (UserWord.Length > 0)
            {
                UserWord = UserWord.Remove(UserWord.Length - 1);
            }
        }

        // Checks user-submitted word against the dictionary.
        public bool CheckWord()
        {
            return Dictionary.Contains(UserWord);
        }

        // Clears UserWord, thereby clearing the input text box and the gameboard.
        public void ClearWord()
        {
            UserWord = "";
        }

        public void CreateSnakes()
        {
            var allSnakes = new List<Snake>();
            var colors = Snake.makeUniqueColors(Paths.Count);
            int colorCounter = 0;

            if (Paths.Count == 0)
            {
                Snakes = new List<Snake>();
                return;
            }
            else if (Paths.Count == 1)
            {
                allSnakes.Add(new Snake(.5, Paths[0], new SolidColorBrush(colors[0])));
            }
            else // More than one path exists
            {
                // Creates a snake for each path.
                List<SortedSet<Path>> groupedPaths = Paths.GroupPaths();
                foreach (var group in groupedPaths)
                {
                    int i = 0;
                    foreach (var path in group)
                    {
                        double location = (1.0 / (group.Count + 1.0)) * (i + 1);
                        allSnakes.Add(new Snake(location, path, new SolidColorBrush(colors[colorCounter])));
                        i++;
                        colorCounter++;
                    }
                }
            }

            // Assigns Points to each snake
            foreach (var snake in allSnakes)
            {
                foreach (var tile in snake.Path)
                {
                    var endCoordsX = 100 * ((double)tile.Coords.Y / Columns + snake.LocationOnTile / Columns);
                    var endCoordsY = 100 * ((double)tile.Coords.X / Rows + snake.LocationOnTile / Rows);
                    // var endCoordsX = 100 * ((double)tile.Coords.Y / Columns + .5 / (double)Columns);
                    // var endCoordsY = 100 * ((double)tile.Coords.X / Rows + .5 / (double)Rows);
                    snake.Points.Add(new Point(endCoordsX, endCoordsY));
                }
            }
            Snakes = allSnakes;
        }

        /// <summary>
        /// Turns used tiles to holes and clears word.
        /// </summary>
        public void FinishTurn()
        {
            GameBoard.ConvertTilesToHoles(Paths[0]);
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
                return !Paths.IsEmpty();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one