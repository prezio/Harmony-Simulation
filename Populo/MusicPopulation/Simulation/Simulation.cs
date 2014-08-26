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
        private static Task _taskSimulation = null;
        private static CancellationTokenSource _tokenCancelSimulation;
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

        public static TaskStatus SimulationStatus
        {
            get
            {
                if (_taskSimulation == null)
                    return TaskStatus.Canceled;
                return _taskSimulation.Status;
            }
        }   
        /// <summary>
        /// Simulation Board with Randomly generated individuals.
        /// </summary>
        public static Board SimulationBoard = new Board();
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

            Task[] tasks = new Task[events];
            AreaThread[] threads = new AreaThread[events];
            for (int i = 0; i < events; i++)
            {
                threads[i] = new AreaThread(i);
            }

            // In first step each thread:
            //  - kills weak indiwviduals,
            //  - chooses best individual,
            //  - reproduces best individuals
            //  - mutates individuals
            //  - influences individuals
            //  - generate random moves for each individual, if individual wants to move outside area
            //    I push movement to other step of evolution(this allows me to avoid memory hazard but we have to
            //    assume that individual won't move so far)
            for(int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart1);
                tasks[i].Start();
            }
            for (int i = 0; i < events; i++)
            {
                tasks[i].Wait();
            }

            // In second step each thread deals with individuals which want to leave area
            // and move to up area
            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart2);
                tasks[i].Start();
            }
            for (int i = 0; i < events; i++)
            {
                tasks[i].Wait();
            }

            // In third step each thread deals with individuals which want to leave area
            // and move to down area
            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart3);
                tasks[i].Start();
            }
            for (int i = 0; i < events; i++)
            {
                tasks[i].Wait();
            }

            // In fourth step each thread deals with individuals which want to leave area
            // and move to left area
            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart4);
                tasks[i].Start();
            }
            for (int i = 0; i < events; i++)
            {
                tasks[i].Wait();
            }

            // In fifth step each thread deals with individuals which want to leave area
            // and move to right area
            for (int i = 0; i < events; i++)
            {
                tasks[i] = new Task(threads[i].EvolvePart5);
                tasks[i].Start();
            }
            for (int i = 0; i < events; i++)
            {
                tasks[i].Wait();
            }
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
                area.SelectChampionWhoCanBecomeCommissar();
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
        public static List<Tuple<int, int[,]>> SimulationBoardState
        {
            get
            {
                return Areas.Select(area => area.ChampionParameters).ToList();
            }
        }
        public static void ResetSimulation()
        {
            SimulationBoard = new Board();
        }
        public delegate bool MelodyNeededDelegate();
        public delegate void RefreshParametersDelegate();

        public static void DoSimulation(int evolveDuration,MelodyNeededDelegate unneeded, RefreshParametersDelegate parameters)
        {
            _tokenCancelSimulation = new CancellationTokenSource();
            _taskSimulation = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        for (int i = 0; i < evolveDuration && unneeded(); i++)
                        {
                            parameters();
                            EvolveUsingThreads();
                            //Console.WriteLine("{0} / {1}", i, evolveDuration);
                            _tokenCancelSimulation.Token.ThrowIfCancellationRequested();
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Operation canceled by user
                    }
                }, _tokenCancelSimulation.Token);
        }
        public static void Wait()
        {
            _taskSimulation.Wait();
        }
        public static void StopSimulation()
        {
            _tokenCancelSimulation.Cancel();
            _taskSimulation.Wait();
        }
    }
}
