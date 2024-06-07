namespace SeriousLauncher {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriousLauncherWindow));
            UpdateButton = new Button();
            SetupProgressBar = new ProgressBar();
            pictureBox1 = new PictureBox();
            StatusStrip = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            RunButton = new Button();
            FolderBrowserDialog = new FolderBrowserDialog();
            ErrorLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // UpdateButton
            // 
            UpdateButton.Enabled = false;
            UpdateButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            UpdateButton.ForeColor = Color.RoyalBlue;
            UpdateButton.Location = new Point(825, 597);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(160, 29);
            UpdateButton.TabIndex = 0;
            UpdateButton.Text = "UPDATE";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += InstallButton_Click;
            // 
            // SetupProgressBar
            // 
            SetupProgressBar.Location = new Point(0, 515);
            SetupProgressBar.Name = "SetupProgressBar";
            SetupProgressBar.Size = new Size(997, 37);
            SetupProgressBar.TabIndex = 2;
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
            StatusStrip.Items.AddRange(new ToolStripItem[] { StatusLabel });
            StatusStrip.Location = new Point(0, 641);
            StatusStrip.MinimumSize = new Size(0, 32);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(997, 32);
            StatusStrip.TabIndex = 4;
            StatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            StatusLabel.BackColor = SystemColors.GradientInactiveCaption;
            StatusLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            StatusLabel.Margin = new Padding(0);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(65, 32);
            StatusLabel.Text = "Status";
            // 
            // RunButton
            // 
            RunButton.Enabled = false;
            RunButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            RunButton.ForeColor = Color.RoyalBlue;
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
            Controls.Add(UpdateButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
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

        private Button UpdateButton;
        private ProgressBar SetupProgressBar;
        private PictureBox pictureBox1;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel StatusLabel;
        private Button RunButton;
        private FolderBrowserDialog FolderBrowserDialog;
        private Label ErrorLabel;
    }
}