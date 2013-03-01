/**
 * ScoreBoard.cs - ScoreBoard
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * 0.09, 01-mar-2013: 
 *     Created
 */

using System;

namespace Game
{
    class ScoreBoard
    {
        short x = 580;
        short y = 200;
        int score = 0;
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
        }

        public void SetScore(int newScore)
        {
            score = newScore;
        }

    }
}
