using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using Core;
using Solve;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WordStrata
{
    public class MainWindowViewModel : MainWindowViewModelBase, INotifyPropertyChanged 
    {
        public MainWindowViewModel(GameModel theGameModel)
        {
            gameModel = theGameModel;
        }

        GameModel gameModel;

        public override Board GameBoard
        {
            get {return gameModel.GameBoard;}
        }

        public int Rows
        {
            get {return gameModel.GameBoard.Rows;}
        }

        public int Columns
        {
            get {return gameModel.GameBoard.Columns;}
        }


        public HashSet<String> Dictionary
        {
            get {return gameModel.Dictionary;}
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

        List<Snake> snakes = new List<Snake>();
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
                UserWord = UserWord.Remove(UserWord.Length-1);
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
            //var pathGroups = Paths.GroupPaths();
            //int i = 0;
            //foreach (var group in pathGroups)
            //{
            //    foreach (var path in group)
            //    {
            //        double location = (1 / (double)(group.Count + 1)) * (i + 1);
            //        allSnakes.Add(new Snake(location, path));
            //        i++;
            //    }
            //}
            var colors = Snake.makeUniqueColors(Paths.Count);
            int i = 0;
            // Creates a snake for each path.
            foreach (var path in Paths)
            {
                double location = (1.0 / (Paths.Count + 1.0)) * (i + 1);
                allSnakes.Add(new Snake(location, path, new SolidColorBrush(colors[i])));
                i++;
            }
            foreach (var snake in allSnakes)
            {
                foreach (var tile in snake.Path)
                {
                    //var endCoordsX = 100 * ((double)tile.Coords.Y / Columns) + snake.LocationOnTile * (1 / (double)Columns);
                    //var endCoordsY = 100 * ((double)tile.Coords.X / Rows) + snake.LocationOnTile * (1 / (double)Rows);
                    var endCoordsX = 100 * ((double)tile.Coords.Y / Columns + .5 / (double)Columns);
                    var endCoordsY = 100 * ((double)tile.Coords.X / Rows + .5 / (double)Rows);
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
