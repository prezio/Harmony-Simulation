using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MusicPopulation
{
    public class Board : IEnumerator, IEnumerable
    {
        private int _width = SimulationParameters.boardWidth;
        private int _height = SimulationParameters.boardHeight;
        private int _position;
        private Member[,] _board;

        public Board()
        {
            _board = new Member[_height, _width];
            var population = RandomGenerator.RandomPermutation(_height * _width, SimulationParameters.populationGrowth);
            foreach (int k in population)
            {
                int i = k / _height;
                int j = k % _width;
                _board[i, j] = new Member();
            }
        }
        public Member this[int i, int j]
        {
            get
            {
                return _board[i, j];
            }
            set
            {
                _board[i, j] = value;
            }
        }
        public object Current
        {
            get 
            {
                Tuple<int, int> cur = RandomGenerator.RandomOrder[_position];
                return _board[cur.Item1, cur.Item2];
            }
        }
        public bool MoveNext()
        {
            _position++;
            return (_position < RandomGenerator.RandomOrder.Length);
        }
        public void Reset()
        {
            _position = 0;
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
        public void Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
