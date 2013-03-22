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
 * 0.08, 06-feb-2013: 
 *     "Move" checks valid positions in the current level
 * 0.11, 22-mar-2013: 
 *     Ghosts can become grey ("eatable")
 */

using System;

namespace Game
{
    class Enemy : Sprite
    {
        protected int maxFramesToWait;  // Game frames needed to change image
        protected int frameWaitCounter; // Counter
        protected Level myLevel;        // The level in which the ghost is, to check collisions
        protected Random randomGenerator;
        protected int baseEnemySpeed;
        protected Image greyImage;      // Image when the ghost can be eaten
        protected bool isGrey;          // Can the ghost be eaten?
        protected int greyTime;         // Remaining time during which can be eaten

        public Enemy()
        {
            LoadSequence(new string[] { "data/ghostGreen.png", "data/ghostGreen2.png" });
            MoveTo(10*32, 4*32);
            xSpeed = 2;
            maxFramesToWait = 10;
            frameWaitCounter = 0;
            randomGenerator = new Random();
            baseEnemySpeed = 2;
            greyImage = new Image("data/ghostGrey.png");
            isGrey = false;
            greyTime = 200;
        }

        public Enemy(Level l): this()
        {
            myLevel = l;
        }

        public override void Move()
        {
            if (myLevel == null)  // If no level has been indicated, it will not move
                return;

            // Decrease ghost chasing time
	        if (isGrey)
	            greyTime --;
            if (greyTime <= 0)
	        {
                greyTime = 200;
                BecomeNormal();
	        }

            // Move through the labyrinth
            if (myLevel.CanMoveTo(x + xSpeed, y + ySpeed, 
                x + width + xSpeed, y + height + ySpeed))
            {
                x += xSpeed;
                y += ySpeed;
            }
            else
            {
                switch (randomGenerator.Next(0, 4))
                {
                    case 0: // Next move: to the right
                        xSpeed = baseEnemySpeed;
                        ySpeed = 0;
                        break;
                    case 1: // Next move: to the left
                        xSpeed = -baseEnemySpeed;
                        ySpeed = 0;
                        break;
                    case 2: // Next move: upwards
                        xSpeed = 0;
                        ySpeed = -baseEnemySpeed;
                        break;
                    case 3: // Next move: downwards
                        xSpeed = 0;
                        ySpeed = baseEnemySpeed;
                        break;
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

        public void BecomeGrey()
        {
            isGrey = true;
            containsSequence = false;
            image = greyImage;
        }

        public void BecomeNormal()
        {
            isGrey = false;
            containsSequence = true; ;
        }

        public bool IsGrey()
        {
            return isGrey;
        }

    }
}
