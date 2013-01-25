
/**
 * Sprite.cs - A basic graphic element to inherit from
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Sprite can be loaded, moved to a new position and drawn
 * 
 */

namespace Game
{
    class Sprite
    {
        protected int x, y;
        protected int xSpeed, ySpeed;
        protected bool visible;
        protected Image image;

        public Sprite()
        {
        }

        public void MoveTo( int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public virtual void Move()
        {
            // To be redefined in subclasses
        }

        public void LoadImage(string name)
        {
            image = new Image(name);
        }

        public void DrawOnHiddenScreen()
        {
            Hardware.DrawHiddenImage(image, x, y);
        }
    }
}
