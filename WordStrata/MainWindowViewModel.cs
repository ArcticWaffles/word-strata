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
        /// Turns used tiles to holes and clears word.
        /// </summary>
        public void FinishTurn()
        {
            GameBoard.ConvertTilesToHoles(UserSelections.Selections.ToList());
            ClearWord();
        }

        public bool WordsRemain()
        {
            return Solver.AnyWordExistsOnBoard(Dictionary, GameBoard);
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
