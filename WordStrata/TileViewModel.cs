using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata.Solver;


namespace WordStrata
{
    public class TileViewModel : INotifyPropertyChanged
    {
        public TileViewModel(Tile tile, int depth)
        {
            TheTile = tile;
            Depth = depth;
        }

        public Tile TheTile { get; }

        public int Depth { get; set; }

        private bool isClicked;
        public bool IsClicked
        {
            get
            {
                return (isClicked);
            }

            set
            {
                if (value != isClicked)
                {
                    isClicked = value;
                    OnPropertyChanged("IsClicked");
                }
            }
        }

        private bool isCurrentTile;
        public bool IsCurrentTile
        {
            get
            {
                return (isCurrentTile);
            }

            set
            {
                if (value != isCurrentTile)
                {
                    isCurrentTile = value;
                    OnPropertyChanged("IsCurrentTile");
                }
            }
        }
        //set change notification, see if being utilized in xaml.cs

        private bool isClickable = true;
        public bool IsClickable
        {
            get
            {
                return (isClickable);
            }

            set
            {
                if (value != isClickable)
                {
                    isClickable = value;
                    OnPropertyChanged("IsClickable");
                }
            }
        }

        public bool determineClickability(TileViewModel officialCurrentTile, List<TileViewModel> userSelections, Board board)
        {
            return IsNeighbor(officialCurrentTile, board) && (userSelections.Contains(this) == false) || IsCurrentTile == true || userSelections.Count == 0 || userSelections == null;
        }


        public bool IsNeighbor(TileViewModel otherTile, Board theBoard)
        {
            if (otherTile == null || theBoard == null)
            {
                return false;
            }
            else
            {
                List<GridSquare> neighbors = theBoard.getAllNeighbors(TheTile, theBoard);
                return (neighbors.Contains((otherTile.TheTile as GridSquare)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
