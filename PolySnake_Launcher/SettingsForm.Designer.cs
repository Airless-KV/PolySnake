namespace PolySnake_Launcher
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlVideo;
        private System.Windows.Forms.Panel pnlAudio;

        // Video Settings
        private System.Windows.Forms.Label lblVideoHeader;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ComboBox comboResolution;
        private System.Windows.Forms.Label lblFrameRate;
        private System.Windows.Forms.ComboBox comboFrameRate;
        private System.Windows.Forms.CheckBox chkFullscreen;

        // Audio Settings
        private System.Windows.Forms.Label lblAudioHeader;
        private System.Windows.Forms.Label lblMasterVol;
        private System.Windows.Forms.TrackBar trackMasterVol;
        private System.Windows.Forms.Label lblMasterVolVal;
        
        private System.Windows.Forms.Label lblMusicVol;
        private System.Windows.Forms.TrackBar trackMusicVol;
        private System.Windows.Forms.Label lblMusicVolVal;
        
        private System.Windows.Forms.Label lblSFXVol;
        private System.Windows.Forms.TrackBar trackSFXVol;
        private System.Windows.Forms.Label lblSFXVolVal;

        // Action Buttons
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlVideo = new Panel();
            lblVideoHeader = new Label();
            lblResolution = new Label();
            comboResolution = new ComboBox();
            lblFrameRate = new Label();
            comboFrameRate = new ComboBox();
            chkFullscreen = new CheckBox();
            pnlAudio = new Panel();
            lblAudioHeader = new Label();
            lblMasterVol = new Label();
            trackMasterVol = new TrackBar();
            lblMasterVolVal = new Label();
            lblMusicVol = new Label();
            trackMusicVol = new TrackBar();
            lblMusicVolVal = new Label();
            lblSFXVol = new Label();
            trackSFXVol = new TrackBar();
            lblSFXVolVal = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            pnlVideo.SuspendLayout();
            pnlAudio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackMasterVol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackMusicVol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackSFXVol).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 40);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1280, 70);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "SYSTEM SETTINGS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlVideo
            // 
            pnlVideo.BackColor = Color.FromArgb(53, 53, 56);
            pnlVideo.BorderStyle = BorderStyle.FixedSingle;
            pnlVideo.Controls.Add(lblVideoHeader);
            pnlVideo.Controls.Add(lblResolution);
            pnlVideo.Controls.Add(comboResolution);
            pnlVideo.Controls.Add(lblFrameRate);
            pnlVideo.Controls.Add(comboFrameRate);
            pnlVideo.Controls.Add(chkFullscreen);
            pnlVideo.Location = new Point(120, 160);
            pnlVideo.Name = "pnlVideo";
            pnlVideo.Size = new Size(480, 360);
            pnlVideo.TabIndex = 1;
            // 
            // lblVideoHeader
            // 
            lblVideoHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblVideoHeader.ForeColor = Color.FromArgb(0, 122, 204);
            lblVideoHeader.Location = new Point(20, 20);
            lblVideoHeader.Name = "lblVideoHeader";
            lblVideoHeader.Size = new Size(440, 40);
            lblVideoHeader.TabIndex = 0;
            lblVideoHeader.Text = "Display / Video";
            // 
            // lblResolution
            // 
            lblResolution.Font = new Font("Segoe UI", 11F);
            lblResolution.ForeColor = Color.White;
            lblResolution.Location = new Point(20, 90);
            lblResolution.Name = "lblResolution";
            lblResolution.Size = new Size(440, 25);
            lblResolution.TabIndex = 1;
            lblResolution.Text = "Resolution";
            // 
            // comboResolution
            // 
            comboResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            comboResolution.Font = new Font("Segoe UI", 10F);
            comboResolution.FormattingEnabled = true;
            comboResolution.Items.AddRange(new object[] { "1920x1080", "1280x720", "2560x1440", "1600x900", "1024x768" });
            comboResolution.Location = new Point(20, 125);
            comboResolution.Name = "comboResolution";
            comboResolution.Size = new Size(400, 31);
            comboResolution.TabIndex = 2;
            // 
            // lblFrameRate
            // 
            lblFrameRate.Font = new Font("Segoe UI", 11F);
            lblFrameRate.ForeColor = Color.White;
            lblFrameRate.Location = new Point(20, 190);
            lblFrameRate.Name = "lblFrameRate";
            lblFrameRate.Size = new Size(440, 25);
            lblFrameRate.TabIndex = 3;
            lblFrameRate.Text = "Frame Rate Limit";
            // 
            // comboFrameRate
            // 
            comboFrameRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFrameRate.Font = new Font("Segoe UI", 10F);
            comboFrameRate.FormattingEnabled = true;
            comboFrameRate.Items.AddRange(new object[] { "30 FPS", "60 FPS", "120 FPS", "144 FPS", "Unlimited" });
            comboFrameRate.Location = new Point(20, 225);
            comboFrameRate.Name = "comboFrameRate";
            comboFrameRate.Size = new Size(400, 31);
            comboFrameRate.TabIndex = 4;
            // 
            // chkFullscreen
            // 
            chkFullscreen.AutoSize = true;
            chkFullscreen.Font = new Font("Segoe UI", 11F);
            chkFullscreen.ForeColor = Color.White;
            chkFullscreen.Location = new Point(20, 285);
            chkFullscreen.Name = "chkFullscreen";
            chkFullscreen.Size = new Size(173, 29);
            chkFullscreen.TabIndex = 5;
            chkFullscreen.Text = "Fullscreen Mode";
            chkFullscreen.UseVisualStyleBackColor = true;
            // 
            // pnlAudio
            // 
            pnlAudio.BackColor = Color.FromArgb(53, 53, 56);
            pnlAudio.BorderStyle = BorderStyle.FixedSingle;
            pnlAudio.Controls.Add(lblAudioHeader);
            pnlAudio.Controls.Add(lblMasterVol);
            pnlAudio.Controls.Add(trackMasterVol);
            pnlAudio.Controls.Add(lblMasterVolVal);
            pnlAudio.Controls.Add(lblMusicVol);
            pnlAudio.Controls.Add(trackMusicVol);
            pnlAudio.Controls.Add(lblMusicVolVal);
            pnlAudio.Controls.Add(lblSFXVol);
            pnlAudio.Controls.Add(trackSFXVol);
            pnlAudio.Controls.Add(lblSFXVolVal);
            pnlAudio.Location = new Point(680, 160);
            pnlAudio.Name = "pnlAudio";
            pnlAudio.Size = new Size(480, 360);
            pnlAudio.TabIndex = 2;
            // 
            // lblAudioHeader
            // 
            lblAudioHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblAudioHeader.ForeColor = Color.FromArgb(0, 122, 204);
            lblAudioHeader.Location = new Point(20, 20);
            lblAudioHeader.Name = "lblAudioHeader";
            lblAudioHeader.Size = new Size(440, 40);
            lblAudioHeader.TabIndex = 0;
            lblAudioHeader.Text = "Audio / Volume";
            // 
            // lblMasterVol
            // 
            lblMasterVol.Font = new Font("Segoe UI", 11F);
            lblMasterVol.ForeColor = Color.White;
            lblMasterVol.Location = new Point(20, 80);
            lblMasterVol.Name = "lblMasterVol";
            lblMasterVol.Size = new Size(200, 25);
            lblMasterVol.TabIndex = 1;
            lblMasterVol.Text = "Master Volume";
            // 
            // trackMasterVol
            // 
            trackMasterVol.Location = new Point(20, 110);
            trackMasterVol.Maximum = 100;
            trackMasterVol.Name = "trackMasterVol";
            trackMasterVol.Size = new Size(360, 56);
            trackMasterVol.TabIndex = 2;
            trackMasterVol.TickStyle = TickStyle.None;
            // 
            // lblMasterVolVal
            // 
            lblMasterVolVal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMasterVolVal.ForeColor = Color.White;
            lblMasterVolVal.Location = new Point(390, 110);
            lblMasterVolVal.Name = "lblMasterVolVal";
            lblMasterVolVal.Size = new Size(70, 25);
            lblMasterVolVal.TabIndex = 3;
            lblMasterVolVal.Text = "100%";
            lblMasterVolVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblMusicVol
            // 
            lblMusicVol.Font = new Font("Segoe UI", 11F);
            lblMusicVol.ForeColor = Color.White;
            lblMusicVol.Location = new Point(20, 170);
            lblMusicVol.Name = "lblMusicVol";
            lblMusicVol.Size = new Size(200, 25);
            lblMusicVol.TabIndex = 4;
            lblMusicVol.Text = "Music / BGM Volume";
            // 
            // trackMusicVol
            // 
            trackMusicVol.Location = new Point(20, 200);
            trackMusicVol.Maximum = 100;
            trackMusicVol.Name = "trackMusicVol";
            trackMusicVol.Size = new Size(360, 56);
            trackMusicVol.TabIndex = 5;
            trackMusicVol.TickStyle = TickStyle.None;
            // 
            // lblMusicVolVal
            // 
            lblMusicVolVal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMusicVolVal.ForeColor = Color.White;
            lblMusicVolVal.Location = new Point(390, 200);
            lblMusicVolVal.Name = "lblMusicVolVal";
            lblMusicVolVal.Size = new Size(70, 25);
            lblMusicVolVal.TabIndex = 6;
            lblMusicVolVal.Text = "80%";
            lblMusicVolVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSFXVol
            // 
            lblSFXVol.Font = new Font("Segoe UI", 11F);
            lblSFXVol.ForeColor = Color.White;
            lblSFXVol.Location = new Point(20, 260);
            lblSFXVol.Name = "lblSFXVol";
            lblSFXVol.Size = new Size(200, 25);
            lblSFXVol.TabIndex = 7;
            lblSFXVol.Text = "SFX Volume";
            // 
            // trackSFXVol
            // 
            trackSFXVol.Location = new Point(20, 290);
            trackSFXVol.Maximum = 100;
            trackSFXVol.Name = "trackSFXVol";
            trackSFXVol.Size = new Size(360, 56);
            trackSFXVol.TabIndex = 8;
            trackSFXVol.TickStyle = TickStyle.None;
            // 
            // lblSFXVolVal
            // 
            lblSFXVolVal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSFXVolVal.ForeColor = Color.White;
            lblSFXVolVal.Location = new Point(390, 290);
            lblSFXVolVal.Name = "lblSFXVolVal";
            lblSFXVolVal.Size = new Size(70, 25);
            lblSFXVolVal.TabIndex = 9;
            lblSFXVolVal.Text = "80%";
            lblSFXVolVal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(63, 63, 70);
            btnSave.FlatAppearance.BorderColor = Color.FromArgb(85, 85, 85);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(410, 570);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(210, 50);
            btnSave.TabIndex = 3;
            btnSave.Text = "✔   SAVE & APPLY";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click_1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(45, 45, 48);
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(85, 85, 85);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 12F);
            btnCancel.ForeColor = Color.FromArgb(200, 200, 200);
            btnCancel.Location = new Point(660, 570);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(210, 50);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "✖   CANCEL";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 45, 48);
            ClientSize = new Size(1280, 720);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(pnlAudio);
            Controls.Add(pnlVideo);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PolySnake Settings";
            pnlVideo.ResumeLayout(false);
            pnlVideo.PerformLayout();
            pnlAudio.ResumeLayout(false);
            pnlAudio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackMasterVol).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackMusicVol).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackSFXVol).EndInit();
            ResumeLayout(false);
        }
    }
}
