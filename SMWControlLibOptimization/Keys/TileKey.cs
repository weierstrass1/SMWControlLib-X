using SMWControlLibUtils;

namespace SMWControlLibOptimization.Keys
{
    public class TileKey : DualKey<int, int>
    {
        public int X { get => element1; }
        public int Y { get => element2; }
        private int TilesPerRow;
        public TileKey(int x, int y, int tilesPerRow) : base(x, y)
        {
            TilesPerRow = tilesPerRow;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        protected override int CalculateHashCode()
        {
            return X + (Y * TilesPerRow);
        }
    }
}
