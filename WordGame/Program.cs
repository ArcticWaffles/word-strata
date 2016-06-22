using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Tile validUnmarkedTileA;
            Tile validMarkedTileB;
            Tile startOfWordTile2x2;
            Tile startOfWordTile3x3;

            StringBuilder newWord;
            StringBuilder wordWithSomeLetters;

            Board board3x3;
            Board board2x2;

            List<string> emptyDictionary;
            List<string> smallDictionary;
            List<string> unfoundWordDictionary;

            validUnmarkedTileA = new Tile(new Coordinates(1, 1), 'a', false);
            validMarkedTileB = new Tile(new Coordinates(2, 2), 'b', true);
            startOfWordTile2x2 = new Tile(new Coordinates(1, 0), 'c', false);
            startOfWordTile3x3 = new Tile(new Coordinates(0, 1), 'b', false);

            newWord = new StringBuilder();
            wordWithSomeLetters = new StringBuilder("cat");

            board2x2 = new Board(new char[,]
            {
                { 'a', 'b'},
                { 'c', 'd'}
            });

            board3x3 = new Board(new char[,]
            {
            { 'a', 'b', 'd' },
            { 'r', 'k', 'x' },
            { 'x', 'x', 'x' }
            });

            emptyDictionary = new List<string> { };
            smallDictionary = new List<string>
            {
            "bark",
            "bar",
            "cab"
            };

            unfoundWordDictionary = new List<string>
            {
                "daisy"
            };

            var solver = new Solver(smallDictionary, board3x3);
            bool word = solver.Walk(startOfWordTile3x3, 0, 3);
            Console.WriteLine(word);
        }
    }
}
