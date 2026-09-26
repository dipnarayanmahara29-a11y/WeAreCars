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
        private Label lblLoginSubtitle;
        private TextBox txtUsername;
        private TextBox txtPassword;                
        private Label lblUsername;
        private Label lblPassword;
        

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

            // Login Input Controls
            CreateLoginInputs();

            // Login Field Labels
            CreateLoginLabels();

            // Login Title
            CreateLoginTitel();

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
                Size = new Size(320, 420),

                // Premium dark background
                BackColor = Color.FromArgb(17, 25, 39),

                BorderStyle = BorderStyle.None
            };

            // Add card to the main form
            this.Controls.Add(pnlLoginCard);

            // Keep card above background
            pnlLoginCard.BringToFront();

            // Rounded professional corners
            SetRoundedCorners(pnlLoginCard, 24);

            // Draw professional border and accent
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
                    16,
                    FontStyle.Bold
                ),

                Location = new Point(85, 20)
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
            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            int radius = 24;

            // -----------------------------
            // Subtle outer border
            // -----------------------------

            using (Pen borderPen = new Pen(
                Color.FromArgb(55, 70, 95),
                1))
            {
                using (GraphicsPath borderPath =
                    new GraphicsPath())
                {
                    borderPath.AddArc(
                        0,
                        0,
                        radius,
                        radius,
                        180,
                        90);

                    borderPath.AddArc(
                        pnlLoginCard.Width - radius - 1,
                        0,
                        radius,
                        radius,
                        270,
                        90);

                    borderPath.AddArc(
                        pnlLoginCard.Width - radius - 1,
                        pnlLoginCard.Height - radius - 1,
                        radius,
                        radius,
                        0,
                        90);

                    borderPath.AddArc(
                        0,
                        pnlLoginCard.Height - radius - 1,
                        radius,
                        radius,
                        90,
                        90);

                    borderPath.CloseFigure();

                    e.Graphics.DrawPath(
                        borderPen,
                        borderPath);
                }
            }

            // -----------------------------
            // Accent line (positioned under the brand label)
            // -----------------------------

            using (Pen accentPen = new Pen(
                Color.FromArgb(45, 170, 255),
                3))
            {
                if (lblBrand != null)
                {
                    // Draw the accent line centered under the brand label.
                    int lineWidth = 50;
                    int startX = (pnlLoginCard.Width - lineWidth) / 2;
                    int endX = startX + lineWidth;
                    int y = lblBrand.Bottom + 12;

                    e.Graphics.DrawLine(
                        accentPen,
                        startX,
                        y,
                        endX,
                        y
                        );
                }
                else
                {
                    // Fallback to original hard-coded position if lblBrand isn't available yet.
                    int lineWidth = 50;
                    int startX = (pnlLoginCard.Width - lineWidth) / 2;
                    int endX = startX + lineWidth;
                    e.Graphics.DrawLine(accentPen, startX, 60, endX, 60);
                }
            }
        }
               
        // Login Inputs Controls

        private void CreateLoginInputs()
        {
            // username textbox
            txtUsername = new TextBox();
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(240, 35);
            txtUsername.Location = new Point(40, 215);

            // Password textbox
            txtPassword = new TextBox();
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(240, 35);
            txtPassword.Location = new Point(40, 285);
            txtPassword.UseSystemPasswordChar = true;

            // Add textboxes INSIDE the login card
            pnlLoginCard.Controls.Add(txtUsername);
            pnlLoginCard.Controls.Add(txtPassword);

            // Make sure they appear above the other controls
            txtUsername.BringToFront();
            txtPassword.BringToFront();

        }

        // Login Fields label

        private void CreateLoginLabels()
        {
            // Username label
            lblUsername = new Label
            {
                Name = "lblUsername",
                Text = "Username",
                AutoSize = true,
                Location = new Point(40, 190),
                Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular
                ),
                ForeColor = Color.White,
                BackColor = Color.Transparent 
            };

            // Password Label
            lblPassword = new Label
            {
                Name = "lblPassword",
                Text = "Password",
                AutoSize = true,
                Location = new Point(40, 260),
                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                    ),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            // Add label to the login card
            pnlLoginCard.Controls.Add(lblUsername);
            pnlLoginCard.Controls.Add(lblPassword);

            // keep the label to the front
            lblUsername.BringToFront();
            lblPassword.BringToFront();

        }

        // Login Title and subtitle
        private void CreateLoginTitel()
        {
            // Login Title 
            lblLoginTitle = new Label
            {
                Name = "lblLoginTitle",
                Text = "Staff Login",
                AutoSize = true,
                Location = new Point(60, 70),
                Font = new Font(
                     "Segoe UI",
                    18,
                    FontStyle.Bold
                    ),
                ForeColor = Color.White,
                BackColor = Color.Transparent

            };

            // Login sub-title

            lblLoginSubtitle = new Label
            {
                Name = "lblLoginSubtitle",
                Text = "Sign in to access the WeAreCars management system.",
                AutoSize = false,
                Size = new Size(240, 60),
                Location = new Point(40, 105),
                Font = new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Regular
                    ),
                ForeColor = Color.White,
                BackColor = Color.Transparent

            };

            // Add to the Card
            pnlLoginCard.Controls.Add(lblLoginTitle);
            pnlLoginCard.Controls.Add(lblLoginSubtitle);

            // keep the label to the fron
            lblLoginTitle.BringToFront();
            lblLoginSubtitle.BringToFront();


        }
    }
}