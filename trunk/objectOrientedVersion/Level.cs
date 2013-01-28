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
 * 0.05, 28-jan-2013
 *     CanMoveTo(), private IsCollision()
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
        int tileWidth = 32;
        int tileHeight = 32;

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
                        Hardware.DrawHiddenImage(rockImage, column * tileWidth, row * tileHeight);
                    if (map[row][column] == '.')
                        Hardware.DrawHiddenImage(dotImage, column * tileWidth, row * tileHeight);
                }
            }
        }


        private bool IsCollision(
            int x1Start, int y1Start, int x1End, int y1End,
            int x2Start, int y2Start, int x2End, int y2End)
        {
            if ((x1Start < x2End) &&
                    (x1End > x2Start) &&
                    (y1Start < y2End) &&
                    (y1End > y2Start)
                    )
                return true;
            return false;
        }
        
        public bool CanMoveTo(int xStart, int yStart, int xEnd, int yEnd)
        {
            // If it touches any brick, it cannot move to those coordinates
            for (int row = 0; row < 15; row++)
            {
                for (int column = 0; column < 17; column++)
                {
                    if ((map[row][column] == '-')
                        && IsCollision(xStart, yStart, xEnd, yEnd,
                             column * tileWidth, row * tileHeight,
                             (column + 1) * tileWidth, (row + 1) * tileHeight))
                    {
                        return false;
                    }
                }
            }
            // Otherwise, it can move to those coordinates            
            return true;
        }


        public int GetPointsFrom(int xStart, int yStart, int xEnd, int yEnd)
        {
            // If it touches any bot, we remove the dot and get the points
            for (int row = 0; row < 15; row++)
            {
                for (int column = 0; column < 17; column++)
                {
                    if ((map[row][column] == '.')
                        && IsCollision(xStart, yStart, xEnd, yEnd,
                             column * tileWidth, row * tileHeight,
                             (column + 1) * tileWidth, (row + 1) * tileHeight))
                    {
                        map[row] = map[row].Remove(column, 1);
                        map[row] = map[row].Insert(column, " ");
                        return 10;
                    }
                }
            }
            // Otherwise, no points
            return 0;
        }

    }
}
