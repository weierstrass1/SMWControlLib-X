using SMWControlLibMusic.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibMusic.Structure
{
    public class KeySignature
    {
        private BasicNotes[] Signature;

        public KeySignature(params BasicNotes[] notes)
        {
            Signature = new BasicNotes[7];
            int i;

            for (i = 0; i < notes.Length && i < Signature.Length; i++)
            {
                Signature[i] = notes[i];
            }

            for (i = 0; i < Signature.Length; i++)
            {
                if (Signature[i] == null)
                    Signature[i] = BasicNotes.GetTone(i);
            }
        }

        public BasicNotes this[int index] 
        { 
            get
            {
                if (index < 0 || index >= Signature.Length)
                    throw new IndexOutOfRangeException(nameof(index));
                return Signature[index];
            }
        }
    }
}
