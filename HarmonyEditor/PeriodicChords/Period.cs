using System;
using System.Collections.Generic;
using System.Linq;

namespace PeriodicChords
{
    public class Period
    {
        public double PeriodA { get; set; }
        public uint Repeats { get; set; }
        public double[] Divides { get; set; }

        public Period(double period, uint repeats, double[] divides)
        {
            if (repeats < 1) throw new ArgumentOutOfRangeException();
            this.Repeats = repeats;
            this.PeriodA = period;
            this.Divides = divides;
        }
        public Period(double period, uint repeats, double[] divides, uint numberOfDivides)
        {
            if (repeats < 1) throw new ArgumentOutOfRangeException();
            this.Repeats = repeats;
            this.PeriodA = period;
            if (numberOfDivides == 0)
            {
                this.Divides = null;
            }
            else
            {
                this.Divides = new double[numberOfDivides];
                Array.Copy(divides, this.Divides, numberOfDivides);
            }
        }
        public double[] derivatives(double baseValue)
        {
            uint n;
            double[] steps;
            if(Repeats == 0)
                return null;
            if (Divides != null)
            {
                n = (uint)Divides.Length + 1;
                steps = new double[n];
                Array.Copy(Divides, steps, n - 1);
                steps[n - 1] = PeriodA - (double) Divides.Sum(x=>(int)x );
            }
            else
            {
                steps = new double[1];
                n = 1;
                steps[0] = PeriodA;
            }

            uint notes = n * Repeats;
            double[] result = new double[notes];
            result[0] = baseValue+steps[0];
            for (int i = 1; i < notes; i++)
            {
                result[i] = result[i - 1] + steps[i % n];
            }
            return result;
        }
    }
}
