using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WordStrata.Solve;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

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

        //List of tiles the user has clicked, removed when they are unclicked
        private UserTileSelections userSelections = new UserTileSelections();
        public override UserTileSelections UserSelections
        {
            get
            {
                return userSelections;
            }

            set
            {
                // TODO: Will this property changed be used?
                if (value != userSelections)
                {
                    userSelections = value;
                    OnPropertyChanged(null);
                }
            }
        }


        //User clicks a tile
        public void ClickTile(Tile theTile)
        {
            UserSelections.Selections.Add(theTile);
        }


        // User unclicks a tile: 
        public void UnclickTile(Tile theTile)
        {
            UserSelections.Selections.Remove(theTile);
        }


        public bool CheckWord()
        {
            return Dictionary.Contains(UserSelections.UserWord);
        }


        public void ClearWord()
        {
            UserSelections.Selections.Clear();
        }

        /// <summary>
        /// Turns used tiles to holes, clears word, and checks that further moves remain.
        /// </summary>
        public void FinishTurn()
        {
            GameBoard.ConvertTilesToHoles(UserSelections.Selections.ToList());
            ClearWord();
            if(!Solver.AnyWordExistsonBoard(Dictionary, GameBoard))
            {
                // TODO: Notify user no more words remain. 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CheckLetter(Key key)
        {
            KeyConverter kc = new KeyConverter();
            string keyChar = kc.ConvertToString(key);

            foreach (var tile in gameModel.GameBoard.Tiles)
            {
                if (keyChar == tile.Letter.ToString())
                {
                    // TODO: Decide how to do this part. New method in solver. Let the
                    // user type any letter instead of checking the input? And
                    // highlight the possible paths or for now just have a list
                    // on the side? Check upon submit button click or
                    // continually check and flash red when they start typing
                    // non-existent letters?
                }
            }
            throw new NotImplementedException();
        }
    }

}

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one
