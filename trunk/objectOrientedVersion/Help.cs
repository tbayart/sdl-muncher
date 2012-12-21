namespace Game
{
    class Help
    {
        public void Run()
        {
            Font sans18;
            sans18 = new Font("data/Joystix.ttf", 18);

            Hardware.ClearScreen();
            Hardware.WriteHiddenText(" Sorry, No help available yet! ",
                200, 500,
                0xCC, 0xCC, 0xCC,
                sans18);
            Hardware.WriteHiddenText("Hit ESC to return",
                300, 540,
                0x99, 0x99, 0x99,
                sans18);
            Hardware.ShowHiddenScreen();
            do
            {
                Hardware.Pause(20);
            }
            while (!Hardware.KeyPressed(Hardware.KEY_ESC));
        }
    }
}
