using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace POE_Helper {
    public class PoEMessager {

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private IntPtr _poeHandle;

        public IntPtr PoeHandle {
            get {
                return _poeHandle;
            }

            set {
                _poeHandle = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PoEMessager() {
            PoeHandle = FindWindow("POEWindowClass", "Path of Exile");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void SendChatMsg(string msg) {
            if (IsPoERunning()) {

                SetForegroundWindow(PoeHandle);

                Thread t = new Thread(SetClipboardSetText);
                t.SetApartmentState(ApartmentState.STA);
                t.Start(msg);
                
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait("^{v}");
                SendKeys.SendWait("{ENTER}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetItemInfo() {
            string ret = "";
            if (IsPoERunning()) {
                SetForegroundWindow(PoeHandle);

                SendKeys.SendWait("^{c}");

                Thread staThread = new Thread(
                    delegate () {
                        try {
                            ret = Clipboard.GetText();
                        } catch (Exception ex) {
                            
                        }
                    });
                staThread.SetApartmentState(ApartmentState.STA);
                staThread.Start();
                staThread.Join();
            }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        private bool IsPoERunning() {
            if (PoeHandle == IntPtr.Zero) {
                // Check again
                PoeHandle = FindWindow("POEWindowClass", "Path of Exile");

                // still not found throw error
                if (PoeHandle == IntPtr.Zero) {
                    throw new Exception("PoE läuft nicht");
                } else {
                    return true;
                }
            } else {
                return true;
            }
        }


        //Separate method to set data
        private void SetClipboardSetText(Object state) {
            String text = state as String;
            Clipboard.SetText(text, TextDataFormat.Text);
        }

    }
}
