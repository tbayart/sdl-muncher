/**
 * ScoreBoard.cs - ScoreBoard
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * 0.09, 01-mar-2013: 
 *     Created
 * 0.12, 23-mar-2013: 
 *     Three lives. Map is centered on screen
 */

using System;

namespace Game
{
    class ScoreBoard
    {
        short x = 50;
        short y = -25;
        short xLives = 350;
        int score = 0;
        int lives = 3;
        Font font;

        public ScoreBoard()
        {
            font = new Font("data/Joystix.ttf", 18);
        }

        public void DrawOnHiddenScreen()
        {
            Hardware.WriteHiddenText("Score: "+score,
                    x, y,
                    0xCC, 0xCC, 0xCC,
                    font);
            Hardware.WriteHiddenText("Lives: " + lives,
                    xLives, y,
                    0xCC, 0xCC, 0xCC,
                    font);
        }

        public void SetScore(int newScore)
        {
            score = newScore;
        }

        public void SetLives(int newLives)
        {
            lives = newLives;
        }

    }
}
