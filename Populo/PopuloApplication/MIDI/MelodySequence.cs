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
        Queue<Tuple<int, ChannelMessage, ChannelMessage>> sequence;
        int counter = -1;
        ChannelMessage prev = null;
        OutputDevice outDevice;
        MIDIPlayer player;
        public void Tick()
        {
            
                counter--;

                if (counter == 0)
                {
                    if (prev != null)
                    {
                        outDevice.Send(prev);
                    }

                    if (sequence.Count > 0)
                    {
                        Tuple<int, ChannelMessage, ChannelMessage> t = sequence.Dequeue();
                        outDevice.Send(t.Item2);
                        counter = t.Item1;
                        prev = t.Item3;
                        
                    }
                }
                if (sequence.Count < 4)
                {
                    player.need = true;
                }

        }
        public void Add(int i, ChannelMessage On, ChannelMessage Off)
        {
            sequence.Enqueue(new Tuple<int, ChannelMessage, ChannelMessage>(i, On, Off));
            if (counter < 0)
                counter = 1;
        }
        public void SimpleAdd(int i, ChannelMessage On, ChannelMessage Off)
        {
            sequence.Enqueue(new Tuple<int, ChannelMessage, ChannelMessage>(i, On, Off));
           
        }
        public void Correct()
        {
            
            if (counter < 0)
                counter = 1;
        }
        public MelodySequence(OutputDevice o,MIDIPlayer player)
        {
            outDevice = o;
            sequence = new Queue<Tuple<int, ChannelMessage, ChannelMessage>>();
            this.player = player;
        }
        public void clean()
        {
            if (prev != null)
            {
                outDevice.Send(prev);
            }
        }
    }
}
