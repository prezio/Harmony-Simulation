using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public partial class Member2
    {
        public static double PitchInfluenceAmount = 0.05;
        public static double RhythmInfluenceAmount = 0.05;
        public static double DynamicsInfluenceAmount = 0.05;
        public static double PauseInfluenceAmount = 0.05;
        public static double RhythmDistortionInfluenceChance = 0.05;
        public static double DynamicsDistortionInfluenceChance = 0.05;
        public static double TypeInfluenceChance = 0.2;
        public static double GrowthChance = 0.1;
        public static double ShrinkChance = 0.1;
        public static double PeakMoveChance = 0.15;
        public static int PeakMaxMove = 7;
        public static double PauseChangeChance = 0.05;
        public static int PauseMaxChange = 20;
        public static double InitialRhythmChangeChance = 0.15;
        public static int InitialRhythmMaxChange = 2;
        public static double InitialDynamicsChangeChance = 0.15;
        public static int InitialDynamicsMaxChange = 20;
        public static double InitialChordChangeChance = 0.05;
        public static double ChordChangeChance = 0.05;
        public static double RhythmDistortionChangeChance = 0.05;
        public static double DynamicsDistortionChangeChance = 0.05;
        public static double PitchChangeChance = 0.05;
        public static int PitchMaxChange = 5;
        public static double RhythmChangeChance = 0.05;
        public static int RhythmMaxChange = 5;
        public static double DynamicsChangeChance = 0.05;
        public static int DynamicsMaxChange = 20;
        public static int PrefferedLength = 10;
        public static int PrefferedPauseLength = 60;
        public static double TypeChangeChance = 0.1;
    }
}
