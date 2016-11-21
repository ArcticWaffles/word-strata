using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordStrata.Solve;



namespace UnitTests
{
    [TestClass]
    public class BoardGeneratorTests
    {
        //RandomBoard tests

        [TestMethod]
        public void RandomBoard_3x3_TileCountIsCorrect()
        {
            var board = BoardGenerator.generateRandomBoard(3,3);
            Assert.AreEqual(board.Tiles.Count, 9);
        }



        //WeightedBoard tests

        [TestMethod]
        public void WeightedBoard_3x3_TileCountIsCorrect()
        {
            var board = BoardGenerator.generateRandomBoard(3, 3);
            Assert.AreEqual(board.Tiles.Count, 9);
        }

        [TestMethod]
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
            foreach(var letter in query)
            {
                j++;
            }
            Assert.AreEqual(j, 12, 4);
        }
    }
}

//Tile list size matches rows x columns  - more data values?


