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
 */

namespace Game
{
    class Game
    {
        bool gameFinished;
        Player pac;
        Enemy ghost;
        Level level;

        public void Run()
        {
            gameFinished = false;
            pac = new Player();
            ghost = new Enemy();
            level = new Level();
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
            ghost.DrawOnHiddenScreen();
            Hardware.ShowHiddenScreen();
        }


        public void CheckInputDevices()
        {
            if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                pac.MoveRight();
            if (Hardware.KeyPressed(Hardware.KEY_LEFT))
                pac.MoveLeft();
            if (Hardware.KeyPressed(Hardware.KEY_DOWN))
                pac.MoveDown();
            if (Hardware.KeyPressed(Hardware.KEY_UP))
                pac.MoveUp();
            if (Hardware.KeyPressed(Hardware.KEY_ESC))
                gameFinished = true;
        }


        public void MoveElements()
        {
            ghost.Move();
        }


        public void CheckCollisions()
        {
            if (pac.CollisionsWith(ghost))
                gameFinished = true;
        }


        public static void PauseTillNextFrame()
        {
            Hardware.Pause(40);
        }
    }
}
