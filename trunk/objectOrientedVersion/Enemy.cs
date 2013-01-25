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
 * 
 */

namespace Game
{
    class Enemy : Sprite
    {
        public Enemy()
        {
            LoadImage("data/ghostGreen.png");
            MoveTo(300, 100);
            xSpeed = 2;
        }

        public virtual void Move()
        {
            x += xSpeed;
            if ((x > 700) || (x < 10))
                xSpeed = -xSpeed;
        }

    }
}
