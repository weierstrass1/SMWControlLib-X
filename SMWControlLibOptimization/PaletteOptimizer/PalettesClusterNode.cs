using SMWControlLibOptimization.Clustering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PalettesClusterNode : ClusterNode<ConcurrentDictionary<Int32, int>>
    {
        public override int Size => Content.Count;
        public PalettesClusterNode() : base(0)
        {
            Content = new ConcurrentDictionary<int, int>();
        }
        public PalettesClusterNode(int maxSize) : base(maxSize)
        {
            Content = new ConcurrentDictionary<int, int>();
        }
        public override int Distance(ConcurrentDictionary<int, int> cont)
        {
            int diff = PaletteProcessor.CountDiffs(Content, cont);

            if (diff + Math.Max(cont.Count, Content.Count) > MaxClusterSize) return 100000;

            return diff;
        }

        public override ClusterNode<ConcurrentDictionary<int, int>> Merge(ConcurrentDictionary<int, int> cont)
        {
            PalettesClusterNode ret = new PalettesClusterNode(MaxClusterSize);
            foreach(var c in cont)
            {
                ret.Content.TryAdd(c.Key, c.Value);
            }
            foreach (var c in Content)
            {
                ret.Content.TryAdd(c.Key, c.Value);
            }
            return ret;
        }

        public override string ToString()
        {
            return "Size: " + Size;
        }

        public override int MergeSize(ConcurrentDictionary<int, int> cont)
        {
            return Math.Max(cont.Count, Content.Count) + PaletteProcessor.CountDiffs(cont, Content);
        }
    }
}
