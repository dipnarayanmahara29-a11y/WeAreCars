
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace WeAreCars
{
    public partial class WelcomeScreen : Form
    {
        // ====================================================
        // VIDEO PLAYBACK
        // ====================================================

        private System.Windows.Forms.Timer? videoTimer;
        private bool videoFrozen = false;

        // ====================================================
        // SPLASH UI
        // ====================================================

        private Panel overlayPanel = null!;
        private Form overlayForm = null!;

        private Label lblBrand = null!;
        private Panel brandAccent = null!;
        private Label lblTitle = null!;
        private Label lblWelcome = null!;
        private Label lblDescription = null!;
        private Label lblInstruction = null!;
        private Button btnGetStarted = null!;
        private Label lblFooter = null!;
        private Button btnClose = null!;

        // ====================================================
        // ANIMATION
        // ====================================================

        private System.Windows.Forms.Timer? animationTimer;
        private System.Windows.Forms.Timer? uiStartTimer;

        private DateTime animationStartTime;
        private DateTime uiStartCheckTime;

        private bool animationStarted = false;

        // Prevents splash logic from running while navigating
        // to another screen.
        private bool isNavigatingToLogin = false;

        // ====================================================
        // HELP
        // ====================================================

        private ToolTip splashToolTip = null!;

        // ====================================================
        // CONSTANTS
        // ====================================================

        // The UI will not appear until the video has actually
        // reached this playback position.
        private const double UiStartVideoPosition = 0.45;

        // Safety fallback in case Windows Media Player takes
        // longer than expected to report its playback position.
        private const int UiStartFallbackMilliseconds = 1000;

        // ====================================================
        // CONSTRUCTOR
        // ====================================================

        public WelcomeScreen()
        {
            InitializeComponent();

            ConfigureVideoPlayer();
            CreateOverlayForm();
            CreateSplashUI();
        }

        // ====================================================
        // VIDEO
        // ====================================================

        /// <summary>
        /// Configures Windows Media Player as the full-screen
        /// background for the WeAreCars welcome screen.
        /// </summary>
        private void ConfigureVideoPlayer()
        {
            axWindowsMediaPlayer.uiMode = "none";
            axWindowsMediaPlayer.enableContextMenu = false;
            axWindowsMediaPlayer.stretchToFit = true;
            axWindowsMediaPlayer.Dock = DockStyle.Fill;
            axWindowsMediaPlayer.Visible = true;
        }

        /// <summary>
        /// Loads the splash video and starts playback.
        /// The video plays once and is frozen shortly before
        /// its actual end to prevent an end-of-video transition.
        /// </summary>
        private void WelcomeScreen_Load(
            object? sender,
            EventArgs e)
        {
            string videoPath = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                "Splash",
                "WeAreCar_Video.mp4"
            );

            // ------------------------------------------------
            // Validate that the splash video exists.
            // ------------------------------------------------

            if (!File.Exists(videoPath))
            {
                MessageBox.Show(
                    $"The Welcome Screen video could not be found.\n\n{videoPath}",
                    "WeAreCars",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            // ------------------------------------------------
            // Reset playback state.
            // ------------------------------------------------

            videoFrozen = false;
            animationStarted = false;
            isNavigatingToLogin = false;

            // ------------------------------------------------
            // Load and play video.
            // ------------------------------------------------

            axWindowsMediaPlayer.URL = videoPath;

            axWindowsMediaPlayer.settings.autoStart = true;
            axWindowsMediaPlayer.settings.setMode(
                "loop",
                false
            );

            axWindowsMediaPlayer.Ctlcontrols.play();

            // ------------------------------------------------
            // Start video end monitoring.
            // ------------------------------------------------

            videoTimer?.Stop();
            videoTimer?.Dispose();

            videoTimer =
                new System.Windows.Forms.Timer
                {
                    Interval = 15
                };

            videoTimer.Tick += VideoTimer_Tick;
            videoTimer.Start();

            // ------------------------------------------------
            // Wait for the video to actually begin playing
            // before displaying the splash UI.
            // ------------------------------------------------

            uiStartCheckTime = DateTime.Now;

            uiStartTimer?.Stop();
            uiStartTimer?.Dispose();

            uiStartTimer =
                new System.Windows.Forms.Timer
                {
                    Interval = 15
                };

            uiStartTimer.Tick += UiStartTimer_Tick;
            uiStartTimer.Start();
        }

        /// <summary>
        /// Waits for Windows Media Player to advance to the
        /// required playback position before starting the UI
        /// entrance animation.
        /// </summary>
        private void UiStartTimer_Tick(
            object? sender,
            EventArgs e)
        {
            if (isNavigatingToLogin)
                return;

            if (animationStarted)
                return;

            // ------------------------------------------------
            // Check actual video playback position.
            // ------------------------------------------------

            if (axWindowsMediaPlayer.currentMedia != null)
            {
                double currentPosition =
                    axWindowsMediaPlayer.Ctlcontrols.currentPosition;

                if (currentPosition >= UiStartVideoPosition)
                {
                    BeginSplashAnimation();
                    return;
                }
            }

            // ------------------------------------------------
            // Safety fallback.
            // ------------------------------------------------

            double elapsed =
                (DateTime.Now - uiStartCheckTime)
                .TotalMilliseconds;

            if (elapsed >= UiStartFallbackMilliseconds)
            {
                BeginSplashAnimation();
            }
        }

        /// <summary>
        /// Starts the splash animation exactly once.
        /// </summary>
        private void BeginSplashAnimation()
        {
            if (isNavigatingToLogin)
                return;

            if (animationStarted)
                return;

            animationStarted = true;

            uiStartTimer?.Stop();
            uiStartTimer?.Dispose();
            uiStartTimer = null;

            StartEntranceAnimation();
        }

        /// <summary>
        /// Monitors the video position and freezes it shortly
        /// before the end of playback.
        /// </summary>
        private void VideoTimer_Tick(
            object? sender,
            EventArgs e)
        {
            if (isNavigatingToLogin)
                return;

            if (videoFrozen)
                return;

            if (axWindowsMediaPlayer.currentMedia == null)
                return;

            double duration =
                axWindowsMediaPlayer.currentMedia.duration;

            double currentPosition =
                axWindowsMediaPlayer.Ctlcontrols.currentPosition;

            if (duration <= 0)
                return;

            double remainingTime =
                duration - currentPosition;

            // Pause just before the end so WMP never reaches
            // the point where it stops and performs its own
            // visible end transition.
            if (remainingTime <= 0.10)
            {
                FreezeVideo();
            }
        }

        /// <summary>
        /// Freezes the video on the current displayed frame.
        /// </summary>
        private void FreezeVideo()
        {
            if (isNavigatingToLogin)
                return;

            if (videoFrozen)
                return;

            videoFrozen = true;

            videoTimer?.Stop();

            axWindowsMediaPlayer.Ctlcontrols.pause();

            // Ensure the overlay is visible and above the video.
            if (overlayForm != null &&
                !overlayForm.IsDisposed)
            {
                try
                {
                    if (!overlayForm.Visible)
                        overlayForm.Show(this);

                    overlayForm.BringToFront();
                    overlayForm.Activate();
                }
                catch
                {
                    // Ignore overlay activation errors.
                }
            }

            // Keep the close button available.
            if (btnClose != null)
            {
                btnClose.Visible = true;
                btnClose.BringToFront();
            }
        }

        // ====================================================
        // OVERLAY FORM
        // ====================================================

        /// <summary>
        /// Creates the transparent UI form positioned over
        /// the video background.
        /// </summary>
        private void CreateOverlayForm()
        {
            overlayForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,

                // Black becomes transparent on this form.
                BackColor = Color.Black,
                TransparencyKey = Color.Black,

                // UI itself remains fully visible.
                Opacity = 1.0,

                // The overlay belongs to the WelcomeScreen,
                // so it does not need to be a global TopMost window.
                TopMost = false,

                AutoScaleMode = AutoScaleMode.None,

                ControlBox = false,
                MinimizeBox = false,
                MaximizeBox = false
            };

            overlayForm.Owner = this;

            PositionOverlayForm();

            // The UI must remain hidden while the video begins.
            overlayForm.Visible = false;

            // Keep the overlay aligned with the main form.
            Move += MainForm_MoveOrResize;
            Resize += MainForm_MoveOrResize;
        }

        /// <summary>
        /// Positions and sizes the transparent UI form over
        /// the main application window.
        /// </summary>
        private void PositionOverlayForm()
        {
            if (overlayForm == null ||
                overlayForm.IsDisposed)
            {
                return;
            }

            Point screenLocation =
                PointToScreen(Point.Empty);

            overlayForm.Location = screenLocation;
            overlayForm.Size = ClientSize;

            PositionCloseButton();
        }

        /// <summary>
        /// Keeps the overlay and close button aligned with the
        /// main application window when it moves or resizes.
        /// </summary>
        private void MainForm_MoveOrResize(
            object? sender,
            EventArgs e)
        {
            PositionOverlayForm();
        }

        // ====================================================
        // SPLASH UI
        // ====================================================

        /// <summary>
        /// Creates the complete splash-screen interface.
        /// All interface elements are placed on the transparent
        /// overlay while the video remains underneath.
        /// </summary>
        private void CreateSplashUI()
        {
            overlayForm.SuspendLayout();

            try
            {
                // ------------------------------------------------
                // UI CONTAINER
                // ------------------------------------------------

                overlayPanel = new Panel
                {
                    BackColor = Color.Transparent,
                    Location = new Point(0, 0),
                    Size = overlayForm.ClientSize,
                    Dock = DockStyle.Fill
                };

                overlayForm.Controls.Add(overlayPanel);

                // ------------------------------------------------
                // BRAND
                // ------------------------------------------------

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

                    Location =
                        new Point(48, 38)
                };

                overlayPanel.Controls.Add(lblBrand);

                // ------------------------------------------------
                // BRAND ACCENT
                // ------------------------------------------------

                brandAccent = new Panel
                {
                    BackColor =
                        Color.FromArgb(
                            249,
                            115,
                            22),

                    Size = new Size(36, 3),

                    Location =
                        new Point(49, 73)
                };

                overlayPanel.Controls.Add(brandAccent);

                // ------------------------------------------------
                // MAIN TITLE
                // ------------------------------------------------

                lblTitle = new Label
                {
                    AutoSize = false,

                    Text =
                        "VEHICLE RENTAL\r\n" +
                        "MANAGEMENT SYSTEM",

                    ForeColor = Color.White,
                    BackColor = Color.Transparent,

                    Font = new Font(
                        "Segoe UI",
                        33,
                        FontStyle.Bold
                    ),

                    Location =
                        new Point(48, 190),

                    Size =
                        new Size(600, 120),

                    TextAlign =
                        ContentAlignment.MiddleLeft
                };

                overlayPanel.Controls.Add(lblTitle);

                // ------------------------------------------------
                // WELCOME MESSAGE
                // ------------------------------------------------

                lblWelcome = new Label
                {
                    AutoSize = true,

                    Text =
                        "Welcome to WeAreCars",

                    ForeColor =
                        Color.FromArgb(
                            249,
                            115,
                            22),

                    BackColor =
                        Color.Transparent,

                    Font = new Font(
                        "Segoe UI",
                        13,
                        FontStyle.Bold
                    ),

                    Location =
                        new Point(50, 335)
                };

                overlayPanel.Controls.Add(lblWelcome);

                // ------------------------------------------------
                // DESCRIPTION
                // ------------------------------------------------

                lblDescription = new Label
                {
                    AutoSize = false,

                    Text =
                        "Manage vehicles, create rental bookings,\r\n" +
                        "and review current rentals.",

                    ForeColor =
                        Color.FromArgb(
                            226,
                            232,
                            240),

                    BackColor =
                        Color.Transparent,

                    Font = new Font(
                        "Segoe UI",
                        10.5f,
                        FontStyle.Regular
                    ),

                    Location =
                        new Point(50, 375),

                    Size =
                        new Size(480, 55),

                    TextAlign =
                        ContentAlignment.TopLeft
                };

                overlayPanel.Controls.Add(lblDescription);

                // ------------------------------------------------
                // INSTRUCTION
                // ------------------------------------------------

                lblInstruction = new Label
                {
                    AutoSize = false,

                    Text =
                        "Select Get Started to sign in and access\r\n" +
                        "the staff management system.",

                    ForeColor =
                        Color.FromArgb(
                            148,
                            163,
                            184),

                    BackColor =
                        Color.Transparent,

                    Font = new Font(
                        "Segoe UI",
                        9.5f,
                        FontStyle.Regular
                    ),

                    Location =
                        new Point(50, 445),

                    Size =
                        new Size(480, 50),

                    TextAlign =
                        ContentAlignment.TopLeft
                };

                overlayPanel.Controls.Add(lblInstruction);

                // ------------------------------------------------
                // GET STARTED BUTTON
                // ------------------------------------------------

                btnGetStarted = new Button
                {
                    Text =
                        "GET STARTED   →",

                    Size =
                        new Size(215, 54),

                    Location =
                        new Point(48, 520),

                    BackColor =
                        Color.FromArgb(
                            249,
                            115,
                            22),

                    ForeColor = Color.White,

                    Font = new Font(
                        "Segoe UI",
                        10.5f,
                        FontStyle.Bold
                    ),

                    FlatStyle =
                        FlatStyle.Flat,

                    Cursor =
                        Cursors.Hand,

                    TabStop = false,

                    TextAlign =
                        ContentAlignment.MiddleCenter,

                    UseVisualStyleBackColor =
                        false
                };

                btnGetStarted.FlatAppearance.BorderSize =
                    0;

                btnGetStarted.FlatAppearance.MouseOverBackColor =
                    Color.FromArgb(
                        234,
                        88,
                        12);

                btnGetStarted.FlatAppearance.MouseDownBackColor =
                    Color.FromArgb(
                        194,
                        65,
                        12);

                btnGetStarted.Click +=
                    BtnGetStarted_Click;

                // Create rounded corners.
                using (
                    GraphicsPath buttonPath =
                        CreateRoundedRectanglePath(
                            new Rectangle(
                                0,
                                0,
                                btnGetStarted.Width,
                                btnGetStarted.Height
                            ),
                            10))
                {
                    btnGetStarted.Region =
                        new Region(buttonPath);
                }

                overlayPanel.Controls.Add(
                    btnGetStarted
                );

                // ------------------------------------------------
                // FOOTER
                // ------------------------------------------------

                lblFooter = new Label
                {
                    AutoSize = true,

                    Text =
                        "Staff Application  •  Version 1.0",

                    ForeColor =
                        Color.FromArgb(
                            148,
                            163,
                            184),

                    BackColor =
                        Color.Transparent,

                    Font = new Font(
                        "Segoe UI",
                        8.5f,
                        FontStyle.Regular
                    ),

                    Location =
                        new Point(
                            50,
                            Math.Max(
                                0,
                                ClientSize.Height - 42
                            )
                        ),

                    Anchor =
                        AnchorStyles.Bottom |
                        AnchorStyles.Left
                };

                overlayPanel.Controls.Add(lblFooter);

                // ------------------------------------------------
                // CLOSE BUTTON
                // ------------------------------------------------

                btnClose = new Button
                {
                    Text = "×",

                    Size =
                        new Size(
                            56,
                            56),

                    BackColor =
                        Color.Transparent,

                    ForeColor =
                        Color.White,

                    FlatStyle =
                        FlatStyle.Flat,

                    Font = new Font(
                        "Segoe UI",
                        18,
                        FontStyle.Bold
                    ),

                    Cursor =
                        Cursors.Hand,

                    TabStop = false,

                    TextAlign =
                        ContentAlignment.MiddleCenter,

                    Anchor =
                        AnchorStyles.Top |
                        AnchorStyles.Right,

                    Visible = false
                };

                btnClose.FlatAppearance.BorderSize =
                    0;

                btnClose.FlatAppearance.MouseOverBackColor =
                    Color.Transparent;

                btnClose.FlatAppearance.MouseDownBackColor =
                    Color.Transparent;

                btnClose.MouseEnter +=
                    BtnClose_MouseEnter;

                btnClose.MouseLeave +=
                    BtnClose_MouseLeave;

                btnClose.Click +=
                    BtnClose_Click;

                overlayForm.Controls.Add(
                    btnClose
                );

                btnClose.BringToFront();

                PositionCloseButton();

                // ------------------------------------------------
                // TOOLTIP / HELP
                // ------------------------------------------------

                splashToolTip = new ToolTip
                {
                    AutoPopDelay = 5000,
                    InitialDelay = 400,
                    ReshowDelay = 200,
                    ShowAlways = true
                };

                splashToolTip.SetToolTip(
                    btnGetStarted,
                    "Continue to staff login"
                );

                splashToolTip.SetToolTip(
                    btnClose,
                    "Close WeAreCars"
                );
            }
            finally
            {
                overlayForm.ResumeLayout(false);
            }
        }

        /// <summary>
        /// Positions the close button at the top-right of the
        /// transparent UI overlay.
        /// </summary>
        private void PositionCloseButton()
        {
            if (btnClose == null ||
                overlayForm == null ||
                btnClose.IsDisposed)
            {
                return;
            }

            btnClose.Location =
                new Point(
                    overlayForm.ClientSize.Width -
                    btnClose.Width -
                    14,

                    14
                );
        }

        // ====================================================
        // ENTRANCE ANIMATION
        // ====================================================

        /// <summary>
        /// Starts the sequential slide-in animation after the
        /// video has already begun playing.
        /// </summary>
        private void StartEntranceAnimation()
        {
            if (isNavigatingToLogin)
                return;

            if (overlayForm == null ||
                overlayForm.IsDisposed)
            {
                return;
            }

            PositionOverlayForm();

            animationStartTime = DateTime.Now;

            // ------------------------------------------------
            // Prepare each element outside the left side.
            // ------------------------------------------------

            PrepareAnimation(
                lblBrand,
                -140
            );

            PrepareAnimation(
                brandAccent,
                -80
            );

            PrepareAnimation(
                lblTitle,
                -180
            );

            PrepareAnimation(
                lblWelcome,
                -140
            );

            PrepareAnimation(
                lblDescription,
                -120
            );

            PrepareAnimation(
                lblInstruction,
                -100
            );

            PrepareAnimation(
                btnGetStarted,
                -140
            );

            PrepareAnimation(
                lblFooter,
                -100
            );

            // Close button is available immediately.
            btnClose.Visible = true;

            // Show the transparent UI layer only now.
            overlayForm.Show(this);
            overlayForm.BringToFront();

            // ------------------------------------------------
            // Start animation timer.
            // ------------------------------------------------

            animationTimer?.Stop();
            animationTimer?.Dispose();

            animationTimer =
                new System.Windows.Forms.Timer
                {
                    Interval = 15
                };

            animationTimer.Tick +=
                AnimationTimer_Tick;

            animationTimer.Start();
        }

        /// <summary>
        /// Places a control at its starting animation position
        /// and hides it until its animation delay has elapsed.
        /// </summary>
        private void PrepareAnimation(
            Control control,
            int startX)
        {
            control.Left = startX;
            control.Visible = false;
        }

        /// <summary>
        /// Updates all splash elements during the entrance
        /// animation using individual delays and durations.
        /// </summary>
        private void AnimationTimer_Tick(
            object? sender,
            EventArgs e)
        {
            if (isNavigatingToLogin)
                return;

            double elapsed =
                (DateTime.Now - animationStartTime)
                .TotalMilliseconds;

            // ------------------------------------------------
            // BRAND
            // ------------------------------------------------

            AnimateControl(
                lblBrand,
                -140,
                48,
                elapsed,
                0,
                450
            );

            // Small orange brand accent.
            AnimateControl(
                brandAccent,
                -80,
                49,
                elapsed,
                80,
                400
            );

            // ------------------------------------------------
            // TITLE
            // ------------------------------------------------

            AnimateControl(
                lblTitle,
                -180,
                48,
                elapsed,
                120,
                600
            );

            // ------------------------------------------------
            // WELCOME
            // ------------------------------------------------

            AnimateControl(
                lblWelcome,
                -140,
                50,
                elapsed,
                350,
                450
            );

            // ------------------------------------------------
            // DESCRIPTION
            // ------------------------------------------------

            AnimateControl(
                lblDescription,
                -120,
                50,
                elapsed,
                470,
                450
            );

            // ------------------------------------------------
            // INSTRUCTION
            // ------------------------------------------------

            AnimateControl(
                lblInstruction,
                -100,
                50,
                elapsed,
                580,
                450
            );

            // ------------------------------------------------
            // GET STARTED
            // ------------------------------------------------

            AnimateControl(
                btnGetStarted,
                -140,
                48,
                elapsed,
                700,
                500
            );

            // ------------------------------------------------
            // FOOTER
            // ------------------------------------------------

            AnimateControl(
                lblFooter,
                -100,
                50,
                elapsed,
                850,
                450
            );

            // ------------------------------------------------
            // Finish animation.
            // ------------------------------------------------

            if (elapsed >= 1350)
            {
                SetAnimationEndState();

                animationTimer?.Stop();
                animationTimer?.Dispose();
                animationTimer = null;
            }
        }

        /// <summary>
        /// Animates one control from a starting horizontal
        /// position to its final position using an ease-out
        /// cubic curve.
        /// </summary>
        private void AnimateControl(
            Control control,
            int startX,
            int endX,
            double elapsed,
            int delay,
            int duration)
        {
            double progress =
                (elapsed - delay) /
                duration;

            // Animation has not started yet.
            if (progress <= 0)
            {
                control.Visible = false;
                control.Left = startX;
                return;
            }

            control.Visible = true;

            // Animation has completed.
            if (progress >= 1)
            {
                control.Left = endX;
                return;
            }

            // ------------------------------------------------
            // Ease-out cubic.
            // ------------------------------------------------

            double easedProgress =
                1 -
                Math.Pow(
                    1 - progress,
                    3
                );

            control.Left =
                startX +
                (int)(
                    (endX - startX) *
                    easedProgress
                );
        }

        /// <summary>
        /// Forces all splash elements into their final
        /// positions once the animation has completed.
        /// </summary>
        private void SetAnimationEndState()
        {
            lblBrand.Left = 48;
            brandAccent.Left = 49;

            lblTitle.Left = 48;

            lblWelcome.Left = 50;

            lblDescription.Left = 50;

            lblInstruction.Left = 50;

            btnGetStarted.Left = 48;

            lblFooter.Left = 50;

            // Ensure every element is visible.
            lblBrand.Visible = true;
            brandAccent.Visible = true;
            lblTitle.Visible = true;
            lblWelcome.Visible = true;
            lblDescription.Visible = true;
            lblInstruction.Visible = true;
            btnGetStarted.Visible = true;
            lblFooter.Visible = true;
            btnClose.Visible = true;
        }

        // ====================================================
        // BUTTON EVENTS
        // ====================================================

        /// <summary>
        /// Navigates from the WelcomeScreen to the LoginForm.
        /// </summary>
        private void BtnGetStarted_Click(
            object? sender,
            EventArgs e)
        {
            // Prevent double-clicks from creating
            // multiple LoginForm instances.
            if (isNavigatingToLogin)
                return;

            isNavigatingToLogin = true;

            btnGetStarted.Enabled = false;

            // ------------------------------------------------
            // Stop splash timers immediately.
            // ------------------------------------------------

            uiStartTimer?.Stop();
            animationTimer?.Stop();
            videoTimer?.Stop();

            // ------------------------------------------------
            // Freeze the current visual state of the
            // WelcomeScreen so it is stable while the next
            // form is being prepared.
            // ------------------------------------------------

            if (!videoFrozen)
            {
                axWindowsMediaPlayer.Ctlcontrols.pause();
                videoFrozen = true;
            }

            SetAnimationEndState();

            // ------------------------------------------------
            // Hide the transparent overlay.
            // ------------------------------------------------

            try
            {
                if (overlayForm != null &&
                    !overlayForm.IsDisposed)
                {
                    overlayForm.Hide();
                }
            }
            catch
            {
                // Continue with navigation.
            }

            // ------------------------------------------------
            // Create the LoginForm while the WelcomeScreen
            // is still visible underneath.
            // ------------------------------------------------

            LoginForm targetForm =
                new LoginForm();

            // Re-enable the button if LoginForm is closed
            // for any reason.
            targetForm.FormClosed +=
                (s, args) =>
                {
                    if (!IsDisposed)
                    {
                        btnGetStarted.Enabled = true;
                    }
                };

            // ------------------------------------------------
            // Show the LoginForm and force an immediate update
            // before hiding the WelcomeScreen.
            // ------------------------------------------------

            targetForm.Show();

            targetForm.Update();

            targetForm.BringToFront();
            targetForm.Activate();

            // The LoginForm has now been shown and explicitly
            // asked to paint before the WelcomeScreen disappears.
            Hide();
        }

        /// <summary>
        /// Closes the WeAreCars application.
        /// </summary>
        private void BtnClose_Click(
            object? sender,
            EventArgs e)
        {
            isNavigatingToLogin = false;

            try
            {
                if (overlayForm != null &&
                    !overlayForm.IsDisposed)
                {
                    overlayForm.Close();
                }
            }
            catch
            {
                // Ignore overlay cleanup errors.
            }

            Close();
        }

        /// <summary>
        /// Returns to the existing WelcomeScreen instance
        /// after the LoginForm has been closed.
        /// </summary>
        public void ReturnFromLogin()
        {
            isNavigatingToLogin = false;

            // Ensure the splash UI is in a stable final state.
            SetAnimationEndState();

            // Re-enable navigation.
            if (btnGetStarted != null &&
                !btnGetStarted.IsDisposed)
            {
                btnGetStarted.Enabled = true;
            }

            // Show the existing WelcomeScreen first.
            Show();
            BringToFront();
            Activate();

            // Then show the existing overlay.
            if (overlayForm != null &&
                !overlayForm.IsDisposed)
            {
                overlayForm.Show(this);
                overlayForm.BringToFront();
                overlayForm.Activate();
            }
        }

        /// <summary>
        /// Highlights the close button when the mouse enters.
        /// </summary>
        private void BtnClose_MouseEnter(
            object? sender,
            EventArgs e)
        {
            btnClose.ForeColor =
                Color.FromArgb(
                    249,
                    115,
                    22);
        }

        /// <summary>
        /// Restores the normal close-button colour.
        /// </summary>
        private void BtnClose_MouseLeave(
            object? sender,
            EventArgs e)
        {
            btnClose.ForeColor =
                Color.White;
        }

        // ====================================================
        // BUTTON SHAPE
        // ====================================================

        /// <summary>
        /// Creates a rounded rectangle path used by the
        /// Get Started button.
        /// </summary>
        private GraphicsPath CreateRoundedRectanglePath(
            Rectangle bounds,
            int radius)
        {
            int diameter =
                radius * 2;

            GraphicsPath path =
                new GraphicsPath();

            path.StartFigure();

            // Top-left corner.
            path.AddArc(
                bounds.X,
                bounds.Y,
                diameter,
                diameter,
                180,
                90
            );

            // Top-right corner.
            path.AddArc(
                bounds.Right - diameter,
                bounds.Y,
                diameter,
                diameter,
                270,
                90
            );

            // Bottom-right corner.
            path.AddArc(
                bounds.Right - diameter,
                bounds.Bottom - diameter,
                diameter,
                diameter,
                0,
                90
            );

            // Bottom-left corner.
            path.AddArc(
                bounds.X,
                bounds.Bottom - diameter,
                diameter,
                diameter,
                90,
                90
            );

            path.CloseFigure();

            return path;
        }

        // ====================================================
        // CLEANUP
        // ====================================================

        /// <summary>
        /// Stops all timers and hides the overlay before the
        /// main form starts closing.
        /// </summary>
        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            isNavigatingToLogin = false;

            uiStartTimer?.Stop();
            uiStartTimer?.Dispose();
            uiStartTimer = null;

            animationTimer?.Stop();
            animationTimer?.Dispose();
            animationTimer = null;

            videoTimer?.Stop();

            if (overlayForm != null &&
                !overlayForm.IsDisposed)
            {
                overlayForm.Hide();
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Releases video, animation and overlay resources
        /// after the form has closed.
        /// </summary>
        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            videoTimer?.Stop();
            videoTimer?.Dispose();
            videoTimer = null;

            splashToolTip?.Dispose();
            splashToolTip = null!;

            if (overlayForm != null &&
                !overlayForm.IsDisposed)
            {
                overlayForm.Hide();
                overlayForm.Dispose();
            }

            axWindowsMediaPlayer.Ctlcontrols.stop();

            base.OnFormClosed(e);
        }

        private void picEndFrame_Click(
            object? sender,
            EventArgs e)
        {
        }
    }
}