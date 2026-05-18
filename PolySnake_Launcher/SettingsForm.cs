using System;
using System.Drawing;
using System.Windows.Forms;

namespace PolySnake_Launcher
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            ApplyHoverStyles();
            LoadCurrentSettings();

            // Set up ESC key handler to close the form
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            // Real-time trackbar label updates
            trackMasterVol.Scroll += (s, e) => lblMasterVolVal.Text = $"{trackMasterVol.Value}%";
            trackMusicVol.Scroll += (s, e) => lblMusicVolVal.Text = $"{trackMusicVol.Value}%";
            trackSFXVol.Scroll += (s, e) => lblSFXVolVal.Text = $"{trackSFXVol.Value}%";

            // Wire up buttons
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void ApplyHoverStyles()
        {
            // Hover styles for SAVE & APPLY
            Color normalSave = Color.FromArgb(63, 63, 70);
            Color hoverSave = Color.FromArgb(85, 85, 90);
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = hoverSave;
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = normalSave;

            // Hover styles for CANCEL
            Color normalCancel = Color.FromArgb(45, 45, 48);
            Color hoverCancel = Color.FromArgb(63, 63, 70);
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = hoverCancel;
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = normalCancel;
        }

        private void LoadCurrentSettings()
        {
            SystemSettings current = SystemSettings.Load();

            // 1. Resolution
            int resIndex = comboResolution.FindStringExact(current.Resolution);
            if (resIndex >= 0)
                comboResolution.SelectedIndex = resIndex;
            else
                comboResolution.SelectedIndex = 1; // Default to 1280x720 (index 1)

            // 2. Frame Rate Limit
            string fpsString = current.FrameRateLimit switch
            {
                30 => "30 FPS",
                60 => "60 FPS",
                120 => "120 FPS",
                144 => "144 FPS",
                _ => "Unlimited"
            };

            int fpsIndex = comboFrameRate.FindStringExact(fpsString);
            if (fpsIndex >= 0)
                comboFrameRate.SelectedIndex = fpsIndex;
            else
                comboFrameRate.SelectedIndex = 1; // Default to 60 FPS (index 1)

            // 2b. Fullscreen
            chkFullscreen.Checked = current.Fullscreen;

            // 3. Audio Volumes
            int masterPct = (int)Math.Round(current.MasterVolume * 100);
            int musicPct = (int)Math.Round(current.MusicVolume * 100);
            int sfxPct = (int)Math.Round(current.SFXVolume * 100);

            trackMasterVol.Value = Math.Clamp(masterPct, 0, 100);
            trackMusicVol.Value = Math.Clamp(musicPct, 0, 100);
            trackSFXVol.Value = Math.Clamp(sfxPct, 0, 100);

            lblMasterVolVal.Text = $"{trackMasterVol.Value}%";
            lblMusicVolVal.Text = $"{trackMusicVol.Value}%";
            lblSFXVolVal.Text = $"{trackSFXVol.Value}%";
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            SystemSettings current = new SystemSettings();

            // 1. Resolution
            if (comboResolution.SelectedItem != null)
            {
                current.Resolution = comboResolution.SelectedItem.ToString() ?? "1280x720";
            }

            // 2. Frame Rate Limit
            if (comboFrameRate.SelectedItem != null)
            {
                string fpsText = comboFrameRate.SelectedItem.ToString() ?? "60 FPS";
                current.FrameRateLimit = fpsText switch
                {
                    "30 FPS" => 30,
                    "60 FPS" => 60,
                    "120 FPS" => 120,
                    "144 FPS" => 144,
                    _ => -1
                };
            }

            // 2b. Fullscreen
            current.Fullscreen = chkFullscreen.Checked;

            // 3. Volumes
            current.MasterVolume = trackMasterVol.Value / 100f;
            current.MusicVolume = trackMusicVol.Value / 100f;
            current.SFXVolume = trackSFXVol.Value / 100f;

            current.Save();

            MessageBox.Show("System settings saved and applied!", "Settings Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
