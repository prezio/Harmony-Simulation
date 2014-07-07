using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public class Board
    {
        private int _width = SimulationParameters.boardWidth;
        private int _height = SimulationParameters.boardHeight;
        private Member[,] _board;

        public Board()
        {
            _board = new Member[_height, _width];
        }
        public Member this[int i, int j]
        {
            get
            {
                return _board[i, j];
            }
        }
        public void NextStep()
        {

        }
        public void Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
