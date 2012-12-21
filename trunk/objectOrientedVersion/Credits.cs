namespace Game
{
    class Credits
    {
        public Credits()
        {
        }

        public void Run()
        {
            Font sans18;
            sans18 = new Font("data/Joystix.ttf", 18);

            Hardware.ClearScreen();
            Hardware.WriteHiddenText("By DAM Ies San Vicente 2012-2013",
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
