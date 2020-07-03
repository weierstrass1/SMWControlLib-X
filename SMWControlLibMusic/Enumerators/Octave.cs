using SMWControlLibUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibMusic.Enumerators
{
    public class Octave : FakeEnumerator
    {
        public static Octave O0 = new Octave(0, 0.0625);
        public static Octave O1 = new Octave(1, 0.125);
        public static Octave O2 = new Octave(2, 0.25);
        public static Octave O3 = new Octave(3, 0.5);
        public static Octave O4 = new Octave(4, 1);
        public static Octave O5 = new Octave(5, 2);
        public static Octave O6 = new Octave(6, 4);
        public static Octave O7 = new Octave(7, 8);
        public static Octave O8 = new Octave(8, 16);
        public int Index { get => Value; }
        public double FrecuencyMultiplier { get; private set; }
        private Octave(int i, double multiplier) : base(i)
        {
            FrecuencyMultiplier = multiplier;
        }

        public static Octave GetOctave(int index)
        {
            switch(index)
            {
                case 0:
                    return O0;
                case 1:
                    return O1;
                case 2:
                    return O2;
                case 3:
                    return O3;
                case 4:
                    return O4;
                case 5:
                    return O5;
                case 6:
                    return O6;
                case 7:
                    return O7;
                case 8:
                    return O8;
                default:
                    throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
}
