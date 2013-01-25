/**
 * Level.cs - A game level
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.03, 18-jan-2013
 *     Skeleton: only draws a block
 */

namespace Game
{
    class Level
    {
        Image rock;

        public Level()
        {
            rock = new Image("data/wall.png");
        }

        public void DrawOnHiddenScreen()
        {
            Hardware.DrawHiddenImage(rock, 10, 10);
        }
    }
}
