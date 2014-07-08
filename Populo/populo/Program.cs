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
        public static void Mutate(Board board)
        {
            foreach (Member m in board)
            {
                if (m != null)
                    m.Mutate();
            }
        }

        public static void Main(string[] args)
        {
            // Test Random Board Generator
            //var temp = RandomGenerator.RandomPermBoard;
            DateTime start = DateTime.Now;
            Board board = new Board();
            for (int i = 0; i < 100; i++)
            {
                Mutate(board);
            }
            DateTime end = DateTime.Now;

            TimeSpan duration = end - start;
            Console.WriteLine(duration);

            //Mutate(board);
            Console.ReadKey();
        }
    }
}
