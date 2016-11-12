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

        ////List of tiles the user has clicked, removed when they are unclicked
        //public List<TileViewModel> UserSelections { get; set; } = new List<TileViewModel>();

        ////The last tile in userSelections
        //public TileViewModel CurrentGuiTile { get; set; }

        ////The word the user is building
        //public string UserWord { get; set; }

    }
}

// TODO: Model shouldn't store view models?
// TODO: Resolve slang use of GuiTile for TileViewModel
// TODO: GameModel unit testing? And remove tests from MWVM section that don't belong anymore