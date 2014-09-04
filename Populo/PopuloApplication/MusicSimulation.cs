using MusicPopulation;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace PopuloApplication
{
    /// <summary>
    /// Static class responsible for Simulation
    /// </summary>
    public static class MusicSimulation
    {
        private static Timer _timer;
        private static MainWindow _window;
        private static int _iEvolveDuration = 300;

        /// <summary>
        /// Static method responsible for starting simulation
        /// </summary>
        /// <param name="context">Window context of simulation</param>
        public static void Start(MainWindow context)
        {
            _window = context;
            _timer = new Timer(500);

            Simulation.DoSimulation(_iEvolveDuration, new Simulation.MelodyNeededDelegate(() => { return true; }), new Simulation.RefreshParametersDelegate(_window.SaveParameters));

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }
        /// <summary>
        /// Static method responsible for stopping simulation
        /// </summary>
        public static void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                //Melody.StopPlaying();
                Simulation.StopSimulation();
            }
        }

        /// <summary>
        /// Event responsible for Simulation Iteration
        /// </summary>
        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _window.RefreshSimulationControls();

            if (Simulation.SimulationStatus != TaskStatus.Running && Melody.IsPlaying == false)
            {
                Melody.StartPlaying();
                Simulation.DoSimulation(_iEvolveDuration, new Simulation.MelodyNeededDelegate(Melody.Unneeded), new Simulation.RefreshParametersDelegate(_window.SaveParameters));
            }
        }
    }
}
