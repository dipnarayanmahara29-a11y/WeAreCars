namespace WeAreCars
{
    partial class WelcomeScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeScreen));
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            picEndFrame = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEndFrame).BeginInit();
            SuspendLayout();
            // 
            // axWindowsMediaPlayer
            // 
            axWindowsMediaPlayer.Dock = DockStyle.Fill;
            axWindowsMediaPlayer.Enabled = true;
            axWindowsMediaPlayer.Location = new Point(0, 0);
            axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            axWindowsMediaPlayer.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer.OcxState");
            axWindowsMediaPlayer.Size = new Size(800, 450);
            axWindowsMediaPlayer.TabIndex = 0;
            axWindowsMediaPlayer.TabStop = false;
            // 
            // picEndFrame
            // 
            picEndFrame.Dock = DockStyle.Fill;
            picEndFrame.Location = new Point(0, 0);
            picEndFrame.Name = "picEndFrame";
            picEndFrame.Size = new Size(800, 450);
            picEndFrame.SizeMode = PictureBoxSizeMode.Zoom;
            picEndFrame.TabIndex = 1;
            picEndFrame.TabStop = false;
            picEndFrame.Visible = false;
            picEndFrame.Click += picEndFrame_Click;
            // 
            // WelcomeScreen
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(11, 18, 32);
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(picEndFrame);
            Controls.Add(axWindowsMediaPlayer);
            DoubleBuffered = true;
            ForeColor = Color.FromArgb(248, 250, 252);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WelcomeScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WeAreCars";
            WindowState = FormWindowState.Maximized;
            Load += WelcomeScreen_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEndFrame).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private PictureBox picEndFrame;
    }
}
