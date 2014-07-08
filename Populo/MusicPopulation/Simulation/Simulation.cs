using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public static class Simulation
    {
        private static Board _board = null;

        public static Tuple<int,int> GetBestInArea(int x1, int y1, int x2, int y2)
        {
            int best_x = -1, best_y = -1, best_rank = -1;
            int i, j = y1;

            while (j <= y2)
            {
                i = x1;
                while (i <= x2)
                {
                    try
                    {
                        var member = SimulationBoard[i, j];
                        if (member != null && member.Rank() > best_rank)
                        {
                            best_x = i;
                            best_y = j;
                            best_rank = member.Rank();
                        }
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                    i++;
                }
                j++;
            }
            if (best_rank == -1)
                return null;
            else
                return new Tuple<int, int>(best_x, best_y);
        }
        public static Board SimulationBoard
        {
            get
            {
                if (_board == null)
                {
                    _board = new Board(SimulationParameters.boardWidth, SimulationParameters.boardHeight, SimulationParameters.populationGrowth);
                }
                return _board;
            }
        }
        
    }
}
