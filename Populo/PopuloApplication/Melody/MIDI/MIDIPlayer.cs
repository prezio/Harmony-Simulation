using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using System.Timers;

namespace PopuloApplication
{

    public class MIDIPlayer
    {
        #region static_members
        private static ChannelMessage[, ,] messageArray;
        static Tuple<int, int[,]> silence = new Tuple<int, int[,]>(10, new int[,] {
        { 0, 8, 0 }, 
        { 0, 8, 0 }, 
        { 0, 8, 0 }, 
        { 0, 8, 0 }, 
        { 0, 8, 0 },
        { 0, 8, 0 }, 
        { 0, 8, 0 }, 
        { 0, 8, 0 }, 
        { 0, 8, 0 }, 
        { 0, 8, 0 } });
        static MIDIPlayer()
        {
            messageArray = new ChannelMessage[16, 128, 128];
            ChannelMessageBuilder builder = new ChannelMessageBuilder();
            builder.Command = ChannelCommand.NoteOn;
            for (int channel = 0; channel < 16; channel++)
            {
                builder.MidiChannel = channel;
                for (int pitch = 0; pitch < 128; pitch++)
                {
                    builder.Data1 = pitch;
                    for (int velocity = 0; velocity < 128; velocity++)
                    {
                        builder.Data2 = velocity;
                        builder.Build();
                        messageArray[channel, pitch, velocity] = builder.Result;
                    }
                }

            }
        }
        #endregion

        private OutputDevice outDevice;
        private MelodySequence[] tracks;
        private int numberOfTracks;
        private Timer timer;
        public static int staccato = 100;
        public bool need = true;
        public bool adding = false;
        public MIDIPlayer(int device, int numberOfTracks, int interval)
        {
            outDevice = new OutputDevice(device);
            this.numberOfTracks = numberOfTracks;
            timer = new Timer(interval);
            tracks = new MelodySequence[numberOfTracks];
            ChannelMessageBuilder builder = new ChannelMessageBuilder(); //for test only
            timer.Elapsed += new ElapsedEventHandler(Tick);
            int[] instruments = new int[] { 1, 4, 12, 14, 11, 26, 47, 56, 3, 55, 84, 59, 29, 71, 21, 8 };
            for (int i = 0; i < numberOfTracks; i++)
            {

                builder.Command = ChannelCommand.Controller;
                builder.MidiChannel = i;
                builder.Data1 = 120;
                builder.Data2 = 0;
                builder.Build();
                tracks[i] = new MelodySequence(outDevice, this, builder.Result);

                //for test only
                builder.Command = ChannelCommand.ProgramChange;
                builder.MidiChannel = i;
                builder.Data1 = instruments[i];
                builder.Data2 = 127;
                builder.Build();
                outDevice.Send(builder.Result);
            }
        }
        public void Start()
        {
            timer.Enabled = true;
        }
        public void Pause()
        {
            timer.Enabled = false;
            for (int i = 0; i < numberOfTracks; i++)
            {
                tracks[i].Clean();
            }

        }
        public void Stop()
        {
            timer.Enabled = false;
            for (int i = 0; i < numberOfTracks; i++)
            {
                tracks[i].Clear();
            }

        }
        public void Tick(object sender, ElapsedEventArgs e)
        {

            for (int i = 0; i < numberOfTracks; i++)
            {
                tracks[i].Tick();
            }
        }
        public void Add(Tuple<int, int[,]>[] voices)
        {
            double baseTime = (60 * 100.0 * 4.0 / (double)Melody.tempo);
            adding = true;
            int numberOfNotes;
            int[,] notes;
            double time = 0;
            int pitch = 0;
            int[][][] stage;
            int length;
            int pause;
            double pausePart = ((100.0 - (double)staccato) / 100.0);
            lock (Melody.currentChords)
            {
                stage = Melody.chords[Melody.phase][Melody.stage];
            }
            Tuple<int, int[,]> current;
            for (int channel = 0; channel < 16; channel++)
            {
                if (!tracks[channel].need)
                    continue;
                int[] chord = stage[Melody.currentChords[channel]][channel];

                time = Melody.common_tempo ? baseTime : (60.0 * 100.0 * 4.0 / (double)Melody.tempi[channel]);
                time /= Melody.common_divider ? Melody.divider : Melody.dividers[channel];
                current = voices[channel] ?? silence;
                numberOfNotes = current.Item1;
                notes = current.Item2;
                for (int index = 0; index < numberOfNotes; index++)
                {
                    //channel = (((int)notes[index,0]) % 100) / 25;
                    if (notes[index, 1] == 1)
                    {
                        Melody.currentChords[channel]++;
                        Melody.currentChords[channel] %= stage.Length;
                        chord = stage[Melody.currentChords[channel]][channel];
                    }
                    pitch = chord[((notes[index, 0])) % chord.Length];
                    length = (int)((notes[index, 2] > 0 ? notes[index, 2] : 1) * time);
                    pause = Math.Max((int)(length * pausePart), 0);
                    length -= pause;
                    tracks[channel].SimpleAdd(length, messageArray[channel, pitch, notes[index, 3]]);
                    tracks[channel].SimpleAdd(pause, messageArray[channel, pitch, 0]);
                }
                //tracks[channel].SimpleAdd(0, messageArray[channel, 0, 0], messageArray[channel, 0, 0]);
                tracks[channel].Correct();
                tracks[channel].need = false;
            }
            need = false;
            adding = false;
        }
    }
}