using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PopuloApplication;
using MusicPopulation;

namespace PopuloApplication
{
    public static class Melody
    {

        private static MIDIPlayer _player = new MIDIPlayer(0, 16, 10);
        public static bool IsPlaying
        {
            get
            {
                return (!_player.need)||_player.adding;
            }
        }
        public static bool Unneeded()
        {
            return IsPlaying;
        }
        public static void StartPlaying()
        {
            _player.Add(Simulation.SimulationBoardState.ToArray());
            _player.Start();
        }
        public static void StopPlaying()
        {
            _player.Stop();
        }
    }
}
