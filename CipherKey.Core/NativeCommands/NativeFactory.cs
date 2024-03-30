using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CipherKey.Core.NativeCommands;

public static class NativeFactory
{
    const uint WM_KEYDOWN = 0x0100;
    const uint WM_KEYUP = 0x0101;
    const uint WM_CHAR = 0x0102;
    const uint INPUT_KEYBOARD = 1;
    const uint KEYEVENTF_UNICODE = 4;
    const uint KEYEVENTF_KEYUP = 2;
    
    public static void SendKey(NativeMethods.VirtualKey key)
    {
        IntPtr hWnd = IntPtr.Zero;
        IntPtr wParam = (IntPtr)key;
        IntPtr lParam = IntPtr.Zero;
        
        WindowsNativeCommands.SendMessage(hWnd, WM_KEYDOWN, wParam, lParam);
        WindowsNativeCommands.SendMessage(hWnd, WM_KEYUP, wParam, lParam);
    }
    public static void SendKeys(string text)
    {
        foreach (char c in text)
        {
            NativeMethods.INPUT[] inputs = new NativeMethods.INPUT[4];
            inputs[0] = new NativeMethods.INPUT();
            inputs[0].type = INPUT_KEYBOARD;

            inputs[1] = new NativeMethods.INPUT();
            inputs[1].type = INPUT_KEYBOARD;
            inputs[1].U.ki.wVk = c;
            inputs[1].U.ki.dwFlags = KEYEVENTF_UNICODE;

            inputs[2] = new NativeMethods.INPUT();
            inputs[2].type = INPUT_KEYBOARD;
            inputs[2].U.ki.wVk = c;
            inputs[2].U.ki.dwFlags = KEYEVENTF_UNICODE | KEYEVENTF_KEYUP;

            inputs[3] = new NativeMethods.INPUT();
            inputs[3].type = INPUT_KEYBOARD;

            WindowsNativeCommands.SendInput(4, inputs, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
        }
    }
}