﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Solver
    {
        string[] dictionary;
        Board board;
        StringBuilder theWord;

        public Solver(string[] dictionary, Board board)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("Dictionary cannot be null");
            }

            if (dictionary.Length == 0)
            {
                throw new ArgumentException("Dictionary cannot be empty");
            }

            if (board == null)
            {
                throw new ArgumentNullException("Board cannot be null");
            }

            this.dictionary = dictionary;
            this.board = board;
        }

        public void MarkAndAppend(Tile tile, StringBuilder theWord)
        {
            if (tile == null)
            {
                throw new ArgumentNullException("Tile cannot be null");
            }
            if (theWord == null)
            {
                throw new ArgumentNullException("theWord cannot be null");
            }
            if (tile.HasMark == true)
            {
                throw new ArgumentException("Tile should be unmarked");
            }
                
            tile.Mark();
            theWord.Append(tile.Letter);
        }

        public void UnmarkAndRemove(Tile tile, StringBuilder theWord)
        {
            if (tile == null)
            {
                throw new ArgumentNullException("Tile cannot be null");
            }
            if (theWord == null)
            {
                throw new ArgumentNullException("theWord cannot be null");
            }
            if (tile.HasMark == false)
            {
                throw new ArgumentException("Tile should be marked");
            }
            tile.Unmark();
            if (theWord.Length < 1)
            {
                throw new ArgumentException("theWord cannot be empty");
            }
            theWord.Remove((theWord.Length - 1), 1);
        }




        //public bool WordExists()
        //{
        //}
    }
}
