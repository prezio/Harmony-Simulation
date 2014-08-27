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
        public static int tempo = 100;
        public static int divider = 16;
        public static bool common_tempo = false;
        public static bool common_divider = false;
        public static int[] tempi = { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
        public static int[] dividers = { 16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31 };
        private static MIDIPlayer _player = new MIDIPlayer(1, 16, 10);
        public static int phase = 0;
        public static int[][] chords = new int[][] { new int[] { 42, 43, 47, 49, 50, 54 }, new int[] { 45, 48, 52, 57, 60, 64 }, new int[] { 48, 52, 55, 59, 60, 64, 67, 71, 72 } };
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
