using MusicPopulation;
//using MusicPopulation;
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
        public class Test
        {
            private Random _random;

            public Test()
            {
                _random = new Random();
            }

            public void foo()
            {
                Console.WriteLine(_random.Next());
            }
        }
        /// <summary>
        /// Max Tests
        /// </summary>
        private static void Test1()
        {
            DateTime start = DateTime.Now;

            SimulationParameters.PopulationGrowth = 256 * 256;
            SimulationParameters.PercentDeath = 1; // low percentage of deaths
            int tries = 5000;

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

            int tries = 25000;

            for (int i = 0; i < tries; i++)
            {
                Console.WriteLine("Step: {0}/{1}", i, tries);
                Simulation.EvolveUsingThreads();
            }

            DateTime end = DateTime.Now;

            List<Tuple<int, int[,]>> result = Simulation.SimulationBoardState;
            StringBuilder text = null;

            int count = 1;
            foreach (Tuple<int, int[,]> sound in result)
            {
                text = new StringBuilder();
                int number = sound.Item1;
                int[,] array = sound.Item2;

                for (int i = 0; i < number; i++)
                {
                    text.Append(array[i, 0].ToString() + "\n");
                }

                File.WriteAllText("symulacja_" + count.ToString() + "_0.txt", text.ToString());

                text = new StringBuilder();

                for (int i = 0; i < number; i++)
                {
                    text.Append(array[i, 1].ToString() + "\n");
                }

                File.WriteAllText("symulacja_" + count.ToString() + "_1.txt", text.ToString());

                text = new StringBuilder();

                for (int i = 0; i < number; i++)
                {
                    text.Append(array[i, 2].ToString() + "\n");
                }

                File.WriteAllText("symulacja_" + count.ToString() + "_2.txt", text.ToString());

                count++;
            }

            Console.WriteLine("Duration: {0}", end - start);
        }

        public static void Main(string[] args)
        {
            //Start test
            //Test2();
            /*Test test = new Test();
            for (int i = 0; i < 100; i++)
            {
                test.foo();
            }*/
            Test2();
            Console.ReadKey();
        }
    }
}
