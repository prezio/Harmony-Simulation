using System;

namespace MusicPopulation
{
    public abstract class Member
    {
        protected const int _maxNotes = 255;

        public Member(Random randContext)
        {
        }
        public Member(Member original)
        {
            Clone(original);
        }
        public Tuple<int, int[,]> CloneParameters()
        {
            int[,] notes = new int[_maxNotes + 1, 4];
            int[,] notesToCopy = Notes;

            Array.Copy(notesToCopy, notes, (_maxNotes + 1) * 4);

            return new Tuple<int, int[,]>(NumberOfNotes, notes);
        }

        #region Absract Methods
        public abstract int NumberOfNotes { get; }
        public abstract int[,] Notes { get; }
        public abstract int Rank();
        public abstract void Mutate(Random randContext);
        public abstract void Influence(Member influencer, Random randContext);
        public abstract void Clone(Member member);
        #endregion
    }
}
