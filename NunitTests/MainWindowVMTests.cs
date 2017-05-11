using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordStrata;
using Core;

namespace NUnitTests
{
    [TestFixture]
    public class MainWindowVMTests
    {
        HashSet<string> dictionary = new HashSet<string>() { "bat", "tab" };
        MainWindowViewModel viewModel;
        static Tile tileA = new Tile(new Coordinates(1, 2, 0), 'a');

        [SetUp]
        public void Init()
        {
            viewModel = new MainWindowViewModel(new GameModel(dictionary));
        }

        [Test]
        public void Constructor_ByDefault_ProducesValidGameBoard()
        {
            Assert.That(viewModel.GameBoard.Tiles, Is.Not.Empty);
        }

        [Test]
        public void EnableSubmit_EmptyBoard_IsFalse()
        {
            Assert.That(viewModel.EnableSubmit.Equals(false));
        }

        [TestCase("bat", true)]
        [TestCase("BAT", true)]
        [TestCase("tab", true)]
        [TestCase("tba", false)]
        public void EnableSubmit_IsTrueOnlyIfWordIsValid(string word, bool isInDictionary)
        {
            viewModel.ThePath = new FakeTilePath(word);
            Assert.AreEqual(viewModel.EnableSubmit, isInDictionary);
        }
    }
}
// TODO: Tests on other public properties and methods
    // Test for OnCollectionChanged error