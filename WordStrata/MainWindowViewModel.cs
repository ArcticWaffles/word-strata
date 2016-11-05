using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WordStrata.Solve;

namespace WordStrata
{
    public class MainWindowViewModel : INotifyPropertyChanged, IMainWindowViewModel
    {
        public MainWindowViewModel(HashSet<string> dictionary)
        {
            GameBoard = BoardGenerator.generateWeightedBoard(5, 5);
            solver = new Solver(dictionary, GameBoard);
            Dictionary = dictionary;

            foreach (var tile in GameBoard.Tiles)
            {
                GuiTiles.Add(new TileViewModel(tile, 0));
            }
        }

        public List<TileViewModel> GuiTiles { get; } = new List<TileViewModel>();

        public Board GameBoard { get; }

        private Solver solver;

        public HashSet<String> Dictionary { get; }

        //List of tiles the user has clicked, removed when they are unclicked
        public List<TileViewModel> UserSelections { get; set; } = new List<TileViewModel>();

        //The last tile in userSelections
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

        //The word the user is building
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }

}
