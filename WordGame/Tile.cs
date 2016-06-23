using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame
{
    public class Tile : GridSquare
    {
        private bool hasMark;
        public bool HasMark
        {
            get { return hasMark; }
            set { hasMark = value; }
        }

        readonly char letter;
        public char Letter
        {
            get { return letter; }
        }

        public Tile(Coordinates coordsIn, char letterIn, bool hasMarkIn) : base(coordsIn)
        {
            letter = letterIn;
            hasMark = hasMarkIn;
        }

        public void Mark()
        {
            hasMark = true;
        }

        public void Unmark()
        {
            hasMark = false;
        }


    }
}
