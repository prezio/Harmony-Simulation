﻿using System;

namespace MusicPopulation
{
    /// <summary>
    /// Each class representing member should be inherit this abstract class.
    /// </summary>
    public abstract class Member
    {
        protected const int _maxNotes = 255;
        public static int[] ModifyAmount = new int[] { 3, 0, 5, 15 };
        public static double[] InfluenceAmount = new double[] { 0.2, 0, 0.2, 0.2 };
        public static double[] TransposeChance = new double[] { 0.05, 0, 0.05, 0.05 };
        public static double[] ExchangeChance = new double[] { 0.05, 0, 0.05, 0.05 };
        public static double[] ModifyChance = new double[] { 0.05, 0, 0.05, 0.05 };
        public static double GrowthChance = 0.05;
        public static double ShrinkChance = 0.05;
        public static int PrefferedLength = 5;
        public static int played = 0;
        
        public Member(Random randContext)
        {
        }
        public Member()
        {
        }
        public Tuple<int, int[,]> CloneParameters()
        {
            int[,] notes = new int[_maxNotes, 4];
            int[,] notesToCopy = Notes;

            Array.Copy(notesToCopy, notes, _maxNotes * 4);

            return new Tuple<int, int[,]>(NumberOfNotes, notes);
        }

        #region Absract Methods
        /// <summary>
        /// Getter which returns number of notes.
        /// </summary>
        public abstract int NumberOfNotes { get; }
        /// <summary>
        /// Getter which returns array of notes.
        /// </summary>
        public abstract int[,] Notes { get; }
        /// <summary>
        /// Rank of the member.
        /// </summary>
        /// <returns>integer representing rank of member</returns>
        public abstract int Rank();
        /// <summary>
        /// Method responsible for member mutation.
        /// </summary>
        /// <param name="randContext">random context</param>
        public abstract void Mutate(Random randContext);
        /// <summary>
        /// Method responible for influencing individual.
        /// </summary>
        /// <param name="influencer">Member which should be influenced</param>
        /// <param name="randContext">random context</param>
        public abstract void Influence(Member influencer, Random randContext);
        /// <summary>
        /// Clone member.
        /// </summary>
        /// <returns>copy of member</returns>
        public abstract Member Clone();
        #endregion
    }
}
