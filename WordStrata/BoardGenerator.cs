using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WordStrata
{
    public static class BoardGenerator
    {
        public static Board generateWeightedBoard(int rows, int columns)
        {
            Random letterGenerator = new Random();
            char[,] letterGrid = new char[rows, columns];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    letterGrid[x, y] = generateWeightedLetter(letterGenerator.Next(0, 100));
                }
            }
            Board newBoard = new Board(letterGrid);
            return newBoard;
        }

        private static char generateWeightedLetter(int random)
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

