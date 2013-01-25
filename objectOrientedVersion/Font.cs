/**
 * Font.cs - To hide SDL TTF font handling
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Ability to load fonts
 * 
 */

using System;
using Tao.Sdl;

namespace Game
{
    class Font
    {
        private IntPtr internalPointer;

        public Font(string fileName, short sizePoints)
        {
            Load(fileName, sizePoints);
        }

        public void Load(string fileName, short sizePoints)
        {
            internalPointer = SdlTtf.TTF_OpenFont(fileName, sizePoints);
            if (internalPointer == IntPtr.Zero)
                Hardware.FatalError("Font not found: " + fileName);
        }

        public IntPtr GetPointer()
        {
            return internalPointer;
        }
    }
}
