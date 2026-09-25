using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace WeAreCars
{
    public partial class LoginForm : Form
    {
        // ====================================================
        // LOGIN UI
        // ====================================================

        private Panel pnlLoginCard;
        private Label lblBrand;
        private Label lblLoginTitle;

        // ====================================================
        // CONSTRUCTOR
        // ====================================================

        public LoginForm()
        {
            InitializeComponent();

            // Configure the main form.
            ConfigureForm();

            // Create the automotive background.
            CreateBackground();

            // Create the main login card.
            CreateLoginCard();

            // Create the WeAreCars branding.
            CreateLoginBranding();

            // Keep the login card correctly positioned
            // when the form changes size.
            this.Resize += LoginForm_Resize;

            // Position the card after the form has reached
            // its final displayed size.
            this.Shown += LoginForm_Shown;
        }

        // ====================================================
        // FORM EVENTS
        // ====================================================

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void picLoginBackground_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Positions the login card after the form has been
        /// displayed at its actual maximized size.
        /// </summary>
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            PositionLoginCard();
        }

        /// <summary>
        /// Keeps the login card correctly positioned when
        /// the form size changes.
        /// </summary>
        private void LoginForm_Resize(object sender, EventArgs e)
        {
            if (pnlLoginCard != null)
            {
                PositionLoginCard();
            }
        }

        // ====================================================
        // FORM CONFIGURATION
        // ====================================================

        /// <summary>
        /// Configures the main Login form appearance
        /// and window behaviour.
        /// </summary>
        private void ConfigureForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(11, 18, 32);
            this.Text = "WeAreCars - Staff Login";
            this.DoubleBuffered = true;
        }

        // ====================================================
        // BACKGROUND
        // ====================================================

        /// <summary>
        /// Creates and loads the automotive background image.
        /// </summary>
        private void CreateBackground()
        {
            picLoginBackground = new PictureBox();

            // Make the background fill the entire form.
            picLoginBackground.Dock = DockStyle.Fill;

            // Keep the image proportional while filling
            // as much of the available area as possible.
            picLoginBackground.SizeMode = PictureBoxSizeMode.Zoom;

            // Build the path to the login background image.
            string backgroundPath = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                "Login",
                "Login_Background.png"
            );

            // Load the background image.
            picLoginBackground.Image =
                Image.FromFile(backgroundPath);

            // Add the background to the form.
            this.Controls.Add(picLoginBackground);

            // Keep the background behind the login card.
            picLoginBackground.BringToFront();
        }

        // ====================================================
        // LOGIN CARD
        // ====================================================

        /// <summary>
        /// Creates the main login card.
        /// </summary>
        private void CreateLoginCard()
        {
            pnlLoginCard = new Panel
            {
                Size = new Size(570, 630),
                BackColor = Color.FromArgb(19, 29, 46),
                BorderStyle = BorderStyle.None
            };

            // Add the card to the main form.
            this.Controls.Add(pnlLoginCard);

            // Keep the card above the background.
            pnlLoginCard.BringToFront();

            // Apply rounded corners to the card.
            SetRoundedCorners(
                pnlLoginCard,
                18
            );

            // Draw the subtle card border.
            pnlLoginCard.Paint += LoginCard_Paint;
        }

        /// <summary>
        /// Positions the login card on the right side
        /// of the screen.
        /// </summary>
        private void PositionLoginCard()
        {
            if (pnlLoginCard == null)
                return;

            pnlLoginCard.Location = new Point(
                this.ClientSize.Width -
                pnlLoginCard.Width -
                90,

                (this.ClientSize.Height -
                 pnlLoginCard.Height) / 2
            );
        }

        // ====================================================
        // LOGIN BRANDING
        // ====================================================

        /// <summary>
        /// Creates the WeAreCars branding displayed
        /// inside the login card.
        /// </summary>
        private void CreateLoginBranding()
        {
            lblBrand = new Label
            {
                AutoSize = true,
                Text = "WEARECARS",

                ForeColor = Color.White,
                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    19,
                    FontStyle.Bold
                ),

                Location = new Point(50, 42)
            };

            // Add the brand to the login card.
            pnlLoginCard.Controls.Add(lblBrand);
        }

        // ====================================================
        // ROUNDED CORNERS
        // ====================================================

        /// <summary>
        /// Applies rounded corners to a control.
        /// </summary>
        private void SetRoundedCorners(
            Control control,
            int radius)
        {
            GraphicsPath path =
                new GraphicsPath();

            // Top-left corner.
            path.AddArc(
                0,
                0,
                radius,
                radius,
                180,
                90
            );

            // Top-right corner.
            path.AddArc(
                control.Width - radius,
                0,
                radius,
                radius,
                270,
                90
            );

            // Bottom-right corner.
            path.AddArc(
                control.Width - radius,
                control.Height - radius,
                radius,
                radius,
                0,
                90
            );

            // Bottom-left corner.
            path.AddArc(
                0,
                control.Height - radius,
                radius,
                radius,
                90,
                90
            );

            path.CloseFigure();

            control.Region =
                new Region(path);
        }

        // ====================================================
        // CARD BORDER
        // ====================================================

        /// <summary>
        /// Draws the subtle border around the login card.
        /// </summary>
        private void LoginCard_Paint(
            object sender,
            PaintEventArgs e)
        {
            using Pen borderPen =
                new Pen(
                    Color.FromArgb(42, 55, 75),
                    1
                );

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            using GraphicsPath borderPath =
                new GraphicsPath();

            int radius = 18;

            // Top-left corner.
            borderPath.AddArc(
                0,
                0,
                radius,
                radius,
                180,
                90
            );

            // Top-right corner.
            borderPath.AddArc(
                pnlLoginCard.Width - radius - 1,
                0,
                radius,
                radius,
                270,
                90
            );

            // Bottom-right corner.
            borderPath.AddArc(
                pnlLoginCard.Width - radius - 1,
                pnlLoginCard.Height - radius - 1,
                radius,
                radius,
                0,
                90
            );

            // Bottom-left corner.
            borderPath.AddArc(
                0,
                pnlLoginCard.Height - radius - 1,
                radius,
                radius,
                90,
                90
            );

            borderPath.CloseFigure();

            e.Graphics.DrawPath(
                borderPen,
                borderPath
            );
        }
    }
}