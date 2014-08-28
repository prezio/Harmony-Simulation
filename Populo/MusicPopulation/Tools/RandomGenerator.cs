using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    /// <summary>
    /// Class responsible for generating thread safe Random Generators and some basic random algorithms.
    /// </summary>
    public static class RandomGenerator
    {
        private static readonly Random _global = new Random();

        /// <summary>
        /// Generates Random Generator
        /// </summary>
        public static Random GenerateRandomGen()
        {
            int seed;
            lock (_global)
            {
               seed = _global.Next();
            }
            return new Random(seed);
        }
        /// <summary>
        /// Generates randomly if you win or not.
        /// </summary>
        /// <param name="chance">chance of winning, should be between 0 and 1</param>
        /// <param name="randContext"> random context</param>
        /// <returns></returns>
        public static bool ChanceProbability(double chance, Random randContext)
        {
            if (randContext.NextDouble() <= chance)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Generates random sequence with legth k of numbers 0..n-1.
        /// </summary>
        /// <param name="n">n-1 is maximum number in sequence</param>
        /// <param name="k">length of sequence</param>
        /// <param name="randContext">random context</param>
        /// <returns>random sequence of k numbers from 0..n-1. Each number is given at most once</returns>
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
