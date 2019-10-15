using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Padel
    {
        public Pixel[] Pixels;

        public int Score { get; set; }

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

        public int Top
        {
            get
            {
                return Pixels[0].Position.YCoordinate;
            }
            private set
            {
                for (int x = 0; x < Pixels.Length; x++)
                {
                    Pixels[x].Position.YCoordinate = value + x;
                }
            }
        }

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

        public ConsoleColor Color { get { return Pixels[0].Color; } }

        public void Move(EnumDirection direction)
        {
            switch (direction)
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

        public Padel(): this(0, 5)
        {

        }

        public Padel(int iScore, int iPadelLength)
        {
            Score = iScore;
            Pixels = new Pixel[iPadelLength];

            for (int x = 0; x < iPadelLength; x++)
            {
                Pixels[x] = new Pixel();
            }
        }

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
