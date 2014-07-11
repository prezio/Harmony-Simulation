using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public static class SimulationParameters
    {
        public static int BoardWidth = 256, BoardHeight = 256;
        public static int PopulationGrowth = 256 * 256 / 2;
        public static int PercentDeath = 10;
        public static int MaxSteps = 20;
        public static double Alfa(int param)
        {
            return 1;
        }

        public static int[] ModifyAmount = new int[] { 3, 5, 15 };
        public static double[] InfluenceAmount = new double[] { 0.2, 0.2, 0.2 };
        public static double[] TransposeChance = new double[] { 0.05, 0.05, 0.05 };
        public static double[] ExchangeChance = new double[] { 0.05, 0.05, 0.05 };
        public static double[] ModifyChance = new double[] { 0.05, 0.05, 0.05 };
        public static double GrowthChance = 0.05;
        public static double ShrinkChance = 0.05;
    }
}
