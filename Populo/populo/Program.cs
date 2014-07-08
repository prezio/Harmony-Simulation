using MusicPopulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace populo
{
    public class Program
    {

        public static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("{0} % completed.", i);
                Simulation.Evolve();
            }
            DateTime end = DateTime.Now;

            TimeSpan duration = end - start;
            Console.WriteLine(duration);

            Console.ReadKey();
        }
    }
}
