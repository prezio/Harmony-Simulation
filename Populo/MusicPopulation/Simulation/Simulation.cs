using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MusicPopulation
{
    public static class Simulation
    {
        private static AreaManager[] CreateAreas()
        {
            Debug.WriteLine("Create Area");
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

        /// <summary>
        /// Simulation Board with Randomly generated individuals.
        /// </summary>
        public static Board SimulationBoard = new Board(SimulationParameters.BoardWidth, SimulationParameters.BoardHeight, SimulationParameters.PopulationGrowth);
        /// <summary>
        /// Each AreaManager commands its own seperated area.
        /// </summary>
        public static AreaManager[] Areas = CreateAreas();

        /// <summary>
        /// Function generating one step of evolutionary algorithm.
        /// Algorithm involves the use of threading.
        /// </summary>
        public static void EvolveUsingThreads()
        {
            int events = 16;

            ManualResetEvent[] doneEvents = new ManualResetEvent[events];

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart1, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart2, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart3, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart4, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart5, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();
        }
        /// <summary>
        /// Function generating one step of evolutionary algorithm.
        /// Algorithm does not involve the use of threading.
        /// </summary>
        public static void EvolveWithoutThreads()
        {
            int i = 0;
            foreach (var area in Areas)
            {
                area.KillWeaksWhoDoesNotServeTheEmperorWell();
                area.ReproduceMenToHaveMoreServantsOfTheEmperor();
                area.MutateWeaksSoTheyCanServeEmperorBetter();
                area.InfluenceMenWithSongsGlorifyingEmperor();
                area.MoveYourMenSergant();

                area.RegroupYourMenToOtherFront(0);
                area.RegroupYourMenToOtherFront(1);
                area.RegroupYourMenToOtherFront(2);
                area.RegroupYourMenToOtherFront(3);
                i++;
            }
        }
        public static List<int[,]> SimulationBoardState
        {
            get
            {
                List<int[,]> result = new List<int[,]>();

                foreach (var area in Areas)
                {
                    var pos = area.ChampionOfArea;
                    if (pos != null)
                    {
                        result.Add(Simulation.SimulationBoard[pos.Item1, pos.Item2].Notes);
                    }
                }
                return result;
            }
        }
        public static void ResetSimulation()
        {
            SimulationBoard = new Board(SimulationParameters.BoardWidth, SimulationParameters.BoardHeight, SimulationParameters.PopulationGrowth); 
        }
    }
}
