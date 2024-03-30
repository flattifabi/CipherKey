using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CipherKey.Core.NativeCommands;

public static class WindowsOpenPathCommand
{
    public static void OpenWebPath(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
        }
    }
}