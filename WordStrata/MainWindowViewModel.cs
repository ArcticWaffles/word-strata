using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WordStrata.Solver;

namespace WordStrata
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            foreach (var tile in gameBoard.Tiles)
            {
                guiTiles.Add(new TileViewModel(tile, 0));
            }
        }

        private List<TileViewModel> guiTiles = new List<TileViewModel>();
        public List<TileViewModel> GuiTiles
        {
            get
            {
                return guiTiles;
            }
        }

        private TileViewModel currentGuiTile;
        public TileViewModel CurrentGuiTile
        {
            get
            {
                return currentGuiTile;
            }

            set
            {
                if (value != currentGuiTile)
                {
                    currentGuiTile = value;
                    OnPropertyChanged("CurrentGuiTile");
                }
            }
        }

        private string userWord = "";
        public string UserWord
        {
            get
            {
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

        private Board gameBoard = BoardGenerator.generateWeightedBoard(5, 5);

        public Board GameBoard
        {
            get
            {
                return gameBoard;
            }
        }

        //List of tiles the user has clicked, in sequential order
        private List<TileViewModel> userSelections = new List<TileViewModel>();
        public List<TileViewModel> UserSelections
        {
            get
            {
                return userSelections;
            }

            set
            {
                userSelections = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }

}
