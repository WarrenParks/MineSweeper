using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper.Core;

namespace MineSweeper.UnitTests.MineSweeper.Core
{
    public class BoardTests
    {
        [TestClass]
        public class WhenInstatiated
        {
            [TestMethod]
            public void ItCreatesABoardWithZeroMines()
            {
                var expectedBombCount = 0;
                var board = new Board(5, 5, expectedBombCount);

                int actualBombCount = 0;

                foreach (var a in board.Tiles)
                {
                    if (a.IsMine)
                    {
                        actualBombCount++;
                    }
                }

                Assert.AreEqual(expectedBombCount, actualBombCount);
            }

            [TestMethod]
            public void ItCreatesABoardWithOneMine()
            {
                var expectedBombCount = 1;
                var board = new Board(5, 5, expectedBombCount);
                int actualBombCount = 0;

                foreach (var a in board.Tiles)
                {
                    if (a.IsMine)
                    {
                        actualBombCount++;
                    }
                }

                Assert.AreEqual(expectedBombCount, actualBombCount);
            }

            [TestMethod]
            public void ItCreatesABoardWithThreeMines()
            {
                var expectedBombCount = 3;
                var board = new Board(5, 5, expectedBombCount);
                int actualBombCount = 0;

                foreach (var a in board.Tiles)
                {
                    if (a.IsMine)
                    {
                        actualBombCount++;
                    }
                }

                Assert.AreEqual(expectedBombCount, actualBombCount);
            }

            [TestMethod]
            public void ItSetsNumberOfMinesCorrectly()
            {
                var expectedAdjacentMineCount = 1;
                var width = 5;
                var board = new Board(width, 5, 1);

                // Get Tile with Mine.
                ////Tile mineTile = board.Tiles.First((t) => t.IsMine);
                ////foreach (var tile in board.Tiles)
                // Fine Tile with Mine
                for (var i = 0; i < board.Tiles.Length; i++)
                {
                    if (board.Tiles[i].IsMine)
                    {
                        // All tiles around it should show 1.
                        //board.Tiles[i - 1].IsVisible = true;
                        var surroundingTiles = board.GetSurroundingTiles(i);

                        foreach (var tile in surroundingTiles)
                        {
                            Assert.AreEqual(expectedAdjacentMineCount, tile.AdjacentMineCount);
                        }
                    }
                }
            }
        }

        [TestClass]
        public class WhenGetSurroundingTiles
        {
            [TestMethod]
            public void ItReturnsThreeSurroundingTilesIfTwoByTwoBoard()
            {
                var board = new Board(2, 2, 0);
                var expectedTiles = new[] { board.Tiles[1], board.Tiles[2], board.Tiles[3] }; // Should return all other tiles.
                
                var surroundingTiles = board.GetSurroundingTiles(0);

                CollectionAssert.AreEquivalent(expectedTiles, surroundingTiles.ToArray());
            }

            public void ItReturnsNothingIfOneByOneBoard() {
                var expectedTileCount = 0;
                var board = new Board(1, 1, 0);

                var surroundingTiles = board.GetSurroundingTiles(0);

                Assert.AreEqual(expectedTileCount, surroundingTiles.Count());
            }
        }

        [TestClass]
        public class WhenGetTilesToShow 
        {
            [TestMethod]
            public void ItReturnsAllTilesIfNoMines() 
            {
                var board = new Board(3, 3, 0);

                var actualTiles = board.GetTilesToShow(0);

                Assert.AreEqual(9, actualTiles.Count());
            }

            [TestMethod]
            public void ItReturnsAllTilesIfNoMinesStartAtEnd() 
            {
                var board = new Board(3, 3, 0);

                var actualTiles = board.GetTilesToShow(8);

                Assert.AreEqual(9, actualTiles.Count());
            }
        }

        [TestClass]
        public class WhenCheckValidPosition 
        {
            [TestMethod]
            public void ItReturnsFalseIfOutOfBounds() {
                var board = new Board(1, 1, 0);

                Assert.IsFalse(board.CheckValidPosition(-1, 0));
                Assert.IsFalse(board.CheckValidPosition(1, 0));
                Assert.IsFalse(board.CheckValidPosition(0, 1));
                Assert.IsFalse(board.CheckValidPosition(0, -1));
            }

            [TestMethod]
            public void ItReturnsTrueIfInBounds() {
                var board = new Board(2, 2, 0);

                Assert.IsTrue(board.CheckValidPosition(0, 0));
                Assert.IsTrue(board.CheckValidPosition(1, 0));
                Assert.IsTrue(board.CheckValidPosition(1, 1));
                Assert.IsTrue(board.CheckValidPosition(0, 1));
            }
        }
    }
}