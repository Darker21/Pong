using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Paddle
    {
        /// <summary>
        /// The pixels that make up the paddle graphically
        /// </summary>
        public Pixel[] Pixels;

        /// <summary>
        /// Stores the score for the paddle
        /// </summary>
        /// 
        /// <remarks>
        /// Should really be abstracted into a user/player class but they're practically the same thing in Pong
        /// </remarks>
        public int Score { get; set; }

        /// <summary>
        /// The XCoordinate of the panel
        /// </summary>
        public int XCoordinate
        {
            get
            {
                return Pixels[0].Position.XCoordinate;
            }
            set
            {
                foreach (Pixel p in Pixels)
                {
                    p.Position.XCoordinate = value;
                }
            }
        }

        /// <summary>
        /// Top of the Paddle objects
        /// </summary>
        public int Top
        {
            get
            {
                return Pixels[0].Position.YCoordinate;
            }
            internal set
            {
                for (int x = 0; x < Pixels.Length; x++)
                {
                    Pixels[x].Position.YCoordinate = value + x;
                }
            }
        }

        /// <summary>
        /// Bottom of the paddle object
        /// </summary>
        public int Bottom
        {
            get
            {
                return Pixels[Pixels.Length - 1].Position.YCoordinate;
            }
            internal set
            {
                for (int x = Pixels.Length - 1; x >= 0; x--)
                {
                    Pixels[x].Position.YCoordinate = value - ((Pixels.Length - 1) - x);
                }
            }
        }

        /// <summary>
        /// Difference of the position from the left side of the game area
        /// </summary>
        public int Left
        {
            get
            {
                return Pixels[0].Position.XCoordinate;
            }
            set
            {
                for (int x = 0; x < Pixels.Length; x++)
                {
                    Pixels[x].Position.XCoordinate = value;
                }
            }
        }

        /// <summary>
        /// Direction the paddle is moving
        /// </summary>
        public EnumDirection Direction
        {
            get
            {
                return Pixels[0].Direction;
            }
            set
            {
                foreach (Pixel p in Pixels)
                {
                    p.Direction = value;
                }
            }
        }

        /// <summary>
        /// The color for the paddle
        /// </summary>
        public ConsoleColor Color { get { return Pixels[0].Color; } }


        /// <summary>
        /// Move the Paddle
        /// </summary>
        /// <param name="direction">The direction to move</param>
        public void Move()
        {
            switch (Direction)
            {
                case EnumDirection.North:
                    Top++;
                    break;
                case EnumDirection.South:
                    Top--;
                    break;
                default:
                    break;
            }
        }

        public Paddle(): this(0, 5)
        {

        }

        public Paddle(int iScore, int iPadelLength)
        {
            Score = iScore;
            Pixels = new Pixel[iPadelLength];

            for (int x = 0; x < iPadelLength; x++)
            {
                Pixels[x] = new Pixel();
            }
        }

        /// <summary>
        /// Checks if the paddle contains the position
        /// </summary>
        /// <param name="vPosition"></param>
        /// <returns></returns>
        public bool Contains(Vector vPosition)
        {
            foreach (Pixel p in Pixels)
            {
                if (p.Position == vPosition)
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
