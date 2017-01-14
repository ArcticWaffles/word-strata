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
        //RandomBoard tests

        [Test]
        public void RandomBoard_3x3_TileCountIsCorrect()
        {
            var board = BoardGenerator.generateRandomBoard(3, 3);
            Assert.That(board.Tiles.Count.Equals(9));
        }



        //WeightedBoard tests

        [Test]
        public void WeightedBoard_3x3_TileCountIsCorrect()
        {
            var board = BoardGenerator.generateWeightedBoard(3, 3);
            Assert.That(board.Tiles.Count.Equals(9));
        }

        [Test]
        // TODO: Improve this test. Pass in a "fake" random number generator and
        // ensure given numbers match up with the right letters
        public void WeightedBoard_LettersAreWeighted()
        {
            var board = BoardGenerator.generateWeightedBoard(10, 10);
            var tiles = board.Tiles;
            var letters = new char[100];
            for (int i = 0; i < 100; i++)
            {
                letters[i] = tiles.ElementAt(i).Letter;
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
            Assert.AreEqual(j, 12, 5);
        }
    }
}

//Tile list size matches rows x columns  - more data values?

