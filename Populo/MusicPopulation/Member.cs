using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public class Member
    {
        public const int maxNotes = 255;
        public int[,] notes = new int[maxNotes,3]; //pitch, duration, dynamics
        public int numberOfNotes;
        static readonly int[] limits = new int[] { 16, 60, 128 };


        protected void Transpose(uint n)
        {
            int temp;
            int place = RandomGenerator.RandomGen.Next(numberOfNotes - 1);
            temp = notes[place, n];
            notes[place, n] = notes[place + 1, n];
            notes[place + 1, n] = temp;
            //place = random.Next(numberOfNotes - 1);
            //temp = notes[place, 1];
            //notes[place, 1] = notes[place + 1, 1];
            //notes[place + 1, 1] = temp;
            //place = random.Next(numberOfNotes - 1);
            //temp = notes[place, 2];
            //notes[place, 2] = notes[place + 1, 2];
            //notes[place + 1, 2] = temp;
        }
        protected void Exchange(uint n)
        {
            int place = RandomGenerator.RandomGen.Next(numberOfNotes);
            notes[place, n] = RandomGenerator.RandomGen.Next(limits[n]);
        }
        public double Rank()
        {
            // Random rank
            return RandomGenerator.RandomGen.NextDouble();
        }
        public void Mutate()
        {
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.growthChance)
            {
                Grow();
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.exchangeChance[0])
            {
                Exchange(0);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.exchangeChance[1])
            {
                Exchange(1);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.exchangeChance[2])
            {
                Exchange(2);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.transposeChance[0])
            {
                Transpose(0);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.transposeChance[1])
            {
                Transpose(1);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.transposeChance[2])
            {
                Transpose(2);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.modifyChance[0])
            {
                Modify(0);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.modifyChance[1])
            {
                Modify(1);
            }
            while (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.modifyChance[2])
            {
                Modify(2);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.shrinkChance)
            {
                Shrink();
            }
        }
        protected void Modify(uint n)
        {
            int temp;
            int place = RandomGenerator.RandomGen.Next(numberOfNotes - 1);
            temp = RandomGenerator.RandomGen.Next(-SimulationParameters.modifyAmount[n], SimulationParameters.modifyAmount[n] + 1);
            notes[place, n] += temp;
            if (notes[place, n] >= limits[n])
            {
                notes[place, n] = limits[n] - 1;
            }
            else if (notes[place,n]<0)
            {
                notes[place, n] = 0;
            }
        }
        protected void Shrink()
        {
            if(numberOfNotes>1)
                --numberOfNotes;
        }
        protected void Grow()
        {
            if (numberOfNotes == maxNotes)
                return;
            notes[numberOfNotes, 0] = RandomGenerator.RandomGen.Next(limits[0]);
            notes[numberOfNotes, 1] = RandomGenerator.RandomGen.Next(limits[1]);
            notes[numberOfNotes, 2] = RandomGenerator.RandomGen.Next(limits[2]);
            ++numberOfNotes;
        }
        public void Influence(Member influencer)
        {
            if(numberOfNotes<influencer.numberOfNotes)
            {
                Grow();
            }
            else if(numberOfNotes>influencer.numberOfNotes)
            {
                --numberOfNotes;
            }
            for(int i=0;i<numberOfNotes;++i)
            {
                notes[i, 0] +=(int)(SimulationParameters.influenceAmount[0] * (influencer.notes[i % influencer.numberOfNotes, 0] - notes[i, 0]));
                notes[i, 1] += (int)(SimulationParameters.influenceAmount[1] * (influencer.notes[i % influencer.numberOfNotes, 1] - notes[i, 1]));
                notes[i, 2] += (int)(SimulationParameters.influenceAmount[2] * (influencer.notes[i % influencer.numberOfNotes, 2] - notes[i, 2]));
            }
        }
        public void Kill()
        {
            
        }
        public Member(Member original)
        {
            numberOfNotes = original.numberOfNotes;
            Array.Copy(original.notes, notes, maxNotes);
        }
        public Member()
        {
            numberOfNotes = RandomGenerator.RandomGen.Next(maxNotes - 1) + 1;
            for(int i=0; i< numberOfNotes; i++)
            {
                notes[i, 0] = RandomGenerator.RandomGen.Next(limits[0]);
                notes[i, 1] = RandomGenerator.RandomGen.Next(limits[1]);
                notes[i, 2] = RandomGenerator.RandomGen.Next(limits[2]);
            }
        }
    }
}
