using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public partial class Member1 : Member
    {
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
                NumberOfNotes --;
        }
        protected void Grow(Random randContext)
        {
            if (NumberOfNotes >= _maxNotes)
                return;

            Notes[NumberOfNotes, 0] = randContext.Next(limits[0]);
            Notes[NumberOfNotes, 1] = randContext.Next(limits[1]);
            Notes[NumberOfNotes, 2] = randContext.Next(limits[2]);

            NumberOfNotes++;
        }

        public Member1(Random randContext)
            : base(randContext)
        {
        }
        public Member1(Member original)
            : base(original)
        {
        }

        public override void Influence(Member influencer, Random randContext)
        {
            if (NumberOfNotes < influencer.NumberOfNotes)
            {
                Grow(randContext);
            }
            else if (NumberOfNotes > influencer.NumberOfNotes)
            {
                NumberOfNotes --;
            }
            for (int i = 0; i < NumberOfNotes; ++i)
            {
                Notes[i, 0] += (int)(InfluenceAmount[0] * (influencer.Notes[i % influencer.NumberOfNotes, 0] - Notes[i, 0]));
                Notes[i, 1] += (int)(InfluenceAmount[1] * (influencer.Notes[i % influencer.NumberOfNotes, 1] - Notes[i, 1]));
                Notes[i, 2] += (int)(InfluenceAmount[2] * (influencer.Notes[i % influencer.NumberOfNotes, 2] - Notes[i, 2]));
            }
        }
        public override int Rank()
        {
            int rank = 0;
            //for (int i = 0; i < NumberOfNotes; i++)
            //{
            //    rank -= (Notes[i, 1] - i) * (Notes[i, 1] - i);
            //}
            int previousChord = Notes[0, 0] / 24;
            int currentChord = 0;
            for (int i = 1; i < NumberOfNotes; i++)
            {
                currentChord = Notes[i, 0] / 24;
                if ((currentChord - previousChord + 3) % 3 == 2)
                {
                    rank -= 10;
                }
                else
                {
                    rank += 10;
                }
                if (Notes[i, 1] > Notes[i - 1, 1])
                {
                    rank += 15;
                }
                if (Notes[i, 2] < Notes[i - 1, 2])
                {
                    rank += 7;
                }
            }
            
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
            if (randContext.NextDouble() < ExchangeChance[1])
            {
                Exchange(1, randContext);
            }
            if (randContext.NextDouble() < ExchangeChance[2])
            {
                Exchange(2, randContext);
            }
            if (randContext.NextDouble() < TransposeChance[0])
            {
                Transpose(0, randContext);
            }
            if (randContext.NextDouble() < TransposeChance[1])
            {
                Transpose(1, randContext);
            }
            if (randContext.NextDouble() < TransposeChance[2])
            {
                Transpose(2, randContext);
            }
            if (randContext.NextDouble() < ModifyChance[0])
            {
                Modify(0, randContext);
            }
            if (randContext.NextDouble() < ModifyChance[1])
            {
                Modify(1, randContext);
            }
            if (randContext.NextDouble() < ModifyChance[2])
            {
                Modify(2, randContext);
            }
            if (randContext.NextDouble() < ShrinkChance)
            {
                Shrink();
            }
        }
    }
}
