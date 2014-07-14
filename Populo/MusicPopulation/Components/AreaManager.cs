using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MusicPopulation
{
    public class AreaManager : IEnumerator, IEnumerable
    {
        private int _mX, _mY;
        private int _width, _height;
        private int _position = -1;
        private Tuple<int, int>[] _areaRandOrder;

        private static Semaphore _semaphore = new Semaphore(1, 1);

        public AreaManager(int x, int y, int w, int h)
        {
            
            _mX = x;
            _mY = y;

            _width = w;
            _height = h;

            int[] array = RandomGenerator.RandomPermutation(w * h, w * h);
            _areaRandOrder = new Tuple<int, int>[w*h];


            for (int ind = 0; ind < array.Length; ind++ )
            {
                int i = array[ind] % _width;
                int j = array[ind] / _height;

                _areaRandOrder[ind] = new Tuple<int, int>(i + x, j + y);
            }
        }
        public int PopulationGrowth
        {
            get
            {
                int res = 0;
                foreach (Member m in this)
                {
                    if (m != null)
                        res++;
                }
                return res;
            }
        }

        #region Algorithm Steps
        public void KillWeaksWhoDoesNotServeTheEmperorWell() // Wymieranie najgorszych osobników
        {
            List<Tuple<int, int, int>> values = new List<Tuple<int, int, int>>();

            for (int i = _mX; i < _mX + _width; i++)
            {
                for (int j = _mY; j < _mY + _height; j++)
                {
                    Member m = Simulation.SimulationBoard[i, j];
                    if (m != null)
                        values.Add(new Tuple<int, int, int>(i, j, m.Rank()));
                }
            }

            values.Sort((a, b) => a.Item3 < b.Item3 ? -1 : a.Item3==b.Item3 ? 0 : 1);
            int deaths = (int)((double)SimulationParameters.PercentDeath / 100 * (double)values.Count);
            
            int done = 0;
            int all = values.Count;

            for (int i = 0; i < deaths; i++)
            {
                Simulation.SimulationBoard[values[i].Item1, values[i].Item2] = null;
                done++;
            }
            Debug.WriteLine("Kill weaks: " + done + "/" + all );
        }
        public void MutateWeaksSoTheyCanServeEmperorBetter() // Mutacje
        {
            int done = 0;
            int all = 0;
            foreach (var pos in _areaRandOrder)
            {
                Member m = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                if (m != null)
                {
                    m.Mutate();
                    done++;
                }
                all++;
            }
            Debug.WriteLine("Mutate: " + done + "/" + all);
        }
        public void ReproduceMenToHaveMoreServantsOfTheEmperor()
        {
            int n = PopulationGrowth, n_max = _width * _height;
            int reproduced = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x1 = _mX + 4 * i;
                    int y1 = _mY + 4 * j;

                    int x2 = x1 + 3;
                    int y2 = y1 + 3;

                    var best_pos = Simulation.SimulationBoard.GetBestInArea(x1, y1, x2, y2);

                    if (best_pos == null)
                        continue;

                    int x, y = y1;
                    while (y <= y2)
                    {
                        x = x1;
                        while (x <= x2)
                        {
                            var member = Simulation.SimulationBoard[x, y];
                            double probability = 1-(double)n / n_max * SimulationParameters.Alfa(0);

                            if (member == null && RandomGenerator.ChanceProbability(probability))
                            {
                                Simulation.SimulationBoard[x, y] = Simulation.SimulationBoard[best_pos.Item1, best_pos.Item2];
                                reproduced++;
                            }
                            x++;
                        }
                        y++;
                    }
                }
            }
            Debug.WriteLine("Reproduced: " + reproduced);
        }
        public void InfluenceMenWithSongsGlorifyingEmperor()
        {
            int influenced = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int x1 = _mX + 4 * i;
                    int y1 = _mY + 4 * j;

                    int x2 = x1 + 3;
                    int y2 = y1 + 3;

                    var best_pos = Simulation.SimulationBoard.GetBestInArea(x1, y1, x2, y2);

                    if(best_pos==null)
                        continue;
                    var best_mem = Simulation.SimulationBoard[best_pos.Item1, best_pos.Item2];

                    int x, y = y1;
                    while (y <= y2)
                    {
                        x = x1;
                        while (x <= x2)
                        {
                            var member = Simulation.SimulationBoard[x, y];
                            if (member != null)
                            {
                                member.Influence(best_mem);
                                influenced++;
                            }
                            x++;
                        }
                        y++;
                    }
                }
            }
            Debug.WriteLine("Influenced: " + influenced);
        }
        public void MoveYourMenSergant()
        {
            int done = 0;
            int all = 0;
            foreach (var pos in _areaRandOrder)
            {
                Member m = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                if (m != null)
                {
                    int dir = RandomGenerator.RandomGen.Next() % 4;
                    int steps = RandomGenerator.RandomGen.Next() % SimulationParameters.MaxSteps + 1;

                    int new_x, new_y;
                    switch (dir)
                    {
                        case 0: // go north
                            new_x = pos.Item1;
                            new_y = pos.Item2 - steps;
                            break;
                        case 1: // go south
                            new_x = pos.Item1;
                            new_y = pos.Item2 + steps;
                            break;
                        case 2: // go west
                            new_x = pos.Item1 - steps;
                            new_y = pos.Item2;
                            break;
                        default: // go east
                            new_x = pos.Item1 + steps;
                            new_y = pos.Item2;
                            break;
                    }

                    if (Simulation.SimulationBoard.IsLegal(new_x, new_y) && Simulation.SimulationBoard[new_x, new_y] == null)
                    {
                        Simulation.SimulationBoard[new_x, new_y] = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                        done++;
                    }

                    all++;
                }
            }

            Debug.WriteLine("Move: " + done + "/" + all);
        }
        #endregion

        #region IEnumerable, IEnumerator interface methods
        public IEnumerator GetEnumerator()
        {
            _position = -1;
            return (IEnumerator)this;
        }
        public object Current
        {
            get 
            { 
                var pos = _areaRandOrder[_position];
                return Simulation.SimulationBoard[pos.Item1, pos.Item2];
            }
        }
        public bool MoveNext()
        {
            _position++;
            return (_position < _width * _height);
        }
        public void Reset()
        {
            _position = -1;
        }
        #endregion
    }
}
