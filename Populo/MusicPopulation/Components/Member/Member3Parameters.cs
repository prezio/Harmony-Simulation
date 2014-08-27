using System;

namespace MusicPopulation
{
    public partial class Member3
    {
        public static int[] ModifyAmount = new int[] { 3, 2, 5, 3, 15, 3,2 };
        public static double[] InfluenceAmount = new double[] { 0.2, 0.2, 0.2, 0.2, 0.2, 0.2,0 };
        public static double[] TransposeChance = new double[] { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
        public static double[] ExchangeChance = new double[] { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
        public static double[] ModifyChance = new double[] { 0.05, 0.05, 0.05, 0.05, 0.05, 0.05, 0.05 };
        public static double GrowthChance = 0.05;
        public static double ShrinkChance = 0.05;
        public static int PrefferedLength = 300;
        public static int PrefferedGroups =10;
        public static int PrefferedNotes = 50;
        public static int PlayedGroup = 0;
    }
}