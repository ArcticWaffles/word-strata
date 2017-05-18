using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WordStrata
{
    public static class Levels
    {
        public static List<Level> GameLevels { get; }

        private static readonly Level level1;
        private static readonly Level level2;
        private static readonly Level level3;

        private static bool[,,] tiles1;
        private static bool[,,] tiles2;
        private static bool[,,] tiles3;

        static Levels()
        {
            tiles1 = new bool[5, 4, 2]
            {
                { {true, false }, {true, true }, {true, true}, {false, false } },
                { {true, true }, {true, true }, {true, true}, {true, true } },
                { {true, true }, {true, true }, {true, true}, {true, true } },
                { {true, true }, {true, true }, {true, true}, {true, true } },
                { {true, true }, {true, true }, {true, true}, {true, true } },
            };

            tiles2 = new bool[4, 6, 2]
            {
                { {true, false }, {true, true }, {true, true}, {false, false }, {true, true }, {true, true } },
                { {true, true }, {true, true }, {true, true}, {true, true }, {true, true }, {true, true } },
                { {false, false }, {true, true }, {true, true }, {true, true}, {true, true }, {true, true } },
                { {true, true }, {true, true }, {true, true}, {true, true }, {true, true }, {true, true } },
            };

            tiles3 = new bool[4, 6, 3]
            {
                { {true, false, false }, {true, true, true }, {true, true, true}, {false, false, false }, {true, true, true }, {true, true, false } },
                { {true, true, true }, {true, true, true }, {true, true, true}, {true, true, true }, {true, true, false }, {true, true, true } },
                { {false, false, true }, {true, true, true }, {true, true, true }, {true, true, true}, {true, true, true }, {true, true, true } },
                { {true, true, true }, {true, true, true }, {true, true, true}, {true, true, true }, {true, true, true }, {true, true, true } },
            };

            level1 = new Level(tiles1, Colors.CadetBlue, Colors.LightSlateGray);
            level2 = new Level(tiles2, Colors.DarkSeaGreen, Colors.DarkOliveGreen);
            level3 = new Level(tiles3, Colors.LightSteelBlue, Colors.SlateGray);

            GameLevels = new List<Level>();
            GameLevels.Add(level1);
            GameLevels.Add(level2);
            GameLevels.Add(level3);
        }
    }
}