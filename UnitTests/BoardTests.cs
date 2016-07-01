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
        char[,] array3x3;
        char[,] arrayEmpty;
        char[,] array1x0;
        char[,] array0x1;
        char[,] array2x2;

        Tile tileA;
        Tile tileB;
        Tile tileD;
        Tile tileE;

        [TestInitialize]
        public void TestInitialize()
        {
            array3x3 = new char[,] 
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            };
            array2x2 = new char[,]
            {
                { 'a', 'b'},
                { 'd', 'e'}
            };
            arrayEmpty = new char[0, 0];
            array1x0 = new char[1, 0];
            array0x1 = new char[0, 1];

            tileA = new Tile(new Coordinates(0, 0), 'a');
            tileB = new Tile(new Coordinates(0, 1), 'b');
            tileD = new Tile(new Coordinates(1, 0), 'd');
            tileE = new Tile(new Coordinates(1, 1), 'e');
            
        }


        //Tiles tests

        [TestMethod]
        public void Tiles_board3x3_CountIs9()
        {
            var board = new Board(array3x3);
            var tiles = board.Tiles;
            Assert.AreEqual(9, tiles.Count);
        }

        [TestMethod]
        public void Tiles_board3x3_TileContentIsCorrect()
        {
            var board = new Board(array3x3);
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
                Assert.AreEqual(tile.Letter, array3x3[tile.Coords.X, tile.Coords.Y]);
            }
        }

        //NewBoard tests

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


        //GetNeighbor tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetNeighbor_TileIsNull_ThrowsException()
        {
            var board = new Board(array2x2);
            board.GetNeighbor(null, Board.Direction.North);
        }

        [TestMethod]
        public void GetNeighbor_board2x2_NeighborIsCorrect_North()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileD, Board.Direction.North) as Tile;
            Assert.AreEqual(neighbor.Letter, tileA.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileA.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileA.Coords.Y);
        }

        [TestMethod]
        public void GetNeighbor_board2x2_NeighborIsCorrect_East()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileD, Board.Direction.East) as Tile;
            Assert.AreEqual(neighbor.Letter, tileE.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileE.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileE.Coords.Y);
        }

        [TestMethod]
        public void GetNeighbor_board2x2_NeigborIsCorrect_South()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileB, Board.Direction.South) as Tile;
            Assert.AreEqual(neighbor.Letter, tileE.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileE.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileE.Coords.Y);
        }

        [TestMethod]
        public void GetNeighbor_board2x2_NeighborIsCorrect_West()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileB, Board.Direction.West) as Tile;
            Assert.AreEqual(neighbor.Letter, tileA.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileA.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileA.Coords.Y);
        }

        [TestMethod]
        public void GetNeighbor_goesBeyondGrid_North_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileA, Board.Direction.North);
            Assert.IsInstanceOfType(neighbor, typeof(Hole));
        }

        [TestMethod]
        public void GetNeighbor_goesBeyondGrid_East_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileB, Board.Direction.East);
            Assert.IsInstanceOfType(neighbor, typeof(Hole));
        }

        [TestMethod]
        public void GetNeighbor_goesBeyondGrid_South_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileE, Board.Direction.South);
            Assert.IsInstanceOfType(neighbor, typeof(Hole));
        }

        [TestMethod]
        public void GetNeighbor_goesBeyondGrid_West_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileD, Board.Direction.West);
            Assert.IsInstanceOfType(neighbor, typeof(Hole));
        }


    }
}
