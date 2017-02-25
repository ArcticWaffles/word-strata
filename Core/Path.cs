using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// A list of tiles.
    /// </summary>
    public class Path : List<Tile>
    {
        public Path()
        {
        }

        public Path(IEnumerable<Tile> collection) : base(collection)
        {
        }
    }
}
