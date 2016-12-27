using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    public static class Solver
    {
        // Public "kick-off" method calls the private, recursive version of the method.
        public static void FindWordFromStartingTileKickoff(Checker checker, Board board)
        {
            // Outer loop: for each targetDepth
            for (int i = checker.StartDepth ; i < checker.EndDepth ; i++)
            {
                // Inner loop: for each starting tile
                foreach (Tile tile in board.Tiles)
                {
                    FindWordFromStartingTileRecursive(board, tile, new List<Tile>(), checker, 0, i);
                    if (!checker.ShallContinue)
                        break;
                }
                if (!checker.ShallContinue)
                    break;
            }
        }

        // Uses a breadth first search to find words on the board, i.e. checks
        // for a 1-letter word first, then a 2-letter word, etc. Stores results
        // in the checker. currentDepth refers to how many tiles are in the
        // current path. targetDepth is the size of the word being searched
        // for in the current iteration.
        private static void FindWordFromStartingTileRecursive(Board board, Tile tile, 
            List<Tile> tilePath, Checker checker, int currentDepth, int targetDepth)
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
                        FindWordFromStartingTileRecursive(board, neighbor as Tile, tilePath, checker, currentDepth + 1, targetDepth);
                        tilePath.Remove(neighbor as Tile);
                    }
                }
            }

        }

        // Checks that a valid word still exists on the board so the user can be
        // alerted when the game is over.
        public static bool AnyWordExistsonBoard(HashSet<string> dictionary, Board board)
        {
            var checker = new DictionaryChecker(dictionary);
            FindWordFromStartingTileKickoff(checker, board);
            return checker.Result;
        }

        // Checks that a given string can be found in a valid path of
        // tiles on the board, and if so, returns all matching tile paths. It
        // does not check the word against the dictionary.
        public static List<List<Tile>> SpecificWordExistsOnBoard(string theWord, Board board)
        {
            var checker = new StringChecker(theWord);
            FindWordFromStartingTileKickoff(checker, board);
            return checker.Result;
        }

        public static string GetLetters(List<Tile> path)
        {
            string word = "";
            foreach (var tile in path)
            {
                word += tile.Letter;
            }
            return word;
        }
    }
}
