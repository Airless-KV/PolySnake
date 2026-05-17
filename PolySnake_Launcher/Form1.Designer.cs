namespace PolySnake_Launcher
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label sep1;
        private System.Windows.Forms.Label sep2;
        private System.Windows.Forms.Label sep3;

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
            lblDifficulty = new Label();
            comboDifficulty = new ComboBox();
            lblPlayerSpeed = new Label();
            trackPlayerSpeed = new TrackBar();
            lblMapType = new Label();
            comboMapType = new ComboBox();
            lblMapSize = new Label();
            trackMapSize = new TrackBar();
            lblTimeOfDay = new Label();
            comboTimeOfDay = new ComboBox();
            lblGameMode = new Label();
            comboGameMode = new ComboBox();
            lblTimeLimit = new Label();
            txtTimeLimit = new TextBox();
            lblTargetSize = new Label();
            txtTargetSize = new TextBox();
            btnLaunchGame = new Button();
            sep1 = new Label();
            sep2 = new Label();
            sep3 = new Label();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)trackPlayerSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackMapSize).BeginInit();
            SuspendLayout();
            // 
            // lblDifficulty
            // 
            lblDifficulty.AutoSize = true;
            lblDifficulty.Location = new Point(34, 40);
            lblDifficulty.Name = "lblDifficulty";
            lblDifficulty.Size = new Size(72, 20);
            lblDifficulty.TabIndex = 12;
            lblDifficulty.Text = "Difficulty:";
            // 
            // comboDifficulty
            // 
            comboDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboDifficulty.FormattingEnabled = true;
            comboDifficulty.Items.AddRange(new object[] { "Easy", "Normal", "Hard", "Custom" });
            comboDifficulty.Location = new Point(149, 36);
            comboDifficulty.Margin = new Padding(3, 4, 3, 4);
            comboDifficulty.Name = "comboDifficulty";
            comboDifficulty.Size = new Size(171, 28);
            comboDifficulty.TabIndex = 13;
            comboDifficulty.SelectedIndexChanged += comboDifficulty_SelectedIndexChanged;
            // 
            // lblPlayerSpeed
            // 
            lblPlayerSpeed.AutoSize = true;
            lblPlayerSpeed.Location = new Point(34, 93);
            lblPlayerSpeed.Name = "lblPlayerSpeed";
            lblPlayerSpeed.Size = new Size(118, 20);
            lblPlayerSpeed.TabIndex = 14;
            lblPlayerSpeed.Text = "Player Speed: 20";
            // 
            // trackPlayerSpeed
            // 
            trackPlayerSpeed.Location = new Point(149, 89);
            trackPlayerSpeed.Margin = new Padding(3, 4, 3, 4);
            trackPlayerSpeed.Maximum = 50;
            trackPlayerSpeed.Minimum = 5;
            trackPlayerSpeed.Name = "trackPlayerSpeed";
            trackPlayerSpeed.Size = new Size(171, 56);
            trackPlayerSpeed.TabIndex = 15;
            trackPlayerSpeed.Value = 20;
            trackPlayerSpeed.Scroll += trackPlayerSpeed_Scroll;
            // 
            // lblMapType
            // 
            lblMapType.AutoSize = true;
            lblMapType.Location = new Point(34, 147);
            lblMapType.Name = "lblMapType";
            lblMapType.Size = new Size(77, 20);
            lblMapType.TabIndex = 0;
            lblMapType.Text = "Map Type:";
            // 
            // comboMapType
            // 
            comboMapType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMapType.FormattingEnabled = true;
            comboMapType.Items.AddRange(new object[] { "TM1_MainTestScene", "TM2_FlatTestScene", "TM3_SphereTestScene", "TM4_CubeTestScene" });
            comboMapType.Location = new Point(149, 139);
            comboMapType.Margin = new Padding(3, 4, 3, 4);
            comboMapType.Name = "comboMapType";
            comboMapType.Size = new Size(171, 28);
            comboMapType.TabIndex = 1;
            // 
            // lblMapSize
            // 
            lblMapSize.AutoSize = true;
            lblMapSize.Location = new Point(34, 200);
            lblMapSize.Name = "lblMapSize";
            lblMapSize.Size = new Size(93, 20);
            lblMapSize.TabIndex = 2;
            lblMapSize.Text = "Map Size: 35";
            // 
            // trackMapSize
            // 
            trackMapSize.Location = new Point(149, 196);
            trackMapSize.Margin = new Padding(3, 4, 3, 4);
            trackMapSize.Maximum = 50;
            trackMapSize.Minimum = 25;
            trackMapSize.Name = "trackMapSize";
            trackMapSize.Size = new Size(171, 56);
            trackMapSize.TabIndex = 3;
            trackMapSize.Value = 35;
            trackMapSize.Scroll += trackMapSize_Scroll;
            // 
            // lblTimeOfDay
            // 
            lblTimeOfDay.AutoSize = true;
            lblTimeOfDay.Location = new Point(34, 253);
            lblTimeOfDay.Name = "lblTimeOfDay";
            lblTimeOfDay.Size = new Size(93, 20);
            lblTimeOfDay.TabIndex = 10;
            lblTimeOfDay.Text = "Time of Day:";
            // 
            // comboTimeOfDay
            // 
            comboTimeOfDay.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTimeOfDay.FormattingEnabled = true;
            comboTimeOfDay.Items.AddRange(new object[] { "Day", "Night" });
            comboTimeOfDay.Location = new Point(149, 249);
            comboTimeOfDay.Margin = new Padding(3, 4, 3, 4);
            comboTimeOfDay.Name = "comboTimeOfDay";
            comboTimeOfDay.Size = new Size(171, 28);
            comboTimeOfDay.TabIndex = 11;
            // 
            // lblGameMode
            // 
            lblGameMode.AutoSize = true;
            lblGameMode.Location = new Point(34, 307);
            lblGameMode.Name = "lblGameMode";
            lblGameMode.Size = new Size(94, 20);
            lblGameMode.TabIndex = 4;
            lblGameMode.Text = "Game Mode:";
            // 
            // comboGameMode
            // 
            comboGameMode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboGameMode.FormattingEnabled = true;
            comboGameMode.Items.AddRange(new object[] { "Time Limit", "Target Size", "Endurance" });
            comboGameMode.Location = new Point(149, 303);
            comboGameMode.Margin = new Padding(3, 4, 3, 4);
            comboGameMode.Name = "comboGameMode";
            comboGameMode.Size = new Size(171, 28);
            comboGameMode.TabIndex = 5;
            comboGameMode.SelectedIndexChanged += comboGameMode_SelectedIndexChanged;
            // 
            // lblTimeLimit
            // 
            lblTimeLimit.AutoSize = true;
            lblTimeLimit.Location = new Point(34, 360);
            lblTimeLimit.Name = "lblTimeLimit";
            lblTimeLimit.Size = new Size(82, 20);
            lblTimeLimit.TabIndex = 6;
            lblTimeLimit.Text = "Time Limit:";
            // 
            // txtTimeLimit
            // 
            txtTimeLimit.Location = new Point(149, 356);
            txtTimeLimit.Margin = new Padding(3, 4, 3, 4);
            txtTimeLimit.Name = "txtTimeLimit";
            txtTimeLimit.Size = new Size(171, 27);
            txtTimeLimit.TabIndex = 7;
            txtTimeLimit.Text = "120.0";
            // 
            // lblTargetSize
            // 
            lblTargetSize.AutoSize = true;
            lblTargetSize.Location = new Point(34, 360);
            lblTargetSize.Name = "lblTargetSize";
            lblTargetSize.Size = new Size(84, 20);
            lblTargetSize.TabIndex = 8;
            lblTargetSize.Text = "Target Size:";
            // 
            // txtTargetSize
            // 
            txtTargetSize.Location = new Point(149, 356);
            txtTargetSize.Margin = new Padding(3, 4, 3, 4);
            txtTargetSize.Name = "txtTargetSize";
            txtTargetSize.Size = new Size(171, 27);
            txtTargetSize.TabIndex = 9;
            txtTargetSize.Text = "50";
            // 
            // btnLaunchGame
            // 
            btnLaunchGame.Location = new Point(149, 427);
            btnLaunchGame.Margin = new Padding(3, 4, 3, 4);
            btnLaunchGame.Name = "btnLaunchGame";
            btnLaunchGame.Size = new Size(171, 47);
            btnLaunchGame.TabIndex = 16;
            btnLaunchGame.Text = "Launch Game";
            btnLaunchGame.UseVisualStyleBackColor = true;
            btnLaunchGame.Click += btnLaunchGame_Click;
            // 
            // sep1
            // 
            sep1.BorderStyle = BorderStyle.Fixed3D;
            sep1.Location = new Point(34, 133);
            sep1.Name = "sep1";
            sep1.Size = new Size(297, 3);
            sep1.TabIndex = 16;
            // 
            // sep2
            // 
            sep2.BorderStyle = BorderStyle.Fixed3D;
            sep2.Location = new Point(34, 280);
            sep2.Name = "sep2";
            sep2.Size = new Size(297, 3);
            sep2.TabIndex = 17;
            // 
            // sep3
            // 
            sep3.BorderStyle = BorderStyle.Fixed3D;
            sep3.Location = new Point(34, 400);
            sep3.Name = "sep3";
            sep3.Size = new Size(297, 3);
            sep3.TabIndex = 18;
            // 
            // btnBack
            // 
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Location = new Point(12, 8);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 19;
            btnBack.Text = "◀ Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1463, 960);
            Controls.Add(btnBack);
            Controls.Add(sep1);
            Controls.Add(sep2);
            Controls.Add(sep3);
            Controls.Add(btnLaunchGame);
            Controls.Add(trackPlayerSpeed);
            Controls.Add(lblPlayerSpeed);
            Controls.Add(comboDifficulty);
            Controls.Add(lblDifficulty);
            Controls.Add(comboTimeOfDay);
            Controls.Add(lblTimeOfDay);
            Controls.Add(txtTargetSize);
            Controls.Add(lblTargetSize);
            Controls.Add(txtTimeLimit);
            Controls.Add(lblTimeLimit);
            Controls.Add(comboGameMode);
            Controls.Add(lblGameMode);
            Controls.Add(trackMapSize);
            Controls.Add(lblMapSize);
            Controls.Add(comboMapType);
            Controls.Add(lblMapType);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "PolySnake Launcher";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackPlayerSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackMapSize).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Label lblMapType;
        private System.Windows.Forms.ComboBox comboMapType;
        private System.Windows.Forms.Label lblMapSize;
        private System.Windows.Forms.TrackBar trackMapSize;
        private System.Windows.Forms.Label lblGameMode;
        private System.Windows.Forms.ComboBox comboGameMode;
        private System.Windows.Forms.Label lblTimeLimit;
        private System.Windows.Forms.TextBox txtTimeLimit;
        private System.Windows.Forms.Label lblTargetSize;
        private System.Windows.Forms.TextBox txtTargetSize;
        private System.Windows.Forms.Label lblTimeOfDay;
        private System.Windows.Forms.ComboBox comboTimeOfDay;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.ComboBox comboDifficulty;
        private System.Windows.Forms.Label lblPlayerSpeed;
        private System.Windows.Forms.TrackBar trackPlayerSpeed;

        private System.Windows.Forms.Button btnLaunchGame;
        private Button btnBack;
    }
}
