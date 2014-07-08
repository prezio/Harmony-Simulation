using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();
        private static int[,] _randomPermBoard = null;
        private static Tuple<int, int>[] _randomOrder = null;
        private static void CreateAndShuffle()
        {
            int n = SimulationParameters.boardHeight * SimulationParameters.boardWidth;
            int h = SimulationParameters.boardHeight, w = SimulationParameters.boardWidth;
            _randomPermBoard = new int[h, w];
            int temp1 = 0;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    _randomPermBoard[i, j] = temp1;
                    temp1++;
                }
            }

            while (n > 0)
            {
                n--;

                int k = RandomGen.Next(n);
                int i1 = n / h;
                int j1 = n % w;
                int i2 = k / h;
                int j2 = k % w;

                int temp2 = _randomPermBoard[i1, j1];
                _randomPermBoard[i1, j1] = _randomPermBoard[i2, j2];
                _randomPermBoard[i2, j2] = temp2;
            }
        }

        public static Random RandomGen
        {
            get
            {
                return _random;
            }
        }
        public static Tuple<int, int>[] RandomOrder
        {
            get
            {
                if (_randomOrder == null)
                {
                    int w = SimulationParameters.boardWidth, h = SimulationParameters.boardHeight;
                    _randomOrder = new Tuple<int, int>[w * h];
                    for (int i = 0; i < h; i++)
                    {
                        for (int j = 0; j < w; j++)
                        {
                            _randomOrder[RandomPermBoard[i, j]] = new Tuple<int, int>(i, j);
                        }
                    }
                }
                return _randomOrder;
            }
        }
        public static int[,] RandomPermBoard
        {
            get
            {
                if (_randomPermBoard == null)
                {
                    CreateAndShuffle();
                }
                return _randomPermBoard;
            }
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
