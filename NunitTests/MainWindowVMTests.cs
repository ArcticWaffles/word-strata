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
        public void CurrentSnake_ByDefault_ProducesCorrectGuiCoords()
        {
            viewModel.ThePath.Add(tileA);
            var snake = viewModel.CurrentSnake;
            Assert.That(snake.Points.Count.Equals(1));
            Assert.That(snake.Points[0].X, Is.EqualTo(50).Within(0.01));
            Assert.That(snake.Points[0].Y, Is.EqualTo(30).Within(0.01));
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
            var viewModel = new FakeMainWindowVM(dictionary);
            viewModel.UserWord = word;
            Assert.AreEqual(viewModel.EnableSubmit, isInDictionary);
        }
    }
}
// TODO: Tests on other public properties and methods
    // OnCollectionChanged error