using System;

namespace Game
{
    class Game
    {
        bool gameFinished;
        Player pac;
        Enemy ghost;

        public void Run()
        {
            gameFinished = false;
            pac = new Player();
            ghost = new Enemy();
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
            pac.DrawOnHiddenScreen();
            ghost.DrawOnHiddenScreen();
            Hardware.ShowHiddenScreen();
        }


        public void CheckInputDevices()
        {
            if (Hardware.KeyPressed(Hardware.KEY_RIGHT))
                pac.MoveRight();
            if (Hardware.KeyPressed(Hardware.KEY_ESC))
                gameFinished = true;
        }


        public void MoveElements()
        {
            ghost.Move();
        }


        public void CheckCollisions()
        {
        }


        public static void PauseTillNextFrame()
        {
            Hardware.Pause(40);
        }
    }
}
