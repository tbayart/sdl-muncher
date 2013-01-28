
/**
 * Sprite.cs - A basic graphic element to inherit from
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Sprite can be loaded, moved to a new position and drawn
 * 0.04, 25-ene-2013
 *     Support for collisions between sprites
 *     Support for sequences (animated images): array of images,
 *     new constructor, LoadSequence method
 * 0.05, 28-jan-2013: Support for collisions with coordinates
 */

namespace Game
{
    class Sprite
    {
        protected int x, y;
        protected int width, height;
        protected int xSpeed, ySpeed;
        protected bool visible;
        protected Image image;
        protected Image[] sequence;
        protected bool containsSequence;
        protected int currentFrame;

        public Sprite()
        {
            width = 32;
            height = 32;
            visible = true;
        }

        public Sprite(string imageName)
            : this()
        {
            LoadImage(imageName);
        }

        public Sprite(string[] imageNames)
            : this()
        {
            LoadSequence(imageNames);
        }

        public void LoadImage(string name)
        {
            image = new Image(name);
            containsSequence = false;
        }

        public void LoadSequence(string[] names)
        {
            int amountOfFrames = names.Length;
            sequence = new Image[amountOfFrames];
            for (int i = 0; i < amountOfFrames; i++)
                sequence[i] = new Image(names[i]);
            containsSequence = true;
            currentFrame = 0;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public int GetSpeedX()
        {
            return xSpeed;
        }

        public int GetSpeedY()
        {
            return ySpeed;
        }

        public bool IsVisible()
        {
            return visible;
        }

        public void MoveTo(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public virtual void Move()
        {
            // To be redefined in subclasses
        }

        public void DrawOnHiddenScreen()
        {
            if (containsSequence)
                Hardware.DrawHiddenImage(sequence[currentFrame], x, y);
            else
                Hardware.DrawHiddenImage(image, x, y);
        }

        public void NextFrame()
        {
            currentFrame++;
            if (currentFrame >= sequence.Length)
                currentFrame = 0;
        }

        public bool CollisionsWith(Sprite otherSprite)
        {
            if (visible && otherSprite.IsVisible() &&
                    (x > otherSprite.GetX() - otherSprite.GetWidth()) &&
                    (x < otherSprite.GetX() + width) &&
                    (y > otherSprite.GetY() - otherSprite.GetHeight()) &&
                    (y < otherSprite.GetY() + height)
                    )
                return true;
            return false;
        }

        public bool CollisionsWith(int xStart, int yStart, int xEnd, int yEnd)
        {
            if (visible && 
                    (x < xEnd) &&    
                    (x + width > xStart) &&
                    (y < yEnd) &&    
                    (y + height > yStart)
                    )
                return true;
            return false;
        }


    }
}
