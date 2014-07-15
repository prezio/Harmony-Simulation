using MusicPopulation;
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
        /// Test generates two files test1.txt and test2.txt.
        /// First is generated using threads, second not.
        /// The output of two files will not be the same because of diffrent order of dealing with areas.
        /// </summary>
        private static void Test1()
        {
            // select same seed and number of tries for both tests
            int seed = 1405067994;
            int tries = 3;

            // First test
            RandomGenerator.SetSeed(seed);
            Simulation.ResetSimulation();

            Console.WriteLine("Test 1 started.");
            StringBuilder output1 = new StringBuilder();
            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Test 1: {0}/{1} computation ...", i, tries);
                Simulation.EvolveUsingThreads();
                Console.WriteLine("Test 1: {0}/{1} serialization ...", i, tries);
                output1.Append(Simulation.SimulationBoard.Serialize());
                Console.WriteLine("Test 1: {0}/{1} completed.", i, tries);
            }

            File.WriteAllText("test1.txt", output1.ToString());
            
            // Second test
            RandomGenerator.SetSeed(seed);
            Simulation.ResetSimulation();

            Console.WriteLine("Test 2 started.");
            StringBuilder output2 = new StringBuilder();
            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Test 2: {0}/{1} computation ...", i, tries);
                Simulation.EvolveWithoutThreads();
                Console.WriteLine("Test 2: {0}/{1} serialization ...", i, tries);
                output2.Append(Simulation.SimulationBoard.Serialize());
                Console.WriteLine("Test 2: {0}/{1} completed.", i, tries);
            }

            File.WriteAllText("test2.txt", output2.ToString());
        }

        /// <summary>
        /// Max Tests
        /// </summary>
        private static void Test2()
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

        public static void Main(string[] args)
        {
            //Start test
            //Test1();
            //Test2();

            Console.ReadKey();
        }
    }
}
