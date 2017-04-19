using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Core;
using Solve;

namespace NUnitTests
{
    [TestFixture]
    class SolverTests
    {
        static HashSet<String> dictionaryA = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        { 
            "bark",
            "bare",
            "bar",
            "cab"
        };

        static HashSet<String> dictionaryB = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "cbd",
            "ark",
            "crab"
        };

        static HashSet<String> dictionaryC = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {                
            "carrot"
        };


        static Board board2x2 = new Board(new char[,]
        {
            { 'a', 'b'},
            { 'c', 'd'}
        });

        static Board board3x3 = new Board(new char[,]
        {
            { 'a', 'b', 'd' },
            { 'r', 'k', 'x' },
            { 'e', 'x', 'x' }
        });


        // AnyWordExistsOnBoard tests (uses DictionaryChecker)

        [Test, TestCaseSource("WordExistsCases")]
        public void AnyWordExistsOnBoard_WordExists_ResultIsTrue(HashSet<string> dictionary, Board gameboard)
        {
            var result = Solver.AnyWordExistsOnBoard(dictionary, gameboard);
            Assert.That(result, Is.True);
        }

        static object[] WordExistsCases =
        {
            new object[] { dictionaryA, board2x2 },
            new object[] { dictionaryA, board3x3 },
            new object[] { dictionaryB, board3x3 },
        };

        [Test, TestCaseSource("WordDoesNotExistCases")]
        public void AnyWordExistsOnBoard_WordDoesNotExist_ResultIsFalse(HashSet<string> dictionary, Board gameboard)
        {
            var result = Solver.AnyWordExistsOnBoard(dictionary, gameboard);
            Assert.That(result, Is.False);
        }

        static object[] WordDoesNotExistCases =
        {
            new object[] { dictionaryB, board2x2 },
            new object[] { dictionaryC, board2x2 },
            new object[] { dictionaryC, board3x3 },
        };

    }
}

// TODO: GetLetters tests