using System.Drawing;
using System.Windows.Forms;

namespace POE_Helper {
    public partial class Overlay : Form {
        #region vars
        private bool _moveMode;
        private bool _resizeMode;
        private bool MouseClicked;
        private Point MouseDownLocation;

        #endregion

        public bool ResizeMode {
            get {
                return _resizeMode;
            }
            set {
                _resizeMode = value;
                pbResize.Visible = value;
            }
        }

        public bool MoveMode {
            get {
                return _moveMode;
            }
            set {
                if (value) {
                    pDemo.BackColor = Color.DarkGreen;
                    pDemo.Cursor = Cursors.SizeAll;
                } else {
                    pDemo.BackColor = Color.Lime;
                    pDemo.Cursor = Cursors.Default;
                }

                _moveMode = value;
            }
        }


        // Stash 12 x 12 
        // Quadstash 24 x 24

        public Overlay() {
            InitializeComponent();

            Bounds = Screen.PrimaryScreen.Bounds;
            ResizeMode = false;
            TopMost = true;
            ShowInTaskbar = false;
            MoveMode = false;
            MouseClicked = false;
        }

        private void pDemo_MouseDown(object sender, MouseEventArgs e) {
            if (MoveMode && e.Button == MouseButtons.Left) {
                MouseDownLocation = e.Location;
            }
        }

        private void pDemo_MouseMove(object sender, MouseEventArgs e) {
            if (MoveMode && e.Button == MouseButtons.Left) {
                pDemo.Left = e.X + pDemo.Left - MouseDownLocation.X;
                pDemo.Top = e.Y + pDemo.Top - MouseDownLocation.Y;
            }
        }

        private void pbResize_MouseDown(object sender, MouseEventArgs e) {
            if (ResizeMode) {
                MouseClicked = true;
            }
        }

        private void pbResize_MouseUp(object sender, MouseEventArgs e) {
            if (ResizeMode) {
                MouseClicked = false;
            }
        }

        private void pbResize_MouseMove(object sender, MouseEventArgs e) {
            if (ResizeMode && MouseClicked) {
                pDemo.Height = pbResize.Top + e.Y;
                pDemo.Width = pbResize.Left + e.X;
            }
        }
    }
}
