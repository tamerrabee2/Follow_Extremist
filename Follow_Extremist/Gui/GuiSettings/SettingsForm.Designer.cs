
namespace Follow_Extremist.Gui.GuiSettings
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSaveGeneral = new System.Windows.Forms.Button();
            this.linkLabelImportImage = new System.Windows.Forms.LinkLabel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.numericUpDownDataRow = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNotification = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCompany = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonSaveConnectionString = new System.Windows.Forms.Button();
            this.radioButtonNetworkConn = new System.Windows.Forms.RadioButton();
            this.radioButtonLocalCon = new System.Windows.Forms.RadioButton();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.numericUpDownTimeOut = new System.Windows.Forms.NumericUpDown();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonBackUp = new System.Windows.Forms.Button();
            this.groupBoxAutoBackup = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoBackup = new System.Windows.Forms.CheckBox();
            this.labelAutoBackupUser = new System.Windows.Forms.Label();
            this.comboBoxAutoBackupUser = new System.Windows.Forms.ComboBox();
            this.labelAutoBackupPath = new System.Windows.Forms.Label();
            this.textBoxAutoBackupPath = new System.Windows.Forms.TextBox();
            this.buttonBrowseAutoBackup = new System.Windows.Forms.Button();
            this.labelLastBackupStatus = new System.Windows.Forms.Label();
            this.labelAutoBackupNote = new System.Windows.Forms.Label();
            this.buttonSaveAutoBackup = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.colorPickEditoutfollow = new DevExpress.XtraEditors.ColorPickEdit();
            this.colorPickEditprison = new DevExpress.XtraEditors.ColorPickEdit();
            this.colorPickEditbreakfollow = new DevExpress.XtraEditors.ColorPickEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotification)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeOut)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBoxAutoBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditoutfollow.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditprison.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditbreakfollow.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colorPickEditbreakfollow);
            this.groupBox1.Controls.Add(this.colorPickEditprison);
            this.groupBox1.Controls.Add(this.colorPickEditoutfollow);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.buttonSaveGeneral);
            this.groupBox1.Controls.Add(this.linkLabelImportImage);
            this.groupBox1.Controls.Add(this.pictureBoxLogo);
            this.groupBox1.Controls.Add(this.numericUpDownDataRow);
            this.groupBox1.Controls.Add(this.numericUpDownNotification);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxCompany);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 533);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "الاعدادات العامة";
            // 
            // buttonSaveGeneral
            // 
            this.buttonSaveGeneral.Image = global::Follow_Extremist.Properties.Resources.Save;
            this.buttonSaveGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveGeneral.Location = new System.Drawing.Point(8, 464);
            this.buttonSaveGeneral.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSaveGeneral.Name = "buttonSaveGeneral";
            this.buttonSaveGeneral.Size = new System.Drawing.Size(354, 55);
            this.buttonSaveGeneral.TabIndex = 5;
            this.buttonSaveGeneral.Text = "حفظ ";
            this.buttonSaveGeneral.UseVisualStyleBackColor = true;
            this.buttonSaveGeneral.Click += new System.EventHandler(this.buttonSaveGeneral_Click);
            // 
            // linkLabelImportImage
            // 
            this.linkLabelImportImage.AutoSize = true;
            this.linkLabelImportImage.Location = new System.Drawing.Point(120, 428);
            this.linkLabelImportImage.Name = "linkLabelImportImage";
            this.linkLabelImportImage.Size = new System.Drawing.Size(47, 26);
            this.linkLabelImportImage.TabIndex = 4;
            this.linkLabelImportImage.TabStop = true;
            this.linkLabelImportImage.Text = "تحميل";
            this.linkLabelImportImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelImportImage_LinkClicked);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Location = new System.Drawing.Point(50, 315);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(195, 105);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // numericUpDownDataRow
            // 
            this.numericUpDownDataRow.Location = new System.Drawing.Point(8, 123);
            this.numericUpDownDataRow.Name = "numericUpDownDataRow";
            this.numericUpDownDataRow.Size = new System.Drawing.Size(190, 32);
            this.numericUpDownDataRow.TabIndex = 2;
            this.numericUpDownDataRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownDataRow.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numericUpDownNotification
            // 
            this.numericUpDownNotification.Location = new System.Drawing.Point(8, 77);
            this.numericUpDownNotification.Name = "numericUpDownNotification";
            this.numericUpDownNotification.Size = new System.Drawing.Size(155, 32);
            this.numericUpDownNotification.TabIndex = 2;
            this.numericUpDownNotification.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownNotification.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(255, 345);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "شعار المؤسسة ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(205, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "عدد البيانات المعروضة ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(170, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "فترة عرض الاشعارات (ثواني) ";
            // 
            // textBoxCompany
            // 
            this.textBoxCompany.Location = new System.Drawing.Point(8, 29);
            this.textBoxCompany.Name = "textBoxCompany";
            this.textBoxCompany.Size = new System.Drawing.Size(235, 32);
            this.textBoxCompany.TabIndex = 1;
            this.textBoxCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(250, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "اسم المؤسسة ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(394, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 533);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "قواعد البيانات";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonSaveConnectionString);
            this.groupBox4.Controls.Add(this.radioButtonNetworkConn);
            this.groupBox4.Controls.Add(this.radioButtonLocalCon);
            this.groupBox4.Controls.Add(this.textBoxPassword);
            this.groupBox4.Controls.Add(this.textBoxUserName);
            this.groupBox4.Controls.Add(this.numericUpDownTimeOut);
            this.groupBox4.Controls.Add(this.textBoxDatabase);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.textBoxServer);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(8, 28);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(364, 497);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "نص الاتصال";
            // 
            // buttonSaveConnectionString
            // 
            this.buttonSaveConnectionString.Image = global::Follow_Extremist.Properties.Resources.Save;
            this.buttonSaveConnectionString.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveConnectionString.Location = new System.Drawing.Point(8, 436);
            this.buttonSaveConnectionString.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSaveConnectionString.Name = "buttonSaveConnectionString";
            this.buttonSaveConnectionString.Size = new System.Drawing.Size(348, 55);
            this.buttonSaveConnectionString.TabIndex = 6;
            this.buttonSaveConnectionString.Text = "حفظ الاتصال بالسيرفر";
            this.buttonSaveConnectionString.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonSaveConnectionString.UseVisualStyleBackColor = true;
            this.buttonSaveConnectionString.Click += new System.EventHandler(this.buttonSaveConnectionString_Click);
            // 
            // radioButtonNetworkConn
            // 
            this.radioButtonNetworkConn.AutoSize = true;
            this.radioButtonNetworkConn.Location = new System.Drawing.Point(8, 28);
            this.radioButtonNetworkConn.Name = "radioButtonNetworkConn";
            this.radioButtonNetworkConn.Size = new System.Drawing.Size(67, 30);
            this.radioButtonNetworkConn.TabIndex = 0;
            this.radioButtonNetworkConn.Text = "شبكي";
            this.radioButtonNetworkConn.UseVisualStyleBackColor = true;
            this.radioButtonNetworkConn.CheckedChanged += new System.EventHandler(this.radioButtonNetworkConn_CheckedChanged);
            // 
            // radioButtonLocalCon
            // 
            this.radioButtonLocalCon.AutoSize = true;
            this.radioButtonLocalCon.Checked = true;
            this.radioButtonLocalCon.Location = new System.Drawing.Point(80, 28);
            this.radioButtonLocalCon.Name = "radioButtonLocalCon";
            this.radioButtonLocalCon.Size = new System.Drawing.Size(62, 30);
            this.radioButtonLocalCon.TabIndex = 0;
            this.radioButtonLocalCon.TabStop = true;
            this.radioButtonLocalCon.Text = "محلي";
            this.radioButtonLocalCon.UseVisualStyleBackColor = true;
            this.radioButtonLocalCon.CheckedChanged += new System.EventHandler(this.radioButtonLocalCon_CheckedChanged);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Enabled = false;
            this.textBoxPassword.Location = new System.Drawing.Point(8, 287);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(245, 32);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Enabled = false;
            this.textBoxUserName.Location = new System.Drawing.Point(8, 232);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(230, 32);
            this.textBoxUserName.TabIndex = 1;
            this.textBoxUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericUpDownTimeOut
            // 
            this.numericUpDownTimeOut.Enabled = false;
            this.numericUpDownTimeOut.Location = new System.Drawing.Point(8, 177);
            this.numericUpDownTimeOut.Name = "numericUpDownTimeOut";
            this.numericUpDownTimeOut.Size = new System.Drawing.Size(210, 32);
            this.numericUpDownTimeOut.TabIndex = 2;
            this.numericUpDownTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownTimeOut.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(8, 122);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxDatabase.Size = new System.Drawing.Size(230, 32);
            this.textBoxDatabase.TabIndex = 1;
            this.textBoxDatabase.Text = "AsrflyDataBase";
            this.textBoxDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(260, 290);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 26);
            this.label10.TabIndex = 0;
            this.label10.Text = "كلمة المرور";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(245, 235);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 26);
            this.label9.TabIndex = 0;
            this.label9.Text = "اسم المستخدم";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(225, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 26);
            this.label8.TabIndex = 0;
            this.label8.Text = "فترة الاتصال (ثانية)";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(8, 67);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxServer.Size = new System.Drawing.Size(255, 32);
            this.textBoxServer.TabIndex = 1;
            this.textBoxServer.Text = ".\\SQLEXPRESS";
            this.textBoxServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(245, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 26);
            this.label6.TabIndex = 0;
            this.label6.Text = "قاعدة البيانات";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(270, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = "السيرفر";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBoxAutoBackup);
            this.groupBox3.Controls.Add(this.buttonRestore);
            this.groupBox3.Controls.Add(this.buttonBackUp);
            this.groupBox3.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(786, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 533);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "النسخ الاحتياطي واستعادة البيانات";
            // 
            // buttonRestore
            // 
            this.buttonRestore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonRestore.Image = global::Follow_Extremist.Properties.Resources.Save;
            this.buttonRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRestore.Location = new System.Drawing.Point(8, 28);
            this.buttonRestore.Margin = new System.Windows.Forms.Padding(5);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(178, 52);
            this.buttonRestore.TabIndex = 7;
            this.buttonRestore.Text = "استعادة نسخة";
            this.buttonRestore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonBackUp
            // 
            this.buttonBackUp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonBackUp.Image = global::Follow_Extremist.Properties.Resources.Save;
            this.buttonBackUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBackUp.Location = new System.Drawing.Point(194, 28);
            this.buttonBackUp.Margin = new System.Windows.Forms.Padding(5);
            this.buttonBackUp.Name = "buttonBackUp";
            this.buttonBackUp.Size = new System.Drawing.Size(178, 52);
            this.buttonBackUp.TabIndex = 6;
            this.buttonBackUp.Text = "نسخ فوري";
            this.buttonBackUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonBackUp.UseVisualStyleBackColor = true;
            this.buttonBackUp.Click += new System.EventHandler(this.buttonBackUp_Click);
            // 
            // groupBoxAutoBackup
            // 
            this.groupBoxAutoBackup.Controls.Add(this.buttonSaveAutoBackup);
            this.groupBoxAutoBackup.Controls.Add(this.labelAutoBackupNote);
            this.groupBoxAutoBackup.Controls.Add(this.labelLastBackupStatus);
            this.groupBoxAutoBackup.Controls.Add(this.buttonBrowseAutoBackup);
            this.groupBoxAutoBackup.Controls.Add(this.textBoxAutoBackupPath);
            this.groupBoxAutoBackup.Controls.Add(this.labelAutoBackupPath);
            this.groupBoxAutoBackup.Controls.Add(this.comboBoxAutoBackupUser);
            this.groupBoxAutoBackup.Controls.Add(this.labelAutoBackupUser);
            this.groupBoxAutoBackup.Controls.Add(this.checkBoxAutoBackup);
            this.groupBoxAutoBackup.Location = new System.Drawing.Point(8, 88);
            this.groupBoxAutoBackup.Name = "groupBoxAutoBackup";
            this.groupBoxAutoBackup.Size = new System.Drawing.Size(366, 437);
            this.groupBoxAutoBackup.TabIndex = 8;
            this.groupBoxAutoBackup.TabStop = false;
            this.groupBoxAutoBackup.Text = "النسخ الاحتياطي التلقائي اليومي";
            // 
            // checkBoxAutoBackup
            // 
            this.checkBoxAutoBackup.AutoSize = true;
            this.checkBoxAutoBackup.Font = new System.Drawing.Font("Cairo", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxAutoBackup.Location = new System.Drawing.Point(12, 28);
            this.checkBoxAutoBackup.Name = "checkBoxAutoBackup";
            this.checkBoxAutoBackup.Size = new System.Drawing.Size(262, 30);
            this.checkBoxAutoBackup.TabIndex = 0;
            this.checkBoxAutoBackup.Text = "تفعيل النسخ الاحتياطي التلقائي اليومي";
            this.checkBoxAutoBackup.UseVisualStyleBackColor = true;
            // 
            // labelAutoBackupUser
            // 
            this.labelAutoBackupUser.AutoSize = true;
            this.labelAutoBackupUser.Location = new System.Drawing.Point(12, 65);
            this.labelAutoBackupUser.Name = "labelAutoBackupUser";
            this.labelAutoBackupUser.Size = new System.Drawing.Size(185, 26);
            this.labelAutoBackupUser.TabIndex = 1;
            this.labelAutoBackupUser.Text = "المستخدم المستهدف للنسخ:";
            // 
            // comboBoxAutoBackupUser
            // 
            this.comboBoxAutoBackupUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAutoBackupUser.FormattingEnabled = true;
            this.comboBoxAutoBackupUser.Location = new System.Drawing.Point(12, 93);
            this.comboBoxAutoBackupUser.Name = "comboBoxAutoBackupUser";
            this.comboBoxAutoBackupUser.Size = new System.Drawing.Size(342, 32);
            this.comboBoxAutoBackupUser.TabIndex = 2;
            // 
            // labelAutoBackupPath
            // 
            this.labelAutoBackupPath.AutoSize = true;
            this.labelAutoBackupPath.Location = new System.Drawing.Point(12, 136);
            this.labelAutoBackupPath.Name = "labelAutoBackupPath";
            this.labelAutoBackupPath.Size = new System.Drawing.Size(193, 26);
            this.labelAutoBackupPath.TabIndex = 3;
            this.labelAutoBackupPath.Text = "مسار حفظ النسخة الاحتياطية:";
            // 
            // textBoxAutoBackupPath
            // 
            this.textBoxAutoBackupPath.Font = new System.Drawing.Font("Cairo", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAutoBackupPath.Location = new System.Drawing.Point(12, 164);
            this.textBoxAutoBackupPath.Name = "textBoxAutoBackupPath";
            this.textBoxAutoBackupPath.Size = new System.Drawing.Size(288, 30);
            this.textBoxAutoBackupPath.TabIndex = 4;
            // 
            // buttonBrowseAutoBackup
            // 
            this.buttonBrowseAutoBackup.Location = new System.Drawing.Point(306, 163);
            this.buttonBrowseAutoBackup.Name = "buttonBrowseAutoBackup";
            this.buttonBrowseAutoBackup.Size = new System.Drawing.Size(48, 33);
            this.buttonBrowseAutoBackup.TabIndex = 5;
            this.buttonBrowseAutoBackup.Text = "...";
            this.buttonBrowseAutoBackup.UseVisualStyleBackColor = true;
            this.buttonBrowseAutoBackup.Click += new System.EventHandler(this.buttonBrowseAutoBackup_Click);
            // 
            // labelLastBackupStatus
            // 
            this.labelLastBackupStatus.Font = new System.Drawing.Font("Cairo", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelLastBackupStatus.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelLastBackupStatus.Location = new System.Drawing.Point(12, 206);
            this.labelLastBackupStatus.Name = "labelLastBackupStatus";
            this.labelLastBackupStatus.Size = new System.Drawing.Size(342, 30);
            this.labelLastBackupStatus.TabIndex = 6;
            this.labelLastBackupStatus.Text = "آخر نسخة تم حفظها: لا يوجد بعد";
            // 
            // labelAutoBackupNote
            // 
            this.labelAutoBackupNote.Font = new System.Drawing.Font("Cairo", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelAutoBackupNote.ForeColor = System.Drawing.Color.DimGray;
            this.labelAutoBackupNote.Location = new System.Drawing.Point(12, 240);
            this.labelAutoBackupNote.Name = "labelAutoBackupNote";
            this.labelAutoBackupNote.Size = new System.Drawing.Size(342, 100);
            this.labelAutoBackupNote.TabIndex = 7;
            this.labelAutoBackupNote.Text = "• يتم أخذ نسخة واحدة تلقائياً كل يوم عند تسجيل دخول المستخدم المحدد.\r\n• يتم الاحتفاظ بآخر نسخة فقط مع حذف النسخة السابقة تلقائياً لتوفير المساحة.\r\n• يمكنك اختيار (الكل) للنسخ عند دخول أي مستخدم.";
            // 
            // buttonSaveAutoBackup
            // 
            this.buttonSaveAutoBackup.Image = global::Follow_Extremist.Properties.Resources.Save;
            this.buttonSaveAutoBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveAutoBackup.Location = new System.Drawing.Point(12, 348);
            this.buttonSaveAutoBackup.Name = "buttonSaveAutoBackup";
            this.buttonSaveAutoBackup.Size = new System.Drawing.Size(342, 55);
            this.buttonSaveAutoBackup.TabIndex = 8;
            this.buttonSaveAutoBackup.Text = "حفظ إعدادات النسخ التلقائي";
            this.buttonSaveAutoBackup.UseVisualStyleBackColor = true;
            this.buttonSaveAutoBackup.Click += new System.EventHandler(this.buttonSaveAutoBackup_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(165, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(195, 26);
            this.label7.TabIndex = 6;
            this.label7.Text = "لون عرض البيانات خارج المتابعة ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(190, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 26);
            this.label11.TabIndex = 7;
            this.label11.Text = "لون عرض البيانات محبوس ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(165, 270);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(195, 26);
            this.label12.TabIndex = 8;
            this.label12.Text = "لون عرض البيانات كسر المتابعة ";
            // 
            // colorPickEditoutfollow
            // 
            this.colorPickEditoutfollow.EditValue = System.Drawing.Color.Empty;
            this.colorPickEditoutfollow.Location = new System.Drawing.Point(8, 167);
            this.colorPickEditoutfollow.Name = "colorPickEditoutfollow";
            this.colorPickEditoutfollow.Properties.Appearance.Font = new System.Drawing.Font("Cairo", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.colorPickEditoutfollow.Properties.Appearance.Options.UseFont = true;
            this.colorPickEditoutfollow.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEditoutfollow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditoutfollow.Size = new System.Drawing.Size(150, 36);
            this.colorPickEditoutfollow.TabIndex = 9;
            // 
            // colorPickEditprison
            // 
            this.colorPickEditprison.EditValue = System.Drawing.Color.Empty;
            this.colorPickEditprison.Location = new System.Drawing.Point(8, 216);
            this.colorPickEditprison.Name = "colorPickEditprison";
            this.colorPickEditprison.Properties.Appearance.Font = new System.Drawing.Font("Cairo", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.colorPickEditprison.Properties.Appearance.Options.UseFont = true;
            this.colorPickEditprison.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEditprison.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditprison.Size = new System.Drawing.Size(175, 36);
            this.colorPickEditprison.TabIndex = 10;
            // 
            // colorPickEditbreakfollow
            // 
            // في دالة InitializeComponent
            this.numericUpDownNotification.Maximum = 3000; // زيادة الحد الأقصى ليستوعب القيم الكبيرة
            this.colorPickEditbreakfollow.EditValue = System.Drawing.Color.Empty;
            this.colorPickEditbreakfollow.Location = new System.Drawing.Point(8, 267);
            this.colorPickEditbreakfollow.Name = "colorPickEditbreakfollow";
            this.colorPickEditbreakfollow.Properties.Appearance.Font = new System.Drawing.Font("Cairo", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.colorPickEditbreakfollow.Properties.Appearance.Options.UseFont = true;
            this.colorPickEditbreakfollow.Properties.AutomaticColor = System.Drawing.Color.Black;
            this.colorPickEditbreakfollow.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.colorPickEditbreakfollow.Size = new System.Drawing.Size(150, 36);
            this.colorPickEditbreakfollow.TabIndex = 11;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 555);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اعدادات النظام";
            this.Activated += new System.EventHandler(this.SettingsForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotification)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeOut)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBoxAutoBackup.ResumeLayout(false);
            this.groupBoxAutoBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditoutfollow.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditprison.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPickEditbreakfollow.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabelImportImage;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.NumericUpDown numericUpDownDataRow;
        private System.Windows.Forms.NumericUpDown numericUpDownNotification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveGeneral;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonSaveConnectionString;
        private System.Windows.Forms.RadioButton radioButtonNetworkConn;
        private System.Windows.Forms.RadioButton radioButtonLocalCon;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeOut;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.Button buttonBackUp;
        private System.Windows.Forms.GroupBox groupBoxAutoBackup;
        private System.Windows.Forms.CheckBox checkBoxAutoBackup;
        private System.Windows.Forms.Label labelAutoBackupUser;
        private System.Windows.Forms.ComboBox comboBoxAutoBackupUser;
        private System.Windows.Forms.Label labelAutoBackupPath;
        private System.Windows.Forms.TextBox textBoxAutoBackupPath;
        private System.Windows.Forms.Button buttonBrowseAutoBackup;
        private System.Windows.Forms.Label labelLastBackupStatus;
        private System.Windows.Forms.Label labelAutoBackupNote;
        private System.Windows.Forms.Button buttonSaveAutoBackup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditbreakfollow;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditprison;
        private DevExpress.XtraEditors.ColorPickEdit colorPickEditoutfollow;
        private System.Windows.Forms.Label label12;
    }
}