using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Gameboard containing a collection of gridsquares.
    /// </summary>
    public class Board : INotifyPropertyChanged
    {
        /// <summary> Constructor. Creates the board's gridsquares.</summary>
        /// <param name="letterGrid"> A 2D array containing letters for tiles and spaces for holes. </param>
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


        /// <summary> A 2D array containing all gridsquares on the board. </summary>
        private GridSquare[,] gridSquares;


        /// <summary> A list of all gridsquares on the board. </summary>
        public List<GridSquare> GridSquares
            // TODO: Make this a custom IEnumerable? Or create a private list field.
            // A list is used for getting the board's gridsquares (rather than a 2D array) for data binding purposes.
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


        /// <summary> Used for navigating the board.</summary>
        public enum Direction { North, South, East, West };


        /// <summary> A list of all tiles on the board (no holes). </summary>
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


        /// <summary>
        /// Returns a tile's neighboring tile or hole of a given compass direction.
        /// </summary>
        /// <param name="originTile">Tile whose neighbor is returned.</param>
        /// <param name="direction">Compass direction of the desired neighbor.</param>
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

            //If the neighbor is outside of the board dimensions, return a hole
            if (x < 0 || x > gridSquares.GetLength(0) - 1 || y < 0 || y > gridSquares.GetLength(1) - 1)
            {
                return new Hole(new Coordinates(x, y));
            }

            return gridSquares[x, y];
        }


        /// <summary> Converts a list of tiles to holes. </summary>
        public void ConvertTilesToHoles(Path tiles)
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
    }
}
