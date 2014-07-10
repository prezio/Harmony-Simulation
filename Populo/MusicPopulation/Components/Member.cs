using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public class Member
    {
        private const int _maxNotes = 255;
        private int[,] _notes = null; //pitch, duration, dynamics
        private int _numberOfNotes;
        static readonly int[] limits = new int[] { 16, 60, 128 };

        protected void Transpose(uint n)
        {
            int temp;
            int place = RandomGenerator.RandomGen.Next(_numberOfNotes - 1);
            temp = _notes[place, n];
            _notes[place, n] = _notes[place + 1, n];
            _notes[place + 1, n] = temp;
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
            int place = RandomGenerator.RandomGen.Next(_numberOfNotes);
            _notes[place, n] = RandomGenerator.RandomGen.Next(limits[n]);
        }
        protected void Modify(uint n)
        {
            int temp;
            int place = RandomGenerator.RandomGen.Next(_numberOfNotes - 1);
            temp = RandomGenerator.RandomGen.Next(-SimulationParameters.modifyAmount[n], SimulationParameters.modifyAmount[n] + 1);
            _notes[place, n] += temp;
            if (_notes[place, n] >= limits[n])
            {
                _notes[place, n] = limits[n] - 1;
            }
            else if (_notes[place,n]<0)
            {
                _notes[place, n] = 0;
            }
        }
        protected void Shrink()
        {
            if(_numberOfNotes>1)
                --_numberOfNotes;
        }
        protected void Grow()
        {
            if (_numberOfNotes == _maxNotes)
                return;
            _notes[_numberOfNotes, 0] = RandomGenerator.RandomGen.Next(limits[0]);
            _notes[_numberOfNotes, 1] = RandomGenerator.RandomGen.Next(limits[1]);
            _notes[_numberOfNotes, 2] = RandomGenerator.RandomGen.Next(limits[2]);
            ++_numberOfNotes;
        }

        public void Influence(Member influencer)
        {
            if(_numberOfNotes<influencer._numberOfNotes)
            {
                Grow();
            }
            else if(_numberOfNotes>influencer._numberOfNotes)
            {
                --_numberOfNotes;
            }
            for(int i=0;i<_numberOfNotes;++i)
            {
                _notes[i, 0] +=(int)(SimulationParameters.influenceAmount[0] * (influencer._notes[i % influencer._numberOfNotes, 0] - _notes[i, 0]));
                _notes[i, 1] += (int)(SimulationParameters.influenceAmount[1] * (influencer._notes[i % influencer._numberOfNotes, 1] - _notes[i, 1]));
                _notes[i, 2] += (int)(SimulationParameters.influenceAmount[2] * (influencer._notes[i % influencer._numberOfNotes, 2] - _notes[i, 2]));
            }
        }
        public Member(Member original)
        {
            _numberOfNotes = original._numberOfNotes;
            Array.Copy(original._notes, _notes, _maxNotes);
        }
        public Member()
        {
            _numberOfNotes = RandomGenerator.RandomGen.Next(_maxNotes - 1) + 1;
            _notes = new int[_maxNotes, 3];

            for(int i=0; i< _numberOfNotes; i++)
            {
                _notes[i, 0] = RandomGenerator.RandomGen.Next(limits[0]);
                _notes[i, 1] = RandomGenerator.RandomGen.Next(limits[1]);
                _notes[i, 2] = RandomGenerator.RandomGen.Next(limits[2]);
            }
        }
        public int Rank()
        {
            int rank = 0;
            foreach (int a in _notes)
            {
                rank += a;
            }
            return rank;
        }
        public void Mutate()
        {
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.growthChance)
            {
                Grow();
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.exchangeChance[0])
            {
                Exchange(0);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.exchangeChance[1])
            {
                Exchange(1);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.exchangeChance[2])
            {
                Exchange(2);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.transposeChance[0])
            {
                Transpose(0);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.transposeChance[1])
            {
                Transpose(1);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.transposeChance[2])
            {
                Transpose(2);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.modifyChance[0])
            {
                Modify(0);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.modifyChance[1])
            {
                Modify(1);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.modifyChance[2])
            {
                Modify(2);
            }
            if (RandomGenerator.RandomGen.NextDouble() < SimulationParameters.shrinkChance)
            {
                Shrink();
            }
        }
    }
}
