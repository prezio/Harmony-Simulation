using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public partial class Member1 : Member
    {
        private int _numberOfNotes;
        private int[,] _notes;

        protected void Transpose(uint n, Random randContext)
        {
            if (NumberOfNotes <= 1)
            {
                return;
            }
            int temp;
            int place = randContext.Next(NumberOfNotes - 1);
            temp = Notes[place, n];
            Notes[place, n] = Notes[place + 1, n];
            Notes[place + 1, n] = temp;
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
            if (NumberOfNotes <= 1)
            {
                return;
            }
            int place = randContext.Next(NumberOfNotes - 1);
            Notes[place, n] = randContext.Next(limits[n]);
        }
        protected void Modify(uint n, Random randContext)
        {
            if (NumberOfNotes <= 1)
            {
                return;
            }
            int temp;
            
            int place = randContext.Next(NumberOfNotes - 1);
            temp = randContext.Next(-ModifyAmount[n], ModifyAmount[n] + 1);
            Notes[place, n] += temp;
            if (Notes[place, n] >= limits[n])
            {
                Notes[place, n] = limits[n] - 1;
            }
            else if (Notes[place,n] < 0)
            {
                Notes[place, n] = 0;
            }
        }
        protected void Shrink()
        {
            if (NumberOfNotes > 1)
                _numberOfNotes --;
        }
        protected void Grow(Random randContext)
        {
            if (NumberOfNotes >= _maxNotes)
                return;

            Notes[NumberOfNotes, 0] = randContext.Next(limits[0]);
            Notes[NumberOfNotes, 1] = 0;
            Notes[NumberOfNotes, 2] = randContext.Next(limits[2]);
            Notes[NumberOfNotes, 3] = randContext.Next(limits[3]);

            _numberOfNotes++;
        }

        protected static readonly int[] limits = new int[] { 20,0, 24, 50 };

        public Member1(Random randContext)
            : base(randContext)
        {
            _numberOfNotes = randContext.Next(_maxNotes - 1) + 1;
            _notes = new int[_maxNotes + 1, 4];

            for (int i = 0; i < NumberOfNotes; i++)
            {
                Notes[i, 0] = randContext.Next(limits[0]);
                Notes[i, 1] = 0;
                Notes[i, 2] = randContext.Next(limits[2]);
                Notes[i, 3] = randContext.Next(limits[3]);
            }
        }
        public Member1(Member original)
            : base(original)
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
        public override void Influence(Member influencer, Random randContext)
        {
            if (NumberOfNotes < influencer.NumberOfNotes)
            {
                Grow(randContext);
            }
            else if (NumberOfNotes > influencer.NumberOfNotes)
            {
                _numberOfNotes --;
            }
            for (int i = 0; i < NumberOfNotes; ++i)
            {
                Notes[i, 0] += (int)(InfluenceAmount[0] * (influencer.Notes[i % influencer.NumberOfNotes, 0] - Notes[i, 0]));
                Notes[i, 2] += (int)(InfluenceAmount[2] * (influencer.Notes[i % influencer.NumberOfNotes, 2] - Notes[i, 2]));
                Notes[i, 3] += (int)(InfluenceAmount[3] * (influencer.Notes[i % influencer.NumberOfNotes, 3] - Notes[i, 3]));
            }
        }
        public override int Rank()
        {
            int[] count = new int[limits[0]];
            int rank = 0;
            double proportion = ((double)Notes[0, 2]) / Notes[1, 2];
            double prevProportion = proportion;
            int difference = Notes[1, 3] - Notes[0, 3];
            int prevDifference = difference;
            int sameDirectionRhythm = 0;
            int sameDirectionDynamics = 0;
            count[Notes[0, 0]]++;
            count[Notes[1, 0]]++;
            if (difference > 40)
                rank += 40;
            for (int i = 2; i < NumberOfNotes; i++)
            {
                count[Notes[i, 0]]++;
                prevProportion = proportion;
                proportion = ((double)Notes[i-1, 2]) / Notes[i, 2];
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
                difference = Notes[i-1, 3] - Notes[i, 3];
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
        public override void Clone(Member member)
        {
            _numberOfNotes = member.NumberOfNotes;
            _notes = new int[_maxNotes + 1, 4];
            Array.Copy(member.Notes, Notes, (_maxNotes + 1) * 4);
        }
        #endregion
    }
}
