using SMWControlLibOptimization.Astar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PaletteGroupNode : AstarNode<ConcurrentDictionary<Int32, int>>
    {
        private static int paletteCost = 1;
        private static int colorStartNumber = 0;
        public int MaxNumberOfColorsPerPalette { get; private set; }
        private List<ConcurrentDictionary<Int32, int>> remainder;
        private ConcurrentDictionary<Int32, int> allColors;
        public List<ConcurrentDictionary<Int32, int>> FinishedPalettes { get; private set; }
        public PaletteGroupNode() : base()
        {

        }

        public PaletteGroupNode(List<ConcurrentDictionary<Int32, int>> allPals, int maxPal) : base()
        {
            MaxNumberOfColorsPerPalette = maxPal;
            Content = new ConcurrentDictionary<int, int>();
            remainder = new List<ConcurrentDictionary<int, int>>();
            FinishedPalettes = new List<ConcurrentDictionary<int, int>>();
            Parent = null;
            ConcurrentDictionary<Int32, int> rootContent = allPals.First();

            foreach (var kvp in rootContent)
            {
                Content.TryAdd(kvp.Key, kvp.Value);
            }

            allColors = new ConcurrentDictionary<int, int>();
            foreach (var p in allPals)
            {
                foreach (var c in p)
                {
                    if (!Content.ContainsKey(c.Key))
                        allColors.TryAdd(c.Key, c.Value);
                }
            }
            colorStartNumber = allColors.Count + Content.Count;
            paletteCost = ((allPals.Count - 1) / 2) + 1;

            foreach (var p in allPals)
            {
                if (p.Count > Content.Count || PaletteProcessor.CountDiffs(Content, p) != 0)
                {
                    if (p.Count == MaxNumberOfColorsPerPalette)
                    {
                        FinishedPalettes.Add(p);
                    }
                    else
                    {
                        remainder.Add(p);
                    }
                }
            }

            updateCost();
            updateHeuristic();
        }
        public override bool Completed(params object[] args)
        {
            return Children.Count == 0 && remainder.Count == 0;
        }

        public override void Expand(params object[] args)
        {
            int best = 0;
            int cur = 0;
            ConcurrentDictionary<Int32, int> curpal = null;
            List<ConcurrentDictionary<Int32, int>> childs = new List<ConcurrentDictionary<int, int>>();
            PaletteGroupNode newNode;
            Children = new List<AstarNode<ConcurrentDictionary<Int32, int>>>();

            PaletteGroupNode parent = (PaletteGroupNode)Parent;
            if (Content.Count < MaxNumberOfColorsPerPalette)
            {
                List<ConcurrentDictionary<Int32, int>> aux = new List<ConcurrentDictionary<int, int>>();
                foreach (var p in remainder)
                {
                    aux.Add(p);
                }

                while (aux.Count > 0)
                {
                    curpal = PaletteProcessor.GetDiffs(Content, aux.First());
                    aux.RemoveAt(0);
                    cur = 0;

                    if (curpal.Count <= MaxNumberOfColorsPerPalette)
                    {
                        foreach (var p in remainder)
                        {
                            if (PaletteProcessor.CountDiffs(curpal, p) == 0)
                            {
                                cur++;
                            }
                        }
                        if (cur > best)
                        {
                            best = cur;
                            Children.Clear();
                        }
                        if (cur == best)
                        {
                            newNode = new PaletteGroupNode();
                            newNode.Content = curpal;
                            Children.Add(newNode);
                        }
                    }
                }
            }

            ConcurrentDictionary<Int32, int> newCont;
            PaletteGroupNode n;

            foreach (var node in Children)
            {
                newCont = new ConcurrentDictionary<Int32, int>();
                n = (PaletteGroupNode)node;
                n.MaxNumberOfColorsPerPalette = MaxNumberOfColorsPerPalette;
                foreach (var kvp in n.Content)
                {
                    newCont.TryAdd(kvp.Key, kvp.Value);
                }
                foreach (var kvp in Content)
                {
                    newCont.TryAdd(kvp.Key, kvp.Value);
                }

                n.Content = newCont;

                n.allColors = new ConcurrentDictionary<int, int>();
                foreach (var c in allColors)
                {
                    if (!n.Content.ContainsKey(c.Key))
                    {
                        n.allColors.TryAdd(c.Key, c.Value);
                    }
                }

                n.FinishedPalettes = new List<ConcurrentDictionary<int, int>>();
                foreach (var p in FinishedPalettes)
                {
                    n.FinishedPalettes.Add(p);
                }

                n.remainder = new List<ConcurrentDictionary<int, int>>();

                foreach (var p in remainder)
                {
                    if (PaletteProcessor.CountDiffs(n.Content, p) != 0)
                    {
                        n.remainder.Add(p);
                    }
                }
                n.Parent = this;
                n.updateCost();
                n.updateHeuristic();
            }

            if (remainder.Count > 0)
            {
                newNode = new PaletteGroupNode();
                newNode.MaxNumberOfColorsPerPalette = MaxNumberOfColorsPerPalette;
                newNode.Parent = this;
                newNode.Content = remainder.First();
                newNode.remainder = new List<ConcurrentDictionary<int, int>>();
                newNode.FinishedPalettes = new List<ConcurrentDictionary<int, int>>();

                foreach (var p in FinishedPalettes)
                {
                    newNode.FinishedPalettes.Add(p);
                }
                newNode.FinishedPalettes.Add(Content);

                newNode.allColors = new ConcurrentDictionary<int, int>();
                foreach (var c in allColors)
                {
                    if (!newNode.Content.ContainsKey(c.Key))
                    {
                        newNode.allColors.TryAdd(c.Key, c.Value);
                    }
                }

                foreach (var p in remainder)
                {
                    if (PaletteProcessor.CountDiffs(newNode.Content, p) != 0)
                    {
                        newNode.remainder.Add(p);
                    }
                }

                newNode.updateCost();
                newNode.updateHeuristic();
                Children.Add(newNode);
            }
            if (Children.Count == 0)
            {
                FinishedPalettes.Add(Content);
            }
        }

        public override bool Equals(object obj)
        {
            return this == obj;
        }

        protected override int setCost()
        {
            if (remainder.Count <= 0 && (Children == null || Children.Count == 0))
                return 0;

            return FinishedPalettes.Count + 1;
        }
        protected override int setHeuristic()
        {
            ConcurrentDictionary<int, int> remainingColors = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<ConcurrentDictionary<int, int>, int> pals = new ConcurrentDictionary<ConcurrentDictionary<int, int>, int>();
            ConcurrentDictionary<int, int> curp;
            foreach (var c in allColors)
            {
                int cur = 0;
                curp = null;
                foreach (var p in remainder)
                {
                    if (p.ContainsKey(c.Key))
                    {
                        cur++;
                        curp = p;
                    }
                    if (cur > 1)
                        break;
                }
                if (cur == 1 && curp != null)
                    if (!pals.TryAdd(curp, 1))
                        pals[curp]++;
            }

            /*int rm = allColors.Count - pals.Count;
            rm = (rm + (MaxNumberOfColorsPerPalette - rm) % MaxNumberOfColorsPerPalette) / MaxNumberOfColorsPerPalette;
            */
            return pals.Count;
        }

        public override bool CanAdd()
        {
            throw new NotImplementedException();
        }
    }
}
