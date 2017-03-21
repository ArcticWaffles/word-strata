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
            GameBoard = BoardGenerator.generateWeightedBoard(5, 5);
            //GameBoard = new Board(new char[,]
            //{
            //    { 'a', 'b', 'c', 'b'},
            //    { 'b', 'c', 'b', 'c' },
            //    { 'a', 'b', 'a', 'b' }
            //});
            Dictionary = dictionary;
        }

        public Board GameBoard { get; }

        public HashSet<string> Dictionary { get; }
    }
}

// TODO: Eventually use GameModel for player and level info.