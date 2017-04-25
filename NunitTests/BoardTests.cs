using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Core;
using WordStrata;

namespace NUnitTests
{
    [TestFixture]
    class BoardTests
    {
        char[,] array3x3 = new char[,]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            };

        char[,] array2x2 = new char[,]
            {
                { 'a', 'b'},
                { 'd', 'e'}
            };

        static Tile tileA = new Tile(new Coordinates(0, 0), 'a');
        static Tile tileB = new Tile(new Coordinates(0, 1), 'b');
        static Tile tileC = new Tile(new Coordinates(0, 2), 'c');
        static Tile tileD = new Tile(new Coordinates(1, 0), 'd');
        static Tile tileE = new Tile(new Coordinates(1, 1), 'e');
        static Tile tileF = new Tile(new Coordinates(1, 2), 'f');
        static Tile tileG = new Tile(new Coordinates(2, 0), 'g');
        static Tile tileH = new Tile(new Coordinates(2, 1), 'h');
        static Tile tileI = new Tile(new Coordinates(2, 2), 'i');


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
            var path = new List<Tile>(board.Tiles);
            board.ConvertTilesToHoles(path);
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


        //GetNeighbor tests

        [Test, TestCaseSource("GetNeighborCases")]
        public void GetNeighbor_EachCompassDirection_NeighborIsCorrect(Board.Direction direction, Tile neighbor)
        {
            var board = new Board(array3x3);
            var result = board.GetNeighbor(tileE, direction) as Tile;
            Assert.AreEqual(result.Letter, neighbor.Letter);
            Assert.AreEqual(result.Coords.X, neighbor.Coords.X);
            Assert.AreEqual(result.Coords.Y, neighbor.Coords.Y);
        }

        static object[] GetNeighborCases =
        {
            new object[] { Board.Direction.North, tileB },
            new object[] { Board.Direction.Northeast, tileC },
            new object[] { Board.Direction.East, tileF },
            new object[] { Board.Direction.Southeast, tileI },
            new object[] { Board.Direction.South, tileH },
            new object[] { Board.Direction.Southwest, tileG },
            new object[] { Board.Direction.West, tileD },
            new object[] { Board.Direction.Northwest, tileA },
        };


        [Test, TestCaseSource("OffGridCases")]
        public void GetNeighbor_GoesBeyondGrid_ReturnsHole(Board.Direction direction, Tile originTile)
        {
            var board = new Board(array2x2);
            var neighbor = board.GetNeighbor(originTile, direction);
            Assert.That(neighbor, Is.InstanceOf(typeof(Hole)));
        }

        static object[] OffGridCases =
{
            new object[] { Board.Direction.North, tileA },
            new object[] { Board.Direction.Northeast, tileA },
            new object[] { Board.Direction.East, tileE },
            new object[] { Board.Direction.Southeast, tileE },
            new object[] { Board.Direction.South, tileE },
            new object[] { Board.Direction.Southwest, tileE },
            new object[] { Board.Direction.West, tileA },
            new object[] { Board.Direction.Northwest, tileA },
        };
    }
}

