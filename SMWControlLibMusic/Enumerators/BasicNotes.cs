using SMWControlLibUtils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SMWControlLibMusic.Enumerators
{
    public class BasicNotes : FakeEnumerator
    {
        public static BasicNotes DoBemol = new BasicNotes(11, "Do♭", "C♭", 493.883);
        public static BasicNotes Do = new BasicNotes(0, "Do", "C", 261.626);
        public static BasicNotes DoSostenido = new BasicNotes(1, "Do♯", "C♯", 277.183);
        public static BasicNotes ReBemol = new BasicNotes(1, "Re♭", "D♭", 277.183);
        public static BasicNotes Re = new BasicNotes(2, "Re", "D", 293.665);
        public static BasicNotes ReSostenido = new BasicNotes(3, "Re♯", "D♯", 311.127);
        public static BasicNotes MiBemol = new BasicNotes(3, "Mi♭", "E♭", 311.127);
        public static BasicNotes Mi = new BasicNotes(4, "Mi", "E", 329.628);
        public static BasicNotes FaBemol = new BasicNotes(4, "Fa♭", "F♭", 329.628);
        public static BasicNotes Fa = new BasicNotes(5, "Fa", "F", 349.228);
        public static BasicNotes FaSostenido = new BasicNotes(6, "Fa♯", "F♯", 369.994);
        public static BasicNotes SolBemol = new BasicNotes(6, "Sol♭", "G♭", 369.994);
        public static BasicNotes Sol = new BasicNotes(7, "Sol", "G", 391.995);
        public static BasicNotes SolSostenido = new BasicNotes(8, "Sol♯", "G♯", 415.305);
        public static BasicNotes LaBemol = new BasicNotes(8, "La♭", "A♭", 415.305);
        public static BasicNotes La = new BasicNotes(9, "La", "A", 440);
        public static BasicNotes LaSostenido = new BasicNotes(10, "La♯", "A♯", 466.164);
        public static BasicNotes SiBemol = new BasicNotes(10, "Si♭", "B♭", 466.164);
        public static BasicNotes Si = new BasicNotes(11, "Si", "B", 493.883);
        public static BasicNotes SiSostenido = new BasicNotes(0, "Si♯", "B", 261.626);
        public int Index { get => Value; }
        public string Name { get; private set; }
        public string NameCharacter { get; private set; }
        public double Frecuency { get; private set; }

        private BasicNotes(int i, string name, string nameChar, double frecuency) : base(i)
        {
            Name = name;
            NameCharacter = nameChar;
            Frecuency = frecuency;
        }

        public static BasicNotes GetTone(int i)
        {
            switch(i)
            {
                case 0:
                    return Do;
                case 1:
                    return Re;
                case 2:
                    return Mi;
                case 3:
                    return Fa;
                case 4:
                    return Sol;
                case 5:
                    return La;
                case 6:
                    return Si;
                default:
                    throw new IndexOutOfRangeException(nameof(i));
            }
        }
    }
}
