using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Solver
    {
        List<string> dictionary;
        Board board;
        StringBuilder theWord = new StringBuilder("");

        public Solver(List<string> dictionary, Board board)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("Dictionary cannot be null");
            }

            if (dictionary.Count == 0)
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

        public bool WordExistsFromStartingTile(Tile startingTile, int maxDepth)
        {
            return WordExistsFromTileRecursive(startingTile, 0, maxDepth);
        }


        private bool WordExistsFromTileRecursive(Tile tile, int currentDepth, int maxDepth)
        {
            if (tile == null)
            {
                throw new ArgumentNullException("Tile cannot be null");
            }

            if(tile.HasMark == true)
            {
                return false;
            }

            MarkAndAppend(tile, theWord);

            if (currentDepth == maxDepth)
            {
                bool result = dictionary.Contains(theWord.ToString());
                UnmarkAndRemove(tile, theWord);
                return result;
            }

            foreach (Board.Direction direction in Enum.GetValues(typeof(Board.Direction)))
            {
                var neighbor = board.GetNeighbor(tile, direction);
                if (neighbor is Tile)
                {
                   if (WordExistsFromTileRecursive(neighbor as Tile, currentDepth + 1, maxDepth))
                    {
                        UnmarkAndRemove(tile, theWord);
                        return true;
                    }
                }
            }

            UnmarkAndRemove(tile, theWord);
            return false;
        }



        public bool WordExistsOnBoard()
        {
            //TODO: Decide how to determine "maxlength" of a word - find longest word in dictionary, or number of tiles on the board, or minimum of the two, or hard code for longest word in English or modified hard code?
            //TODO: more comments to explain what's going on
            //TODO: rename walk and wordExists for clarity
            //TODO: extra check for clearing mark on tiles

            //Clear any existing marks
            foreach (Tile tile in board.Tiles)
            {
                tile.HasMark = false;
            }

            //Word length can't exceed the number of tiles on the board (temporary upper limit)
            int tilesOnBoard = board.Tiles.Count();

            //Outer loop: for each max depth
            for (int i = 0; i <= tilesOnBoard; i++)
            {
                //Inner loop: for each starting tile
                foreach (Tile tile in board.Tiles)
                {
                    theWord.Clear();
                    if(WordExistsFromStartingTile(tile, i))
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

    }
}
