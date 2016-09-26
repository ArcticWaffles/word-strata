using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStrata.Solver;

namespace WordStrata
{
    public class MainWindowContext
    {
        private Board gameBoard = BoardGenerator.RandomBoardGenerator(3, 4);
        
        public Board GameBoard
        {
            get
            {
                return gameBoard;
            }

            set
            {
                gameBoard = value;
            }
        }

        private string theWord;

        public string TheWord
        {
            get
            {
                return theWord;
            }

            set
            {
                theWord = value;
            }
        }
    }

}
