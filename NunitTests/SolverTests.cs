using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordStrata.Solve;
using NUnit.Framework.Internal;

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



        // FindWord DictionaryChecker tests

        [Test]
        public void FindWordDC_WordExists2x2DA_ResultIsTrue()
        {
            var solver = new Solver(dictionaryA, board2x2);
            var checker = solver.AnyWordExistsonBoard();
            Assert.That(checker.Result, Is.True);
        }

        [Test]
        public void FindWordDC_WordExists3x3DA_ResultIsTrue()
        {
            var solver = new Solver(dictionaryA, board3x3);
            var checker = solver.AnyWordExistsonBoard();
            Assert.That(checker.Result, Is.True);
        }

        [Test]
        public void FindWordDC_WordExists3x3DB_ResultIsTrue()
        {
            var solver = new Solver(dictionaryB, board3x3);
            var checker = solver.AnyWordExistsonBoard();
            Assert.That(checker.Result, Is.True);
        }

        [Test]
        public void FindWordDC_WordDoesNotExist2x2DC_ResultIsFalse()
        {
            var solver = new Solver(dictionaryC, board2x2);
            var checker = solver.AnyWordExistsonBoard();
            Assert.That(checker.Result, Is.False);
        }

        [Test]
        public void FindWordDC_WordDoesNotExist3x3DC_ResultIsFalse()
        {
            var solver = new Solver(dictionaryC, board3x3);
            var checker = solver.AnyWordExistsonBoard();
            Assert.That(checker.Result, Is.False);
        }



        // FindWord StringChecker tests

        [Test]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("cab")]
        [TestCase("dba")]
        [TestCase("acdb")]
        public void FindWordSC_SinglePathWordExists2x2_ResultIsWord(string word)
        {
            var solver = new Solver(dictionaryA, board2x2);
            var checker = solver.SpecificWordExistsOnBoard(word);
            Assert.That(checker.Result.Count, Is.EqualTo(1));
            Assert.That(checker.GetLetters(checker.Result[0]), Is.EqualTo(word));
        }

        [Test]
        [TestCase("k")]
        [TestCase("xd")]
        [TestCase("barex")]
        [TestCase("dbarexk")]
        [TestCase("dbarexkxx")]
        public void FindWordSC_SinglePathWordExists3x3_ResultIsWord(string word)
        {
            var solver = new Solver(dictionaryA, board3x3);
            var checker = solver.SpecificWordExistsOnBoard(word);
            Assert.That(checker.Result.Count, Is.EqualTo(1));
            Assert.That(checker.GetLetters(checker.Result[0]), Is.EqualTo(word));
        }

        [Test]
        [TestCase("xk")]
        [TestCase("bkx")]
        [TestCase("kxx")]
        public void FindWordSC_2PathWordExists3x3_ResultIsAll(string word)
        {
            var solver = new Solver(dictionaryA, board3x3);
            var checker = solver.SpecificWordExistsOnBoard(word);
            Assert.That(checker.Result.Count, Is.EqualTo(2));
            foreach (var list in checker.Result)
            {
                Assert.That(checker.GetLetters(list), Is.EqualTo(word));
            }
        }

        [Test]
        public void FindWordSC_3PathWordExists3x3_ResultIsAll()
        {
            var solver = new Solver(dictionaryA, board3x3);
            var checker = solver.SpecificWordExistsOnBoard("x");
            Assert.That(checker.Result.Count, Is.EqualTo(3));
            foreach (var list in checker.Result)
            {
                Assert.That(checker.GetLetters(list), Is.EqualTo("x"));
            }
        }

        [Test]
        public void FindWordSC_WordDoesNotExist2x2_ResultIsEmpty()
        {
            var solver = new Solver(dictionaryA, board2x2);
            var checker = solver.SpecificWordExistsOnBoard("sheep");
            Assert.That(checker.Result, Is.Empty);
        }
    }
}
