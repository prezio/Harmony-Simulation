using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicChords
{
    [DataContract]
    public class ChordData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Content { get; set; }
    }
}
