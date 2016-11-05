using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordStrata;

namespace NunitTests
{
    [TestFixture]
    public class MainWindowVMTests
    {
        HashSet<string> dictionary = new HashSet<string>() { "apple", "banana", "sailboat", "zebra" };

        //[OneTimeSetUp]
        //public void Init()
        //{
        //    dictionary.Add("apple");
        //    dictionary.Add("banana");
        //    dictionary.Add("sailboat");
        //    dictionary.Add("zebra");
        //}
        
        [Test]
        public void Constructor_ByDefault_ProducesValidGameBoard()
        {
            var viewModel = new MainWindowViewModel(dictionary);
            Assert.That(viewModel.GameBoard.Tiles, Is.Not.Empty);
        }

    }
}
