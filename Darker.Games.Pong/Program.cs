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
        static int _iGameSpeed = 100;
        static int _iMaxScore = 11;

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
            g.GameSpeed = _iGameSpeed;
            g.MaxScore = _iMaxScore;

            Console.ReadKey();
            g.Play();

            Console.ReadKey();
        }

        /// <summary>
        /// Allows the user to customize the properties of the game (e.g. colours, score and gamespeed)
        /// </summary>
        private static void SetGameProperties()
        {
            // construct a list of possible colours
            List<string> asConsoleColours = new List<string>();
            foreach (ConsoleColor c in Enum.GetValues(typeof(ConsoleColor)))
            {
                asConsoleColours.Add(c.ToString());
            }

            // Write the list of possible colours to console
            Console.WriteLine("Allowed Colours:");
            foreach (string sColor in asConsoleColours)
            {
                Console.WriteLine(sColor);
            }

            // Empty line for presentation reasons
            Console.WriteLine();

            // Ask the user to enter a colour for Player1's paddle
            Console.WriteLine("Enter a color for Player1 (Default: White):");
            string sColour = Console.ReadLine();

            // parse the colour and assign it to correct variable
            ParseColor(sColour, out _colorPlayer1);

            Console.WriteLine();

            Console.WriteLine("Enter a color for Player2 (Default: White):");
            sColour = Console.ReadLine();

            ParseColor(sColour, out _colorPlayer2);

            Console.WriteLine();

            Console.WriteLine("Enter a color for the ball (Default: White):");
            sColour = Console.ReadLine();

            ParseColor(sColour, out _colorBall);

            Console.WriteLine();

            // Ask the user for a desired gamespeed
            Console.WriteLine("Please enter a gamespeed in milliseconds (Default: 100):");
            string sSpeed = Console.ReadLine();

            // user has entered an incorrect int - ask them to reenter a valid speed
            while (!int.TryParse(sSpeed, out _iGameSpeed))
            {
                Console.WriteLine("Invalid speed! Please try again. Enter a speed:");
                sSpeed = Console.ReadLine();
            }

            Console.WriteLine();

            Console.WriteLine("Please enter a max score (Default: 11):");
            string sScore = Console.ReadLine();

            while (!int.TryParse(sScore, out _iMaxScore))
            {
                Console.WriteLine("Invalid score! Please try again. Enter a max score:");
                sScore = Console.ReadLine();
            }
        }

        /// <summary>
        /// Parse the string representation of the color to be a ConsoleColour
        /// </summary>
        /// <param name="sColor">The string representation of the colour</param>
        /// <param name="col">The ConsoleColour output</param>
        private static void ParseColor(string sColor, out ConsoleColor col)
        {
            while (true)
            {
                // if empty return default (needs tweaking to be correct but cba at this point)
                if (string.IsNullOrEmpty(sColor))
                {
                    col = ConsoleColor.White;
                    return;
                }
                // if the string cannot be parsed to a console colour - ask the user to reenter a colour
                else if (!Enum.TryParse(sColor, true, out col))
                {
                    Console.WriteLine("Invalid color! Please try again. Enter a color:");
                    sColor = Console.ReadLine();
                }
                // already assigned the colour in the previous else if statement so return
                else
                {
                    return;
                }
            }
        }
    }
}
