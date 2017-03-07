using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Core;

namespace NUnitTests
{
    [TestFixture]
    class SnakeTests
    {
        static Board board3x3;
        static Board board4x4;

        [SetUp]
        public void Init()
        {
            board3x3 = new Board(new char[,]
            {
                { 'a', 'b', 'a' },
                { 'b', 'a', 'b' },
                { 'a', 'b', 'a' }
            });
        }

    }
}
