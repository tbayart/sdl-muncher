/**
 * Enemy.cs - Ghost that chases the player
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Constructor loads image and places ghost
 * 0.03, 18-ene-2013: Can move side to side
 * 0.04, 25-ene-2013
 *     Animated when moving (two images)
 *     Image changes after n passes (10 as starting value)
 * 
 */

namespace Game
{
    class Enemy : Sprite
    {
        protected int maxFramesToWait;  // Game frames needed to change image
        protected int frameWaitCounter; // Counter

        public Enemy()
        {
            LoadSequence(new string[] { "data/ghostGreen.png", "data/ghostGreen2.png" });
            MoveTo(300, 100);
            xSpeed = 2;
            maxFramesToWait = 10;
            frameWaitCounter = 0;
        }

        public override void Move()
        {
            // Adjust position and speed
            x += xSpeed;
            if ((x > 700) || (x < 10))
                xSpeed = -xSpeed;
            // And change image if needed
            frameWaitCounter++;
            if (frameWaitCounter >= maxFramesToWait)
            {
                NextFrame();
                frameWaitCounter = 0;
            }
        }

    }
}
