using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public static class SimulationParameters
    {
        public static int[] modifyAmount = new int[] { 3, 5, 15 };
        public static double[] influenceAmount = new double[] { 0.2, 0.2, 0.2 };
        public static double[] transposeChance = new double[] { 0.05, 0.05, 0.05 };
        public static double[] exchangeChance = new double[] { 0.05, 0.05, 0.05 };
        public static double[] modifyChance = new double[] { 0.05, 0.05, 0.05 };
        public static double growthChance = 0.05;
        public static double shrinkChance = 0.05;
    }
}
