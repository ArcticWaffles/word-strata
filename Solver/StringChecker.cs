﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Solve
{ 
    /// <summary>
    /// Used by Solver to check a word on the board against a provided string.
    /// Verifies that a user-entered word exists on the board as a valid path.
    /// Since the word may be formed by more than one combination of tiles,
    /// StringChecker looks for and returns all matching tile paths.
    /// </summary>
    public class StringChecker : Checker
    {
        public StringChecker(string theMasterWord)
        {
            masterWord = theMasterWord;
            // ShallContinue is always true because StringChecker searches for all possible matches.
            ShallContinue = true;
            StartDepth = theMasterWord.Length - 1;
            EndDepth = theMasterWord.Length;
        }

        /// <summary> Word to be searched for on the board. </summary>
        private string masterWord;

        /// <summary> Contains all matching tile paths found on the board. </summary>
        public List<List<Tile>> Result { get; set; } = new List<List<Tile>>();

        /// <summary> Checks <see cref="masterWord"/> against a tile path's letters. </summary>
        /// <param name="path"></param>
        public override void Check(List<Tile> path)
        {
            string word = Solver.GetLetters(path);
            if (word.Equals(masterWord, StringComparison.OrdinalIgnoreCase))
            {
                var pathCopy = new List<Tile>(path);
                Result.Add(pathCopy);
            }
        }
    }
}
    