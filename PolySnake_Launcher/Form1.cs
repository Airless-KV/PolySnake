using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.Json;

namespace PolySnake_Launcher
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer resultsTimer;

        public Form1()
        {
            InitializeComponent();
            
            // ESC key closes settings and goes back (no title bar since window is borderless)
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };

            resultsTimer = new System.Windows.Forms.Timer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyDarkMode(this);

            comboMapType.SelectedIndex = 0; // Default to TM1_MainTestScene
            comboGameMode.SelectedIndex = 0; // Default to Time Limit
            comboTimeOfDay.SelectedIndex = 0; // Default to Day
            comboDifficulty.SelectedIndex = 1; // Default to Normal

            // Start listening for game results!
            resultsTimer.Interval = 1000; // Check every 1 second
            resultsTimer.Tick += ResultsTimer_Tick;
            resultsTimer.Start();
        }

        private void ResultsTimer_Tick(object? sender, EventArgs e)
        {
            string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string resultsPath = Path.Combine(docsPath, "SnakeGameResults.json");

            if (File.Exists(resultsPath))
            {
                resultsTimer.Stop(); // Pause timer while we process

                try
                {
                    string json = File.ReadAllText(resultsPath);
                    var result = JsonSerializer.Deserialize<GameResults>(json);

                    // Delete the file
                    File.Delete(resultsPath);

                    // Force the Launcher to the front of the screen
                    this.TopMost = true;
                    MessageBox.Show($"Game Finished!\n\nReason: {result.Reason}\nFinal Score: {result.FinalScore}", "Score Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.TopMost = false;
                }
                catch { }

                resultsTimer.Start(); // Resume listening for the next game
            }
        }

        public class GameResults
        {
            public int FinalScore { get; set; }
            public string? Reason { get; set; }
        }

        private void ApplyDarkMode(Control control)
        {
            // Soft dark grey — VS Code style
            control.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            control.ForeColor = System.Drawing.Color.White;

            foreach (Control child in control.Controls)
            {
                ApplyDarkMode(child);

                if (child is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(85, 85, 85);
                }

                if (child is Label sep && sep.Name.StartsWith("sep"))
                {
                    sep.BackColor = System.Drawing.Color.FromArgb(85, 85, 85);
                }
            }
        }

        private void comboGameMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide everything first
            lblTimeLimit.Visible = false;
            txtTimeLimit.Visible = false;
            lblTargetSize.Visible = false;
            txtTargetSize.Visible = false;

            string? selectedMode = comboGameMode.SelectedItem?.ToString();

            // Show only what's relevant for the selected game mode
            if (selectedMode == "Time Limit")
            {
                lblTimeLimit.Visible = true;
                txtTimeLimit.Visible = true;
            }
            else if (selectedMode == "Target Size")
            {
                lblTargetSize.Visible = true;
                txtTargetSize.Visible = true;
            }
        }

        private void trackMapSize_Scroll(object sender, EventArgs e)
        {
            lblMapSize.Text = "Map Size: " + trackMapSize.Value;
        }

        private void comboDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? diff = comboDifficulty.SelectedItem?.ToString();
            if (diff == "Easy")
            {
                trackPlayerSpeed.Value = 5;
                trackPlayerSpeed.Enabled = false;
            }
            else if (diff == "Normal")
            {
                trackPlayerSpeed.Value = 10;
                trackPlayerSpeed.Enabled = false;
            }
            else if (diff == "Hard")
            {
                trackPlayerSpeed.Value = 20;
                trackPlayerSpeed.Enabled = false;
            }
            else if (diff == "Custom")
            {
                trackPlayerSpeed.Enabled = true;
            }

            lblPlayerSpeed.Text = "Player Speed: " + trackPlayerSpeed.Value;
        }

        private void trackPlayerSpeed_Scroll(object sender, EventArgs e)
        {
            lblPlayerSpeed.Text = "Player Speed: " + trackPlayerSpeed.Value;
        }

        public class GameSettings
        {
            public string MapMode { get; set; } = "TM1_MainTestScene";
            public float MapSize { get; set; }
            public string GameMode { get; set; } = "Time Limit";
            public float TimeLimitSeconds { get; set; }
            public int TargetSize { get; set; }
            public bool IsEndurance { get; set; }
            public string TimeOfDay { get; set; } = "Day";
            public float PlayerSpeed { get; set; } = 20f;
            public string Difficulty { get; set; } = "Normal";
        }

        private void btnLaunchGame_Click(object sender, EventArgs e)
        {
            try
            {
                float parsedMapSize = trackMapSize.Value;

                string selectedMode = comboGameMode.SelectedItem?.ToString() ?? "Time Limit";

                GameSettings settings = new GameSettings
                {
                    MapMode = comboMapType.SelectedItem?.ToString() ?? "TM1_MainTestScene",
                    MapSize = parsedMapSize,
                    GameMode = selectedMode,
                    TimeLimitSeconds = float.TryParse(txtTimeLimit.Text, out float timeLimit) ? timeLimit : 120.0f,
                    TargetSize = int.TryParse(txtTargetSize.Text, out int targetSize) ? targetSize : 50,
                    IsEndurance = (selectedMode == "Endurance"),
                    TimeOfDay = comboTimeOfDay.SelectedItem?.ToString() ?? "Day",
                    PlayerSpeed = trackPlayerSpeed.Value,
                    Difficulty = comboDifficulty.SelectedItem?.ToString() ?? "Normal"
                };

                string jsonString = JsonSerializer.Serialize(settings);
                string docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(docsPath, "SnakeGameSettings.json");
                File.WriteAllText(filePath, jsonString);

                string gamePath = @"..\..\..\..\PolySnake_Game\Build\PolySnake.exe";
                if (File.Exists(gamePath))
                {
                    Process.Start(gamePath);
                }
                else
                {
                    MessageBox.Show("Settings saved successfully to My Documents!\n\nTo actually launch the game from here, build your Unity project and update the path in Form1.cs.", "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
            this.Close();
        }
    }
}
