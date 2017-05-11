using MoreLinq;
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
    /// Gameboard containing a collection of layered gridsquares.
    /// </summary>
    public class Board : INotifyPropertyChanged
    {
        /// <summary> Constructor. Creates the board's gridsquares.</summary>
        /// <param name="letterGrid"> A 3D array containing letters for tiles and spaces for holes. </param>
        public Board(char[,,] letterGrid)
        {
            if (letterGrid == null)
            {
                throw new ArgumentNullException("letterGrid cannot be null");
            }

            Rows = letterGrid.GetLength(0);
            Columns = letterGrid.GetLength(1);
            Layers = letterGrid.GetLength(2);

            if (Rows <= 0 || Columns <= 0 || Layers <= 0)
            {
                throw new ArgumentException("letterGrid dimensions must be positive");
            }

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    for (int z = 0; z < Layers; z++)
                    {
                        var currentLetter = letterGrid[x, y, z];
                        if (currentLetter == ' ')
                        {
                            Gridsquares.Add(new Hole(new Coordinates(x, y, z)));
                        }
                        else
                        {
                            Gridsquares.Add(new Tile(new Coordinates(x, y, z), currentLetter));
                        }
                    }
                }
            }
        }

        public int Rows { get; }

        public int Columns { get; }

        public int Layers { get; }

        /// <summary> A list of all gridsquares on the board. </summary>
        public List<Gridsquare> Gridsquares { get; } = new List<Gridsquare>();

        /// <summary> Find a gridsquare by its xy coordinates, using only the top layer of the board. </summary>
        public Gridsquare this[int x, int y]
        {
            get
            {
                return TopLayer.Find(g => g.Coords.X == x && g.Coords.Y == y);
            }
            set
            {
                var match = TopLayer.FindIndex(g => g.Coords.X == x && g.Coords.Y == y);
                TopLayer[match] = value;
            }
        }

        /// <summary> Find a gridsquare by its xyz coordinates. </summary>
        public Gridsquare this[int x, int y, int z]
        {
            get
            {
                return Gridsquares.Find(g => g.Coords.X == x && g.Coords.Y == y && g.Coords.Z == z);
            }
            set
            {
                var match = Gridsquares.FindIndex(g => g.Coords.X == x && g.Coords.Y == y && g.Coords.Z == z);
                Gridsquares[match] = value;
            }
        }

        /// <summary> Used for navigating the board.</summary>
        public enum Direction { North, South, East, West, Northeast, Northwest, Southeast, Southwest };

        /// <summary> A list of all gridsquares currently visible on the board. </summary>
        public List<Gridsquare> TopLayer
        {
            get
            {
                var topLayer = new List<Gridsquare>();

                for (int x = 0; x < Rows; x++)
                {
                    for (int y = 0; y < Columns; y++)
                    {
                        var tileStack = Gridsquares.FindAll(t => (t is Tile && t.Coords.X == x && t.Coords.Y == y));
                        if (tileStack.Any())
                        {
                            var topTile = tileStack.MaxBy(t => t.Coords.Z);
                            topLayer.Add(topTile);
                        }
                        else topLayer.Add(new Hole(new Coordinates(x, y, 0)));
                    }
                }
                return topLayer;
            }
        }

        /// <summary> A list of all tiles currently visible on the board (no holes). </summary>///
        public List<Tile> Tiles
        {
            get
            {
                List<Tile> tiles = new List<Tile>();
                foreach (var square in TopLayer)
                {
                    if (square is Tile)
                    {
                        tiles.Add(square as Tile);
                    }
                }
                return tiles;
            }
        }

        /// <summary> Returns a tile's neighboring tile or hole of a given compass direction. </summary>
        public Gridsquare GetNeighbor(Tile originTile, Direction direction)
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

                case Direction.Northeast:
                    x--;
                    y++;
                    break;

                case Direction.Northwest:
                    x--;
                    y--;
                    break;

                case Direction.Southeast:
                    x++;
                    y++;
                    break;

                case Direction.Southwest:
                    x++;
                    y--;
                    break;
            }

            //If the neighbor is outside of the board dimensions, return a hole
            if (x < 0 || x > Rows - 1 || y < 0 || y > Columns - 1)
            {
                return new Hole(new Coordinates(x, y, 0));
            }

            return this[x, y];
        }

        /// <summary> Converts a list of tiles to holes. </summary>
        public void ConvertTilesToHoles(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                int match = Gridsquares.FindIndex(g => g == tile);
                if (match >= 0) // tile was found in the list
                {
                    Gridsquares[match] = new Hole(tile.Coords);
                }
            }
            OnPropertyChanged("");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}