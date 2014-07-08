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
        private int position;
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
        public void Mutate()
        {
            foreach (Member m in _board)
            {
                m.Mutate();
            }
        }
        public void Serialize()
        {
            throw new NotImplementedException();
        }
        public object Current
        {
            get 
            {
                Tuple<int, int> cur = RandomGenerator.RandomOrder[position];
                return _board[cur.Item1, cur.Item2];
            }
        }
        public bool MoveNext()
        {
            position++;
            return (position < RandomGenerator.RandomOrder.Length);
        }
        public void Reset()
        {
            position = 0;
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }
    }
}
