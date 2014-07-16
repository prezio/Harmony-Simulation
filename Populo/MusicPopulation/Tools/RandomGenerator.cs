using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic]
        private static Random _local;

        public ThreadSafeRandom()
        {
            if (_local == null)
            {
                int seed;
                lock (_global)
                {
                    seed = _global.Next();
                }
                _local = new Random(seed);
            }
        }
        public int Next()
        {
            return _local.Next();
        }
        public int Next(int maxValue)
        {
            return _local.Next(maxValue);
        }
        public double NextDouble()
        {
            return _local.NextDouble();
        }
        public int Next(int a, int b)
        {
            return _local.Next(a, b);
        }
    }
    public static class RandomGenerator
    {
        public static Random RandomGen = new Random();
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
