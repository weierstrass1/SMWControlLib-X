using SMWControlLibOptimization.Clustering;
using SMWControlLibOptimization.Keys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PaletteMixerClusterNode : ClusterNode<KeyValuePair<ConcurrentDictionary<Int32, int>, ConcurrentDictionary<TileKey, int>>>
    {
        public PaletteMixerClusterNode() : base(0)
        {
            Content = new KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>();
        }
        public PaletteMixerClusterNode(int maxSize) : base(maxSize)
        {
            Content = new KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>();
        }

        public override float BreakDraw(KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>> cont)
        {
            return (CountDiffs(cont) - Math.Max(Content.Value.Count, cont.Value.Count));
        }
        private int CountDiffs(KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>> cont)
        {
            var p1 = Content.Value;
            var p2 = cont.Value;
            if (p1.Count > p2.Count)
            {
                p2 = Content.Value;
                p1 = cont.Value;
            }
            int count = 0;
            Parallel.ForEach(p1, til =>
            {
                if (!p2.ContainsKey(til.Key))
                    count++;
            });
            return count;
        }
        public override bool Contains(KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>> cont)
        {
            return CountDiffs(cont) == 0 && Content.Value.Count >= cont.Value.Count;
        }
        public override int Distance(KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>> cont)
        {
            var p1 = Content.Value;
            var p2 = cont.Value;
            int count = 0;
            Parallel.ForEach(p1, til =>
            {
                if (p2.ContainsKey(til.Key))
                    count++;
            });
            return -count;
        }

        public override ClusterNode<KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>> Merge(KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>> cont)
        {
            ConcurrentDictionary<int, int> paux = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<TileKey, int> taux = new ConcurrentDictionary<TileKey, int>();
            var p1 = Content.Key;


            if (p1 != null)
            {
                Parallel.ForEach(p1, til =>
                {
                    paux.TryAdd(til.Key, til.Value);
                });
                var t1 = Content.Value;
                Parallel.ForEach(t1, til =>
                {
                    taux.TryAdd(til.Key, til.Value);
                });
            }

            var p2 = cont.Key;
            Parallel.ForEach(p2, til =>
            {
                paux.TryAdd(til.Key, til.Value);
            });
            var t2 = cont.Value;
            Parallel.ForEach(t2, til =>
            {
                taux.TryAdd(til.Key, til.Value);
            });

            PaletteMixerClusterNode ret = new PaletteMixerClusterNode(MaxClusterSize);
            ret.Content =
                new KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>(paux, taux);
            return ret;
        }

        public override int MergeSize(KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>> cont)
        {
            return PaletteProcessor.CountDiffs(Content.Key, cont.Key) +
                Math.Max(Content.Key.Count, cont.Key.Count);
        }
    }
}
