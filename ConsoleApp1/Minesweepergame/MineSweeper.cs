using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Minesweepergame
{
    class MineSweeper
    {
        spot[,] _Map;
        place[,] _Places;
        byte _NumOfBombs;
        byte _size;

        public enum place
        {
            capped = 'x',
            cover =  '?',//'⊞',
            empty = '*',
            one = '1',
            two = '2',
            three = '3',
            four = '4',
            five = '5',
            six = '6',
            seven = '7',
            bomb = '!'

        }

        public enum spot
        {
            free = 0x0000,
            one = 0x0001,
            two = 0x0002,
            three = 0x0003,
            four = 0x0004,
            five = 0x0005,
            six = 0x0006,
            bomb = 0x0007,
            cap = 0x0008,
            error = 0x0009
        }

        public MineSweeper(byte Size, byte BombCount)
        {
            _size = Size;
            _Map = new spot[_size, _size];
            _Places = new place[_size,_size];
            _NumOfBombs = BombCount;
            int limit = _size - 5;
            int i = 0;
            for (; i < limit; i += 5)
                for (int j = 0; j < _size; j += 1)
                {
                    _Map[i, j] = spot.free;
                    _Map[i + 1, j] = spot.free;
                    _Map[i + 2, j] = spot.free;
                    _Map[i + 3, j] = spot.free;
                    _Map[i + 4, j] = spot.free;
                    _Places[i,j] = place.cover;
                    _Places[i + 1,j] = place.cover;
                    _Places[i + 2,j] = place.cover;
                    _Places[i + 3,j] = place.cover;
                    _Places[i + 4,j] = place.cover;
                }
            for (; i < _size; i++)
                for (int j = 0; j < _size; j += 1)
                {
                    _Map[i, j] = spot.free;
                    _Places[i,j] = place.cover;
                }
            _SetBombs();
            _SetLocations();
        }

        public int GetMapSize() => _size;            

        public bool PickSpot(int x, int y)
        {
            if(x != -1)
                if(_Map[x,y] == spot.bomb)
                {
                    return false;
                }
            return true;
        }

        public void reveal (int d1, int d2){
            throw NotImplementedException;
        }

        private void _SetLocations(){
            int[,] directions = new int[8,2] {{-1,-1},{-1,0},{-1,1},{0,-1},{0,1},{1,-1},{1,0},{1,1}};
            int number = 0;
            int iteration =0;
            int size = Convert.ToInt32(_size);
            for(int i = 0; i < _size; i++)
                for(int j = 0; j < _size; j++){
                    if(_Map[i,j] == spot.free)
                        _Map[i,j] = _determine(i,j);
                    iteration = 0;
                    number = 0;
                }
            
            bool _inRange(int x, int y){
                if( 0 <= x && x < size && 0 <= y && y < size)
                    return true;
                return false;
            }
        
            spot _determine(int d1, int d2){
                if( _inRange(d1 + directions[iteration,0],d2 + directions[iteration,1]) &&
                    _Map[d1 + directions[iteration,0],d2 + directions[iteration,1]] == spot.bomb){
                    number++;
                }
                if(iteration == 7)
                    switch(number){
                        case 0:
                            return spot.free;
                        case 1:
                            return spot.one;
                        case 2:
                            return spot.two;
                        case 3:
                            return spot.three;
                        case 4:
                            return spot.four;
                        case 5:
                            return spot.five;
                        case 6:
                            return spot.six;
                        default:
                            return spot.error;
                    }
                iteration++;
                return _determine(d1, d2);
                 
            }
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
                    _Map[x, y] = spot.bomb;
                    PlacedBombs++;
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
                    @string.Append($"[{i},{j}] {_Map[i, j]} ");
                }
                @string.Append("\r\n");
            }
            return @string.ToString();
        }

        public string ToStringCover(){
            StringBuilder @string = new StringBuilder();
            for (int i=0; i < _size; i++){
                for (int j = 0; j < _size; j++){
                @string.Append($"{(char) _Places[i,j]} ");
                }
                @string.Append("\r\n");
            }
            return @string.ToString();
        }
    }
}
