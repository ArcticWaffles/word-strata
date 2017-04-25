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
        HashSet<string> dictionary = new HashSet<string>() { "apple", "banana", "sailboat", "zebra" };
        MainWindowViewModel viewModel;
        static Tile tileA = new Tile(new Coordinates(1, 2), 'a');

        [SetUp]
        public void Init()
        {
            viewModel = new MainWindowViewModel(new GameModel(dictionary));
            viewModel.ThePath.Add(tileA);
        }

        [Test]
        public void Constructor_ByDefault_ProducesValidGameBoard()
        {
            Assert.That(viewModel.GameBoard.Tiles, Is.Not.Empty);
        }

        [Test]
        public void CurrentSnake_ByDefault_ProducesCorrectGuiCoords()
        {
            var snake = viewModel.CurrentSnake;
            Assert.That(snake.Points.Count.Equals(1));
            Assert.That(snake.Points[0].X, Is.EqualTo(50).Within(0.01));
            Assert.That(snake.Points[0].Y, Is.EqualTo(30).Within(0.01));
        }

        [Test]
        public void EnableSubmit_ByDefault_IsCorrect()
        {
            Assert.That(viewModel.EnableSubmit.Equals(false));
        }
    }
}
// TODO: Tests on other public properties and methods
    // OnCollectionChanged error