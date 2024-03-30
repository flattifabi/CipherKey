using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CipherKey.Core.NativeCommands;

public static class WindowsOpenPathCommand
{
    public static void OpenWebPath(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
        }
        // Process[] chromeProcesses = Process.GetProcessesByName("chrome");
        // Process[] firefoxProcesses = Process.GetProcessesByName("firefox");
        // Process[] operaProcesses = Process.GetProcessesByName("opera");
        //
        // if (chromeProcesses.Length > 0)
        // {
        //     foreach (Process chromeProcess in chromeProcesses)
        //     {
        //         Process.Start(url);
        //     }
        // }
        //
        // if (firefoxProcesses.Length > 0)
        // {
        //     foreach (Process firefoxProcess in firefoxProcesses)
        //     {
        //         if (firefoxProcess.MainWindowHandle != IntPtr.Zero)
        //         {
        //             SendKeys.SendWait("^t");
        //             SendKeys.SendWait(url);
        //             return;
        //         }
        //     }
        // }
        //
        // if (operaProcesses.Length > 0)
        // {
        //     foreach (Process operaProcess in operaProcesses)
        //     {
        //         if (operaProcess.MainWindowHandle != IntPtr.Zero)
        //         {
        //             SendKeys.SendWait("^t");
        //             SendKeys.SendWait(url);
        //             return;
        //         }
        //     }
        // }
        // else if (firefoxProcesses.Length > 0)
        // {
        //     Process.Start("firefox", $"-new-tab {url}");
        // }
        // else if (operaProcesses.Length > 0)
        // {
        //     Process.Start("opera", $"--new-tab {url}");
        // }
        // else
        // {
        //     Process.Start(url);
        // }
    }
}