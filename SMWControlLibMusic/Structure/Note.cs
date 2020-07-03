using SMWControlLibMusic.Enumerators;
using SMWControlLibUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibMusic.Structure
{
    public class Note
    {
        public BasicNotes BaseNote { get; private set; }
        public Octave Octave { get; private set; }

        public double Frecuency { get; private set; }

        public Note(BasicNotes basenote, Octave octave)
        {
            Octave = octave;
            BaseNote = basenote;
            Frecuency = basenote.Frecuency * Octave.FrecuencyMultiplier;
        }
    }
}
