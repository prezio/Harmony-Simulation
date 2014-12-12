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
    /// <summary>
    /// Static class responible for playing melody of simulation.
    /// </summary>
    public static class Melody
    {
        private static MIDIPlayer _player = new MIDIPlayer(0, 16, 10);

        public static int tempo = 100;
        public static int divider = 16;
        public static bool common_tempo = false;
        public static bool common_divider = false;
        public static int[] tempi = { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
        public static int[] dividers = { 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 };
        public static int[][][] tempiInStages = new int[][][]
            {
                new int[][]
                {
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },

                },
                new int[][]
                {
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },

                },
               
            };
        public static int[][][] dividersInStages= new int[][][]
            {
                new int[][]
                {
                    new int[]{ 32,35,37,39,40,43,45,47,48,45,43,3,9,7,6,5 },

                },
                new int[][]
                {
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                },

            };
        public static int[][] sustainInStages = new int[][]
            {
                new int[]{100,},
                new int[]{100,},
            };
        public static bool[][] commonTempiInStages = new bool[][]
        {
            new bool[]{true,},
            new bool[]{true,},

        };
        public static bool[][] commonDividersInStages= new bool[][]
        {
            new bool[]{false,},
            new bool[]{false,},

        };
        public static int[][] mainTempiInStages= new int[][]
            {
                new int[]{100},
                new int[]{95},
            };
        public static int[][] mainDividersInStages = new int[][]
            {
                new int[]{16,},
                new int[]{16,},
            };
        public static int phase = 0;
        public static int stage = 0;
        public static int[] currentChords = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[][][]chords = new int[][][]{

            new int[][]
            {
                
                new int[]{29, 32, 40, 43, 51, 54, 62, 65, 73, 76, 84, 87, 95, 98},
                new int[]{28, 37, 39, 48, 50, 59, 61, 70, 72, 81, 83, 92, 94, 103},
                new int[]{33, 36, 44, 47, 55, 58, 66, 69, 77, 80, 88, 91, 99, 102},
                new int[]{32, 37, 43, 48, 54, 59, 65, 70, 76, 81, 87, 92, 98, 103},
                new int[]{33, 39, 44, 50, 55, 61, 66, 72, 77, 83, 88, 94, 99, 105},
                new int[]{29, 36, 40, 47, 51, 58, 62, 69, 73, 80, 84, 91, 95, 102},
                new int[]{33, 37, 44, 48, 55, 59, 66, 70, 77, 81, 88, 92, 99, 103},
                new int[]{32, 40, 43, 51, 54, 62, 65, 73, 76, 84, 87, 95, 98, 106},
                new int[]{37, 39, 48, 50, 59, 61, 70, 72, 81, 83, 92, 94, 103, 105},
                new int[]{36, 40, 47, 51, 58, 62, 69, 73, 80, 84, 91, 95, 102, 106},
                new int[]{33, 43, 44, 54, 55, 65, 66, 76, 77, 87, 88, 98, 99, 109},
                new int[]{32, 40, 43, 51, 54, 62, 65, 73, 76, 84, 87, 95, 98, 106},
                new int[]{33, 39, 44, 50, 55, 61, 66, 72, 77, 83, 88, 94, 99, 105},
                new int[]{28, 37, 39, 48, 50, 59, 61, 70, 72, 81, 83, 92, 94, 103},
                new int[]{29, 36, 40, 47, 51, 58, 62, 69, 73, 80, 84, 91, 95, 102},
                new int[]{28, 33, 39, 44, 50, 55, 61, 66, 72, 77, 83, 88, 94, 99}

            },
            new int[][]
            {
                new int[]{36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,
                73,74,75,76,77,78,79,80,81,82,83,84,85,86}
            },
        };
        public static int[] offset = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        /// <summary>
        /// Returns whether music is playing or not.
        /// </summary>
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
        /// <summary>
        /// Starts playing. It takes current state of simulation.
        /// </summary>
        public static void StartPlaying()
        {
            _player.Add(Simulation.SimulationBoardState.ToArray());
            _player.Start();
        }
        /// <summary>
        /// Stops playing simulation.
        /// </summary>
        public static void StopPlaying()
        {
            _player.Stop();
        }
        public static void ChangeStage()
        {
            lock (currentChords)
            {
                stage++;
                stage %= chords[phase].Length;
                for (int i = 0; i < 16; i++)
                {
                    currentChords[i] = 0;
                    offset[i] = 0;
                }
            }
        }
        public static void ChangePhase(int newPhase)
        {
            lock (currentChords)
            {
                phase = newPhase;
                stage = 0;
                for (int i = 0; i < 16; i++)
                {
                    currentChords[i] = 0;
                    offset[i] = 0;
                }
            }
        }
    }
}
