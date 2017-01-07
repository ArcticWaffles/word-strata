using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStrata.Solve
{
    /// <summary>
    /// Finds words on the gameboard.
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// Public "kick-off" method used to inititate a search using <see cref="FindWordFromStartingTileRecursive"/>. 
        /// </summary>
        /// <param name="checker"> Checker used for the search. </param>
        /// <param name="board"> The gameboard. </param>
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


        /// <summary>
        /// Uses a breadth first search to find words on the board, i.e. checks
        /// for a 1-letter word first, then a 2-letter word, etc. Stores results
        /// in the checker.
        /// </summary>
        /// <param name="board"> The gameboard.</param>
        /// <param name="tile"> Tile to be added to the path. </param>
        /// <param name="tilePath"> Running list of tiles being checked. </param>
        /// <param name="checker"> See <see cref="Checker"/>  </param>
        /// <param name="currentDepth"> Size of the current tile path. </param>
        /// <param name="targetDepth"> Size of word being checked in the current iteration. </param>
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


        /// <summary> Checks that a valid word remains on the board. </summary>
        /// <param name="dictionary">All valid words.</param>
        /// <param name="board">The gameboard.</param>
        public static bool AnyWordExistsonBoard(HashSet<string> dictionary, Board board)
        {
            var checker = new DictionaryChecker(dictionary);
            FindWordFromStartingTileKickoff(checker, board);
            return checker.Result;
        }


        /// <summary> Checks that a given string can be found on the board. Returns all matches. </summary>
        /// <param name="theWord">The string to be searched for on the board.</param>
        /// <param name="board">The gameboard. </param>
        public static List<List<Tile>> SpecificWordExistsOnBoard(string theWord, Board board)
        {
            var checker = new StringChecker(theWord);
            FindWordFromStartingTileKickoff(checker, board);
            return checker.Result;
        }

        /// <summary> Retrieves letters from a list of tiles. </summary>
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

// TODO: Make Solver a DLL? (Solver, Checkers) and Core a DLL (Tile, Hole, etc.)?
// TODO: Re-evaluate access modifiers throughout the Solve namespace.