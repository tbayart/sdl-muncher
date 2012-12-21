namespace Game
{
    class Program
    {
        private Intro intro;

        public Program()
        {
            bool fullScreen = false;
            Hardware.Init(800, 600, 24, fullScreen);
            
            intro = new Intro();
        }

        public void Run()
        {
            intro.Run();
        }


        static void Main(string[] args)
        {
            Program myGame = new Program();
            myGame.Run();
        }
    }
}
