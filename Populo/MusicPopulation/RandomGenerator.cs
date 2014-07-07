using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();
        private static int[,] _randomPermBoard = null;

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
                if (_randomPermBoard != null)
                {
                    return _randomPermBoard;
                }
                _randomPermBoard = new int[SimulationParameters.boardHeight, SimulationParameters.boardWidth];

            }
        }
    }
}
