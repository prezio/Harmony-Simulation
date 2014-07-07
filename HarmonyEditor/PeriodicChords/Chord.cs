using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicChords
{
    public abstract partial class Chord
    {
        public abstract double[] Frequencies
        {
            get;
        }
        public abstract double[] Notes
        {
            get;
        }
        public double NoteToFrequency(double note)
        {
            try
            {
                return n2f[(uint)note];
            }
            catch (Exception)
            {
                throw new SoundOutOfRangeException();
            }
        }
        public double FrequencyToNote(double frequency)
        {
            try
            {
                return (double)n2f.BinarySearch(frequency);
            }
            catch (Exception)
            {
                throw new SoundOutOfRangeException();
            }
        }
        protected abstract double[] getValues();
    }
}
