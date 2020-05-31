using SMWControlLibUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibOptimization.Keys
{
    public class TileKey : DualKey<int,int>
    {
        public int X { get => element1; }
        public int Y { get => element2; }
        public TileKey(int x, int y) : base(x, y)
        {

        }
    }
}
