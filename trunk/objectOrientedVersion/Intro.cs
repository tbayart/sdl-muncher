namespace Game
{
    class Intro
    {
        public void Run()
        {
            Font sans18 = new Font("data/Joystix.ttf", 18);
            Image background = new Image("data/background.png");

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
                Hardware.ShowHiddenScreen();

                Hardware.Pause(20);

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
