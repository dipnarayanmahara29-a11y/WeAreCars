
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using WeAreCars.Services;

namespace WeAreCars
{
    public partial class LoginForm : Form
    {
        // ====================================================
        // LOGIN UI
        // ====================================================

        private Panel pnlLoginCard = null!;

        private Label lblBrand = null!;
        private Label lblLoginTitle = null!;
        private Label lblLoginSubtitle = null!;

        private Label lblUsername = null!;
        private Label lblPassword = null!;

        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;

        private Panel pnlUsernameInput = null!;
        private Panel pnlPasswordInput = null!;

        private Label lblUsernameIcon = null!;
        private Label lblPasswordIcon = null!;

        private Button btnSignIn = null!;
        private Label lblHelp = null!;
        private Label lblFooter = null!;

        // ====================================================
        // BACK BUTTON
        // ====================================================

        private Button btnBack = null!;


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

            // Create Back button only
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

            // Position everything after form is displayed
            this.Shown += LoginForm_Shown;
        }


        // ====================================================
        // FORM EVENTS
        // ====================================================

        private void LoginForm_Load(
            object? sender,
            EventArgs e)
        {
        }

        private void picLoginBackground_Click(
            object? sender,
            EventArgs e)
        {
        }


        private void LoginForm_Shown(
            object? sender,
            EventArgs e)
        {
            PositionLoginCard();
        }


        private void LoginForm_Resize(
            object? sender,
            EventArgs e)
        {
            if (pnlLoginCard != null)
            {
                PositionLoginCard();
            }
        }


        // ====================================================
        // SIGN IN AUTHENTICATION
        // ====================================================

        private void BtnSignIn_Click(
            object? sender,
            EventArgs e)
        {
            string username =
                txtUsername.Text.Trim();

            string password =
                txtPassword.Text;


            // ====================================================
            // VALIDATE USERNAME
            // ====================================================

            if (string.IsNullOrWhiteSpace(username) ||
                username == "Enter your staff username")
            {
                MessageBox.Show(
                    "Please enter your staff username.",
                    "WeAreCars",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtUsername.Focus();

                return;
            }


            // ====================================================
            // VALIDATE PASSWORD
            // ====================================================

            if (string.IsNullOrWhiteSpace(password) ||
                password == "Enter your password")
            {
                MessageBox.Show(
                    "Please enter your password.",
                    "WeAreCars",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPassword.Focus();

                return;
            }


            // ====================================================
            // AUTHENTICATE AGAINST SQLITE
            // ====================================================

            try
            {
                bool loginSuccessful =
                    StaffUserService.VerifyStaffUser(
                        username,
                        password
                    );


                if (loginSuccessful)
                {
                    SessionService.Login(username);

                    MessageBox.Show(
                        "Login successful.",
                        "WeAreCars",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Dashboard navigation will be added
                    // after authentication is confirmed.
                }
                else
                {
                    MessageBox.Show(
                        "Invalid username or password.",
                        "WeAreCars",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtPassword.Focus();

                    txtPassword.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to connect to the application database.\n\n" +
                    ex.Message,
                    "WeAreCars",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // ====================================================
        // FORM CONFIGURATION
        // ====================================================

        private void ConfigureForm()
        {
            this.FormBorderStyle =
                FormBorderStyle.None;

            this.WindowState =
                FormWindowState.Maximized;

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.BackColor =
                Color.FromArgb(
                    11,
                    18,
                    32);

            this.Text =
                "WeAreCars - Staff Login";

            this.DoubleBuffered = true;
        }


        // ====================================================
        // BACKGROUND
        // ====================================================

        private void CreateBackground()
        {
            // picLoginBackground is created by the Designer.
            // It remains the main background control.

            string backgroundPath =
                Path.Combine(
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
                    Color.FromArgb(
                        11,
                        18,
                        32);
            }


            picLoginBackground.Dock =
                DockStyle.Fill;

            picLoginBackground.SendToBack();
        }


        // ====================================================
        // BACK BUTTON
        // ====================================================

        private void CreateLeftOverlay()
        {
            // ====================================================
            // BACK BUTTON
            // ====================================================

            btnBack = new Button
            {
                Name = "btnBack",

                Text = "←  Back",

                Size = new Size(
                    100,
                    38),

                Location = new Point(
                    25,
                    20),

                FlatStyle =
                    FlatStyle.Flat,

                BackColor =
                    Color.Transparent,

                ForeColor =
                    Color.White,

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular),

                Cursor =
                    Cursors.Hand,

                TextAlign =
                    ContentAlignment.MiddleLeft,

                TabStop = false,

                UseVisualStyleBackColor = false
            };


            btnBack.FlatAppearance.BorderSize =
                0;

            btnBack.FlatAppearance.MouseOverBackColor =
                Color.Transparent;

            btnBack.FlatAppearance.MouseDownBackColor =
                Color.Transparent;
            btnBack.Click += BtnBack_Click;


            // Put button directly on the background.
            picLoginBackground.Controls.Add(
                btnBack
            );

            btnBack.BringToFront();
        }

        // ====================================================
        // Back Button function
        // ====================================================

        private void BtnBack_Click( object? sender, EventArgs e)
        {
            WelcomeScreen? welcomeScreen = null;

            foreach (Form form in Application.OpenForms)
            {
                if (form is WelcomeScreen existingWelcomeScreen)
                {
                    welcomeScreen =
                        existingWelcomeScreen;

                    break;
                }
            }

            if (welcomeScreen != null &&
                !welcomeScreen.IsDisposed)
            {
                welcomeScreen.ReturnFromLogin();
            }

            // Completely close the LoginForm.
            this.Close();
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

                Size = new Size(
                    480,
                    580),

                BackColor =
                    Color.FromArgb(
                        16,
                        25,
                        40),

                BorderStyle =
                    BorderStyle.None
            };


            // Add card to form
            this.Controls.Add(
                pnlLoginCard
            );


            // Put card above background
            pnlLoginCard.BringToFront();


            // Rounded corners
            SetRoundedCorners(
                pnlLoginCard,
                18
            );


            // Enable border and accent line
            pnlLoginCard.Paint +=
                LoginCard_Paint;


            // ====================================================
            // USERNAME INPUT PANEL
            // ====================================================

            pnlUsernameInput = new Panel
            {
                Name = "pnlUsernameInput",

                Size = new Size(
                    360,
                    45),

                Location = new Point(
                    60,
                    210),

                BackColor =
                    Color.FromArgb(
                        30,
                        42,
                        62)
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

                Size = new Size(
                    45,
                    45),

                Location =
                    new Point(
                        5,
                        0),

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


            pnlUsernameInput.Controls.Add(
                lblUsernameIcon
            );


            // ====================================================
            // USERNAME TEXTBOX
            // ====================================================

            txtUsername = new TextBox
            {
                Name = "txtUsername",

                BorderStyle =
                    BorderStyle.None,

                Size = new Size(
                    290,
                    25),

                Location =
                    new Point(
                        52,
                        11),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        100,
                        110,
                        130),

                BackColor =
                    Color.FromArgb(
                        30,
                        42,
                        62),

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
                            100,
                            110,
                            130);
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

                Size = new Size(
                    360,
                    45),

                Location =
                    new Point(
                        60,
                        295),

                BackColor =
                    Color.FromArgb(
                        30,
                        42,
                        62)
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

                Size = new Size(
                    45,
                    45),

                Location =
                    new Point(
                        5,
                        0),

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

                BorderStyle =
                    BorderStyle.None,

                Size = new Size(
                    245,
                    25),

                Location =
                    new Point(
                        52,
                        11),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                // Password text is WHITE.
                ForeColor =
                    Color.White,

                BackColor =
                    Color.FromArgb(
                        30,
                        42,
                        62),

                Text =
                    "Enter your password",

                UseSystemPasswordChar =
                    false
            };


            // ====================================================
            // PASSWORD PLACEHOLDER
            // ====================================================

            txtPassword.GotFocus += (s, e) =>
            {
                if (txtPassword.Text ==
                    "Enter your password")
                {
                    txtPassword.Text = "";

                    txtPassword.ForeColor =
                        Color.White;

                    txtPassword.UseSystemPasswordChar =
                        true;
                }
            };


            txtPassword.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(
                    txtPassword.Text))
                {
                    txtPassword.UseSystemPasswordChar =
                        false;

                    txtPassword.Text =
                        "Enter your password";

                    txtPassword.ForeColor =
                        Color.FromArgb(
                            100,
                            110,
                            130);
                }
                else
                {
                    // Keep entered password text white.
                    txtPassword.ForeColor =
                        Color.White;
                }
            };


            pnlPasswordInput.Controls.Add(
                txtPassword
            );


            // ====================================================
            // PASSWORD VISIBILITY TOGGLE
            // ====================================================

            Button btnTogglePassword =
                new Button
                {
                    Name =
                        "btnTogglePassword",

                    Size =
                        new Size(
                            40,
                            45),

                    Location =
                        new Point(
                            315,
                            0),

                    Text = "👁",

                    FlatStyle =
                        FlatStyle.Flat,

                    BackColor =
                        Color.Transparent,

                    ForeColor =
                        Color.FromArgb(
                            150,
                            165,
                            185),

                    Font = new Font(
                        "Segoe UI Emoji",
                        11,
                        FontStyle.Regular
                    ),

                    Cursor =
                        Cursors.Hand,

                    TabStop = false,

                    UseVisualStyleBackColor =
                        false
                };


            btnTogglePassword.FlatAppearance.BorderSize =
                0;

            btnTogglePassword.FlatAppearance.MouseOverBackColor =
                Color.Transparent;

            btnTogglePassword.FlatAppearance.MouseDownBackColor =
                Color.Transparent;


            btnTogglePassword.Click += (s, e) =>
            {
                // Don't toggle if placeholder.
                if (txtPassword.Text ==
                    "Enter your password")
                {
                    return;
                }


                txtPassword.UseSystemPasswordChar =
                    !txtPassword.UseSystemPasswordChar;


                // Keep password text white.
                txtPassword.ForeColor =
                    Color.White;


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


            // Keep inputs visible.
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


            // Center the login card horizontally.
            int x =
                (this.ClientSize.Width
                - pnlLoginCard.Width) / 2;


            // Center the login card vertically.
            int y =
                (this.ClientSize.Height
                - pnlLoginCard.Height) / 2;


            // Prevent negative positions on
            // very small screens.
            x = Math.Max(
                0,
                x);

            y = Math.Max(
                0,
                y);


            pnlLoginCard.Location =
                new Point(
                    x,
                    y);


            // Keep the Back button in the
            // same position during resizing.
            if (btnBack != null)
            {
                btnBack.Location =
                    new Point(
                        25,
                        20);
            }
        }


        // ====================================================
        // LOGIN BRANDING
        // ====================================================

        private void CreateLoginBranding()
        {
            lblBrand = new Label
            {
                Name =
                    "lblBrand",

                Text =
                    "WEARECARS",

                AutoSize =
                    false,

                Size =
                    new Size(
                        pnlLoginCard.Width,
                        30),

                Location =
                    new Point(
                        0,
                        30),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                // Same orange as accent line.
                ForeColor =
                    Color.FromArgb(
                        255,
                        107,
                        53),

                BackColor =
                    Color.Transparent,

                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold
                )
            };


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
            object? sender,
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
            // ORANGE ACCENT LINE
            // ====================================================

            using (
                Pen accentPen =
                    new Pen(
                        Color.FromArgb(
                            255,
                            107,
                            53
                        ),
                        3
                    )
            )
            {
                if (lblBrand != null)
                {
                    int textWidth =
                        TextRenderer.MeasureText(
                            lblBrand.Text,
                            lblBrand.Font
                        ).Width;


                    int startX =
                        (pnlLoginCard.Width
                        - textWidth) / 2;


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
                Name =
                    "lblUsername",

                Text =
                    "Username",

                AutoSize =
                    true,

                Location =
                    new Point(
                        60,
                        185),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.White,

                BackColor =
                    Color.Transparent
            };


            // ====================================================
            // PASSWORD LABEL
            // ====================================================

            lblPassword = new Label
            {
                Name =
                    "lblPassword",

                Text =
                    "Password",

                AutoSize =
                    true,

                Location =
                    new Point(
                        60,
                        270),

                Font = new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.White,

                BackColor =
                    Color.Transparent
            };


            // Add labels.
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
                Name =
                    "lblLoginTitle",

                Text =
                    "Staff Login",

                AutoSize =
                    false,

                Size =
                    new Size(
                        pnlLoginCard.Width,
                        30),

                Location =
                    new Point(
                        0,
                        95),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.White,

                BackColor =
                    Color.Transparent
            };


            // ====================================================
            // SUBTITLE
            // ====================================================

            lblLoginSubtitle = new Label
            {
                Name =
                    "lblLoginSubtitle",

                Text =
                    "Sign in to access the WeAreCars management system.",

                AutoSize =
                    false,

                Size =
                    new Size(
                        pnlLoginCard.Width,
                        30
                    ),

                Location =
                    new Point(
                        0,
                        130),

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


            // Add title.
            pnlLoginCard.Controls.Add(
                lblLoginTitle
            );


            // Add subtitle.
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
                Name =
                    "btnSignIn",

                Text =
                    "SIGN IN  →",

                Size =
                    new Size(
                        360,
                        45),

                Location =
                    new Point(
                        60,
                        370),

                FlatStyle =
                    FlatStyle.Flat,

                BackColor =
                    Color.FromArgb(
                        255,
                        107,
                        53),

                ForeColor =
                    Color.White,

                Font = new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                ),

                Cursor =
                    Cursors.Hand,

                TextAlign =
                    ContentAlignment.MiddleCenter
            };


            btnSignIn.FlatAppearance.BorderSize =
                0;


            btnSignIn.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(
                    230,
                    90,
                    40);


            btnSignIn.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(
                    200,
                    80,
                    35);


            SetRoundedCorners(
                btnSignIn,
                8
            );


            pnlLoginCard.Controls.Add(
                btnSignIn
            );

            btnSignIn.BringToFront();


            // Connect the button to the
            // authentication event.
            btnSignIn.Click +=
                BtnSignIn_Click;


            // ====================================================
            // HELP LINK
            // ====================================================

            lblHelp = new Label
            {
                Name =
                    "lblHelp",

                Text =
                    "Need help signing in?",

                AutoSize =
                    false,

                Size =
                    new Size(
                        pnlLoginCard.Width,
                        20),

                Location =
                    new Point(
                        0,
                        430),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    8,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        100,
                        140,
                        255),

                BackColor =
                    Color.Transparent,

                Cursor =
                    Cursors.Hand
            };


            pnlLoginCard.Controls.Add(
                lblHelp
            );

            lblHelp.BringToFront();


            // ====================================================
            // FOOTER
            // ====================================================

            lblFooter = new Label
            {
                Name =
                    "lblFooter",

                Text =
                    "🔒  Authorised staff access only",

                AutoSize =
                    false,

                Size =
                    new Size(
                        pnlLoginCard.Width,
                        25),

                Location =
                    new Point(
                        0,
                        540),

                TextAlign =
                    ContentAlignment.MiddleCenter,

                Font = new Font(
                    "Segoe UI",
                    8,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        140,
                        150,
                        170),

                BackColor =
                    Color.Transparent
            };


            pnlLoginCard.Controls.Add(
                lblFooter
            );

            lblFooter.BringToFront();
        }
    }
}