
namespace Follow
{
    partial class StartForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
		private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm1));
            progressBarControl = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            labelCopyright = new DevExpress.XtraEditors.LabelControl();
            labelStatus = new DevExpress.XtraEditors.LabelControl();
            peImage = new DevExpress.XtraEditors.PictureEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).BeginInit();
            SuspendLayout();
            // 
            // progressBarControl
            // 
            progressBarControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            progressBarControl.EditValue = 0;
            progressBarControl.Location = new System.Drawing.Point(17, 324);
            progressBarControl.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            progressBarControl.Name = "progressBarControl";
            progressBarControl.Properties.ProgressAnimationMode = DevExpress.Utils.Drawing.ProgressAnimationMode.Cycle;
            progressBarControl.Size = new System.Drawing.Size(606, 28);
            progressBarControl.TabIndex = 5;
            // 
            // labelCopyright
            // 
            labelCopyright.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            labelCopyright.Appearance.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            labelCopyright.Appearance.Options.UseFont = true;
            labelCopyright.Appearance.Options.UseForeColor = true;
            labelCopyright.Appearance.Options.UseTextOptions = true;
            labelCopyright.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            labelCopyright.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            labelCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelCopyright.Location = new System.Drawing.Point(336, 356);
            labelCopyright.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new System.Drawing.Size(287, 32);
            labelCopyright.TabIndex = 6;
            labelCopyright.Text = "Copyright";
            // 
            // labelStatus
            // 
            labelStatus.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            labelStatus.Appearance.Options.UseFont = true;
            labelStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            labelStatus.Location = new System.Drawing.Point(17, 269);
            labelStatus.Margin = new System.Windows.Forms.Padding(4, 7, 4, 2);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new System.Drawing.Size(606, 30);
            labelStatus.TabIndex = 7;
            labelStatus.Text = "البداية ....";
            // 
            // peImage
            // 
            peImage.Dock = System.Windows.Forms.DockStyle.Top;
            peImage.EditValue = resources.GetObject("peImage.EditValue");
            peImage.Location = new System.Drawing.Point(1, 2);
            peImage.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            peImage.Name = "peImage";
            peImage.Properties.AllowFocused = false;
            peImage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            peImage.Properties.Appearance.Options.UseBackColor = true;
            peImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            peImage.Properties.ShowMenu = false;
            peImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            peImage.Properties.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            peImage.Size = new System.Drawing.Size(659, 253);
            peImage.TabIndex = 9;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Location = new System.Drawing.Point(1, 377);
            labelControl1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 2);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(117, 25);
            labelControl1.TabIndex = 10;
            labelControl1.Text = "برنامج المتابعة ";
            // 
            // StartForm1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(661, 417);
            Controls.Add(labelControl1);
            Controls.Add(peImage);
            Controls.Add(labelStatus);
            Controls.Add(labelCopyright);
            Controls.Add(progressBarControl);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            Name = "StartForm1";
            Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "StartForm1";
            Load += StartForm1_Load;
            ((System.ComponentModel.ISupportInitialize)progressBarControl.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)peImage.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBarControl;
        private DevExpress.XtraEditors.LabelControl labelCopyright;
        private DevExpress.XtraEditors.LabelControl labelStatus;
        private DevExpress.XtraEditors.PictureEdit peImage;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
