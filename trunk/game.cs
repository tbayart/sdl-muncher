/*
  First mini-graphics-game skeleton
  Version I: functions Init and Intro
*/

using System;

public class Game02f
{
    public static void Init()
    {
        bool fullScreen = false;
        SdlHardware.Init(800, 600, 24, fullScreen);
    }

    public static void Intro()
    {
        Image pac = new Image("pac01r.bmp");
        Image ghost = new Image("ghostGreen.bmp");
        int x = -40;
        do
        {
            SdlHardware.ClearScreen();
            SdlHardware.DrawHiddenImage(ghost, x-50, 300);
            SdlHardware.DrawHiddenImage(pac,x,300);
            SdlHardware.ShowHiddenScreen();
            x += 8;
            if (x > 850) x = -40;      
            SdlHardware.Pause(20);
        }
        while (!SdlHardware.KeyPressed(SdlHardware.KEY_SPC));

    }


    public static void Main()
    {
        Init();

        Image dotImage = new Image("dot.bmp");
        Image enemyImage = new Image("ghostGreen.bmp");
        Image pacImage = new Image("pac01r.bmp");
        Image wallImage = new Image("wall.bmp");

        int x = 32, y = 32;
        int pacSpeed = 4;

        int amountOfEnemies = 4;
        float[] xEnemy = { 150, 400, 500, 600 };
        float[] yEnemy = { 100, 200, 300, 400 };
        float[] incrXEnemy = { 5f, 3f, 6f, 4.5f };

        string[] map = {
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

        // Data for the dots
        // First: count how many
        int amountOfDots = 0;
        for (int row = 0; row < 15; row++)
        {
            for (int column = 0; column < 17; column++)
            {
                if (map[row][column] == '.')
                    amountOfDots++;
            }
        }
        int[] xDot = new int[amountOfDots];
        int[] yDot = new int[amountOfDots];
        bool[] visible = new bool[amountOfDots];
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


        int score = 0;
        bool gameFinished = false;

        Intro();

        // Game Loop
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
            if (SdlHardware.KeyPressed(SdlHardware.KEY_RIGHT))
            {
                bool canMove = true;
                for (int row = 0; row < 15; row++)
                {
                    for (int column = 0; column < 17; column++)
                    {
                        if (map[row][column] == '-')
                            if ((x + pacSpeed > column * 32 - 32) &&
                                (x + pacSpeed < column * 32 + 32) &&
                                (y > row * 32 - 32) &&
                                (y < row * 32 + 32)
                                )
                            {
                                canMove = false;
                            }
                    }
                }
                if (canMove)
                    x += pacSpeed;
            }

            if (SdlHardware.KeyPressed(SdlHardware.KEY_LEFT))
            {
                bool canMove = true;
                for (int row = 0; row < 15; row++)
                {
                    for (int column = 0; column < 17; column++)
                    {
                        if (map[row][column] == '-')
                            if ((x - pacSpeed > column * 32 - 32) &&
                                (x - pacSpeed < column * 32 + 32) &&
                                (y > row * 32 - 32) &&
                                (y < row * 32 + 32)
                                )
                            {
                                canMove = false;
                            }
                    }
                }
                if (canMove)
                    x -= pacSpeed;
            }

            if (SdlHardware.KeyPressed(SdlHardware.KEY_DOWN))
            {
                bool canMove = true;
                for (int row = 0; row < 15; row++)
                {
                    for (int column = 0; column < 17; column++)
                    {
                        if (map[row][column] == '-')
                            if ((x > column * 32 - 32) &&
                                (x < column * 32 + 32) &&
                                (y + pacSpeed > row * 32 - 32) &&
                                (y + pacSpeed < row * 32 + 32)
                                )
                            {
                                canMove = false;
                            }
                    }
                }
                if (canMove)
                    y += pacSpeed;
            }

            if (SdlHardware.KeyPressed(SdlHardware.KEY_UP))
            {
                bool canMove = true;
                for (int row = 0; row < 15; row++)
                {
                    for (int column = 0; column < 17; column++)
                    {
                        if (map[row][column] == '-')
                            if ((x > column * 32 - 32) &&
                                (x < column * 32 + 32) &&
                                (y - pacSpeed > row * 32 - 32) &&
                                (y - pacSpeed < row * 32 + 32)
                                )
                            {
                                canMove = false;
                            }
                    }
                }
                if (canMove)
                    y -= pacSpeed;
            }

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
