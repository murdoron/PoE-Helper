using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestConsole {
    class Program {
        /*
        // https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static bool ctrlPressed = false;

        public static void Main() {
            _hookID = SetHook(_proc);
            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc) {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule) {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);

            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN) {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((Keys)vkCode == Keys.LControlKey) {
                    ctrlPressed = true;
                } 

                if (ctrlPressed && (Keys)vkCode == Keys.D) {
                    Console.WriteLine("yay!");
                    ctrlPressed = false;
                } else {
                    ctrlPressed = false;
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        */


        [STAThread]
        static void Main(string[] args) {

            IntPtr poeHandle = FindWindow("POEWindowClass", "Path of Exile");

            //Process[] processes = Process.GetProcessesByName("Path of Exile");
            //Process game1 = processes[0];

            //IntPtr poeHandle = game1.MainWindowHandle;

            // Verify that Calculator is a running process.
            if (poeHandle == IntPtr.Zero) {
                //MessageBox.Show("Calculator is not running.");
                Console.WriteLine("PoE läuft nicht");
                return;
            } else {
                Console.WriteLine("PoE läuft!!");
                Console.ReadKey();
                SetForegroundWindow(poeHandle);
                // Clipboard.SetText("test");
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("^{v}");
                SendKeys.SendWait("{ENTER}");
                //SendKeys.SendWait("=");
                Console.ReadKey();
            }

            // Make Calculator the foreground application and send it 
            // a set of calculations.
            //SetForegroundWindow(poeHandle);
            //SendKeys.SendWait("111");
            //SendKeys.SendWait("*");
            //SendKeys.SendWait("11");
            //SendKeys.SendWait("=");

            //ReadTail("C:\\Users\\Andreas\\Desktop\\Client.txt");
            //ReadTail("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Path of Exile\\logs\\Client.txt");
        }

        static void ReadTail(string filename) {
            while (true) {
                using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    // Seek 1024 bytes from the end of the file
                    fs.Seek(-1024, SeekOrigin.End);

                    //fs.Seek(SeekOrigin.)

                    // read 1024 bytes
                    byte[] bytes = new byte[1024];
                    fs.Read(bytes, 0, 1024);
                    // Convert bytes to string
                    string s = Encoding.Default.GetString(bytes);
                    // or string s = Encoding.UTF8.GetString(bytes);
                    // and output to console
                    Console.WriteLine(s);
                }

                Thread.Sleep(1000);
            }
        }

        public static void MonitorTailOfFile(string filePath) {
            var initialFileSize = new FileInfo(filePath).Length;
            var lastReadLength = initialFileSize - 1024;
            if (lastReadLength < 0)
                lastReadLength = 0;

            while (true) {
                try {
                    var fileSize = new FileInfo(filePath).Length;
                    if (fileSize > lastReadLength) {
                        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                            fs.Seek(lastReadLength, SeekOrigin.Begin);
                            var buffer = new byte[1024];

                            while (true) {
                                var bytesRead = fs.Read(buffer, 0, buffer.Length);
                                lastReadLength += bytesRead;

                                if (bytesRead == 0)
                                    break;

                                // var text = ASCIIEncoding.ASCII.GetString(buffer, 0, bytesRead);
                                var text = UTF8Encoding.UTF8.GetString(buffer, 0, bytesRead);
                                Console.Write(text);
                            }
                        }
                    }
                } catch { }

                Thread.Sleep(1000);
            }
        }

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
