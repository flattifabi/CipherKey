using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CipherKey.Core.NativeCommands;

public static class WindowsNativeCommands
{
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetDllDirectory(string lpPathName);

		[DllImport("Wer.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int WerAddExcludedApplication(string pwzExeName,
			[MarshalAs(UnmanagedType.Bool)] bool bAllUsers);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWindow(IntPtr hWnd);

		[DllImport("User32.dll")]
		internal static extern IntPtr SendMessage(IntPtr hWnd, int nMsg,
			IntPtr wParam, IntPtr lParam);

		

		// [DllImport("User32.dll", EntryPoint = "SendMessage")]
		// private static extern IntPtr SendMessageLVGroup(IntPtr hWnd, int nMsg,
		//	IntPtr wParam, ref LVGROUP lvGroup);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern IntPtr SendMessageTimeout(IntPtr hWnd, int nMsg,
			IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout,
			ref IntPtr lpdwResult);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PostMessage(IntPtr hWnd, int nMsg,
			IntPtr wParam, IntPtr lParam);

		// [DllImport("User32.dll")]
		// internal static extern uint GetMessagePos();

		[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern int RegisterWindowMessage(string lpString);

		// [DllImport("User32.dll")]
		// internal static extern IntPtr GetDesktopWindow();

		[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr FindWindowEx(IntPtr hwndParent,
			IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("User32.dll")]
		internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		// [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
		// internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
		private static extern IntPtr GetClassLong(IntPtr hWnd, int nIndex);

		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
		private static extern IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex);

		// [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		// private static extern int GetClassName(IntPtr hWnd,
		//	StringBuilder lpClassName, int nMaxCount);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsIconic(IntPtr hWnd);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsZoomed(IntPtr hWnd);

		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		// [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
		// private static extern int GetWindowText(IntPtr hWnd,
		//	StringBuilder lpString, int nMaxCount);
		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, IntPtr lpString,
			int nMaxCount);

		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWindowText(IntPtr hWnd, string lpString);

		

		// [DllImport("User32.dll")]
		// internal static extern IntPtr GetActiveWindow();

		[DllImport("User32.dll")]
		private static extern IntPtr GetForegroundWindow(); // Private, is wrapped

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

	
		

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RegisterHotKey(IntPtr hWnd, int id,
			uint fsModifiers, uint vk);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		

	

		[DllImport("User32.dll")]
		internal static extern IntPtr GetMessageExtraInfo();

		// [DllImport("User32.dll")]
		// internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags,
		//	IntPtr dwExtraInfo);

		[DllImport("User32.dll")]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport("User32.dll")]
		private static extern uint MapVirtualKeyEx(uint uCode, uint uMapType,
			IntPtr hKL);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		private static extern ushort VkKeyScan(char ch);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		private static extern ushort VkKeyScanEx(char ch, IntPtr hKL);

	

	

		// [DllImport("User32.dll")]
		// [return: MarshalAs(UnmanagedType.Bool)]
		// private static extern bool GetKeyboardState(IntPtr lpKeyState);

		// [DllImport("User32.dll", CharSet = CharSet.Auto)]
		// [return: MarshalAs(UnmanagedType.Bool)]
		// private static extern bool GetKeyboardLayoutName([MarshalAs(UnmanagedType.LPTStr)]
		//	StringBuilder pwszKLID);

		[DllImport("User32.dll")]
		internal static extern ushort GetKeyState(int vKey);

		[DllImport("User32.dll")]
		internal static extern ushort GetAsyncKeyState(int vKey);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)]
			bool fBlockIt);

		// [DllImport("User32.dll")]
		// [return: MarshalAs(UnmanagedType.Bool)]
		// internal static extern bool AttachThreadInput(uint idAttach,
		//	uint idAttachTo, [MarshalAs(UnmanagedType.Bool)] bool fAttach);

		[DllImport("User32.dll")]
		internal static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ChangeClipboardChain(IntPtr hWndRemove,
			IntPtr hWndNewNext);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EmptyClipboard();

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseClipboard();

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

		// [DllImport("User32.dll", SetLastError = true)]
		// internal static extern IntPtr GetClipboardData(uint uFormat);

		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		internal static extern uint RegisterClipboardFormat(string lpszFormat);

		// [DllImport("User32.dll")]
		// internal static extern uint GetClipboardSequenceNumber();

		// [DllImport("User32.dll")]
		// internal static extern IntPtr GetClipboardOwner();

		[DllImport("Kernel32.dll")]
		internal static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

		[DllImport("Kernel32.dll")]
		internal static extern IntPtr GlobalFree(IntPtr hMem);

		[DllImport("Kernel32.dll")]
		internal static extern IntPtr GlobalLock(IntPtr hMem);

		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalUnlock(IntPtr hMem);

		[DllImport("Kernel32.dll")]
		internal static extern UIntPtr GlobalSize(IntPtr hMem);

	



		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseDesktop(IntPtr hDesktop);

		// [DllImport("User32.dll", SetLastError = true)]
		// internal static extern IntPtr OpenDesktop(string lpszDesktop,
		//	UInt32 dwFlags, [MarshalAs(UnmanagedType.Bool)] bool fInherit,
		//	[MarshalAs(UnmanagedType.U4)] DesktopFlags dwDesiredAccess);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SwitchDesktop(IntPtr hDesktop);


		[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetUserObjectInformation(IntPtr hObj,
			int nIndex, IntPtr pvInfo, uint nLength, ref uint lpnLengthNeeded);

		[DllImport("User32.dll", SetLastError = true)]
		internal static extern IntPtr GetThreadDesktop(uint dwThreadId);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetThreadDesktop(IntPtr hDesktop);

		[DllImport("Kernel32.dll")]
		internal static extern uint GetCurrentThreadId();

		[DllImport("Imm32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ImmDisableIME(uint idThread);

		// [DllImport("Imm32.dll")]
		// internal static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);


		[DllImport("Kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr hObject);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		internal static extern uint GetFileAttributes(string lpFileName);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = false,
			SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteFile(string lpFileName);

		[DllImport("Kernel32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode,
			IntPtr lpInBuffer, uint nInBufferSize, IntPtr lpOutBuffer, uint nOutBufferSize,
			out uint lpBytesReturned, IntPtr lpOverlapped);


		[DllImport("UxTheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
		internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
			string pszSubIdList);

		[DllImport("Shell32.dll")]
		internal static extern void SHChangeNotify(int wEventId, uint uFlags,
			IntPtr dwItem1, IntPtr dwItem2);

		// [DllImport("Shell32.dll")]
		// internal static extern uint SHChangeNotifyRegister(IntPtr hwnd, int fSources,
		//	int fEvents, uint wMsg, int cEntries, ref SHCHANGENOTIFYENTRY pshcne);

		// [DllImport("Shell32.dll")]
		// [return: MarshalAs(UnmanagedType.Bool)]
		// internal static extern bool SHChangeNotifyDeregister(uint ulID);

	

		// [DllImport("User32.dll")]
		// private static extern int SetScrollInfo(IntPtr hwnd, int fnBar,
		//	[In] ref SCROLLINFO lpsi, [MarshalAs(UnmanagedType.Bool)] bool fRedraw);

		// [DllImport("User32.dll")]
		// private static extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy,
		//	IntPtr prcScroll, IntPtr prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate,
		//	uint flags);

		[DllImport("User32.dll")]
		internal static extern IntPtr GetKeyboardLayout(uint idThread);

		[DllImport("User32.dll")]
		internal static extern IntPtr ActivateKeyboardLayout(IntPtr hkl, uint uFlags);

		[DllImport("User32.dll")]
		internal static extern uint GetWindowThreadProcessId(IntPtr hWnd,
			[Out] out uint lpdwProcessId);

		// [DllImport("UxTheme.dll")]
		// internal static extern IntPtr OpenThemeData(IntPtr hWnd,
		//	[MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

		// [DllImport("UxTheme.dll")]
		// internal static extern uint CloseThemeData(IntPtr hTheme);

		// [DllImport("UxTheme.dll")]
		// internal extern static uint DrawThemeBackground(IntPtr hTheme, IntPtr hdc,
		//	int iPartId, int iStateId, ref RECT pRect, ref RECT pClipRect);	

		[DllImport("Gdi32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeleteObject(IntPtr hObject);



		[DllImport("ComCtl32.dll")]
		internal static extern int LoadIconWithScaleDown(IntPtr hInst,
			IntPtr pszName, int cx, int cy, ref IntPtr phIco);

		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DestroyIcon(IntPtr hIcon);

		// [DllImport("User32.dll", SetLastError = true)]
		// [return: MarshalAs(UnmanagedType.Bool)]
		// internal static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop,
		//	IntPtr hIcon, int cxWidth, int cyWidth, uint istepIfAniCur,
		//	IntPtr hbrFlickerFreeDraw, uint diFlags);

		[DllImport("User32.dll")]
		internal static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("User32.dll")]
		internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("Gdi32.dll")]
		internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

		[DllImport("WinMM.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PlaySound(string pszSound, IntPtr hmod,
			uint fdwSound);

		// [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
		// internal static extern IntPtr ShellExecute(IntPtr hwnd,
		//	string lpOperation, string lpFile, string lpParameters,
		//	string lpDirectory, int nShowCmd);


		[DllImport("User32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetProcessDPIAware();





		// [DllImport("Kernel32.dll", SetLastError = true)]
		// internal static extern IntPtr OpenProcess(uint dwDesiredAccess,
		//	[MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

		// [DllImport("User32.dll")]
		// [return: MarshalAs(UnmanagedType.Bool)]
		// internal static extern bool IsImmersiveProcess(IntPtr hProcess);
		
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint nInputs, NativeMethods.INPUT[] pInputs, int cbSize);
		
		[DllImport("Kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ActivateActCtx(IntPtr hActCtx,
			ref UIntPtr lpCookie);

		[DllImport("Kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DeactivateActCtx(uint dwFlags,
			UIntPtr ulCookie);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ShutdownBlockReasonCreate(IntPtr hWnd,
			[MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);

		// [DllImport("User32.dll")]
		// internal static extern int GetSystemMetrics(int nIndex);

		[DllImport("User32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWindowDisplayAffinity(IntPtr hWnd,
			uint dwAffinity);

		[DllImport("User32.dll", EntryPoint = "SystemParametersInfo",
			CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SystemParametersInfoI32(uint uiAction,
			uint uiParam, ref int pvParam, uint fWinIni);
	
}