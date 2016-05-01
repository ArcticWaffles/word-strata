using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Solver
    {
        string[] dictionary;
        char[,] board;
        StringBuilder theWord;

        public Solver(string[] dictionary, char[,] board)
        {
            if (dictionary.Length == 0)
            {
                throw new ArgumentException("Dictionary is empty");
            }
            if (board.GetLength(0) == 0 || board.GetLength(1) == 0)
            {
                throw new ArgumentException("Invalid board dimensions: " + board.GetLength(0) + ", " + board.GetLength(1));
            }

            this.dictionary = dictionary;
            this.board = board;
        }



        //public bool WordExists()
        //{
        //}
    }
}
