using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Pixel
    {
        public Vector Position { get; set; }

        public EnumDirection Direction { get; set; }

        public ConsoleColor Color { get; set; }
    }
}
