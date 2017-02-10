using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
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
                UserWord.Remove(UserWord.Last());
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

        public bool CheckKeyboardEntry(KeyEventArgs args)
        {
            // Let user type any letter. Check word after each char.
            // add contents of textbox + new letter to userword. find specific word on board. 
            // if 0 lists exist, clear user selections (unclick all tiles)
            // if > 0 lists exist, enable submit button. 
            // if 1 list exists, update usertileselections. 
            // if > 1 list exists, add alternate paths to usertileselections? 

            // TODO: Change User selections to observable collection class. Selections as the collection (of tile lists)
            
            throw new NotImplementedException();
        }
    }

}

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one
