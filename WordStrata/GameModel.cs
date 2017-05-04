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
            //GameBoard = BoardGenerator.generateRectangularBoard(5, 5, 0);
            var coords = new List<Coordinates>
            {
                new Coordinates(0,0,0),
                new Coordinates(0,0,1),
                new Coordinates(0,3,0),
                new Coordinates(0,3,1),
                new Coordinates(3,0,0),
                new Coordinates(3,0,1),
                new Coordinates(3,3,0),
                new Coordinates(3,3,1),
            };
            GameBoard = BoardGenerator.generateShapedBoard(4, 4, 2, coords);
            Dictionary = dictionary;
        }

        public Board GameBoard { get; }

        public HashSet<string> Dictionary { get; }
    }
}

// TODO: Eventually use GameModel for player and level info.