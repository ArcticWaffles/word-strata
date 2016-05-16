using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Board
    {
        private GridSquare[,] gridSquares;
        public enum Direction {North, South, East, West};

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
                        gridSquares[x, y] = new Tile(new Coordinates(x, y), currentLetter, false);
                    }
                }
            }
        }

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

            if (x < 0 || x > gridSquares.GetLength(0)-1 || y < 0 || y > gridSquares.GetLength(1)-1)
            {
                return new Hole(new Coordinates(x, y));
            }
            return gridSquares[x,y];
        }


    }
}
