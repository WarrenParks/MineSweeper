using System;
using System.Drawing;
using MineSweeper.Core;

namespace MineSweeper
{
    public class Program
    {
        static void Main(string[] args)
        {
            // todo: get from args.
            var numberOfColumns = 10;
            var numberOfRows = 10;
            var numberOfMines = 10;

            var game = new Game(numberOfColumns, numberOfRows, numberOfMines);
            game.Start();

            while (game.Status == Status.Running)
            {
                Console.Clear();
                OutputStats(game.FlagCount, game.MineCount);
                OutputBoard(game.Board.Tiles, numberOfColumns, numberOfRows, game.CurrentPosition);
                ShowDebugInfo(game.VisibleCount, numberOfColumns * numberOfMines);

                Console.SetCursorPosition(game.CurrentPosition.X * 4 + 1, game.CurrentPosition.Y * 2 + 2); // 3 tile width + 1 to center

                var key = Console.ReadKey().Key;
                ProcessInput(game, key);
            }

            if (game.Status == Status.GameOver) 
            {
                Console.Clear();
                OutputStats(game.FlagCount, game.MineCount);
                Console.WriteLine("Sorry. You Lost.");
                Console.WriteLine();
                ShowDebugInfo(game.VisibleCount, numberOfColumns * numberOfMines);
            }

            if (game.Status == Status.Won) 
            {
                Console.Clear();
                OutputStats(game.FlagCount, game.MineCount);
                Console.WriteLine("Congratulations. You Won!!");
                Console.WriteLine();
                ShowDebugInfo(game.VisibleCount, numberOfColumns * numberOfMines);
            }
        }

        private static void OutputStats(int numberOfFlags, int numberOfMines)
        {
            Console.WriteLine($"Flagged: {numberOfFlags} / Mines: {numberOfMines}");
            Console.WriteLine();
        }

        private static void ShowDebugInfo(int numberOfVisible, int total)
        {
            Console.WriteLine($"Visible: {numberOfVisible} / {total}");
        }

        private static void ProcessInput(Game game, ConsoleKey key)
        {
            switch(key)
            {
                // Directions
                case ConsoleKey.DownArrow:
                    game.CurrentPosition += new Size(0, 1);
                    break;
                case ConsoleKey.UpArrow:
                    game.CurrentPosition -= new Size(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    game.CurrentPosition -= new Size(1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    game.CurrentPosition += new Size(1, 0);
                    break;
                    // Flip Tile
                case ConsoleKey.Spacebar:
                    game.FlipTile();
                    break;
                    // Flag Tile
                case ConsoleKey.F:
                    game.FlagTile();
                    break;
                    // Reset Board
                case ConsoleKey.R:
                    game.Start();
                    break;
                    // Quit Game
                case ConsoleKey.Q:
                    Console.Clear();
                    Console.WriteLine("Thanks For Playing!");
                    game.Status = Status.Quit;
                    break;
                default:
                    Console.WriteLine("Unknown Key");
                    break;
            }
        }

        public static void OutputBoard(Tile[] tiles, int width, int height, Point currentPosition)
        {
            for (var y = 0; y < width; y++)
            {
                for (var x = 0; x < height; x++)
                {
                    if (currentPosition.X == x && currentPosition.Y == y)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    var tile = tiles[x + y * width];

                    switch(tile.TileDisplay)
                    {
                        case TileDisplay.Initial:
                            Console.Write($"[ ] ");
                            break;

                        case TileDisplay.Empty:
                            Console.Write("    ");
                            break;

                        case TileDisplay.Mine:
                            Console.Write("[M] ");
                            break;

                        case TileDisplay.Flag:
                            Console.Write("[F] ");
                            break;

                        case TileDisplay.Number:
                            Console.Write($"[{tile.AdjacentMineCount}] ");
                            break;
                    }

                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}