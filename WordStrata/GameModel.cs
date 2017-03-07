﻿using System;
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
            //    { 'a', 'b', 'c' },
            //    { 'b', 'c', 'b' },
            //    { 'a', 'b', 'a' }
            //});
            Dictionary = dictionary;
        }

        public Board GameBoard { get; }

        public HashSet<string> Dictionary { get; }
    }
}

// TODO: Eventually use GameModel for player and level info.