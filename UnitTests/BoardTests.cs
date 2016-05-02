using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordGame;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class BoardTests
    {
        char[,] board3x3 = new char[,] {
            { 'a', 'b', 'd' },
            { 'r', 'x', 'x' },
            { 'x', 'x', 'x' }
        };
        char[,] arrayEmpty = new char[0, 0];
        char[,] array1x0 = new char[1, 0];
        char[,] array0x1 = new char[0, 1];


        [TestMethod]
        public void Tiles_board3x3_CountIs9()
        {
            var board = new Board(board3x3);
            var tiles = board.Tiles;
            Assert.AreEqual(9, tiles.Count);
        }

        [TestMethod]
        public void Tiles_board3x3_TileContentIsCorrect()
        {
            var board = new Board(board3x3);
            var tiles = board.Tiles;
            var previousCoords = new List<Coordinates>();
            foreach (var tile in tiles)
            {
                //TODO: Test case where next assertion fails
                if (previousCoords.Any(coord => coord.X == tile.Coords.X && coord.Y == tile.Coords.Y))
                {
                    Assert.Fail("Tiles weren't unique");
                }
                previousCoords.Add(tile.Coords);
                Assert.IsFalse(tile.HasMark);
                Assert.AreEqual(tile.Letter, board3x3[tile.Coords.X, tile.Coords.Y]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewBoard_arrayEmpty_ThrowsException()
        {
            var board = new Board(arrayEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewBoard_array1x0_ThrowsException()
        {
            var board = new Board(array1x0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NewBoard_array0x1_ThrowsException()
        {
            var board = new Board(array0x1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewBoard_arraynull_ThrowsException()
        {
            var board = new Board(null);
        }

    }
}
