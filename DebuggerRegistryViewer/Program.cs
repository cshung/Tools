namespace DebuggerRegistryViewer
{
    using System;
    using Microsoft.Win32;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options");
            string[] processes = sk1.GetSubKeyNames();
            foreach (var process in processes)
            {
                RegistryKey sk2 = sk1.OpenSubKey(process);
                object debugger = sk2.GetValue("Debugger");
                if (debugger != null)
                {
                    if (!process.EndsWith(","))
                    {
                        Console.WriteLine(process + " is debugged by " + debugger);
                    }
                }
            }
        }
    }
}
