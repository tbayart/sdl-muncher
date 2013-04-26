
/**
 * Help.cs - Help screen
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Basic help screen, showing static text
 * 0.06, 01-feb-2013: Private "enhanced" enemy, which can fall and jump
 * 0.14, 26-apr-2013: Example of mouse usage 
 */

using System;

namespace Game
{
    class Help
    {
        private class JumpingEnemy : Enemy
        {
            private int maxY = 500;
            private bool isFalling = true;
            private bool isJumping = false;
            private int currentJumpFrame = 0;
            
            private int[] jumpSteps = {-10, -10, -8, -8, -6, -6, -4, -2, -1, -1, 0,
                             0, 1, 1, 2, 4, 6, 6, 8, 8, 10, 10 };

            public JumpingEnemy()
            {
                ySpeed = 4;
                xSpeed = 0;
            }

            public override void Move()
            {
                // Adjust horizontal position and speed
                x += xSpeed;
                if ((x > 700) || (x < 10))
                    xSpeed = -xSpeed;

                // Move down if falling
                if (isFalling)
                {
                    y += ySpeed;
                    if (y > maxY)  // if collision
                    {
                        ySpeed = 0;
                        xSpeed = 4;
                        isFalling = false;
                    }
                }

                // Continue jump sequence, if jumping
                if (isJumping) 
                {
                    y += jumpSteps[currentJumpFrame];  // We should check collisions
                    currentJumpFrame++;
                    if (currentJumpFrame >= jumpSteps.Length)
                    {
                        currentJumpFrame = 0;
                        isJumping = false;
                    }
                }

                // And change image if needed
                frameWaitCounter++;
                if (frameWaitCounter >= maxFramesToWait)
                {
                    NextFrame();
                    frameWaitCounter = 0;
                }
            }

            public void Jump()
            {
                if ((!isFalling) && (!isJumping))
                    isJumping = true;
            }
        }


        JumpingEnemy myGhost;
        Player myPac;

        public Help()
        {
            myGhost = new JumpingEnemy();
            myGhost.MoveTo(DateTime.Now.Millisecond % 750, 20);
            myPac = new Player();
        }

        public void Run()
        {
            Font sans18;
            sans18 = new Font("data/Joystix.ttf", 18);

            do
            {
                Hardware.ClearScreen();
                Hardware.WriteHiddenText(" Sorry, No help available yet! ",
                    200, 500,
                    0xCC, 0xCC, 0xCC,
                    sans18);
                Hardware.WriteHiddenText("Hit ESC to return",
                    300, 540,
                    0x99, 0x99, 0x99,
                    sans18);

                if (Hardware.GetMouseX() != -1)
                {
                    int xPac = Hardware.GetMouseX();
                    int yPac = Hardware.GetMouseY();
                    if (Hardware.MouseClicked())
                        myPac.NextFrame();
                    myPac.MoveTo(xPac, yPac);
                    myPac.DrawOnHiddenScreen();
                }
                    

                myGhost.DrawOnHiddenScreen();
                myGhost.Move();

                Hardware.ShowHiddenScreen();

                if (Hardware.KeyPressed(Hardware.KEY_SPC))
                    myGhost.Jump();
            
                Hardware.Pause(20);
            }
            while (!Hardware.KeyPressed(Hardware.KEY_ESC));
        }
    }
}
