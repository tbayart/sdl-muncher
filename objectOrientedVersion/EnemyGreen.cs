/**
 * EnemyGreen.cs - Green ghost
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
    class EnemyGreen : Enemy
    {
        public EnemyGreen(Level l)
            : base(l)
        {
            LoadSequence(new string[] { "data/ghostGreen.png", 
                "data/ghostGreen2.png" });
            MoveTo(10 * 32, 4 * 32);
        }
    }
}
