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
            SetupButton = new Button();
            SetupProgressBar = new ProgressBar();
            pictureBox1 = new PictureBox();
            StatusStrip = new StatusStrip();
            StatusStripLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            StatusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // SetupButton
            // 
            SetupButton.Location = new Point(891, 597);
            SetupButton.Name = "SetupButton";
            SetupButton.Size = new Size(94, 29);
            SetupButton.TabIndex = 0;
            SetupButton.Text = "Setup";
            SetupButton.UseVisualStyleBackColor = true;
            SetupButton.Click += SetupButton_Click;
            // 
            // SetupProgressBar
            // 
            SetupProgressBar.Location = new Point(12, 549);
            SetupProgressBar.Minimum = 1;
            SetupProgressBar.Name = "SetupProgressBar";
            SetupProgressBar.Size = new Size(973, 29);
            SetupProgressBar.TabIndex = 2;
            SetupProgressBar.Value = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.ImageLocation = "https://storage.yandexcloud.net/serious-trouble-resources/SeriosLauncherBackground.jpg";
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(997, 518);
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
            // SeriousLauncherWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(997, 673);
            Controls.Add(StatusStrip);
            Controls.Add(pictureBox1);
            Controls.Add(SetupProgressBar);
            Controls.Add(SetupButton);
            Name = "SeriousLauncherWindow";
            Text = "Serious Trouble Launcher";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SetupButton;
        private ProgressBar SetupProgressBar;
        private PictureBox pictureBox1;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel StatusStripLabel;
    }
}