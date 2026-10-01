using System;
using System.Drawing;
using System.Windows.Forms;
using WeAreCars.Services;

namespace WeAreCars
{
    public partial class AppShell : Form
    {
        // ====================================================
        // APP SHELL LAYOUT
        // ====================================================

        private Panel pnlSidebar = null!;
        private Panel pnlTopBar = null!;
        private Panel pnlContent = null!;

        // ====================================================
        // SIDEBAR
        // ====================================================

        private Panel pnlSidebarBrand = null!;
        private Panel pnlSidebarNavigation = null!;
        private Panel pnlSidebarFooter = null!;

        private Label lblSidebarBrand = null!;
        private Label lblSidebarSubtitle = null!;

        private Button btnDashboard = null!;
        private Button btnNewBooking = null!;
        private Button btnRentedCars = null!;

        // ====================================================
        // HEADER
        // ====================================================

        private Button btnStaff = null!;
        private Button btnLogout = null!;

        // ====================================================
        // BRAND COLOURS
        // ====================================================

        private readonly Color BrandOrange =
            Color.FromArgb(
                255,
                107,
                53
            );

        private readonly Color TopBarColor =
            Color.FromArgb(
                16,
                25,
                40
            );

        // ====================================================
        // CONSTRUCTOR
        // ====================================================

        public AppShell()
        {
            InitializeComponent();

            ConfigureAppShell();

            CreateAppShellLayout();

            CreateSidebarFoundation();

            CreateHeaderActions();
        }

        // ====================================================
        // FORM LOAD
        // ====================================================

        private void AppShell_Load(
            object? sender,
            EventArgs e
        )
        {
        }

        // ====================================================
        // APP SHELL CONFIGURATION
        // ====================================================

        private void ConfigureAppShell()
        {
            FormBorderStyle =
                FormBorderStyle.None;

            WindowState =
                FormWindowState.Maximized;

            StartPosition =
                FormStartPosition.CenterScreen;

            BackColor =
                Color.FromArgb(
                    11,
                    18,
                    32
                );

            Text =
                "WeAreCars";

            DoubleBuffered = true;
        }

        // ====================================================
        // APP SHELL LAYOUT
        // ====================================================

        private void CreateAppShellLayout()
        {
            // ====================================================
            // SIDEBAR
            // ====================================================

            pnlSidebar = new Panel
            {
                Name = "pnlSidebar",

                Dock = DockStyle.Left,

                Width = 240,

                BackColor =
                    Color.FromArgb(
                        10,
                        16,
                        28
                    )
            };

            Controls.Add(
                pnlSidebar
            );


            // ====================================================
            // TOP BAR
            // ====================================================

            pnlTopBar = new Panel
            {
                Name = "pnlTopBar",

                Dock = DockStyle.Top,

                Height = 72,

                BackColor =
                    TopBarColor
            };

            Controls.Add(
                pnlTopBar
            );


            // ====================================================
            // CONTENT AREA
            // ====================================================

            pnlContent = new Panel
            {
                Name = "pnlContent",

                Dock = DockStyle.Fill,

                BackColor =
                    Color.FromArgb(
                        11,
                        18,
                        32
                    )
            };

            Controls.Add(
                pnlContent
            );


            // ====================================================
            // CONTROL ORDER
            // ====================================================

            pnlSidebar.BringToFront();

            pnlTopBar.BringToFront();
        }

        // ====================================================
        // SIDEBAR FOUNDATION
        // ====================================================

        private void CreateSidebarFoundation()
        {
            // ====================================================
            // SIDEBAR BRAND
            // ====================================================

            pnlSidebarBrand = new Panel
            {
                Name = "pnlSidebarBrand",

                Dock = DockStyle.Top,

                Height = 110,

                BackColor =
                    Color.FromArgb(
                        10,
                        16,
                        28
                    )
            };


            // ====================================================
            // BRAND NAME
            // ====================================================

            lblSidebarBrand = new Label
            {
                Name = "lblSidebarBrand",

                Text = "WEARECARS",

                AutoSize = false,

                Size = new Size(
                    210,
                    32
                ),

                Location = new Point(
                    20,
                    24
                ),

                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold
                ),

                ForeColor =
                    BrandOrange,

                BackColor =
                    Color.Transparent,

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            pnlSidebarBrand.Controls.Add(
                lblSidebarBrand
            );


            // ====================================================
            // BRAND SUBTITLE
            // ====================================================

            lblSidebarSubtitle = new Label
            {
                Name = "lblSidebarSubtitle",

                Text = "MANAGEMENT SYSTEM",

                AutoSize = false,

                Size = new Size(
                    210,
                    20
                ),

                Location = new Point(
                    21,
                    57
                ),

                Font = new Font(
                    "Segoe UI",
                    7,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(
                        120,
                        135,
                        155
                    ),

                BackColor =
                    Color.Transparent,

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            pnlSidebarBrand.Controls.Add(
                lblSidebarSubtitle
            );


            // ====================================================
            // NAVIGATION AREA
            // ====================================================

            pnlSidebarNavigation = new Panel
            {
                Name = "pnlSidebarNavigation",

                Dock = DockStyle.Fill,

                BackColor =
                    Color.FromArgb(
                        10,
                        16,
                        28
                    ),

                Padding = new Padding(
                    15,
                    15,
                    15,
                    15
                ),

                AutoScroll = true
            };


            // ====================================================
            // FOOTER
            // ====================================================

            pnlSidebarFooter = new Panel
            {
                Name = "pnlSidebarFooter",

                Dock = DockStyle.Bottom,

                Height = 70,

                BackColor =
                    Color.FromArgb(
                        10,
                        16,
                        28
                    )
            };


            // ====================================================
            // FOOTER ACCENT LINE
            // ====================================================

            Panel pnlFooterAccent = new Panel
            {
                Name = "pnlFooterAccent",

                Width = 80,

                Height = 3,

                Location = new Point(
                    10,
                    8
                ),

                BackColor =
                    BrandOrange
            };

            pnlSidebarFooter.Controls.Add(
                pnlFooterAccent
            );


            // ====================================================
            // FOOTER TITLE
            // ====================================================

            Label lblFooterTitle = new Label
            {
                Name = "lblFooterTitle",

                Text = "Staff Management Portal",

                AutoSize = false,

                Size = new Size(
                    220,
                    20
                ),

                Location = new Point(
                    10,
                    20
                ),

                Font = new Font(
                    "Segoe UI",
                    8.5f,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(
                        203,
                        213,
                        225
                    ),

                BackColor =
                    Color.Transparent,

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            pnlSidebarFooter.Controls.Add(
                lblFooterTitle
            );


            // ====================================================
            // FOOTER SECURITY
            // ====================================================

            Label lblFooterSecurity = new Label
            {
                Name = "lblFooterSecurity",

                Text =
                    "Secure Access for Authorized Staff",

                AutoSize = false,

                Size = new Size(
                    220,
                    18
                ),

                Location = new Point(
                    10,
                    42
                ),

                Font = new Font(
                    "Segoe UI",
                    7.5f,
                    FontStyle.Regular
                ),

                ForeColor =
                    Color.FromArgb(
                        120,
                        135,
                        155
                    ),

                BackColor =
                    Color.Transparent,

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            pnlSidebarFooter.Controls.Add(
                lblFooterSecurity
            );


            // ====================================================
            // ADD SIDEBAR SECTIONS
            // ====================================================

            pnlSidebar.Controls.Add(
                pnlSidebarNavigation
            );

            pnlSidebar.Controls.Add(
                pnlSidebarFooter
            );

            pnlSidebar.Controls.Add(
                pnlSidebarBrand
            );


            // ====================================================
            // NAVIGATION BUTTONS
            // ====================================================

            btnDashboard =
                CreateNavigationButton(
                    "Dashboard"
                );

            btnNewBooking =
                CreateNavigationButton(
                    "New Booking"
                );

            btnRentedCars =
                CreateNavigationButton(
                    "Rented Cars"
                );


            // ====================================================
            // ADD NAVIGATION BUTTONS
            // ====================================================

            pnlSidebarNavigation.SuspendLayout();

            pnlSidebarNavigation.Controls.Add(
                btnRentedCars
            );

            pnlSidebarNavigation.Controls.Add(
                btnNewBooking
            );

            pnlSidebarNavigation.Controls.Add(
                btnDashboard
            );

            pnlSidebarNavigation.ResumeLayout();
        }

        // ====================================================
        // CREATE NAVIGATION BUTTON
        // ====================================================

        private Button CreateNavigationButton(
            string text
        )
        {
            Button button = new Button
            {
                Text = text,

                Dock = DockStyle.Top,

                Height = 48,

                FlatStyle =
                    FlatStyle.Flat,

                BackColor =
                    Color.FromArgb(
                        10,
                        16,
                        28
                    ),

                ForeColor =
                    Color.FromArgb(
                        203,
                        213,
                        225
                    ),

                TextAlign =
                    ContentAlignment.MiddleLeft,

                Padding =
                    new Padding(
                        24,
                        0,
                        0,
                        0
                    ),

                Font =
                    new Font(
                        "Segoe UI",
                        10.5f,
                        FontStyle.Regular
                    ),

                Cursor =
                    Cursors.Hand,

                Margin =
                    new Padding(
                        0,
                        0,
                        0,
                        4
                    ),

                UseVisualStyleBackColor = false
            };

            button.FlatAppearance.BorderSize = 0;


            // ====================================================
            // ROUNDED CORNERS
            // ====================================================

            button.Resize += (sender, e) =>
            {
                Button currentButton =
                    (Button)sender;

                int radius = 8;

                using (
                    System.Drawing.Drawing2D.GraphicsPath path =
                        new System.Drawing.Drawing2D.GraphicsPath()
                )
                {
                    path.AddArc(
                        0,
                        0,
                        radius,
                        radius,
                        180,
                        90
                    );

                    path.AddArc(
                        currentButton.Width - radius,
                        0,
                        radius,
                        radius,
                        270,
                        90
                    );

                    path.AddArc(
                        currentButton.Width - radius,
                        currentButton.Height - radius,
                        radius,
                        radius,
                        0,
                        90
                    );

                    path.AddArc(
                        0,
                        currentButton.Height - radius,
                        radius,
                        radius,
                        90,
                        90
                    );

                    path.CloseFigure();

                    currentButton.Region =
                        new Region(path);
                }
            };


            // ====================================================
            // HOVER
            // ====================================================

            button.MouseEnter += (sender, e) =>
            {
                Button currentButton =
                    (Button)sender;

                currentButton.BackColor =
                    BrandOrange;

                currentButton.ForeColor =
                    Color.White;
            };


            button.MouseLeave += (sender, e) =>
            {
                Button currentButton =
                    (Button)sender;

                currentButton.BackColor =
                    Color.FromArgb(
                        10,
                        16,
                        28
                    );

                currentButton.ForeColor =
                    Color.FromArgb(
                        203,
                        213,
                        225
                    );
            };


            return button;
        }

        // ====================================================
        // HEADER ACTIONS
        // ====================================================

        private void CreateHeaderActions()
        {
            // ====================================================
            // STAFF ICON
            // ====================================================

            btnStaff = new Button
            {
                Name = "btnStaff",

                Size = new Size(
                    52,
                    52
                ),

                Text = "",

                BackColor =
                    TopBarColor,

                ForeColor =
                    Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Cursor =
                    Cursors.Default,

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                TabStop = false,

                UseVisualStyleBackColor = false
            };

            btnStaff.FlatAppearance.BorderSize = 0;

            btnStaff.FlatAppearance.MouseOverBackColor =
                TopBarColor;

            btnStaff.FlatAppearance.MouseDownBackColor =
                TopBarColor;


            // ====================================================
            // LOGOUT BUTTON
            // ====================================================

            btnLogout = new Button
            {
                Name = "btnLogout",

                Size = new Size(
                    92,
                    36
                ),

                Text = "Logout",

                Font =
                    new Font(
                        "Segoe UI",
                        11f,
                        FontStyle.Bold
                    ),

                ForeColor =
                    Color.White,

                BackColor =
                    BrandOrange,

                FlatStyle =
                    FlatStyle.Flat,

                Cursor =
                    Cursors.Hand,

                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Right,

                TextAlign =
                    ContentAlignment.MiddleCenter,

                TabStop = false,

                UseVisualStyleBackColor = false
            };

            btnLogout.FlatAppearance.BorderSize = 0;

            btnLogout.FlatAppearance.MouseOverBackColor =
                BrandOrange;

            btnLogout.FlatAppearance.MouseDownBackColor =
                BrandOrange;


            // ====================================================
            // POSITION STAFF
            // ====================================================

            btnStaff.Location =
                new Point(
                    pnlTopBar.Width - 190,
                    10
                );


            // ====================================================
            // POSITION LOGOUT
            // ====================================================

            btnLogout.Location =
                new Point(
                    pnlTopBar.Width - 125,
                    18
                );


            // ====================================================
            // ADD TO TOP BAR
            // ====================================================

            pnlTopBar.Controls.Add(
                btnStaff
            );

            pnlTopBar.Controls.Add(
                btnLogout
            );


            // ====================================================
            // STAFF ICON
            // ====================================================

            btnStaff.Paint +=
                BtnStaff_Paint;


            // ====================================================
            // LOGOUT
            // ====================================================

            btnLogout.Click +=
                BtnLogout_Click;


            // ====================================================
            // APPLY ROUNDED LOGOUT SHAPE
            // ====================================================

            ApplyRoundedRegion(
                btnLogout,
                22
            );

            // Apply again whenever the button changes size
            btnLogout.Resize +=
                (sender, e) =>
                {
                    ApplyRoundedRegion(
                        btnLogout,
                        22
                    );
                };
        }

        // ====================================================
        // STAFF ICON DRAWING
        // ====================================================

        private void BtnStaff_Paint(
            object? sender,
            PaintEventArgs e
        )
        {
            Graphics graphics =
                e.Graphics;

            graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            // ====================================================
            // PERMANENT STAFF CIRCLE
            // ====================================================

            int iconSize = 36;

            int iconX =
                (btnStaff.Width - iconSize) / 2;

            int iconY =
                (btnStaff.Height - iconSize) / 2;


            // ====================================================
            // ORANGE CIRCLE
            // ====================================================

            using (
                Brush circleBrush =
                    new SolidBrush(
                        BrandOrange
                    )
            )
            {
                graphics.FillEllipse(
                    circleBrush,
                    iconX,
                    iconY,
                    iconSize,
                    iconSize
                );
            }


            // ====================================================
            // STAFF HEAD
            // ====================================================

            float scale =
                iconSize / 36f;

            float headSize =
                10f * scale;

            float headX =
                iconX + (13f * scale);

            float headY =
                iconY + (6f * scale);


            // ====================================================
            // STAFF BODY
            // ====================================================

            float bodyWidth =
                18f * scale;

            float bodyHeight =
                10f * scale;

            float bodyX =
                iconX + (9f * scale);

            float bodyY =
                iconY + (19f * scale);


            // ====================================================
            // WHITE PERSON ICON
            // ====================================================

            using (
                Brush iconBrush =
                    new SolidBrush(
                        Color.White
                    )
            )
            {
                // Head
                graphics.FillEllipse(
                    iconBrush,
                    headX,
                    headY,
                    headSize,
                    headSize
                );

                // Body
                graphics.FillEllipse(
                    iconBrush,
                    bodyX,
                    bodyY,
                    bodyWidth,
                    bodyHeight
                );
            }
        }

        // ====================================================
        // APPLY ROUNDED REGION
        // ====================================================

        private void ApplyRoundedRegion(
            Control control,
            int radius
        )
        {
            using (
                System.Drawing.Drawing2D.GraphicsPath path =
                    new System.Drawing.Drawing2D.GraphicsPath()
            )
            {
                path.AddArc(
                    0,
                    0,
                    radius,
                    radius,
                    180,
                    90
                );

                path.AddArc(
                    control.Width - radius,
                    0,
                    radius,
                    radius,
                    270,
                    90
                );

                path.AddArc(
                    control.Width - radius,
                    control.Height - radius,
                    radius,
                    radius,
                    0,
                    90
                );

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
        }

        // ====================================================
        // LOGOUT FUNCTION
        // ====================================================

        private void BtnLogout_Click(
            object? sender,
            EventArgs e
        )
        {
            // ====================================================
            // CREATE LOGIN FORM
            // ====================================================

            LoginForm loginForm =
                new LoginForm();


            // ====================================================
            // WHEN LOGIN FORM IS SHOWN
            // END SESSION AND CLOSE APP SHELL
            // ====================================================

            loginForm.Shown +=
                (loginSender, loginEvent) =>
                {
                    // End current staff session
                    SessionService.Logout();

                    // Close current AppShell
                    Close();
                };


            // ====================================================
            // SHOW LOGIN FORM FIRST
            // ====================================================

            loginForm.Show();

            loginForm.BringToFront();

            loginForm.Activate();


            // ====================================================
            // HIDE APP SHELL
            // ====================================================

            Hide();
        }
    }
}