using System;
using System.Diagnostics;
using System.Threading;

namespace MusicPopulation
{
    /// <summary>
    /// Class responsible for area thread management
    /// </summary>
    public class AreaThread
    {
        private int _indexOfArea;

        /// <summary>
        /// Constructs new object responsible for given area.
        /// </summary>
        /// <param name="index">Number of area, integer between 0 and 15.</param>
        public AreaThread(int index)
        {
            _indexOfArea = index;
        }

        /// <summary>
        /// First part of evolution.
        /// </summary>
        public void EvolvePart1()
        {
            Simulation.Areas[_indexOfArea].KillWeaksWhoDoesNotServeTheEmperorWell();
            Simulation.Areas[_indexOfArea].SelectChampionWhoCanBecomeCommissar();
            Simulation.Areas[_indexOfArea].ReproduceMenToHaveMoreServantsOfTheEmperor();
            Simulation.Areas[_indexOfArea].MutateWeaksSoTheyCanServeEmperorBetter();
            Simulation.Areas[_indexOfArea].InfluenceMenWithSongsGlorifyingEmperor();
            Simulation.Areas[_indexOfArea].MoveYourMenSergant();
        }
        /// <summary>
        /// Second part of evolution.
        /// </summary>
        public void EvolvePart2()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(0);
        }
        /// <summary>
        /// Third part of evolution.
        /// </summary>
        public void EvolvePart3()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(1);
        }
        /// <summary>
        /// Fourth part of evolution.
        /// </summary>
        public void EvolvePart4()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(2);
        }
        /// <summary>
        /// Fifth part of evolution.
        /// </summary>
        public void EvolvePart5()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(3);
        }
    }
}
