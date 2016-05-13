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
        Board board2x2;

        string[] emptyDictionary;
        string[] smallDictionary;

        [TestInitialize]
        public void TestInitialize()
        {

            validUnmarkedTileA = new Tile(new Coordinates(1, 1), 'a', false);
            validMarkedTileB = new Tile(new Coordinates(2, 2), 'b', true);

            newWord = new StringBuilder();
            wordWithSomeLetters = new StringBuilder("cat");

            board2x2 = new Board(new char[,]
            {
                { 'a', 'b'},
                { 'c', 'd'}
            });

            board3x3 = new Board(new char[,]
            {
            { 'a', 'b', 'd' },
            { 'r', 'x', 'x' },
            { 'x', 'x', 'x' }
            });

            emptyDictionary = new string[] { };
            smallDictionary = new string[]
            {
            "bar",
            "cab"
            };

        }

        //NewSolver tests

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




        //MarkAndAppend tests

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
            solver.MarkAndAppend(validUnmarkedTileA, null);
        }




        //UnmarkAndRemove tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnmarkAndRemove_TileIsNull_ThrowsException()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.UnmarkAndRemove(null, newWord);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnmarkAndRemove_theWordIsNull_ThrowsExeption()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.UnmarkAndRemove(validMarkedTileB, null);
        }

        [TestMethod]
        public void UnmarkAndRemove_TileIsValid_RemovesLetter()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.UnmarkAndRemove(validMarkedTileB, wordWithSomeLetters);
            Assert.AreEqual(wordWithSomeLetters.ToString(), "ca");
        }

        [TestMethod]
        public void UnmarkAndRemove_TileIsValid_ChangesMarkedToUnmarked()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.UnmarkAndRemove(validMarkedTileB, wordWithSomeLetters);
            Assert.IsTrue(validMarkedTileB.HasMark == false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UnmarkAndRemove_TileIsAlreadyUnmarked_ThrowsExeption()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.UnmarkAndRemove(validUnmarkedTileA, wordWithSomeLetters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UnmarkAndRemove_theWordIsAlreadyEmpty_ThrowsExeption()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.UnmarkAndRemove(validMarkedTileB, newWord);
        }


        //unmark and remove tests - last letter in string is equal to tile letter? (do m/a and then u/r?)
        //test words of more lengths?

        //do other smaller methods: 
        //dictionary.contains - sort and binary search?
        //GetNeighbor()
     


        //Walk: don't backtrack yourself before you're done searching
        //Null tests : tile, currentDepth, maxDepth
        //maxDepth not greater than longest word in dictionary or size of board
        //test on specific boards


        //[TestMethod]
        //public void WordExists_WordDoesExist_ReturnsTrue()
        //{
        //    var solver = new Solver(smallDictionary, board3x3);
        //    var result = solver.WordExists();
        //    Assert.IsTrue(result, "No words were found");
        //}
    }



}
