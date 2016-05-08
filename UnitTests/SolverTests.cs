using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordGame;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class SolverTests
    {

        Tile validUnmarkedTileA;
        Tile validMarkedTileB;

        StringBuilder newWord;
        StringBuilder wordWithSomeLetters;

        Board board3x3;

        string[] emptyDictionary;
        string[] smallDictionary;

        [TestInitialize]
        public void TestInitialize()
        {

            validUnmarkedTileA = new Tile(new Coordinates(1, 1), 'a', false);
            validMarkedTileB = new Tile(new Coordinates(2, 2), 'b', true);

            newWord = new StringBuilder();
            wordWithSomeLetters = new StringBuilder("cat");

            board3x3 = new Board(new char[,]
            {
            { 'a', 'b', 'd' },
            { 'r', 'x', 'x' },
            { 'x', 'x', 'x' }
            });

            emptyDictionary = new string[] { };
            smallDictionary = new string[]
            {
            "bar"
            };

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewSolver_EmptyDictionary_ThrowsException()
        {
            var solver = new Solver(emptyDictionary, board3x3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewSolver_DictionaryIsNull_ThrowsException()
        {
            var solver = new Solver(null, board3x3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewSolver_BoardIsNull_ThrowsException()
        {
            var solver = new Solver(smallDictionary, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MarkAndAppend_TileIsNull_ThrowsException()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.MarkAndAppend(null, newWord);
        }

        [TestMethod]
        public void MarkAndAppend_TileIsValid_StartsEmpty_AppendsLetter()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.MarkAndAppend(validUnmarkedTileA, newWord);
            Assert.AreEqual(newWord.ToString(), validUnmarkedTileA.Letter.ToString());
        }

        [TestMethod]
        public void MarkAndAppend_TileIsValid_AlreadyHasLetters_AppendsLetter()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.MarkAndAppend(validUnmarkedTileA, wordWithSomeLetters);
            Assert.AreEqual(wordWithSomeLetters.ToString(), "cat" + validUnmarkedTileA.Letter);
        }

        [TestMethod]
        public void MarkAndAppend_TileIsValid_ChangesUnmarkedToMarked()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.MarkAndAppend(validUnmarkedTileA, newWord);
            Assert.IsTrue(validUnmarkedTileA.HasMark == true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MarkAndAppend_TileIsAlreadyMarked_ThrowsExeption()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.MarkAndAppend(validMarkedTileB, newWord);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MarkAndAppend_theWordIsNull_ThrowsExeption()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.MarkAndAppend(validMarkedTileB, null);
        }

        //[TestMethod]
        //public void WordExists_WordDoesExist_ReturnsTrue()
        //{
        //    var solver = new Solver(smallDictionary, board3x3);
        //    var result = solver.WordExists();
        //    Assert.IsTrue(result, "No words were found");
        //}
    }



}
