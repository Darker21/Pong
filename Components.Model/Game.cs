using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Model
{
    public class Game
    {
        private int _iSpeed;
        private int _iScore;

        /// <summary>
        /// Player1's Padel
        /// </summary>
        public Paddle Player1 { get; set; }

        /// <summary>
        /// Player2's Padel
        /// </summary>
        public Paddle Player2 { get; set; }

        /// <summary>
        /// The 'ball'
        /// </summary>
        public Pixel Ball { get; set; }

        /// <summary>
        /// The center relative to the game area
        /// </summary>
        private Vector _center
        {
            get
            {
                return new Vector(Width / 2 + 1, Height / 2 + 1);
            }
        }

        /// <summary>
        /// The width variable
        /// </summary>
        private int _width;

        /// <summary>
        /// The height variable
        /// </summary>
        private int _height;

        /// <summary>
        /// The accessor for the _width variable
        /// </summary>
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

        /// <summary>
        /// The accessor for the _height variable
        /// </summary>
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

        /// <summary>
        /// The max score which determines the winner
        /// </summary>
        public int MaxScore
        {
            get
            {
                return _iScore;
            }
            set
            {
                if (value <= 0)
                {
                    _iScore = 1;
                }
                else
                {
                    _iScore = value;
                }
            }
        }

        /// <summary>
        /// Get's whether the game is over
        /// </summary>
        public bool GameOver
        {
            get
            {
                return Player1.Score >= MaxScore || Player2.Score >= MaxScore;
            }
        }

        /// <summary>
        /// The speed of refresh in milliseconds
        /// </summary>
        public int GameSpeed
        {
            get
            {
                return _iSpeed;
            }
            set
            {
                // don't allow the game to be too slow or fast
                if (value < 75)
                {
                    _iSpeed = 75;
                }
                else if (value > 200)
                {
                    _iSpeed = 200;
                }
                else
                {
                    _iSpeed = value;
                }
            }
        }

        /// <summary>
        /// base constructor
        /// </summary>
        public Game() : this(70, 32, 11, 100)
        {

        }

        /// <summary>
        /// Constructor for whether the game should start playing
        /// </summary>
        /// <param name="bPlay"></param>
        public Game(bool bPlay) : this(70, 32, 11, 100, bPlay)
        {

        }

        /// <summary>
        /// Constructor to initialize the game
        /// </summary>
        /// <param name="iWidth">Width of the playable area</param>
        /// <param name="iHeight">Height of the playable area</param>
        /// <param name="iMaxScore">Max score to reach before a player wins</param>
        /// <param name="iGameSpeed">The game speed in milliseconds</param>
        /// <param name="bPlay">Whether to play the game straight away (if not, the Play method will need to be utilised)</param>
        public Game(int iWidth, int iHeight, int iMaxScore, int iGameSpeed, bool bPlay = true)
        {
            // Set the properties
            Width = iWidth;
            Height = iHeight;
            MaxScore = iMaxScore;
            GameSpeed = iGameSpeed;

            Ball = new Pixel();
            Player1 = new Paddle();
            Player2 = new Paddle();

            // Set paddels to be in the center of the y axis
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
            // Declare the DateTime variables which will be compared for the gamespeed
            DateTime dtLoopStart;
            DateTime dtWaiting;

            // set the console width and height
            Console.WindowHeight = Height + 10;
            Console.WindowWidth = Width;

            // Set the ball to a random start location and direction
            SetRandomBallProperties();

            // Clear the console
            Console.Clear();

            // Ensure black color
            Console.BackgroundColor = ConsoleColor.Black;

            // Set the title of the window
            Console.Title = $"First to: {MaxScore}";

            // enter a do while loop which will continue until 'GameOver' is True
            do
            {
                // Clear and redraw components
                Console.Clear();
                DrawPlayArea();
                DrawPaddles();
                DrawBall();
                DrawScore();

                // check whether the ball has passed a players paddle
                if (Ball.Position.XCoordinate < Player1.Left)
                {
                    // Increment Player2's score
                    Player2.Score++;

                    // Set the ball to a random start location and direction
                    SetRandomBallProperties();
                }
                else if (Ball.Position.XCoordinate > Player2.Left)
                {
                    // Increment Player1's score
                    Player1.Score++;

                    // Set the ball to a random start location and direction
                    SetRandomBallProperties();
                }

                // If ball touches edge, invert y direction
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

                // If it's gameover - break
                if (GameOver)
                {
                    break;
                }

                #region Determine the Ball's future position
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
                #endregion

                // Instantiate the DateTime variables which will be compared for the gamespeed
                dtLoopStart = DateTime.Now;
                dtWaiting = DateTime.Now;

                // While the difference in time is less that the desired game speed
                while (dtWaiting.Subtract(dtLoopStart).TotalMilliseconds <= GameSpeed)
                {
                    // Refresh the DateTime Waiting to Now
                    dtWaiting = DateTime.Now;

                    // if there is a keyinfo object in the input stream
                    if (Console.KeyAvailable)
                    {
                        // get the key that has been pressed and intercept this so it doesn't right to the console
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                        // Set the respective player's direction based on the key pressed or pauses the game
                        if (keyPressed.Key.Equals(ConsoleKey.UpArrow))
                        {
                            Player2.Direction = EnumDirection.North;
                        }
                        else if (keyPressed.Key.Equals(ConsoleKey.DownArrow))
                        {
                            Player2.Direction = EnumDirection.South;
                        }
                        else if (keyPressed.Key.Equals(ConsoleKey.W))
                        {
                            Player1.Direction = EnumDirection.North;
                        }
                        else if (keyPressed.Key.Equals(ConsoleKey.S))
                        {
                            Player1.Direction = EnumDirection.South;
                        }
                        else if (keyPressed.Key.Equals(ConsoleKey.P))
                        {
                            Pause();
                        }
                        else if (keyPressed.Key.Equals(ConsoleKey.Escape))
                        {
                            Environment.Exit(0);
                        }
                    }

                    #region check whether it is possible for the player's paddle to move in the desired direction
                    int iBottom = Player1.Bottom;
                    switch (Player1.Direction)
                    {
                        case EnumDirection.South:
                            if (iBottom + 2 < Height)
                            {
                                Player1.Move();
                            }
                            break;
                        case EnumDirection.North:
                            if (iBottom - Player1.Pixels.Length > 0)
                            {
                                Player1.Move();
                            }
                            break;
                    }

                    iBottom = Player2.Bottom;
                    switch (Player2.Direction)
                    {
                        case EnumDirection.South:
                            if (iBottom + 2 < Height)
                            {
                                Player2.Move();
                            }
                            break;
                        case EnumDirection.North:
                            if (iBottom - Player2.Pixels.Length > 0)
                            {
                                Player2.Move();
                            }
                            break;
                    }
                    #endregion

                    #region If the ball reaches a player's paddle - reverse the direction
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
                    #endregion

                    // reset player's directions to stop them from moving infinitely
                    Player1.Direction = EnumDirection.Unknown;
                    Player2.Direction = EnumDirection.Unknown;
                }

                // Set the ball's position to the future position
                Ball.Position = posBallFuture;

            } while (!GameOver);

            // Display GameOverScreen
            DisplayGameOverScreen();
        }

        /// <summary>
        /// Pauses the game state
        /// </summary>
        private void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(_center.XCoordinate - 4, Height + 6);
            Console.Write("Paused!");

            // Continue looping
            while (true)
            {
                // if there is a keyinfo object in the input stream
                if (Console.KeyAvailable)
                {
                    // get the key that has been pressed and intercept this so it doesn't right to the console
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                    // Break if the user has unpaused the game
                    if (keyPressed.Key.Equals(ConsoleKey.P))
                    {
                        break;
                    }
                    else if (keyPressed.Key.Equals(ConsoleKey.Escape))
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        /// <summary>
        /// Draws both players scores underneath the game board
        /// </summary>
        private void DrawScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, Height + 5);
            Console.WriteLine("----");
            Console.WriteLine($"|{Player1.Score}|");
            Console.WriteLine("----");

            // Draw player2's score on the right-hand side of the screen
            Console.SetCursorPosition(Width - 5, Height + 5);
            Console.Write("----\n");
            Console.SetCursorPosition(Width - 5, Height + 6);
            Console.Write($"|{Player2.Score}|\n");
            Console.SetCursorPosition(Width - 5, Height + 7);
            Console.Write("----");
        }

        /// <summary>
        /// Draw the ball object
        /// </summary>
        private void DrawBall()
        {
            // Set the foreground color to be green for the ball
            Console.ForegroundColor = Ball.Color;
            DrawPixel(Ball);
        }

        /// <summary>
        /// Draws the players paddles in the respected positions
        /// </summary>
        private void DrawPaddles()
        {
            Console.ForegroundColor = Player1.Color;
            DrawPixel(Player1.Pixels);

            Console.ForegroundColor = Player2.Color;
            DrawPixel(Player2.Pixels);
        }

        /// <summary>
        /// Draws pixel objects on the screen
        /// </summary>
        /// <param name="apixDraw">An array of pixels to draw</param>
        private void DrawPixel(Pixel[] apixDraw)
        {
            foreach (Pixel p in apixDraw)
            {
                DrawPixel(p);
            }
        }

        /// <summary>
        /// Draws a pixel object
        /// </summary>
        /// <param name="pixDraw">The pixel object to draw</param>
        private void DrawPixel(Pixel pixDraw)
        {
            Console.SetCursorPosition(pixDraw.Position.XCoordinate, pixDraw.Position.YCoordinate);
            Console.Write(pixDraw.PixelString);
        }

        /// <summary>
        /// Draws the play area
        /// </summary>
        private void DrawPlayArea()
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
                if (i == 0 || i == Height - 1) { continue; }
                Console.SetCursorPosition(_center.XCoordinate, i);
                Console.Write("|");
            }
        }

        /// <summary>
        /// Set the ball's direction and start position to be random
        /// </summary>
        private void SetRandomBallProperties()
        {
            int iRandDirection;
            EnumDirection directionRand;

            // new random object
            Random rand = new Random();

            int iEnumLength = ((int[])Enum.GetValues(typeof(EnumDirection))).Length;

            // Get a random direction whilst not allowing Unknown, North or South to be used
            do
            {
                // Get a random direction using the integar values of the enumerator
                iRandDirection = rand.Next(1, iEnumLength);
                // cast the int to the relevant enumerator value/representation
                directionRand = (EnumDirection)iRandDirection;

            } while (directionRand == EnumDirection.North || directionRand == EnumDirection.South || directionRand == EnumDirection.Unknown);

            // Assign the random direction to the ball object
            Ball.Direction = directionRand;

            // Get a new random Y position
            int iRandY = rand.Next(1, Height);
            Ball.Position.XCoordinate = _center.XCoordinate;
            Ball.Position.YCoordinate = iRandY;
        }

        /// <summary>
        /// Displays the GameOver Screen declaring who is the winner
        /// </summary>
        private void DisplayGameOverScreen()
        {
            // Write the gameover message in the middle of the screen
            Console.SetCursorPosition(Width / 5, Height / 2);
            string sPlayerWon = Player1.Score == MaxScore ? "Player 1" : "Player 2"; 
            Console.WriteLine($"Game over, {sPlayerWon} wins!");
            Console.SetCursorPosition(Width / 5, Height / 2 + 1);
        }
    }
}
