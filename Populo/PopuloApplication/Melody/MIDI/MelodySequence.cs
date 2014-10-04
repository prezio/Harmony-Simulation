using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using System.Timers; 

namespace PopuloApplication
{
    public class MelodySequence
    {
        Queue<Tuple<int, ChannelMessage>> sequence;
        ChannelMessage endMessage;
        int counter = -1;
        public bool need = true;
        OutputDevice outDevice;
        MIDIPlayer player;
        public void Tick()
        {
            
                counter--;

                while (counter == 0)
                {
                    if (sequence.Count > 0)
                    {
                        Tuple<int, ChannelMessage> t = sequence.Dequeue();
                        if (t != null)
                        {
                            outDevice.Send(t.Item2);
                            counter = t.Item1;
                        }
                        else 
                            counter--;

                    }
                    else
                    {
                        counter--;
                    }
                }
                if (sequence.Count < 2)
                {
                    player.need = true;
                    need = true;
                }

        }
        public void Add(int i, ChannelMessage On)
        {
            sequence.Enqueue(new Tuple<int, ChannelMessage>(i, On));
            if (counter < 0)
                counter = 1;
        }
        public void SimpleAdd(int i, ChannelMessage On)
        {
            sequence.Enqueue(new Tuple<int, ChannelMessage>(i, On));
           
        }
        public void Correct()
        {
            
            if (counter < 0)
                counter = 1;
        }
        public MelodySequence(OutputDevice o,MIDIPlayer player,ChannelMessage endMessage)
        {
            outDevice = o;
            sequence = new Queue<Tuple<int, ChannelMessage>>();
            this.player = player;
            this.endMessage = endMessage;
        }

        public void Clear()
        {
            outDevice.Send(endMessage);
            sequence.Clear();
            counter = 1;
        }

        internal void Clean()
        {
            outDevice.Send(endMessage);
        }
    }
}
