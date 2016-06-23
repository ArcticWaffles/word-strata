using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WordGame;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class SolverTests
    {

        Tile validUnmarkedTileA;
        Tile validMarkedTileB;
        Tile startOfWordTile2x2;
        Tile startOfWordTile3x3;

        StringBuilder newWord;
        StringBuilder wordWithSomeLetters;

        Board board3x3;
        Board board2x2;
        Board boardWithNoWords;

        List<string> emptyDictionary;
        List<string> smallDictionary;
        List<string> wordCannotBeFoundDictionary;

        [TestInitialize]
        public void TestInitialize()
        {

            validUnmarkedTileA = new Tile(new Coordinates(1, 1), 'a', false);
            validMarkedTileB = new Tile(new Coordinates(2, 2), 'b', true);
            startOfWordTile2x2 = new Tile(new Coordinates(1, 0), 'c', false);
            startOfWordTile3x3 = new Tile(new Coordinates(0, 1), 'b', false);

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
                { 'r', 'k', 'x' },
                { 'x', 'x', 'x' }
            });

            boardWithNoWords = new Board(new char[,]
            {
                {'x', 'x' },
                {'x', 'x' }
            });

            emptyDictionary = new List<string> { };

            smallDictionary = new List<string>
            {
            "bark",
            "bar",
            "cab"
            };

            wordCannotBeFoundDictionary = new List<string>
            {
                "daisy"
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



        //Walk tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Walk_TileIsNull_ThrowsException()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.WordExistsFromStartingTile(null, 0); 
        }

        [TestMethod]
        public void Walk_TileIsMarked_ReturnsFalse()
        {
            var solver = new Solver(smallDictionary, board2x2);
            bool result = solver.WordExistsFromStartingTile(validMarkedTileB, 0);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Walk_WordIsFound3Letters2x2_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board2x2);
            Assert.IsTrue(solver.WordExistsFromStartingTile(startOfWordTile2x2, 2));
        }

        [TestMethod]
        public void Walk_WordIsFound3Letters3x3_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board3x3);
            Assert.IsTrue(solver.WordExistsFromStartingTile(startOfWordTile3x3, 2));
        }

        [TestMethod]
        public void Walk_WordIsFound4Letters3x3_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board3x3);
            Assert.IsTrue(solver.WordExistsFromStartingTile(startOfWordTile3x3, 3));
        }

        [TestMethod]
        public void Walk_WordExistsButMaxDepthTooSmall_ReturnsFalse()
        {
            var solver = new Solver(smallDictionary, board3x3);
            Assert.IsFalse(solver.WordExistsFromStartingTile(startOfWordTile3x3, 1));
        }

        [TestMethod]
        public void Walk_WordIsNotFound_ReturnsFalse()
        {
            var solver = new Solver(wordCannotBeFoundDictionary, board2x2);
            Assert.IsFalse(solver.WordExistsFromStartingTile(startOfWordTile2x2, 2));
        }

        [TestMethod]
        public void Walk_WordIsNotFoundFromStartingLetter_ReturnsFalse()
        {
            var solver = new Solver(smallDictionary, board2x2);
            Assert.IsFalse(solver.WordExistsFromStartingTile(validUnmarkedTileA, 3));
        }



        //Word Exists Tests
        //TODO: remove marked attribute from tiles, add list of marked tiles to solver class
        //TODO: unit tests to ensure all marks are clear after performing WordExists

        [TestMethod]
        public void WordExists_WordDoesExist3x3_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board3x3);
            Assert.IsTrue(solver.WordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesExist2x2_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board2x2);
            Assert.IsTrue(solver.WordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesNotExistDictionary_ReturnsFalse()
        {
            var solver = new Solver(wordCannotBeFoundDictionary, board3x3);
            Assert.IsFalse(solver.WordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesNotExistBoard_ReturnsFalse()
        {
            var solver = new Solver(smallDictionary, boardWithNoWords);
            Assert.IsFalse(solver.WordExistsOnBoard());
        }

        //TODO: unmark and remove tests - last letter in string is equal to tile letter? (do m/a and then u/r)?
        //TODO: Walk - test words of more lengths?
        //TODO: dictionary - would sort and binary search be helpful for large dictionaries?

    }



}
