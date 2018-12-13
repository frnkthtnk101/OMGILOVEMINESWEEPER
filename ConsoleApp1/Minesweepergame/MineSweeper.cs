using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Minesweepergame
{
    class MineSweeper
    {
        byte[,] _Map;
        byte _NumOfBombs;
        byte _size;

        public MineSweeper(byte Size, byte BombCount)
        {
            _size = Size;
            _Map = new byte[_size, _size];
            _NumOfBombs = BombCount;
            int limit = _size - 5;
            int i = 0;
            for (; i < limit; i += 5)
                for (int j = 0; j < _size; j += 1)
                {
                    _Map[i, j] = 0;
                    _Map[i + 1, j] = 0;
                    _Map[i + 2, j] = 0;
                    _Map[i + 3, j] = 0;
                    _Map[i + 4, j] = 0;
                }
            for (; i < _size; i++)
                for (int j = 0; j < _size; j += 1)
                {
                    _Map[i, j] = 0;
                }
            _SetBombs();
        }

        public bool PickSpot(int x, int y)
        {
            if(x != -1)
                if(_Map[y,x] == 1)
                {
                    return false;
                }
            return true;
        }

        private void _SetBombs()
        {
            Random random = new Random();
            byte PlacedBombs = 0;
            do
            {
                int x = random.Next(0, _size);
                int y = random.Next(0, _size);
                if(_Map[y,x] == 0)
                {
                    _Map[x, y] = 1;
                    _NumOfBombs++;
                }
            } while (PlacedBombs < _NumOfBombs);
        }

        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j += 1)
                {
                    @string.Append($"{_Map[i, j]}");
                }
                @string.Append("\r\n");
            }
            return @string.ToString();
        }
    }
}
