using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordStrata;
using Core;


namespace NUnitTests
{
    [TestFixture]
    class BoardGeneratorTests
    {
        //WeightedBoard tests

        [TestCase(3, 3, 1)]
        [TestCase(3, 3, 2)]
        [TestCase(1, 4, 1)]
        [TestCase(2, 4, 3)]
        public void WeightedBoard_ByDefault_GridsquareCountIsCorrect(int rows, int columns, int layers)
        {
            var board = BoardGenerator.generateRectangularBoard(rows, columns, layers);
            Assert.That(board.Gridsquares.Count.Equals(rows * columns * layers));
        }

        [Test]
        // TODO: Improve this test?
        public void WeightedBoard_LettersAreWeighted()
        {
            var board = BoardGenerator.generateRectangularBoard(10, 10, 1);
            var tiles = board.Tiles;
            var letters = new char[100];
            for (int i = 0; i < 100; i++)
            {
                letters[i] = tiles[i].Letter;
            }
            var query =
                from letter in letters
                where letter == 'E'
                select letter;
            int j = 0;
            foreach (var letter in query)
            {
                j++;
            }
            Assert.AreEqual(12, j, 6);
        }
    }
}

