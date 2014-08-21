using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPopulation
{
    public static class SimulationParameters
    {
        // Parameters valid for all types of members
        #region Variables which will change during simulation
        public static double Alfa = 1;
        public static int MaxSteps = 20;
        public static int PercentDeath = 10;
        #endregion
    }
}
