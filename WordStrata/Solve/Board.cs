using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    public class Board : INotifyPropertyChanged
    {
        //A gridsquare is a specific square on the board and is either a tile or a hole. 
        //gridSquares is a 2D array representing all the tiles and holes on the board.
        private GridSquare[,] gridSquares;

        public List<GridSquare> GridSquares
        {
            get
            {
                var squares = new List<GridSquare>();
                for (int x = 0; x < gridSquares.GetLength(0); x++)
                {
                    for (int y = 0; y < gridSquares.GetLength(1); y++)
                    {
                        squares.Add(gridSquares[x, y]);
                    }
                }
                return squares;
            }
        }

        //Used for navigating the board
        public enum Direction { North, South, East, West };

        //Returns all the tiles (no holes) on the board as a list. Used for solving.
        public List<Tile> Tiles
        {
            get
            {
                List<Tile> tiles = new List<Tile>();
                for (int x = 0; x < gridSquares.GetLength(0); x++)
                {
                    for (int y = 0; y < gridSquares.GetLength(1); y++)
                    {
                        if (gridSquares[x, y] is Tile)
                            tiles.Add(gridSquares[x, y] as Tile);
                    }
                }
                return tiles;
            }
        }


        //Board constructor. letterGrid is a 2D array representing all the letters on the tiles.
        //A hole has a space character.
        public Board(char[,] letterGrid)
        {
            if (letterGrid == null)
            {
                throw new ArgumentNullException("letterGrid cannot be null");
            }

            if (letterGrid.GetLength(0) == 0 || letterGrid.GetLength(1) == 0)
            {
                throw new ArgumentException("letterGrid dimensions cannot be zero");
            }
            gridSquares = new GridSquare[letterGrid.GetLength(0), letterGrid.GetLength(1)];
            for (int x = 0; x < letterGrid.GetLength(0); x++)
            {
                for (int y = 0; y < letterGrid.GetLength(1); y++)
                {
                    var currentLetter = letterGrid[x, y];
                    if (currentLetter == ' ')
                    {
                        gridSquares[x, y] = new Hole(new Coordinates(x, y));
                    }
                    else
                    {
                        gridSquares[x, y] = new Tile(new Coordinates(x, y), currentLetter);
                    }
                }
            }
        }

        //Returns a tile's neighboring tile or hole of a given compass direction.
        public GridSquare GetNeighbor(Tile originTile, Direction direction)
        {
            if (originTile == null)
            {
                throw new ArgumentNullException("originTile cannot be null");
            }

            int x = originTile.Coords.X;
            int y = originTile.Coords.Y;

            switch (direction)
            {
                case Direction.North:
                    x--;
                    break;
                case Direction.East:
                    y++;
                    break;
                case Direction.South:
                    x++;
                    break;
                case Direction.West:
                    y--;
                    break;
            }

            //If the neighbor is outside of the board dimensions, returns a hole
            if (x < 0 || x > gridSquares.GetLength(0) - 1 || y < 0 || y > gridSquares.GetLength(1) - 1)
            {
                return new Hole(new Coordinates(x, y));
            }
            return gridSquares[x, y];
        }

        public void ConvertTilesToHoles(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                gridSquares[tile.Coords.X, tile.Coords.Y] = new Hole(tile.Coords);
                OnPropertyChanged(null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(null);
        }

        // TODO: Get rid of? Not being used anymore
        //Returns all of a tile's neighboring tiles or holes
        public List<GridSquare> getAllNeighbors(Tile originTile, Board theBoard)
        {
            List<GridSquare> allNeighbors = new List<GridSquare>();
            allNeighbors.Add(theBoard.GetNeighbor(originTile, Board.Direction.North));
            allNeighbors.Add(theBoard.GetNeighbor(originTile, Board.Direction.East));
            allNeighbors.Add(theBoard.GetNeighbor(originTile, Board.Direction.South));
            allNeighbors.Add(theBoard.GetNeighbor(originTile, Board.Direction.West));

            return allNeighbors;
        }
    }
}
