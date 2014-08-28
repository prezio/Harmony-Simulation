using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    /// <summary>
    /// Implementation of First phase member.
    /// </summary>
    public partial class Member1 : Member
    {
        private int _numberOfNotes;
        private int[,] _notes;

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
            int place = randContext.Next(_numberOfNotes - 1);
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
            temp = randContext.Next(-ModifyAmount[n], ModifyAmount[n] + 1);
            _notes[place, n] += temp;
            if (_notes[place, n] >= limits[n])
            {
                _notes[place, n] = limits[n] - 1;
            }
            else if (_notes[place, n] < 0)
            {
                _notes[place, n] = 0;
            }
        }
        protected void Shrink()
        {
            if (_numberOfNotes > 1)
                _numberOfNotes --;
        }
        protected void Grow(Random randContext)
        {
            if (_numberOfNotes >= _maxNotes)
                return;

            _notes[_numberOfNotes, 0] = randContext.Next(limits[0]);
            _notes[_numberOfNotes, 1] = 0;
            _notes[_numberOfNotes, 2] = randContext.Next(limits[2]);
            _notes[_numberOfNotes, 3] = randContext.Next(limits[3]);

            _numberOfNotes++;
        }

        protected static readonly int[] limits = new int[] { 20,0, 24, 50 };

        public Member1(Random randContext)
            : base(randContext)
        {
            _numberOfNotes = randContext.Next(_maxNotes - 1) + 1;
            _notes = new int[_maxNotes, 4];

            for (int i = 0; i < NumberOfNotes; i++)
            {
                _notes[i, 0] = randContext.Next(limits[0]);
                _notes[i, 1] = 0;
                _notes[i, 2] = randContext.Next(limits[2]);
                _notes[i, 3] = randContext.Next(limits[3]);
            }
        }
        public Member1()
            : base()
        {
        }

        #region Overriden Methods
        public override int NumberOfNotes
        {
            get
            {
                return _numberOfNotes;
            }
        }
        public override int[,] Notes  // pitch, duration, dynamics
        {
            get
            {
                return _notes;
            }
        }
        public override void Influence(Member influencer , Random randContext)
        {
            Member1 member = influencer as Member1;
            if (_numberOfNotes < member._numberOfNotes)
            {
                Grow(randContext);
            }
            else if (_numberOfNotes > member._numberOfNotes)
            {
                _numberOfNotes --;
            }
            for (int i = 0; i < NumberOfNotes; ++i)
            {
                _notes[i, 0] += (int)(InfluenceAmount[0] * (member._notes[i % member._numberOfNotes, 0] - _notes[i, 0]));
                _notes[i, 2] += (int)(InfluenceAmount[2] * (member._notes[i % member._numberOfNotes, 2] - _notes[i, 2]));
                _notes[i, 3] += (int)(InfluenceAmount[3] * (member._notes[i % member._numberOfNotes, 3] - _notes[i, 3]));
            }
        }
        public override int Rank()
        {
            int[] count = new int[limits[0]];
            int rank = 0;
            double proportion = ((double)_notes[0, 2]) / _notes[1, 2];
            double prevProportion = proportion;
            int difference = _notes[1, 3] - _notes[0, 3];
            int prevDifference = difference;
            int sameDirectionRhythm = 0;
            int sameDirectionDynamics = 0;
            count[_notes[0, 0]]++;
            count[_notes[1, 0]]++;
            if (difference > 40)
                rank += 40;
            for (int i = 2; i < NumberOfNotes; i++)
            {
                count[_notes[i, 0]]++;
                prevProportion = proportion;
                proportion = ((double)_notes[i - 1, 2]) / _notes[i, 2];
                if (Math.Abs(proportion) == Math.Abs(prevProportion))
                    rank -= 30;
                if(proportion*prevProportion<0)
                {
                    sameDirectionRhythm = 0;
                }
                else
                {
                    sameDirectionRhythm++;
                    if(sameDirectionRhythm>2)
                    {
                        rank -= sameDirectionRhythm * 20;
                    }
                }
                prevDifference = difference;
                difference = _notes[i - 1, 3] - _notes[i, 3];
                if (difference > 20)
                    rank += 40;
                if (difference * prevDifference < 0)
                {
                    sameDirectionDynamics = 0;
                }
                else
                {
                    sameDirectionDynamics++;
                    if (sameDirectionDynamics > 2)
                    {
                        rank -= sameDirectionDynamics * 30;
                    }
                }
            }
            int mean = _numberOfNotes / limits[0];
            for (int i = 0; i < limits[0];i++ )
            {
                rank -= (count[i] - mean) * (count[i] - mean);
            }
            rank -= (_numberOfNotes - PrefferedLength) * (_numberOfNotes - PrefferedLength)*60;
               return rank;
        }
        public override void Mutate(Random randContext)
        {
            if (randContext.NextDouble() < GrowthChance)
            {
                Grow(randContext);
            }
            if (randContext.NextDouble() < ExchangeChance[0])
            {
                Exchange(0, randContext);
            }
            if (randContext.NextDouble() < ExchangeChance[2])
            {
                Exchange(2, randContext);
            }
            if (randContext.NextDouble() < ExchangeChance[3])
            {
                Exchange(3, randContext);
            }
            if (randContext.NextDouble() < TransposeChance[0])
            {
                Transpose(0, randContext);
            }
            if (randContext.NextDouble() < TransposeChance[2])
            {
                Transpose(2, randContext);
            }
            if (randContext.NextDouble() < TransposeChance[3])
            {
                Transpose(3, randContext);
            }
            if (randContext.NextDouble() < ModifyChance[0])
            {
                Modify(0, randContext);
            }
            if (randContext.NextDouble() < ModifyChance[2])
            {
                Modify(2, randContext);
            }
            if (randContext.NextDouble() < ModifyChance[3])
            {
                Modify(3, randContext);
            }
            if (randContext.NextDouble() < ShrinkChance)
            {
                Shrink();
            }
        }
        public override Member Clone()
        {
            Member1 result = new Member1();
            result._numberOfNotes = NumberOfNotes;
            int[,] notes = new int[_maxNotes, 4];
            Array.Copy(_notes, notes, _maxNotes * 4);
            result._notes = notes;

            return result;
        }
        #endregion
    }
}
