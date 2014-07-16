﻿using System;
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
            Random randContext = RandomGenerator.GenerateRandomGen();

            var population = RandomGenerator.RandomPermutation(_height * _width, growth, randContext);
            foreach (int k in population)
            {
                int i = k % _height;
                int j = k / _width;
                _board[i, j] = new Member(randContext);
            }
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
                    var member = _board[i, j];
                    if (member != null && member.Rank() > best_rank)
                    {
                        best_x = i;
                        best_y = j;
                        best_rank = member.Rank();
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
