/**
 * EnemyPurple.cs - Purple ghost
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
    class EnemyPurple : Enemy
    {
        public EnemyPurple(Level l): base(l)
        {
            LoadSequence(new string[] { "data/ghostPurple.png", 
                "data/ghostPurple2.png" });
            MoveTo(10 * 32, 12 * 32);
        }
    }
}
