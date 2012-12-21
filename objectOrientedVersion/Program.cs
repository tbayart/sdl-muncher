//Angel Gonzalez

using System;

namespace GameSkeleton
{
    class Program
    {
        private Intro intro;

        public Program()
        {
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
