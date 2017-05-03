using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WordStrata
{
    /// <summary>
    /// Creates a gameboard.
    /// </summary>
    public static class BoardGenerator
    {
        /// <summary> Creates a board in which all gridsquares are tiles. </summary>
        public static Board generateRectangularBoard(int rows, int columns, int layers)
        {
            return weightedBoard(rows, columns, layers);
        }

        /// <summary> Creates a board in which some gridsquares are holes. Used for creating irregular shapes. </summary>
        // TODO: Make parameter a flat list of coordinates and then black out each layer beneath it, or keep fine-grained control?
        public static Board generateShapedBoard(int rows, int columns, int layers, List<Coordinates> holes)
        {
            var startingBoard = weightedBoard(rows, columns, layers);
            foreach(var coords in holes)
            {
                startingBoard[coords.X, coords.Y, coords.Z] = new Hole(coords);
            }
            return startingBoard;
        }

        /// <summary> Creates a board in which the letter selection is weighted according to frequency of use ("Scrabble style") rather than random. </summary>
        private static Board weightedBoard(int rows, int columns, int layers)
        {
            Random letterGenerator = new Random();
            char[,,] letterGrid = new char[rows, columns, layers];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    for (int z = 0; z < layers; z++)
                    {
                        letterGrid[x, y, z] = weightedLetter(letterGenerator.Next(0, 100));
                    }
                }
            }
            Board newBoard = new Board(letterGrid);
            return newBoard;
        }

        private static char weightedLetter(int random)
        {
            var dictionary = new Dictionary<int, char>()
            {
             {9 , 'A' },
             {11 , 'B' },
             {13 , 'C' },
             {17 , 'D' },
             {29 , 'E' },
             {31 , 'F' },
             {34 , 'G' },
             {36 , 'H' },
             {45 , 'I' },
             {46 , 'J' },
             {47 , 'K' },
             {51 , 'L' },
             {54 , 'M' },
             {60 , 'N' },
             {68 , 'O' },
             {70 , 'P' },
             {71 , 'Q' },
             {77 , 'R' },
             {82 , 'S' },
             {88 , 'T' },
             {92 , 'U' },
             {94 , 'V' },
             {96 , 'W' },
             {97 , 'X' },
             {99 , 'Y' },
             {100 , 'Z' },
            };
            var key = dictionary.First(x=> random < x.Key);
            return key.Value;
        }
    }
}

