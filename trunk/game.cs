/*
  First mini-graphics-game skeleton
  Version R: ghosts with random movement
*/

using System;

public class SdlMuncher
{
    struct Dot
    {
        public int x;
        public int y;
        public bool visible;
    }
    static Dot[] dots;
    static int amountOfDots;

    struct Enemy
    {
        public float x;
        public float y;
        public float xSpeed;
        public float ySpeed;
    }
    static Enemy[] enemies;
    static int amountOfEnemies;

    static bool sessionFinished = false;
    static int startX = 32 * 8;
    static int startY = 32 * 8;
    static int lives;
    static int x, y;
    static int pacSpeed;

    static Image dotImage;
    static Image enemyImage;
    static Image pacImage;
    static Image wallImage;

    static string[] map = {
            "-----------------",
            "-.......-.......-",
            "-.--.--.-.--.--.-",
            "-...............-",
            "-.--.-.---.-.--.-",
            "-....-..-..-....-",
            "----.--...--.----",
            ".................",
            "----.-.-.-.-.----",
            "-....-.-.-.-....-",
            "-.--.-.---.-.--.-",
            "-...............-",
            "-.--.-.---.-.--.-",
            "-....-.....-....-",
            "-----------------"
    };
    static int score;
    static Font sans18;
    static bool gameFinished;
    static Random randomGenerator;


    public static void Init()
    {
        bool fullScreen = false;
        SdlHardware.Init(800, 600, 24, fullScreen);

        dotImage = new Image("data/dot.png");
        enemyImage = new Image("data/ghostGreen.png");
        pacImage = new Image("data/pac01r.png");
        wallImage = new Image("data/wall.png");

        sans18 = new Font("data/Joystix.ttf", 18);

        // Data for the dots
        // First: count how many dots are there
        amountOfDots = 0;
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 17; column++)
            {
                if (map[row][column] == '.')
                    amountOfDots++;
            }
        }
        dots = new Dot[amountOfDots];

        // Now, assign their coordinates
        int currentDot = 0;
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 17; column++)
            {
                if (map[row][column] == '.')
                {
                    dots[currentDot].x = column * 32;
                    dots[currentDot].y = row * 32;
                    currentDot++;
                }
            }
        }

        // And enemies
        amountOfEnemies = 4;
        enemies = new Enemy[amountOfEnemies];

        randomGenerator = new Random();
    }


    public static void PrepareGameStart()
    {
        // Pac coordinates and speed
        x = startX;
        y = startY;
        pacSpeed = 4;

        // Coordinates for the enemies        
        enemies[0].x = 32; enemies[0].y = 1 * 32; enemies[0].xSpeed = 4;
        enemies[1].x = 32; enemies[1].y = 3 * 32; enemies[1].xSpeed = 4;
        enemies[2].x = 3 * 32; enemies[2].y = 9 * 32; enemies[2].xSpeed = -4;
        enemies[3].x = 32; enemies[3].y = 11 * 32; enemies[3].xSpeed = 4;

        // All dots must be visible
        for (int i = 0; i < amountOfDots; i++)
            dots[i].visible = true;

        // Resto of data for a new game
        score = 0;
        lives = 3;
    }


    public static void Intro()
    {
        int x = -40;
        bool exitIntro = false;
        do
        {
            SdlHardware.ClearScreen();
            SdlHardware.WriteHiddenText("Hit SPACE to start",
                300, 400,
                0xCC, 0xCC, 0xCC,
                sans18);
            SdlHardware.WriteHiddenText("or H for Help",
                330, 440,
                0xAA, 0xAA, 0xAA,
                sans18);
            SdlHardware.WriteHiddenText("or C for Credits",
                310, 480,
                0x88, 0x88, 0x88,
                sans18);
            SdlHardware.WriteHiddenText("or Q to Quit",
                340, 520,
                0x66, 0x66, 0x66,
                sans18);

            SdlHardware.DrawHiddenImage(enemyImage, x - 50, 300);
            SdlHardware.DrawHiddenImage(pacImage, x, 300);
            SdlHardware.ShowHiddenScreen();
            x += 8;
            if (x > 850) x = -40;
            SdlHardware.Pause(20);
            if (SdlHardware.KeyPressed(SdlHardware.KEY_C))
                ShowCredits();
            if (SdlHardware.KeyPressed(SdlHardware.KEY_H))
                ShowHelp();
            if (SdlHardware.KeyPressed(SdlHardware.KEY_SPC))
                exitIntro = true;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_Q))
            {
                exitIntro = true;
                sessionFinished = true;
            }
        }
        while (!exitIntro);
    }


    public static void ShowCredits()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("By DAM Ies San Vicente 2012-2013",
            200, 500,
            0xCC, 0xCC, 0xCC,
            sans18);
        SdlHardware.WriteHiddenText("Hit ESC to return",
            300, 540,
            0x99, 0x99, 0x99,
            sans18);
        SdlHardware.ShowHiddenScreen();
        do
        {
            SdlHardware.Pause(20);
        }
        while (!SdlHardware.KeyPressed(SdlHardware.KEY_ESC));
    }


    public static void ShowHelp()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("Eat the dots, avoid the ghosts.",
            200, 500,
            0xCC, 0xCC, 0xCC,
            sans18);
        SdlHardware.WriteHiddenText("Hit ESC to return",
            300, 540,
            0x99, 0x99, 0x99,
            sans18);
        SdlHardware.ShowHiddenScreen();
        do
        {
            SdlHardware.Pause(20);
        }
        while (!SdlHardware.KeyPressed(SdlHardware.KEY_ESC));
    }


    public static bool CanMoveTo(int x, int y, string[] map)
    {
        bool canMove = true;
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 17; column++)
            {
                if (map[row][column] == '-')
                    if ((x > column * 32 - 32) &&
                        (x < column * 32 + 32) &&
                        (y > row * 32 - 32) &&
                        (y < row * 32 + 32)
                        )
                    {
                        canMove = false;
                    }
            }
        }
        return canMove;
    }


    public static void DrawElements()
    {
        // Draw
        SdlHardware.ClearScreen();
        //Console.Write("Score: {0}",score);

        // Background map
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 17; column++)
            {
                if (map[row][column] == '-')
                    SdlHardware.DrawHiddenImage(wallImage, column * 32, row * 32);
            }
        }

        for (int i = 0; i < amountOfDots; i++)
        {
            if (dots[i].visible)
                SdlHardware.DrawHiddenImage(dotImage, dots[i].x, dots[i].y);
        }

        SdlHardware.DrawHiddenImage(pacImage, x, y);

        for (int i = 0; i < amountOfEnemies; i++)
            SdlHardware.DrawHiddenImage(enemyImage,
                (int)enemies[i].x, (int)enemies[i].y);

        SdlHardware.WriteHiddenText("Score: " + score,
            610, 100,
            0x80, 0x80, 0xFF,
            sans18);

        SdlHardware.WriteHiddenText("Lives: " + lives,
            610, 140,
            0x80, 0x80, 0xFF,
            sans18);

        SdlHardware.ShowHiddenScreen();
    }


    public static void CheckInputDevices()
    {
        // Read keys and calculate new position
        if (SdlHardware.KeyPressed(SdlHardware.KEY_RIGHT)
                && CanMoveTo(x + pacSpeed, y, map))
            x += pacSpeed;

        if (SdlHardware.KeyPressed(SdlHardware.KEY_LEFT)
                && CanMoveTo(x - pacSpeed, y, map))
            x -= pacSpeed;

        if (SdlHardware.KeyPressed(SdlHardware.KEY_DOWN)
                && CanMoveTo(x, y + pacSpeed, map))
            y += pacSpeed;

        if (SdlHardware.KeyPressed(SdlHardware.KEY_UP)
                && CanMoveTo(x, y - pacSpeed, map))
            y -= pacSpeed;

        if (SdlHardware.KeyPressed(SdlHardware.KEY_ESC))
            gameFinished = true;
    }


    public static void MoveElements()
    {
        // Move enemies and environment
        for (int i = 0; i < amountOfEnemies; i++)
        {
            if (CanMoveTo((int)(enemies[i].x + enemies[i].xSpeed),
                    (int)(enemies[i].y + enemies[i].ySpeed), map))
            {
                enemies[i].x += enemies[i].xSpeed;
                enemies[i].y += enemies[i].ySpeed;
            }
            else
            {
                switch (randomGenerator.Next(0, 4))
                {
                    case 0: // Next move: to the right
                        enemies[i].xSpeed = 4;
                        enemies[i].ySpeed = 0;
                        break;
                    case 1: // Next move: to the left
                        enemies[i].xSpeed = -4;
                        enemies[i].ySpeed = 0;
                        break;
                    case 2: // Next move: upwards
                        enemies[i].xSpeed = 0;
                        enemies[i].ySpeed = -4;
                        break;
                    case 3: // Next move: downwards
                        enemies[i].xSpeed = 0;
                        enemies[i].ySpeed = 4;
                        break;
                }
            }
        }
    }


    public static void CheckCollisions()
    {
        // Collisions, lose energy or lives, etc
        for (int i = 0; i < amountOfDots; i++)
            if (dots[i].visible &&
                (x > dots[i].x - 32) &&
                (x < dots[i].x + 32) &&
                (y > dots[i].y - 32) &&
                (y < dots[i].y + 32)
                )
            {
                score += 10;
                dots[i].visible = false;
            }

        for (int i = 0; i < amountOfEnemies; i++)
            if (
                (x > enemies[i].x - 32) &&
                (x < enemies[i].x + 32) &&
                (y > enemies[i].y - 32) &&
                (y < enemies[i].y + 32)
                )
            {
                x = startX;
                y = startY;
                lives--;
                if (lives == 0)
                    gameFinished = true;
            }
    }


    public static void PauseTillNextFrame()
    {
        SdlHardware.Pause(40);
    }


    public static void Main()
    {
        Init();
        Intro();

        while (!sessionFinished)
        {
            PrepareGameStart();

            // Game Loop
            gameFinished = false;
            while (!gameFinished)
            {
                DrawElements();
                CheckInputDevices();
                MoveElements();
                CheckCollisions();
                PauseTillNextFrame();
            } // end of game loop

            Intro();

        } // end of session
    } // end of  Main
} // end of class
