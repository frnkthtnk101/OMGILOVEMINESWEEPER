using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Minesweepergame;

namespace ConsoleApp1
{
    class Program
    {
        enum GameState {
            going,
            done
        };
        static void Main(string[] args)
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            Regex regex = new Regex(@"([\d]{1,3})");
            Regex regex_limiter = new Regex(@"^([\d]{1,3}[\ \,][\d]{1,3})$");
            GameState gamestate = GameState.going;
            Minesweepergame.MineSweeper game = new Minesweepergame.MineSweeper(10,5);
            int size = game.GetMapSize();
            Console.WriteLine(game.ToStringCover());
            int[] cords = new int[2];
            
            do
            {
                Console.WriteLine($"pick an y,x. it is a [{size},{size}] map");
                string input = Console.ReadLine();
                MatchCollection capture = regex.Matches(input);
                if (regex_limiter.IsMatch(input) &&
                   int.Parse(capture[0].Value) < size &&
                   int.Parse(capture[1].Value) < size )
                {
                    cords[0] = int.Parse(capture[0].Value);
                    cords[1] = int.Parse(capture[1].Value);
                    if(game.PickSpot(cords[0], cords[1]))
                    {
                       gamestate = GameState.going;
                    }
                    else
                    { 
                        gamestate = GameState.done;
                    }
                    game.reveal(cords[0], cords[1]);
                }
                else
                {
                    continue;
                }
            } while (gamestate == GameState.going);
            Console.WriteLine("Game over!");
            Console.ReadLine();
        }
    }
}
