using System;
using System.Runtime.InteropServices;

namespace MediaControlGUI
{
    public static class Controls
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
        
        public static void Pause()
            => keybd_event(0xB3, 0,1, IntPtr.Zero);
            
        public static void Next()
            => keybd_event(0xB0, 0,1, IntPtr.Zero);
        
        public static void Previous()
            => keybd_event(0xB1, 0,1, IntPtr.Zero);
    }
}