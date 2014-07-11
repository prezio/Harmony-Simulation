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
        public static void Test1()
        {
            // select same seed and number of tries for both tests
            int seed = 1405067994;
            int tries = 1;

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

        public static void Main(string[] args)
        {
            //Start test
            Test1();

            Console.ReadKey();
        }
    }
}
