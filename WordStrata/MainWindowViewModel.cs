using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WordStrata.Solve;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WordStrata
{
    public class MainWindowViewModel : MainWindowViewModelBase, INotifyPropertyChanged 
    {
        public MainWindowViewModel(IGameModel theGameModel)
        {
            gameModel = theGameModel;
        }

        IGameModel gameModel;

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


        //User unclicks a tile: 
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
// TODO 11/10: Make utility? Clean up. Think about taking out VM storage. Modify
// unit tests - add game model, move MWVM where appropriate, add more MWVM

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one
