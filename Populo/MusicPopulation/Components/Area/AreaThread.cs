using System;
using System.Diagnostics;
using System.Threading;

namespace MusicPopulation
{
    public class AreaThread
    {
        private int _indexOfArea;

        public AreaThread(int index)
        {
            _indexOfArea = index;
        }

        public void EvolvePart1()
        {
            Simulation.Areas[_indexOfArea].KillWeaksWhoDoesNotServeTheEmperorWell();
            Simulation.Areas[_indexOfArea].SelectChampionWhoCanBecomeCommissar();
            Simulation.Areas[_indexOfArea].ReproduceMenToHaveMoreServantsOfTheEmperor();
            Simulation.Areas[_indexOfArea].MutateWeaksSoTheyCanServeEmperorBetter();
            Simulation.Areas[_indexOfArea].InfluenceMenWithSongsGlorifyingEmperor();
            Simulation.Areas[_indexOfArea].MoveYourMenSergant();
        }
        public void EvolvePart2()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(0);
        }
        public void EvolvePart3()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(1);
        }
        public void EvolvePart4()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(2);
        }
        public void EvolvePart5()
        {
            Simulation.Areas[_indexOfArea].RegroupYourMenToOtherFront(3);
        }
    }
}
