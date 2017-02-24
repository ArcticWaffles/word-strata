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
            get
            {
                return gameModel.GameBoard;
            }
        }

        public HashSet<String> Dictionary
        {
            get
            {
                return gameModel.Dictionary;
            }
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
                OnPropertyChanged("Paths");
                OnPropertyChanged("EnableSubmit");
            }
        }

        List<Snake> snakes;
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


        public bool CheckWord()
        {
            return Dictionary.Contains(UserWord);
        }


        public void ClearWord()
        {
            UserWord = "";
        }

        public void CreateSnakes(System.Windows.Controls.ItemsControl control)
        {
            var allSnakes = new List<Snake>();
            var pathGroups = Paths.OrganizePaths();
            int i = 0;
            foreach (var group in pathGroups)
            {
                foreach (var path in group)
                {
                    var location = (1 / (group.Count + 1)) * (i + 1);
                    allSnakes.Add(new Snake(location, path));
                    i++;
                }
            }
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
