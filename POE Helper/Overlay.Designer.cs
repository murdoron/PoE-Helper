namespace POE_Helper {
    partial class Overlay {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Overlay));
            this.pDemo = new System.Windows.Forms.Panel();
            this.pbResize = new System.Windows.Forms.PictureBox();
            this.pDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResize)).BeginInit();
            this.SuspendLayout();
            // 
            // pDemo
            // 
            this.pDemo.BackColor = System.Drawing.Color.Lime;
            this.pDemo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pDemo.Controls.Add(this.pbResize);
            this.pDemo.Location = new System.Drawing.Point(68, 74);
            this.pDemo.Name = "pDemo";
            this.pDemo.Size = new System.Drawing.Size(50, 50);
            this.pDemo.TabIndex = 0;
            this.pDemo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pDemo_MouseDown);
            this.pDemo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDemo_MouseMove);
            // 
            // pbResize
            // 
            this.pbResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pbResize.Image = ((System.Drawing.Image)(resources.GetObject("pbResize.Image")));
            this.pbResize.Location = new System.Drawing.Point(16, 15);
            this.pbResize.Name = "pbResize";
            this.pbResize.Size = new System.Drawing.Size(32, 33);
            this.pbResize.TabIndex = 0;
            this.pbResize.TabStop = false;
            this.pbResize.Visible = false;
            this.pbResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbResize_MouseDown);
            this.pbResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResize_MouseMove);
            this.pbResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbResize_MouseUp);
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pDemo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Overlay";
            this.Text = "Overlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.pDemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbResize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pDemo;
        private System.Windows.Forms.PictureBox pbResize;
    }
}