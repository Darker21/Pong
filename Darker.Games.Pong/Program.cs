using Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darker.Games.Pong
{
    class Program
    {

        static void Main(string[] args)
        {
            // Display Instructions
            Console.WriteLine("Player 1: Use the W, S keys to move your padel.");
            Console.WriteLine("Player 2: Use the Up, Down Arrow keys to move your padel.");
            Console.WriteLine("Press Any Key To Play.");
            Console.ReadKey();

            // Instantiate and play new game
            Game g = new Game();

            Console.ReadKey();
        }
    }
}
