/**
 * EnemyRed.cs - Red ghost
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
    class EnemyRed : Enemy
    {
        public EnemyRed(Level l): base(l)
        {
            LoadSequence(new string[] { "data/ghostRed.png", 
                "data/ghostRed2.png" });
            MoveTo(4 * 32, 4 * 32);
        }
    }
}
