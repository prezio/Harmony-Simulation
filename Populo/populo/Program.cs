using MusicPopulation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace populo
{
    public class Program
    {

        public static void Main(string[] args)
        {
            int tries = 200;

            DateTime start = DateTime.Now;
            for (int i = 0; i <= tries; i++)
            {
                Debug.WriteLine("{0} / {1} completed.", i, tries);
                Console.WriteLine("{0} / {1} completed.", i, tries);

                //Simulation.EvolveUsingThreads();
                Simulation.Evolve();

                Debug.WriteLine("==================\n");
            }

            DateTime end = DateTime.Now;
            TimeSpan duration = end - start;
            Debug.WriteLine(duration);

            Console.ReadKey();
        }
    }
}
