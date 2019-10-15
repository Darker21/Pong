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

        private Vector _center
        {
            get
            {
                return new Vector(Width / 2 + 1, Height / 2 + 1);
            }
        }
        private int _width;
        private int _height;

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
                return _height;
            }
            set
            {
                if (value % 2 == 0)
                {
                    _height = value + 1;
                }
                else
                {
                    _height = value;
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

        public Game() : this(70, 32, 11, 75)
        {

        }

        public Game(int iWidth, int iHeight, int iMaxScore, int iGameSpeed, bool bPlay = true)
        {
            Width = iWidth;
            Height = iHeight;
            MaxScore = iMaxScore;
            GameSpeed = iGameSpeed;

            Ball = new Pixel();
            Player1 = new Padel();
            Player2 = new Padel();
            Ball.Position.XCoordinate = Width / 2 + 1;
            Ball.Position.YCoordinate = Height / 2 + 1;

            Player1.Bottom = (Height / 2) + ((Player1.Pixels.Length / 2) + 2);
            Player2.Bottom = (Height / 2) + ((Player1.Pixels.Length / 2) + 2);

            Player1.Left = 2;
            Player2.Left = Width - 3;

            if (bPlay)
            {
                Play();
            }
        }

        public void Play()
        {
            DateTime dtLoopStart;
            DateTime dtWaiting;

            Console.WindowHeight = Height + 10;
            Console.WindowWidth = Width;

            SetRandomBallProperties();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Title = $"First to: {MaxScore}";

            do
            {
                Console.Clear();
                DrawGrid();
                DrawPadels();
                DrawBall();
                DrawScore();

                if (Ball.Position.XCoordinate < Player1.Left)
                {
                    Player2.Score++;
                    Ball.Position = _center;
                    SetRandomBallProperties();
                }
                else if (Ball.Position.XCoordinate > Player2.Left)
                {
                    Player1.Score++;
                    Ball.Position = _center;
                    SetRandomBallProperties();
                }

                if (Ball.Position.YCoordinate <= 1)
                {
                    switch (Ball.Direction)
                    {
                        case EnumDirection.NorthWest:
                            Ball.Direction = EnumDirection.SouthWest;
                            break;
                        case EnumDirection.NorthEast:
                            Ball.Direction = EnumDirection.SouthEast;
                            break;
                        case EnumDirection.North:
                            Ball.Direction = EnumDirection.West;
                            break;
                    }
                }
                else if (Ball.Position.YCoordinate >= Height - 1)
                {
                    switch (Ball.Direction)
                    {
                        case EnumDirection.SouthWest:
                            Ball.Direction = EnumDirection.NorthWest;
                            break;
                        case EnumDirection.SouthEast:
                            Ball.Direction = EnumDirection.NorthEast;
                            break;
                        case EnumDirection.South:
                            Ball.Direction = EnumDirection.West;
                            break;
                    }
                }



                if (GameOver)
                {
                    continue;
                }


                Vector posBallFuture = new Vector(Ball.Position.XCoordinate, Ball.Position.YCoordinate);

                switch (Ball.Direction)
                {
                    case EnumDirection.NorthWest:
                        posBallFuture.XCoordinate--;
                        posBallFuture.YCoordinate--;
                        break;
                    case EnumDirection.SouthWest:
                        posBallFuture.XCoordinate--;
                        posBallFuture.YCoordinate++;
                        break;
                    case EnumDirection.NorthEast:
                        posBallFuture.XCoordinate++;
                        posBallFuture.YCoordinate--;
                        break;
                    case EnumDirection.SouthEast:
                        posBallFuture.XCoordinate++;
                        posBallFuture.YCoordinate++;
                        break;
                    case EnumDirection.East:
                        posBallFuture.XCoordinate++;
                        break;
                    case EnumDirection.West:
                        posBallFuture.XCoordinate--;
                        break;
                }

                dtLoopStart = DateTime.Now;
                dtWaiting = DateTime.Now;
                while (dtWaiting.Subtract(dtLoopStart).TotalMilliseconds <= GameSpeed)
                {
                    dtWaiting = DateTime.Now;

                    if (Console.KeyAvailable)
                    {
                        // get the key that has been pressed
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                        // can't move the opposite way or press multiple directions within the gamespeed time
                        if (keyPressed.Key.Equals(ConsoleKey.UpArrow))
                        {
                            Player2.Direction = EnumDirection.North;
                        }
                        if (keyPressed.Key.Equals(ConsoleKey.DownArrow))
                        {
                            Player2.Direction = EnumDirection.South;
                        }
                        if (keyPressed.Key.Equals(ConsoleKey.W))
                        {
                            Player1.Direction = EnumDirection.North;
                        }
                        if (keyPressed.Key.Equals(ConsoleKey.S))
                        {
                            Player1.Direction = EnumDirection.South;
                        }
                    }

                    int iBottom = Player1.Bottom;
                    switch (Player1.Direction)
                    {
                        case EnumDirection.South:
                            if (iBottom + 2 < Height)
                            {
                                Player1.Bottom++;
                            }
                            break;
                        case EnumDirection.North:
                            if (iBottom - Player1.Pixels.Length > 0)
                            {
                                Player1.Bottom--;
                            }
                            break;
                    }

                    iBottom = Player2.Bottom;
                    switch (Player2.Direction)
                    {
                        case EnumDirection.South:
                            if (iBottom + 2 < Height)
                            {
                                Player2.Bottom++;
                            }
                            break;
                        case EnumDirection.North:
                            if (iBottom - Player2.Pixels.Length > 0)
                            {
                                Player2.Bottom--;
                            }
                            break;
                    }

                    if (Player1.Contains(posBallFuture))
                    {
                        if (Player1.Direction == EnumDirection.North && Ball.Direction == EnumDirection.NorthWest)
                        {
                            Ball.Direction = EnumDirection.East;
                        }
                        else if (Player1.Direction == EnumDirection.South && Ball.Direction == EnumDirection.SouthWest)
                        {
                            Ball.Direction = EnumDirection.East;
                        }
                        else if (Player1.Direction == EnumDirection.North && Ball.Direction == EnumDirection.West)
                        {
                            Ball.Direction = EnumDirection.SouthEast;
                        }
                        else if (Player1.Direction == EnumDirection.South && Ball.Direction == EnumDirection.West)
                        {
                            Ball.Direction = EnumDirection.NorthEast;
                        }
                        else if (Player1.Direction == EnumDirection.North && Ball.Direction == EnumDirection.West)
                        {
                            Ball.Direction = EnumDirection.SouthEast;
                        }
                        else if (Player1.Direction == EnumDirection.Unknown)
                        {
                            string sBallDirection = Ball.Direction.ToString();

                            if (sBallDirection.Contains("South"))
                            {
                                Ball.Direction = EnumDirection.SouthEast;
                            }
                            else if (sBallDirection.Contains("North"))
                            {
                                Ball.Direction = EnumDirection.NorthEast;
                            }
                            else
                            {
                                Ball.Direction = EnumDirection.East;
                            }
                        }
                    }

                    if (Player2.Contains(posBallFuture))
                    {
                        if (Player2.Direction == EnumDirection.North && Ball.Direction == EnumDirection.NorthEast)
                        {
                            Ball.Direction = EnumDirection.West;
                        }
                        else if (Player2.Direction == EnumDirection.South && Ball.Direction == EnumDirection.SouthEast)
                        {
                            Ball.Direction = EnumDirection.West;
                        }
                        else if (Player2.Direction == EnumDirection.North && Ball.Direction == EnumDirection.East)
                        {
                            Ball.Direction = EnumDirection.SouthWest;
                        }
                        else if (Player2.Direction == EnumDirection.South && Ball.Direction == EnumDirection.East)
                        {
                            Ball.Direction = EnumDirection.NorthWest;
                        }
                        else if (Player2.Direction == EnumDirection.North && Ball.Direction == EnumDirection.East)
                        {
                            Ball.Direction = EnumDirection.SouthWest;
                        }
                        else if (Player2.Direction == EnumDirection.Unknown)
                        {
                            string sBallDirection = Ball.Direction.ToString();

                            if (sBallDirection.Contains("South"))
                            {
                                Ball.Direction = EnumDirection.SouthWest;
                            }
                            else if (sBallDirection.Contains("North"))
                            {
                                Ball.Direction = EnumDirection.NorthWest;
                            }
                            else
                            {
                                Ball.Direction = EnumDirection.West;
                            }
                        }
                    }
                    Player1.Direction = EnumDirection.Unknown;
                    Player2.Direction = EnumDirection.Unknown;
                }

                Ball.Position = posBallFuture;

            } while (!GameOver);

            GameOverScreen();
        }

        private void DrawScore()
        {
            Console.SetCursorPosition(0, Height + 5);
            Console.Write("----\n");
            Console.WriteLine($"|{Player1.Score}|");
            Console.WriteLine("----");

            Console.SetCursorPosition(Width - 5, Height + 5);
            Console.Write("----\n");
            Console.SetCursorPosition(Width - 5, Height + 6);
            Console.Write($"|{Player2.Score}|\n");
            Console.SetCursorPosition(Width - 5, Height + 7);
            Console.Write("----");
        }

        private void DrawBall()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Ball.Position.XCoordinate, Ball.Position.YCoordinate);
            Console.Write("■");
        }

        private void DrawPadels()
        {
            Console.ForegroundColor = Player1.Color;
            foreach (Pixel p in Player1.Pixels)
            {
                Console.SetCursorPosition(p.Position.XCoordinate, p.Position.YCoordinate);
                Console.Write("■");
            }
            Console.ForegroundColor = Player2.Color;
            foreach (Pixel p in Player2.Pixels)
            {
                Console.SetCursorPosition(p.Position.XCoordinate, p.Position.YCoordinate);
                Console.Write("■");
            }
        }

        private void DrawGrid()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < Width; i++)
            {
                // Set the cursor position to draw the block there
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
                Console.SetCursorPosition(i, Height - 1);
                Console.Write("-");
            }
            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(Width - 1, i);
                Console.Write("|");
            }
        }

        private void SetRandomBallProperties()
        {
            Random rand = new Random();
            int iRandDirection = rand.Next(1, ((int[])Enum.GetValues(typeof(EnumDirection))).Length);
            EnumDirection directionRand = (EnumDirection)iRandDirection;
            do
            {
                iRandDirection = rand.Next(1, ((int[])Enum.GetValues(typeof(EnumDirection))).Length);
                directionRand = (EnumDirection)iRandDirection;
            } while (directionRand == EnumDirection.North || directionRand == EnumDirection.South || directionRand == EnumDirection.Unknown);

            Ball.Direction = directionRand;

            int iRandY = rand.Next(1, Height);
            Ball.Position.YCoordinate = iRandY;
        }

        private void GameOverScreen()
        {
            // Write the gameover message in the middle of the screen
            Console.SetCursorPosition(Width / 5, Height / 2);
            string sPlayerWon = Player1.Score == MaxScore ? "Player 1" : "Player 2"; 
            Console.WriteLine($"Game over, {sPlayerWon} wins!");
            Console.SetCursorPosition(Width / 5, Height / 2 + 1);
        }
    }
}
