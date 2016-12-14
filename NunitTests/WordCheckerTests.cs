using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordStrata;
using WordStrata.Solve;

namespace NUnitTests
{
    [TestFixture]
    class WordCheckerTests
    {
        HashSet<string> dictionaryA;
        HashSet<string> dictionaryB;

        Tile tileA;
        Tile tileB;
        Tile tileC;
        Tile tileK;
        Tile tileR;

        DictionaryChecker dChecker;
        StringChecker sChecker;
        List<Tile> tileList;

        [SetUp]
        public void Init()
        {
            dictionaryA = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
            "bark",
            "bar",
            "cab"
            };

            dictionaryB = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
            "back",
            "ark",
            "crab",
            "arc"
            };

            tileA = new Tile(new Coordinates(0, 0), 'A');
            tileB = new Tile(new Coordinates(0, 0), 'B');
            tileC = new Tile(new Coordinates(0, 0), 'C');
            tileK = new Tile(new Coordinates(0, 0), 'K');
            tileR = new Tile(new Coordinates(0, 0), 'R');

            tileList = new List<Tile>();
        }

        // DictionaryChecker Tests

        [Test]
        public void DictionaryChecker_NoMatch_ResultIsFalse()
        {
            dChecker = new DictionaryChecker(dictionaryA);
            tileList.Add(tileB);
            tileList.Add(tileK);
            dChecker.Check(tileList);
            Assert.That(dChecker.Result, Is.False);
        }

        [Test]
        public void DictionaryChecker_Match_ResultIsTrue()
        {
            dChecker = new DictionaryChecker(dictionaryB);
            tileList.Add(tileA);
            tileList.Add(tileR);
            tileList.Add(tileK);
            dChecker.Check(tileList);
            Assert.That(dChecker.Result, Is.True);
        }

        [Test]
        public void DictionaryChecker_EmptyTileList_ResultIsFalse()
        {
            dChecker = new DictionaryChecker(dictionaryB);
            dChecker.Check(tileList);
            Assert.That(dChecker.Result, Is.False);
        }

        [Test]
        public void DictionaryChecker_NoMatch_ShallContinueIsTrue()
        {
            dChecker = new DictionaryChecker(dictionaryA);
            tileList.Add(tileC);
            dChecker.Check(tileList);
            Assert.That(dChecker.ShallContinue, Is.True);
        }

        [Test]
        public void DictionaryChecker_Match_ShallContinueIsFalse()
        {
            dChecker = new DictionaryChecker(dictionaryB);
            tileList.Add(tileA);
            tileList.Add(tileR);
            tileList.Add(tileC);
            dChecker.Check(tileList);
            Assert.That(dChecker.ShallContinue, Is.False);
        }

        // StringChecker Tests

        [Test]
        public void StringChecker_NoMatch_ResultIsEmpty()
        {
            sChecker = new StringChecker("banana");
            sChecker.Check(tileList);
            Assert.That(sChecker.Result, Is.Empty);
        }

        [Test]
        public void StringChecker_Match_ResultSavesPath()
        {
            sChecker = new StringChecker("back");
            tileList.Add(tileB);
            tileList.Add(tileA);
            tileList.Add(tileC);
            tileList.Add(tileK);
            sChecker.Check(tileList);
            Assert.That(sChecker.Result.Count.Equals(1));
            Assert.That(sChecker.GetLetters(sChecker.Result[0]), Is.EqualTo("back").IgnoreCase);
        }

        [Test]
        public void StringChecker_2ndMatch_ResultSaves2ndPath()
        {
            sChecker = new StringChecker("back");
            tileList.Add(tileB);
            tileList.Add(tileA);
            tileList.Add(tileC);
            tileList.Add(tileK);
            sChecker.Check(tileList);
            sChecker.Check(tileList);
            Assert.That(sChecker.Result.Count.Equals(2));
            Assert.That(sChecker.Result[0], Is.EqualTo(sChecker.Result[1]));
        }

        [Test]
        public void StringChecker_Match_CheckIsCaseInsensitive()
        {
            sChecker = new StringChecker("bArK");
            tileList.Add(tileB);
            tileList.Add(tileA);
            tileList.Add(tileR);
            tileList.Add(tileK);
            sChecker.Check(tileList);
            Assert.That(sChecker.Result.Count.Equals(1));
            Assert.That(sChecker.GetLetters(sChecker.Result[0]), Is.EqualTo("bark").IgnoreCase);
        }

        //ShallContinue should always be true when using a StringChecker
        [Test]
        public void StringChecker_NoMatch_ShallContinueIsTrue()
        {
            sChecker = new StringChecker("b");
            tileList.Add(tileC);
            sChecker.Check(tileList);
            Assert.That(sChecker.ShallContinue, Is.True);
        }

        [Test]
        public void StringChecker_Match_ShallContinueIsTrue()
        {
            sChecker = new StringChecker("b");
            tileList.Add(tileB);
            sChecker.Check(tileList);
            Assert.That(sChecker.ShallContinue, Is.True);
        }

        [Test]
        public void StringChecker_EmptyTileList_ResultIsEmpty()
        {
            sChecker = new StringChecker("ba");
            sChecker.Check(tileList);
            Assert.That(sChecker.Result, Is.Empty);
        }

        [Test]
        public void StringChecker_EmptyString_ResultIsEmpty()
        {
            sChecker = new StringChecker("");
            tileList.Add(tileA);
            sChecker.Check(tileList);
            Assert.That(sChecker.Result, Is.Empty);
        }
    }
}
