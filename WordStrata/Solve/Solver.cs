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


        // Public method calls the private, recursive version of the method.
        public void FindWordFromStartingTileKickoff(WordChecker checker)
        {
            // Outer loop: for each targetDepth
            for (int i = checker.StartDepth ; i < checker.EndDepth ; i++)
            {
                // Inner loop: for each starting tile
                foreach (Tile tile in board.Tiles)
                {
                    List<Tile> tilePath = new List<Tile>();
                    FindWordFromStartingTileRecursive(tile, tilePath, checker, 0, i);
                    if(!checker.ShallContinue)
                    {
                        break;
                    }
                }
                if(!checker.ShallContinue)
                {
                    break;
                }
            }

        }

        // Uses a breadth first search to find words on the board, i.e. checks
        // for a 1-letter word first, then a 2-letter word, etc. Stores results
        // in the checker. currentDepth refers to how many tiles are in the
        // current path. targetDepth is the size of the word being searched
        // for in the current iteration.
        private void FindWordFromStartingTileRecursive(Tile tile, List<Tile> tilePath, WordChecker checker, int currentDepth, int targetDepth)
        {
            tilePath.Add(tile);

            // If the path has reached the targetDepth, it is ready to be
            // checked. (If it is shorter than targetDepth, it has already been
            // checked in a previous iteration.)
            if (currentDepth == targetDepth)
            {
                checker.Check(tilePath);
            }

            else //currentDepth < targetDepth
            {
                foreach (Board.Direction direction in Enum.GetValues(typeof(Board.Direction)))
                {
                    var neighbor = board.GetNeighbor(tile, direction);
                    if (neighbor is Tile && !tilePath.Contains(neighbor) && checker.ShallContinue)
                    {
                        FindWordFromStartingTileRecursive(neighbor as Tile, tilePath, checker, currentDepth + 1, targetDepth);
                        tilePath.Remove(neighbor as Tile);
                    }
                }
            }

        }

        // Checks that a valid word still exists on the board so the user can be
        // alerted when the game is over.
        public DictionaryChecker AnyWordExistsonBoard()
        {
            var checker = new DictionaryChecker(dictionary);
            FindWordFromStartingTileKickoff(checker);
            return checker;
        }

        // Checks that a given string can be found in a valid path of
        // tiles on the board, and if so, returns all matching tile paths. It
        // does not check the word against the dictionary.
        public StringChecker SpecificWordExistsOnBoard(string theWord)
        {
            var checker = new StringChecker(theWord);
            FindWordFromStartingTileKickoff(checker);
            return checker;
        }
    }
}
