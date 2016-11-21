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
        MainWindowViewModel viewModel;


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
    }

}
// TODO: Tests on other public properties and methods
// TODO: UserTileSelections tests?
// TODO: DictionaryModel tests?