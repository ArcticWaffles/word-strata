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
        static char[,,] array3x3x1 = new char[,,]
        {
            { {'a' }, {'b' }, {'c' } },
            { {'d' }, {'e' }, {'f' } },
            { {'g' }, {'h' }, {'i' } }
        };

        static char[,,] array2x2x1 = new char[,,]
        {
            { {'a' }, {'b' } },
            { {'d' }, {'e' } }
        };

        static char[,,] array3x3x2 = new char[,,]
        {
            { {'s', ' ' }, {'k', ' ' }, {'c', 'l' } },
            { {'d', 'm' }, {'e', 'n' }, {'f', 'o' } },
            { {'g', 'p' }, {'h', 'q' }, {'r', ' ' } }
        };

        static char[,,] array2x2x3 = new char[,,]
        {
            { {'g', ' ', ' ' }, {' ', ' ', ' ' } },
            { {'d', 'j', 'k' }, {'e', 'l', 'm' } }
        };


        //Constructor tests

        [Test, TestCaseSource("RowColumnLayerCases")]
        public void RowsColumnsAndLayers_CountIsCorrect(char[,,] letters, int expectedRows, int expectedColumns, int expectedLayers)
        {
            var board = new Board(letters);
            Assert.AreEqual(board.Rows, expectedRows);
            Assert.AreEqual(board.Columns, expectedColumns);
            Assert.AreEqual(board.Layers, expectedLayers);
        }

        static object[] RowColumnLayerCases =
        {
            new object[] { array3x3x1, 3, 3, 1 },
            new object[] { array2x2x1, 2, 2, 1 },
            new object[] { array3x3x2, 3, 3, 2 },
            new object[] { array2x2x3, 2, 2, 3 },
        };


        [Test, TestCaseSource("GridsquaresAndTilesCases")]
        public void GridsquaresAndTiles_CountIsCorrect(char[,,] letters, int expectedGridsquareCount, int expectedTileCount)
        {
            var board = new Board(letters);
            Assert.AreEqual(board.Gridsquares.Count, expectedGridsquareCount);
            Assert.AreEqual(board.Tiles.Count, expectedTileCount);
        }

        static object[] GridsquaresAndTilesCases =
        {
            new object[] { array3x3x1, 9, 9 },
            new object[] { array2x2x1, 4, 4 },
            new object[] { array3x3x2, 18, 9 },
            new object[] { array2x2x3, 12, 3 },
        };


        [Test, TestCaseSource("AllArrays")]
        public void Gridsquares_ContentIsCorrect(char[,,] letters)
        {
            var board = new Board(letters);
            var alreadyUsedCoords = new List<Coordinates>();
            foreach (var square in board.Gridsquares)
            {
                if (alreadyUsedCoords.Any(coord => coord.X == square.Coords.X && coord.Y == square.Coords.Y && coord.Z == square.Coords.Z))
                {
                    Assert.Fail("Coordinates weren't unique");
                }
                alreadyUsedCoords.Add(square.Coords);
                if (square is Tile)
                {
                    Assert.AreEqual((square as Tile).Letter, letters[square.Coords.X, square.Coords.Y, square.Coords.Z]);
                }
            }
        }

        static char[][,,] AllArrays =
        {
            array3x3x1,
            array2x2x1,
            array3x3x2,
            array2x2x3,
        };


        // ConvertTilesToHoles tests

        [Test, TestCaseSource("AllArrays")]
        public void ConvertToHoles_WholeBoard_AllSquaresAreHoles(char[,,] letters)
        {
            var board = new Board(letters);
            var tilePath = new List<Tile>();
            foreach(var square in board.Gridsquares)
            {
                if (square is Tile) tilePath.Add(square as Tile);
            }
            board.ConvertTilesToHoles(tilePath);
            foreach (var square in board.Gridsquares)
            {
                Assert.That(square, Is.InstanceOf(typeof(Hole)));
            }
            Assert.That(board.Tiles, Is.Empty);
        }

        [Test, TestCaseSource("TilesToHolesCases")]
        public void ConvertToHoles_PartialBoard_RemainingTileCountIsCorrect(char[,,] letters, int remainingTiles)
        {
            var board = new Board(letters);
            var tiles = new List<Tile>();
            tiles.Add(board[0, 0] as Tile);
            tiles.Add(board[1, 1] as Tile);
            board.ConvertTilesToHoles(tiles);
            Assert.That(board.Tiles.Count.Equals(remainingTiles));
        }

        [Test, TestCaseSource("AllArrays")]
        public void ConvertToHoles_PartialBoard_GridsquaresRemainUnique(char[,,] letters)
        {
            var board = new Board(letters);
            var tiles = new List<Tile>();
            tiles.Add(board[0, 0] as Tile);
            tiles.Add(board[1, 1] as Tile);
            board.ConvertTilesToHoles(tiles);
            Assert.That(board.Gridsquares, Is.Unique);
        }

        static object[] TilesToHolesCases =
        {
            new object[] { array3x3x1, 7 },
            new object[] { array2x2x1, 2 },
            new object[] { array3x3x2, 8 },
            new object[] { array2x2x3, 2 },
        };


        //GetNeighbor tests

        [Test, TestCaseSource("GetNeighbor2DCases")]
        public void GetNeighbor_SingleLayerBoard_NeighborIsCorrect(Board.Direction direction, char expectedLetter,
            int expectedCoordX, int expectedCoordY)
        {
            var board = new Board(array3x3x1);
            Tile centralTile = board[1, 1] as Tile;
            var result = board.GetNeighbor(centralTile, direction) as Tile;
            Assert.AreEqual(result.Letter, expectedLetter);
            Assert.AreEqual(result.Coords.X, expectedCoordX);
            Assert.AreEqual(result.Coords.Y, expectedCoordY);
        }

        static object[] GetNeighbor2DCases =
        {
            new object[] { Board.Direction.North, 'b', 0, 1 },
            new object[] { Board.Direction.Northeast, 'c', 0, 2 },
            new object[] { Board.Direction.East, 'f', 1, 2 },
            new object[] { Board.Direction.Southeast, 'i', 2, 2 },
            new object[] { Board.Direction.South, 'h', 2 , 1 },
            new object[] { Board.Direction.Southwest, 'g', 2, 0 },
            new object[] { Board.Direction.West, 'd', 1, 0 },
            new object[] { Board.Direction.Northwest, 'a', 0, 0 },
        };

        [Test, TestCaseSource("GetNeighbor3DCases")]
        public void GetNeighbor_MultiLayerBoard_NeighborIsCorrect(Board.Direction direction, char expectedLetter,
            int expectedCoordX, int expectedCoordY, int expectedCoordZ)
        {
            var board = new Board(array3x3x2);
            Tile centralTile = board[1, 1] as Tile;
            var result = board.GetNeighbor(centralTile, direction) as Tile;
            Assert.AreEqual(result.Letter, expectedLetter);
            Assert.AreEqual(result.Coords.X, expectedCoordX);
            Assert.AreEqual(result.Coords.Y, expectedCoordY);
            Assert.AreEqual(result.Coords.Z, expectedCoordZ);
        }

        static object[] GetNeighbor3DCases =
        {
            new object[] { Board.Direction.North, 'k', 0, 1, 0 },
            new object[] { Board.Direction.Northeast, 'l', 0, 2, 1 },
            new object[] { Board.Direction.East, 'o', 1, 2, 1 },
            new object[] { Board.Direction.Southeast, 'r', 2, 2, 0 },
            new object[] { Board.Direction.South, 'q', 2 , 1, 1 },
            new object[] { Board.Direction.Southwest, 'p', 2, 0, 1 },
            new object[] { Board.Direction.West, 'm', 1, 0, 1 },
            new object[] { Board.Direction.Northwest, 's', 0, 0, 0 },
        };


        [Test, TestCaseSource("OffGridCases")]
        public void GetNeighbor_GoesBeyondGrid_ReturnsHole(Board.Direction direction, int originCoordsX, 
            int originCoordsY)
        {
            var board = new Board(array2x2x1);
            var origin = board[originCoordsX, originCoordsY];
            var neighbor = board.GetNeighbor(origin as Tile, direction);
            Assert.That(neighbor, Is.InstanceOf(typeof(Hole)));
        }

        static object[] OffGridCases =
        {
            new object[] { Board.Direction.North, 0, 0},
            new object[] { Board.Direction.Northeast, 0, 0 },
            new object[] { Board.Direction.East, 1, 1 },
            new object[] { Board.Direction.Southeast, 1, 1 },
            new object[] { Board.Direction.South, 1, 1 },
            new object[] { Board.Direction.Southwest, 1, 1 },
            new object[] { Board.Direction.West, 0, 0 },
            new object[] { Board.Direction.Northwest, 0, 0 },
        };


        // TopLayer tests

        [Test, TestCaseSource("TopLayerCases")]
        public void TopLayer_IsCorrect(char[,,] boardLetters, List<char> tileLetters, int expectedHoles)
        {
            var board = new Board(boardLetters);
            var letters = new List<char>();
            int holes = 0;
            foreach(var square in board.TopLayer)
            {
                if (square is Tile) letters.Add((square as Tile).Letter);
                else holes++;
            }
            Assert.That(letters, Is.EquivalentTo(tileLetters));
            Assert.That(holes, Is.EqualTo(expectedHoles));
        }

        static object[] TopLayerCases =
        {
            new object[] { array3x3x2, new List<char> {'s','k', 'l', 'm', 'n', 'o', 'p', 'q', 'r' }, 0 },
            new object[] { array2x2x3, new List<char> {'g','k', 'm' }, 1 },
        };

        [Test, TestCaseSource("TopLayerModifiedCases")]
        public void TopLayer_AfterConvertingSomeTilesToHoles_IsCorrect(char[,,] boardLetters, List<char> tileLetters, int expectedHoles)
        {
            var board = new Board(boardLetters);
            var letters = new List<char>();
            int holes = 0;
            var tileList = new List<Tile>();
            tileList.Add(board[0, 0] as Tile);
            tileList.Add(board[1, 0] as Tile);
            board.ConvertTilesToHoles(tileList);
            foreach (var square in board.TopLayer)
            {
                if (square is Tile) letters.Add((square as Tile).Letter);
                else holes++;
            }
            Assert.That(letters, Is.EquivalentTo(tileLetters));
            Assert.That(holes, Is.EqualTo(expectedHoles));
        }

        static object[] TopLayerModifiedCases =
        {
            new object[] { array3x3x2, new List<char> {'k', 'l', 'd', 'n', 'o', 'p', 'q', 'r' }, 1 },
            new object[] { array2x2x3, new List<char> {'j', 'm' }, 2 },
        };
    }
}

// TODO: Tiles list tests

