/**
 * Intro.cs - Welcome screen and main menu
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: User can enter the Help Screen, Credits screen or a Game
 * 0.05, 28-jan-2013: Animation (pac+ghost)
 */

namespace Game
{
    class Intro
    {
        public void Run()
        {
            Font sans18 = new Font("data/Joystix.ttf", 18);
            Image background = new Image("data/background.png");
            int x = -40;
            Player myPlayer = new Player();
            Enemy myGhost = new Enemy();

            do
            {
                Hardware.ClearScreen();

                Hardware.DrawHiddenImage(background, 0, 0);

                Hardware.WriteHiddenText("Hit SPACE to start",
                    300, 400,
                    0xCC, 0xCC, 0xCC,
                    sans18);
                Hardware.WriteHiddenText("or H for Help",
                    330, 440,
                    0xAA, 0xAA, 0xAA,
                    sans18);
                Hardware.WriteHiddenText("or C for Credits",
                    310, 480,
                    0x88, 0x88, 0x88,
                    sans18);
                Hardware.WriteHiddenText("or Q to Quit",
                    340, 520,
                    0x66, 0x66, 0x66,
                    sans18);

                myGhost.MoveTo(x-50, 300);
                myGhost.NextFrame();
                myGhost.DrawOnHiddenScreen();
                myPlayer.MoveTo(x, 300);
                myPlayer.NextFrame();
                myPlayer.DrawOnHiddenScreen();

                x += 8;
                if (x > 850) 
                    x = -40;
                
                Hardware.ShowHiddenScreen();

                Hardware.Pause(40);

                if (Hardware.KeyPressed(Hardware.KEY_C))
                {
                    Credits creditsScreen = new Credits();
                    creditsScreen.Run();
                }
                
                if (Hardware.KeyPressed(Hardware.KEY_H))
                {
                    Help helpScreen = new Help();
                    helpScreen.Run();
                }

                if (Hardware.KeyPressed(Hardware.KEY_SPC))
                {
                    Game myGame = new Game();
                    myGame.Run();
                }
            }
            while (!  Hardware.KeyPressed(Hardware.KEY_Q) );
        }
    }
}
