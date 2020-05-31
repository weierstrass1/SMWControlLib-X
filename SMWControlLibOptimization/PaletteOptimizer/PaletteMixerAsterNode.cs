using SMWControlLibOptimization.Astar;
using SMWControlLibOptimization.Keys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PaletteMixerAsterNode : AstarNode<ConcurrentDictionary<ConcurrentDictionary<Int32, int>, ConcurrentDictionary<TileKey, int>>>
    {
        private int palettCost;
        public int MaxPalette { get; private set; }
        public ConcurrentQueue<ConcurrentDictionary<Int32, int>> FinishedPalettes { get; private set; }
        private ConcurrentDictionary<TileKey, int> remainingTiles, tiles;
        public PaletteMixerAsterNode() : base()
        {
            FinishedPalettes = new ConcurrentQueue<ConcurrentDictionary<int, int>>();
            remainingTiles = new ConcurrentDictionary<TileKey, int>();
            tiles = new ConcurrentDictionary<TileKey, int>();
            Content = new ConcurrentDictionary<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>();
        }
        public PaletteMixerAsterNode(int maxPalettesize, ConcurrentDictionary<ConcurrentDictionary<Int32, int>, ConcurrentDictionary<TileKey, int>> allPals) : base()
        {
            MaxPalette = maxPalettesize;
            FinishedPalettes = new ConcurrentQueue<ConcurrentDictionary<int, int>>();
            Content = allPals;
            remainingTiles = new ConcurrentDictionary<TileKey, int>();
            Parallel.ForEach(allPals, kvp =>
            {
                Parallel.ForEach(kvp.Value, kvp2 =>
                {
                    remainingTiles.TryAdd(kvp2.Key, 0);
                });
            });
            tiles = new ConcurrentDictionary<TileKey, int>();
            palettCost = remainingTiles.Count;
            calculatePaletteValue();
            updateCost();
            updateHeuristic();
            getBestElement();
        }
        public override bool Completed(params object[] args)
        {
            return remainingTiles.Count == 0;
        }

        public override void Expand(params object[] args)
        {
            ConcurrentDictionary<Int32, int> p;
            ConcurrentDictionary<TileKey, int> tl;
            ConcurrentQueue<PaletteMixerAsterNode> childs = new ConcurrentQueue<PaletteMixerAsterNode>();

            p = best.Key;
            Content.TryRemove(p, out tl);

            if (p.Count < MaxPalette)
            {
                Parallel.ForEach(Content, kvp =>
                {
                    if (PaletteProcessor.CountDiffs(p, kvp.Key) + 
                        Math.Max(p.Count, kvp.Key.Count) <= MaxPalette)
                    {
                        PaletteMixerAsterNode newNode = new PaletteMixerAsterNode();
                        ConcurrentDictionary<Int32, int> paux = new ConcurrentDictionary<int, int>();
                        ConcurrentDictionary<TileKey, int> tlaux = new ConcurrentDictionary<TileKey, int>();
                        Parallel.ForEach(p, c =>
                        {
                            paux.TryAdd(c.Key, c.Value);
                        });
                        Parallel.ForEach(kvp.Key, c =>
                        {
                            paux.TryAdd(c.Key, c.Value);
                        });
                        Parallel.ForEach(tl, t =>
                        {
                            tlaux.TryAdd(t.Key, t.Value);
                        });
                        Parallel.ForEach(kvp.Value, t =>
                        {
                            tlaux.TryAdd(t.Key, t.Value);
                        });

                        newNode.MaxPalette = MaxPalette;
                        newNode.Parent = this;
                        newNode.best = new KeyValuePair<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>(paux, tlaux);
                        newNode.gotBest = true;
                        newNode.updateCost();
                        newNode.updateHeuristic();

                        childs.Enqueue(newNode);
                    }
                });
            }

            PaletteMixerAsterNode n = new PaletteMixerAsterNode();
            n.Content = Content;
            n.best = best;
            n.MaxPalette = MaxPalette;
            n.Parent = this;
            n.gotBest = true;
            n.updateCost();
            n.updateHeuristic();

            childs.Enqueue(n);

            foreach (var ch in childs)
            {
                Children.Add(ch);
            }
        }
        KeyValuePair<ConcurrentDictionary<Int32, int>, ConcurrentDictionary<TileKey, int>> best = default;
        bool gotBest = false;
        int nextRemoved = 0;
        private void getBestElement()
        {
            if (gotBest) return;

            int max = 0;
            int maxh = 0;
            int countT, countT2;
            foreach (var kvp in Content)
            {
                countT = 0;
                Parallel.ForEach(kvp.Value, til =>
                {
                    if (remainingTiles.ContainsKey(til.Key))
                        countT++;
                });
                if (countT > maxh) 
                {
                    gotBest = true;
                    maxh = countT;
                    best = kvp;
                }
                if (countT > 0 && countT == maxh && kvp.Key.Count < best.Key.Count) 
                {
                    best = kvp;
                }
            }
            nextRemoved = maxh;
        }

        int pcost = -1, pheur = -1;
        float pval = -1;

        protected override int setCost()
        {
            if (pcost < 0) 
                return -1;
            if (Cost >= 0)
                return Cost;
            if (Parent != null)
            {
                PaletteMixerAsterNode p = (PaletteMixerAsterNode)Parent;
                Parallel.ForEach(p.tiles, til =>
                {
                    tiles.TryAdd(til.Key, 0);
                });
                Parallel.ForEach(p.remainingTiles, til =>
                {
                    remainingTiles.TryAdd(til.Key, 0);
                });

                if (FinishedPalettes.Count > p.FinishedPalettes.Count) 
                {
                    Parallel.ForEach(best.Value, kvp =>
                    {
                        int v;
                        remainingTiles.TryRemove(kvp.Key, out v);
                        tiles.TryAdd(kvp.Key, 0);
                    });
                }
            }
            getBestElement();

            return 0; //tiles.Count;
        }

        protected override int setHeuristic()
        {
            if (pheur < 0)
                return -1;
            if (Heuristic >= 0)
                return Heuristic;

            return remainingTiles.Count - nextRemoved;
        }
        private void calculatePaletteValue()
        {
            if (pval >= 0) return;
            if (Parent != null)
            {
                PaletteMixerAsterNode p = (PaletteMixerAsterNode)Parent;
                if (Content == Parent.Content)
                {
                    Parallel.ForEach(p.FinishedPalettes, kvp =>
                    {
                        FinishedPalettes.Enqueue(kvp);
                    });
                    FinishedPalettes.Enqueue(best.Key);
                }
                else
                {
                    Parallel.ForEach(p.FinishedPalettes, kvp =>
                    {
                        FinishedPalettes.Enqueue(kvp);
                    });
                    Parallel.ForEach(p.Content, kvp =>
                    {
                        if (PaletteProcessor.CountDiffs(kvp.Key, best.Key) > 0) 
                            Content.TryAdd(kvp.Key, kvp.Value);
                    });
                    if (best.Key.Count == MaxPalette)
                        FinishedPalettes.Enqueue(best.Key);
                    else
                    {
                        Content.TryAdd(best.Key, best.Value);
                    } 
                }
            }
            gotBest = false;
            pcost = FinishedPalettes.Count;
            pheur = Content.Count;
            pval = (pcost * WeightCost) + (pheur * WeightHeuristic);
        }
        public override int CompareTo(AstarNode<ConcurrentDictionary<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>> other)
        {
            PaletteMixerAsterNode n = (PaletteMixerAsterNode)other;

            int c = base.CompareTo(other);

            if (c != 0) return c;

            if (pval < n.pval) return -1;
            else if (pval > n.pval) return 1;
            return 0;
        }

        public override bool CanAdd()
        {
            PaletteMixerAsterNode p = (PaletteMixerAsterNode)Parent;
            if (p.Content.Count == 0)
            {
                if (best.Value.Count < p.remainingTiles.Count)
                    return false;
                foreach (var kvp in p.remainingTiles)
                {
                    if (!best.Value.ContainsKey(kvp.Key))
                        return false;
                }
            }
            calculatePaletteValue();
            updateCost();
            updateHeuristic();
            Parent = null;

            if (Content.Count <= 0) return false;
            if (nextRemoved == 0) 
                return false;

            ConcurrentDictionary<TileKey, int> tl2 = new ConcurrentDictionary<TileKey, int>();

            Parallel.ForEach(Content, kvp =>
            {
                Parallel.ForEach(kvp.Value, kvp2 =>
                {
                    tl2.TryAdd(kvp2.Key, 0);
                });
            });
            if (tl2.Count < remainingTiles.Count)
                return false;

            return true;
        }
    }
}
