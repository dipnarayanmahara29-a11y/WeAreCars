namespace WeAreCars
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (components != null)
                    {
                        components.Dispose();
                    }
                }
                catch
                {
                    // Suppress any resource disposal exceptions from components
                }
            }

            try
            {
                base.Dispose(disposing);
            }
            catch
            {
                // Suppress any disposal exceptions from the base class
                // to ensure the form closes cleanly
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picLoginBackground = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLoginBackground).BeginInit();
            SuspendLayout();
            // 
            // picLoginBackground
            // 
            picLoginBackground.Dock = DockStyle.Fill;
            picLoginBackground.Location = new Point(0, 0);
            picLoginBackground.Name = "picLoginBackground";
            picLoginBackground.Size = new Size(800, 450);
            picLoginBackground.SizeMode = PictureBoxSizeMode.Zoom;
            picLoginBackground.TabIndex = 0;
            picLoginBackground.TabStop = false;
            picLoginBackground.Click += picLoginBackground_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(picLoginBackground);
            Name = "LoginForm";
            Text = "LoginForm";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)picLoginBackground).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picLoginBackground;
    }
}