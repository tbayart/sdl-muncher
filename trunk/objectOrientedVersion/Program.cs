/**
 * Program.cs - Game loader
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Enters graphics mode and launchs the intro
 * 
 */

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
