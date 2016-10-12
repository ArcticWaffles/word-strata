using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WordStrata.Solve;

namespace WordStrata
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            GameBoard = BoardGenerator.generateWeightedBoard(5, 5);
            Dictionary = loadDictionary();
            solver = new Solver(Dictionary, GameBoard);

            foreach (var tile in GameBoard.Tiles)
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


        public Board GameBoard { get; }

        private Solver solver;

        public HashSet<string> Dictionary { get; }

        private HashSet<string> loadDictionary()
        {
            var list = new HashSet<string>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader("sowpods.txt");
            while ((line = file.ReadLine()) != null)
            {
                list.Add(line);
            }
            file.Close();
            return list;
        }

        //List of tiles the user has clicked, removed when they are unclicked
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
