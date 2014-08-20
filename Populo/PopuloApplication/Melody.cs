using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PopuloApplication.MIDI;

namespace PopuloApplication
{
    public static class Melody
    {

        private static MIDIPlayer player;
        public static Melody()
        {
            player = new MIDIPlayer(0, 16, 3);
        }
        public static bool IsPlaying
        {
            get
            {
                return player.need;
            }
        }
        public static void StartPlaying()
        {
            player.Start();
        }
        public static void StopPlaying()
        {
            player.Stop();
        }
    }
}
