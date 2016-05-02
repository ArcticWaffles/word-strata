using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordGame;

namespace UnitTests
{
    [TestClass]
    public class SolverTests
    {
        Board board3x3 = new Board(new char[,] {
            { 'a', 'b', 'd' },
            { 'r', 'x', 'x' },
            { 'x', 'x', 'x' }
        });


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

        //[TestMethod]
        //public void WordExists_WordDoesExist_ReturnsTrue()
        //{
        //    var solver = new Solver(smallDictionary, board3x3);
        //    var result = solver.WordExists();
        //    Assert.IsTrue(result, "No words were found");
        //}
    }



}
