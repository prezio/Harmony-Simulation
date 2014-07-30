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
        private Random _randContext;
        private Member _championOfArea = null;

        // Move Synchronization containers
        private List<Tuple<int, int, int, int>> _listLeftMove;
        private List<Tuple<int, int, int, int>> _listRightMove;
        private List<Tuple<int, int, int, int>> _listDownMove;
        private List<Tuple<int, int, int, int>> _listUpMove;
        //================================

        private bool IsInsideArea(int x, int y)
        {
            return x >= _mX && x < _mX + _width && y >= _mY && y < _mY + _height;
        }

        public AreaManager(int x, int y, int w, int h)
        {
            _mX = x;
            _mY = y;

            _width = w;
            _height = h;

            _randContext = RandomGenerator.GenerateRandomGen();

            int[] array = RandomGenerator.RandomPermutation(w * h, w * h, _randContext);
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
        public Member ChampionOfArea
        {
            get
            {
                return _championOfArea;
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
        public void SelectChampionWhoCanBecomeCommissar()
        {
            var pos = Simulation.SimulationBoard.GetBestInArea(_mX, _mY, _mX + _width - 1, _mY + _height - 1);
            Member best = Simulation.SimulationBoard[pos.Item1, pos.Item2];
            _championOfArea = new Member1(best);
        }
        public void MutateWeaksSoTheyCanServeEmperorBetter() // Mutacje
        {
            int done = 0;
            int all = 0;
            foreach (Member m in this)
            {
                if (m != null)
                {
                    m.Mutate(_randContext);
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
                            double probability = 1-(double)n / n_max * SimulationParameters.Alfa;

                            if (member == null && RandomGenerator.ChanceProbability(probability, _randContext))
                            {
                                Simulation.SimulationBoard[x, y] = new Member1(Simulation.SimulationBoard[best_pos.Item1, best_pos.Item2]);
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

                    if (best_mem.NumberOfNotes <= 0)
                    {
                        Console.WriteLine("line");
                        continue;
                    }

                    int x, y = y1;
                    while (y <= y2)
                    {
                        x = x1;
                        while (x <= x2)
                        {
                            var member = Simulation.SimulationBoard[x, y];
                            if (member != null && (x != best_pos.Item1 || y != best_pos.Item2))
                            {
                                member.Influence(best_mem, _randContext);
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

            _listDownMove = new List<Tuple<int, int, int, int>>();
            _listLeftMove = new List<Tuple<int, int, int, int>>();
            _listRightMove = new List<Tuple<int, int, int, int>>();
            _listUpMove = new List<Tuple<int, int, int, int>>();

            foreach (var pos in _areaRandOrder)
            {
                Member m = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                if (m != null)
                {
                    int dir = _randContext.Next() % 4;
                    int steps = _randContext.Next() % SimulationParameters.MaxSteps + 1;

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

                    if (IsInsideArea(new_x, new_y))
                    {
                        if (Simulation.SimulationBoard[new_x, new_y] == null)
                        {
                            Simulation.SimulationBoard[new_x, new_y] = Simulation.SimulationBoard[pos.Item1, pos.Item2];
                            Simulation.SimulationBoard[pos.Item1, pos.Item2] = null;
                        }
                        done++;
                    }
                    else
                    {
                        if (Simulation.SimulationBoard.IsLegal(new_x, new_y) && Simulation.SimulationBoard[new_x, new_y] == null)
                        {
                            Tuple<int, int, int, int> cmdMove = new Tuple<int,int,int,int>(pos.Item1, pos.Item2, new_x, new_y);

                            if (new_x >= _mX + _width)
                                _listRightMove.Add(cmdMove);
                            else if (new_x < _mX)
                                _listLeftMove.Add(cmdMove);
                            else if (new_y >= _mY + _height)
                                _listDownMove.Add(cmdMove);
                            else if (new_y < _mY)
                                _listUpMove.Add(cmdMove);
                        }
                    }

                    all++;
                }
            }

            Debug.WriteLine("Move Inside: " + done + "/" + all);
        }
        /// <summary>
        /// Moves Individuals in given direction
        /// </summary>
        /// <param name="dir">0 - north, 1 - south, 2 - west, 3 - east</param>
        public void RegroupYourMenToOtherFront(int dir)
        {
            List<Tuple<int, int, int, int>> listMove = null;

            if (dir == 0)
                listMove = _listUpMove;
            else if (dir == 1)
                listMove = _listDownMove;
            else if (dir == 2)
                listMove = _listLeftMove;
            else if (dir == 3)
                listMove = _listRightMove;

            foreach (var move in listMove)
            {
                int sX = move.Item1, sY = move.Item2;
                int tX = move.Item3, tY = move.Item4;

                if (Simulation.SimulationBoard[tX, tY] == null)
                {
                    Simulation.SimulationBoard[tX, tY] = Simulation.SimulationBoard[sX, sY];
                    Simulation.SimulationBoard[sX, sY] = null;
                }
            }
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
