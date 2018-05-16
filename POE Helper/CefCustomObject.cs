using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;
using System.Windows.Forms;

namespace POE_Helper {
    class CefCustomObject {
        // Declare a local instance of chromium and the main form in order to execute things from here in the main thread
        private static ChromiumWebBrowser _instanceBrowser = null;
        // The form class needs to be changed according to yours
        private static MainForm _instanceMainForm = null;


        public CefCustomObject(ChromiumWebBrowser originalBrowser, MainForm mainForm) {
            _instanceBrowser = originalBrowser;
            _instanceMainForm = mainForm;
        }

        public void showDevTools() {
            _instanceBrowser.ShowDevTools();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void sendPoEMessage(string msg) {
            _instanceMainForm.sendPoEMessage(msg);
        }

        public void test() {
            MessageBox.Show("Test");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getFilterChangedDate() {
            return _instanceMainForm.GetFilterChangedDate();
        }
    }
}