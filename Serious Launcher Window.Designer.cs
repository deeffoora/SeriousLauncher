﻿namespace SeriousLauncher {
    partial class SeriousLauncherWindow {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            InstallButton = new Button();
            SetupProgressBar = new ProgressBar();
            pictureBox1 = new PictureBox();
            StatusStrip = new StatusStrip();
            StatusStripLabel = new ToolStripStatusLabel();
            RunButton = new Button();
            FolderBrowserDialog = new FolderBrowserDialog();
            ErrorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // SetupButton
            // 
            InstallButton.Enabled = false;
            InstallButton.Location = new Point(825, 597);
            InstallButton.Name = "SetupButton";
            InstallButton.Size = new Size(160, 29);
            InstallButton.TabIndex = 0;
            InstallButton.Text = "INSTALL";
            InstallButton.UseVisualStyleBackColor = true;
            InstallButton.Click += InstallButton_Click;
            // 
            // SetupProgressBar
            // 
            SetupProgressBar.Location = new Point(0, 515);
            SetupProgressBar.Minimum = 1;
            SetupProgressBar.Name = "SetupProgressBar";
            SetupProgressBar.Size = new Size(997, 37);
            SetupProgressBar.TabIndex = 2;
            SetupProgressBar.Value = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.ImageLocation = "https://storage.yandexcloud.net/serious-trouble-resources/SeriosLauncherBackground.jpg";
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(997, 517);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // StatusStrip
            // 
            StatusStrip.BackColor = SystemColors.GradientInactiveCaption;
            StatusStrip.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            StatusStrip.ImageScalingSize = new Size(20, 20);
            StatusStrip.Items.AddRange(new ToolStripItem[] { StatusStripLabel });
            StatusStrip.Location = new Point(0, 641);
            StatusStrip.MinimumSize = new Size(0, 32);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(997, 32);
            StatusStrip.TabIndex = 4;
            StatusStrip.Text = "statusStrip1";
            // 
            // StatusStripLabel
            // 
            StatusStripLabel.BackColor = SystemColors.GradientInactiveCaption;
            StatusStripLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            StatusStripLabel.Margin = new Padding(0);
            StatusStripLabel.Name = "StatusStripLabel";
            StatusStripLabel.Size = new Size(65, 32);
            StatusStripLabel.Text = "Status";
            // 
            // RunButton
            // 
            RunButton.Enabled = false;
            RunButton.Location = new Point(11, 597);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(160, 29);
            RunButton.TabIndex = 5;
            RunButton.Text = "RUN";
            RunButton.UseVisualStyleBackColor = true;
            RunButton.Click += RunButton_Click;
            // 
            // FolderBrowserDialog
            // 
            FolderBrowserDialog.Description = "Select path where the folder 'Serious Trouble' will be created";
            FolderBrowserDialog.RootFolder = Environment.SpecialFolder.ProgramFiles;
            FolderBrowserDialog.UseDescriptionForTitle = true;
            // 
            // ErrorLabel
            // 
            ErrorLabel.AutoSize = true;
            ErrorLabel.BackColor = Color.Red;
            ErrorLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ErrorLabel.ForeColor = Color.White;
            ErrorLabel.Location = new Point(0, 555);
            ErrorLabel.Margin = new Padding(0);
            ErrorLabel.Name = "ErrorLabel";
            ErrorLabel.Size = new Size(0, 23);
            ErrorLabel.TabIndex = 6;
            // 
            // SeriousLauncherWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 673);
            Controls.Add(ErrorLabel);
            Controls.Add(RunButton);
            Controls.Add(StatusStrip);
            Controls.Add(pictureBox1);
            Controls.Add(SetupProgressBar);
            Controls.Add(InstallButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "SeriousLauncherWindow";
            Text = "Serious Trouble Launcher";
            Shown += SeriousLauncherWindow_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button InstallButton;
        private ProgressBar SetupProgressBar;
        private PictureBox pictureBox1;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel StatusStripLabel;
        private Button RunButton;
        private FolderBrowserDialog FolderBrowserDialog;
        private Label ErrorLabel;
    }
}