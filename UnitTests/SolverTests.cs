using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordGame;

namespace UnitTests
{
    [TestClass]
    public class SolverTests
    {
        char[,] board3x3 = new char[,] {
            { 'a', 'b', 'd' },
            { 'r', 'x', 'x' },
            { 'x', 'x', 'x' }
        };
        char[,] boardEmpty = new char[0, 0];
        char[,] board1x0 = new char[1, 0];
        char[,] board0x1 = new char[0, 1];

        string[] emptyDictionary = new string[] { };
        string[] smallDictionary = new string[]
        {
            "bar"
        };


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewSolver_EmptyDictionary_ThrowsException()
        {
            var solver = new Solver(emptyDictionary, board3x3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewSolver_EmptyBoard_ThrowsException()
        {
            var solver = new Solver(smallDictionary, boardEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewSolver_board1x0_ThrowsException()
        {
            var solver = new Solver(smallDictionary, board1x0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewSolver_board0x1_ThrowsException()
        {
            var solver = new Solver(smallDictionary, board0x1);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NewSolver_DictionaryIsNull_ThrowsException()
        {
            var solver = new Solver(null, board3x3);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NewSolver_BoardIsNull_ThrowsException()
        {
            var solver = new Solver(smallDictionary, null);
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
