/**
 * EnemyBlue.cs - Blue ghost
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * 0.09, 01-mar-2013: 
 *     Created. Inherits from Enemy
 */

using System;

namespace Game
{
    class EnemyBlue : Enemy
    {
        public EnemyBlue(Level l)
            : base(l)
        {
            LoadSequence(new string[] { "data/ghostBlue.png", 
                "data/ghostBlue2.png" });
            MoveTo(4 * 32, 12 * 32);
        }
    }
}
