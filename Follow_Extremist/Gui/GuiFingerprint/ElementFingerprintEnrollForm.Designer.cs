namespace Follow_Extremist.Gui.GuiFingerprint
{
    partial class ElementFingerprintEnrollForm
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
            buttonDeleteUserFromDevice = new System.Windows.Forms.Button();
            buttonDeleteFingerprint = new System.Windows.Forms.Button();
            buttonBulkSyncAll = new System.Windows.Forms.Button();
            buttonAutoMatchDeviceUsers = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            panelContent = new System.Windows.Forms.Panel();
            dataGridViewFingerprints = new System.Windows.Forms.DataGridView();
            colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFingerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            labelGridTitle = new System.Windows.Forms.Label();
            labelStatus = new System.Windows.Forms.Label();
            groupBoxEnroll = new System.Windows.Forms.GroupBox();
            labelHintEnroll = new System.Windows.Forms.Label();
            buttonFetchAllFingers = new System.Windows.Forms.Button();
            buttonSyncUserName = new System.Windows.Forms.Button();
            buttonStartRemoteEnroll = new System.Windows.Forms.Button();
            buttonUploadToDevice = new System.Windows.Forms.Button();
            buttonFetchFromDevice = new System.Windows.Forms.Button();
            comboBoxFinger = new System.Windows.Forms.ComboBox();
            labelFinger = new System.Windows.Forms.Label();
            labelElementDetails = new System.Windows.Forms.Label();
            searchableElementDropDown = new SearchableElementDropDown();
            labelSelectElement = new System.Windows.Forms.Label();
            panelHeader.SuspendLayout();
            panelFooter.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFingerprints).BeginInit();
            groupBoxEnroll.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new System.Drawing.Size(920, 58);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            labelTitle.ForeColor = System.Drawing.Color.White;
            labelTitle.Location = new System.Drawing.Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(920, 58);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "👆 إدارة وتسجيل ومزامنة البصمات مع الأجهزة";
            labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            panelFooter.Controls.Add(buttonDeleteUserFromDevice);
            panelFooter.Controls.Add(buttonDeleteFingerprint);
            panelFooter.Controls.Add(buttonBulkSyncAll);
            panelFooter.Controls.Add(buttonAutoMatchDeviceUsers);
            panelFooter.Controls.Add(buttonClose);
            panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelFooter.Location = new System.Drawing.Point(0, 657);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new System.Windows.Forms.Padding(16, 11, 16, 11);
            panelFooter.Size = new System.Drawing.Size(920, 58);
            panelFooter.TabIndex = 1;
            // 
            // buttonDeleteUserFromDevice
            // 
            buttonDeleteUserFromDevice.BackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            buttonDeleteUserFromDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonDeleteUserFromDevice.Dock = System.Windows.Forms.DockStyle.Right;
            buttonDeleteUserFromDevice.FlatAppearance.BorderSize = 0;
            buttonDeleteUserFromDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonDeleteUserFromDevice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonDeleteUserFromDevice.ForeColor = System.Drawing.Color.White;
            buttonDeleteUserFromDevice.Location = new System.Drawing.Point(524, 11);
            buttonDeleteUserFromDevice.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            buttonDeleteUserFromDevice.Name = "buttonDeleteUserFromDevice";
            buttonDeleteUserFromDevice.Size = new System.Drawing.Size(195, 36);
            buttonDeleteUserFromDevice.TabIndex = 2;
            buttonDeleteUserFromDevice.Text = "⚠️ مسح العنصر من الجهاز";
            buttonDeleteUserFromDevice.UseVisualStyleBackColor = false;
            buttonDeleteUserFromDevice.Click += buttonDeleteUserFromDevice_Click;
            // 
            // buttonDeleteFingerprint
            // 
            buttonDeleteFingerprint.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            buttonDeleteFingerprint.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonDeleteFingerprint.Dock = System.Windows.Forms.DockStyle.Right;
            buttonDeleteFingerprint.FlatAppearance.BorderSize = 0;
            buttonDeleteFingerprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonDeleteFingerprint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonDeleteFingerprint.ForeColor = System.Drawing.Color.White;
            buttonDeleteFingerprint.Location = new System.Drawing.Point(719, 11);
            buttonDeleteFingerprint.Name = "buttonDeleteFingerprint";
            buttonDeleteFingerprint.Size = new System.Drawing.Size(185, 36);
            buttonDeleteFingerprint.TabIndex = 0;
            buttonDeleteFingerprint.Text = "🗑️ حذف البصمة المحددة";
            buttonDeleteFingerprint.UseVisualStyleBackColor = false;
            buttonDeleteFingerprint.Click += buttonDeleteFingerprint_Click;
            // 
            // buttonBulkSyncAll
            // 
            buttonBulkSyncAll.BackColor = System.Drawing.Color.FromArgb(79, 70, 229);
            buttonBulkSyncAll.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonBulkSyncAll.Dock = System.Windows.Forms.DockStyle.Left;
            buttonBulkSyncAll.FlatAppearance.BorderSize = 0;
            buttonBulkSyncAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonBulkSyncAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonBulkSyncAll.ForeColor = System.Drawing.Color.White;
            buttonBulkSyncAll.Location = new System.Drawing.Point(321, 11);
            buttonBulkSyncAll.Name = "buttonBulkSyncAll";
            buttonBulkSyncAll.Size = new System.Drawing.Size(185, 36);
            buttonBulkSyncAll.TabIndex = 3;
            buttonBulkSyncAll.Text = "🔄 مزامنة شاملة بالجهاز";
            buttonBulkSyncAll.UseVisualStyleBackColor = false;
            buttonBulkSyncAll.Click += buttonBulkSyncAll_Click;
            // 
            // buttonAutoMatchDeviceUsers
            // 
            buttonAutoMatchDeviceUsers.BackColor = System.Drawing.Color.FromArgb(13, 148, 136);
            buttonAutoMatchDeviceUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonAutoMatchDeviceUsers.Dock = System.Windows.Forms.DockStyle.Left;
            buttonAutoMatchDeviceUsers.FlatAppearance.BorderSize = 0;
            buttonAutoMatchDeviceUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonAutoMatchDeviceUsers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonAutoMatchDeviceUsers.ForeColor = System.Drawing.Color.White;
            buttonAutoMatchDeviceUsers.Location = new System.Drawing.Point(116, 11);
            buttonAutoMatchDeviceUsers.Name = "buttonAutoMatchDeviceUsers";
            buttonAutoMatchDeviceUsers.Size = new System.Drawing.Size(205, 36);
            buttonAutoMatchDeviceUsers.TabIndex = 4;
            buttonAutoMatchDeviceUsers.Text = "🔄 مطابقة وربط أكواد الماكينة";
            buttonAutoMatchDeviceUsers.UseVisualStyleBackColor = false;
            buttonAutoMatchDeviceUsers.Click += buttonAutoMatchDeviceUsers_Click;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = System.Drawing.Color.FromArgb(148, 163, 184);
            buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonClose.Dock = System.Windows.Forms.DockStyle.Left;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonClose.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonClose.ForeColor = System.Drawing.Color.White;
            buttonClose.Location = new System.Drawing.Point(16, 11);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(100, 36);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "❌ إغلاق";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(dataGridViewFingerprints);
            panelContent.Controls.Add(labelGridTitle);
            panelContent.Controls.Add(labelStatus);
            panelContent.Controls.Add(groupBoxEnroll);
            panelContent.Controls.Add(labelElementDetails);
            panelContent.Controls.Add(searchableElementDropDown);
            panelContent.Controls.Add(labelSelectElement);
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(0, 58);
            panelContent.Name = "panelContent";
            panelContent.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            panelContent.Size = new System.Drawing.Size(920, 599);
            panelContent.TabIndex = 2;
            // 
            // dataGridViewFingerprints
            // 
            dataGridViewFingerprints.AllowUserToAddRows = false;
            dataGridViewFingerprints.AllowUserToDeleteRows = false;
            dataGridViewFingerprints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewFingerprints.BackgroundColor = System.Drawing.Color.White;
            dataGridViewFingerprints.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewFingerprints.ColumnHeadersHeight = 34;
            dataGridViewFingerprints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewFingerprints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colId, colFingerName, colCreatedDate });
            dataGridViewFingerprints.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewFingerprints.EnableHeadersVisualStyles = false;
            dataGridViewFingerprints.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dataGridViewFingerprints.Location = new System.Drawing.Point(20, 384);
            dataGridViewFingerprints.MultiSelect = false;
            dataGridViewFingerprints.Name = "dataGridViewFingerprints";
            dataGridViewFingerprints.ReadOnly = true;
            dataGridViewFingerprints.RowHeadersVisible = false;
            dataGridViewFingerprints.RowHeadersWidth = 51;
            dataGridViewFingerprints.RowTemplate.Height = 32;
            dataGridViewFingerprints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewFingerprints.Size = new System.Drawing.Size(840, 191);
            dataGridViewFingerprints.TabIndex = 6;
            // 
            // colId
            // 
            colId.FillWeight = 25F;
            colId.HeaderText = "م";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colFingerName
            // 
            colFingerName.FillWeight = 90F;
            colFingerName.HeaderText = "إصبع البصمة المسجل";
            colFingerName.MinimumWidth = 6;
            colFingerName.Name = "colFingerName";
            colFingerName.ReadOnly = true;
            // 
            // colCreatedDate
            // 
            colCreatedDate.FillWeight = 80F;
            colCreatedDate.HeaderText = "تاريخ ووقت التسجيل";
            colCreatedDate.MinimumWidth = 6;
            colCreatedDate.Name = "colCreatedDate";
            colCreatedDate.ReadOnly = true;
            // 
            // labelGridTitle
            // 
            labelGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelGridTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            labelGridTitle.Location = new System.Drawing.Point(20, 348);
            labelGridTitle.Name = "labelGridTitle";
            labelGridTitle.Size = new System.Drawing.Size(840, 36);
            labelGridTitle.TabIndex = 5;
            labelGridTitle.Text = "قائمة البصمات المسجلة لهذا العنصر في قاعدة البيانات:";
            labelGridTitle.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // labelStatus
            // 
            labelStatus.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            labelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelStatus.Dock = System.Windows.Forms.DockStyle.Top;
            labelStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            labelStatus.Location = new System.Drawing.Point(20, 303);
            labelStatus.Name = "labelStatus";
            labelStatus.Padding = new System.Windows.Forms.Padding(6);
            labelStatus.Size = new System.Drawing.Size(840, 45);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "حالة العمليات: جاهز";
            labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxEnroll
            // 
            groupBoxEnroll.BackColor = System.Drawing.Color.White;
            groupBoxEnroll.Controls.Add(labelHintEnroll);
            groupBoxEnroll.Controls.Add(buttonSyncUserName);
            groupBoxEnroll.Controls.Add(labelFinger);
            groupBoxEnroll.Controls.Add(comboBoxFinger);
            groupBoxEnroll.Controls.Add(buttonStartRemoteEnroll);
            groupBoxEnroll.Controls.Add(buttonFetchFromDevice);
            groupBoxEnroll.Controls.Add(buttonFetchAllFingers);
            groupBoxEnroll.Controls.Add(buttonUploadToDevice);
            groupBoxEnroll.Dock = System.Windows.Forms.DockStyle.Top;
            groupBoxEnroll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            groupBoxEnroll.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            groupBoxEnroll.Location = new System.Drawing.Point(20, 141);
            groupBoxEnroll.Name = "groupBoxEnroll";
            groupBoxEnroll.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            groupBoxEnroll.Size = new System.Drawing.Size(880, 175);
            groupBoxEnroll.TabIndex = 3;
            groupBoxEnroll.TabStop = false;
            groupBoxEnroll.Text = "عمليات سحب ومزامنة وتسجيل البصمة مع الجهاز";
            // 
            // labelHintEnroll
            // 
            labelHintEnroll.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            labelHintEnroll.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelHintEnroll.Location = new System.Drawing.Point(20, 116);
            labelHintEnroll.Name = "labelHintEnroll";
            labelHintEnroll.Size = new System.Drawing.Size(840, 48);
            labelHintEnroll.TabIndex = 7;
            labelHintEnroll.Text = "💡 تسلسل العمل الموصى به: 1️⃣ اضغط 'مزامنة وكود العنصر بالجهاز' لإنشاء الكود الموحد وإرسال الاسم، ثم 2️⃣ حدد الإصبع واضغط 'بدء تسجيل البصمة بالجهاز' وضعه 3 مرات على الحساس، وأخيراً 3️⃣ اضغط 'سحب بصمة الإصبع المحدد' لحفظ القالب المركزي.";
            labelHintEnroll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSyncUserName
            // 
            buttonSyncUserName.BackColor = System.Drawing.Color.FromArgb(2, 132, 199);
            buttonSyncUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonSyncUserName.FlatAppearance.BorderSize = 0;
            buttonSyncUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSyncUserName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonSyncUserName.ForeColor = System.Drawing.Color.White;
            buttonSyncUserName.Location = new System.Drawing.Point(590, 28);
            buttonSyncUserName.Name = "buttonSyncUserName";
            buttonSyncUserName.Size = new System.Drawing.Size(270, 36);
            buttonSyncUserName.TabIndex = 0;
            buttonSyncUserName.Text = "👤 1. مزامنة وكود العنصر بالجهاز";
            buttonSyncUserName.UseVisualStyleBackColor = false;
            buttonSyncUserName.Click += buttonSyncUserName_Click;
            // 
            // labelFinger
            // 
            labelFinger.AutoSize = true;
            labelFinger.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelFinger.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelFinger.Location = new System.Drawing.Point(490, 35);
            labelFinger.Name = "labelFinger";
            labelFinger.Size = new System.Drawing.Size(85, 21);
            labelFinger.TabIndex = 1;
            labelFinger.Text = "تحديد الإصبع:";
            // 
            // comboBoxFinger
            // 
            comboBoxFinger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxFinger.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            comboBoxFinger.FormattingEnabled = true;
            comboBoxFinger.Items.AddRange(new object[] { "0 - إبهام يمين", "1 - سبابة يمنى (مفضل)", "2 - وسطى يمنى", "3 - بنصر أيمن", "4 - خنصر أيمن", "5 - إبهام يسار", "6 - سبابة يسرى", "7 - وسطى يسرى", "8 - بنصر أيسر", "9 - خنصر أيسر" });
            comboBoxFinger.Location = new System.Drawing.Point(305, 32);
            comboBoxFinger.Name = "comboBoxFinger";
            comboBoxFinger.Size = new System.Drawing.Size(180, 29);
            comboBoxFinger.TabIndex = 2;
            // 
            // buttonStartRemoteEnroll
            // 
            buttonStartRemoteEnroll.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            buttonStartRemoteEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonStartRemoteEnroll.FlatAppearance.BorderSize = 0;
            buttonStartRemoteEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonStartRemoteEnroll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonStartRemoteEnroll.ForeColor = System.Drawing.Color.White;
            buttonStartRemoteEnroll.Location = new System.Drawing.Point(20, 28);
            buttonStartRemoteEnroll.Name = "buttonStartRemoteEnroll";
            buttonStartRemoteEnroll.Size = new System.Drawing.Size(270, 36);
            buttonStartRemoteEnroll.TabIndex = 3;
            buttonStartRemoteEnroll.Text = "👆 2. بدء تسجيل البصمة بالجهاز";
            buttonStartRemoteEnroll.UseVisualStyleBackColor = false;
            buttonStartRemoteEnroll.Click += buttonStartRemoteEnroll_Click;
            // 
            // buttonFetchFromDevice
            // 
            buttonFetchFromDevice.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            buttonFetchFromDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonFetchFromDevice.FlatAppearance.BorderSize = 0;
            buttonFetchFromDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonFetchFromDevice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonFetchFromDevice.ForeColor = System.Drawing.Color.White;
            buttonFetchFromDevice.Location = new System.Drawing.Point(590, 72);
            buttonFetchFromDevice.Name = "buttonFetchFromDevice";
            buttonFetchFromDevice.Size = new System.Drawing.Size(270, 36);
            buttonFetchFromDevice.TabIndex = 4;
            buttonFetchFromDevice.Text = "📥 3. سحب بصمة الإصبع المحدد";
            buttonFetchFromDevice.UseVisualStyleBackColor = false;
            buttonFetchFromDevice.Click += buttonFetchFromDevice_Click;
            // 
            // buttonFetchAllFingers
            // 
            buttonFetchAllFingers.BackColor = System.Drawing.Color.FromArgb(8, 145, 178);
            buttonFetchAllFingers.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonFetchAllFingers.FlatAppearance.BorderSize = 0;
            buttonFetchAllFingers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonFetchAllFingers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonFetchAllFingers.ForeColor = System.Drawing.Color.White;
            buttonFetchAllFingers.Location = new System.Drawing.Point(305, 72);
            buttonFetchAllFingers.Name = "buttonFetchAllFingers";
            buttonFetchAllFingers.Size = new System.Drawing.Size(270, 36);
            buttonFetchAllFingers.TabIndex = 5;
            buttonFetchAllFingers.Text = "📥 سحب كافة بصمات العنصر (0-9)";
            buttonFetchAllFingers.UseVisualStyleBackColor = false;
            buttonFetchAllFingers.Click += buttonFetchAllFingers_Click;
            // 
            // buttonUploadToDevice
            // 
            buttonUploadToDevice.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            buttonUploadToDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonUploadToDevice.FlatAppearance.BorderSize = 0;
            buttonUploadToDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonUploadToDevice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonUploadToDevice.ForeColor = System.Drawing.Color.White;
            buttonUploadToDevice.Location = new System.Drawing.Point(20, 72);
            buttonUploadToDevice.Name = "buttonUploadToDevice";
            buttonUploadToDevice.Size = new System.Drawing.Size(270, 36);
            buttonUploadToDevice.TabIndex = 6;
            buttonUploadToDevice.Text = "📤 رفع قوالب البصمات للجهاز";
            buttonUploadToDevice.UseVisualStyleBackColor = false;
            buttonUploadToDevice.Click += buttonUploadToDevice_Click;
            // 
            // labelElementDetails
            // 
            labelElementDetails.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            labelElementDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelElementDetails.Dock = System.Windows.Forms.DockStyle.Top;
            labelElementDetails.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            labelElementDetails.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelElementDetails.Location = new System.Drawing.Point(20, 89);
            labelElementDetails.Name = "labelElementDetails";
            labelElementDetails.Padding = new System.Windows.Forms.Padding(8);
            labelElementDetails.Size = new System.Drawing.Size(880, 52);
            labelElementDetails.TabIndex = 2;
            labelElementDetails.Text = "بيانات الشخص: لم يتم تحديد اسم بعد";
            labelElementDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // searchableElementDropDown
            // 
            searchableElementDropDown.BackColor = System.Drawing.Color.White;
            searchableElementDropDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            searchableElementDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            searchableElementDropDown.Location = new System.Drawing.Point(20, 49);
            searchableElementDropDown.Name = "searchableElementDropDown";
            searchableElementDropDown.Padding = new System.Windows.Forms.Padding(1);
            searchableElementDropDown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            searchableElementDropDown.Size = new System.Drawing.Size(880, 40);
            searchableElementDropDown.TabIndex = 1;
            // 
            // labelSelectElement
            // 
            labelSelectElement.AutoSize = true;
            labelSelectElement.Dock = System.Windows.Forms.DockStyle.Top;
            labelSelectElement.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            labelSelectElement.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            labelSelectElement.Location = new System.Drawing.Point(20, 16);
            labelSelectElement.Name = "labelSelectElement";
            labelSelectElement.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            labelSelectElement.Size = new System.Drawing.Size(172, 33);
            labelSelectElement.TabIndex = 0;
            labelSelectElement.Text = "اختيار الأسم للتسجيل:";
            labelSelectElement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ElementFingerprintEnrollForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(920, 715);
            Controls.Add(panelContent);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ElementFingerprintEnrollForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "تسجيل وإدارة بصمات العناصر مع الأجهزة";
            Load += ElementFingerprintEnrollForm_Load;
            panelHeader.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFingerprints).EndInit();
            groupBoxEnroll.ResumeLayout(false);
            groupBoxEnroll.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button buttonDeleteFingerprint;
        private System.Windows.Forms.Button buttonDeleteUserFromDevice;
        private System.Windows.Forms.Button buttonBulkSyncAll;
        private System.Windows.Forms.Button buttonAutoMatchDeviceUsers;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label labelSelectElement;
        private Follow_Extremist.Gui.GuiFingerprint.SearchableElementDropDown searchableElementDropDown;
        private System.Windows.Forms.Label labelElementDetails;
        private System.Windows.Forms.GroupBox groupBoxEnroll;
        private System.Windows.Forms.Label labelFinger;
        private System.Windows.Forms.ComboBox comboBoxFinger;
        private System.Windows.Forms.Button buttonStartRemoteEnroll;
        private System.Windows.Forms.Button buttonSyncUserName;
        private System.Windows.Forms.Button buttonFetchFromDevice;
        private System.Windows.Forms.Button buttonFetchAllFingers;
        private System.Windows.Forms.Button buttonUploadToDevice;
        private System.Windows.Forms.Label labelHintEnroll;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelGridTitle;
        private System.Windows.Forms.DataGridView dataGridViewFingerprints;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFingerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedDate;
    }
}
