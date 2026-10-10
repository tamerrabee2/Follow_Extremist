namespace Follow_Extremist.Gui.GuiFingerprint
{
    partial class FingerprintDeviceForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.buttonClearLogs = new System.Windows.Forms.Button();
            this.buttonSyncTime = new System.Windows.Forms.Button();
            this.buttonTestConnect = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxMachineNum = new System.Windows.Forms.TextBox();
            this.labelMachineNum = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.labelIp = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(560, 58);
            this.panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(560, 58);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "⚙️ إعدادات جهاز البصمة (ZKTeco)";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFooter.Controls.Add(this.buttonSave);
            this.panelFooter.Controls.Add(this.buttonCancel);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 482);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelFooter.Size = new System.Drawing.Size(560, 58);
            this.panelFooter.TabIndex = 1;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(384, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(160, 34);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "💾 حفظ الإعدادات";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(16, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "إلغاء";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.buttonClearLogs);
            this.panelContent.Controls.Add(this.buttonSyncTime);
            this.panelContent.Controls.Add(this.buttonTestConnect);
            this.panelContent.Controls.Add(this.labelStatus);
            this.panelContent.Controls.Add(this.groupBoxSettings);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 58);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.panelContent.Size = new System.Drawing.Size(560, 424);
            this.panelContent.TabIndex = 2;
            // 
            // buttonClearLogs
            // 
            this.buttonClearLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.buttonClearLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonClearLogs.FlatAppearance.BorderSize = 0;
            this.buttonClearLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClearLogs.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonClearLogs.ForeColor = System.Drawing.Color.White;
            this.buttonClearLogs.Location = new System.Drawing.Point(20, 368);
            this.buttonClearLogs.Name = "buttonClearLogs";
            this.buttonClearLogs.Size = new System.Drawing.Size(160, 36);
            this.buttonClearLogs.TabIndex = 4;
            this.buttonClearLogs.Text = "🗑️ مسح سجلات الماكينة";
            this.buttonClearLogs.UseVisualStyleBackColor = false;
            this.buttonClearLogs.Click += new System.EventHandler(this.buttonClearLogs_Click);
            // 
            // buttonSyncTime
            // 
            this.buttonSyncTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.buttonSyncTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSyncTime.FlatAppearance.BorderSize = 0;
            this.buttonSyncTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSyncTime.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonSyncTime.ForeColor = System.Drawing.Color.White;
            this.buttonSyncTime.Location = new System.Drawing.Point(190, 368);
            this.buttonSyncTime.Name = "buttonSyncTime";
            this.buttonSyncTime.Size = new System.Drawing.Size(170, 36);
            this.buttonSyncTime.TabIndex = 3;
            this.buttonSyncTime.Text = "⏱️ مزامنة وقت الجهاز";
            this.buttonSyncTime.UseVisualStyleBackColor = false;
            this.buttonSyncTime.Click += new System.EventHandler(this.buttonSyncTime_Click);
            // 
            // buttonTestConnect
            // 
            this.buttonTestConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonTestConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTestConnect.FlatAppearance.BorderSize = 0;
            this.buttonTestConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTestConnect.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonTestConnect.ForeColor = System.Drawing.Color.White;
            this.buttonTestConnect.Location = new System.Drawing.Point(360, 368);
            this.buttonTestConnect.Name = "buttonTestConnect";
            this.buttonTestConnect.Size = new System.Drawing.Size(180, 36);
            this.buttonTestConnect.TabIndex = 2;
            this.buttonTestConnect.Text = "⚡ اختبار الاتصال الآن";
            this.buttonTestConnect.UseVisualStyleBackColor = false;
            this.buttonTestConnect.Click += new System.EventHandler(this.buttonTestConnect_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.labelStatus.Location = new System.Drawing.Point(20, 316);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(520, 36);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "حالة الاتصال: جاهز";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.BackColor = System.Drawing.Color.White;
            this.groupBoxSettings.Controls.Add(this.textBoxLocation);
            this.groupBoxSettings.Controls.Add(this.labelLocation);
            this.groupBoxSettings.Controls.Add(this.textBoxPassword);
            this.groupBoxSettings.Controls.Add(this.labelPassword);
            this.groupBoxSettings.Controls.Add(this.textBoxMachineNum);
            this.groupBoxSettings.Controls.Add(this.labelMachineNum);
            this.groupBoxSettings.Controls.Add(this.textBoxPort);
            this.groupBoxSettings.Controls.Add(this.labelPort);
            this.groupBoxSettings.Controls.Add(this.textBoxIp);
            this.groupBoxSettings.Controls.Add(this.labelIp);
            this.groupBoxSettings.Controls.Add(this.textBoxName);
            this.groupBoxSettings.Controls.Add(this.labelName);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSettings.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.groupBoxSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.groupBoxSettings.Location = new System.Drawing.Point(20, 16);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.groupBoxSettings.Size = new System.Drawing.Size(520, 300);
            this.groupBoxSettings.TabIndex = 0;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "بيانات ومعلمات الاتصال بالشبكة";
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxLocation.Location = new System.Drawing.Point(20, 245);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(350, 25);
            this.textBoxLocation.TabIndex = 11;
            this.textBoxLocation.Text = "بوابة المتابعة الرئيسية";
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelLocation.Location = new System.Drawing.Point(395, 248);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(78, 17);
            this.labelLocation.TabIndex = 10;
            this.labelLocation.Text = "موقع الجهاز:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxPassword.Location = new System.Drawing.Point(20, 202);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '●';
            this.textBoxPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPassword.Size = new System.Drawing.Size(350, 25);
            this.textBoxPassword.TabIndex = 9;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelPassword.Location = new System.Drawing.Point(395, 205);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(89, 17);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "كلمة سر الاتصال:";
            // 
            // textBoxMachineNum
            // 
            this.textBoxMachineNum.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxMachineNum.Location = new System.Drawing.Point(20, 160);
            this.textBoxMachineNum.Name = "textBoxMachineNum";
            this.textBoxMachineNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxMachineNum.Size = new System.Drawing.Size(100, 25);
            this.textBoxMachineNum.TabIndex = 7;
            this.textBoxMachineNum.Text = "1";
            // 
            // labelMachineNum
            // 
            this.labelMachineNum.AutoSize = true;
            this.labelMachineNum.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelMachineNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelMachineNum.Location = new System.Drawing.Point(125, 163);
            this.labelMachineNum.Name = "labelMachineNum";
            this.labelMachineNum.Size = new System.Drawing.Size(74, 17);
            this.labelMachineNum.TabIndex = 6;
            this.labelMachineNum.Text = "رقم الماكينة:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxPort.Location = new System.Drawing.Point(230, 160);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPort.Size = new System.Drawing.Size(140, 25);
            this.textBoxPort.TabIndex = 5;
            this.textBoxPort.Text = "4370";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelPort.Location = new System.Drawing.Point(395, 163);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(73, 17);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "منفذ (Port):";
            // 
            // textBoxIp
            // 
            this.textBoxIp.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxIp.Location = new System.Drawing.Point(20, 115);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxIp.Size = new System.Drawing.Size(350, 25);
            this.textBoxIp.TabIndex = 3;
            this.textBoxIp.Text = "192.168.1.201";
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelIp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelIp.Location = new System.Drawing.Point(395, 118);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(89, 17);
            this.labelIp.TabIndex = 2;
            this.labelIp.Text = "عنوان الـ IP:";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxName.Location = new System.Drawing.Point(20, 70);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(350, 25);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.Text = "جهاز البصمة الرئيسي";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.labelName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelName.Location = new System.Drawing.Point(395, 73);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(73, 17);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "اسم الجهاز:";
            // 
            // FingerprintDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(560, 540);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FingerprintDeviceForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إعدادات جهاز البصمة";
            this.Load += new System.EventHandler(this.FingerprintDeviceForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxMachineNum;
        private System.Windows.Forms.Label labelMachineNum;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonTestConnect;
        private System.Windows.Forms.Button buttonSyncTime;
        private System.Windows.Forms.Button buttonClearLogs;
    }
}
