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
        static void Main(string[] args)
        {
            Regex regex = new Regex(@"([\d]{0,3})");
            Minesweepergame.MineSweeper game = new Minesweepergame.MineSweeper(25,5);
            Console.WriteLine(game.ToString());
            int[] cords = new int[2];
            do
            {
                Console.WriteLine("pick an x,y");
                string input = Console.ReadLine();
                if (regex.IsMatch(input))
                {
                    MatchCollection capture = regex.Matches(input);
                    cords[0] = int.Parse(capture[0].Value);
                    cords[0] = int.Parse(capture[1].Value);
                }
                else
                {
                    cords[0] = -1;
                    cords[1] = -1;
                }



            } while (game.PickSpot(cords[0], cords[1]));
            Console.WriteLine("Game over!");
            Console.ReadLine();
        }
    }
}
