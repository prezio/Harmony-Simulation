using MusicPopulation;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace PopuloApplication
{
    public static class MusicSimulation
    {
        private static Timer _timer;
        private static MainWindow _window;
        private static int _iEvolveDuration = 100;

        public static void Start(MainWindow context)
        {
            _window = context;
            _timer = new Timer(500);

            Simulation.StartSimulation(_iEvolveDuration);

            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }
        public static void Stop()
        {
            _timer.Stop();
            Melody.StopPlaying();
            Simulation.StopSimulation();
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _window.RefreshSimulationControls();

            if (Simulation.SimulationStatus != TaskStatus.Running && Melody.IsPlaying == false)
            {
                Melody.StartPlaying();
                _window.SaveParameters();
                Simulation.StartSimulation(_iEvolveDuration);
            }
        }
    }
}
