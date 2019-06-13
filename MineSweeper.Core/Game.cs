using System;
using System.Collections.Generic;
using System.Drawing;
using MineSweeper.Core;

namespace MineSweeper.Core
{
    public class Game
    {
        private readonly int width;

        private readonly int height;

        private readonly int mineCount;

        public int Height { get => this.height; }

        public int Width { get => this.width; }

        public int MineCount { get => this.mineCount; }

        public Status Status { get; set; }

        public Point CurrentPosition { get; set; }

        public int CurrentIndex { get => this.CurrentPosition.X + this.CurrentPosition.Y * this.Width; }
        public Board Board { get; private set; }

        public Tile CurrentTile
        {
            get => this.Board.Tiles[this.CurrentIndex];
        }

        public int FlagCount
        {
            get
            {
                var flagCount = 0;
                foreach (var tile in this.Board.Tiles)
                {
                    if (tile.Flagged)
                    {
                        flagCount++;
                    }
                }

                return flagCount;
            }
        }

        public int VisibleCount
        {
            get
            {
                var visibleCount = 0;
                foreach (var tile in this.Board.Tiles)
                {
                    if (tile.IsVisible)
                    {
                        visibleCount++;
                    }
                }

                return visibleCount;
            }
        }

        // todo: make game settings class
        public Game(int width, int height, int mineCount)
        {
            this.width = width;
            this.height = height;
            this.mineCount = mineCount;
            // //this.CurrentPosition = new Point(0,0);
        }

        public void Start()
        {
            this.Board = this.CreateBoard();
        }

        public Board CreateBoard()
        {
            var board = new Board(this.width, this.height, this.mineCount);

            return board;
        }

        public void FlagTile()
        {
            this.CurrentTile.Flagged = true;

            // todo: this logic is not correct. Just because you flagged number as mines doesn't mean it's true.
            if (this.FlagCount == this.MineCount)
            {
                this.Status = Status.Won;
            }
        }

        public void FlipTile()
        {
            // If Bomb Lose
            if (this.CurrentTile.IsMine)
            {
                this.Status = Status.GameOver;
                return;
            }

            var tiles = this.Board.GetTilesToShow(this.CurrentIndex);

            foreach (var index in tiles)
            {
                this.Board.Tiles[index].IsVisible = true;
            }
        }
    }
}