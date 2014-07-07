using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();
        private static int[,] _randomPermBoard = null;
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
    }
}
