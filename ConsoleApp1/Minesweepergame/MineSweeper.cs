using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Minesweepergame
{
    /// <summary>
    /// the class of the minesweeper game. It holds everything.
    /// There are two maps. One is the map the player does not touch and
    /// a map the player touches. The user interacts with _Places. the class
    /// checks if a coordinate from _Map is whatever and determines what happens
    /// next.
    /// </summary>
    class MineSweeper
    {
        spot[,] _Map;
        place[,] _Places;
        bool _running;
        byte _NumOfBombs;
        byte _size;

        public enum place
        {
            capped = 'x',
            cover = '?',//'⊞',
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
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConsoleApp1.Minesweepergame.MineSweeper"/> class.
        /// </summary>
        /// <param name="Size">Size.</param>
        /// <param name="BombCount">Bomb count.</param>
        public MineSweeper(byte Size, byte BombCount)
        {
            _size = Size;
            _Map = new spot[_size, _size];
            _Places = new place[_size, _size];
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
                    _Places[i, j] = place.cover;
                    _Places[i + 1, j] = place.cover;
                    _Places[i + 2, j] = place.cover;
                    _Places[i + 3, j] = place.cover;
                    _Places[i + 4, j] = place.cover;
                }
            for (; i < _size; i++)
                for (int j = 0; j < _size; j += 1)
                {
                    _Map[i, j] = spot.free;
                    _Places[i, j] = place.cover;
                }
            _SetBombs();
            _SetLocations();
        }

        public bool GetState() => _running;

        public int GetMapSize() => _size;
        /// <summary>
        /// Picks the spot. determines if the spot picked has a bob or not.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void PickSpot(int x, int y)
        {
            if (x != -1)
                if (_Map[x, y] == spot.bomb)
                {
                    _running = false;
                    return;
                }
            _running = true;
            return;
        }

        /// <summary>
        /// Translate what is on the _Map to _Places.
        /// </summary>
        /// <param name="d1">D1.</param>
        /// <param name="d2">D2.</param>
        public void reveal(int d1, int d2)
        {
            PickSpot(d1, d1);
            if (_running)
            {
                switch(_Map[d1,d2]){
                    case spot.one:
                        _Places[d1, d2] = place.one;
                        break;
                    case spot.two:
                        _Places[d1, d2] = place.two;
                        break;
                    case spot.three:
                        _Places[d1, d2] = place.three;
                        break;
                    case spot.four:
                        _Places[d1, d2] = place.four;
                        break;
                    case spot.five:
                        _Places[d1, d2] = place.five;
                        break;
                    case spot.six:
                        _Places[d1, d2] = place.six;
                        break;
                    default:
                        //_Places[d1, d2] = place.empty;
                        GetEmptySpots(d1, d2);
                        break;
                }
            }
            return;
        }
        /// <summary>
        /// Gets the empty spots.
        /// </summary>
        /// <param name="d1">D1.</param>
        /// <param name="d2">D2.</param>
        private void GetEmptySpots(int d1, int d2){
            /*
             *1 1 1 1
             *1 0 0 1
             *1 0 0 1
             *1 1 1 1
             */
            int[,] directions = new int[8, 2] { { -1, -1 }, { -1, 0 }, { -1, 1 }, 
                { 0, -1 }, { 0, 1 }, { 1, -1 }, { 1, 0 }, { 1, 1 } };
            //int number = 0;
            int iteration = 0;
            int size = Convert.ToInt32(_size);
            void adjecentEmpty(int d11, int d22){
                if(_Map[d11 + directions[iteration,0],d22 + directions[iteration,1]] != spot.bomb &&
                   _inRange(d11,d2)
                  ){
                    switch(_Map[d11 + directions[iteration, 0], d22 + directions[iteration, 1]]){
                        case spot.one:
                            _Places[d11, d22] = place.one;
                            break;
                        case spot.two:
                            _Places[d11, d22] = place.two;
                            break;
                        case spot.three:
                            _Places[d11, d22] = place.three;
                            break;
                        case spot.four:
                            _Places[d11, d22] = place.four;
                            break;
                        case spot.five:
                            _Places[d11, d22] = place.five;
                            break;
                        case spot.six:
                            _Places[d11, d22] = place.six;
                            break;
                    }
                    iteration++;
                    adjecentEmpty(d11, d22);
                }
            }
            bool _inRange(int x, int y)
            {
                if (0 <= x && x < size && 0 <= y && y < size)
                    return true;
                return false;
            }
            adjecentEmpty(d1, d2);


        }

        /// <summary>
        /// sets the locations to when the map is being intialized.
        /// </summary>
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
            
            /// <summary>
            /// Determins if any pick from the class is in range.
            /// </summary>
            /// <returns><c>true</c>, if range was ined, <c>false</c> otherwise.</returns>
            /// <param name="x">The x coordinate.</param>
            /// <param name="y">The y coordinate.</param>
            bool _inRange(int x, int y){
                if( 0 <= x && x < size && 0 <= y && y < size)
                    return true;
                return false;
            }

            /// <summary>
            /// Determine the specified d1 and d2.
            /// </summary>
            /// <returns>The determine.</returns>
            /// <param name="d1">D1.</param>
            /// <param name="d2">D2.</param>
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
