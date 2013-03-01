/**
 * Game.cs - Game logic
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012
 *     Empty skeleton
 * 0.02, 21-dec-2012 
 *     Basic game loop, player can move to the right, 
 *     enemy moves on its own
 * 0.03, 18-jan-2013
 *     Player can move in 4 directions, level is drawn
 * 0.04, 25-ene-2013
 *     Basic collisions checking
 * 0.05, 28-jan-2013
 *     Player checks if it is a valid position before moving
 *     If the position is valid, he gets points
 * 0.08, 06-feb-2013: 
 *     Current level is passed to the Enemy, so that it checks collision
 *     Keys T+L simultaneously allow advancing a level (Trick: Level)
 * 0.09, 01-mar-2013: 
 *     Four ghosts
 */

namespace Game
{
    class Game
    {
        const int NUM_ENEMIES = 4;

        bool gameFinished;
        Player pac;
        Enemy[] ghosts;
        Level level;
        ScoreBoard scoreBoard;
        int score;

        public void Run()
        {
            gameFinished = false;
            pac = new Player();
            pac.MoveTo(8 * 32, 6 * 32);
            level = new Level();
            ghosts = new Enemy[NUM_ENEMIES];
            ghosts[0] = new EnemyGreen(level);
            ghosts[1] = new EnemyRed(level);
            ghosts[2] = new EnemyBlue(level);
            ghosts[3] = new EnemyPurple(level);
            scoreBoard = new ScoreBoard();
            score = 0;
            while (!gameFinished)
            {
                DrawElements();
                CheckInputDevices();
                MoveElements();
                CheckCollisions();
                PauseTillNextFrame();
            } // end of game loop
        }

        public void DrawElements()
        {
            Hardware.ClearScreen();
            level.DrawOnHiddenScreen();
            pac.DrawOnHiddenScreen();
            for (int i=0; i<NUM_ENEMIES; i++)
                ghosts[i].DrawOnHiddenScreen();
            scoreBoard.DrawOnHiddenScreen();
            Hardware.ShowHiddenScreen();
        }


        public void CheckInputDevices()
        {
            if (Hardware.KeyPressed(Hardware.KEY_RIGHT)
                    && level.CanMoveTo(pac.GetX() + pac.GetSpeedX(), pac.GetY(),
                    pac.GetX() + pac.GetWidth() + pac.GetSpeedX(), pac.GetY() + pac.GetHeight()))
            {
                pac.MoveRight();
                score += level.GetPointsFrom(pac.GetX() + pac.GetSpeedX(), pac.GetY(),
                    pac.GetX() + pac.GetWidth() + pac.GetSpeedX(), pac.GetY() + pac.GetHeight());
            }

            if (Hardware.KeyPressed(Hardware.KEY_LEFT)
                    && level.CanMoveTo(pac.GetX() - pac.GetSpeedX(), pac.GetY(),
                    pac.GetX() + pac.GetWidth() - pac.GetSpeedX(), pac.GetY() + pac.GetHeight()))
            {
                pac.MoveLeft();
                score += level.GetPointsFrom(pac.GetX() - pac.GetSpeedX(), pac.GetY(),
                    pac.GetX() + pac.GetWidth() - pac.GetSpeedX(), pac.GetY() + pac.GetHeight());
            }

            if (Hardware.KeyPressed(Hardware.KEY_DOWN)
                    && level.CanMoveTo(pac.GetX(), pac.GetY() + pac.GetSpeedY(),
                    pac.GetX() + pac.GetWidth(), pac.GetY() + pac.GetHeight() + pac.GetSpeedY()))
            {
                pac.MoveDown();
                score += level.GetPointsFrom(pac.GetX(), pac.GetY() + pac.GetSpeedY(),
                    pac.GetX() + pac.GetWidth(), pac.GetY() + pac.GetHeight() + pac.GetSpeedY());
            }

            if (Hardware.KeyPressed(Hardware.KEY_UP)
                    && level.CanMoveTo(pac.GetX(), pac.GetY() - pac.GetSpeedY(),
                    pac.GetX() + pac.GetWidth(), pac.GetY() + pac.GetHeight() - pac.GetSpeedY()))
            {
                pac.MoveUp();
                score += level.GetPointsFrom(pac.GetX(), pac.GetY() - pac.GetSpeedY(),
                    pac.GetX() + pac.GetWidth(), pac.GetY() + pac.GetHeight() - pac.GetSpeedY());
            }

            if (Hardware.KeyPressed(Hardware.KEY_T) && Hardware.KeyPressed(Hardware.KEY_L))
                level.Advance();

            if (Hardware.KeyPressed(Hardware.KEY_ESC))
                gameFinished = true;
        }


        public void MoveElements()
        {
            for (int i = 0; i < NUM_ENEMIES; i++)
                ghosts[i].Move();
            scoreBoard.SetScore(score);
        }


        public void CheckCollisions()
        {
            for (int i=0; i<NUM_ENEMIES; i++)
                if (pac.CollisionsWith(ghosts[i]))
                    gameFinished = true;
        }


        public static void PauseTillNextFrame()
        {
            Hardware.Pause(40);
        }
    }
}
