using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WordStrata
{
    public class GameModel : IGameModel
    {
        public GameModel(HashSet<string> dictionary)
        {
            Dictionary = dictionary;
            LevelNumber = 1;
        }

        public Board GameBoard { get; private set; }

        public HashSet<string> Dictionary { get; }

        private int levelNumber;
        public int LevelNumber
        {
            get
            {
                return levelNumber;
            }

            set
            {
                if (value == levelNumber) return;
                levelNumber = value;
                GetLevel(levelNumber);
            }
        }

        private void GetLevel(int levelNumber)
        {
            if (LevelNumber < Levels.GameLevels.Count)
            {
                var level = Levels.GameLevels[LevelNumber - 1];
                GameBoard = BoardGenerator.generateShapedBoard(level.TileLocations);
            }
            // TODO: else they have played all the levels-- give stats, provide options for revisiting old levels, etc.
        }
    }
}

// TODO: Eventually use GameModel for player and level info.
// TODO: Should previous boards be kept so the player can try the exact same level again for a higher score?