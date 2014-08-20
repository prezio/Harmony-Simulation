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

        public bool need=true;
        public MIDIPlayer(int device, int numberOfTracks, int interval)
        {
            outDevice = new OutputDevice(device);
            this.numberOfTracks = numberOfTracks;
            timer = new Timer(interval);
            tracks = new MelodySequence[numberOfTracks];
            ChannelMessageBuilder builder = new ChannelMessageBuilder(); //for test only
            for (int i = 0; i < numberOfTracks; i++)
            {
                tracks[i] = new MelodySequence(outDevice,this);
                timer.Elapsed +=new ElapsedEventHandler(tracks[i].Tick);
                //for test only
                builder.Command = ChannelCommand.ProgramChange;
                builder.MidiChannel = i;
                builder.Data1 = i * 3;
                builder.Data2 = 127;
                builder.Build();
                outDevice.Send(builder.Result);
            }
        }
        public void Start()
        {
            timer.Enabled = true;
        }
        public void Stop()
        {
            timer.Enabled = false;
            for (int i = 0; i < numberOfTracks; i++)
            {
                tracks[i].clean();
            }
              
        }
        public void Add(Tuple<int,int[,]>[] voices)
        {
            int numberOfNotes;
            int[,] notes;
            int pitch = 0;

            for (int channel = 0; channel < 16; channel++)
            {
                numberOfNotes = voices[channel].Item1;
                notes = voices[channel].Item2;
                int i = 0;
                for (int index = 0; index < numberOfNotes; index++)
                {
                    //channel = (((int)notes[index,0]) % 100) / 25;
                    pitch = notes[index, 0] + 40;

                    
                    tracks[channel].SimpleAdd(i, messageArray[channel, pitch, notes[index, 3]], messageArray[channel, pitch, 0]);
                    i += notes[index, 2];
                    
                }
                tracks[channel].Correct();
            }
            need = false;
        }
    }
}