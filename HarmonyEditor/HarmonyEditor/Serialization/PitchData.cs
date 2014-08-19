using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarmonyEditor
{
    [DataContract]
    public class PitchData
    {
        [DataMember]
        public double[] Pitches { get; set; }
        [DataMember]
        public int Left { get; set; }
        [DataMember]
        public int Right { get; set; }
    }
}
