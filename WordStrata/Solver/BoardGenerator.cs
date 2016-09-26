using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solver
{
    public static class BoardGenerator
    {

        public static Board RandomBoardGenerator(int rows, int columns)
        {
            Random letterGenerator = new Random();
            char[,] letterGrid = new char[rows, columns];
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    letterGrid[x, y] = Convert.ToChar(letterGenerator.Next(65, 91));
                }
            }
            Board newBoard = new Board(letterGrid);
            return newBoard;
        }
    }
}

