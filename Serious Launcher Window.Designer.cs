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
            this.SetupButton = new System.Windows.Forms.Button();
            this.SetupProgressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.RunButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SetupButton
            // 
            this.SetupButton.Enabled = false;
            this.SetupButton.Location = new System.Drawing.Point(722, 448);
            this.SetupButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SetupButton.Name = "SetupButton";
            this.SetupButton.Size = new System.Drawing.Size(140, 22);
            this.SetupButton.TabIndex = 0;
            this.SetupButton.Text = "SETUP";
            this.SetupButton.UseVisualStyleBackColor = true;
            this.SetupButton.Click += new System.EventHandler(this.SetupButton_Click);
            // 
            // SetupProgressBar
            // 
            this.SetupProgressBar.Location = new System.Drawing.Point(0, 386);
            this.SetupProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SetupProgressBar.Minimum = 1;
            this.SetupProgressBar.Name = "SetupProgressBar";
            this.SetupProgressBar.Size = new System.Drawing.Size(872, 28);
            this.SetupProgressBar.TabIndex = 2;
            this.SetupProgressBar.Value = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ImageLocation = "https://storage.yandexcloud.net/serious-trouble-resources/SeriosLauncherBackgroun" +
    "d.jpg";
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(872, 388);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // StatusStrip
            // 
            this.StatusStrip.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.StatusStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 481);
            this.StatusStrip.MinimumSize = new System.Drawing.Size(0, 24);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.StatusStrip.Size = new System.Drawing.Size(872, 24);
            this.StatusStrip.TabIndex = 4;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.StatusStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusStripLabel.Margin = new System.Windows.Forms.Padding(0);
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(52, 24);
            this.StatusStripLabel.Text = "Status";
            // 
            // RunButton
            // 
            this.RunButton.Enabled = false;
            this.RunButton.Location = new System.Drawing.Point(10, 448);
            this.RunButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(140, 22);
            this.RunButton.TabIndex = 5;
            this.RunButton.Text = "RUN";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // SeriousLauncherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 505);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SetupProgressBar);
            this.Controls.Add(this.SetupButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SeriousLauncherWindow";
            this.Text = "Serious Trouble Launcher";
            this.Shown += new System.EventHandler(this.SeriousLauncherWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SetupButton;
        private ProgressBar SetupProgressBar;
        private PictureBox pictureBox1;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel StatusStripLabel;
        private Button RunButton;
    }
}