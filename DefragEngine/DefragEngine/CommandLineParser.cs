// Source: http://intellitect.com/converting-command-line-string-to-args-using-commandlinetoargvw-api/

using System;
using System.Runtime.InteropServices;

namespace DefragEngine
{
    public class CommandLineParser
    {
        [DllImport("shell32.dll", SetLastError = true)]
        private static extern IntPtr CommandLineToArgvW(
        [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] CommandLineToArgs(string commandLine, out string executableName)
        {
            int argCount;
            IntPtr result;
            string arg;
            IntPtr pStr;
            result = CommandLineToArgvW(commandLine, out argCount);

            if (result == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }
            else
            {
                try
                {
                    pStr = Marshal.ReadIntPtr(result, 0 * IntPtr.Size);
                    executableName = Marshal.PtrToStringUni(pStr);

                    string[] args = new string[argCount - 1];
                    for (int i = 0; i < args.Length; i++)
                    {
                        pStr = Marshal.ReadIntPtr(result, (i + 1) * IntPtr.Size);
                        arg = Marshal.PtrToStringUni(pStr);
                        args[i] = arg;
                    }

                    return args;
                }
                finally
                {
                    Marshal.FreeHGlobal(result);
                }
            }
        }
    }
}