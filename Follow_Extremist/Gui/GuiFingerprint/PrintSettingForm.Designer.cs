namespace Follow_Extremist.Gui.GuiFingerprint
{
    partial class PrintSettingForm
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
            panelHeader = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            panelFooter = new System.Windows.Forms.Panel();
            buttonTestPrint = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            panelContent = new System.Windows.Forms.Panel();
            groupBoxContent = new System.Windows.Forms.GroupBox();
            checkBoxNextDate = new System.Windows.Forms.CheckBox();
            checkBoxNationalId = new System.Windows.Forms.CheckBox();
            checkBoxBarcode = new System.Windows.Forms.CheckBox();
            textBoxFooter = new System.Windows.Forms.TextBox();
            labelFooter = new System.Windows.Forms.Label();
            textBoxHeader = new System.Windows.Forms.TextBox();
            labelHeader = new System.Windows.Forms.Label();
            groupBoxHardware = new System.Windows.Forms.GroupBox();
            checkBoxAutoPrint = new System.Windows.Forms.CheckBox();
            numericCopies = new System.Windows.Forms.NumericUpDown();
            labelCopies = new System.Windows.Forms.Label();
            comboBoxPaperWidth = new System.Windows.Forms.ComboBox();
            labelPaperWidth = new System.Windows.Forms.Label();
            comboBoxPrinterName = new System.Windows.Forms.ComboBox();
            labelPrinterName = new System.Windows.Forms.Label();
            comboBoxPrinterType = new System.Windows.Forms.ComboBox();
            labelPrinterType = new System.Windows.Forms.Label();
            groupBoxMode = new System.Windows.Forms.GroupBox();
            buttonPreset30 = new System.Windows.Forms.Button();
            buttonPreset15 = new System.Windows.Forms.Button();
            buttonPreset12 = new System.Windows.Forms.Button();
            buttonPreset10 = new System.Windows.Forms.Button();
            buttonPreset5 = new System.Windows.Forms.Button();
            labelPresets = new System.Windows.Forms.Label();
            labelDurationUnit = new System.Windows.Forms.Label();
            buttonDurationPlus = new System.Windows.Forms.Button();
            numericScreenDuration = new System.Windows.Forms.NumericUpDown();
            buttonDurationMinus = new System.Windows.Forms.Button();
            labelScreenDuration = new System.Windows.Forms.Label();
            radioButtonPrintOnly = new System.Windows.Forms.RadioButton();
            radioButtonScreenOnly = new System.Windows.Forms.RadioButton();
            radioButtonBoth = new System.Windows.Forms.RadioButton();
            panelHeader.SuspendLayout();
            panelFooter.SuspendLayout();
            panelContent.SuspendLayout();
            groupBoxContent.SuspendLayout();
            groupBoxHardware.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericCopies).BeginInit();
            groupBoxMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericScreenDuration).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new System.Drawing.Size(694, 58);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            labelTitle.ForeColor = System.Drawing.Color.White;
            labelTitle.Location = new System.Drawing.Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(694, 58);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "🖨️ إعدادات طباعة إشعار حضور المتابعة";
            labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            panelFooter.Controls.Add(buttonTestPrint);
            panelFooter.Controls.Add(buttonSave);
            panelFooter.Controls.Add(buttonCancel);
            panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelFooter.Location = new System.Drawing.Point(0, 631);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            panelFooter.Size = new System.Drawing.Size(694, 58);
            panelFooter.TabIndex = 1;
            // 
            // buttonTestPrint
            // 
            buttonTestPrint.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            buttonTestPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonTestPrint.Dock = System.Windows.Forms.DockStyle.Right;
            buttonTestPrint.FlatAppearance.BorderSize = 0;
            buttonTestPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonTestPrint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            buttonTestPrint.ForeColor = System.Drawing.Color.White;
            buttonTestPrint.Location = new System.Drawing.Point(498, 12);
            buttonTestPrint.Name = "buttonTestPrint";
            buttonTestPrint.Size = new System.Drawing.Size(180, 34);
            buttonTestPrint.TabIndex = 0;
            buttonTestPrint.Text = "👁️ معاينة / اختبار الطباعة";
            buttonTestPrint.UseVisualStyleBackColor = false;
            buttonTestPrint.Click += buttonTestPrint_Click;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonSave.Dock = System.Windows.Forms.DockStyle.Left;
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSave.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            buttonSave.ForeColor = System.Drawing.Color.White;
            buttonSave.Location = new System.Drawing.Point(146, 12);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(150, 34);
            buttonSave.TabIndex = 1;
            buttonSave.Text = "💾 حفظ الإعدادات";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = System.Drawing.Color.FromArgb(148, 163, 184);
            buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonCancel.Dock = System.Windows.Forms.DockStyle.Left;
            buttonCancel.FlatAppearance.BorderSize = 0;
            buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            buttonCancel.ForeColor = System.Drawing.Color.White;
            buttonCancel.Location = new System.Drawing.Point(16, 12);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(130, 34);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "إلغاء";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // panelContent
            // 
            panelContent.AutoScroll = true;
            panelContent.Controls.Add(groupBoxContent);
            panelContent.Controls.Add(groupBoxHardware);
            panelContent.Controls.Add(groupBoxMode);
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(0, 58);
            panelContent.Name = "panelContent";
            panelContent.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            panelContent.Size = new System.Drawing.Size(694, 573);
            panelContent.TabIndex = 2;
            // 
            // groupBoxContent
            // 
            groupBoxContent.BackColor = System.Drawing.Color.White;
            groupBoxContent.Controls.Add(checkBoxNextDate);
            groupBoxContent.Controls.Add(checkBoxNationalId);
            groupBoxContent.Controls.Add(checkBoxBarcode);
            groupBoxContent.Controls.Add(textBoxFooter);
            groupBoxContent.Controls.Add(labelFooter);
            groupBoxContent.Controls.Add(textBoxHeader);
            groupBoxContent.Controls.Add(labelHeader);
            groupBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxContent.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            groupBoxContent.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            groupBoxContent.Location = new System.Drawing.Point(20, 336);
            groupBoxContent.Name = "groupBoxContent";
            groupBoxContent.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            groupBoxContent.Size = new System.Drawing.Size(654, 225);
            groupBoxContent.TabIndex = 1;
            groupBoxContent.TabStop = false;
            groupBoxContent.Text = "محتوى وتنسيق إشعار الحضور";
            // 
            // checkBoxNextDate
            // 
            checkBoxNextDate.AutoSize = true;
            checkBoxNextDate.Checked = true;
            checkBoxNextDate.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxNextDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            checkBoxNextDate.Location = new System.Drawing.Point(40, 162);
            checkBoxNextDate.Name = "checkBoxNextDate";
            checkBoxNextDate.Size = new System.Drawing.Size(198, 25);
            checkBoxNextDate.TabIndex = 6;
            checkBoxNextDate.Text = "إظهار تاريخ المتابعة القادم";
            checkBoxNextDate.UseVisualStyleBackColor = true;
            // 
            // checkBoxNationalId
            // 
            checkBoxNationalId.AutoSize = true;
            checkBoxNationalId.Checked = true;
            checkBoxNationalId.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxNationalId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            checkBoxNationalId.Location = new System.Drawing.Point(235, 162);
            checkBoxNationalId.Name = "checkBoxNationalId";
            checkBoxNationalId.Size = new System.Drawing.Size(155, 25);
            checkBoxNationalId.TabIndex = 5;
            checkBoxNationalId.Text = "إظهار الرقم القومي";
            checkBoxNationalId.UseVisualStyleBackColor = true;
            // 
            // checkBoxBarcode
            // 
            checkBoxBarcode.AutoSize = true;
            checkBoxBarcode.Checked = true;
            checkBoxBarcode.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxBarcode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            checkBoxBarcode.Location = new System.Drawing.Point(395, 162);
            checkBoxBarcode.Name = "checkBoxBarcode";
            checkBoxBarcode.Size = new System.Drawing.Size(120, 25);
            checkBoxBarcode.TabIndex = 4;
            checkBoxBarcode.Text = "إظهار الباركود";
            checkBoxBarcode.UseVisualStyleBackColor = true;
            // 
            // textBoxFooter
            // 
            textBoxFooter.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBoxFooter.Location = new System.Drawing.Point(20, 115);
            textBoxFooter.Name = "textBoxFooter";
            textBoxFooter.Size = new System.Drawing.Size(400, 30);
            textBoxFooter.TabIndex = 3;
            textBoxFooter.Text = "يرجى الالتزام بموعد المتابعة القادم والاحتفاظ بهذا الإشعار";
            // 
            // labelFooter
            // 
            labelFooter.AutoSize = true;
            labelFooter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelFooter.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelFooter.Location = new System.Drawing.Point(435, 118);
            labelFooter.Name = "labelFooter";
            labelFooter.Size = new System.Drawing.Size(127, 21);
            labelFooter.TabIndex = 2;
            labelFooter.Text = "نص التذييل أسفل:";
            // 
            // textBoxHeader
            // 
            textBoxHeader.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBoxHeader.Location = new System.Drawing.Point(20, 70);
            textBoxHeader.Name = "textBoxHeader";
            textBoxHeader.Size = new System.Drawing.Size(400, 30);
            textBoxHeader.TabIndex = 1;
            textBoxHeader.Text = "حضور متابعة";
            // 
            // labelHeader
            // 
            labelHeader.AutoSize = true;
            labelHeader.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelHeader.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelHeader.Location = new System.Drawing.Point(435, 73);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new System.Drawing.Size(101, 21);
            labelHeader.TabIndex = 0;
            labelHeader.Text = "عنوان الإشعار:";
            // 
            // groupBoxHardware
            // 
            groupBoxHardware.BackColor = System.Drawing.Color.White;
            groupBoxHardware.Controls.Add(checkBoxAutoPrint);
            groupBoxHardware.Controls.Add(numericCopies);
            groupBoxHardware.Controls.Add(labelCopies);
            groupBoxHardware.Controls.Add(comboBoxPaperWidth);
            groupBoxHardware.Controls.Add(labelPaperWidth);
            groupBoxHardware.Controls.Add(comboBoxPrinterName);
            groupBoxHardware.Controls.Add(labelPrinterName);
            groupBoxHardware.Controls.Add(comboBoxPrinterType);
            groupBoxHardware.Controls.Add(labelPrinterType);
            groupBoxHardware.Dock = System.Windows.Forms.DockStyle.Top;
            groupBoxHardware.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            groupBoxHardware.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            groupBoxHardware.Location = new System.Drawing.Point(20, 146);
            groupBoxHardware.Name = "groupBoxHardware";
            groupBoxHardware.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            groupBoxHardware.Size = new System.Drawing.Size(654, 190);
            groupBoxHardware.TabIndex = 0;
            groupBoxHardware.TabStop = false;
            groupBoxHardware.Text = "نوع الطابعة ومواصفات الورق";
            // 
            // checkBoxAutoPrint
            // 
            checkBoxAutoPrint.AutoSize = true;
            checkBoxAutoPrint.Checked = true;
            checkBoxAutoPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxAutoPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            checkBoxAutoPrint.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            checkBoxAutoPrint.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            checkBoxAutoPrint.Location = new System.Drawing.Point(220, 150);
            checkBoxAutoPrint.Name = "checkBoxAutoPrint";
            checkBoxAutoPrint.Size = new System.Drawing.Size(309, 29);
            checkBoxAutoPrint.TabIndex = 8;
            checkBoxAutoPrint.Text = "⚡ طباعة تلقائية فور تسجيل البصمة";
            checkBoxAutoPrint.UseVisualStyleBackColor = true;
            // 
            // numericCopies
            // 
            numericCopies.Font = new System.Drawing.Font("Segoe UI", 10F);
            numericCopies.Location = new System.Drawing.Point(60, 108);
            numericCopies.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericCopies.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericCopies.Name = "numericCopies";
            numericCopies.Size = new System.Drawing.Size(90, 30);
            numericCopies.TabIndex = 7;
            numericCopies.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelCopies
            // 
            labelCopies.AutoSize = true;
            labelCopies.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelCopies.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelCopies.Location = new System.Drawing.Point(155, 111);
            labelCopies.Name = "labelCopies";
            labelCopies.Size = new System.Drawing.Size(79, 21);
            labelCopies.TabIndex = 6;
            labelCopies.Text = "عدد النسخ:";
            // 
            // comboBoxPaperWidth
            // 
            comboBoxPaperWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPaperWidth.Font = new System.Drawing.Font("Segoe UI", 10F);
            comboBoxPaperWidth.FormattingEnabled = true;
            comboBoxPaperWidth.Items.AddRange(new object[] { "80 مم (ورق قياسي عريض)", "58 مم (ورق صغير)" });
            comboBoxPaperWidth.Location = new System.Drawing.Point(240, 108);
            comboBoxPaperWidth.Name = "comboBoxPaperWidth";
            comboBoxPaperWidth.Size = new System.Drawing.Size(220, 31);
            comboBoxPaperWidth.TabIndex = 5;
            // 
            // labelPaperWidth
            // 
            labelPaperWidth.AutoSize = true;
            labelPaperWidth.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelPaperWidth.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelPaperWidth.Location = new System.Drawing.Point(475, 111);
            labelPaperWidth.Name = "labelPaperWidth";
            labelPaperWidth.Size = new System.Drawing.Size(90, 21);
            labelPaperWidth.TabIndex = 4;
            labelPaperWidth.Text = "عرض الورق:";
            // 
            // comboBoxPrinterName
            // 
            comboBoxPrinterName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxPrinterName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxPrinterName.Font = new System.Drawing.Font("Segoe UI", 10F);
            comboBoxPrinterName.FormattingEnabled = true;
            comboBoxPrinterName.Location = new System.Drawing.Point(60, 68);
            comboBoxPrinterName.Name = "comboBoxPrinterName";
            comboBoxPrinterName.Size = new System.Drawing.Size(400, 31);
            comboBoxPrinterName.TabIndex = 3;
            // 
            // labelPrinterName
            // 
            labelPrinterName.AutoSize = true;
            labelPrinterName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelPrinterName.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelPrinterName.Location = new System.Drawing.Point(475, 71);
            labelPrinterName.Name = "labelPrinterName";
            labelPrinterName.Size = new System.Drawing.Size(92, 21);
            labelPrinterName.TabIndex = 2;
            labelPrinterName.Text = "اسم الطابعة:";
            // 
            // comboBoxPrinterType
            // 
            comboBoxPrinterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPrinterType.Font = new System.Drawing.Font("Segoe UI", 10F);
            comboBoxPrinterType.FormattingEnabled = true;
            comboBoxPrinterType.Items.AddRange(new object[] { "طابعة إيصالات حرارية (Thermal Printer)", "طابعة ليزر عادية (Laser A4)" });
            comboBoxPrinterType.Location = new System.Drawing.Point(60, 28);
            comboBoxPrinterType.Name = "comboBoxPrinterType";
            comboBoxPrinterType.Size = new System.Drawing.Size(400, 31);
            comboBoxPrinterType.TabIndex = 1;
            comboBoxPrinterType.SelectedIndexChanged += comboBoxPrinterType_SelectedIndexChanged;
            // 
            // labelPrinterType
            // 
            labelPrinterType.AutoSize = true;
            labelPrinterType.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelPrinterType.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelPrinterType.Location = new System.Drawing.Point(475, 31);
            labelPrinterType.Name = "labelPrinterType";
            labelPrinterType.Size = new System.Drawing.Size(88, 21);
            labelPrinterType.TabIndex = 0;
            labelPrinterType.Text = "نوع الطابعة:";
            // 
            // groupBoxMode
            // 
            groupBoxMode.BackColor = System.Drawing.Color.White;
            groupBoxMode.Controls.Add(buttonPreset30);
            groupBoxMode.Controls.Add(buttonPreset15);
            groupBoxMode.Controls.Add(buttonPreset12);
            groupBoxMode.Controls.Add(buttonPreset10);
            groupBoxMode.Controls.Add(buttonPreset5);
            groupBoxMode.Controls.Add(labelPresets);
            groupBoxMode.Controls.Add(labelDurationUnit);
            groupBoxMode.Controls.Add(buttonDurationPlus);
            groupBoxMode.Controls.Add(numericScreenDuration);
            groupBoxMode.Controls.Add(buttonDurationMinus);
            groupBoxMode.Controls.Add(labelScreenDuration);
            groupBoxMode.Controls.Add(radioButtonPrintOnly);
            groupBoxMode.Controls.Add(radioButtonScreenOnly);
            groupBoxMode.Controls.Add(radioButtonBoth);
            groupBoxMode.Dock = System.Windows.Forms.DockStyle.Top;
            groupBoxMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            groupBoxMode.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            groupBoxMode.Location = new System.Drawing.Point(20, 12);
            groupBoxMode.Name = "groupBoxMode";
            groupBoxMode.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            groupBoxMode.Size = new System.Drawing.Size(654, 134);
            groupBoxMode.TabIndex = 0;
            groupBoxMode.TabStop = false;
            groupBoxMode.Text = "نظام إخراج وتوجيه إشعار الحضور";
            // 
            // buttonPreset30
            // 
            buttonPreset30.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            buttonPreset30.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonPreset30.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            buttonPreset30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPreset30.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            buttonPreset30.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            buttonPreset30.Location = new System.Drawing.Point(75, 92);
            buttonPreset30.Name = "buttonPreset30";
            buttonPreset30.Size = new System.Drawing.Size(65, 26);
            buttonPreset30.TabIndex = 13;
            buttonPreset30.Tag = "30";
            buttonPreset30.Text = "30 ث";
            buttonPreset30.UseVisualStyleBackColor = false;
            buttonPreset30.Click += buttonPreset_Click;
            // 
            // buttonPreset15
            // 
            buttonPreset15.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            buttonPreset15.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonPreset15.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            buttonPreset15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPreset15.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            buttonPreset15.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            buttonPreset15.Location = new System.Drawing.Point(145, 92);
            buttonPreset15.Name = "buttonPreset15";
            buttonPreset15.Size = new System.Drawing.Size(65, 26);
            buttonPreset15.TabIndex = 12;
            buttonPreset15.Tag = "15";
            buttonPreset15.Text = "15 ث";
            buttonPreset15.UseVisualStyleBackColor = false;
            buttonPreset15.Click += buttonPreset_Click;
            // 
            // buttonPreset12
            // 
            buttonPreset12.BackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            buttonPreset12.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonPreset12.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(147, 197, 253);
            buttonPreset12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPreset12.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            buttonPreset12.ForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            buttonPreset12.Location = new System.Drawing.Point(215, 92);
            buttonPreset12.Name = "buttonPreset12";
            buttonPreset12.Size = new System.Drawing.Size(120, 26);
            buttonPreset12.TabIndex = 11;
            buttonPreset12.Tag = "12";
            buttonPreset12.Text = "⭐ 12 ث (افتراضي)";
            buttonPreset12.UseVisualStyleBackColor = false;
            buttonPreset12.Click += buttonPreset_Click;
            // 
            // buttonPreset10
            // 
            buttonPreset10.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            buttonPreset10.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonPreset10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            buttonPreset10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPreset10.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            buttonPreset10.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            buttonPreset10.Location = new System.Drawing.Point(340, 92);
            buttonPreset10.Name = "buttonPreset10";
            buttonPreset10.Size = new System.Drawing.Size(60, 26);
            buttonPreset10.TabIndex = 10;
            buttonPreset10.Tag = "10";
            buttonPreset10.Text = "10 ث";
            buttonPreset10.UseVisualStyleBackColor = false;
            buttonPreset10.Click += buttonPreset_Click;
            // 
            // buttonPreset5
            // 
            buttonPreset5.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            buttonPreset5.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonPreset5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            buttonPreset5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPreset5.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            buttonPreset5.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            buttonPreset5.Location = new System.Drawing.Point(405, 92);
            buttonPreset5.Name = "buttonPreset5";
            buttonPreset5.Size = new System.Drawing.Size(56, 26);
            buttonPreset5.TabIndex = 9;
            buttonPreset5.Tag = "5";
            buttonPreset5.Text = "5 ث";
            buttonPreset5.UseVisualStyleBackColor = false;
            buttonPreset5.Click += buttonPreset_Click;
            // 
            // labelPresets
            // 
            labelPresets.AutoSize = true;
            labelPresets.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            labelPresets.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            labelPresets.Location = new System.Drawing.Point(465, 96);
            labelPresets.Name = "labelPresets";
            labelPresets.Size = new System.Drawing.Size(97, 20);
            labelPresets.TabIndex = 8;
            labelPresets.Text = "خيارات سريعة:";
            // 
            // labelDurationUnit
            // 
            labelDurationUnit.AutoSize = true;
            labelDurationUnit.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            labelDurationUnit.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelDurationUnit.Location = new System.Drawing.Point(142, 58);
            labelDurationUnit.Name = "labelDurationUnit";
            labelDurationUnit.Size = new System.Drawing.Size(37, 21);
            labelDurationUnit.TabIndex = 7;
            labelDurationUnit.Text = "ثانية";
            // 
            // buttonDurationPlus
            // 
            buttonDurationPlus.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            buttonDurationPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonDurationPlus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            buttonDurationPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonDurationPlus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            buttonDurationPlus.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            buttonDurationPlus.Location = new System.Drawing.Point(182, 52);
            buttonDurationPlus.Name = "buttonDurationPlus";
            buttonDurationPlus.Size = new System.Drawing.Size(36, 32);
            buttonDurationPlus.TabIndex = 6;
            buttonDurationPlus.Text = "➕";
            buttonDurationPlus.UseVisualStyleBackColor = false;
            buttonDurationPlus.Click += buttonDurationPlus_Click;
            // 
            // numericScreenDuration
            // 
            numericScreenDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            numericScreenDuration.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            numericScreenDuration.Location = new System.Drawing.Point(222, 53);
            numericScreenDuration.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numericScreenDuration.Name = "numericScreenDuration";
            numericScreenDuration.Size = new System.Drawing.Size(68, 33);
            numericScreenDuration.TabIndex = 5;
            numericScreenDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericScreenDuration.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            numericScreenDuration.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // buttonDurationMinus
            // 
            buttonDurationMinus.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            buttonDurationMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonDurationMinus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            buttonDurationMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonDurationMinus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            buttonDurationMinus.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            buttonDurationMinus.Location = new System.Drawing.Point(294, 52);
            buttonDurationMinus.Name = "buttonDurationMinus";
            buttonDurationMinus.Size = new System.Drawing.Size(36, 32);
            buttonDurationMinus.TabIndex = 4;
            buttonDurationMinus.Text = "➖";
            buttonDurationMinus.UseVisualStyleBackColor = false;
            buttonDurationMinus.Click += buttonDurationMinus_Click;
            // 
            // labelScreenDuration
            // 
            labelScreenDuration.AutoSize = true;
            labelScreenDuration.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelScreenDuration.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelScreenDuration.Location = new System.Drawing.Point(340, 58);
            labelScreenDuration.Name = "labelScreenDuration";
            labelScreenDuration.Size = new System.Drawing.Size(222, 21);
            labelScreenDuration.TabIndex = 3;
            labelScreenDuration.Text = "⏱️ مدة بقاء التقرير على الشاشة:";
            // 
            // radioButtonPrintOnly
            // 
            radioButtonPrintOnly.AutoSize = true;
            radioButtonPrintOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            radioButtonPrintOnly.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            radioButtonPrintOnly.Location = new System.Drawing.Point(8, 24);
            radioButtonPrintOnly.Name = "radioButtonPrintOnly";
            radioButtonPrintOnly.Size = new System.Drawing.Size(168, 25);
            radioButtonPrintOnly.TabIndex = 2;
            radioButtonPrintOnly.Text = "🖨️ طباعة ورقية فقط";
            radioButtonPrintOnly.UseVisualStyleBackColor = true;
            radioButtonPrintOnly.CheckedChanged += OutputMode_CheckedChanged;
            // 
            // radioButtonScreenOnly
            // 
            radioButtonScreenOnly.AutoSize = true;
            radioButtonScreenOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            radioButtonScreenOnly.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            radioButtonScreenOnly.Location = new System.Drawing.Point(176, 24);
            radioButtonScreenOnly.Name = "radioButtonScreenOnly";
            radioButtonScreenOnly.Size = new System.Drawing.Size(177, 25);
            radioButtonScreenOnly.TabIndex = 1;
            radioButtonScreenOnly.Text = "🖥️ شاشة العرض فقط";
            radioButtonScreenOnly.UseVisualStyleBackColor = true;
            radioButtonScreenOnly.CheckedChanged += OutputMode_CheckedChanged;
            // 
            // radioButtonBoth
            // 
            radioButtonBoth.AutoSize = true;
            radioButtonBoth.Checked = true;
            radioButtonBoth.Cursor = System.Windows.Forms.Cursors.Hand;
            radioButtonBoth.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            radioButtonBoth.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            radioButtonBoth.Location = new System.Drawing.Point(352, 24);
            radioButtonBoth.Name = "radioButtonBoth";
            radioButtonBoth.Size = new System.Drawing.Size(282, 25);
            radioButtonBoth.TabIndex = 0;
            radioButtonBoth.TabStop = true;
            radioButtonBoth.Text = "⚡ العمل بالإثنين معاً (شاشة + طباعة)";
            radioButtonBoth.UseVisualStyleBackColor = true;
            radioButtonBoth.CheckedChanged += OutputMode_CheckedChanged;
            // 
            // PrintSettingForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(694, 689);
            Controls.Add(panelContent);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PrintSettingForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "إعدادات الطباعة";
            Load += PrintSettingForm_Load;
            panelHeader.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            groupBoxContent.ResumeLayout(false);
            groupBoxContent.PerformLayout();
            groupBoxHardware.ResumeLayout(false);
            groupBoxHardware.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericCopies).EndInit();
            groupBoxMode.ResumeLayout(false);
            groupBoxMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericScreenDuration).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button buttonTestPrint;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonBoth;
        private System.Windows.Forms.RadioButton radioButtonScreenOnly;
        private System.Windows.Forms.RadioButton radioButtonPrintOnly;
        private System.Windows.Forms.Label labelScreenDuration;
        private System.Windows.Forms.NumericUpDown numericScreenDuration;
        private System.Windows.Forms.Button buttonDurationPlus;
        private System.Windows.Forms.Button buttonDurationMinus;
        private System.Windows.Forms.Label labelDurationUnit;
        private System.Windows.Forms.Label labelPresets;
        private System.Windows.Forms.Button buttonPreset5;
        private System.Windows.Forms.Button buttonPreset10;
        private System.Windows.Forms.Button buttonPreset12;
        private System.Windows.Forms.Button buttonPreset15;
        private System.Windows.Forms.Button buttonPreset30;
        private System.Windows.Forms.GroupBox groupBoxHardware;
        private System.Windows.Forms.Label labelPrinterType;
        private System.Windows.Forms.ComboBox comboBoxPrinterType;
        private System.Windows.Forms.Label labelPrinterName;
        private System.Windows.Forms.ComboBox comboBoxPrinterName;
        private System.Windows.Forms.Label labelPaperWidth;
        private System.Windows.Forms.ComboBox comboBoxPaperWidth;
        private System.Windows.Forms.Label labelCopies;
        private System.Windows.Forms.NumericUpDown numericCopies;
        private System.Windows.Forms.CheckBox checkBoxAutoPrint;
        private System.Windows.Forms.GroupBox groupBoxContent;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.TextBox textBoxHeader;
        private System.Windows.Forms.Label labelFooter;
        private System.Windows.Forms.TextBox textBoxFooter;
        private System.Windows.Forms.CheckBox checkBoxBarcode;
        private System.Windows.Forms.CheckBox checkBoxNationalId;
        private System.Windows.Forms.CheckBox checkBoxNextDate;
    }
}
