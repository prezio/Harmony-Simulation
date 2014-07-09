using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public class AreaManager : IEnumerator, IEnumerable
    {
        private int _mX, _mY;
        private int _width, _height;
        private int _position = -1;
        private Tuple<int, int>[] _areaRandOrder;

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
            int done = 0;
            int all = 0;

            foreach(Member m in this)
            {
                if ( m != null && m.Rank() < SimulationParameters.deathLimit)
                {
                    Simulation.SimulationBoard[_areaRandOrder[_position].Item1, _areaRandOrder[_position].Item2] = null;
                    done++;
                }
                all++;
            }
            if(done!=0)
                Console.WriteLine("Kill weaks: " + done + "/" + all);
        }
        public void MutateWeaksSoTheyCanServeEmperorBetter() // Mutacje
        {
            int done = 0;
            int all = 0;
            foreach (var pos in _areaRandOrder)
            {
                var tile = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                if (tile != null)
                {
                    tile.Mutate();
                    done++;
                }
                all++;
            }
            if(done!=0)
                Console.WriteLine("Mutate: " + done + "/" + all);
        }
        public void ReproduceMenToHaveMoreServantsOfTheEmperor()
        {
            int n = PopulationGrowth, n_max = _width * _height;
            int r = SimulationParameters.radiusReproduce;

            int all = 0;
            int done = 0;

            foreach (var pos in _areaRandOrder)
            {
                var member = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                if (member == null && RandomGenerator.ChanceProbability((double)n / n_max * SimulationParameters.alfa(0)))
                {
                    var best_pos = Simulation.GetBestInArea(pos.Item1 - r, pos.Item2 - r, pos.Item1 + r, pos.Item2 + r);
                    if (best_pos != null)
                        Simulation.SimulationBoard[pos.Item1, pos.Item2] = Simulation.SimulationBoard[best_pos.Item1, best_pos.Item2];
                    done++;
                }
                all++;
            }
            if(done!=0)
                Console.WriteLine("Reproduce: " + done + "/" + all);
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

                    var best_pos = Simulation.GetBestInArea(x1, y1, x2, y2);

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
                            if (member != null && (x != best_pos.Item1 || y != best_pos.Item2))
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
            Console.WriteLine("Influenced: " + influenced);
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
