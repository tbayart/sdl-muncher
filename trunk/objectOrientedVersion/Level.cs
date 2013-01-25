/**
 * Level.cs - A game level
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.03, 18-jan-2013
 *     Skeleton: only draws a block
 * 0.04, 25-jan-2013
 *     Draws several blocks, taken from an array
 *     Dots are also drawn
 */

namespace Game
{
    class Level
    {
        static string[] map = 
        {
            "-----------------",
            "-.......-.......-",
            "-.--.--.-.--.--.-",
            "-...............-",
            "-.--.-.---.-.--.-",
            "-....-..-..-....-",
            "----.--...--.----",
            ".................",
            "----.-.-.-.-.----",
            "-....-.-.-.-....-",
            "-.--.-.---.-.--.-",
            "-...............-",
            "-.--.-.---.-.--.-",
            "-....-.....-....-",
            "-----------------"
        };

        Image rockImage;
        Image dotImage;

        public Level()
        {
            rockImage = new Image("data/wall.png");
            dotImage = new Image("data/dot.png");
        }

        public void DrawOnHiddenScreen()
        {
            // Background map
            for (int row = 0; row < 15; row++)
            {
                for (int column = 0; column < 17; column++)
                {
                    if (map[row][column] == '-')
                        Hardware.DrawHiddenImage(rockImage, column * 32, row * 32);
                    if (map[row][column] == '.')
                        Hardware.DrawHiddenImage(dotImage, column * 32, row * 32);
                }
            }
        }
    }
}
