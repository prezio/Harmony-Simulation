﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MusicPopulation
{
    /// <summary>
    /// Represents simulation board.
    /// </summary>
    public partial class Board
    {
        private int _indexOfPhase; // index of current phase
        private int _numberOfPhases = 4;
        private void SetPhase(int indOfPh)
        {
            _indexOfPhase = indOfPh;
            Random randContext = RandomGenerator.GenerateRandomGen();
            int[] population = RandomGenerator.RandomPermutation(BoardHeight * BoardWidth, InitPopulationGrowth, randContext);
            _board = new Member[BoardWidth, BoardHeight];

            foreach (int k in population)
            {
                int i = k % BoardHeight;
                int j = k / BoardWidth;
                switch (_indexOfPhase)
                {
                    case 0:
                        _board[i, j] = new Member1(randContext);
                        break;
                    case 1:
                        _board[i, j] = new Member2(randContext);
                        break;
                    case 2:
                        _board[i, j] = new Member3(randContext);
                        break;
                    case 3:
                        _board[i, j] = new Member4(randContext);
                        break;
                }
            }
        }
        private Member[,] _board;
        private int[,] _rankTable;

        /// <summary>
        /// Constructs new Board filled with random values
        /// and in first phase.
        /// </summary>
        public Board()
        {
            Debug.WriteLine("Create Board");
            _rankTable = new int[BoardWidth, BoardHeight];
            SetPhase(0);
        }
        /// <summary>
        /// Basic board getter, if index is out of range returns null.
        /// </summary>
        public Member this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= BoardWidth || j >= BoardHeight)
                    return null;
                return _board[i, j];
            }
            set
            {
                _board[i, j] = value;
            }
        }
        /// <summary>
        /// Method responsible for changing simulation phase.
        /// </summary>
        public void ChangePhase()
        {
            SetPhase((_indexOfPhase + 1) % _numberOfPhases);
        }

        /// <summary>
        /// Getter which returns index of phase.
        /// </summary>
        public int IndexOfPhase
        {
            get
            {
                return _indexOfPhase;
            }
        }
        /// <summary>
        /// Retrurns string representing all ranks in board.
        /// </summary>
        public string RankTableMsg
        {
            get
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < BoardWidth; i++)
                {
                    for (int j = 0; j < BoardHeight; j++)
                    {
                        result.Append(string.Format("{0};", _rankTable[i, j]));
                    }
                    result.Append("\n");
                }
                return result.ToString();
            }
        }
        /// <summary>
        /// Returns Population Growth in board.
        /// </summary>
        public int PopulationGrowth
        {
            get
            {
                int res = 0;
                for(int i=0; i<BoardWidth; i++)
                {
                    for(int j=0; j<BoardHeight; j++)
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
            return x >= 0 && y >= 0 && x < BoardWidth && y < BoardHeight;
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
                    int rank = Int32.MinValue;

                    if (member != null)
                    {
                        rank = member.Rank();

                        if (rank > best_rank)
                        {
                            best_x = i;
                            best_y = j;
                            best_rank = rank;
                        }
                    }
                    _rankTable[i, j] = rank;
                    i++;
                }
                j++;
            }
            if (best_x == -1 && best_y == -1)
                return null;
            else
                return new Tuple<int, int>(best_x, best_y);
        }
        /// <summary>
        /// Moves individual in (sX, sY) to (dX, dY).
        /// </summary>
        public void MoveIndividual(int sX, int sY, int dX, int dY)
        {
            this[dX, dY] = this[sX, sY];
            this[sX, sY] = null;
        }
        /// <summary>
        /// Copies individual in (sX,sY) to (dX, dY).
        /// </summary>
        public void CopyIndividual(int sX, int sY, int dX, int dY)
        {
            this[dX, dY] = this[sX, sY].Clone();
        }
    }
}
