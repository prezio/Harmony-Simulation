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
        private static MIDIPlayer _player = new MIDIPlayer(1, 16, 10);

        public static int tempo = 100;
        public static int divider = 16;
        public static bool common_tempo = false;
        public static bool common_divider = false;
        public static int[] tempi = { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };
        public static int[] dividers = { 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 };
        public static int phase = 0;
        public static int stage = 0;
        public static int[] currentChords = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[][][][] chords = new int[][][][]{
        new int[][][]
            { 
            new int[][]{new int[] { 30, 33, 43, 46, 56, 60, 72, 73, 83, 85, 96 } }, 
            new int[][]{new int[] { 21, 27, 34, 40, 47, 53, 64, 68, 77, 79, 89, 92 } },
            new int[][]{new int[] { 26, 30, 39, 43, 52, 59, 69, 72, 85, 86, 98 } },
            new int[][]{new int[] { 18, 23, 31, 36, 44, 49, 60, 66, 73, 81, 90, 94 } }
            },
        new int[][][]
            { 
            new int[][]
                {
                    new int[] { 24, 26, 37, 39, 50, 56, 65, 69, 82, 85, 95, 98, 108 }, 
                    new int[] { 20, 30, 33, 43, 46, 57, 62, 70, 79, 86, 92, 99, 105 },
                    new int[] { 26, 27, 39, 40, 52, 63, 65, 78, 82, 91, 95, 104 },
                }, 
            new int[][]
                {
                    new int[] { 22, 31, 35, 44, 48, 56, 59, 69, 75, 83, 88, 96, 101 }, 
                    new int[] { 23, 26, 36, 39, 49, 53, 65, 66, 76, 78, 89, 91, 102 },
                    new int[] { 20, 27, 33, 40, 46, 57, 61, 70, 72, 82, 85, 95, 98},
                }, 
            new int[][]
                {
                    new int[] { 22, 26, 35, 39, 48, 52, 62, 65, 78, 79, 91, 92, 104 }, 
                    new int[] { 19, 27, 32, 40, 45, 53, 59, 66, 74, 83, 87, 96, 100 },
                    new int[] { 20, 22, 33, 35, 46, 52, 61, 65, 75, 78, 88, 91, 101},
                }
            },
        new int[][][]
            { 
            new int[][]
                {
                    new int[] { 33, 35, 46, 52, 61, 65, 75, 78, 88 }, 
                    new int[] { 29, 39, 42, 53, 58, 66, 72, 79, 85 },
                    new int[] { 32, 33, 45, 48, 59, 61, 74, 78, 87 },
                    new int[] { 28, 37, 41, 52, 55, 65, 71, 79, 84, }, 
                    new int[] { 33, 36, 46, 46, 58, 59, 72, 74, 85 },
                    new int[] { 32, 39, 45, 50, 54, 63, 68, 78, 81 },
                    new int[] { 33, 37, 46, 49, 59, 62, 71, 72, 84 },
                    new int[] { 32, 40, 45, 52, 58, 65, 67, 76, 80 },
                }, 
            new int[][]
                {
                    new int[] { 37, 39, 50, 50, 59, 63, 72, 75, 85 }, 
                    new int[] { 33, 43, 46, 53, 58, 66, 71, 78, 84 },
                    new int[] { 39, 40, 52, 52, 63, 65, 72, 76, 85},
                    new int[] { 35, 44, 48, 56, 59, 69, 71, 79, 84 },
                    new int[] { 40, 43, 53, 53, 65, 66, 76, 78, 89, }, 
                    new int[] { 39, 46, 52, 57, 61, 70, 72, 82, 85 },
                    new int[] { 43, 47, 56, 56, 66, 69, 78, 79, 91 },                
                    new int[] { 42, 50, 55, 59, 65, 72, 74, 83, 87 },
                },
            new int[][]
                {
                    new int[] { 47, 49, 60, 60, 69, 73, 79, 82, 92 }, 
                    new int[] { 43, 53, 56, 63, 68, 76, 78, 85, 91 },
                    new int[] { 46, 47, 59, 62, 73, 75, 82, 86, 95 },
                    new int[] { 42, 51, 55, 66, 69, 79, 81, 89, 94, }, 
                    new int[] { 43, 46, 56, 60, 72, 73, 86, 88, 99 },
                    new int[] { 40, 47, 53, 64, 68, 77, 82, 92, 95 },
                    new int[] { 42, 46, 55, 59, 69, 72, 85, 86, 98 },
                    new int[] { 39, 47, 52, 60, 66, 73, 81, 90, 94 },
                },
            new int[][]
                {
                    new int[] { 40, 42, 53, 59, 68, 72, 82, 85, 95 }, 
                    new int[] { 36, 46, 49, 60, 65, 73, 79, 86, 92 },
                    new int[] { 39, 40, 52, 55, 66, 68, 81, 85, 94 },
                    new int[] { 35, 44, 48, 59, 62, 72, 78, 86, 91 }, 
                    new int[] { 36, 39, 49, 53, 65, 66, 79, 81, 92 },
                    new int[] { 33, 40, 46, 57, 61, 70, 75, 85, 88 },
                    new int[] { 32, 36, 45, 52, 62, 65, 78, 79, 91 },
                    new int[] { 29, 37, 42, 53, 59, 66, 74, 83, 87 },
                }
            }
        };

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
                }
            }
        }
    }
}
