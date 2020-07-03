using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.ColorReduction
{
    public class ColorGroup : IComparable<ColorGroup>
    {
        public int A { get; private set; }
        public int R { get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }
        private int a, r, g, b, c;
        private ConcurrentDictionary<int, int> colors;
        public ColorGroup LeftSon { get; private set; }
        public ColorGroup RightSon { get; private set; }
        public int Length { get; private set; }
        public ColorGroup()
        {
            colors = new ConcurrentDictionary<int, int>();
            a = 0;
            r = 0;
            g = 0;
            b = 0;
            c = 0;
            A = 0;
            R = 0;
            G = 0;
            B = 0;
            Length = 0;
        }

        public ColorGroup(int color, int count)
        {
            colors = new ConcurrentDictionary<int, int>();
            colors.TryAdd(color, count);
            a = (color >> 24) & 0x000000FF;
            r = (color >> 16) & 0x000000FF;
            b = (color >> 8) & 0x000000FF;
            g = (color) & 0x000000FF;
            A = a;
            R = R;
            G = B;
            B = G;

            a *= count;
            r *= count;
            g *= count;
            b *= count;

            c = count;
            Length = 1;
        }

        public bool Contains(ColorGroup bro)
        {
            if (Length < bro.Length)
                return false;

            foreach (var kvp in bro.colors) 
            {
                if (!colors.ContainsKey(kvp.Key)) 
                    return false;
            }

            return true;
        }

        public int Distance(ColorGroup bro)
        {
            if (a == 0 && bro.a == 0) return 0;

            return Math.Abs(R - bro.R) + Math.Abs(G - bro.G) + Math.Abs(B - bro.B);
        }

        private void getPublicValues()
        {
            int midc = c / 2;
            A = a / c;
            if (a % c >= midc) A++;
            R = r / c;
            if (r % c >= midc) R++;
            G = g / c;
            if (g % c >= midc) G++;
            B = b / c;
            if (b % c >= midc) B++;
        }

        public ColorGroup Merge(ColorGroup bro)
        {
            ColorGroup ret = new ColorGroup();
            ret.a = a + bro.a;
            ret.r = r + bro.r;
            ret.g = g + bro.g;
            ret.b = b + bro.b;
            ret.c = c + bro.c;
            ret.getPublicValues();
            ret.Length = Length + bro.Length;

            Parallel.ForEach(bro.colors, kvp =>
            {
                ret.colors.TryAdd(kvp.Key, kvp.Value);
            });
            Parallel.ForEach(colors, kvp =>
            {
                ret.colors.TryAdd(kvp.Key, kvp.Value);
            });

            ret.LeftSon = this;
            ret.RightSon = bro;

            return ret;
        }

        public int CompareTo(ColorGroup other)
        {
            if (Length > other.Length) return 1;
            if (Length < other.Length) return -1;
            return 0;
        }

        public override string ToString()
        {
            return $"L: { Length }, A: {A}, R: {R}, G: {G}, B: {B}";
        }
    }
}
