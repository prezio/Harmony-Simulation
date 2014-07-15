using MusicPopulation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace populo
{
    public class Program
    {

        /// <summary>
        /// Max Tests
        /// </summary>
        private static void Test1()
        {
            DateTime start = DateTime.Now;

            SimulationParameters.PopulationGrowth = 256 * 256;
            SimulationParameters.PercentDeath = 1; // low percentage of deaths
            int tries = 100;

            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Step: {0}/{1}", i, tries);
                Simulation.EvolveUsingThreads();
            }

            DateTime end = DateTime.Now;
            Console.WriteLine("Duration: {0}", end - start);
        }

        /// <summary>
        /// Standard Test
        /// </summary>
        private static void Test2()
        {
            DateTime start = DateTime.Now;

            int tries = 100;

            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Step: {0}/{1}", i, tries);
                Simulation.EvolveUsingThreads();
            }

            DateTime end = DateTime.Now;

            List<int[,]> result = Simulation.SimulationBoardState;
            StringBuilder text = new StringBuilder();
            string fileName = "symulacja";

            

            Console.WriteLine("Duration: {0}", end - start);
        }

        public static void Main(string[] args)
        {
            //Start test
            Test2();

            Console.ReadKey();
        }
    }
}
