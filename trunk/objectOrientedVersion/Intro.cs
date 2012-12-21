using System;

namespace GameSkeleton
{
    class Intro
    {
        private Credits credits;
        private Help help;
        private Game game;

        public Intro()
        {
            credits = new Credits();
            help = new Help();
            game = new Game();
        }

        public void Run()
        {
        }
    }
}
