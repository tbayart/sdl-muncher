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
 * 0.07, 05-feb-2013: 
 *   Two images in each direction
 *   MoveRight and so on can deb used to change the direction
 */

namespace Game
{
    class Player : Sprite
    {
        public Player()
        {
            LoadSequence(RIGHT, new string[] { "data/pac01r.png", "data/pac02r.png" });
            LoadSequence(LEFT, new string[] { "data/pac01l.png", "data/pac02l.png" });
            LoadSequence(UP, new string[] { "data/pac01u.png", "data/pac02u.png" });
            LoadSequence(DOWN, new string[] { "data/pac01d.png", "data/pac02d.png" });
            MoveTo(200, 200);
            xSpeed = 4;
            ySpeed = 4;
        }

        public void MoveRight()
        {
            ChangeDirection(RIGHT);
            x += xSpeed;
            NextFrame();
        }

        public void MoveLeft()
        {
            ChangeDirection(LEFT);
            x -= xSpeed;
            NextFrame();
        }

        public void MoveUp()
        {
            ChangeDirection(UP);
            y -= ySpeed;
            NextFrame();
        }

        public void MoveDown()
        {
            ChangeDirection(DOWN);
            y += ySpeed;
            NextFrame();
        }

    }
}
