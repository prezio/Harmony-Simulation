using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicChords
{
    public abstract class PeriodicChord : Chord
    {
        public uint BaseNote
        {
            get;
            set;
        }
        public Period[] Periods
        {
            get;
            set;
        }
        protected override double[] getValues()
        {
            List<double> result = new List<double>(50);
            result.Add(BaseNote);
            foreach (Period period in Periods)
            {
                double[] temp = period.derivatives(result.Last());
                if (temp != null)
                {
                    result.AddRange(temp);
                }
            }
            return result.ToArray();
        }
    }
    public class MidiCentPeriodicChord : PeriodicChord
    {
        public override double[] Frequencies
        {
            get { return Notes.Select(NoteToFrequency).ToArray(); }
        }
        public override double[] Notes
        {
            get { return getValues(); }
        }
    }
    public class MidiPeriodicChord : PeriodicChord
    {
        public override double[] Frequencies
        {
            get { return Notes.Select(NoteToFrequency).ToArray(); }
        }
        public override double[] Notes
        {
            get { return getValues().Select(x => x * 100).ToArray(); }
        }
    }
    public class HerzPeriodicChord : PeriodicChord
    {
        public override double[] Frequencies
        {
            get { return getValues(); }
        }
        public override double[] Notes
        {
            get { return getValues().Select(FrequencyToNote).ToArray(); }
        }
    }
}
