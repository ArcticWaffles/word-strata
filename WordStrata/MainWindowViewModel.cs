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
    public class MainWindowViewModel : INotifyPropertyChanged, IMainWindowViewModel
    {
        public MainWindowViewModel(IGameModel theGameModel)
        {
            gameModel = theGameModel;
            foreach (var tile in GameBoard.Tiles)
            {
                GuiTiles.Add(new TileViewModel(tile, 0));
            }
        }

        IGameModel gameModel;

        public List<TileViewModel> GuiTiles { get; } = new List<TileViewModel>();

        public Board GameBoard
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
        public ObservableCollection<TileViewModel> UserSelections { get; set; } = new ObservableCollection<TileViewModel>();

        //The last tile in userSelections
        public TileViewModel CurrentTile
        {
            get
            {
                if (UserSelections != null && UserSelections.Count > 0)
                {
                    return UserSelections.Last();
                }
                else
                {
                    return null;
                }
            }

        }

        //The word the user is building
        private string userWord;
        public string UserWord
        {
            get
            {
                userWord = "";
                foreach (var tile in UserSelections)
                {
                    userWord += tile.TheTile.Letter;
                }
                return userWord;
            }

            set
            {
                if (value != userWord)
                {
                    userWord = value;
                    OnPropertyChanged("UserWord");
                }
            }

        }

            
        // User clicks a tile 
        public void ClickTile(TileViewModel tileVM)
        {
            UserSelections.Add(tileVM);
            OnPropertyChanged("UserWord");
            OnPropertyChanged("CurrentTile");
        }


        // User unclicks a tile: 
        public void UnclickTile(TileViewModel tileVM)
        {
            UserSelections.Remove(tileVM);
            OnPropertyChanged("UserWord");
            OnPropertyChanged("CurrentTile");
        }


        public void DetermineClickability()
        {
            foreach (var tile in GuiTiles)
            {
                tile.IsClickable = tile.IsNeighbor(CurrentTile, GameBoard) && (UserSelections.Contains(tile) == false) 
                    || tile == CurrentTile || UserSelections.Count == 0 || UserSelections == null;
            }
        }


        public bool CheckWord()
        {
            return Dictionary.Contains(UserWord);
        }


        public void ClearWord()
        {
            foreach (var tile in UserSelections.ToList())
            {
                UnclickTile(tile);
            }
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
