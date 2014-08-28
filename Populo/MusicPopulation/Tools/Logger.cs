using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MusicPopulation
{
    /// <summary>
    /// Tool used to store some logs representing state of simulation.
    /// </summary>
    public static class Logger
    {
        private static StreamWriter _logger = new StreamWriter("log.txt");
        private static readonly object _syncLog = new object();
        private static string GenBoardDescription()
        {
            int phase = Simulation.SimulationBoard.IndexOfPhase;
            StringBuilder res = new StringBuilder();

            res.Append(string.Format("Percent death: {0}\n", SimulationParameters.PercentDeath));
            res.Append(string.Format("Population growth: {0}\n", Simulation.SimulationBoard.PopulationGrowth));

            switch(phase)
            {
                case 0:
                    res.Append(string.Format("Modify amount: {0}, {1}, {2}\n", Member1.ModifyAmount[0], Member1.ModifyAmount[1], Member1.ModifyAmount[2]));
                    res.Append(string.Format("Influence amount: {0}, {1}, {2}\n", Member1.InfluenceAmount[0], Member1.InfluenceAmount[1], Member1.InfluenceAmount[2]));
                    res.Append(string.Format("Growth chance: {0}\n", Member1.GrowthChance));
                    res.Append(string.Format("Shrink chance: {0}\n", Member1.ShrinkChance));
                    break;
            }
            return res.ToString();
        }

        /// <summary>
        /// Generates Overall simulation description, especially board parameters.
        /// </summary>
        /// <param name="numberOfTries">number of simulation iterations</param>
        /// <returns>string representing board description</returns>
        public static string GenBoardDescription(int numberOfTries)
        {
            StringBuilder res = new StringBuilder();
            res.Append(GenBoardDescription());
            res.Append(string.Format("Simulation steps: {0}\n", numberOfTries));
            res.Append("========================\n");
            return res.ToString();
        }
        /// <summary>
        /// Static method which generates all areas description:
        ///  - population growth,
        ///  - area Champions,
        ///  etc.
        /// </summary>
        /// <returns>string representing areas description</returns>
        public static string GenAreaDescription()
        {
            List<Tuple<int, int[,]>> sounds = Simulation.SimulationBoardState;

            StringBuilder res = new StringBuilder();
            int area = 1;
            foreach (var sound in sounds)
            {
                res.Append(string.Format("Area number {0}\n", area));

                if (sound != null)
                {
                    int number = sound.Item1;
                    int[,] array = sound.Item2;

                    for (int i = 0; i < number; i++)
                    {
                        res.Append(string.Format("{0};{1};{2};{3}\n", array[i, 0], array[i, 1], array[i, 2], array[i, 3]));
                    }
                }

                res.Append(string.Format("End of area {0}\n", area));
                area++;
            }
            return res.ToString();
        }
        /// <summary>
        /// Store information about error.
        /// </summary>
        /// <param name="ex">exception</param>
        /// <param name="note">additional note</param>
        public static void AddError(Exception ex, string note)
        {
            lock (_syncLog)
            {
                _logger.WriteLine("Error occured.");
                _logger.WriteLine(GenBoardDescription());
                _logger.WriteLine("Exception description: {0}", ex.Message);
                if (note != null)
                {
                    _logger.WriteLine("Additional note: {0}", note);
                }
                _logger.WriteLine("-----------End of Error--------------");
                _logger.Flush();
            }
        }
    }
}
