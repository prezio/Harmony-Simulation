using System;
using System.Diagnostics;
using System.Threading;

namespace MusicPopulation
{
    public class AreaThread
    {
        private int _indexOfArea;
        private ManualResetEvent _doneEvent;

        public AreaThread(int index, ManualResetEvent doneEvent)
        {
            _indexOfArea = index;
            _doneEvent = doneEvent;
        }
        public void Evolve(Object threadContext)
        {
            Debug.WriteLine("Thread {0} started...", _indexOfArea);

            Simulation.Areas[_indexOfArea].KillWeaksWhoDoesNotServeTheEmperorWell();
            Simulation.Areas[_indexOfArea].ReproduceMenToHaveMoreServantsOfTheEmperor();
            Simulation.Areas[_indexOfArea].MutateWeaksSoTheyCanServeEmperorBetter();
            Simulation.Areas[_indexOfArea].InfluenceMenWithSongsGlorifyingEmperor();

            Debug.WriteLine("Thread {0} calculated...", _indexOfArea);

            if (_doneEvent != null)
            {
                _doneEvent.Set();
            }
        }
    }
}
