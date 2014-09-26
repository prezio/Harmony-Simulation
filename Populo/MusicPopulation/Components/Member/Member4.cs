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
    public partial class Member4 : Member
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
                _numberOfNotes--;
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

        protected static readonly int[] limits = new int[] { 20, 0, 16, 127 };

        public Member4(Random randContext)
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
            _notes[0, 1] = 1;
        }
        public Member4()
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
        public override void Influence(Member influencer, Random randContext)
        {
            Member4 member = influencer as Member4;
            if (_numberOfNotes < member._numberOfNotes)
            {
                Grow(randContext);
            }
            else if (_numberOfNotes > member._numberOfNotes)
            {
                _numberOfNotes--;
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
            int rhythmChange = 0;
            int rank = 0;
            rank -= _notes[0, 2] & (_notes[0, 2] - 1) * 10;
            for (int i = 1; i < _numberOfNotes; i++)
            {
                rank -= _notes[i, 2] & (_notes[i, 2] - 1) * 10;
                if (_notes[i, 2] != _notes[i - 1, 2])
                    rhythmChange++;
                if ((_notes[i, 0] - _notes[i - 1, 0] + limits[0]) % limits[0] < 2)
                    rank += 40;
                if (Math.Abs(_notes[i, 3] - _notes[i - 1, 3]) < 20)
                    rank += 30;
            }
            rank -= (_numberOfNotes / 3 - rhythmChange) * (_numberOfNotes / 3 - rhythmChange) * 30;
            rank -= (_numberOfNotes - PrefferedLength) * (_numberOfNotes - PrefferedLength) * 60;
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
            Member4 result = new Member4();
            result._numberOfNotes = NumberOfNotes;
            int[,] notes = new int[_maxNotes, 4];
            Array.Copy(_notes, notes, _maxNotes * 4);
            result._notes = notes;

            return result;
        }
        #endregion
    }
}
