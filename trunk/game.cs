/*
  First mini-graphics-game skeleton
  Version G: dots from map
*/
 
using System;
 
public class Game02f
{
    public static void Main()
    {
        bool fullScreen = false;
        SdlHardware.Init(800, 600, 24, fullScreen);
        
        Image dotImage = new Image("dot.bmp");
        Image enemyImage = new Image("ghostGreen.bmp");
        Image pacImage = new Image("pac01r.bmp");
        Image wallImage = new Image("wall.bmp");
        
        int x=32, y=32;
        int pacSpeed = 6;
        
        int amountOfEnemies = 4;
        float[] xEnemy = { 150, 400, 500, 600 };
        float[] yEnemy = { 100, 200, 300, 400 };
        float[] incrXEnemy = { 5f, 3f, 6f, 4.5f };
                
        string [] map = {
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
                    xDot[currentDot] = column*32;
                    yDot[currentDot] = row * 32;
                    visible[currentDot] = true;
                    currentDot++;
                }
            }
        }


        int score = 0;
        bool gameFinished = false;
 
        // Game Loop
        while( ! gameFinished )
        {
            // Draw
            SdlHardware.ClearScreen();
            //Console.Write("Score: {0}",score);
            
            // Background map
            for(int row=0; row<15; row++)
            {
                for(int column=0; column<17; column++)
                {
                    if (map[row][column] == '-')
                        SdlHardware.DrawHiddenImage(wallImage, column*32, row*32);
                }
            }
            
            for(int i=0; i<amountOfDots; i++)
            {
                if (visible[i])
                    SdlHardware.DrawHiddenImage(dotImage, xDot[i], yDot[i]);
            }
            
            SdlHardware.DrawHiddenImage(pacImage, x, y);
            
            for(int i=0; i<amountOfEnemies; i++)
                SdlHardware.DrawHiddenImage(enemyImage, 
                    (int)xEnemy[i], (int)yEnemy[i]);
                    
            SdlHardware.ShowHiddenScreen();

            // Read keys and calculate new position
            if (SdlHardware.KeyPressed(SdlHardware.KEY_RIGHT)) x += pacSpeed;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_LEFT)) x -= pacSpeed;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_DOWN)) y += pacSpeed;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_UP)) y -= pacSpeed;
            
            if (SdlHardware.KeyPressed(SdlHardware.KEY_ESC)) 
                gameFinished = true;
                        
            // Move enemies and environment
            for(int i=0; i<amountOfEnemies; i++)
            {
                xEnemy[i] += incrXEnemy[i];
                if ((xEnemy[i] < 1) || (xEnemy[i] > 760))
                    incrXEnemy[i] = -incrXEnemy[i];
            }
 
            // Collisions, lose energy or lives, etc
            for(int i=0; i<amountOfDots; i++)
                if ( visible[i] &&
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
