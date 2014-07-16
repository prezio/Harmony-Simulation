using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public static class RandomGenerator
    {
        private static readonly Random _global = new Random();

        public static Random GenerateRandomGen()
        {
            int seed;
            lock (_global)
            {
               seed = _global.Next();
            }
            return new Random(seed);
        }
        public static bool ChanceProbability(double chance, Random randContext)
        {
            if (randContext.NextDouble() <= chance)
                return true;
            else
                return false;
        }
        public static int[] RandomPermutation(int n, int k, Random randContext)
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
                int r = randContext.Next(n);
                int tmp = array[r];
                array[r] = array[n];
                array[n] = tmp;
            }

            return array.Skip(n1 - k).ToArray();
        }
    }
}
