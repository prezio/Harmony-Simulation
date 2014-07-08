using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public static class SimulationParameters
    {
        public static int boardWidth = 256, boardHeight = 256;
        public static int populationGrowth = 256*256 / 2;
        public static int radiusReproduce = 2;
        public static double deathLimit = 100;
        public static double alfa(int param)
        {
            return 1;
        }

        public static int[] modifyAmount = new int[] { 3, 5, 15 };
        public static double[] influenceAmount = new double[] { 0.2, 0.2, 0.2 };
        public static double[] transposeChance = new double[] { 0.05, 0.05, 0.05 };
        public static double[] exchangeChance = new double[] { 0.05, 0.05, 0.05 };
        public static double[] modifyChance = new double[] { 0.05, 0.05, 0.05 };
        public static double growthChance = 0.05;
        public static double shrinkChance = 0.05;
    }
}
