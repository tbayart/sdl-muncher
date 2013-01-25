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
 * 
 */

namespace Game
{
    class Player : Sprite
    {
        public Player()
        {
            LoadImage("data/pac01r.png");
            MoveTo(200, 200);
            xSpeed = 4;
            ySpeed = 4;
        }

        public void MoveRight()
        {
            x += xSpeed;
        }

        public void MoveLeft()
        {
            x -= xSpeed;
        }

        public void MoveUp()
        {
            y -= ySpeed;
        }

        public void MoveDown()
        {
            y += ySpeed;
        }

    }
}
