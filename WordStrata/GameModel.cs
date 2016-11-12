using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata.Solve;

namespace WordStrata
{
    public class GameModel: IGameModel
    {
        public GameModel(HashSet<string> dictionary)
        {
            GameBoard = BoardGenerator.generateWeightedBoard(5, 5);
            solver = new Solver(dictionary, GameBoard);
            Dictionary = dictionary;
        }

        public Board GameBoard { get; }

        private Solver solver;

        public HashSet<String> Dictionary { get; }
    }
}

// TODO: GameModel unit testing? And remove tests from MWVM section that don't belong anymore