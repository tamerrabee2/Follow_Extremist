namespace Follow_Extremist.Gui.GuiFingerprint
{
    partial class FingerprintAttendanceUserControl
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
            panelTopBar = new System.Windows.Forms.Panel();
            flowLayoutPanelActions = new System.Windows.Forms.FlowLayoutPanel();
            buttonConnect = new System.Windows.Forms.Button();
            buttonSyncLogs = new System.Windows.Forms.Button();
            buttonSyncTime = new System.Windows.Forms.Button();
            buttonManualAttendance = new System.Windows.Forms.Button();
            buttonManageWanted = new System.Windows.Forms.Button();
            buttonEnrollFingerprint = new System.Windows.Forms.Button();
            buttonDeviceSettings = new System.Windows.Forms.Button();
            buttonPrintSettings = new System.Windows.Forms.Button();
            buttonOpenKiosk = new System.Windows.Forms.Button();
            panelStatusPill = new System.Windows.Forms.Panel();
            labelDeviceStatus = new System.Windows.Forms.Label();
            lblStatusDot = new System.Windows.Forms.Label();
            panelKpi = new System.Windows.Forms.Panel();
            panelKpiPrinted = new System.Windows.Forms.Panel();
            labelKpiPrintedVal = new System.Windows.Forms.Label();
            labelKpiPrintedTitle = new System.Windows.Forms.Label();
            panelKpiWanted = new System.Windows.Forms.Panel();
            labelKpiWantedVal = new System.Windows.Forms.Label();
            labelKpiWantedTitle = new System.Windows.Forms.Label();
            panelKpiTotal = new System.Windows.Forms.Panel();
            labelKpiTotalVal = new System.Windows.Forms.Label();
            labelKpiTotalTitle = new System.Windows.Forms.Label();
            splitContainerMain = new System.Windows.Forms.SplitContainer();
            panelGridContainer = new System.Windows.Forms.Panel();
            dataGridViewToday = new System.Windows.Forms.DataGridView();
            panelSearch = new System.Windows.Forms.Panel();
            comboBoxFilterStatus = new System.Windows.Forms.ComboBox();
            buttonRefreshGrid = new System.Windows.Forms.Button();
            textBoxSearch = new System.Windows.Forms.TextBox();
            labelSearch = new System.Windows.Forms.Label();
            panelCard = new System.Windows.Forms.Panel();
            panelCardContent = new System.Windows.Forms.Panel();
            buttonReprint = new System.Windows.Forms.Button();
            panelNextDate = new System.Windows.Forms.Panel();
            labelNextDateValue = new System.Windows.Forms.Label();
            labelNextDateTitle = new System.Windows.Forms.Label();
            panelWantedAlert = new System.Windows.Forms.Panel();
            labelWantedReason = new System.Windows.Forms.Label();
            labelWantedTitle = new System.Windows.Forms.Label();
            labelCardJob = new System.Windows.Forms.Label();
            labelCardNationalId = new System.Windows.Forms.Label();
            labelCardName = new System.Windows.Forms.Label();
            panelPhotoFrame = new System.Windows.Forms.Panel();
            pictureBoxElement = new System.Windows.Forms.PictureBox();
            labelCardHeader = new System.Windows.Forms.Label();
            colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNationalId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNextDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPrinted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            panelTopBar.SuspendLayout();
            flowLayoutPanelActions.SuspendLayout();
            panelStatusPill.SuspendLayout();
            panelKpi.SuspendLayout();
            panelKpiPrinted.SuspendLayout();
            panelKpiWanted.SuspendLayout();
            panelKpiTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            panelGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewToday).BeginInit();
            panelSearch.SuspendLayout();
            panelCard.SuspendLayout();
            panelCardContent.SuspendLayout();
            panelNextDate.SuspendLayout();
            panelWantedAlert.SuspendLayout();
            panelPhotoFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxElement).BeginInit();
            SuspendLayout();
            // 
            // panelTopBar
            // 
            panelTopBar.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            panelTopBar.Controls.Add(flowLayoutPanelActions);
            panelTopBar.Controls.Add(panelStatusPill);
            panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            panelTopBar.Location = new System.Drawing.Point(0, 0);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            panelTopBar.Size = new System.Drawing.Size(1720, 79);
            panelTopBar.TabIndex = 0;
            // 
            // flowLayoutPanelActions
            // 
            flowLayoutPanelActions.AutoScroll = true;
            flowLayoutPanelActions.Controls.Add(buttonConnect);
            flowLayoutPanelActions.Controls.Add(buttonSyncLogs);
            flowLayoutPanelActions.Controls.Add(buttonSyncTime);
            flowLayoutPanelActions.Controls.Add(buttonManualAttendance);
            flowLayoutPanelActions.Controls.Add(buttonManageWanted);
            flowLayoutPanelActions.Controls.Add(buttonEnrollFingerprint);
            flowLayoutPanelActions.Controls.Add(buttonDeviceSettings);
            flowLayoutPanelActions.Controls.Add(buttonPrintSettings);
            flowLayoutPanelActions.Controls.Add(buttonOpenKiosk);
            flowLayoutPanelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanelActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanelActions.Location = new System.Drawing.Point(10, 8);
            flowLayoutPanelActions.Name = "flowLayoutPanelActions";
            flowLayoutPanelActions.Size = new System.Drawing.Size(1455, 63);
            flowLayoutPanelActions.TabIndex = 1;
            flowLayoutPanelActions.WrapContents = false;
            // 
            // buttonConnect
            // 
            buttonConnect.BackColor = System.Drawing.Color.FromArgb(16, 185, 129);
            buttonConnect.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonConnect.FlatAppearance.BorderSize = 0;
            buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonConnect.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonConnect.ForeColor = System.Drawing.Color.White;
            buttonConnect.Location = new System.Drawing.Point(3, 2);
            buttonConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new System.Drawing.Size(188, 38);
            buttonConnect.TabIndex = 0;
            buttonConnect.Text = "⚡ اتصال بجهاز البصمة";
            buttonConnect.UseVisualStyleBackColor = false;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonSyncLogs
            // 
            buttonSyncLogs.BackColor = System.Drawing.Color.FromArgb(217, 119, 6);
            buttonSyncLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonSyncLogs.FlatAppearance.BorderSize = 0;
            buttonSyncLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSyncLogs.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonSyncLogs.ForeColor = System.Drawing.Color.White;
            buttonSyncLogs.Location = new System.Drawing.Point(197, 2);
            buttonSyncLogs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonSyncLogs.Name = "buttonSyncLogs";
            buttonSyncLogs.Size = new System.Drawing.Size(206, 38);
            buttonSyncLogs.TabIndex = 1;
            buttonSyncLogs.Text = "🔄 سحب حركات الحضور";
            buttonSyncLogs.UseVisualStyleBackColor = false;
            buttonSyncLogs.Click += buttonSyncLogs_Click;
            // 
            // buttonSyncTime
            // 
            buttonSyncTime.BackColor = System.Drawing.Color.FromArgb(2, 132, 199);
            buttonSyncTime.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonSyncTime.FlatAppearance.BorderSize = 0;
            buttonSyncTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSyncTime.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonSyncTime.ForeColor = System.Drawing.Color.White;
            buttonSyncTime.Location = new System.Drawing.Point(409, 2);
            buttonSyncTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonSyncTime.Name = "buttonSyncTime";
            buttonSyncTime.Size = new System.Drawing.Size(185, 38);
            buttonSyncTime.TabIndex = 8;
            buttonSyncTime.Text = "⏱️ مزامنة وقت الجهاز";
            buttonSyncTime.UseVisualStyleBackColor = false;
            // 
            // buttonManualAttendance
            // 
            buttonManualAttendance.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            buttonManualAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonManualAttendance.FlatAppearance.BorderSize = 0;
            buttonManualAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonManualAttendance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonManualAttendance.ForeColor = System.Drawing.Color.White;
            buttonManualAttendance.Location = new System.Drawing.Point(600, 2);
            buttonManualAttendance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonManualAttendance.Name = "buttonManualAttendance";
            buttonManualAttendance.Size = new System.Drawing.Size(202, 38);
            buttonManualAttendance.TabIndex = 2;
            buttonManualAttendance.Text = "✍️ تسجيل حضور يدوي";
            buttonManualAttendance.UseVisualStyleBackColor = false;
            buttonManualAttendance.Click += buttonManualAttendance_Click;
            // 
            // buttonManageWanted
            // 
            buttonManageWanted.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            buttonManageWanted.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonManageWanted.FlatAppearance.BorderSize = 0;
            buttonManageWanted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonManageWanted.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonManageWanted.ForeColor = System.Drawing.Color.White;
            buttonManageWanted.Location = new System.Drawing.Point(808, 2);
            buttonManageWanted.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonManageWanted.Name = "buttonManageWanted";
            buttonManageWanted.Size = new System.Drawing.Size(218, 38);
            buttonManageWanted.TabIndex = 3;
            buttonManageWanted.Text = "🚨 سجل الاشخاص المطلوبة";
            buttonManageWanted.UseVisualStyleBackColor = false;
            buttonManageWanted.Click += buttonManageWanted_Click;
            // 
            // buttonEnrollFingerprint
            // 
            buttonEnrollFingerprint.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            buttonEnrollFingerprint.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonEnrollFingerprint.FlatAppearance.BorderSize = 0;
            buttonEnrollFingerprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonEnrollFingerprint.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonEnrollFingerprint.ForeColor = System.Drawing.Color.White;
            buttonEnrollFingerprint.Location = new System.Drawing.Point(1032, 2);
            buttonEnrollFingerprint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonEnrollFingerprint.Name = "buttonEnrollFingerprint";
            buttonEnrollFingerprint.Size = new System.Drawing.Size(200, 38);
            buttonEnrollFingerprint.TabIndex = 4;
            buttonEnrollFingerprint.Text = "👆 إدارة وتسجيل البصمات";
            buttonEnrollFingerprint.UseVisualStyleBackColor = false;
            buttonEnrollFingerprint.Click += buttonEnrollFingerprint_Click;
            // 
            // buttonDeviceSettings
            // 
            buttonDeviceSettings.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            buttonDeviceSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonDeviceSettings.FlatAppearance.BorderSize = 0;
            buttonDeviceSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonDeviceSettings.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonDeviceSettings.ForeColor = System.Drawing.Color.White;
            buttonDeviceSettings.Location = new System.Drawing.Point(1238, 2);
            buttonDeviceSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonDeviceSettings.Name = "buttonDeviceSettings";
            buttonDeviceSettings.Size = new System.Drawing.Size(202, 38);
            buttonDeviceSettings.TabIndex = 5;
            buttonDeviceSettings.Text = "⚙️ إعدادات أجهزة البصمة";
            buttonDeviceSettings.UseVisualStyleBackColor = false;
            buttonDeviceSettings.Click += buttonDeviceSettings_Click;
            // 
            // buttonPrintSettings
            // 
            buttonPrintSettings.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            buttonPrintSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonPrintSettings.FlatAppearance.BorderSize = 0;
            buttonPrintSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonPrintSettings.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonPrintSettings.ForeColor = System.Drawing.Color.White;
            buttonPrintSettings.Location = new System.Drawing.Point(1446, 2);
            buttonPrintSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonPrintSettings.Name = "buttonPrintSettings";
            buttonPrintSettings.Size = new System.Drawing.Size(229, 38);
            buttonPrintSettings.TabIndex = 6;
            buttonPrintSettings.Text = "🖨️ إعدادات الطباعة والشاشة";
            buttonPrintSettings.UseVisualStyleBackColor = false;
            buttonPrintSettings.Click += buttonPrintSettings_Click;
            // 
            // buttonOpenKiosk
            // 
            buttonOpenKiosk.BackColor = System.Drawing.Color.FromArgb(14, 116, 144);
            buttonOpenKiosk.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonOpenKiosk.FlatAppearance.BorderSize = 0;
            buttonOpenKiosk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonOpenKiosk.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            buttonOpenKiosk.ForeColor = System.Drawing.Color.White;
            buttonOpenKiosk.Location = new System.Drawing.Point(1681, 2);
            buttonOpenKiosk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonOpenKiosk.Name = "buttonOpenKiosk";
            buttonOpenKiosk.Size = new System.Drawing.Size(188, 38);
            buttonOpenKiosk.TabIndex = 7;
            buttonOpenKiosk.Text = "🖥️ فتح شاشة العرض";
            buttonOpenKiosk.UseVisualStyleBackColor = false;
            buttonOpenKiosk.Click += buttonOpenKiosk_Click;
            // 
            // panelStatusPill
            // 
            panelStatusPill.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            panelStatusPill.Controls.Add(labelDeviceStatus);
            panelStatusPill.Controls.Add(lblStatusDot);
            panelStatusPill.Dock = System.Windows.Forms.DockStyle.Right;
            panelStatusPill.Location = new System.Drawing.Point(1465, 8);
            panelStatusPill.Name = "panelStatusPill";
            panelStatusPill.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            panelStatusPill.Size = new System.Drawing.Size(245, 63);
            panelStatusPill.TabIndex = 0;
            // 
            // labelDeviceStatus
            // 
            labelDeviceStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            labelDeviceStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelDeviceStatus.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            labelDeviceStatus.Location = new System.Drawing.Point(8, 4);
            labelDeviceStatus.Name = "labelDeviceStatus";
            labelDeviceStatus.Size = new System.Drawing.Size(207, 55);
            labelDeviceStatus.TabIndex = 1;
            labelDeviceStatus.Text = "جهاز البصمة: غير متصل";
            labelDeviceStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusDot
            // 
            lblStatusDot.Dock = System.Windows.Forms.DockStyle.Right;
            lblStatusDot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblStatusDot.ForeColor = System.Drawing.Color.FromArgb(239, 68, 68);
            lblStatusDot.Location = new System.Drawing.Point(215, 4);
            lblStatusDot.Name = "lblStatusDot";
            lblStatusDot.Size = new System.Drawing.Size(22, 55);
            lblStatusDot.TabIndex = 0;
            lblStatusDot.Text = "●";
            lblStatusDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelKpi
            // 
            panelKpi.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            panelKpi.Controls.Add(panelKpiPrinted);
            panelKpi.Controls.Add(panelKpiWanted);
            panelKpi.Controls.Add(panelKpiTotal);
            panelKpi.Dock = System.Windows.Forms.DockStyle.Top;
            panelKpi.Location = new System.Drawing.Point(0, 79);
            panelKpi.Name = "panelKpi";
            panelKpi.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            panelKpi.Size = new System.Drawing.Size(1720, 68);
            panelKpi.TabIndex = 1;
            // 
            // panelKpiPrinted
            // 
            panelKpiPrinted.BackColor = System.Drawing.Color.White;
            panelKpiPrinted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelKpiPrinted.Controls.Add(labelKpiPrintedVal);
            panelKpiPrinted.Controls.Add(labelKpiPrintedTitle);
            panelKpiPrinted.Location = new System.Drawing.Point(12, 8);
            panelKpiPrinted.Name = "panelKpiPrinted";
            panelKpiPrinted.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            panelKpiPrinted.Size = new System.Drawing.Size(220, 50);
            panelKpiPrinted.TabIndex = 2;
            // 
            // labelKpiPrintedVal
            // 
            labelKpiPrintedVal.Dock = System.Windows.Forms.DockStyle.Left;
            labelKpiPrintedVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            labelKpiPrintedVal.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            labelKpiPrintedVal.Location = new System.Drawing.Point(10, 4);
            labelKpiPrintedVal.Name = "labelKpiPrintedVal";
            labelKpiPrintedVal.Size = new System.Drawing.Size(65, 40);
            labelKpiPrintedVal.TabIndex = 1;
            labelKpiPrintedVal.Text = "0";
            labelKpiPrintedVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelKpiPrintedTitle
            // 
            labelKpiPrintedTitle.Dock = System.Windows.Forms.DockStyle.Right;
            labelKpiPrintedTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKpiPrintedTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            labelKpiPrintedTitle.Location = new System.Drawing.Point(85, 4);
            labelKpiPrintedTitle.Name = "labelKpiPrintedTitle";
            labelKpiPrintedTitle.Size = new System.Drawing.Size(123, 40);
            labelKpiPrintedTitle.TabIndex = 0;
            labelKpiPrintedTitle.Text = "إشعارات مطبوعة";
            labelKpiPrintedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelKpiWanted
            // 
            panelKpiWanted.BackColor = System.Drawing.Color.White;
            panelKpiWanted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelKpiWanted.Controls.Add(labelKpiWantedVal);
            panelKpiWanted.Controls.Add(labelKpiWantedTitle);
            panelKpiWanted.Location = new System.Drawing.Point(245, 8);
            panelKpiWanted.Name = "panelKpiWanted";
            panelKpiWanted.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            panelKpiWanted.Size = new System.Drawing.Size(220, 50);
            panelKpiWanted.TabIndex = 1;
            // 
            // labelKpiWantedVal
            // 
            labelKpiWantedVal.Dock = System.Windows.Forms.DockStyle.Left;
            labelKpiWantedVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            labelKpiWantedVal.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            labelKpiWantedVal.Location = new System.Drawing.Point(10, 4);
            labelKpiWantedVal.Name = "labelKpiWantedVal";
            labelKpiWantedVal.Size = new System.Drawing.Size(65, 40);
            labelKpiWantedVal.TabIndex = 1;
            labelKpiWantedVal.Text = "0";
            labelKpiWantedVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelKpiWantedTitle
            // 
            labelKpiWantedTitle.Dock = System.Windows.Forms.DockStyle.Right;
            labelKpiWantedTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKpiWantedTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            labelKpiWantedTitle.Location = new System.Drawing.Point(85, 4);
            labelKpiWantedTitle.Name = "labelKpiWantedTitle";
            labelKpiWantedTitle.Size = new System.Drawing.Size(123, 40);
            labelKpiWantedTitle.TabIndex = 0;
            labelKpiWantedTitle.Text = "مطلوبين مسجلين";
            labelKpiWantedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelKpiTotal
            // 
            panelKpiTotal.BackColor = System.Drawing.Color.White;
            panelKpiTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelKpiTotal.Controls.Add(labelKpiTotalVal);
            panelKpiTotal.Controls.Add(labelKpiTotalTitle);
            panelKpiTotal.Dock = System.Windows.Forms.DockStyle.Right;
            panelKpiTotal.Location = new System.Drawing.Point(1478, 8);
            panelKpiTotal.Name = "panelKpiTotal";
            panelKpiTotal.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            panelKpiTotal.Size = new System.Drawing.Size(230, 52);
            panelKpiTotal.TabIndex = 0;
            // 
            // labelKpiTotalVal
            // 
            labelKpiTotalVal.Dock = System.Windows.Forms.DockStyle.Left;
            labelKpiTotalVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            labelKpiTotalVal.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            labelKpiTotalVal.Location = new System.Drawing.Point(10, 4);
            labelKpiTotalVal.Name = "labelKpiTotalVal";
            labelKpiTotalVal.Size = new System.Drawing.Size(75, 42);
            labelKpiTotalVal.TabIndex = 1;
            labelKpiTotalVal.Text = "0";
            labelKpiTotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelKpiTotalTitle
            // 
            labelKpiTotalTitle.Dock = System.Windows.Forms.DockStyle.Right;
            labelKpiTotalTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            labelKpiTotalTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            labelKpiTotalTitle.Location = new System.Drawing.Point(90, 4);
            labelKpiTotalTitle.Name = "labelKpiTotalTitle";
            labelKpiTotalTitle.Size = new System.Drawing.Size(128, 42);
            labelKpiTotalTitle.TabIndex = 0;
            labelKpiTotalTitle.Text = "إجمالي حضور اليوم";
            labelKpiTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainerMain.Location = new System.Drawing.Point(0, 147);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(panelGridContainer);
            splitContainerMain.Panel1.Controls.Add(panelSearch);
            splitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(12, 10, 6, 12);
            splitContainerMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(panelCard);
            splitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(6, 10, 12, 12);
            splitContainerMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            splitContainerMain.Size = new System.Drawing.Size(1720, 573);
            splitContainerMain.SplitterDistance = 1320;
            splitContainerMain.TabIndex = 2;
            // 
            // panelGridContainer
            // 
            panelGridContainer.BackColor = System.Drawing.Color.White;
            panelGridContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelGridContainer.Controls.Add(dataGridViewToday);
            panelGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelGridContainer.Location = new System.Drawing.Point(12, 58);
            panelGridContainer.Name = "panelGridContainer";
            panelGridContainer.Size = new System.Drawing.Size(1302, 503);
            panelGridContainer.TabIndex = 2;
            // 
            // dataGridViewToday
            // 
            dataGridViewToday.AllowUserToAddRows = false;
            dataGridViewToday.AllowUserToDeleteRows = false;
            dataGridViewToday.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewToday.BackgroundColor = System.Drawing.Color.White;
            dataGridViewToday.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewToday.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewToday.ColumnHeadersHeight = 38;
            dataGridViewToday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewToday.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colId, colTime, colName, colNationalId, colStatus, colNextDate, colPrinted });
            dataGridViewToday.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewToday.EnableHeadersVisualStyles = false;
            dataGridViewToday.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            dataGridViewToday.Location = new System.Drawing.Point(0, 0);
            dataGridViewToday.MultiSelect = false;
            dataGridViewToday.Name = "dataGridViewToday";
            dataGridViewToday.ReadOnly = true;
            dataGridViewToday.RowHeadersVisible = false;
            dataGridViewToday.RowHeadersWidth = 51;
            dataGridViewToday.RowTemplate.Height = 34;
            dataGridViewToday.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewToday.Size = new System.Drawing.Size(1300, 501);
            dataGridViewToday.TabIndex = 0;
            dataGridViewToday.SelectionChanged += dataGridViewToday_SelectionChanged;
            // 
            // panelSearch
            // 
            panelSearch.BackColor = System.Drawing.Color.White;
            panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelSearch.Controls.Add(comboBoxFilterStatus);
            panelSearch.Controls.Add(buttonRefreshGrid);
            panelSearch.Controls.Add(textBoxSearch);
            panelSearch.Controls.Add(labelSearch);
            panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            panelSearch.Location = new System.Drawing.Point(12, 10);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new System.Windows.Forms.Padding(8);
            panelSearch.Size = new System.Drawing.Size(1302, 48);
            panelSearch.TabIndex = 1;
            // 
            // comboBoxFilterStatus
            // 
            comboBoxFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            comboBoxFilterStatus.FormattingEnabled = true;
            comboBoxFilterStatus.Items.AddRange(new object[] { "عرض الكل", "سليم فقط", "مطلوب أمنياً فقط" });
            comboBoxFilterStatus.Location = new System.Drawing.Point(108, 8);
            comboBoxFilterStatus.Name = "comboBoxFilterStatus";
            comboBoxFilterStatus.Size = new System.Drawing.Size(140, 29);
            comboBoxFilterStatus.TabIndex = 3;
            comboBoxFilterStatus.SelectedIndexChanged += comboBoxFilterStatus_SelectedIndexChanged;
            // 
            // buttonRefreshGrid
            // 
            buttonRefreshGrid.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            buttonRefreshGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonRefreshGrid.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            buttonRefreshGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonRefreshGrid.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            buttonRefreshGrid.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            buttonRefreshGrid.Location = new System.Drawing.Point(8, 8);
            buttonRefreshGrid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            buttonRefreshGrid.Name = "buttonRefreshGrid";
            buttonRefreshGrid.Size = new System.Drawing.Size(100, 30);
            buttonRefreshGrid.TabIndex = 2;
            buttonRefreshGrid.Text = "🔄 تحديث";
            buttonRefreshGrid.UseVisualStyleBackColor = false;
            buttonRefreshGrid.Click += buttonRefreshGrid_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBoxSearch.Location = new System.Drawing.Point(260, 10);
            textBoxSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.PlaceholderText = "اكتب اسم الشخص، الرقم القومي، أو الكود للتصفية السريعة...";
            textBoxSearch.Size = new System.Drawing.Size(360, 30);
            textBoxSearch.TabIndex = 1;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // labelSearch
            // 
            labelSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelSearch.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelSearch.Location = new System.Drawing.Point(1188, 8);
            labelSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new System.Drawing.Size(104, 30);
            labelSearch.TabIndex = 0;
            labelSearch.Text = "🔍 بحث سريع:";
            labelSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelCard
            // 
            panelCard.BackColor = System.Drawing.Color.White;
            panelCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelCard.Controls.Add(panelCardContent);
            panelCard.Controls.Add(labelCardHeader);
            panelCard.Dock = System.Windows.Forms.DockStyle.Fill;
            panelCard.Location = new System.Drawing.Point(6, 10);
            panelCard.Name = "panelCard";
            panelCard.Size = new System.Drawing.Size(378, 551);
            panelCard.TabIndex = 0;
            // 
            // panelCardContent
            // 
            panelCardContent.AutoScroll = true;
            panelCardContent.Controls.Add(buttonReprint);
            panelCardContent.Controls.Add(panelNextDate);
            panelCardContent.Controls.Add(panelWantedAlert);
            panelCardContent.Controls.Add(labelCardJob);
            panelCardContent.Controls.Add(labelCardNationalId);
            panelCardContent.Controls.Add(labelCardName);
            panelCardContent.Controls.Add(panelPhotoFrame);
            panelCardContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelCardContent.Location = new System.Drawing.Point(0, 44);
            panelCardContent.Name = "panelCardContent";
            panelCardContent.Padding = new System.Windows.Forms.Padding(16);
            panelCardContent.Size = new System.Drawing.Size(376, 505);
            panelCardContent.TabIndex = 1;
            // 
            // buttonReprint
            // 
            buttonReprint.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            buttonReprint.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonReprint.Dock = System.Windows.Forms.DockStyle.Bottom;
            buttonReprint.FlatAppearance.BorderSize = 0;
            buttonReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonReprint.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            buttonReprint.ForeColor = System.Drawing.Color.White;
            buttonReprint.Location = new System.Drawing.Point(16, 445);
            buttonReprint.Name = "buttonReprint";
            buttonReprint.Size = new System.Drawing.Size(344, 44);
            buttonReprint.TabIndex = 6;
            buttonReprint.Text = "🖨️ طباعة إشعار الحضور الآن";
            buttonReprint.UseVisualStyleBackColor = false;
            buttonReprint.Click += buttonReprint_Click;
            // 
            // panelNextDate
            // 
            panelNextDate.BackColor = System.Drawing.Color.FromArgb(240, 253, 244);
            panelNextDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelNextDate.Controls.Add(labelNextDateValue);
            panelNextDate.Controls.Add(labelNextDateTitle);
            panelNextDate.Dock = System.Windows.Forms.DockStyle.Top;
            panelNextDate.Location = new System.Drawing.Point(16, 190);
            panelNextDate.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            panelNextDate.Name = "panelNextDate";
            panelNextDate.Padding = new System.Windows.Forms.Padding(6);
            panelNextDate.Size = new System.Drawing.Size(344, 85);
            panelNextDate.TabIndex = 5;
            // 
            // labelNextDateValue
            // 
            labelNextDateValue.Dock = System.Windows.Forms.DockStyle.Fill;
            labelNextDateValue.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            labelNextDateValue.ForeColor = System.Drawing.Color.FromArgb(6, 95, 70);
            labelNextDateValue.Location = new System.Drawing.Point(6, 32);
            labelNextDateValue.Name = "labelNextDateValue";
            labelNextDateValue.Size = new System.Drawing.Size(330, 45);
            labelNextDateValue.TabIndex = 1;
            labelNextDateValue.Text = "-";
            labelNextDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNextDateTitle
            // 
            labelNextDateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelNextDateTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            labelNextDateTitle.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            labelNextDateTitle.Location = new System.Drawing.Point(6, 6);
            labelNextDateTitle.Name = "labelNextDateTitle";
            labelNextDateTitle.Size = new System.Drawing.Size(330, 26);
            labelNextDateTitle.TabIndex = 0;
            labelNextDateTitle.Text = "📅 موعد المتابعة القادم";
            labelNextDateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelWantedAlert
            // 
            panelWantedAlert.BackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            panelWantedAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelWantedAlert.Controls.Add(labelWantedReason);
            panelWantedAlert.Controls.Add(labelWantedTitle);
            panelWantedAlert.Dock = System.Windows.Forms.DockStyle.Top;
            panelWantedAlert.Location = new System.Drawing.Point(16, 105);
            panelWantedAlert.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            panelWantedAlert.Name = "panelWantedAlert";
            panelWantedAlert.Padding = new System.Windows.Forms.Padding(6);
            panelWantedAlert.Size = new System.Drawing.Size(344, 85);
            panelWantedAlert.TabIndex = 4;
            panelWantedAlert.Visible = false;
            // 
            // labelWantedReason
            // 
            labelWantedReason.Dock = System.Windows.Forms.DockStyle.Fill;
            labelWantedReason.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelWantedReason.ForeColor = System.Drawing.Color.FromArgb(153, 27, 27);
            labelWantedReason.Location = new System.Drawing.Point(6, 38);
            labelWantedReason.Name = "labelWantedReason";
            labelWantedReason.Size = new System.Drawing.Size(330, 39);
            labelWantedReason.TabIndex = 1;
            labelWantedReason.Text = "السبب: -";
            labelWantedReason.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWantedTitle
            // 
            labelWantedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelWantedTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            labelWantedTitle.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            labelWantedTitle.Location = new System.Drawing.Point(6, 6);
            labelWantedTitle.Name = "labelWantedTitle";
            labelWantedTitle.Size = new System.Drawing.Size(330, 32);
            labelWantedTitle.TabIndex = 0;
            labelWantedTitle.Text = "⚠️ تـنـبـيـه: شخص مـطـلـوب ⚠️";
            labelWantedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCardJob
            // 
            labelCardJob.Dock = System.Windows.Forms.DockStyle.Top;
            labelCardJob.Font = new System.Drawing.Font("Segoe UI", 10F);
            labelCardJob.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            labelCardJob.Location = new System.Drawing.Point(16, 81);
            labelCardJob.Name = "labelCardJob";
            labelCardJob.Size = new System.Drawing.Size(344, 24);
            labelCardJob.TabIndex = 3;
            labelCardJob.Text = "المهنة: -";
            labelCardJob.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCardNationalId
            // 
            labelCardNationalId.Dock = System.Windows.Forms.DockStyle.Top;
            labelCardNationalId.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            labelCardNationalId.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelCardNationalId.Location = new System.Drawing.Point(16, 56);
            labelCardNationalId.Name = "labelCardNationalId";
            labelCardNationalId.Size = new System.Drawing.Size(344, 25);
            labelCardNationalId.TabIndex = 2;
            labelCardNationalId.Text = "الرقم القومي: -";
            labelCardNationalId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCardName
            // 
            labelCardName.Dock = System.Windows.Forms.DockStyle.Top;
            labelCardName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            labelCardName.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            labelCardName.Location = new System.Drawing.Point(16, 16);
            labelCardName.Name = "labelCardName";
            labelCardName.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            labelCardName.Size = new System.Drawing.Size(344, 40);
            labelCardName.TabIndex = 1;
            labelCardName.Text = "في انتظار تسجيل بصمة...";
            labelCardName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPhotoFrame
            // 
            panelPhotoFrame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            panelPhotoFrame.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            panelPhotoFrame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelPhotoFrame.Controls.Add(pictureBoxElement);
            panelPhotoFrame.Location = new System.Drawing.Point(113, 14);
            panelPhotoFrame.Name = "panelPhotoFrame";
            panelPhotoFrame.Padding = new System.Windows.Forms.Padding(4);
            panelPhotoFrame.Size = new System.Drawing.Size(150, 150);
            panelPhotoFrame.TabIndex = 0;
            // 
            // pictureBoxElement
            // 
            pictureBoxElement.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            pictureBoxElement.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBoxElement.Location = new System.Drawing.Point(4, 4);
            pictureBoxElement.Name = "pictureBoxElement";
            pictureBoxElement.Size = new System.Drawing.Size(140, 140);
            pictureBoxElement.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxElement.TabIndex = 0;
            pictureBoxElement.TabStop = false;
            // 
            // labelCardHeader
            // 
            labelCardHeader.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            labelCardHeader.Dock = System.Windows.Forms.DockStyle.Top;
            labelCardHeader.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            labelCardHeader.ForeColor = System.Drawing.Color.White;
            labelCardHeader.Location = new System.Drawing.Point(0, 0);
            labelCardHeader.Name = "labelCardHeader";
            labelCardHeader.Size = new System.Drawing.Size(376, 44);
            labelCardHeader.TabIndex = 0;
            labelCardHeader.Text = "بطاقة حضور الشخص اللحظية";
            labelCardHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colId
            // 
            colId.FillWeight = 22F;
            colId.HeaderText = "م";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colTime
            // 
            colTime.FillWeight = 42F;
            colTime.HeaderText = "وقت الحضور";
            colTime.MinimumWidth = 6;
            colTime.Name = "colTime";
            colTime.ReadOnly = true;
            // 
            // colName
            // 
            colName.HeaderText = "الاسم";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colNationalId
            // 
            colNationalId.FillWeight = 65F;
            colNationalId.HeaderText = "الرقم القومي";
            colNationalId.MinimumWidth = 6;
            colNationalId.Name = "colNationalId";
            colNationalId.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.FillWeight = 45F;
            colStatus.HeaderText = "الحالة";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // colNextDate
            // 
            colNextDate.FillWeight = 55F;
            colNextDate.HeaderText = "المتابعة القادمة";
            colNextDate.MinimumWidth = 6;
            colNextDate.Name = "colNextDate";
            colNextDate.ReadOnly = true;
            // 
            // colPrinted
            // 
            colPrinted.FillWeight = 32F;
            colPrinted.HeaderText = "الطباعة";
            colPrinted.MinimumWidth = 6;
            colPrinted.Name = "colPrinted";
            colPrinted.ReadOnly = true;
            // 
            // FingerprintAttendanceUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            Controls.Add(splitContainerMain);
            Controls.Add(panelKpi);
            Controls.Add(panelTopBar);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            Name = "FingerprintAttendanceUserControl";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Size = new System.Drawing.Size(1720, 720);
            Load += FingerprintAttendanceUserControl_Load;
            panelTopBar.ResumeLayout(false);
            flowLayoutPanelActions.ResumeLayout(false);
            panelStatusPill.ResumeLayout(false);
            panelKpi.ResumeLayout(false);
            panelKpiPrinted.ResumeLayout(false);
            panelKpiWanted.ResumeLayout(false);
            panelKpiTotal.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            panelGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewToday).EndInit();
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelCard.ResumeLayout(false);
            panelCardContent.ResumeLayout(false);
            panelNextDate.ResumeLayout(false);
            panelWantedAlert.ResumeLayout(false);
            panelPhotoFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxElement).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelActions;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonSyncLogs;
        private System.Windows.Forms.Button buttonSyncTime;
        private System.Windows.Forms.Button buttonManualAttendance;
        private System.Windows.Forms.Button buttonManageWanted;
        private System.Windows.Forms.Button buttonEnrollFingerprint;
        private System.Windows.Forms.Button buttonDeviceSettings;
        private System.Windows.Forms.Button buttonPrintSettings;
        private System.Windows.Forms.Button buttonOpenKiosk;
        private System.Windows.Forms.Panel panelStatusPill;
        private System.Windows.Forms.Label labelDeviceStatus;
        private System.Windows.Forms.Label lblStatusDot;
        private System.Windows.Forms.Panel panelKpi;
        private System.Windows.Forms.Panel panelKpiTotal;
        private System.Windows.Forms.Label labelKpiTotalVal;
        private System.Windows.Forms.Label labelKpiTotalTitle;
        private System.Windows.Forms.Panel panelKpiWanted;
        private System.Windows.Forms.Label labelKpiWantedVal;
        private System.Windows.Forms.Label labelKpiWantedTitle;
        private System.Windows.Forms.Panel panelKpiPrinted;
        private System.Windows.Forms.Label labelKpiPrintedVal;
        private System.Windows.Forms.Label labelKpiPrintedTitle;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelGridContainer;
        private System.Windows.Forms.DataGridView dataGridViewToday;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ComboBox comboBoxFilterStatus;
        private System.Windows.Forms.Button buttonRefreshGrid;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label labelCardHeader;
        private System.Windows.Forms.Panel panelCardContent;
        private System.Windows.Forms.Panel panelPhotoFrame;
        private System.Windows.Forms.PictureBox pictureBoxElement;
        private System.Windows.Forms.Label labelCardName;
        private System.Windows.Forms.Label labelCardNationalId;
        private System.Windows.Forms.Label labelCardJob;
        private System.Windows.Forms.Panel panelWantedAlert;
        private System.Windows.Forms.Label labelWantedTitle;
        private System.Windows.Forms.Label labelWantedReason;
        private System.Windows.Forms.Panel panelNextDate;
        private System.Windows.Forms.Label labelNextDateTitle;
        private System.Windows.Forms.Label labelNextDateValue;
        private System.Windows.Forms.Button buttonReprint;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNationalId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrinted;
    }
}
