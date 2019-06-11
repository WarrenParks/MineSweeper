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

        public IEnumerable<Tile> GetSurroundingTiles(int index)
        {
            var surroundingTiles = new List<Tile>();

            var x = (index) % width;
            var y = (index) / width;
            Console.WriteLine($"x: {x}, y: {y}, index: {index}, width: {width}, height: {height}");

            // // // top left
            // // if (index - width < 0)
            // // {

            // // }

            // // // Right
            // // if (x + 1 < this.width)
            // // {

            // //     surroundingTiles.Add(this.Tiles[index + 1]);
            // // }

            // // // Bottom
            // // if (y + 1 < this.height)
            // // {
            // //     surroundingTiles.Add(this.Tiles[index + width]);
            // // }

            // // // Bottom Right
            // // if (x + 1 < this.width && y + 1 < height)
            // // {
            // //     surroundingTiles.Add(this.Tiles[index + width + 1]);
            // // }

            // Get all surrounding tiles 
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    // check if cell is on board
                    if (this.CheckValidPosition(i, j))
                    {
                        surroundingTiles.Add(this.Tiles[i + j * width]);
                    }
                }
            }

            return surroundingTiles;
        }

        public IEnumerable<int> GetTilesToShow(int index)
        {
            //var max = this.Tiles.Length;
            var wasHere = new bool[width, height];
            var correctPath = new bool[width, height]; // The solution to the maze

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

            // // if (this.Tiles[index].IsFlippable)
            // // {
            // //     flippableTiles.Add(index);
            // // }

            // Up
            // // var flippableIndex = index - width;
            // // if (y - 1 >= 0 && !flippableTiles.Contains(index - width) && this.Tiles[index - width].IsFlippable)
            // // {
            // //     flippableTiles.Add(index - width);
            // //     this.GetRecursiveFlippableTiles(index - width, flippableTiles);
            // // }

            // // // Right
            // // if (x + 1 < width && !flippableTiles.Contains(index + 1))
            // // {
            // //     flippableTiles.Add(index + 1);
            // //     this.GetRecursiveFlippableTiles(index + 1, flippableTiles);
            // // }

            // // // Down
            // // if (y + 1 < height && !flippableTiles.Contains(index + width))
            // // {
            // //     flippableTiles.Add(index + width);
            // //     this.GetRecursiveFlippableTiles(index + width, flippableTiles);
            // // }

            // // // Left
            // // if (x - 1 >= 0 && !flippableTiles.Contains(index - 1))
            // // {
            // //     flippableTiles.Add(index - 1);
            // //     this.GetRecursiveFlippableTiles(index - 1, flippableTiles);
            // // }

            // Get all surrounding tiles 
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    var arrayIndex = i + j * this.width;
                    // check if cell is on board
                    if (this.CheckValidPosition(i, j) && 
                        !this.Tiles[arrayIndex].IsMine && 
                        !flippableTiles.Contains(arrayIndex))
                    {
                        flippableTiles.Add(arrayIndex);

                        // only look for more flippable if this was a 0 
                        if (this.Tiles[arrayIndex].AdjacentMineCount == 0)
                        {
                            this.GetTilesToShowRecursive(arrayIndex, flippableTiles);
                        }
                        //surroundingTiles.Add(this.Tiles[i + j * width]);
                    }
                }
            }
        }

        public bool CheckValidPosition(int x, int y)
        {
            return (x >= 0 && x < this.width && y >= 0 && y < this.height);
        }
    }
}