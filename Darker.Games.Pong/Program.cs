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
        // Store the colours the user wants
        static ConsoleColor _colorPlayer1 = ConsoleColor.White;
        static ConsoleColor _colorPlayer2 = ConsoleColor.White;
        static ConsoleColor _colorBall = ConsoleColor.White;

        static void Main(string[] args)
        {
            // Set the game properties 
            SetGameProperties();

            Console.Clear();

            // Display Instructions
            Console.WriteLine("Player 1: Use the W, S keys to move your padel.");
            Console.WriteLine("Player 2: Use the Up, Down Arrow keys to move your padel.");
            Console.WriteLine("Anytime during the game, use P to pause the game.");
            Console.WriteLine("To quit the game at anytime, use the escape key ('Esc' at top left of keyboard)");
            Console.WriteLine("Press Any Key To Play.");

            // Instantiate and play new game
            Game g = new Game(false);
            g.Player1.Color = _colorPlayer1;
            g.Player2.Color = _colorPlayer2;
            g.Ball.Color = _colorBall;

            Console.ReadKey();
            g.Play();

            Console.ReadKey();
        }

        private static void SetGameProperties()
        {
            List<string> asConsoleColours = new List<string>();
            foreach (ConsoleColor c in Enum.GetValues(typeof(ConsoleColor)))
            {
                asConsoleColours.Add(c.ToString());
            }

            Console.WriteLine("Allowed Colours:");
            foreach (string sColor in asConsoleColours)
            {
                Console.WriteLine(sColor);
            }

            Console.WriteLine();

            Console.WriteLine("Enter a color for Player1 (Default: White):");
            string sColour = Console.ReadLine();

            ParseColor(sColour, out _colorPlayer1);

            Console.WriteLine();

            Console.WriteLine("Enter a color for Player2 (Default: White):");
            sColour = Console.ReadLine();

            ParseColor(sColour, out _colorPlayer2);

            Console.WriteLine();

            Console.WriteLine("Enter a color for the ball (Default: White):");
            sColour = Console.ReadLine();

            ParseColor(sColour, out _colorBall);
        }

        private static void ParseColor(string sColor, out ConsoleColor col)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(sColor))
                {
                    col = ConsoleColor.White;
                    return;
                }
                else if (!Enum.TryParse(sColor, true, out col))
                {
                    Console.WriteLine("Invalid color! Please try again. Enter a color:");
                    sColor = Console.ReadLine();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
