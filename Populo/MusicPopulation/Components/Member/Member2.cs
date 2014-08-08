using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public partial class Member2: Member
    {
        private int _numberOfNotes;
        private int _peak;
        private int _initialRhythm;
        private int _initialDynamics;
        private int _initialChord;
        private int _pauseDuration;
        const int numberOfChords = 3;
        private int[,] _notes; //pitch, chord change, rhythm, rhythm distortion, dynamics, dynamics distortion
        static int[] limits={24,2,20,1,127,1};
        const int maxLength = 25;
        const int minLength = 3;
        const int maxPause = 101;

        public override int NumberOfNotes
        {
            get { return _numberOfNotes+1; }
        }

        public override int[,] Notes
        {
            get 
            {
                
                int[,] notes = new int[_maxNotes + 1, 4];
                notes[0, 0] = _notes[0, 0];
                notes[0, 1] = (_initialChord+ _notes[0, 1])%numberOfChords;
                notes[0, 2] = _initialRhythm;
                notes[0, 3] = _initialDynamics;
                
                for(int i=1; (i<=_peak)&&(i<_numberOfNotes); i++)
                {
                    notes[i, 0] = _notes[i, 0];
                    notes[i, 1] = (notes[i - 1, 1] + _notes[0, 1]) % numberOfChords;
                    notes[i, 2] = notes[i - 1, 2] - _notes[i, 2] * _notes[i, 3];
                    if(notes[i,2]<1)
                    {
                        notes[i, 2] = 1;
                    }
                    else if(notes[i,2]>limits[2])
                    {
                        notes[i, 2] = limits[2];
                    }
                    notes[i, 3] = notes[i - 1, 3] + _notes[i, 4] * _notes[i, 5];
                    if (notes[i, 3] < 1)
                    {
                        notes[i, 3] = 1;
                    }
                    else if (notes[i, 3] > limits[4])
                    {
                        notes[i, 3] = limits[4];
                    }
                }
                for (int i = _peak; i < _numberOfNotes; i++)
                {
                    notes[i, 0] = _notes[i, 0];
                    notes[i, 1] = (notes[i - 1, 1] + _notes[0, 1]) % numberOfChords;
                    notes[i, 2] = notes[i - 1, 2] + _notes[i, 2] * _notes[i, 3];
                    if (notes[i, 2] < 1)
                    {
                        notes[i, 2] = 1;
                    }
                    else if (notes[i, 2] > limits[2])
                    {
                        notes[i, 2] = limits[2];
                    }
                    notes[i, 3] = notes[i - 1, 3] - _notes[i, 4] * _notes[i, 5];
                    if (notes[i, 3] < 1)
                    {
                        notes[i, 3] = 1;
                    }
                    else if (notes[i, 3] > limits[4])
                    {
                        notes[i, 3] = limits[4];
                    }
                }
                notes[_numberOfNotes, 2] = _pauseDuration;
                notes[_numberOfNotes, 3] = 0;
                return notes;

            }
        }

        public override int Rank()
        {
            int rank = 0;
            
            
            int rhythm = _initialRhythm;
            int dynamics = _initialDynamics;

            for (int i = 0; (i <= _peak) && (i < _numberOfNotes); i++)
            {

                rhythm -=  _notes[i, 2] * _notes[i, 3];
                if (rhythm < 1)
                {
                    rank -= 40;
                }
                else if (rhythm > limits[2])
                {
                    rank -= 30;
                }
                dynamics+= _notes[i, 4] * _notes[i, 5];
                if (dynamics < 1)
                {
                    rank -= 40;
                }
                else if (dynamics > limits[4])
                {
                    rank -= 30;
                }
            }
            for (int i = Math.Max(_peak, 0); i < _numberOfNotes; i++)
            {
                rhythm += _notes[i, 2] * _notes[i, 3];
                if (rhythm < 1)
                {
                    rank -= 40;
                }
                else if (rhythm > limits[2])
                {
                    rank -= 30;
                }
                dynamics -= _notes[i, 4] * _notes[i, 5];
                if (dynamics < 1)
                {
                    rank -= 40;
                }
                else if (dynamics > limits[4])
                {
                    rank -= 30;
                }
            }
            
            rank -= (_numberOfNotes - PrefferedLength) * (_numberOfNotes - PrefferedLength) * 60;
            rank -= (_pauseDuration - PrefferedPauseLength) * (_pauseDuration - PrefferedPauseLength) * 5;
            return rank;
        }

        public override void Mutate(Random randContext)
        {
            if (randContext.NextDouble() < GrowthChance)
            {
                Grow(randContext);
            }
            if (randContext.NextDouble() < PeakMoveChance)
            {
                _peak += (randContext.Next(-PeakMaxMove, PeakMaxMove + 1));
                if(_peak<-10)
                {
                    _peak = -10;
                }
                else if(_peak>maxLength+10)
                {
                    _peak = maxLength + 10;
                }
            }
            if (randContext.NextDouble() < PauseChangeChance)
            {
                _peak += (randContext.Next(-PauseMaxChange, PauseMaxChange + 1));
                if (_pauseDuration < 10)
                {
                    _pauseDuration = 10;
                }
                else if (_pauseDuration > maxPause)
                {
                    _pauseDuration = maxPause;
                }
            }
            if (randContext.NextDouble() < InitialRhythmChangeChance)
            {
                _peak += (randContext.Next(-InitialRhythmMaxChange, InitialRhythmMaxChange + 1));
                if (_initialRhythm < 1)
                {
                    _initialRhythm = 1;
                }
                else if (_initialRhythm > limits[2])
                {
                    _initialRhythm = limits[2];
                }
            }
            if (randContext.NextDouble() < InitialDynamicsChangeChance)
            {
                _peak += (randContext.Next(-InitialDynamicsMaxChange, InitialDynamicsMaxChange + 1));
                if (_initialDynamics < 1)
                {
                    _initialDynamics = 1;
                }
                else if (_initialDynamics > limits[4])
                {
                    _initialDynamics = limits[4];
                }
            }
            if (randContext.NextDouble() < InitialChordChangeChance)
            {
                _initialChord++;
                _initialChord %= numberOfChords;
            }
            if (randContext.NextDouble() < RhythmDistortionChangeChance)
            {
                int place = randContext.Next(_numberOfNotes);
                _notes[place,3]*=-1;
            }
            if (randContext.NextDouble() < DynamicsDistortionChangeChance)
            {
                int place = randContext.Next(_numberOfNotes);
                _notes[place,5]*=-1;
            }
            if (randContext.NextDouble() < ChordChangeChance)
            {
                int place = randContext.Next(_numberOfNotes);
                _notes[place, 1] = (_notes[place, 1] + 1) % 2;
            }
            if (randContext.NextDouble() < PitchChangeChance)
            {
                int place = randContext.Next(_numberOfNotes);
                _notes[place, 0] += randContext.Next(-PitchMaxChange, PitchMaxChange + 1);
                if (_notes[place, 0] > limits[0])
                    _notes[place, 0] = limits[0];
                else if (_notes[place, 0] <0)
                    _notes[place, 0] = 0;
            }
            if (randContext.NextDouble() < RhythmChangeChance)
            {
                int place = randContext.Next(_numberOfNotes);
                _notes[place, 2] += randContext.Next(-RhythmMaxChange, RhythmMaxChange + 1);
                if (_notes[place, 2] > limits[2])
                    _notes[place, 2] = limits[2];
                else if (_notes[place, 2] < 1)
                    _notes[place, 2] = 1;
            }
            if (randContext.NextDouble() < DynamicsChangeChance)
            {
                int place = randContext.Next(_numberOfNotes);
                _notes[place, 4] += randContext.Next(-DynamicsMaxChange, DynamicsMaxChange + 1);
                if (_notes[place, 4] > limits[4])
                    _notes[place, 4] = limits[4];
                else if (_notes[place, 4] < 1)
                    _notes[place, 4] = 1;
            }
            if (randContext.NextDouble() < ShrinkChance)
            {
                Shrink();
            }
        }
        
        public override void Influence(Member influencer, Random randContext)
        {
            Member2 m = (influencer as Member2);
            _initialDynamics +=(int) ((_initialDynamics - m._initialDynamics) * DynamicsInfluenceAmount);
            _initialRhythm += (int)((_initialRhythm - m._initialRhythm) * RhythmInfluenceAmount);
            if(_numberOfNotes<m._numberOfNotes)
            {
                Grow(randContext);
            }
            else if(_numberOfNotes>m._numberOfNotes)
            {
                _numberOfNotes--;
            }
            _pauseDuration+=(int)((_pauseDuration-m._pauseDuration)*PauseInfluenceAmount);
            for (int i = 0; i < _numberOfNotes; i++)
            {
                _notes[i, 0] += (int)((_notes[i, 0] - m._notes[i%m._numberOfNotes, 0])*PitchInfluenceAmount);
                _notes[i, 1] = (_notes[i, 1] + m._notes[i % m._numberOfNotes, 1]) % 2;
                _notes[i, 2] += (int)((_notes[i, 2] - m._notes[i % m._numberOfNotes, 2]) * RhythmInfluenceAmount);
                if (randContext.NextDouble() < RhythmDistortionInfluenceProbability) _notes[i, 3] = m._notes[i % m._numberOfNotes, 3];
                _notes[i, 4] += (int)((_notes[i, 4] - m._notes[i % m._numberOfNotes, 4]) * DynamicsInfluenceAmount);
                if (randContext.NextDouble() < DynamicsDistortionInfluenceProbability) _notes[i, 5] = m._notes[i % m._numberOfNotes, 5];
            }

        }
        protected void Shrink()
        {
            if (_numberOfNotes > minLength)
                _numberOfNotes--;
        }
        protected void Grow(Random randContext)
        {
            if (NumberOfNotes >= maxLength)
                return;

            _notes[_numberOfNotes, 0] = randContext.Next(limits[0]);
            _notes[_numberOfNotes, 1] = randContext.Next(limits[1]);
            _notes[_numberOfNotes, 2] = randContext.Next(limits[2]);
            _notes[_numberOfNotes, 3] = 1;
            _notes[_numberOfNotes, 4] = randContext.Next(limits[4]);
            _notes[_numberOfNotes, 0] = 1;

            _numberOfNotes++;
        }
        public Member2(Random randContext):base(randContext)
        {
            _initialChord = randContext.Next(numberOfChords);
            _initialDynamics = randContext.Next(limits[4]);
            _initialRhythm = randContext.Next(limits[2]);
            _numberOfNotes = randContext.Next(minLength, maxLength);
            _notes = new int[_maxNotes, 6];
            _peak = randContext.Next(_numberOfNotes);
            _pauseDuration = randContext.Next(maxPause);
            for(int i=0; i<_numberOfNotes;i++)
            {
                _notes[i, 0] = randContext.Next(limits[0]);
                _notes[i, 1] = randContext.Next(limits[1]);
                _notes[i, 2] = randContext.Next(limits[2]);
                _notes[i, 3] = 1;
                _notes[i, 4] = randContext.Next(limits[4]);
                _notes[i, 0] = 1;
            }
            int n = randContext.Next(_numberOfNotes/4);
            for(int i=0; i<n;i++)
            {
                _notes[randContext.Next(_numberOfNotes), 3] = -1;
            }
            n = randContext.Next(_numberOfNotes / 4);
            for (int i = 0; i < n; i++)
            {
                _notes[randContext.Next(_numberOfNotes), 5] = -1;
            }
        }
        public Member2(Member original)
            : base(original)
        {
        }
        public override void Clone(Member member)
        {
            Member2 m = (member as Member2);
            _initialChord = m._initialChord;
            _initialDynamics = m._initialDynamics;
            _initialRhythm = m._initialRhythm;

            _notes = new int[_maxNotes, 6];
            Array.Copy(m._notes, _notes, 6 * _maxNotes);

            _numberOfNotes = m._numberOfNotes;
            _pauseDuration = m._pauseDuration;
            _peak = m._peak;
        }
    }
}
