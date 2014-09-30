using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    /// <summary>
    /// Implementation of second phase member.
    /// </summary>
    public partial class Member2: Member
    {
        private int _numberOfNotes;
        private int _peak;
        private int _peakRhythm;
        private int _peakDynamics;
        private int _initialChord;
        private int _pauseDuration;
        private int _type; //0-r, 1-ar, 2-a
        private int[,] _notes; //pitch, chord change, rhythm, rhythm distortion, dynamics, dynamics distortion
        static int[] limits={24,2,3,1,20,1};
        const int maxLength = 24;
        const int minLength = 5;
        const int maxPause = 100;

        public override int NumberOfNotes
        {
            get { return _numberOfNotes+1; }
        }

        public override int[,] Notes
        {
            get 
            {
                int peak = _peak;
                int[,] notes = new int[_maxNotes + 1, 4];
                
                if(_type == 0)
                {
                    peak = 0;
                   
                }
                else if (_type == 2)
                {
                    peak = _numberOfNotes-1;
                    
                }
                int difference = 1;
                notes[peak, 0] = _notes[peak, 0];
                notes[peak, 1] = 0;
                notes[peak, 3] = _peakDynamics;
                notes[peak, 2] = _peakRhythm;
                
                
                for(int i=peak-1; i>=0; i--)
                {
                    notes[i, 0] = _notes[i, 0];
                    notes[i, 1] = 0;
                    notes[i, 2] = notes[i + 1, 2] + difference;
                    difference+=_notes[i, 2];
                    
                    if(notes[i,2]>30)
                    {
                        notes[i, 2] = 30;
                    }
                    notes[i, 3] = notes[i + 1, 3] - _notes[i, 4];
                    if (notes[i, 3] < 15)
                    {
                        notes[i, 3] = 15;
                    }
                    
                }
                difference = 1;
                for (int i = peak+1; i < _numberOfNotes; i++)
                {
                    notes[i, 0] = _notes[i, 0];
                    notes[i, 1] = 0;
                    notes[i, 2] = notes[i - 1, 2] + difference;
                    difference += _notes[i, 2];
                    if (notes[i, 2] > 30)
                    {
                        notes[i, 2] = 30;
                    }
                    notes[i, 3] = notes[i - 1, 3] - _notes[i, 4];
                    if (notes[i, 3] < 15)
                    {
                        notes[i, 3] = 15;
                    }
                    
                }

                notes[_numberOfNotes, 1] = 1;
                notes[_numberOfNotes, 2] = _pauseDuration;
                notes[_numberOfNotes, 3] = 0;
                return notes;

            }
        }

        public override int Rank()
        {
            int rank = 0;
            
            
            int rhythm = _peakRhythm;
            int dynamics = _peakDynamics;
            int peak = _peak;
            int difference = 1;
            if (_type == 0)
            {
                peak = 0;
                rank +=20;
               
            }
            else if (_type == 2)
            {
                peak = _numberOfNotes-1;
                rank += 15;
                
            }
            rank -= _peakRhythm * _peakRhythm * 200;
            
            for (int i = peak-1; i>=0; i--)
            {

                rhythm += difference;
                difference+=  _notes[i, 2];
                if (rhythm > 40)
                {
                    rank -= 15;
                }
                dynamics-= _notes[i, 4];
                if (dynamics > 20)
                {
                    rank += 10;
                }
                
                if (_notes[i, 4] > 2)
                {
                    rank += 15;
                }
                if (_notes[i, 2] >= _notes[i+1,2])
                {
                    rank += 15;
                }
            }
            if (rhythm / _peakRhythm >= 10)
            {
                rank += 100;
            }
            if (_peakDynamics - dynamics > 50)
            {
                rank += 100;
            }
            rhythm = _peakRhythm;
            dynamics = _peakDynamics;
            difference = 1;
            for (int i = peak+1; i < _numberOfNotes; i++)
            {
                rhythm += difference;
                difference += _notes[i, 2];
                if (rhythm > 40)
                {
                    rank -= 15;
                }
                dynamics -= _notes[i, 4];
                if (dynamics > 20)
                {
                    rank += 10;
                }
                if (_notes[i, 4] > 2)
                {
                    rank += 15;
                }

                if (_notes[i, 2] >= _notes[i - 1, 2])
                {
                    rank += 15;
                }
                
            }


            if (Math.Abs(_numberOfNotes - PrefferedLength) < 5)
            {
                rank += 1000;
                if (_numberOfNotes == PrefferedLength)
                {
                    rank += 2000;
                }
            }
            //rank -= (_numberOfNotes - PrefferedLength) * (_numberOfNotes - PrefferedLength) * 400;
            rank -= (_pauseDuration - PrefferedPauseLength) * (_pauseDuration - PrefferedPauseLength)*5;
            if(Math.Abs(_peak-_numberOfNotes/2)<_numberOfNotes/7)
            {
                rank += 300;
            }
            if (rhythm / _peakRhythm >= 10)
            {
                rank += 100;
            }
           
            if (_peakDynamics - dynamics > 50)
            {
                rank += 100;
            }
            
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
                if(_peak<0)
                {
                    _peak = 0;
                }
                else if(_peak>=_numberOfNotes)
                {
                    _peak = _numberOfNotes - 1;
                }
            }
            if (randContext.NextDouble() < PauseChangeChance)
            {
                _pauseDuration += (randContext.Next(-PauseMaxChange, PauseMaxChange + 1));
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
                _peakRhythm += (randContext.Next(-InitialRhythmMaxChange, InitialRhythmMaxChange + 1));
                if (_peakRhythm < 1)
                {
                    _peakRhythm = 1;
                }
                else if (_peakRhythm > 4)
                {
                    _peakRhythm = 4;
                }
            }
            if (randContext.NextDouble() < InitialDynamicsChangeChance)
            {
                _peakDynamics += (randContext.Next(-InitialDynamicsMaxChange, InitialDynamicsMaxChange + 1));
                if (_peakDynamics < 60)
                {
                    _peakDynamics = 60;
                }
                else if (_peakDynamics > 127)
                {
                    _peakDynamics = 127;
                }
            }
            if (randContext.NextDouble() < InitialChordChangeChance)
            {
                _initialChord++;
                
            }
            if (randContext.NextDouble() < TypeChangeChance)
            {
                _type++;
                _type %= 3;

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
            _peakDynamics +=(int) ((m._peakDynamics - _peakDynamics) * DynamicsInfluenceAmount);
            _peakRhythm += (int)((m._peakRhythm - _peakRhythm) * RhythmInfluenceAmount);
            if(_numberOfNotes<m._numberOfNotes)
            {
                Grow(randContext);
            }
            else if(_numberOfNotes>m._numberOfNotes)
            {
                _numberOfNotes--;
            }
            if(randContext.NextDouble()<TypeInfluenceChance)
            {
                _type = m._type;
            }
            _pauseDuration += (int)((m._pauseDuration - _pauseDuration) * PauseInfluenceAmount);
            for (int i = 0; i < _numberOfNotes; i++)
            {
                _notes[i, 0] += (int)((m._notes[i % m._numberOfNotes, 0] - _notes[i, 0]) * PitchInfluenceAmount);
                _notes[i, 1] = (m._notes[i % m._numberOfNotes, 1] + _notes[i , 1]) % 2;
                _notes[i, 2] += (int)((m._notes[i % m._numberOfNotes, 2] - _notes[i , 2]) * RhythmInfluenceAmount);
                if (randContext.NextDouble() < RhythmDistortionInfluenceChance) _notes[i, 3] = m._notes[i % m._numberOfNotes, 3];
                _notes[i, 4] += (int)((m._notes[i % m._numberOfNotes, 4] - _notes[i , 4]) * DynamicsInfluenceAmount);
                if (randContext.NextDouble() < DynamicsDistortionInfluenceChance) _notes[i, 5] = m._notes[i % m._numberOfNotes, 5];
            }

        }
        protected void Shrink()
        {
            if (_numberOfNotes > minLength)
                _numberOfNotes--;
            if(_peak>=_numberOfNotes)
                _peak=_numberOfNotes-1;
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
            _notes[_numberOfNotes, 5] = 1;

            _numberOfNotes++;
        }
        public Member2(Random randContext):base(randContext)
        {
            _initialChord = randContext.Next(10); //UGLY!
            _peakDynamics = randContext.Next(60, 127);
            _peakRhythm = randContext.Next(4)+1;
            _numberOfNotes = randContext.Next(minLength, maxLength);
            _notes = new int[_maxNotes, 6];
            _peak = randContext.Next(_numberOfNotes);
            _pauseDuration = randContext.Next(maxPause);
            _type = randContext.Next(3);
            for(int i=0; i<_numberOfNotes;i++)
            {
                _notes[i, 0] = randContext.Next(limits[0]);
                _notes[i, 1] = randContext.Next(limits[1]);
                _notes[i, 2] = randContext.Next(limits[2]);
                _notes[i, 3] = 1;
                _notes[i, 4] = randContext.Next(limits[4]);
                _notes[i, 5] = 1;
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
        public Member2()
            : base()
        {
        }
        public override Member Clone()
        {
            Member2 result = new Member2();

            result._initialChord = _initialChord;
            result._peakDynamics = _peakDynamics;
            result._peakRhythm = _peakRhythm;
            result._type = _type;
            result._notes = new int[_maxNotes, 6];

            Array.Copy(_notes, result._notes, 6 * _maxNotes);

            result._numberOfNotes = _numberOfNotes;
            result._pauseDuration = _pauseDuration;
            result._peak = _peak;

            return result;
        }
    }
}
