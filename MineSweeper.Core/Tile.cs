namespace MineSweeper.Core
{
    public class Tile
    {
        //public int Index { get; set; }

        public bool IsMine { get; set; }

        public bool IsVisible { get; set; }

        public bool Flagged { get; set; }

        public int AdjacentMineCount { get; set; }

        public TileDisplay TileDisplay
        {
            get
            {
                if (this.Flagged)
                    return TileDisplay.Flag;

                if (!this.IsVisible)
                    return TileDisplay.Initial;

                if (this.IsVisible && !this.IsMine && AdjacentMineCount == 0)
                    return TileDisplay.Empty;

                if (this.IsVisible && !this.IsMine && AdjacentMineCount > 0)
                    return TileDisplay.Number;

                if (this.IsVisible && this.IsMine)
                    return TileDisplay.Mine;

                var tileDisplay = TileDisplay.Initial;

                return tileDisplay;
            }
        }
    }
}