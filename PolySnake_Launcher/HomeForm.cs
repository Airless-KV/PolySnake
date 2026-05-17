using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PolySnake_Launcher
{
    public partial class HomeForm : Form
    {
        // Soft dark grey palette — matches Form1
        private readonly Color darkBg     = Color.FromArgb(45, 45, 48);
        private readonly Color accentBtn  = Color.FromArgb(63, 63, 70);   // slightly lighter grey for buttons
        private readonly Color borderCol  = Color.FromArgb(85, 85, 85);   // subtle border

        public HomeForm()
        {
            InitializeComponent();
            ApplyDarkTheme();
            LoadBackgroundImage();
            
            // Wire up events
            btnStartGame.Click += BtnStartGame_Click;
            btnSettings.Click += BtnSettings_Click;
            btnCredits.Click += BtnCredits_Click;
            btnGitHub.Click += BtnGitHub_Click;

            // Hover effects
            SetupHoverEffects(btnStartGame, Color.FromArgb(63, 63, 70), Color.FromArgb(85, 85, 90));
            SetupHoverEffects(btnSettings, Color.FromArgb(45, 45, 48), Color.FromArgb(63, 63, 70));
            SetupHoverEffects(btnCredits, Color.FromArgb(45, 45, 48), Color.FromArgb(63, 63, 70));
            SetupHoverEffects(btnGitHub, Color.FromArgb(45, 45, 48), Color.FromArgb(63, 63, 70));

            // ESC closes the launcher
            this.KeyPreview = true;
            this.KeyDown   += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
        }

        private void SetupHoverEffects(Button btn, Color normal, Color hover)
        {
            btn.MouseEnter += (s, e) => { btn.BackColor = hover; };
            btn.MouseLeave += (s, e) => { btn.BackColor = normal; };
        }

        // ─── Load background PNG/GIF next to the .exe ────────────────────────────
        private void LoadBackgroundImage()
        {
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;

            string[] candidates = {
                System.IO.Path.Combine(exeDir, "background.png"),
                System.IO.Path.Combine(exeDir, "background.gif"),
                System.IO.Path.Combine(exeDir, "background.jpg"),
            };

            foreach (string path in candidates)
            {
                if (System.IO.File.Exists(path))
                {
                    Image img = Image.FromFile(path);
                    picBackground.Image    = img;
                    picBackground.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Resize form and picBackground to exactly 1280x720
                    this.ClientSize        = new Size(1280, 720);
                    picBackground.Size     = new Size(1280, 720);
                    picBackground.Location = new Point(0, 0);

                    // Update labels to span the full width
                    lblTitle.Size          = new Size(1280, 70);
                    lblSubtitle.Size       = new Size(1280, 26);
                    lblPlaceholderNote.Visible = false;
                    return;
                }
            }
            // No image found — ensure form is still 1280x720 with no border
            this.ClientSize        = new Size(1280, 720);
            picBackground.Size     = new Size(1280, 720);
            lblTitle.Size          = new Size(1280, 70);
            lblSubtitle.Size       = new Size(1280, 26);
        }



        // ─── Dark theme ───────────────────────────────────────────────────────────
        private void ApplyDarkTheme()
        {
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;
        }

        // ─── Solid soft-dark background (no gradient) ───────────────────────────────────
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Simple solid fill matching Form1
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(45, 45, 48)), this.ClientRectangle);
        }

        // ─── Button click handlers ────────────────────────────────────────────────
        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            var settingsForm = new Form1();
            settingsForm.FormClosed += (s, args) => this.Show();
            settingsForm.Show();
            this.Hide();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Launcher settings coming soon!",
                "Settings",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnCredits_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "PolySnake\n\nDeveloped by the Airless-KV Team.\n\nThanks for playing!",
                "Credits",
                MessageBoxButtons.OK,
                MessageBoxIcon.None);
        }

        private void BtnGitHub_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName        = "https://github.com/Airless-KV/PolySnake",
                UseShellExecute = true
            });
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {
        }
    }
}
