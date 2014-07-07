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

        static Random random= new Random();

        protected void transpose(uint n)
        {
            int temp;
            int place = random.Next(numberOfNotes - 1);
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
        protected void exchange(uint n)
        {
            int place = random.Next(numberOfNotes);
            notes[place, n] = random.Next(limits[n]);
        }
        public void mutate()
        {
            if(random.NextDouble()<SimulationParameters.growthChance)
            {
                grow();
            }
            while(random.NextDouble()<SimulationParameters.exchangeChance[0])
            {
                exchange(0);
            }
            while (random.NextDouble() < SimulationParameters.exchangeChance[1])
            {
                exchange(1);
            }
            while (random.NextDouble() < SimulationParameters.exchangeChance[2])
            {
                exchange(2);
            }
            while (random.NextDouble() < SimulationParameters.transposeChance[0])
            {
                transpose(0);
            }
            while (random.NextDouble() < SimulationParameters.transposeChance[1])
            {
                transpose(1);
            }
            while (random.NextDouble() < SimulationParameters.transposeChance[2])
            {
                transpose(2);
            }
            while (random.NextDouble() < SimulationParameters.modifyChance[0])
            {
                modify(0);
            }
            while (random.NextDouble() < SimulationParameters.modifyChance[1])
            {
                modify(1);
            }
            while (random.NextDouble() < SimulationParameters.modifyChance[2])
            {
                modify(2);
            }
            if (random.NextDouble() < SimulationParameters.shrinkChance)
            {
                shrink();
            }
        }
        protected void modify(uint n)
        {
            int temp;
            int place = random.Next(numberOfNotes - 1);
            temp = random.Next(-SimulationParameters.modifyAmount[n],SimulationParameters.modifyAmount[n]+1);
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
        protected void shrink()
        {
            if(numberOfNotes>1)
                --numberOfNotes;
        }
        protected void grow()
        {
            if (numberOfNotes == maxNotes)
                return;
            notes[numberOfNotes, 0] = random.Next(limits[0]);
            notes[numberOfNotes, 1] = random.Next(limits[1]);
            notes[numberOfNotes, 2] = random.Next(limits[2]);
            ++numberOfNotes;
        }
        public void influence(Member influencer)
        {
            if(numberOfNotes<influencer.numberOfNotes)
            {
                grow();
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
        public Member(Member original)
        {
            numberOfNotes = original.numberOfNotes;
            Array.Copy(original.notes, notes, maxNotes);
        }
        public Member()
        {
            numberOfNotes = random.Next(maxNotes - 1) + 1;
            for(int i=0; i< numberOfNotes; i++)
            {
                notes[i, 0] = random.Next(limits[0]);
                notes[i, 1] = random.Next(limits[1]);
                notes[i, 2] = random.Next(limits[2]);
            }
        }
    }
}
