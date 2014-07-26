using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            int events = 16; // number of threads; each one commands its own area

            // I divided algorithm into 5 steps in order to avoid locking memory. In each step I run parallely 16 threads.

            // In first step each thread:
            //  - kills weak indiwviduals,
            //  - chooses best individual,
            //  - reproduces best individuals
            //  - mutates individuals
            //  - influences individuals
            //  - generate random moves for each individual, if individual wants to move outside area
            //    I push movement to other step of evolution(this allows me to avoid memory hazard but we have to
            //    assume that individual won't move so far)

            Task[] tasks = new Task[events];
            AreaThread[] threads = new AreaThread[events];
            for(int i = 0; i < events; i++)
            {
                threads[i] = new AreaThread(i);
            }

            for(int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart1);
                tasks[i].Start();
            }
            try
            {
                for (int i = 0; i < events; i++)
                {
                    tasks[i].Wait();
                }
            }
            catch(AggregateException ex)
            {
                Logger.AddError(ex, null);
            }

            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart2);
                tasks[i].Start();
            }
            try
            {
                for (int i = 0; i < events; i++)
                {
                    tasks[i].Wait();
                }
            }
            catch (AggregateException ex)
            {
                Logger.AddError(ex, null);
            }

            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart3);
                tasks[i].Start();
            }
            try
            {
                for (int i = 0; i < events; i++)
                {
                    tasks[i].Wait();
                }
            }
            catch (AggregateException ex)
            {
                Logger.AddError(ex, null);
            }

            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart4);
                tasks[i].Start();
            }
            try
            {
                for (int i = 0; i < events; i++)
                {
                    tasks[i].Wait();
                }
            }
            catch (AggregateException ex)
            {
                Logger.AddError(ex, null);
            }

            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart5);
                tasks[i].Start();
            }
            try
            {
                for (int i = 0; i < events; i++)
                {
                    tasks[i].Wait();
                }
            }
            catch (AggregateException ex)
            {
                Logger.AddError(ex, null);
            }
            /*ManualResetEvent[] doneEvents = new ManualResetEvent[events];

            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart1, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            // In second step each thread deals with individuals which want to leave area
            // and move to up area
            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart2, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            // In third step each thread deals with individuals which want to leave area
            // and move to down area
            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart3, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            // In fourth step each thread deals with individuals which want to leave area
            // and move to left area
            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart4, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();

            // In fifth step each thread deals with individuals which want to leave area
            // and move to right area
            for (int i = 0; i < events; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                AreaThread thread = new AreaThread(i, doneEvents[i]);
                ThreadPool.QueueUserWorkItem(thread.EvolvePart5, null);
            }

            foreach (var e in doneEvents)
                e.WaitOne();*/
        }
        /// <summary>
        /// Function generating one step of evolutionary algorithm.
        /// Algorithm does not involve the use of threading.
        /// </summary>
        public static void EvolveWithoutThreads()
        {
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
            }
        }
        public static List<Member> SimulationBoardState
        {
            get
            {
                return Areas.Select(area => area.ChampionOfArea).ToList();
            }
        }
        public static void ResetSimulation()
        {
            SimulationBoard = new Board(SimulationParameters.BoardWidth, SimulationParameters.BoardHeight, SimulationParameters.PopulationGrowth); 
        }
    }
}
