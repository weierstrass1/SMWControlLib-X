using SMWControlLibOptimization.Astar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PalletteGroupNode : AstarNode<ConcurrentDictionary<Int32, int>>
    {
        float w = 0;
        public int MaxNumberOfColorsPerPalette { get; private set; }
        private List<ConcurrentDictionary<Int32, int>> remainder;
        public List<ConcurrentDictionary<Int32, int>> FinishedPalettes { get; private set; }
        public PalletteGroupNode() : base()
        {

        }

        public PalletteGroupNode(List<ConcurrentDictionary<Int32, int>> allPals, int maxPal) : base()
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

            Heuristic = 0;
            int diff;

            foreach (var p in allPals)
            {
                diff = PaletteProcessor.CountDiffs(Content, p);
                if (diff != 0)
                {
                    remainder.Add(p);
                    Heuristic += (int)(diff * w);
                }
            }

            Cost = Content.Count;

            Value = Cost + Heuristic;
        }
        public override bool Completed(params object[] args)
        {
            return remainder.Count == 0;
        }

        public override void Expand(params object[] args)
        {
            int bestEq = 0;
            int eq, diff;
            PalletteGroupNode newNode;
            Children = new List<AstarNode<ConcurrentDictionary<Int32, int>>>();
            foreach (var p in remainder)
            {
                diff = PaletteProcessor.CountDiffs(Content, p);
                eq = PaletteProcessor.CountEquals(Content, p) - diff;
                diff += Math.Max(p.Count, Content.Count);
                if (bestEq < eq && diff <= MaxNumberOfColorsPerPalette) 
                {
                    Children.Clear();
                    bestEq = eq;
                }
                if (bestEq == eq && diff <= MaxNumberOfColorsPerPalette)
                {
                    newNode = new PalletteGroupNode();
                    newNode.Content = p;
                    Children.Add(newNode);
                }
            }

            ConcurrentDictionary<Int32, int> newCont;
            PalletteGroupNode n;

            foreach (var node in Children)
            {
                newCont = new ConcurrentDictionary<Int32, int>();
                n = (PalletteGroupNode)node;
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
                n.remainder = new List<ConcurrentDictionary<int, int>>();
                n.Heuristic = 0;

                foreach (var p in remainder)
                {
                    diff = PaletteProcessor.CountDiffs(n.Content, p);
                    if (diff!=0)
                    {
                        n.remainder.Add(p);
                        n.Heuristic += (int)(diff * w);
                    }
                }

                n.Cost = n.Content.Count;
                n.Value = n.Cost + n.Heuristic;
                n.Parent = this;
                n.FinishedPalettes = new List<ConcurrentDictionary<int, int>>();
                foreach(var p in FinishedPalettes)
                {
                    n.FinishedPalettes.Add(p);
                    n.Cost += (p.Count * 2);
                }
            }

            if (Children.Count <= 0 && remainder.Count > 0)  
            {
                newNode = new PalletteGroupNode();
                newNode.MaxNumberOfColorsPerPalette = MaxNumberOfColorsPerPalette;
                newNode.Parent = this;
                newNode.Content = remainder.First();
                newNode.remainder = new List<ConcurrentDictionary<int, int>>();
                newNode.FinishedPalettes = new List<ConcurrentDictionary<int, int>>();
                newNode.Cost = newNode.Content.Count;

                foreach (var p in FinishedPalettes)
                {
                    newNode.FinishedPalettes.Add(p);
                    newNode.Cost += (p.Count * 2);
                }
                newNode.FinishedPalettes.Add(Content);
                newNode.Cost += (Content.Count * 2);

                newNode.Heuristic = 0;
                foreach(var p in remainder)
                {
                    diff = PaletteProcessor.CountDiffs(newNode.Content, p);
                    if (diff != 0)
                    {
                        newNode.remainder.Add(p);
                        newNode.Heuristic += (int)(diff * w);
                    }
                }

                newNode.Value = newNode.Cost + newNode.Heuristic;
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
    }
}
