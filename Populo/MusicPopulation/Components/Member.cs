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
        static readonly int[] limits = new int[] { 72, 60, 128 };

        protected void Transpose(uint n, Random randContext)
        {
            if (_numberOfNotes <= 1)
            {
                return;
            }
            int temp;
            int place = randContext.Next(_numberOfNotes - 1);
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
        protected void Exchange(uint n, Random randContext)
        {
            if (_numberOfNotes <= 1)
            {
                return;
            }
            int place = randContext.Next(_numberOfNotes);
            _notes[place, n] = randContext.Next(limits[n]);
        }
        protected void Modify(uint n, Random randContext)
        {
            if (_numberOfNotes <= 1)
            {
                return;
            }
            int temp;
            
            int place = randContext.Next(_numberOfNotes - 1);
            temp = randContext.Next(-SimulationParameters.ModifyAmount[n], SimulationParameters.ModifyAmount[n] + 1);
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
        protected void Grow(Random randContext)
        {
            if (_numberOfNotes >= _maxNotes)
                return;

            int tmp1 = randContext.Next(limits[0]);
            int tmp2 = randContext.Next(limits[1]);
            int tmp3 = randContext.Next(limits[2]);

            _notes[_numberOfNotes, 0] = tmp1;
            _notes[_numberOfNotes, 1] = tmp2;
            _notes[_numberOfNotes, 2] = tmp3;

            _numberOfNotes++;
        }

        public int NumberOfNotes
        {
            get
            {
                return _numberOfNotes;
            }
        }
        public int[,] Notes
        {
            get
            {
                return _notes;
            }
        }
        public void Influence(Member influencer, Random randContext)
        {
            if(_numberOfNotes<influencer._numberOfNotes)
            {
                Grow(randContext);
            }
            else if(_numberOfNotes>influencer._numberOfNotes)
            {
                --_numberOfNotes;
            }
            for(int i=0;i<_numberOfNotes;++i)
            {
                _notes[i, 0] +=(int)(SimulationParameters.InfluenceAmount[0] * (influencer._notes[i % influencer._numberOfNotes, 0] - _notes[i, 0]));
                _notes[i, 1] += (int)(SimulationParameters.InfluenceAmount[1] * (influencer._notes[i % influencer._numberOfNotes, 1] - _notes[i, 1]));
                _notes[i, 2] += (int)(SimulationParameters.InfluenceAmount[2] * (influencer._notes[i % influencer._numberOfNotes, 2] - _notes[i, 2]));
            }
        }
        public Member(Member original)
        {
            _numberOfNotes = original._numberOfNotes;
            Array.Copy(original._notes, _notes, _maxNotes);
        }
        public Member(Random randContext)
        {
            _numberOfNotes = randContext.Next(_maxNotes - 1) + 1;
            _notes = new int[_maxNotes + 1, 3];

            for(int i=0; i < _numberOfNotes; i++)
            {
                _notes[i, 0] = randContext.Next(limits[0]);
                _notes[i, 1] = randContext.Next(limits[1]);
                _notes[i, 2] = randContext.Next(limits[2]);
            }
        }
        public int Rank()
        {
            int rank = 0;
            for (int i = 0; i < _numberOfNotes; i++)
            {
                rank -= (Notes[i, 1] - i) * (Notes[i, 1] - i);
            }
            //int previousChord = _notes[0, 0] / 24;
            //int currentChord = 0;
            //for (int i = 1; i < _numberOfNotes; i++ )
            //{
            //    currentChord = _notes[i, 0] / 24;
            //    if((currentChord-previousChord+3)%3==2)
            //    {
            //        rank-=10;
            //    }
            //    else
            //    {
            //        rank += 10;
            //    }
            //    if(_notes[i,1]>_notes[i-1,1])
            //    {
            //        rank += 15;
            //    }
            //    if (_notes[i, 2] < _notes[i - 1, 2])
            //    {
            //        rank += 7;
            //    }
            //}
                return rank;
        }
        public void Mutate(Random randContext)
        {
            if (randContext.NextDouble() < SimulationParameters.GrowthChance)
            {
                Grow(randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ExchangeChance[0])
            {
                Exchange(0, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ExchangeChance[1])
            {
                Exchange(1, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ExchangeChance[2])
            {
                Exchange(2, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.TransposeChance[0])
            {
                Transpose(0, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.TransposeChance[1])
            {
                Transpose(1, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.TransposeChance[2])
            {
                Transpose(2, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ModifyChance[0])
            {
                Modify(0, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ModifyChance[1])
            {
                Modify(1, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ModifyChance[2])
            {
                Modify(2, randContext);
            }
            if (randContext.NextDouble() < SimulationParameters.ShrinkChance)
            {
                Shrink();
            }
        }
    }
}
