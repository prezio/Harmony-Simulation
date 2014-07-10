using System;

namespace MusicPopulation
{
    public class AreaThread
    {
        private AreaManager _area = null;

        public AreaThread(AreaManager area)
        {
            _area = area;
        }
        public void Evolve()
        {
            _area.KillWeaksWhoDoesNotServeTheEmperorWell();
            _area.ReproduceMenToHaveMoreServantsOfTheEmperor();
            _area.MutateWeaksSoTheyCanServeEmperorBetter();
            _area.InfluenceMenWithSongsGlorifyingEmperor();
        }
    }
}
