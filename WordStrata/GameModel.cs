using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WordStrata
{
    public class GameModel: IGameModel
    {
        public GameModel(HashSet<string> dictionary)
        {
            // GameBoard = BoardGenerator.generateRectangularBoard(5, 5, 3);
            // GameBoard = BoardGenerator.generateShapedBoard(5, 4, 2, TestBoardA);
            // GameBoard = BoardGenerator.generateShapedBoard(4, 6, 2, TestBoardB);
            GameBoard = BoardGenerator.generateShapedBoard(4, 6, 3, TestBoardC);

            Dictionary = dictionary;
        }

        public Board GameBoard { get; }

        public HashSet<string> Dictionary { get; }

        private bool[,,] TestBoardA { get; } = new bool[,,]
        {
            { {true, false }, {true, true }, {true, true}, {false, false } },
            { {true, true }, {true, true }, {true, true}, {true, true } },
            { {true, true }, {true, true }, {true, true}, {true, true } },
            { {true, true }, {true, true }, {true, true}, {true, true } },
            { {true, true }, {true, true }, {true, true}, {true, true } },
        };

        private bool[,,] TestBoardB { get; } = new bool[,,]
        {
            { {true, false }, {true, true }, {true, true}, {false, false }, {true, true }, {true, true } },
            { {true, true }, {true, true }, {true, true}, {true, true }, {true, true }, {true, true } },
            { {false, false }, {true, true }, {true, true }, {true, true}, {true, true }, {true, true } },
            { {true, true }, {true, true }, {true, true}, {true, true }, {true, true }, {true, true } },
        };

        private bool[,,] TestBoardC { get; } = new bool[,,]
{
            { {true, false, false }, {true, true, true }, {true, true, true}, {false, false, false }, {true, true, true }, {true, true, false } },
            { {true, true, true }, {true, true, true }, {true, true, true}, {true, true, true }, {true, true, false }, {true, true, true } },
            { {false, false, true }, {true, true, true }, {true, true, true }, {true, true, true}, {true, true, true }, {true, true, true } },
            { {true, true, true }, {true, true, true }, {true, true, true}, {true, true, true }, {true, true, true }, {true, true, true } },
};
    }
}

// TODO: Eventually use GameModel for player and level info.