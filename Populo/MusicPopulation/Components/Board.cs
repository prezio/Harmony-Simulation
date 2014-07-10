﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public class Board
    {
        private int _width = SimulationParameters.boardWidth;
        private int _height = SimulationParameters.boardHeight;
        private Member[,] _board;

        public Board(int w, int h, int growth)
        {
            _board = new Member[w, h];
            this._width = w;
            this._height = h;

            var population = RandomGenerator.RandomPermutation(_height * _width, growth);
            foreach (int k in population)
            {
                int i = k % _height;
                int j = k / _width;
                _board[i, j] = new Member();
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
        public void Serialize()
        {
            throw new NotImplementedException();
        }
    }
}