using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    /// <summary>
    /// This is a just a very basic minesweeper game created While I was waiting
    /// to go on a trip. It took a couple of days to complete and I have no
    /// intents of continuing any more development of this.
    /// Features will be used of an online version of this.
    /// </summary>
    class Program
    {
        /// <summary>
        /// used to determine the game state
        /// </summary>
        enum GameState {
            going,
            done
        };

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            // make sure that a number is really a number
            Regex regex = new Regex(@"([\d]{1,3})");
            // make sure that a input is really a set of x,y cords
            Regex regex_limiter = new Regex(@"^([\d]{1,3}[\ \,][\d]{1,3})$");
            Minesweepergame.MineSweeper game = new Minesweepergame.MineSweeper(10,5);
            int size = game.GetMapSize();
            int[] cords = new int[2];
            string input = "";
            do
            {
                Console.Clear();
                Console.WriteLine(game.ToStringCover());
                Console.WriteLine($"pick an y,x. it is a [{size},{size}] map");
                input = Console.ReadLine();
                MatchCollection capture = regex.Matches(input);
                if (input != "stop")
                {
                    if (regex_limiter.IsMatch(input) &&
                       int.Parse(capture[0].Value) < size &&
                       int.Parse(capture[1].Value) < size)
                    {
                        cords[0] = int.Parse(capture[0].Value);
                        cords[1] = int.Parse(capture[1].Value);
                        game.reveal(cords[0], cords[1]);
                    }
                    else
                    {
                        continue;
                    }
                }else{
                    Console.WriteLine("thank you for playing");
                    return;
                }
                } while (game.GetState()) ;
                Console.WriteLine("Game over!");
                Console.ReadLine();

        }
    }
}
