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
        public TileViewModel CurrentGuiTile
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
        public string UserWord
        {
            get
            {
                gameModel.UserWord = "";
                foreach (var tile in UserSelections)
                {
                    gameModel.UserWord += tile.TheTile.Letter;
                }
                return gameModel.UserWord;
            }

            set
            {
                OnPropertyChanged("UserWord");
                gameModel.UserWord = value;
            }
            //set
            //{
            //    //if (value != gameModel.UserWord)
            //    //{
            //        gameModel.UserWord = value;
            //    //    OnPropertyChanged("UserSelections");
            //    //}
            //}
        }

            
        // User clicks a tile: Letter is added to UserWord. Tile is added to the
        // UserSelections list and its properties are updated. 

        public void ClickTile(TileViewModel tileVM)
        {
            //UserWord += tileVM.TheTile.Letter;
            UserSelections.Add(tileVM);
            gameModel.UserSelections.Add(tileVM);
            OnPropertyChanged("UserWord");
            OnPropertyChanged("CurrentGuiTile");
            tileVM.IsClicked = true;
        }

        // User unclicks a tile: Letter is removed from UserWord. Tile is
        // removed from the UserSelections list and its properties are updated.
        public void UnclickTile(TileViewModel tileVM)
        {
            //UserWord = UserWord.Remove(UserWord.Length - 1);
            UserSelections.Remove(tileVM);
            gameModel.UserSelections.Remove(tileVM);
            OnPropertyChanged("UserWord");
            OnPropertyChanged("CurrentGuiTile");
            tileVM.IsClicked = false;
        }

        public bool CheckWord()
        {
            return Dictionary.Contains(UserWord);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }

}
 