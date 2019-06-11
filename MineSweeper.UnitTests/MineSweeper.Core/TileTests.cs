using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Core;

namespace MineSweeper.UnitTests.MineSweeper.Core
{
    public class TileTests
    {
        [TestClass]
        public class WhenGetTileDisplay
        {
            [TestMethod]
            public void ItReturnsTileDisplayOfFlagIfTileIsFlagged()
            {
                var expectedTileDisplay = TileDisplay.Flag;
                var tile = new Tile();
                tile.Flagged = true;

                var actualTileDisplay = tile.TileDisplay;

                Assert.AreEqual(expectedTileDisplay, actualTileDisplay);
            }

            [TestMethod]
            public void ItReturnsTileDisplayOfMineIfTileIsVisibleAndMine()
            {
                var expectedTileDisplay = TileDisplay.Mine;
                var tile = new Tile();
                tile.IsVisible = true;
                tile.IsMine = true;

                var actualTileDisplay = tile.TileDisplay;

                Assert.AreEqual(expectedTileDisplay, actualTileDisplay);
            }

            [TestMethod]
            public void ItReturnsTileDisplayOfInitialIfTileIsNotVisibleAndMine()
            {
                var expectedTileDisplay = TileDisplay.Initial;
                var tile = new Tile();
                tile.IsVisible = false;
                tile.IsMine = true;

                var actualTileDisplay = tile.TileDisplay;

                Assert.AreEqual(expectedTileDisplay, actualTileDisplay);
            }
        }

        // // [TestClass]
        // // public class WhenIsFlippable
        // // {
        // //     [TestMethod]
        // //     public void ItIsWhenNotVisible() 
        // //     {
        // //         var expectedIsFlippable = true;

        // //         var tile = new Tile();
        // //         tile.IsVisible = false; // default but making it obvious ;-)

        // //         var actualIsFlippable = tile.IsFlippable;

        // //         Assert.AreEqual(expectedIsFlippable, actualIsFlippable);
        // //     }

        // //     [TestMethod]
        // //     public void ItIsNotWhenVisible() 
        // //     {
        // //         var tile = new Tile();
        // //         tile.IsVisible = true;

        // //         var actualIsFlippable = tile.IsFlippable;

        // //         Assert.IsFalse(actualIsFlippable);
        // //     }

        // //     [TestMethod]
        // //     public void ItIsNotWhenMine() {
        // //         var tile = new Tile();

        // //         var actualIsFlippable = tile.IsFlippable;

        // //         Assert.IsFalse(actualIsFlippable);
        // //     }
        // // }
    }
}