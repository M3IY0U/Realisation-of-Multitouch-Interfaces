using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MediaControlGUI
{
    public static class Controls
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        public static void Pause()
            => keybd_event((byte) Keys.MediaPlayPause, 0,1, IntPtr.Zero);
        public static void Next()
            => keybd_event((byte) Keys.MediaNextTrack, 0,1, IntPtr.Zero);
        
        public static void Previous()
            => keybd_event((byte) Keys.MediaPreviousTrack, 0,1, IntPtr.Zero);

        public static void VolumeUp()
            => keybd_event((byte) Keys.VolumeUp, 0, 0, IntPtr.Zero);
        public static void VolumeDown()
            => keybd_event((byte) Keys.VolumeDown, 0, 0, IntPtr.Zero);
    }
}