using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Game
    {
        Padel Player1 { get; set; }
        Padel Player2 { get; set; }
        Pixel Ball { get; set; }

        private int _width;

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value % 2 == 0)
                {
                    _width = value + 1;
                }
                _width = value;
            }
        }
        public int Height
        {
            get
            {
                return Height;
            }
            set
            {
                if (value % 2 == 0)
                {
                    Height = value + 1;
                }
                else
                {
                    Height = value;
                }
            }
        }
        public int MaxScore { get; set; }
        public bool GameOver
        {
            get
            {
                return Player1.Score >= MaxScore || Player2.Score >= MaxScore;
            }
        }

        public int GameSpeed { get; private set; }

        public Game() : this(32, 32, 150, 11)
        {

        }

        public Game(int iWidth, int iHeight, int iMaxScore, int iGameSpeed, bool bPlay = true)
        {
            Width = iWidth;
            Height = iHeight;
            MaxScore = iMaxScore;

            Ball.Position.XCoordinate = Width / 2 + 1;
            Ball.Position.YCoordinate = Height / 2 + 1;

            if (bPlay)
            {
                Play();
            }
        }

        public void Play()
        {
            Ball.Direction = EnumDirection.East;

            DrawGrid();
            DrawPadels();

            do
            {
                break;
            } while (!GameOver);
        }

        private void DrawPadels()
        {
            foreach (Pixel p in Player1.Pixels)
            {
                Console.SetCursorPosition(p.Position.XCoordinate, p.Position.YCoordinate);
                Console.Write("■");
            }
        }

        private void DrawGrid()
        {
            for (int i = 0; i < Width; i++)
            {
                // Set the cursor position to draw the block there
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
            }
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, Height - 1);
                Console.Write("-");
            }
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(Width - 1, i);
                Console.Write("|");
            }
        }
    }
}
