﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Board
    {
        private char[,] letterGrid;

        public List<Tile> Tiles
        {
            get
            {
                var tileList = new List<Tile>();
                for (int x = 0; x < letterGrid.GetLength(0); x++)
                {
                    for (int y = 0; y < letterGrid.GetLength(1); y++)
                    {
                        var currentLetter = letterGrid[x, y];
                        if (currentLetter != ' ')
                        {
                            tileList.Add(new Tile(new Coordinates(x,y), currentLetter, false));
                        }
                    }
                }
                return tileList;
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
            this.letterGrid = letterGrid;
            
        }

        public Tile GetNeighbor(Tile originTile, char direction)
        {
            if (originTile == null)
            {
                throw new ArgumentNullException("originTile cannot be null");
            }

            int x = originTile.Coords.X;
            int y = originTile.Coords.Y;

            switch (direction)
            {
                case 'n':
                    x--;
                    break;
                case 'e':
                    y++;
                    break;
                case 's':
                    x++;
                    break;
                case 'w':
                    y--;
                    break;
            }

            var neighbor = Tiles.Find(Tile => (Tile.Coords.X == x && Tile.Coords.Y == y));
            return neighbor;
        }


    }
}
