using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MusicPopulation
{
    public static class Simulation
    {
        private static Board _board = null;
        private static AreaThread[] _arrayAreaThread = null;
        private static Thread[] GenerateThreads()
        {
            List<Thread> result = new List<Thread>();
            foreach (var areaThread in AreaThreads)
            {
                result.Add(new Thread(new ThreadStart(areaThread.Evolve)));
            }
            return result.ToArray();
        }

        public static void EvolveUsingThreads()
        {
            var threads = GenerateThreads();
            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
        public static void Evolve()
        {
            foreach (var area in AreaThreads)
            {
                area.Evolve();
            }
        }
        public static Tuple<int,int> GetBestInArea(int x1, int y1, int x2, int y2)
        {
            int best_x = -1, best_y = -1, best_rank = -1;
            int i, j = y1;

            while (j <= y2)
            {
                i = x1;
                while (i <= x2)
                {
                    var member = SimulationBoard[i, j];
                    if (member != null && member.Rank() > best_rank)
                    {
                        best_x = i;
                        best_y = j;
                        best_rank = member.Rank();
                    }
                    i++;
                }
                j++;
            }
            if (best_rank == -1)
                return null;
            else
                return new Tuple<int, int>(best_x, best_y);
        }
        public static AreaThread[] AreaThreads
        {
            get
            {
                if (_arrayAreaThread == null)
                {
                    List<AreaThread> result = new List<AreaThread>();
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            AreaManager area = new AreaManager(i * 64, j * 64, 64, 64);
                            result.Add(new AreaThread(area));
                        }
                    }
                    _arrayAreaThread = result.ToArray();
                }
                return _arrayAreaThread;
            }
        }
        public static Board SimulationBoard
        {
            get
            {
                if (_board == null)
                {
                    _board = new Board(SimulationParameters.boardWidth, SimulationParameters.boardHeight, SimulationParameters.populationGrowth);
                }
                return _board;
            }
        }
    }
}
