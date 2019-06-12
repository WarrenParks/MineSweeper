using System;
using System.Collections.Generic;

namespace MineSweeper.Core
{
    public class Board
    {
        private readonly int width;

        private readonly int height;

        public Tile[] Tiles { get; set; }
        // // public int NumberOfMines { get; }

        public Board(int width, int height, int numberOfMines)
        {
            this.width = width;
            this.height = height;

            this.Tiles = new Tile[width * height];

            for (var x = 0; x < this.Tiles.Length; x++)
            {
                this.Tiles[x] = new Tile();
            }

            var randomNumber = new Random();

            // Add the mines
            for (var x = 0; x < numberOfMines; x++)
            {
                var index = randomNumber.Next(this.Tiles.Length);
                var tile = this.Tiles[index];
                tile.IsMine = true;

                // add to the adjacent mine count.
                foreach (var adjacentTile in this.GetSurroundingTiles(index))
                {
                    adjacentTile.AdjacentMineCount++;
                }
            }
        }

        public bool CheckValidPosition(int x, int y)
        {
            return (x >= 0 && x < this.width && y >= 0 && y < this.height);
        }

        public IEnumerable<Tile> GetSurroundingTiles(int index)
        {
            var surroundingTiles = new List<Tile>();

            var x = (index) % width;
            var y = (index) / width;

            // Get all surrounding tiles 
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    // check if cell is on board and that it isn't original tile.
                    if (this.CheckValidPosition(i, j) && index != (i + j * width))
                    {
                        surroundingTiles.Add(this.Tiles[i + j * width]);
                    }
                }
            }

            return surroundingTiles;
        }

        public IEnumerable<int> GetTilesToShow(int index)
        {
            var tiles = new List<int>();

            if (!this.Tiles[index].IsVisible)
            {
                tiles.Add(index);
            }

            this.GetTilesToShowRecursive(index, tiles);

            return tiles;
        }

        private void GetTilesToShowRecursive(int index, List<int> flippableTiles)
        {
            var x = (index) % width;
            var y = (index) / width;

            // Get surrounding tiles (+(-)1 all around itself)
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    // get array index from current x(i) y(j) positions
                    var currentIndex = i + j * this.width;

                    // check if cell is on board
                    // it's not a mine
                    // and we haven't already added this to tiles to flip
                    if (this.CheckValidPosition(i, j) &&
                        !this.Tiles[currentIndex].IsMine &&
                        !flippableTiles.Contains(currentIndex))
                    {
                        flippableTiles.Add(currentIndex);

                        // only look for more flippable if this was a 0 
                        if (this.Tiles[currentIndex].AdjacentMineCount == 0)
                        {
                            this.GetTilesToShowRecursive(currentIndex, flippableTiles);
                        }
                    }
                }
            }
        }
    }
}