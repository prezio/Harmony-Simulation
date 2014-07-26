using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MusicPopulation
{
    public static class Logger
    {
        private static StreamWriter _logger = new StreamWriter("log.txt");
        private static readonly object _syncLog = new object();
        private static string GenBoardDescription()
        {
            StringBuilder res = new StringBuilder();
            res.Append(string.Format("Percent death: {0}\n", SimulationParameters.PercentDeath));
            res.Append(string.Format("Modify amount: {0}, {1}, {2}\n", SimulationParameters.ModifyAmount[0], SimulationParameters.ModifyAmount[1], SimulationParameters.ModifyAmount[2]));
            res.Append(string.Format("Influence amount: {0}, {1}, {2}\n", SimulationParameters.InfluenceAmount[0], SimulationParameters.InfluenceAmount[1], SimulationParameters.InfluenceAmount[2]));
            res.Append(string.Format("Growth chance: {0}\n", SimulationParameters.GrowthChance));
            res.Append(string.Format("Shrink chance: {0}\n", SimulationParameters.ShrinkChance));
            res.Append(string.Format("Population growth: {0}\n", Simulation.SimulationBoard.PopulationGrowth));
            return res.ToString();
        }

        public static string GenBoardDescription(int numberOfTries)
        {
            StringBuilder res = new StringBuilder();
            res.Append(GenBoardDescription());
            res.Append(string.Format("Simulation steps: {0}\n", numberOfTries));
            res.Append("========================\n");
            return res.ToString();
        }
        public static string GenAreaDescription()
        {
            List<Member> sounds = Simulation.SimulationBoardState;

            StringBuilder res = new StringBuilder();
            int area = 1;
            foreach (Member sound in sounds)
            {
                res.Append(string.Format("Area number {0}\n", area));

                int number = sound.NumberOfNotes;
                int[,] array = sound.Notes;

                for (int i = 0; i < number; i++)
                {
                    res.Append(string.Format("{0}; {1}; {2}\n", array[i, 0], array[i, 1], array[i, 2]));
                }
                res.Append(string.Format("End of area {0}\n", area));
                area++;
            }
            return res.ToString();
        }
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
