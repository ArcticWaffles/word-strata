using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Core;


namespace NUnitTests
{
    [TestFixture]
    class BoardTests
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

        [SetUp]
        public void Init()
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

        [Test]
        public void Tiles_board3x3_CountIs9()
        {
            var board = new Board(array3x3);
            var tiles = board.Tiles;
            Assert.AreEqual(9, tiles.Count);
        }

        [Test]
        public void Tiles_board3x3_TileContentIsCorrect()
        {
            var board = new Board(array3x3);
            var tiles = board.Tiles;
            var previousCoords = new List<Coordinates>();
            foreach (var tile in tiles)
            {
                // TODO: Test case where next assertion fails
                if (previousCoords.Any(coord => coord.X == tile.Coords.X && coord.Y == tile.Coords.Y))
                {
                    Assert.Fail("Tiles weren't unique");
                }
                previousCoords.Add(tile.Coords);
                Assert.AreEqual(tile.Letter, array3x3[tile.Coords.X, tile.Coords.Y]);
            }
        }



        // ConvertTilesToHoles tests

        [Test]
        public void ConvertTiles_WholeBoard_AllSquaresAreHoles()
        {
            var board = new Board(array2x2);
            var tiles = board.Tiles;
            board.ConvertTilesToHoles(tiles);
            foreach (var gridsquare in board.GridSquares)
            {
                Assert.That(gridsquare, Is.InstanceOf(typeof(Hole)));
            }
            Assert.That(board.Tiles, Is.Empty);
        }

        [Test]
        public void ConvertTiles_PartialBoard_RemainingTileCountIsCorrect()
        {
            var board = new Board(array2x2);
            var tiles = new List<Tile>();
            tiles.Add(tileA);
            tiles.Add(tileB);
            board.ConvertTilesToHoles(tiles);
            Assert.That(board.Tiles.Count.Equals(2));
        }



        //TODO: Are these tests worth resurrecting in NUnit syntax?

        //NewBoard tests

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void NewBoard_arrayEmpty_ThrowsException()
        //{
        //    var board = new Board(arrayEmpty);
        //}

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void NewBoard_array1x0_ThrowsException()
        //{
        //    var board = new Board(array1x0);
        //}

        //[Test]
        //[ExpectedException(typeof(ArgumentException))]
        //public void NewBoard_array0x1_ThrowsException()
        //{
        //    var board = new Board(array0x1);
        //}

        //[Test]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void NewBoard_arraynull_ThrowsException()
        //{
        //    var board = new Board(null);
        //}



        //GetNeighbor tests

        [Test]
        public void GetNeighbor_board2x2_NeighborIsCorrect_North()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileD, Board.Direction.North) as Tile;
            Assert.AreEqual(neighbor.Letter, tileA.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileA.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileA.Coords.Y);
        }

        [Test]
        public void GetNeighbor_board2x2_NeighborIsCorrect_East()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileD, Board.Direction.East) as Tile;
            Assert.AreEqual(neighbor.Letter, tileE.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileE.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileE.Coords.Y);
        }

        [Test]
        public void GetNeighbor_board2x2_NeigborIsCorrect_South()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileB, Board.Direction.South) as Tile;
            Assert.AreEqual(neighbor.Letter, tileE.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileE.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileE.Coords.Y);
        }

        [Test]
        public void GetNeighbor_board2x2_NeighborIsCorrect_West()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileB, Board.Direction.West) as Tile;
            Assert.AreEqual(neighbor.Letter, tileA.Letter);
            Assert.AreEqual(neighbor.Coords.X, tileA.Coords.X);
            Assert.AreEqual(neighbor.Coords.Y, tileA.Coords.Y);
        }


        [Test]
        public void GetNeighbor_goesBeyondGrid_North_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileA, Board.Direction.North);
            Assert.That(neighbor, Is.InstanceOf(typeof(Hole)));
        }

        [Test]
        public void GetNeighbor_goesBeyondGrid_East_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileB, Board.Direction.East);
            Assert.That(neighbor, Is.InstanceOf(typeof(Hole)));
        }

        [Test]
        public void GetNeighbor_goesBeyondGrid_South_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileE, Board.Direction.South);
            Assert.That(neighbor, Is.InstanceOf(typeof(Hole)));
        }

        [Test]
        public void GetNeighbor_goesBeyondGrid_West_returnsHole()
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(tileD, Board.Direction.West);
            Assert.That(neighbor, Is.InstanceOf(typeof(Hole)));
        }

    }
}

