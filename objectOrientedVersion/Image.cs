/**
 * Image.cs - To hide SDL image handling
 * 
 * Part of SdlMuncher - A PacMan clone using C# and SDL
 * Nacho Cabanes, 2013
 * 
 * Changes:
 * 0.01, 21-dec-2012: Empty skeleton
 * 0.02, 21-dec-2012: Ability to load images
 * 
 */

using System;
using Tao.Sdl;

namespace Game
{
    class Image
    {
        private IntPtr internalPointer;

        public Image(string fileName)  // Constructor
        {
            Load(fileName);
        }

        public void Load(string fileName)
        {
            internalPointer = SdlImage.IMG_Load(fileName);
            if (internalPointer == IntPtr.Zero)
                Hardware.FatalError("Image not found: " + fileName);
        }


        public IntPtr GetPointer()
        {
            return internalPointer;
        }
    }
}
