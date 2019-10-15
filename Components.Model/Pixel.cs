using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Pixel
    {
        /// <summary>
        /// The position of the pixel in terms of the grid
        /// </summary>
        public Vector Position { get; set; }

        /// <summary>
        /// Direction of the pixel to move
        /// </summary>
        public EnumDirection Direction { get; set; }

        /// <summary>
        /// The color of the pixel
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// The character/string representation of the pixel
        /// </summary>
        public string PixelString { get; set; }

        public Pixel()
        {
            Position = new Vector();
            Direction = EnumDirection.Unknown;
            Color = ConsoleColor.White;
            PixelString = "■";
        }
    }
}
