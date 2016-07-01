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

        private void MarkAndAppend(Tile tile, StringBuilder theWord, List<Tile> markedTiles)
        {
            if (tile == null)
            {
                throw new ArgumentNullException("Tile cannot be null");
            }
            if (theWord == null)
            {
                throw new ArgumentNullException("theWord cannot be null");
            }
            if (markedTiles.Contains(tile))
            {
                throw new ArgumentException("Tile should be unmarked");
            }

            markedTiles.Add(tile);
            theWord.Append(tile.Letter);
        }

        private void UnmarkAndRemove(Tile tile, StringBuilder theWord, List<Tile> markedTiles)
        {
            if (tile == null)
            {
                throw new ArgumentNullException("Tile cannot be null");
            }
            if (theWord == null)
            {
                throw new ArgumentNullException("theWord cannot be null");
            }
            if (!markedTiles.Contains(tile))
            {
                throw new ArgumentException("Tile should be marked");
            }
            markedTiles.Remove(tile);
            if (theWord.Length < 1)
            {
                throw new ArgumentException("theWord cannot be empty");
            }
            theWord.Remove((theWord.Length - 1), 1);
        }

        public bool WordExistsFromStartingTile(Tile startingTile, int maxDepth)
        {
            List<Tile> markedTiles = new List<Tile>();
            StringBuilder theWord = new StringBuilder("");
            return WordExistsFromTileRecursive(startingTile, 0, maxDepth, markedTiles, theWord);
        }


        private bool WordExistsFromTileRecursive(Tile tile, int currentDepth, int maxDepth, List<Tile> markedTiles, StringBuilder theWord)
        {
            if (tile == null)
            {
                throw new ArgumentNullException("Tile cannot be null");
            }

            if(markedTiles.Contains(tile))
            {
                return false;
            }

            MarkAndAppend(tile, theWord, markedTiles);

            if (currentDepth == maxDepth)
            {
                bool result = dictionary.Contains(theWord.ToString());
                UnmarkAndRemove(tile, theWord, markedTiles);
                return result;
            }

            foreach (Board.Direction direction in Enum.GetValues(typeof(Board.Direction)))
            {
                var neighbor = board.GetNeighbor(tile, direction);
                if (neighbor is Tile)
                {
                   if (WordExistsFromTileRecursive(neighbor as Tile, currentDepth + 1, maxDepth, markedTiles, theWord))
                    {
                        UnmarkAndRemove(tile, theWord, markedTiles);
                        return true;
                    }
                }
            }

            UnmarkAndRemove(tile, theWord, markedTiles);
            return false;
        }



        public bool WordExistsOnBoard()
        {
            //TODO: Decide how to determine "maxlength" of a word - find longest word in dictionary, or number of tiles on the board, or minimum of the two, or hard code for longest word in English or modified hard code?
            //TODO: more comments to explain what's going on
            //TODO: rename walk and wordExists for clarity

            //Word length can't exceed the number of tiles on the board (temporary upper limit)
            int tilesOnBoard = board.Tiles.Count();

            //Outer loop: for each max depth
            for (int i = 0; i <= tilesOnBoard; i++)
            {
                //Inner loop: for each starting tile
                foreach (Tile tile in board.Tiles)
                {
                    if (WordExistsFromStartingTile(tile, i))
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }

    }
}
