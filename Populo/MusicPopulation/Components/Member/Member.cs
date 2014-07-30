using System;

namespace MusicPopulation
{
    public abstract class Member
    {
        protected const int _maxNotes = 255;
        protected static readonly int[] limits = new int[] { 72, 60, 128 };
        
        public int NumberOfNotes { get; set; }
        public int[,] Notes { get; set; } //pitch, duration, dynamics
        public Member(Random randContext)
        {
            NumberOfNotes = randContext.Next(_maxNotes - 1) + 1;
            Notes = new int[_maxNotes + 1, 3];

            for (int i = 0; i < NumberOfNotes; i++)
            {
                Notes[i, 0] = randContext.Next(limits[0]);
                Notes[i, 1] = randContext.Next(limits[1]);
                Notes[i, 2] = randContext.Next(limits[2]);
            }
        }
        public Member(Member original)
        {
            NumberOfNotes =original.NumberOfNotes;
            Notes = new int[_maxNotes + 1, 3];
            Array.Copy(original.Notes, Notes, _maxNotes);
        }
        public void Clone(Member member)
        {
            NumberOfNotes = member.NumberOfNotes;
            Notes = new int[_maxNotes + 1, 3];
            Array.Copy(member.Notes, Notes, _maxNotes);
        }

        public abstract int Rank();
        public abstract void Mutate(Random randContext);
        public abstract void Influence(Member influencer, Random randContext);
    }
}
