using Newtonsoft.Json;
using PeriodicChords;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicChords
{
    public static class Serialization
    {
        public static void WriteToPitch(this List<List<Chord>> data, string fileName)
        {
            List<List<PitchData>> result = new List<List<PitchData>>();

            foreach(List<Chord> list in data)
            {
                result.Add(list.Select(ch => new PitchData()
                {
                    Pitches = ch.Notes,
                    Left = ch.Left,
                    Right = ch.Right
                }).ToList());
            }
            
            File.WriteAllText(fileName, JsonConvert.SerializeObject(result));
        }
        public static List<List<PitchData>> ReadFromPitch(string fileName)
        {
            return JsonConvert.DeserializeObject <List<List<PitchData>>>(File.ReadAllText(fileName));
        }

        public static void SaveResultsToJson(this List<List<Chord>> list, string fileName)
        {
            string text = JsonConvert.SerializeObject(list.Select(odc => odc.Select(ch => new ChordData() { Name = ch.GetType().Name, Content = JsonConvert.SerializeObject(ch) })));
            File.WriteAllText(fileName, text);
        }
        public static List<List<Chord>> LoadResultsFromJson(string fileName)
        {
            List<List<Chord>> result = new List<List<Chord>>();
            var data = JsonConvert.DeserializeObject<List<List<ChordData>>>(File.ReadAllText(fileName));

            foreach (List<ChordData> list in data)
            {
                result.Add(new List<Chord>());

                foreach (ChordData cd in list)
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

                    result.Last().Add(ch);
                }
            }
            return result;
        }
    }
}
