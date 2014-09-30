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
        public static int[][][] tempiInStages = new int[][][]
            {
                new int[][]
                {
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 }
                },
                new int[][]
                {
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 }
                },
                new int[][]
                {
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 }
                },
                new int[][]
                {
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 },
                    new int[]{ 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 }
                }
            };
        public static int[][][] dividersInStages= new int[][][]
            {
                new int[][]
                {
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 }
                },
                new int[][]
                {
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 }
                },
                new int[][]
                {
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 },
                    new int[]{ 32,35,37,39,40,43,45,47,51,52,53,57,58,59,60,64 }
                },
                new int[][]
                {
                    new int[]{ 16,16,12,12,16,16,32,32,24,48,16,48,32,32,24,32 },
                    new int[]{ 16,32,24,48,16,16,32,32,24,48,16,48,32,32,24,32 },
                    
                }
            };
        public static bool[][] commonTempiInStages = new bool[][]
        {
            new bool[]{true,true,true,true},
            new bool[]{true,true,true},
            new bool[]{true,true,true,true},
            new bool[]{true,true,true,true}
        };
        public static bool[][] commonDividersInStages= new bool[][]
        {
            new bool[]{false,false,false,false},
            new bool[]{false,false,false},
            new bool[]{false,false,false,false},
            new bool[]{false,false,false,false}
        };
        public static int[][] mainTempiInStages= new int[][]
            {
                new int[]{55,55,55,55},
                new int[]{200,220,240},
                new int[]{80,80,80,80},
                new int[]{108,108},
            };
        public static int[][] mainDividersInStages = new int[][]
            {
                new int[]{16,16,16,16},
                new int[]{16,16,16},
                new int[]{16,16,16,16},
                new int[]{16,16,16,16},
            };
        public static int phase = 0;
        public static int stage = 0;
        public static int[] currentChords = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int[][][][][] chords = new int[][][][][]{
        new int[][][][]
            { 
            new int[][][]
            {
                new int[][] 
                {
                    new int[] {30, 33, 43},
                    new int[] {30, 33, 43},
                    new int[] {30, 33, 43},
                    new int[] {30, 33, 43}, 
                    new int[] {46, 56, 60},
                    new int[] {46, 56, 60},
                    new int[] {46, 56, 60},
                    new int[] {46, 56, 60},
                    new int[] {72, 73, 83},
                    new int[] {72, 73, 83},
                    new int[] {72, 73, 83},
                    new int[] {72, 73, 83},
                    new int[]{85, 96 },
                    new int[]{85, 96 },
                    new int[]{85, 96 },
                    new int[]{85, 96 }
                }
            },
            new int[][][]
            {
                new int[][] 
                { 
                    new int[] {21, 27, 34}, 
                    new int[] {21, 27, 34},
                    new int[] {21, 27, 34},
                    new int[] {21, 27, 34},
                    new int[] {40, 47, 53},
                    new int[] {40, 47, 53},
                    new int[] {40, 47, 53},
                    new int[] {40, 47, 53},
                    new int[] {64, 68, 77},
                    new int[] {64, 68, 77},
                    new int[] {64, 68, 77},
                    new int[] {64, 68, 77},
                    new int[] {79, 89, 92},
                    new int[] {79, 89, 92},
                    new int[] {79, 89, 92},
                    new int[] {79, 89, 92},
                } 
            },
            new int[][][]
            {
                new int[][] 
                {
                    new int[]{26, 30, 39},
                    new int[]{26, 30, 39},
                    new int[]{26, 30, 39},
                    new int[]{26, 30, 39},
                    new int[]{43, 52, 59},
                    new int[]{43, 52, 59},
                    new int[]{43, 52, 59},
                    new int[]{43, 52, 59},
                    new int[] {69, 72, 85},
                    new int[] {69, 72, 85},
                    new int[] {69, 72, 85},
                    new int[] {69, 72, 85},
                    new int[] {86, 98},
                    new int[] {86, 98},
                    new int[] {86, 98},
                    new int[] {86, 98},
                } 
            },
            new int[][][]
            {
                new int[][] 
                {
                    new int[]{18, 23, 31},
                    new int[]{18, 23, 31},
                    new int[]{18, 23, 31},
                    new int[]{18, 23, 31},
                    new int[]{36, 44, 49},
                    new int[]{36, 44, 49},
                    new int[]{36, 44, 49},
                    new int[]{36, 44, 49},
                    new int[]{60, 66, 73},
                    new int[]{60, 66, 73},
                    new int[]{60, 66, 73},
                    new int[]{60, 66, 73},
                    new int[]{81, 90, 94 },
                    new int[]{81, 90, 94 },
                    new int[]{81, 90, 94 },
                    new int[]{81, 90, 94 },
                } 
            }
        },
        new int[][][][]
            { 
            new int[][][]
                {
                    new int[][] 
                    {
                        new int[]{24, 26, 37},
                        new int[]{24, 26, 37},
                        new int[]{24, 26, 37},
                        new int[]{24, 26, 37},
                        new int[]{39, 50, 56,},
                        new int[]{39, 50, 56,},
                        new int[]{39, 50, 56,},
                        new int[]{39, 50, 56,},
                        new int[]{65, 69, 82},
                        new int[]{65, 69, 82},
                        new int[]{65, 69, 82},
                        new int[]{65, 69, 82},
                        new int[] {85, 95, 98, 108 },
                        new int[] {85, 95, 98, 108 },
                        new int[] {85, 95, 98, 108 },
                        new int[] {85, 95, 98, 108 },
                    }, 
                    new int[][] 
                    {
                        new int[]{20, 30, 33},
                        new int[]{20, 30, 33},
                        new int[]{20, 30, 33},
                        new int[]{20, 30, 33},
                        new int[]{43, 46, 57},
                        new int[]{43, 46, 57},
                        new int[]{43, 46, 57},
                        new int[]{43, 46, 57},
                        new int[]{62, 70, 79},
                        new int[]{62, 70, 79},
                        new int[]{62, 70, 79},
                        new int[]{62, 70, 79},
                        new int[]{86, 92, 99, 105 },
                        new int[]{86, 92, 99, 105 },
                        new int[]{86, 92, 99, 105 },
                        new int[]{86, 92, 99, 105 },
                    },
                    new int[][] 
                    {
                        new int[] {26, 27, 39},
                        new int[] {26, 27, 39},
                        new int[] {26, 27, 39},
                        new int[] {26, 27, 39},
                        new int[] {40, 52, 63},
                        new int[] {40, 52, 63},
                        new int[] {40, 52, 63},
                        new int[] {40, 52, 63},
                        new int[]{65, 78, 82},
                        new int[]{65, 78, 82},
                        new int[]{65, 78, 82},
                        new int[]{65, 78, 82},
                        new int[]{91, 95, 104 },
                        new int[]{91, 95, 104 },
                        new int[]{91, 95, 104 },
                        new int[]{91, 95, 104 },
                    },
                }, 
            new int[][][]
                {
                    new int[][] 
                    {
                        new int[]{22, 31, 35},
                        new int[]{22, 31, 35},
                        new int[]{22, 31, 35},
                        new int[]{22, 31, 35},
                        new int[]{44, 48, 56},
                        new int[]{44, 48, 56},
                        new int[]{44, 48, 56},
                        new int[]{44, 48, 56},
                        new int[]{59, 69, 75},
                        new int[]{59, 69, 75},
                        new int[]{59, 69, 75},
                        new int[]{59, 69, 75},
                        new int[]{83, 88, 96, 101},
                        new int[]{83, 88, 96, 101},
                        new int[]{83, 88, 96, 101},
                        new int[]{83, 88, 96, 101},
                    }, 
                    new int[][] 
                    {
                        new int[]{23, 26, 36},
                        new int[]{23, 26, 36},
                        new int[]{23, 26, 36},
                        new int[]{23, 26, 36},
                        new int[]{39, 49, 53},
                        new int[]{39, 49, 53},
                        new int[]{39, 49, 53},
                        new int[]{39, 49, 53},
                        new int[]{65, 66, 76},
                        new int[]{65, 66, 76},
                        new int[]{65, 66, 76},
                        new int[]{65, 66, 76},
                        new int[]{78, 89, 91, 102},
                        new int[]{78, 89, 91, 102},
                        new int[]{78, 89, 91, 102},
                        new int[]{78, 89, 91, 102},
                    },
                    new int[][] 
                    {
                        new int[]{20, 27, 33},
                        new int[]{20, 27, 33},
                        new int[]{20, 27, 33},
                        new int[]{20, 27, 33},
                        new int[] {40, 46, 57},
                        new int[] {40, 46, 57},
                        new int[] {40, 46, 57},
                        new int[] {40, 46, 57},
                        new int[]{61, 70, 72,},
                        new int[]{61, 70, 72,},
                        new int[]{61, 70, 72,},
                        new int[]{61, 70, 72,},
                        new int[]{82, 85, 95, 98},
                        new int[]{82, 85, 95, 98},
                        new int[]{82, 85, 95, 98},
                        new int[]{82, 85, 95, 98},
                    },
                }, 
            new int[][][]
                {
                    new int[][]
                    {
                        new int[]{22, 26, 35},
                        new int[]{22, 26, 35},
                        new int[]{22, 26, 35},
                        new int[]{22, 26, 35},
                        new int[]{39, 48, 52},
                        new int[]{39, 48, 52},
                        new int[]{39, 48, 52},
                        new int[]{39, 48, 52},
                        new int[]{62, 65, 78},
                        new int[]{62, 65, 78},
                        new int[]{62, 65, 78},
                        new int[]{62, 65, 78},
                        new int[]{79, 91, 92, 104}, 
                        new int[]{79, 91, 92, 104},
                        new int[]{79, 91, 92, 104},
                        new int[]{79, 91, 92, 104},
                    }, 
                    new int[][] 
                    { 
                        new int[]{19, 27, 32},
                        new int[]{19, 27, 32},
                        new int[]{19, 27, 32},
                        new int[]{19, 27, 32},
                        new int[]{40, 45, 53},
                        new int[]{40, 45, 53},
                        new int[]{40, 45, 53},
                        new int[]{40, 45, 53},
                        new int[]{59, 66, 74},
                        new int[]{59, 66, 74},
                        new int[]{59, 66, 74},
                        new int[]{59, 66, 74},
                        new int[]{83, 87, 96, 100},
                        new int[]{83, 87, 96, 100},
                        new int[]{83, 87, 96, 100},
                        new int[]{83, 87, 96, 100},
                    },
                    new int[][] 
                    {
                        new int[]{20, 22, 33},
                        new int[]{20, 22, 33},
                        new int[]{20, 22, 33},
                        new int[]{20, 22, 33},
                        new int[]{35, 46, 52},
                        new int[]{35, 46, 52},
                        new int[]{35, 46, 52},
                        new int[]{35, 46, 52},
                        new int[]{61, 65, 75},
                        new int[]{61, 65, 75},
                        new int[]{61, 65, 75},
                        new int[]{61, 65, 75},
                        new int[]{78, 88, 91, 101},
                        new int[]{78, 88, 91, 101},
                        new int[]{78, 88, 91, 101},
                        new int[]{78, 88, 91, 101},
                    },
                }
            },
        new int[][][][]
            { 
            new int[][][]
                {
                    new int[][] 
                    {
                        new int[]{33, 35}, 
                        new int[]{33, 35, 46},
                        new int[]{ 35, 46},
                        new int[]{ 35, 46},
                        new int[]{ 35, 46, 52},
                        new int[]{ 46, 52},
                        new int[]{ 46, 52, 61},
                        new int[]{ 52, 61, 65},
                        new int[]{ 52, 61, 65},
                        new int[]{  61, 65, 75},
                        new int[]{  65, 75},
                        new int[]{  65, 75,78},
                        new int[]{ 75,78},
                        new int[]{ 75,78},
                        new int[]{ 75,78,88},
                        new int[]{ 78,88 }
                    }, 
                    new int[][] 
                    {
                        new int[]{29, 39}, 
                        new int[]{29, 39, 42, },
                        new int[]{ 39, 42,},
                        new int[]{ 39, 42,},
                        new int[]{39, 42, 53,},
                        new int[]{ 42, 53,},
                        new int[]{ 42, 53, 58,},
                        new int[]{  53, 58, 66,},
                        new int[]{  53, 58, 66,},
                        new int[]{  58, 66, 72,},
                        new int[]{  66, 72,},
                        new int[]{  66, 72, 79},
                        new int[]{ 72, 79},
                        new int[]{ 72, 79},
                        new int[]{ 72, 79, 85},
                        new int[]{ 79, 85},
                        
                    },
                    new int[][] 
                    {
                        new int[]{32, 33}, 
                        new int[]{32, 33, 45},
                        new int[]{ 33, 45,},
                        new int[]{ 33, 45,},
                        new int[]{33, 45, 48},
                        new int[]{  45, 48,},
                        new int[]{ 45, 48, 59},
                        new int[]{  48, 59, 61},
                        new int[]{  48, 59, 61},
                        new int[]{  59, 61, 74,},
                        new int[]{  61, 74},
                        new int[]{  61, 74, 78,},
                        new int[]{  74, 78,},
                        new int[]{  74, 78,},
                        new int[]{ 74, 78, 87},
                        new int[]{ 78, 87 },
                        
                    },
                    new int[][] 
                    {
                        new int[]{28, 37}, 
                        new int[]{28, 37, 41,},
                        new int[]{ 37, 41,},
                        new int[]{ 37, 41,},
                        new int[]{37, 41, 52,},
                        new int[]{  41, 52,},
                        new int[]{ 41, 52, 55},
                        new int[]{  52, 55, 65},
                        new int[]{  52, 55, 65},
                        new int[]{  55, 65, 71},
                        new int[]{  65, 71,},
                        new int[]{  65, 71, 79,},
                        new int[]{  71, 79,},
                        new int[]{  71, 79,},
                        new int[]{ 71, 79, 84},
                        new int[]{ 79, 84, },
                        
                    },
                    new int[][] 
                    {
                        new int[]{33, 36}, 
                        new int[]{ 33, 36, 46},
                        new int[]{ 36, 46,},
                        new int[]{ 36, 46,},
                        new int[]{36, 46, 46,},
                        new int[]{   46, 46,},
                        new int[]{ 46, 46, 58,},
                        new int[]{   46, 58, 59},
                        new int[]{   46, 58, 59},
                        new int[]{  58, 59, 72},
                        new int[]{  59, 72,},
                        new int[]{ 59, 72, 74,},
                        new int[]{ 72, 74,},
                        new int[]{ 72, 74,},
                        new int[]{ 72, 74, 85},
                        new int[]{  74, 85  },
                        
                    },
                    new int[][] 
                    {
                        new int[]{32, 39, }, 
                        new int[]{ 32, 39, 45},
                        new int[]{ 39, 45,},
                        new int[]{ 39, 45,},
                        new int[]{39, 45, 50,},
                        new int[]{  45, 50},
                        new int[]{  45, 50, 54,},
                        new int[]{   50, 54, 63,},
                        new int[]{   50, 54, 63,},
                        new int[]{   54, 63, 68,},
                        new int[]{  63, 68,},
                        new int[]{  63, 68, 78,},
                        new int[]{ 68, 78,},
                        new int[]{ 68, 78,},
                        new int[]{ 68, 78, 81},
                        new int[]{ 78, 81  },
                        
                    },
                     new int[][] 
                    {
                        new int[]{33, 37 }, 
                        new int[]{ 37, 46, 49},
                        new int[]{  37, 46},
                        new int[]{  37, 46},
                        new int[]{37, 46, 49},
                        new int[]{  46, 49},
                        new int[]{  46, 49, 59,},
                        new int[]{    49, 59, 62,},
                        new int[]{    49, 59, 62,},
                        new int[]{  59, 62, 71,},
                        new int[]{   62, 71},
                        new int[]{   62, 71, 72,},
                        new int[]{ 71, 72,},
                        new int[]{ 71, 72,},
                        new int[]{71, 72, 84},
                        new int[]{ 72, 84  },
                        
                    },
                    new int[][] 
                    {
                        new int[]{32, 40 }, 
                        new int[]{ 32, 40, 45,},
                        new int[]{   40, 45,},
                        new int[]{  40, 45,},
                        new int[]{ 40, 45, 52,},
                        new int[]{ 45, 52,},
                        new int[]{  45, 52, 58},
                        new int[]{   52, 58, 65,},
                        new int[]{    52, 58, 65,},
                        new int[]{ 58, 65, 67,},
                        new int[]{   65, 67},
                        new int[]{  65, 67, 76},
                        new int[]{ 67, 76,},
                        new int[]{ 67, 76,},
                        new int[]{67, 76, 80},
                        new int[]{  76, 80  },
                        
                    },
                    
                   
                }, 
            //new int[][][]
            //    {
            //        new int[][] { 37, 39, 50, 50, 59, 63, 72, 75, 85 }, 
            //        new int[][] { 33, 43, 46, 53, 58, 66, 71, 78, 84 },
            //        new int[][] { 39, 40, 52, 52, 63, 65, 72, 76, 85},
            //        new int[][] { 35, 44, 48, 56, 59, 69, 71, 79, 84 },
            //        new int[][] { 40, 43, 53, 53, 65, 66, 76, 78, 89, }, 
            //        new int[][] { 39, 46, 52, 57, 61, 70, 72, 82, 85 },
            //        new int[][] { 43, 47, 56, 56, 66, 69, 78, 79, 91 },                
            //        new int[][] { 42, 50, 55, 59, 65, 72, 74, 83, 87 },
            //    },
            //new int[][][]
            //    {
            //        new int[][] { 47, 49, 60, 60, 69, 73, 79, 82, 92 }, 
            //        new int[][] { 43, 53, 56, 63, 68, 76, 78, 85, 91 },
            //        new int[][] { 46, 47, 59, 62, 73, 75, 82, 86, 95 },
            //        new int[][] { 42, 51, 55, 66, 69, 79, 81, 89, 94, }, 
            //        new int[][] { 43, 46, 56, 60, 72, 73, 86, 88, 99 },
            //        new int[][] { 40, 47, 53, 64, 68, 77, 82, 92, 95 },
            //        new int[][] { 42, 46, 55, 59, 69, 72, 85, 86, 98 },
            //        new int[][] { 39, 47, 52, 60, 66, 73, 81, 90, 94 },
            //    },
            //new int[][][]
            //    {
            //        new int[][] { 40, 42, 53, 59, 68, 72, 82, 85, 95 }, 
            //        new int[][] { 36, 46, 49, 60, 65, 73, 79, 86, 92 },
            //        new int[][] { 39, 40, 52, 55, 66, 68, 81, 85, 94 },
            //        new int[][] { 35, 44, 48, 59, 62, 72, 78, 86, 91 }, 
            //        new int[][] { 36, 39, 49, 53, 65, 66, 79, 81, 92 },
            //        new int[][] { 33, 40, 46, 57, 61, 70, 75, 85, 88 },
            //        new int[][] { 32, 36, 45, 52, 62, 65, 78, 79, 91 },
            //        new int[][] { 29, 37, 42, 53, 59, 66, 74, 83, 87 },
            //    }
            },
        new int[][][][]
            { 
             new int[][][]
            {
                new int[][] 
                {
                    new int[] {48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96},
                    new int[] {48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96},
                    new int[] {48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96},
                    new int[] {24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48}, 
                    new int[] {24,25,26,27,28,29,30,31,32,33,34,35},
                    new int[] {36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95},
                    new int[] {24,25,26,27,28,29,30,31,32,33,34,35},
                    new int[] {24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75},
                    new int[] {72, 73, 83},
                    new int[] {72, 73, 83},
                    new int[] {72, 73, 83},
                    new int[] {72, 73, 83},
                    new int[]{85, 96 },
                    new int[]{85, 96 },
                    new int[]{85, 96 },
                    new int[]{85, 96 }
                }
            },
            new int[][][]
            {
                new int[][] 
                { 
                    new int[] {21, 27, 34}, 
                    new int[] {21, 27, 34},
                    new int[] {21, 27, 34},
                    new int[] {21, 27, 34},
                    new int[] {40, 47, 53},
                    new int[] {40, 47, 53},
                    new int[] {40, 47, 53},
                    new int[] {40, 47, 53},
                    new int[] {64, 68, 77},
                    new int[] {64, 68, 77},
                    new int[] {64, 68, 77},
                    new int[] {64, 68, 77},
                    new int[] {79, 89, 92},
                    new int[] {79, 89, 92},
                    new int[] {79, 89, 92},
                    new int[] {79, 89, 92},
                } 
            },
            
            
        },
        
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
