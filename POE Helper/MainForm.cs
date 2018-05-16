using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Web;

namespace POE_Helper {
    public partial class MainForm : Form {

        public ChromiumWebBrowser chrome;
        private bool _ctrlPressed;
        private PoEMessenger _poE;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public bool CtrlPressed {
            get {
                return _ctrlPressed;
            }

            set {
                _ctrlPressed = value;
            }
        }

        public PoEMessenger PoE {
            get {
                return _poE;
            }

            set {
                _poE = value;
            }
        }

        public MainForm() {
            InitializeComponent();
            // Start the browser after initialize global component
            InitializeChromium();
            // Register an object in javascript named "cefCustomObject" with function of the CefCustomObject class :3
            chrome.RegisterJsObject("cefCustomObject", new CefCustomObject(chrome, this));

            //
            PoE = new PoEMessenger();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeChromium() {
            CefSettings settings = new CefSettings();
            CefSharpSettings.WcfTimeout = TimeSpan.Zero; // FIX - Close app faster TODO test
            CefSharpSettings.LegacyJavascriptBindingEnabled = true; // TODO raus
            string page = string.Format(@"{0}\html-resources\html\main.html", Application.StartupPath);

            if (!File.Exists(page)) {
                MessageBox.Show("Error The html file doesn't exists : " + page);
            }

            Cef.Initialize(settings);
            chrome = new ChromiumWebBrowser(page);
            pContainer.Controls.Add(chrome);

            // Allow the use of local resources in the browser
            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.FileAccessFromFileUrls = CefState.Enabled;
            browserSettings.UniversalAccessFromFileUrls = CefState.Enabled;
            chrome.BrowserSettings = browserSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void sendPoEMessage(string msg) {
            try {
                 PoE.SendChatMsg(msg);
            } catch(Exception e) {
                chrome.ExecuteScriptAsync("showError('PoE läuft nicht','Ist der Pfad zum PoE Verzeichnis gesetzt?');");
                chrome.ExecuteScriptAsync("console.log('" + e.Message + "');");
            }
        }

        public void GetItemInfo() {
            outputText(PoE.GetItemInfo());
        }

        public void outputText(string txt) {
            txt = HttpUtility.JavaScriptStringEncode(txt);
            chrome.ExecuteScriptAsync("showError('','" + txt + "');");
            chrome.ExecuteScriptAsync("console.log('" + txt + "');");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            Cef.Shutdown();

            Properties.Settings.Default.MainFormState = this.WindowState;
            if (this.WindowState == FormWindowState.Normal) {
                // save location and size if the state is normal
                Properties.Settings.Default.MainFormLoaction = this.Location;
                Properties.Settings.Default.MainFormSize = this.Size;
            } else {
                // save the RestoreBounds if the form is minimized or maximized!
                Properties.Settings.Default.MainFormLoaction = this.RestoreBounds.Location;
                Properties.Settings.Default.MainFormSize = this.RestoreBounds.Size;
            }

            Properties.Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            if (Properties.Settings.Default.MainFormSize.Width == 0 || Properties.Settings.Default.MainFormSize.Height == 0) {
                // Default Size
            } else {
                this.WindowState = Properties.Settings.Default.MainFormState;

                if (this.WindowState == FormWindowState.Minimized) {
                    this.WindowState = FormWindowState.Normal;
                }

                this.Location = Properties.Settings.Default.MainFormLoaction;
                this.Size = Properties.Settings.Default.MainFormSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFilterChangedDate() {
            string res = "";
            string filePath = "";

            try {
                res = File.GetLastWriteTimeUtc(filePath).ToString();
            } catch (Exception ex) {
                // TODO
            }

            return res;
        }
    }
}
