/*
  First mini-graphics-game skeleton
  Version C: Collisions between player and dots
*/
 
using System;
 
public class Game02c
{
    public static void Main()
    {
        bool fullScreen = false;
        SdlHardware.Init(800, 600, 24, fullScreen);
        
        Image dotImage = new Image("dot.bmp");
        Image enemyImage = new Image("ghostGreen.bmp");
        Image pacImage = new Image("pac01r.bmp");
        
        int x=40, y=12;
        int pacSpeed = 6;
        
        int amountOfDots = 100;
        int[] xDot = new int[amountOfDots];
        int[] yDot = new int[amountOfDots];
        bool[] visible = new bool[amountOfDots];
        
        Random randomNumberGenerator = new Random();
        for(int i=0; i<amountOfDots; i++)
        {
            xDot[i] = randomNumberGenerator.Next(0,760);
            yDot[i] = randomNumberGenerator.Next(40,560);
            visible[i] = true;
        }
        
        int amountOfEnemies = 4;
        float[] xEnemy = { 150, 400, 500, 600 };
        float[] yEnemy = { 100, 200, 300, 400 };
        float[] incrXEnemy = { 5f, 3f, 6f, 4.5f };
                
        int score = 0;
        bool gameFinished = false;
 
        // Game Loop
        while( ! gameFinished )
        {
            // Draw
            SdlHardware.ClearScreen();
            //Console.Write("Score: {0}",score);
            
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
            if (SdlHardware.KeyPressed(SdlHardware.KEY_RIGHT)) x+=pacSpeed;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_LEFT)) x-=pacSpeed;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_DOWN)) y+=pacSpeed;
            if (SdlHardware.KeyPressed(SdlHardware.KEY_UP)) y-=pacSpeed;
            
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
