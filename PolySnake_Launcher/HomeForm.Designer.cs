namespace PolySnake_Launcher
{
    partial class HomeForm
    {
        private System.ComponentModel.IContainer components = null;

        // ── Controls ──────────────────────────────────────────────────────────────
        private System.Windows.Forms.Label     lblTitle;
        private System.Windows.Forms.Label     lblSubtitle;
        private System.Windows.Forms.Label     lblVersion;
        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Button    btnStartGame;
        private System.Windows.Forms.Button    btnSettings;
        private System.Windows.Forms.Button    btnCredits;
        private System.Windows.Forms.Button    btnGitHub;
        private System.Windows.Forms.Label     lblPlaceholderNote;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            picBackground = new PictureBox();
            lblPlaceholderNote = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblVersion = new Label();
            btnStartGame = new Button();
            btnSettings = new Button();
            btnCredits = new Button();
            btnGitHub = new Button();
            ((System.ComponentModel.ISupportInitialize)picBackground).BeginInit();
            SuspendLayout();
            // 
            // picBackground
            // 
            picBackground.BackColor = Color.FromArgb(45, 45, 48);
            picBackground.Location = new Point(0, 0);
            picBackground.Name = "picBackground";
            picBackground.Size = new Size(1280, 720);
            picBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            picBackground.TabIndex = 0;
            picBackground.TabStop = false;
            // 
            // lblPlaceholderNote
            // 
            lblPlaceholderNote.Location = new Point(0, 0);
            lblPlaceholderNote.Name = "lblPlaceholderNote";
            lblPlaceholderNote.Size = new Size(100, 23);
            lblPlaceholderNote.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1280, 70);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "POLYSNAKE";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblSubtitle.ForeColor = Color.FromArgb(200, 200, 200);
            lblSubtitle.Location = new Point(0, 135);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(1280, 26);
            lblSubtitle.TabIndex = 3;
            lblSubtitle.Text = "A Geometric Snake Experience";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubtitle.Click += lblSubtitle_Click;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI", 7.5F);
            lblVersion.ForeColor = Color.FromArgb(120, 120, 120);
            lblVersion.Location = new Point(1180, 700);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(74, 17);
            lblVersion.TabIndex = 4;
            lblVersion.Text = "v1.0.0-BETA";
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = Color.FromArgb(63, 63, 70);
            btnStartGame.FlatAppearance.BorderColor = Color.FromArgb(120, 120, 120);
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnStartGame.ForeColor = Color.White;
            btnStartGame.Location = new Point(490, 560);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(300, 60);
            btnStartGame.TabIndex = 5;
            btnStartGame.Text = "▶   START GAME";
            btnStartGame.UseVisualStyleBackColor = false;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.FromArgb(45, 45, 48);
            btnSettings.FlatAppearance.BorderColor = Color.FromArgb(85, 85, 85);
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 8.5F);
            btnSettings.ForeColor = Color.FromArgb(200, 200, 200);
            btnSettings.Location = new Point(20, 670);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(110, 34);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "⚙  Settings";
            btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnCredits
            // 
            btnCredits.BackColor = Color.FromArgb(45, 45, 48);
            btnCredits.FlatAppearance.BorderColor = Color.FromArgb(85, 85, 85);
            btnCredits.FlatStyle = FlatStyle.Flat;
            btnCredits.Font = new Font("Segoe UI", 8.5F);
            btnCredits.ForeColor = Color.FromArgb(200, 200, 200);
            btnCredits.Location = new Point(522, 670);
            btnCredits.Name = "btnCredits";
            btnCredits.Size = new Size(110, 34);
            btnCredits.TabIndex = 7;
            btnCredits.Text = "★  Credits";
            btnCredits.UseVisualStyleBackColor = false;
            // 
            // btnGitHub
            // 
            btnGitHub.BackColor = Color.FromArgb(45, 45, 48);
            btnGitHub.FlatAppearance.BorderColor = Color.FromArgb(85, 85, 85);
            btnGitHub.FlatStyle = FlatStyle.Flat;
            btnGitHub.Font = new Font("Segoe UI", 8.5F);
            btnGitHub.ForeColor = Color.FromArgb(200, 200, 200);
            btnGitHub.Location = new Point(650, 670);
            btnGitHub.Name = "btnGitHub";
            btnGitHub.Size = new Size(110, 34);
            btnGitHub.TabIndex = 8;
            btnGitHub.Text = "⌂  GitHub";
            btnGitHub.UseVisualStyleBackColor = false;
            // 
            // HomeForm
            // 
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1280, 720);
            Controls.Add(btnGitHub);
            Controls.Add(btnCredits);
            Controls.Add(btnSettings);
            Controls.Add(btnStartGame);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblVersion);
            Controls.Add(picBackground);
            Name = "HomeForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "S";
            ((System.ComponentModel.ISupportInitialize)picBackground).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
