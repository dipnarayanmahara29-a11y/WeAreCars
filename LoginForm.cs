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

        private Label lblUsername;
        private Label lblPassword;

        private TextBox txtUsername;
        private TextBox txtPassword;

        private Panel pnlUsernameInput;
        private Panel pnlPasswordInput;

        private Label lblUsernameIcon;
        private Label lblPasswordIcon;

        private Button btnSignIn;
        private Label lblHelp;
        private Label lblFooter;

        // Left side overlay
        private Panel pnlLeftOverlay;
        private Button btnBack;
        private Label lblLeftBrand;
        private Label lblLeftTitle;
        private Label lblLeftTagline;
        private Label lblLeftFooterTitle;
        private Label lblLeftFooterDesc;
        private Panel pnlAccentBar;
        private Button btnClose;


        // ====================================================
        // CONSTRUCTOR
        // ====================================================

        public LoginForm()
        {
            InitializeComponent();

            // Configure form
            ConfigureForm();

            // Create background
            CreateBackground();

            // Create left side overlay content
            CreateLeftOverlay();

            // Create login card
            CreateLoginCard();

            // Create branding
            CreateLoginBranding();

            // Create title and subtitle
            CreateLoginTitel();

            // Create username/password labels
            CreateLoginLabels();

            // Create sign-in button and extras
            CreateSignInSection();

            // Keep card positioned correctly
            this.Resize += LoginForm_Resize;

            // Position card after form is displayed
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


        private void LoginForm_Shown(object sender, EventArgs e)
        {
            PositionLoginCard();
        }


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

        private void CreateBackground()
        {
            // picLoginBackground is created by the Designer
            // (InitializeComponent) and is already docked to
            // fill the form. Only load the image into it.

            string backgroundPath = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                "Login",
                "Login_Background.png"
            );

            if (File.Exists(backgroundPath))
            {
                picLoginBackground.Image =
                    Image.FromFile(backgroundPath);

                picLoginBackground.SizeMode =
                    PictureBoxSizeMode.Zoom;
            }
            else
            {
                this.BackColor =
                    Color.FromArgb(11, 18, 32);
            }

            picLoginBackground.SendToBack();
        }


        // ====================================================
        // LEFT OVERLAY
        // ====================================================

        private void CreateLeftOverlay()
        {
            // Close button (top right)
            btnClose = new Button
            {
                Text = "✕",
                Size = new Size(35, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font(
                    "Segoe UI", 14, FontStyle.Regular),
                Cursor = Cursors.Hand
            };

            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance
                .MouseOverBackColor =
                    Color.FromArgb(40, 50, 70);
            btnClose.FlatAppearance
                .MouseDownBackColor =
                    Color.FromArgb(30, 40, 60);

            btnClose.Click += (s, e) =>
                this.Close();

            this.Controls.Add(btnClose);
            btnClose.BringToFront();


            // Back button (top left)
            btnBack = new Button
            {
                Text = "←  Back",
                Size = new Size(90, 35),
                Location = new Point(20, 15),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Regular),
                Cursor = Cursors.Hand,
                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatAppearance
                .MouseOverBackColor =
                    Color.FromArgb(40, 50, 70);
            btnBack.FlatAppearance
                .MouseDownBackColor =
                    Color.FromArgb(30, 40, 60);

            this.Controls.Add(btnBack);
            btnBack.BringToFront();


            // Left brand
            lblLeftBrand = new Label
            {
                Text = "WEARECARS",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(
                    255, 107, 53),
                BackColor = Color.Transparent,
                Location = new Point(25, 70)
            };

            this.Controls.Add(lblLeftBrand);
            lblLeftBrand.BringToFront();


            // Left title
            lblLeftTitle = new Label
            {
                Text =
                    "VEHICLE RENTAL\n" +
                    "MANAGEMENT SYSTEM",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(25, 105)
            };

            this.Controls.Add(lblLeftTitle);
            lblLeftTitle.BringToFront();


            // Left tagline
            lblLeftTagline = new Label
            {
                Text =
                    "Premium vehicle rental management\n" +
                    "at your fingertips.",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI", 10,
                    FontStyle.Regular),
                ForeColor = Color.FromArgb(
                    170, 180, 195),
                BackColor = Color.Transparent,
                Location = new Point(25, 165)
            };

            this.Controls.Add(lblLeftTagline);
            lblLeftTagline.BringToFront();


            // Accent bar (bottom left)
            pnlAccentBar = new Panel
            {
                Size = new Size(40, 3),
                BackColor = Color.FromArgb(
                    255, 107, 53),
                Location = new Point(25, 0)
            };

            this.Controls.Add(pnlAccentBar);
            pnlAccentBar.BringToFront();


            // Bottom left - footer title
            lblLeftFooterTitle = new Label
            {
                Text = "STAFF MANAGEMENT PORTAL",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(25, 0)
            };

            this.Controls.Add(lblLeftFooterTitle);
            lblLeftFooterTitle.BringToFront();


            // Bottom left - footer description
            lblLeftFooterDesc = new Label
            {
                Text =
                    "Secure access for authorised " +
                    "WeAreCars staff.",
                AutoSize = true,
                Font = new Font(
                    "Segoe UI", 8,
                    FontStyle.Regular),
                ForeColor = Color.FromArgb(
                    140, 150, 170),
                BackColor = Color.Transparent,
                Location = new Point(25, 0)
            };

            this.Controls.Add(lblLeftFooterDesc);
            lblLeftFooterDesc.BringToFront();
        }


        // ====================================================
        // LOGIN CARD
        // ====================================================

        private void CreateLoginCard()
        {
            // ====================================================
            // MAIN LOGIN CARD
            // ====================================================

            pnlLoginCard = new Panel
            {
                Name = "pnlLoginCard",

                Size = new Size(400, 580),

                BackColor =
                    Color.FromArgb(16, 25, 40),

                BorderStyle = BorderStyle.None
            };


            // Add card to form
            this.Controls.Add(pnlLoginCard);


            // Put card above background
            pnlLoginCard.BringToFront();


            // Rounded corners
            SetRoundedCorners(
                pnlLoginCard,
                18
            );


            // Enable border and accent line
            pnlLoginCard.Paint += LoginCard_Paint;


            // ====================================================
            // USERNAME INPUT PANEL
            // ====================================================

            pnlUsernameInput = new Panel
            {
                Name = "pnlUsernameInput",

                Size = new Size(300, 45),

                Location = new Point(50, 210),

                BackColor =
                    Color.FromArgb(30, 42, 62)
            };


            SetRoundedCorners(
                pnlUsernameInput,
                10
            );


            // ====================================================
            // USERNAME ICON
            // ====================================================

            lblUsernameIcon = new Label
            {
                Name = "lblUsernameIcon",

                Text = "👤",

                AutoSize = false,

                Size = new Size(40, 45),

                Location = new Point(5, 0),

                TextAlign = ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI Emoji",
                    12,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        150,
                        165,
                        185
                    ),

                BackColor = Color.Transparent
            };


            pnlUsernameInput.Controls.Add(
                lblUsernameIcon
            );


            // ====================================================
            // USERNAME TEXTBOX
            // ====================================================

            txtUsername = new TextBox
            {
                Name = "txtUsername",

                BorderStyle = BorderStyle.None,

                Size = new Size(240, 25),

                Location = new Point(48, 11),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor = Color.FromArgb(
                    100, 110, 130),

                BackColor =
                    Color.FromArgb(
                        30, 42, 62),

                Text =
                    "Enter your staff username"
            };

            // Placeholder behavior
            txtUsername.GotFocus += (s, e) =>
            {
                if (txtUsername.Text ==
                    "Enter your staff username")
                {
                    txtUsername.Text = "";
                    txtUsername.ForeColor =
                        Color.White;
                }
            };

            txtUsername.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(
                    txtUsername.Text))
                {
                    txtUsername.Text =
                        "Enter your staff username";
                    txtUsername.ForeColor =
                        Color.FromArgb(
                            100, 110, 130);
                }
            };


            pnlUsernameInput.Controls.Add(
                txtUsername
            );


            // ====================================================
            // PASSWORD INPUT PANEL
            // ====================================================

            pnlPasswordInput = new Panel
            {
                Name = "pnlPasswordInput",

                Size = new Size(300, 45),

                Location = new Point(50, 295),

                BackColor =
                    Color.FromArgb(30, 42, 62)
            };


            SetRoundedCorners(
                pnlPasswordInput,
                10
            );


            // ====================================================
            // PASSWORD ICON
            // ====================================================

            lblPasswordIcon = new Label
            {
                Name = "lblPasswordIcon",

                Text = "🔒",

                AutoSize = false,

                Size = new Size(40, 45),

                Location = new Point(5, 0),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI Emoji",
                    12,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        150,
                        165,
                        185
                    ),

                BackColor =
                    Color.Transparent
            };


            pnlPasswordInput.Controls.Add(
                lblPasswordIcon
            );


            // ====================================================
            // PASSWORD TEXTBOX
            // ====================================================

            txtPassword = new TextBox
            {
                Name = "txtPassword",

                BorderStyle = BorderStyle.None,

                Size = new Size(200, 25),

                Location = new Point(48, 11),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor = Color.FromArgb(
                    100, 110, 130),

                BackColor =
                    Color.FromArgb(
                        30, 42, 62),

                Text = "Enter your password",

                UseSystemPasswordChar = false
            };

            // Placeholder behavior
            txtPassword.GotFocus += (s, e) =>
            {
                if (txtPassword.Text ==
                    "Enter your password")
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor =
                        Color.White;
                    txtPassword
                        .UseSystemPasswordChar =
                            true;
                }
            };

            txtPassword.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(
                    txtPassword.Text))
                {
                    txtPassword
                        .UseSystemPasswordChar =
                            false;
                    txtPassword.Text =
                        "Enter your password";
                    txtPassword.ForeColor =
                        Color.FromArgb(
                            100, 110, 130);
                }
            };


            pnlPasswordInput.Controls.Add(
                txtPassword
            );


            // ====================================================
            // PASSWORD VISIBILITY TOGGLE
            // ====================================================

            Button btnTogglePassword = new Button
            {
                Name = "btnTogglePassword",

                Size = new Size(40, 45),

                Location = new Point(255, 0),

                Text = "👁",

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.Transparent,

                ForeColor =
                    Color.FromArgb(150, 165, 185),

                Font = new Font(
                    "Segoe UI Emoji",
                    11,
                    FontStyle.Regular
                ),

                Cursor = Cursors.Hand,

                TabStop = false
            };


            btnTogglePassword.FlatAppearance.BorderSize = 0;


            btnTogglePassword.Click += (s, e) =>
            {
                // Don't toggle if placeholder
                if (txtPassword.Text ==
                    "Enter your password")
                    return;

                txtPassword.UseSystemPasswordChar =
                    !txtPassword.UseSystemPasswordChar;

                btnTogglePassword.Text =
                    txtPassword.UseSystemPasswordChar
                        ? "👁"
                        : "🙈";
            };


            pnlPasswordInput.Controls.Add(
                btnTogglePassword
            );

            btnTogglePassword.BringToFront();


            // ====================================================
            // ADD INPUT PANELS TO LOGIN CARD
            // ====================================================

            pnlLoginCard.Controls.Add(
                pnlUsernameInput
            );

            pnlLoginCard.Controls.Add(
                pnlPasswordInput
            );


            // Keep inputs visible
            pnlUsernameInput.BringToFront();
            pnlPasswordInput.BringToFront();
        }


        // ====================================================
        // POSITION LOGIN CARD
        // ====================================================

        private void PositionLoginCard()
        {
            if (pnlLoginCard == null)
                return;


            // Card on right side with margin
            pnlLoginCard.Location = new Point(
                this.ClientSize.Width
                - pnlLoginCard.Width
                - 60,

                (this.ClientSize.Height
                - pnlLoginCard.Height) / 2
            );


            // Close button top-right
            if (btnClose != null)
            {
                btnClose.Location = new Point(
                    this.ClientSize.Width
                    - btnClose.Width - 15,
                    15
                );
            }


            // Accent bar above footer
            if (pnlAccentBar != null)
            {
                pnlAccentBar.Location =
                    new Point(25,
                        this.ClientSize.Height
                        - 120);
            }


            // Footer title
            if (lblLeftFooterTitle != null)
            {
                lblLeftFooterTitle.Location =
                    new Point(25,
                        this.ClientSize.Height
                        - 110);
            }


            // Footer description
            if (lblLeftFooterDesc != null)
            {
                lblLeftFooterDesc.Location =
                    new Point(25,
                        this.ClientSize.Height
                        - 90);
            }
        }


        // ====================================================
        // LOGIN BRANDING
        // ====================================================

        private void CreateLoginBranding()
        {
            lblBrand = new Label
            {
                Name = "lblBrand",

                Text = "WEARECARS",

                AutoSize = false,

                Size = new Size(
                    pnlLoginCard.Width, 30),

                Location = new Point(0, 30),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                ForeColor = Color.White,

                BackColor = Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold
                )
            };


            // Add to card
            pnlLoginCard.Controls.Add(
                lblBrand
            );


            lblBrand.BringToFront();
        }


        // ====================================================
        // ROUNDED CORNERS
        // ====================================================

        private void SetRoundedCorners(
            Control control,
            int radius)
        {
            GraphicsPath path =
                new GraphicsPath();


            // Top-left
            path.AddArc(
                0,
                0,
                radius,
                radius,
                180,
                90
            );


            // Top-right
            path.AddArc(
                control.Width - radius,
                0,
                radius,
                radius,
                270,
                90
            );


            // Bottom-right
            path.AddArc(
                control.Width - radius,
                control.Height - radius,
                radius,
                radius,
                0,
                90
            );


            // Bottom-left
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
        // LOGIN CARD BORDER + ACCENT
        // ====================================================

        private void LoginCard_Paint(
            object sender,
            PaintEventArgs e)
        {
            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;


            int radius = 24;


            // ====================================================
            // CARD BORDER
            // ====================================================

            using (
                Pen borderPen =
                    new Pen(
                        Color.FromArgb(
                            55,
                            70,
                            95
                        ),
                        1
                    )
            )
            {
                using (
                    GraphicsPath borderPath =
                        new GraphicsPath()
                )
                {
                    // Top-left
                    borderPath.AddArc(
                        0,
                        0,
                        radius,
                        radius,
                        180,
                        90
                    );


                    // Top-right
                    borderPath.AddArc(
                        pnlLoginCard.Width
                        - radius
                        - 1,

                        0,

                        radius,
                        radius,

                        270,
                        90
                    );


                    // Bottom-right
                    borderPath.AddArc(
                        pnlLoginCard.Width
                        - radius
                        - 1,

                        pnlLoginCard.Height
                        - radius
                        - 1,

                        radius,
                        radius,

                        0,
                        90
                    );


                    // Bottom-left
                    borderPath.AddArc(
                        0,

                        pnlLoginCard.Height
                        - radius
                        - 1,

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


            // ====================================================
            // BLUE ACCENT LINE
            // ====================================================

            using (
                Pen accentPen =
                    new Pen(
                        Color.FromArgb(
                            45,
                            170,
                            255
                        ),
                        3
                    )
            )
            {
                if (lblBrand != null)
                {
                    // Measure text width to find
                    // where the text starts on
                    // the left side
                    int textWidth =
                        TextRenderer.MeasureText(
                            lblBrand.Text,
                            lblBrand.Font
                        ).Width;


                    // Left edge of centered text
                    int startX =
                        (pnlLoginCard.Width
                        - textWidth) / 2;


                    // Accent spans half the
                    // text width from the left
                    int endX =
                        startX
                        + (textWidth / 2);


                    int y =
                        lblBrand.Bottom + 5;


                    e.Graphics.DrawLine(
                        accentPen,

                        startX,
                        y,

                        endX,
                        y
                    );
                }
            }
        }


        // ====================================================
        // LOGIN LABELS
        // ====================================================

        private void CreateLoginLabels()
        {
            // ====================================================
            // USERNAME LABEL
            // ====================================================

            lblUsername = new Label
            {
                Name = "lblUsername",

                Text = "Username",

                AutoSize = true,

                Location =
                    new Point(50, 185),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor = Color.White,

                BackColor =
                    Color.Transparent
            };


            // ====================================================
            // PASSWORD LABEL
            // ====================================================

            lblPassword = new Label
            {
                Name = "lblPassword",

                Text = "Password",

                AutoSize = true,

                Location =
                    new Point(50, 270),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor = Color.White,

                BackColor =
                    Color.Transparent
            };


            // Add labels
            pnlLoginCard.Controls.Add(
                lblUsername
            );

            pnlLoginCard.Controls.Add(
                lblPassword
            );


            lblUsername.BringToFront();
            lblPassword.BringToFront();
        }


        // ====================================================
        // LOGIN TITLE + SUBTITLE
        // ====================================================

        private void CreateLoginTitel()
        {
            // ====================================================
            // TITLE
            // ====================================================

            lblLoginTitle = new Label
            {
                Name = "lblLoginTitle",

                Text = "Staff Login",

                AutoSize = false,

                Size = new Size(
                    pnlLoginCard.Width, 30),

                Location = new Point(0, 95),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold
                ),

                ForeColor = Color.White,

                BackColor =
                    Color.Transparent
            };


            // ====================================================
            // SUBTITLE
            // ====================================================

            lblLoginSubtitle = new Label
            {
                Name = "lblLoginSubtitle",

                Text =
                    "Sign in to access the WeAreCars management system.",

                AutoSize = false,

                Size = new Size(
                    pnlLoginCard.Width,
                    30
                ),

                Location = new Point(0, 130),

                Font = new Font(
                    "Segoe UI",
                    9,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        170,
                        180,
                        195
                    ),

                BackColor =
                    Color.Transparent,

                TextAlign =
                    ContentAlignment.MiddleCenter
            };


            // Add title
            pnlLoginCard.Controls.Add(
                lblLoginTitle
            );


            // Add subtitle
            pnlLoginCard.Controls.Add(
                lblLoginSubtitle
            );


            lblLoginTitle.BringToFront();
            lblLoginSubtitle.BringToFront();
        }


        // ====================================================
        // SIGN IN SECTION
        // ====================================================

        private void CreateSignInSection()
        {
            // ====================================================
            // SIGN IN BUTTON
            // ====================================================

            btnSignIn = new Button
            {
                Name = "btnSignIn",

                Text = "SIGN IN  →",

                Size = new Size(300, 45),

                Location = new Point(50, 370),

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.FromArgb(
                    255, 107, 53),

                ForeColor = Color.White,

                Font = new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                ),

                Cursor = Cursors.Hand,

                TextAlign =
                    ContentAlignment.MiddleCenter
            };

            btnSignIn.FlatAppearance.BorderSize = 0;

            btnSignIn.FlatAppearance
                .MouseOverBackColor =
                    Color.FromArgb(230, 90, 40);

            btnSignIn.FlatAppearance
                .MouseDownBackColor =
                    Color.FromArgb(200, 80, 35);

            SetRoundedCorners(btnSignIn, 8);

            pnlLoginCard.Controls.Add(btnSignIn);
            btnSignIn.BringToFront();


            // ====================================================
            // HELP LINK
            // ====================================================

            lblHelp = new Label
            {
                Name = "lblHelp",

                Text = "Need help signing in?",

                AutoSize = false,

                Size = new Size(
                    pnlLoginCard.Width, 20),

                Location = new Point(0, 430),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    8,
                    FontStyle.Regular
                ),

                ForeColor = Color.FromArgb(
                    100, 140, 255),

                BackColor = Color.Transparent,

                Cursor = Cursors.Hand
            };

            pnlLoginCard.Controls.Add(lblHelp);
            lblHelp.BringToFront();


            // ====================================================
            // FOOTER
            // ====================================================

            lblFooter = new Label
            {
                Name = "lblFooter",

                Text =
                    "🔒  Authorised staff access only",

                AutoSize = false,

                Size = new Size(
                    pnlLoginCard.Width, 25),

                Location = new Point(0, 540),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    8,
                    FontStyle.Regular
                ),

                ForeColor = Color.FromArgb(
                    140, 150, 170),

                BackColor = Color.Transparent
            };

            pnlLoginCard.Controls.Add(lblFooter);
            lblFooter.BringToFront();
        }
    }
}