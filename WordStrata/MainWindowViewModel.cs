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
            foreach (var tile in GameBoard.Tiles)
            {
                GuiTiles.Add(new TileViewModel(tile, ClickTile, TileIsChecked, TileIsClickable));
            }
        }

        IGameModel gameModel;

        public override List<TileViewModel> GuiTiles { get; } = new List<TileViewModel>();

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
        public override UserTileSelections UserSelections { get; set; } = new UserTileSelections();


        //User clicks a tile
        //public void ClickTile(TileViewModel tileVM)
        //{
        //    UserSelections.Selections.Add(tileVM);
        //    OnPropertyChanged("UserWord");
        //    OnPropertyChanged("CurrentTile");
        //}


        //User unclicks a tile: 
        //public void UnclickTile(TileViewModel tileVM)
        //{
        //    UserSelections.Selections.Remove(tileVM);
        //    OnPropertyChanged("UserWord");
        //    OnPropertyChanged("CurrentTile");
        //}


        //public void DetermineClickability()
        //{
        //    foreach (var tile in GuiTiles)
        //    {
        //        tile.IsClickable = tile.IsNeighbor(UserSelections.CurrentTile, GameBoard) && (UserSelections.Selections.Contains(tile) == false) 
        //            || tile == UserSelections.CurrentTile || UserSelections.Selections.Count == 0 || UserSelections == null;
        //    }
        //}


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

// TODO: Resolve slang use of GuiTile for TileViewModel

// TODO retrieving saved game - make new MWVM but pass in existing game model instead of new one
