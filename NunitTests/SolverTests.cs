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
        Board board2x2;
        static Board board3x3;

        static HashSet<string> dictionaryA;
        static HashSet<string> dictionaryB;
        HashSet<string> dictionaryC;

        private static HashSet<string>[] ValidDictionaries = new HashSet<string>[] {
            dictionaryA,
            dictionaryB,
        };

        [SetUp]
        public void Init()
        {
            dictionaryA = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
            "bark",
            "bare",
            "bar",
            "cab"
            };

            dictionaryB = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
            "back",
            "ark",
            "crab",
            "are",
            "crabcake"
            };
            
            dictionaryC = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {                
            "carrot"
            };


            board2x2 = new Board(new char[,]
            {
                { 'a', 'b'},
                { 'c', 'd'}
            });

            board3x3 = new Board(new char[,]
            {
                { 'a', 'b', 'd' },
                { 'r', 'k', 'x' },
                { 'e', 'x', 'x' }
            });
        }



        // AnyWordExistsOnBoard DictionaryChecker tests
        // TODO: Use test case attribute to test multiple scenarios in one test (Condense the following tests into one.)

        [Test]
        public void FindWordDC_WordExists2x2DA_ResultIsTrue()
        {
            var result = Solver.AnyWordExistsOnBoard(dictionaryA, board2x2);
            Assert.That(result, Is.True);
        }

        [Test]
        public void FindWordDC_WordExists3x3DA_ResultIsTrue()
        {
            var result = Solver.AnyWordExistsOnBoard(dictionaryA, board3x3);
            Assert.That(result, Is.True);
        }

        [Test]
        public void FindWordDC_WordExists3x3DB_ResultIsTrue()
        {
            var result = Solver.AnyWordExistsOnBoard(dictionaryB, board3x3);
            Assert.That(result, Is.True);
        }

        [Test]
        public void FindWordDC_WordDoesNotExist2x2DC_ResultIsFalse()
        {
            var result = Solver.AnyWordExistsOnBoard(dictionaryC, board2x2);
            Assert.That(result, Is.False);
        }

        [Test]
        public void FindWordDC_WordDoesNotExist3x3DC_ResultIsFalse()
        {
            var result = Solver.AnyWordExistsOnBoard(dictionaryC, board3x3);
            Assert.That(result, Is.False);
        }
    }
}

// TODO: GetLetters tests