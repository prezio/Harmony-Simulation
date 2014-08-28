using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    /// <summary>
    /// Implementation of third phase member.
    /// </summary>
    public partial class Member3:Member
    {
        int _numberOfGroups;
        const int maxGroups=30;
        int[,] _groups; //pitch, chord change, repeats, rhythm, initial dynamics, dynamic difference, group number
        //int _initialChord;
        static int[] limits = { 9, 3, 11, 20, 127, 40, 10 };
        protected void Transpose(uint n, Random randContext)
        {
            if (_numberOfGroups <= 1)
            {
                return;
            }
            int temp;
            int place = randContext.Next(_numberOfGroups - 1);
            temp = _groups[place, n];
            _groups[place, n] = _groups[place + 1, n];
            _groups[place + 1, n] = temp;

        }
        protected void Exchange(uint n, Random randContext)
        {
            if (_numberOfGroups <= 1)
            {
                return;
            }
            int place = randContext.Next(_numberOfGroups - 1);
            _groups[place, n] = randContext.Next(limits[n]);
        }
        protected void Modify(uint n, Random randContext)
        {
            if (_numberOfGroups <= 1)
            {
                return;
            }
            int temp;

            int place = randContext.Next(_numberOfGroups - 1);
            temp = randContext.Next(-ModifyAmount[n], ModifyAmount[n] + 1);
            _groups[place, n] += temp;
            if (_groups[place, n] >= limits[n])
            {
                _groups[place, n] = limits[n] - 1;
            }
            else if (_groups[place, n] < 0)
            {
                _groups[place, n] = 0;
            }
        }
        protected void Shrink()
        {
            if (_numberOfGroups > 1)
                _numberOfGroups--;
        }
        protected void Grow(Random randContext)
        {
            if (_numberOfGroups >= maxGroups)
                return;

            _groups[_numberOfGroups, 0] = randContext.Next(limits[0]);
            _groups[_numberOfGroups, 1] = randContext.Next(limits[1]);
            _groups[_numberOfGroups, 2] = randContext.Next(limits[2]);
            _groups[_numberOfGroups, 3] = randContext.Next(limits[3]);
            _groups[_numberOfGroups, 4] = randContext.Next(limits[4]);
            _groups[_numberOfGroups, 5] = randContext.Next(limits[5]);
            _groups[_numberOfGroups, 6] = randContext.Next(limits[6]);

            _numberOfGroups++;
        }
        public override int NumberOfNotes
        {
            get
            {
                int sum = 0;
                for(int i=0; i<_numberOfGroups;i++)
                {
                    sum+=_groups[i,2];
                }
                return sum;
            }
        }

        public override int[,] Notes
        {
            get 
            {
                int dynamics;
                int [,] notes = new int[_maxNotes, 4];
                for(int i=0, group=0;i<_maxNotes&&group<_numberOfGroups;group++)
                {
                    dynamics=_groups[group,4];
                    if (_groups[group, 6] < PlayedGroup)
                        dynamics = 0;

                    for (int j = 0; j < _groups[group, 2] && i < _maxNotes;j++,i++ )
                    {
                        notes[i, 0] = _groups[group, 0];
                        notes[i, 1] = _groups[group, 1];
                        notes[i, 2] = _groups[group, 3];
                        notes[i, 3] = dynamics;
                        dynamics -= _groups[group, 5];
                        if (dynamics < 0)
                            dynamics = 0;
                    }
                }
                return notes;
            }
        }

        public override int Rank()
        {
            //pitch, chord change, repeats, rhythm, initial dynamics, dynamic difference, group number
            int[] count = new int[limits[6]];
            int rank = 0;
            rank -= (_numberOfGroups - PrefferedGroups) * (_numberOfGroups - PrefferedGroups)*30;
            int length = _groups[0, 2]*_groups[0, 3];
            int notes = _groups[0, 2];
            count[_groups[0, 6]]++;
            for (int i = 1; i < _numberOfGroups;i++)
            {
                if (Math.Abs((_groups[i, 0] - _groups[i - 1, 0]) % limits[0]) <= 1)
                    rank -= 20;
                if (_groups[i,2]==_groups[i-1,2])
                    rank -= 15;
                if (_groups[i, 3] == _groups[i - 1, 3])
                    rank -= 100;
                if (_groups[i, 5] * _groups[i, 2]>=_groups[i,4])
                    rank -= 240;
                if (_groups[i, 5] >= 15)
                    rank += 40;
                count[_groups[i, 6]]++;
                length += _groups[i, 2] * _groups[i, 3];
                notes += _groups[i, 2];
            }
            int mean = _numberOfGroups / limits[6];
            for (int i = 0; i < limits[6]; i++)
            {
                rank -= (count[i] - mean) * (count[i] - mean);
            }
            rank -= (length - PrefferedLength) * (length - PrefferedLength);
            rank -= (notes - PrefferedNotes) * (notes - PrefferedNotes) * 10;
            if (notes > _maxNotes)
                rank -= 1000;
            return rank;
        }

        public override void Mutate(Random randContext)
        {
            if (randContext.NextDouble() < GrowthChance)
            {
                Grow(randContext);
            }
            for (uint i = 0; i < 7;i++)
            {
                if (randContext.NextDouble() < ExchangeChance[i])
                {
                    Exchange(i, randContext);
                }
            }

            for (uint i = 0; i < 7; i++)
            {
                if (randContext.NextDouble() < TransposeChance[i])
                {
                    Transpose(i, randContext);
                }
            }
            for (uint i = 0; i < 7; i++)
            {
                if (randContext.NextDouble() < ModifyChance[i])
                {
                    Modify(i, randContext);
                }
            }

            if (randContext.NextDouble() < ShrinkChance)
            {
                Shrink();
            }
        }

       

        public override void Influence(Member influencer, Random randContext)
        {
            Member3 member = influencer as Member3;
            if (_numberOfGroups<member._numberOfGroups)
            {
                Grow(randContext);
            }
            else if (_numberOfGroups > member._numberOfGroups)
            {
                _numberOfGroups--;
            }
            for (int i = 0; i < _numberOfGroups; ++i)
            {
                for (uint j = 0; j < 6; j++)
                {
                    _groups[i, j] += (int)(InfluenceAmount[j] * (member._groups[i % member._numberOfGroups, j] - _groups[i, j]));
                }
            }
        }

        public override Member Clone()
        {
            Member3 result = new Member3();
            
            result._groups = new int [maxGroups,7];
            result._numberOfGroups = _numberOfGroups;
            Array.Copy(_groups, result._groups, _numberOfGroups * 7);

            return result;
        }
        public Member3(Random randContext):base(randContext)
        {
            _groups = new int[maxGroups, 7];
            _numberOfGroups = randContext.Next(maxGroups);
            for (int i = 0; i < _numberOfGroups; i++)
            {
                _groups[i, 0] = randContext.Next(limits[0]);
                _groups[i, 1] = randContext.Next(limits[1]);
                _groups[i, 2] = randContext.Next(limits[2]);
                _groups[i, 3] = randContext.Next(limits[3]);
                _groups[i, 4] = randContext.Next(limits[4]);
                _groups[i, 5] = randContext.Next(limits[5]);
                _groups[i, 6] = randContext.Next(limits[6]);
            }
        }
        public Member3()
            : base()
        {
        }
    }
}
