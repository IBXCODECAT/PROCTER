using System;
using System.Runtime.InteropServices;
using UnityEngine;

//https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-messagebox

public static class NativeWinAlert
{

    public struct Modal
    {
        public const long App = 0x00000000L;
        public const long System = 0x00001000L;
        public const long Task = 0x00002000L;
    }

    public struct Type
    {
        public const long Error = 0x00000010L;
        public const long Query = 0x00000020L;
        public const long Warn = 0x00000030L;
        public const long Info = 0x00000040L;
    }

    public struct Prompt
    {
        public const long OK = 0x00000000L;
        public const long OKCancel = 0x00000001L;
        public const long AbortRetryIgnore = 0x00000002L;
        public const long YesNoCancel = 0x00000003L;
        public const long YesNo = 0x00000004L;
        public const long RetryCancel = 0x00000005L;
        public const long CancelRetryContinue = 0x00000006L;
        public const long OptionalHelpButton = 0x00004000L;
    }

    public struct DefaultResponse
    {
        public const long DefaultButton1 = 0x00000000L;
        public const long DefaultButton2 = 0x00000100L;
        public const long DefaultButton3 = 0x00000200L;
        public const long DefaultButton4 = 0x00000300L;
    }

    public struct Response
    {

    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    public static IntPtr GetWindowHandle() { return GetActiveWindow(); }

    [DllImport("user32.dll", SetLastError = true)]
    static extern int externAlert(IntPtr hwnd, string lpText, string lpCaption, uint uType);

    public static void Alert(string content, string title, long modal, long type, long prompt, long defaultResponse)
    {
        try { externAlert(GetWindowHandle(), content, title, (uint)(type | prompt)); } catch (Exception ex) { Debug.LogException(ex); }
    }
}