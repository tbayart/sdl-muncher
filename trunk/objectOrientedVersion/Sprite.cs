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

        public void Move()
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
