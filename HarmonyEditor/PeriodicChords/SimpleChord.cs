using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PeriodicChords
{
    [DataContract]
    public abstract class SimpleChord : Chord
    {
        [DataMember]
        public double[] Peaks
        {
            get;
            set;
        }
        protected override double[] getValues()
        {
            return Peaks;
        }
    }
    public class MidiCentSimpleChord : SimpleChord
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
    public class MidiSimpleChord : SimpleChord
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
    public class HerzSimpleChord : SimpleChord
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
