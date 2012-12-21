namespace Game
{
    class Enemy:Sprite
    {
        public Enemy()
        {
            LoadImage("data/ghostGreen.png");
            MoveTo(300, 100);
        }
    }
}
