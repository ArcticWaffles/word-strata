using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WordStrata.Solve;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class SolverTests
    {

        Tile validTileA;
        Tile validTileB;
        Tile startOfWordTile2x2;
        Tile startOfWordTile3x3;

        StringBuilder newWord;
        StringBuilder wordWithSomeLetters;

        Board board3x3;
        Board board2x2;
        Board boardWithNoWords;

        HashSet<string> emptyDictionary;
        HashSet<string> smallDictionary;
        HashSet<string> wordCannotBeFoundDictionary;

        [TestInitialize]
        public void TestInitialize()
        {

            validTileA = new Tile(new Coordinates(1, 1), 'a');
            validTileB = new Tile(new Coordinates(2, 2), 'b');
            startOfWordTile2x2 = new Tile(new Coordinates(1, 0), 'c');
            startOfWordTile3x3 = new Tile(new Coordinates(0, 1), 'b');

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

            emptyDictionary = new HashSet<string>();

            smallDictionary = new HashSet<string>
            {
            "bark",
            "bar",
            "cab"
            };

            wordCannotBeFoundDictionary = new HashSet<string>
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




        //Walk tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Walk_TileIsNull_ThrowsException()
        {
            var solver = new Solver(smallDictionary, board3x3);
            solver.WordExistsFromStartingTile(null, 0); 
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
            Assert.IsFalse(solver.WordExistsFromStartingTile(validTileA, 3));
        }



        //AnyWordExists Tests

        [TestMethod]
        public void WordExists_CheckThatMarkedTilesListIsEmpty_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board3x3);
            Assert.IsTrue(solver.AnyWordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesExist3x3_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board3x3);
            Assert.IsTrue(solver.AnyWordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesExist2x2_ReturnsTrue()
        {
            var solver = new Solver(smallDictionary, board2x2);
            Assert.IsTrue(solver.AnyWordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesNotExistDictionary_ReturnsFalse()
        {
            var solver = new Solver(wordCannotBeFoundDictionary, board3x3);
            Assert.IsFalse(solver.AnyWordExistsOnBoard());
        }

        [TestMethod]
        public void WordExists_WordDoesNotExistBoard_ReturnsFalse()
        {
            var solver = new Solver(smallDictionary, boardWithNoWords);
            Assert.IsFalse(solver.AnyWordExistsOnBoard());
        }

        //TODO: Walk - test words of more lengths?

        // SpecificWordExists Tests
        [TestMethod]
        public void SpecificWordExists_WordDoesNotExist_ReturnsEmptyList()
        {
            var solver = new Solver(smallDictionary, board3x3);
            var list = solver.SpecificWordExistsOnBoard("yo");
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void SpecificWordExists_OnePathExists_ReturnsOneList()
        {
            var solver = new Solver(smallDictionary, board3x3);
            var list = solver.SpecificWordExistsOnBoard("kb");
            Assert.IsTrue(list.Count == 1);
        }

        [TestMethod]
        public void SpecificWordExists_TwoPathsExist_ReturnsTwoLists()
        {
            var solver = new Solver(smallDictionary, board3x3);
            var list = solver.SpecificWordExistsOnBoard("xk");
            Assert.IsTrue(list.Count == 2);
        }

        [TestMethod]
        public void SpecificWordExists_FourPathsExist_ReturnsFourLists()
        {
            var solver = new Solver(smallDictionary, board3x3);
            var list = solver.SpecificWordExistsOnBoard("x");
            Assert.IsTrue(list.Count == 4);
        }

    }



}
