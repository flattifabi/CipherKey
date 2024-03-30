using System.Runtime.InteropServices;

namespace CipherKey.Core.NativeCommands;

public static class WindowsNativeCommands
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public uint type;
        public InputUnion u;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)]
        public MOUSEINPUT mi;

        [FieldOffset(0)]
        public KEYBDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    const int INPUT_KEYBOARD = 1;
    const uint KEYEVENTF_KEYDOWN = 0x0000;
    const uint KEYEVENTF_KEYUP = 0x0002;
    const ushort VK_TAB = 0x09;
    const ushort VK_ENTER = 0x0D;

    static void Main(string[] args)
    {
        SimulateKeyPress(VK_TAB);
        SimulateKeyPress(VK_ENTER);
    }

    static void SimulateKeyPress(ushort keyCode)
    {
        INPUT[] inputs = new INPUT[1];
        inputs[0] = new INPUT();
        inputs[0].type = INPUT_KEYBOARD;
        inputs[0].u.ki = new KEYBDINPUT();
        inputs[0].u.ki.wVk = keyCode;
        inputs[0].u.ki.dwFlags = KEYEVENTF_KEYDOWN;

        SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));

        System.Threading.Thread.Sleep(100);

        inputs[0].u.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }
}