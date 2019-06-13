using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Core;

namespace MineSweeper.UnitTests.MineSweeper.Core
{
    public class GameTests
    {
        [TestClass]
        public class WhenGameInstantiated
        {
            [TestMethod]
            public void ItStartsWithPlayerSelectionInTopLeft()
            {
                var expectedPosition = new Point(0, 0);

                var game = new Game(1, 1, 1);
                Point actualPosition = game.CurrentPosition;

                Assert.AreEqual(expectedPosition, actualPosition);
            }
        }

        [TestClass]
        public class WhenFlipTile
        {
            [TestMethod]
            public void ItMarksTheCurrentTileAsVisible()
            {
                var game = new Game(5, 5, 0);
                game.Start();
                game.CurrentPosition = new Point(2, 3);

                game.FlipTile();

                var actualFlagged = game.Board.Tiles[2 + 3 * 5].IsVisible;
                Assert.IsTrue(actualFlagged);
            }

            [TestMethod]
            public void ItSetsTileDisplayToEmptyIfNoMines()
            {
                var expectedTileDisplay = TileDisplay.Empty;
                var game = new Game(5, 5, 0);
                game.Start();
                game.CurrentPosition = new Point(1, 1);

                game.FlipTile();

                var actualTileDisplay = game.Board.Tiles[1 + 1 * 5].TileDisplay;
                Assert.AreEqual(expectedTileDisplay, actualTileDisplay);
            }

            [TestMethod]
            public void ItFlipsAllTilesIfNoMines()
            {
                var expectedTileDisplay = TileDisplay.Empty;
                var game = new Game(5, 5, 0);
                game.Start();
                game.CurrentPosition = new Point(1, 1);

                game.FlipTile();

                foreach (var actual in game.Board.Tiles)
                {
                    Assert.AreEqual(expectedTileDisplay, actual.TileDisplay);
                }
            }

            [TestMethod]
            public void ItSetsGameStatusToLostIfYouAreOnMine() 
            {
                var game = new Game(1, 1, 1);
                game.Start();

                game.FlipTile();

                Assert.AreEqual(Status.GameOver, game.Status);
            }
        }

        [TestClass]
        public class WhenFlagTile
        {
            [TestMethod]
            public void ItMarksTheCurrentTileAsFlagged()
            {
                var game = new Game(5, 5, 0);
                game.Start();
                game.CurrentPosition = new Point(2, 3);

                game.FlagTile();

                var actualFlagged = game.Board.Tiles[2 + 3 * 5].Flagged;
                Assert.IsTrue(actualFlagged);
            }

            [TestMethod]
            public void ItSetsFlagCount()
            {
                var expectedFlagCount = 1;
                var game = new Game(5, 5, 0);
                game.Start();
                game.CurrentPosition = new Point(1, 1);

                game.FlagTile();

                var actualFlagCount = game.FlagCount;
                Assert.AreEqual(expectedFlagCount, actualFlagCount);
            }

            [TestMethod]
            public void ItSetsTileDisplayToFlag()
            {
                var expectedTileDisplay = TileDisplay.Flag;
                var game = new Game(5, 5, 0);
                game.Start();
                game.CurrentPosition = new Point(1, 1);

                game.FlagTile();

                var actualTileDisplay = game.Board.Tiles[1 + 1 * 5].TileDisplay;
                Assert.AreEqual(expectedTileDisplay, actualTileDisplay);
            }

            [TestMethod]
            public void ItSetsGameStatusToWonOnLastMine() 
            {
                var game = new Game(1, 1, 1);
                game.Start();

                game.FlagTile();

                Assert.AreEqual(Status.Won, game.Status);
            }
        }

        [TestClass]
        public class WhenGetVisibleCount
        {
            public static Random random;

            [ClassInitialize]
            public static void OnClassInitialize(TestContext testContext)
            {
                random = new Random();
            }

            [TestMethod]
            public void ItReturnsOneIfOneVisible()
            {
                var expectedVisibleCount = 1;
                var game = new Game(5, 5, 0);
                game.Start();
                game.Board.Tiles[random.Next(25)].IsVisible = true;

                Assert.AreEqual(expectedVisibleCount, game.VisibleCount);
            }

            [TestMethod]
            public void ItReturnsZeroIfNoneVisible()
            {
                var expectedVisibleCount = 0;
                var game = new Game(5, 5, 0);
                game.Start();

                Assert.AreEqual(expectedVisibleCount, game.VisibleCount);
            }

            [TestMethod]
            public void ItReturnsThreeIfThreeVisible()
            {
                var expectedVisibleCount = 3;
                var game = new Game(100, 100, 0);
                game.Start();
                game.Board.Tiles[random.Next(1000)].IsVisible = true;
                game.Board.Tiles[random.Next(1000)].IsVisible = true;
                game.Board.Tiles[random.Next(1000)].IsVisible = true;

                Assert.AreEqual(expectedVisibleCount, game.VisibleCount);
            }
        }
    }
}