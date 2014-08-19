using Newtonsoft.Json;
using PeriodicChords;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyEditor
{
    public static class Serialization
    {
        public static void WriteToJson(this List<List<Chord>> list, string fileName)
        {
            string text = JsonConvert.SerializeObject(list.Select(odc => odc.Select( ch => new ChordData() { Name = ch.GetType().Name, Content = JsonConvert.SerializeObject(ch) })));
            File.WriteAllText(fileName, text);
        }
        public static void WriteToPitch(this List<List<Chord>> list, string fileName)
        {
            List<double[]> pitches = list.Select(ch => ch.Notes).ToList();
            string text = JsonConvert.SerializeObject(pitches);

            File.WriteAllText(fileName, text);
        }
        public static IEnumerable<Chord> ReadFromJson(string fileName)
        {
            List<Chord> result = new List<Chord>();
            var data = JsonConvert.DeserializeObject<IEnumerable<ChordData>>(File.ReadAllText(fileName));
            foreach (ChordData cd in data)
            {
                Chord ch = null;
                switch (cd.Name)
                {
                    case "MidiPeriodicChord":
                        ch = JsonConvert.DeserializeObject<MidiPeriodicChord>(cd.Content);
                        break;
                    case "MidiSimpleChord":
                        ch = JsonConvert.DeserializeObject<MidiSimpleChord>(cd.Content);
                        break;

                    case "MidiCentPeriodicChord":
                        ch = JsonConvert.DeserializeObject<MidiCentPeriodicChord>(cd.Content);
                        break;
                    case "MidiCentSimpleChord":
                        JsonConvert.DeserializeObject<MidiCentSimpleChord>(cd.Content);
                        break;

                    case "HerzPeriodicChord":
                        JsonConvert.DeserializeObject<HerzPeriodicChord>(cd.Content);
                        break;
                    case "HerzSimpleChord":
                        JsonConvert.DeserializeObject<HerzSimpleChord>(cd.Content);
                        break;
                }
                result.Add(ch);
            }
            return result;
        }
    }
}
