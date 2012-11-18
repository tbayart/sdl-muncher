/*
  First mini-graphics-game skeleton
  Version K: more detailed "Init", most variables become static attributes
*/

using System;

public class Game02f
{
    static int x, y;
    static int pacSpeed;
    static int amountOfEnemies;

    static float[] xEnemy = { 150, 400, 500, 600 };
    static float[] yEnemy = { 100, 200, 300, 400 };
    static float[] incrXEnemy = { 5f, 3f, 6f, 4.5f };

    static int[] xDot;
    static int[] yDot;
    static bool[] visible;
    static int amountOfDots;

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

    public static void Init()
    {
        bool fullScreen = false;
        SdlHardware.Init(800, 600, 24, fullScreen);

        dotImage = new Image("dot.bmp");
        enemyImage = new Image("ghostGreen.bmp");
        pacImage = new Image("pac01r.bmp");
        wallImage = new Image("wall.bmp");

        x = 32;
        y = 32;
        pacSpeed = 4;

        amountOfEnemies = 4;
        
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
        xDot = new int[amountOfDots];
        yDot = new int[amountOfDots];
        visible = new bool[amountOfDots];
        // Now, assign their coordinates
        int currentDot = 0;
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 17; column++)
            {
                if (map[row][column] == '.')
                {
                    xDot[currentDot] = column * 32;
                    yDot[currentDot] = row * 32;
                    visible[currentDot] = true;
                    currentDot++;
                }
            }
        }
        score = 0;
    }


    public static void Intro()
    {
        Image pac = new Image("pac01r.bmp");
        Image ghost = new Image("ghostGreen.bmp");
        int x = -40;
        do
        {
            SdlHardware.ClearScreen();
            SdlHardware.DrawHiddenImage(ghost, x - 50, 300);
            SdlHardware.DrawHiddenImage(pac, x, 300);
            SdlHardware.ShowHiddenScreen();
            x += 8;
            if (x > 850) x = -40;
            SdlHardware.Pause(20);
        }
        while (!SdlHardware.KeyPressed(SdlHardware.KEY_SPC));

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

    public static void Main()
    {
        Init();
        Intro();

        // Game Loop
        bool gameFinished = false;
        while (!gameFinished)
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
                if (visible[i])
                    SdlHardware.DrawHiddenImage(dotImage, xDot[i], yDot[i]);
            }

            SdlHardware.DrawHiddenImage(pacImage, x, y);

            for (int i = 0; i < amountOfEnemies; i++)
                SdlHardware.DrawHiddenImage(enemyImage,
                    (int)xEnemy[i], (int)yEnemy[i]);

            SdlHardware.ShowHiddenScreen();

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

            // Move enemies and environment
            for (int i = 0; i < amountOfEnemies; i++)
            {
                xEnemy[i] += incrXEnemy[i];
                if ((xEnemy[i] < 1) || (xEnemy[i] > 760))
                    incrXEnemy[i] = -incrXEnemy[i];
            }

            // Collisions, lose energy or lives, etc
            for (int i = 0; i < amountOfDots; i++)
                if (visible[i] &&
                    (x > xDot[i] - 32) &&
                    (x < xDot[i] + 32) &&
                    (y > yDot[i] - 32) &&
                    (y < yDot[i] + 32)
                    )
                {
                    score += 10;
                    visible[i] = false;
                }

            // Pause till next fotogram
            SdlHardware.Pause(40);
        }
    }
}
