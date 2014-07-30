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
        private static int[] tries = { 1000 };      
        private static void WriteToFile(int count, int tries)
        {
            StringBuilder text = new StringBuilder();

            text.Append(Logger.GenBoardDescription(tries));
            text.Append(Logger.GenAreaDescription());
            
            text.Append("\nEND\n");

            string fileName = "symulacja" + count.ToString() + ".txt";
            File.WriteAllText(fileName, text.ToString());
        }
        private static void automationTest()
        {
            int count = 0;

            for (SimulationParameters.PercentDeath = 0; SimulationParameters.PercentDeath <= 10; SimulationParameters.PercentDeath ++)
            {
                for (SimulationParameters.ModifyAmount[0] = 0; SimulationParameters.ModifyAmount[0] <= 20; SimulationParameters.ModifyAmount[0] ++)
                {
                    for (SimulationParameters.ModifyAmount[1] = 0; SimulationParameters.ModifyAmount[1] <= 20; SimulationParameters.ModifyAmount[1]++)
                    {
                        for (SimulationParameters.ModifyAmount[2] = 0; SimulationParameters.ModifyAmount[2] <= 20; SimulationParameters.ModifyAmount[2]++)
                        {
                            for (SimulationParameters.InfluenceAmount[0] = 0; SimulationParameters.InfluenceAmount[0] <= 1; SimulationParameters.InfluenceAmount[0]+=0.01)
                            {
                                for (SimulationParameters.InfluenceAmount[1] = 0; SimulationParameters.InfluenceAmount[1] <= 1; SimulationParameters.InfluenceAmount[1] += 0.01)
                                {
                                    for (SimulationParameters.InfluenceAmount[2] = 0; SimulationParameters.InfluenceAmount[2] <= 1; SimulationParameters.InfluenceAmount[2] += 0.01)
                                    {
                                        for (SimulationParameters.GrowthChance = 0; SimulationParameters.GrowthChance <= 0.1; SimulationParameters.GrowthChance += 0.05)
                                        {
                                            for (SimulationParameters.ShrinkChance = 0; SimulationParameters.ShrinkChance <= 0.1; SimulationParameters.ShrinkChance += 0.05)
                                            {
                                                for(int i=0; i<tries.Length; i++)
                                                {
                                                    Simulation.ResetSimulation();
                                                    for(int j=0; j<tries[i]; j++)
                                                    {
                                                        Simulation.EvolveUsingThreads();
                                                    }

                                                    WriteToFile(count, tries[i]);
                                                    
                                                    Console.WriteLine("{0} completed.", count);
                                                    count++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        private static void Test()
        {
            SimulationParameters.PercentDeath = 0;
            SimulationParameters.ModifyAmount[0] = 0;
            SimulationParameters.ModifyAmount[1] = 0;
            SimulationParameters.ModifyAmount[2] = 0;
            SimulationParameters.InfluenceAmount[0] = 0;
            SimulationParameters.InfluenceAmount[1] = 0;
            SimulationParameters.InfluenceAmount[2] = 0;
            SimulationParameters.GrowthChance = 0.05;
            SimulationParameters.ShrinkChance = 0.05;
            int tries = 1000;
            
            for(int i=0; i<tries; i++)
            {
                Simulation.EvolveUsingThreads();
                Console.WriteLine(i);
            }
        }
        private static void NormalTest()
        {
            int tries = 10000;

            for (int i = 0; i < tries; i++)
            {
                Simulation.EvolveUsingThreads();
                Console.WriteLine(i);
            }

            WriteToFile(0, tries);
        }

        public static void Main(string[] args)
        {
            //automationTest();
            //Test();
            NormalTest();

            //Tuple<int, int> a1 = Simulation.SimulationBoard.GetBestInArea(0, 0, 15, 15);
            //Tuple<int, int> a2 = Simulation.SimulationBoard.GetBestInArea(16, 0, 31, 15);
            //Console.WriteLine("Done. {0} {1} || {2} {3}", a1.Item1, a1.Item2, a2.Item1, a2.Item2);
            Console.ReadKey();
        }
    }
}
