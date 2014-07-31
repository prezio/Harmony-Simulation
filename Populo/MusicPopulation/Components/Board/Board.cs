using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MusicPopulation
{
    public class Board
    {
        private int _width = SimulationParameters.BoardWidth;
        private int _height = SimulationParameters.BoardHeight;
        private int _growth;
        private int _indexOfPhase; // index of current phase
        private int _numberOfPhases = 1;
        private void SetPhase(int indOfPh)
        {
            _indexOfPhase = indOfPh;
            Random randContext = RandomGenerator.GenerateRandomGen();
            int[] population = RandomGenerator.RandomPermutation(_height * _width, _growth, randContext);

            foreach (int k in population)
            {
                int i = k % _height;
                int j = k / _width;
                switch (_indexOfPhase)
                {
                    case 0:
                        _board[i, j] = new Member1(randContext);
                        break;
                }
            }
        }
        private Member[,] _board;

        /// <summary>
        /// Constructs new Board
        /// </summary>
        /// <param name="w"> Width of board </param>
        /// <param name="h"> Height of board </param>
        /// <param name="growth"> Initial size of population </param>
        public Board(int w, int h, int growth)
        {
            Debug.WriteLine("Create Board");
            _board = new Member[w, h];
            this._width = w;
            this._height = h;
            this._growth = growth;

            SetPhase(0);
        }
        public Member this[int i, int j]
        {
            get
            {
                if (i < 0 || j<0 || i>=_width || j >= _height)
                    return null;
                return _board[i, j];
            }
            set
            {
                _board[i, j] = value;
            }
        }
        public void ChangePhase()
        {
            SetPhase((_indexOfPhase + 1) % _numberOfPhases);
        }

        public int PopulationGrowth
        {
            get
            {
                int res = 0;
                for(int i=0; i<_width; i++)
                {
                    for(int j=0; j<_height; j++)
                    {
                        if (_board[i, j] != null)
                            res++;
                    }
                }
                return res;
            }
        }
        /// <summary>
        /// Returns whether point (x, y) is legal or not.
        /// </summary>
        public bool IsLegal(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _width && y < _height;
        }
        /// <summary>
        /// Returns best individual in rectangle determined by positions (x1, y1), (x2, y2)
        /// </summary>
        public Tuple<int, int> GetBestInArea(int x1, int y1, int x2, int y2)
        {
            int best_x = -1, best_y = -1, best_rank = Int32.MinValue;
            int i, j = y1;

            while (j <= y2)
            {
                i = x1;
                while (i <= x2)
                {
                    Member member = _board[i, j];

                    if (member != null)
                    {
                        int rank = member.Rank();

                        if (rank > best_rank)
                        {
                            best_x = i;
                            best_y = j;
                            best_rank = rank;
                        }
                    }
                    i++;
                }
                j++;
            }
            if (best_x == -1 && best_y == -1)
                return null;
            else
                return new Tuple<int, int>(best_x, best_y);
        }
    }
}
