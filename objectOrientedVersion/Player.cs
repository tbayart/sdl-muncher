namespace Game
{
    class Player : Sprite
    {
        public Player()
        {
            LoadImage("data/pac01r.png");
            MoveTo(200, 200);
            xSpeed = 4;
        }

        public void MoveRight()
        {
            x += xSpeed;
        }

        private void MoveUp()
        {
        }

        private void MoveDown()
        {
        }

        private void MoveLeft()
        {
        }
    }
}
