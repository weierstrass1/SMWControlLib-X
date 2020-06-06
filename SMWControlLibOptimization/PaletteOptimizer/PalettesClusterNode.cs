using SMWControlLibOptimization.Clustering;
using System;
using System.Collections.Concurrent;

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
            return -PaletteProcessor.CountEquals(Content, cont);
        }

        public override ClusterNode<ConcurrentDictionary<int, int>> Merge(ConcurrentDictionary<int, int> cont)
        {
            PalettesClusterNode ret = new PalettesClusterNode(MaxClusterSize);
            foreach (var c in cont)
            {
                ret.Content.TryAdd(c.Key, c.Value);
            }
            foreach (var c in Content)
            {
                if (ret.Content.ContainsKey(c.Key))
                {
                    ret.Content[c.Key] += c.Value;
                }
                else
                {
                    ret.Content.TryAdd(c.Key, c.Value);
                }
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

        public override bool Contains(ConcurrentDictionary<int, int> cont)
        {
            return Content.Count >= cont.Count && PaletteProcessor.CountDiffs(Content, cont) == 0;
        }

        public override float BreakDraw(ConcurrentDictionary<int, int> cont)
        {
            return -Math.Abs(Content.Count - cont.Count);
        }
    }
}
