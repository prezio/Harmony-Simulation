using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public static class RandomGenerator
    {
        public static Random RandomGen = new Random();
        public static void SetSeed(int seed)
        {
            RandomGen = new Random(seed);
        }
        public static bool ChanceProbability(double chance)
        {
            if (RandomGen.NextDouble() <= chance)
                return true;
            else
                return false;
        }
        public static int[] RandomPermutation(int n, int k)
        {
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = i;
            }
            int n1 = n;

            while (n > n1 - k)
            {
                n--;
                int r = RandomGen.Next(n);
                int tmp = array[r];
                array[r] = array[n];
                array[n] = tmp;
            }

            return array.Skip(n1 - k).ToArray();
        }
    }
}
