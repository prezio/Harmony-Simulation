using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MusicPopulation
{
    public static class Simulation
    {
        private static AreaManager[] CreateAreas()
        {
            Console.WriteLine("Create Area");
            List<AreaManager> result = new List<AreaManager>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    AreaManager area = new AreaManager(i * 64, j * 64, 64, 64);
                    result.Add(area);
                }
            }
            return result.ToArray();
        }

        public static Board SimulationBoard = new Board(SimulationParameters.boardWidth, SimulationParameters.boardHeight, SimulationParameters.populationGrowth);
        public static AreaManager[] Areas = CreateAreas();
        public static void EvolveUsingThreads()
        {
            int events = 16;

            ManualResetEvent[] doneEvents = new ManualResetEvent[events];

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.Evolve, null);
            }

            WaitHandle.WaitAll(doneEvents);
        }
        public static void Evolve()
        {
            int i = 0;
            foreach (var area in Areas)
            {
                area.KillWeaksWhoDoesNotServeTheEmperorWell();
                area.ReproduceMenToHaveMoreServantsOfTheEmperor();
                area.MutateWeaksSoTheyCanServeEmperorBetter();
                area.InfluenceMenWithSongsGlorifyingEmperor();
                i++;
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
    }
}
