using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    public class Solver
    {
        HashSet<string> dictionary;
        Board board;

        public Solver(HashSet<string> dictionary, Board board)
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


        // "Mark" keeps track of tiles that have already been checked, to
        // prevent using the same letter twice in a word and prevent checking
        // duplicate paths. "Append" updates theWord with a new letter as a new
        // tile is added to the path.
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


        // The opposite of MarkAndAppend(). Used when the solver backtracks.
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

        // Verifies that a word still exists on the board so the user can be
        // alerted when the game is over. Public method calls the private,
        // recursive version of the method.
        public bool WordExistsFromStartingTile(Tile startingTile, int targetDepth)
        {
            List<Tile> markedTiles = new List<Tile>();
            StringBuilder theWord = new StringBuilder("");
            return WordExistsFromTileRecursive(startingTile, 0, targetDepth, markedTiles, theWord);
        }


        // Uses a breadth first search to find words on the board, i.e. checks
        // for a 1-letter word first, then a 2-letter word, etc. Returns true as
        // soon as it finds any word. currentDepth refers to how many tiles are
        // in the current path. targetDepth refers to the word length being
        // searched for in the current iteration.
        private bool WordExistsFromTileRecursive(Tile tile, int currentDepth, int targetDepth, List<Tile> markedTiles, StringBuilder theWord)
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

            // If the path has reached the targetDepth, it is ready to be checked
            // against the dictionary. (If it is shorter than targetDepth, it
            // has already been checked in a previous iteration.)
            if (currentDepth == targetDepth)
            {
                bool result = dictionary.Contains(theWord.ToString());
                UnmarkAndRemove(tile, theWord, markedTiles);
                return result;
            }

            // TODO: more comments to explain what's going on

            foreach (Board.Direction direction in Enum.GetValues(typeof(Board.Direction)))
            {
                var neighbor = board.GetNeighbor(tile, direction);
                if (neighbor is Tile)
                {
                   if (WordExistsFromTileRecursive(neighbor as Tile, currentDepth + 1, targetDepth, markedTiles, theWord))
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
            // TODO: Decide how to determine "max length" of a word - find
            // longest word in dictionary, or number of tiles on the board, or
            // minimum of the two, or hard code for longest word in English or
            // modified hard code (longest word that doesn't contain another
            // word within)?

            // Word length can't exceed the number of tiles on the board
            // (temporary upper limit until better method is decided)
            int tilesOnBoard = board.Tiles.Count();

            // Outer loop: for each targetDepth
            for (int i = 0; i <= tilesOnBoard; i++)
            {
                // Inner loop: for each starting tile
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
