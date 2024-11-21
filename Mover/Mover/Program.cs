using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern void SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;

    static void Main()
    {
        while (true)
        {
            // Set cursor position to bottom-left corner
            SetCursorPos(10, 1080); // Adjust "1080" if screen height differs

            // Perform a left-click
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

            // Wait for 1 minute
            Thread.Sleep(60000);
        }
    }
}