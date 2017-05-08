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
            GameBoard = BoardGenerator.generateRectangularBoard(5, 5, 3);
        //    var tilePlacement = new bool[,,]
        //{
        //    { {true, false }, {true, true }, {true, true}, {false, false } },
        //    { {true, true }, {true, true }, {true, true}, {true, true } },
        //    { {true, true }, {true, true }, {true, true}, {true, true } },
        //    { {true, true }, {true, true }, {true, true}, {true, true } },
        //    { {true, true }, {true, true }, {true, true}, {true, true } },
        //};
        //    GameBoard = BoardGenerator.generateShapedBoard(5, 4, 2, tilePlacement);
            Dictionary = dictionary;
        }

        public Board GameBoard { get; }

        public HashSet<string> Dictionary { get; }
    }
}

// TODO: Eventually use GameModel for player and level info.