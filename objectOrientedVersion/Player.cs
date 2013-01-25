/**
 * Player.cs - Character controlled by the player
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Can move to the right
 * 0.03, 18-ene-2013: Can move to the left, up and down
 * 0.04, 25-ene-2013: Animated when moving (two images)
 * 
 */

namespace Game
{
    class Player : Sprite
    {
        public Player()
        {
            LoadSequence(new string[] { "data/pac01r.png", "data/pac02r.png" });
            MoveTo(200, 200);
            xSpeed = 4;
            ySpeed = 4;
        }

        public void MoveRight()
        {
            x += xSpeed;
            NextFrame();
        }

        public void MoveLeft()
        {
            x -= xSpeed;
            NextFrame();
        }

        public void MoveUp()
        {
            y -= ySpeed;
            NextFrame();
        }

        public void MoveDown()
        {
            y += ySpeed;
            NextFrame();
        }

    }
}
