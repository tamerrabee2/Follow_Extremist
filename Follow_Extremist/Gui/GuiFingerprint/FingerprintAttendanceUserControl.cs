using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Code.Services;
using Follow_Extremist.Core;
using Follow_Extremist.Data;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public partial class FingerprintAttendanceUserControl : UserControl
    {
        private static FingerprintAttendanceUserControl _instance;

        private readonly IDataHelper<FingerprintDevice> dataHelperDevice;
        private readonly IDataHelper<AttendanceLog> dataHelperAttendanceLog;
        private readonly IDataHelper<ElementInfo> dataHelperElement;
        private readonly IDataHelper<ElementWantedStatus> dataHelperWanted;
        private readonly IDataHelper<PrintSetting> dataHelperPrint;

        private readonly ZKDeviceService zkService;
        private readonly AttendanceProcessor attendanceProcessor;
        private readonly ThermalPrintService printService;

        private List<AttendanceLog> todayLogs = new();
        private AttendanceLog currentSelectedLog;
        private ElementInfo currentSelectedElement;
        private ElementWantedStatus currentSelectedWanted;
        private int cardDurationSeconds = 12;
        private System.Windows.Forms.Timer timerCardAutoReset;

        private Panel panelPhotoContainer;
        private Panel panelPunchBadge;
        private Label labelPunchStatus;

        public static FingerprintAttendanceUserControl Instance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new FingerprintAttendanceUserControl();
            }
            return _instance;
        }

        public FingerprintAttendanceUserControl()
        {
            InitializeComponent();

            dataHelperDevice = (IDataHelper<FingerprintDevice>)ConfigurationObjectManager.GetObject("FingerprintDevice");
            dataHelperAttendanceLog = (IDataHelper<AttendanceLog>)ConfigurationObjectManager.GetObject("AttendanceLog");
            dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperWanted = (IDataHelper<ElementWantedStatus>)ConfigurationObjectManager.GetObject("ElementWantedStatus");
            dataHelperPrint = (IDataHelper<PrintSetting>)ConfigurationObjectManager.GetObject("PrintSetting");

            zkService = new ZKDeviceService();
            attendanceProcessor = new AttendanceProcessor();
            printService = new ThermalPrintService();

            zkService.OnAttendanceReceived += ZkService_OnAttendanceReceived;
            zkService.OnStatusChanged += ZkService_OnStatusChanged;
            zkService.OnErrorOccurred += ZkService_OnErrorOccurred;
            attendanceProcessor.OnAttendanceProcessed += AttendanceProcessor_OnAttendanceProcessed;

            timerCardAutoReset = new System.Windows.Forms.Timer();
            timerCardAutoReset.Interval = 12000;
            timerCardAutoReset.Tick += TimerCardAutoReset_Tick;

            buttonSyncTime.Click += buttonSyncTime_Click;

            toolTipMain = new ToolTip
            {
                AutoPopDelay = 8000,
                InitialDelay = 350,
                ReshowDelay = 150,
                ShowAlways = true,
                UseAnimation = true,
                UseFading = true
            };

            SetupPagination();
            SetupDataGridStyling();
            SetupTopBarResponsiveLayout();
            SetupKpiBarLayout();
            SetupPersonCardLayout();
            this.Resize += (s, e) => AdjustSplitterDistance();
        }

        private GuiCommon.PaginationControl paginationControl;

        private void SetupPagination()
        {
            paginationControl = new GuiCommon.PaginationControl();
            paginationControl.Dock = DockStyle.Bottom;
            paginationControl.PageChanged += (s, e) => FilterAndBindGrid(resetPagination: false);
            panelGridContainer.Controls.Add(paginationControl);
            paginationControl.BringToFront();
        }

        private FlowLayoutPanel flowSearchToolbar;
        private CheckBox checkBoxShowPrintCol;
        private Button buttonPrintWantedReport;
        private Button buttonUnenrolledReport;
        private ToolTip toolTipMain;

        private void SetupDataGridStyling()
        {
            dataGridViewToday.RowHeadersVisible = false;
            dataGridViewToday.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewToday.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewToday.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewToday.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewToday.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dataGridViewToday.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewToday.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewToday.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewToday.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            comboBoxFilterStatus.SelectedIndex = 0;

            // ضبط أوزان وعروض الأعمدة بنسب FillWeight هندسية متناسقة
            colId.MinimumWidth = 45;
            colId.FillWeight = 5;
            colTime.MinimumWidth = 95;
            colTime.FillWeight = 11;
            colName.MinimumWidth = 200;
            colName.FillWeight = 26;
            colNationalId.MinimumWidth = 130;
            colNationalId.FillWeight = 16;
            colStatus.MinimumWidth = 100;
            colStatus.FillWeight = 14;
            colNextDate.MinimumWidth = 110;
            colNextDate.FillWeight = 14;
            colPrinted.MinimumWidth = 85;
            colPrinted.FillWeight = 10;

            // عمود الطباعة اختياري
            colPrinted.Visible = false;

            SetupSearchToolbarLayout();
        }

        private void SetupSearchToolbarLayout()
        {
            panelSearch.SuspendLayout();
            panelSearch.Height = 46;
            panelSearch.Padding = new Padding(6, 4, 6, 4);

            if (flowSearchToolbar == null)
            {
                flowSearchToolbar = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    RightToLeft = RightToLeft.Yes,
                    WrapContents = false,
                    AutoScroll = true,
                    BackColor = Color.White,
                    Padding = new Padding(0),
                    Margin = new Padding(0)
                };
                panelSearch.Controls.Clear();
                panelSearch.Controls.Add(flowSearchToolbar);
            }

            // 1. عنوان البحث
            labelSearch.Dock = DockStyle.None;
            labelSearch.Anchor = AnchorStyles.None;
            labelSearch.AutoSize = true;
            labelSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            labelSearch.ForeColor = Color.FromArgb(51, 65, 85);
            labelSearch.Text = "🔍 بحث سريع:";
            labelSearch.Margin = new Padding(4, 8, 4, 4);

            // 2. خانة البحث
            textBoxSearch.Dock = DockStyle.None;
            textBoxSearch.Anchor = AnchorStyles.None;
            textBoxSearch.Size = new Size(240, 28);
            textBoxSearch.Font = new Font("Segoe UI", 9.5F);
            textBoxSearch.PlaceholderText = "اسم الشخص أو الرقم القومي...";
            textBoxSearch.Margin = new Padding(4, 6, 6, 4);

            // 3. قائمة التصفية
            comboBoxFilterStatus.Dock = DockStyle.None;
            comboBoxFilterStatus.Anchor = AnchorStyles.None;
            comboBoxFilterStatus.Size = new Size(120, 28);
            comboBoxFilterStatus.Font = new Font("Segoe UI", 9F);
            comboBoxFilterStatus.Margin = new Padding(4, 6, 6, 4);

            // 4. زر التحديث
            buttonRefreshGrid.Dock = DockStyle.None;
            buttonRefreshGrid.Anchor = AnchorStyles.None;
            buttonRefreshGrid.Size = new Size(95, 30);
            buttonRefreshGrid.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonRefreshGrid.BackColor = Color.FromArgb(241, 245, 249);
            buttonRefreshGrid.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            buttonRefreshGrid.FlatStyle = FlatStyle.Flat;
            buttonRefreshGrid.ForeColor = Color.FromArgb(51, 65, 85);
            buttonRefreshGrid.Margin = new Padding(4, 5, 8, 4);

            // 5. فاصل بصري
            var lblSep = new Label
            {
                Text = "|",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(203, 213, 225),
                AutoSize = true,
                Margin = new Padding(2, 6, 6, 4)
            };

            // 6. خيار إظهار عمود الطباعة مع ضبط أبعاد واضحة تمنع قص النص
            if (checkBoxShowPrintCol == null)
            {
                checkBoxShowPrintCol = new CheckBox
                {
                    Text = "إظهار عمود الطباعة",
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    AutoSize = false,
                    Size = new Size(130, 28),
                    CheckAlign = ContentAlignment.MiddleRight,
                    TextAlign = ContentAlignment.MiddleRight,
                    Checked = false,
                    Margin = new Padding(4, 6, 8, 4),
                    Cursor = Cursors.Hand
                };
                checkBoxShowPrintCol.CheckedChanged += (s, e) =>
                {
                    colPrinted.Visible = checkBoxShowPrintCol.Checked;
                };
            }
            else
            {
                checkBoxShowPrintCol.Dock = DockStyle.None;
                checkBoxShowPrintCol.Anchor = AnchorStyles.None;
                checkBoxShowPrintCol.AutoSize = false;
                checkBoxShowPrintCol.Size = new Size(130, 28);
                checkBoxShowPrintCol.CheckAlign = ContentAlignment.MiddleRight;
                checkBoxShowPrintCol.TextAlign = ContentAlignment.MiddleRight;
                checkBoxShowPrintCol.Margin = new Padding(4, 6, 8, 4);
                checkBoxShowPrintCol.Font = new Font("Segoe UI", 9F);
            }

            // 7. زر تقرير المطلوبين أمنياً بجانب الجدول
            if (buttonPrintWantedReport == null)
            {
                buttonPrintWantedReport = new Button
                {
                    Text = "🚨 تقرير المطلوبين أمنياً",
                    BackColor = Color.FromArgb(185, 28, 28),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(180, 30),
                    Margin = new Padding(4, 5, 4, 4),
                    Cursor = Cursors.Hand
                };
                buttonPrintWantedReport.FlatAppearance.BorderSize = 0;
                buttonPrintWantedReport.Click += async (s, e) =>
                {
                    await WantedDailyReportService.ShowWantedReportAsync();
                };
            }
            else
            {
                buttonPrintWantedReport.Text = "🚨 تقرير المطلوبين أمنياً";
                buttonPrintWantedReport.Dock = DockStyle.None;
                buttonPrintWantedReport.Anchor = AnchorStyles.None;
                buttonPrintWantedReport.Size = new Size(180, 30);
                buttonPrintWantedReport.Margin = new Padding(4, 5, 4, 4);
            }

            // 8. زر تقرير الأشخاص غير المسجلين بجهاز البصمة
            if (buttonUnenrolledReport == null)
            {
                buttonUnenrolledReport = new Button
                {
                    Text = "⚠️ غير المسجلين بالبصمة",
                    BackColor = Color.FromArgb(217, 119, 6),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(185, 30),
                    Margin = new Padding(4, 5, 4, 4),
                    Cursor = Cursors.Hand
                };
                buttonUnenrolledReport.FlatAppearance.BorderSize = 0;
                buttonUnenrolledReport.Click += async (s, e) =>
                {
                    await UnenrolledFingerprintsReportService.ShowUnenrolledReportAsync();
                };
            }
            else
            {
                buttonUnenrolledReport.Text = "⚠️ غير المسجلين بالبصمة";
                buttonUnenrolledReport.Dock = DockStyle.None;
                buttonUnenrolledReport.Anchor = AnchorStyles.None;
                buttonUnenrolledReport.Size = new Size(185, 30);
                buttonUnenrolledReport.Margin = new Padding(4, 5, 4, 4);
            }

            // إضافة التلميحات لأدوات شريط البحث
            if (toolTipMain != null)
            {
                toolTipMain.SetToolTip(textBoxSearch, "ابحث فوراً في كشف اليوم بالاسم أو بالرقم القومي");
                toolTipMain.SetToolTip(comboBoxFilterStatus, "تصفية كشف اليوم حسب الحالة (الكل / سليم / مطلوب / يدوي)");
                toolTipMain.SetToolTip(buttonRefreshGrid, "تحديث كشف حضور اليوم وإعادة تحميل الحركات والإحصائيات فوراً");
                toolTipMain.SetToolTip(checkBoxShowPrintCol, "إظهار أو إخفاء عمود حالة طباعة إشعار الحضور الورقي");
                toolTipMain.SetToolTip(buttonPrintWantedReport, "معاينة وطباعة تقرير رسمي شامل بكافة الأشخاص المطلوبين أمنياً المسجلين في المنظومة");
                toolTipMain.SetToolTip(buttonUnenrolledReport, "معاينة وطباعة تقرير بكافة الأشخاص الذين لم يتم أخذ أو تسجيل بصمات أصابع لهم في النظام");
            }

            flowSearchToolbar.Controls.Clear();
            flowSearchToolbar.Controls.Add(labelSearch);
            flowSearchToolbar.Controls.Add(textBoxSearch);
            flowSearchToolbar.Controls.Add(comboBoxFilterStatus);
            flowSearchToolbar.Controls.Add(buttonRefreshGrid);
            flowSearchToolbar.Controls.Add(lblSep);
            flowSearchToolbar.Controls.Add(checkBoxShowPrintCol);
            flowSearchToolbar.Controls.Add(buttonPrintWantedReport);
            flowSearchToolbar.Controls.Add(buttonUnenrolledReport);

            panelSearch.ResumeLayout(true);
        }

        private void SetupTopBarResponsiveLayout()
        {
            panelTopBar.SuspendLayout();
            panelTopBar.Height = 54;
            panelTopBar.Padding = new Padding(8, 6, 8, 6);

            // مربع حالة جهاز البصمة
            panelStatusPill.Dock = DockStyle.Right;
            panelStatusPill.Width = 190;
            panelStatusPill.Height = 42;
            panelStatusPill.Padding = new Padding(6, 4, 6, 4);
            lblStatusDot.Dock = DockStyle.Right;
            lblStatusDot.Width = 20;
            labelDeviceStatus.Dock = DockStyle.Fill;
            labelDeviceStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelDeviceStatus.TextAlign = ContentAlignment.MiddleRight;

            if (toolTipMain != null)
            {
                toolTipMain.SetToolTip(panelStatusPill, "مؤشر حالة الاتصال اللحظية بجهاز البصمة الشبكي الرئيسي");
                toolTipMain.SetToolTip(labelDeviceStatus, "مؤشر حالة الاتصال اللحظية بجهاز البصمة الشبكي الرئيسي");
            }

            // شريط أزرار الإجراءات العلوي
            flowLayoutPanelActions.Dock = DockStyle.Fill;
            flowLayoutPanelActions.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelActions.RightToLeft = RightToLeft.Yes;
            flowLayoutPanelActions.WrapContents = false;
            flowLayoutPanelActions.AutoScroll = true;
            flowLayoutPanelActions.Padding = new Padding(0);

            // ضبط أبعاد ونصوص الأزرار بدقة تمنع التفاف النص وقصه مع تلميحات مفصلة
            ConfigureTopButton(buttonConnect, "⚡ اتصال بالبصمة", 160, Color.FromArgb(16, 185, 129), "الاتصال بجهاز البصمة الشبكي الرئيسي أو فصل الاتصال الحالي");
            ConfigureTopButton(buttonSyncLogs, "🔄 سحب الحركات", 170, Color.FromArgb(217, 119, 6), "سحب وتحميل كافة حركات وسجلات الحضور المخزنة على جهاز البصمة وتحديث كشف اليوم");
            ConfigureTopButton(buttonSyncTime, "⏱️ ضبط الوقت", 160, Color.FromArgb(2, 132, 199), "مزامنة وضبط ساعة وتاريخ جهاز البصمة مع توقيت جهاز الكمبيوتر الحالي");
            ConfigureTopButton(buttonManualAttendance, "✍️ حضور يدوي", 160, Color.FromArgb(5, 150, 105), "تسجيل حضور يدوي لشخص غير قادر على البصم مع تحديد موعد متابعته القادم");
            ConfigureTopButton(buttonEnrollFingerprint, "👆 تسجيل بصمة", 160, Color.FromArgb(37, 99, 235), "فتح نافذة إدارة وتسجيل بصمات أصابع الأشخاص ورفع القوالب الحيوية للجهاز");
            ConfigureTopButton(buttonManageWanted, "🚨 سجل المطلوبين", 170, Color.FromArgb(220, 38, 38), "إدارة ومتابعة سجل الأشخاص المطلوبين أمنياً وتحديث أسباب الطلب والملاحظات");
            ConfigureTopButton(buttonOpenKiosk, "🖥️ شاشة العرض", 160, Color.FromArgb(14, 116, 144), "فتح شاشة الكشك والعرض الخارجية المستقلة لعرض بيانات الحضور ومواعيد المتابعة");
            ConfigureTopButton(buttonDeviceSettings, "⚙️ إدارة الأجهزة", 170, Color.FromArgb(71, 85, 105), "إدارة وضبط إعدادات أجهزة البصمة وعناوين IP ومنافذ الاتصال");
            ConfigureTopButton(buttonPrintSettings, "🖨️ إعدادات الطباعة", 170, Color.FromArgb(71, 85, 105), "تخصيص إعدادات طابعة الإشعارات وتنسيق الترويسة وتوقيتات شاشة العرض");

            // إعادة ترتيب الأزرار هندسياً بسيمترية ثلاثية متناسقة وفق تدفق العمل:
            // 1. أجهزة واتصال (يمين) | 2. عمليات وبصمات (وسط) | 3. عرض وإعدادات (يسار)
            flowLayoutPanelActions.Controls.Clear();
            flowLayoutPanelActions.Controls.Add(buttonConnect);
            flowLayoutPanelActions.Controls.Add(buttonSyncLogs);
            flowLayoutPanelActions.Controls.Add(buttonSyncTime);
            flowLayoutPanelActions.Controls.Add(buttonManualAttendance);
            flowLayoutPanelActions.Controls.Add(buttonEnrollFingerprint);
            flowLayoutPanelActions.Controls.Add(buttonManageWanted);
            flowLayoutPanelActions.Controls.Add(buttonOpenKiosk);
            flowLayoutPanelActions.Controls.Add(buttonDeviceSettings);
            flowLayoutPanelActions.Controls.Add(buttonPrintSettings);

            panelTopBar.ResumeLayout(true);
        }

        private void ConfigureTopButton(Button btn, string text, int width, Color backColor, string toolTipText)
        {
            if (btn == null) return;
            btn.Text = text;
            btn.Size = new Size(width, 38);
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Margin = new Padding(3, 2, 3, 2);
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.AutoEllipsis = false;

            if (toolTipMain != null && !string.IsNullOrEmpty(toolTipText))
            {
                toolTipMain.SetToolTip(btn, toolTipText);
            }
        }

        private void SetupKpiBarLayout()
        {
            panelKpi.SuspendLayout();
            panelKpi.Height = 68;
            panelKpi.Padding = new Padding(12, 4, 12, 4);
            panelKpi.BackColor = Color.FromArgb(248, 250, 252);

            panelKpi.Controls.Clear();

            var tableKpi = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.Transparent,
                Padding = new Padding(0),
                Margin = new Padding(0),
                RightToLeft = RightToLeft.Yes
            };
            tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            tableKpi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));
            tableKpi.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            // بطاقة 1: إجمالي حضور اليوم (اليمين)
            ConfigureKpiCard(panelKpiTotal, labelKpiTotalTitle, labelKpiTotalVal,
                "📊 إجمالي الحضور اليوم",
                Color.FromArgb(30, 64, 175),
                Color.FromArgb(37, 99, 235),
                Color.FromArgb(239, 246, 255),
                "إجمالي عدد حركات الحضور المسجلة بتاريخ اليوم حتى الآن");

            // بطاقة 2: مطلوبين مسجلين (الوسط)
            ConfigureKpiCard(panelKpiWanted, labelKpiWantedTitle, labelKpiWantedVal,
                "🚨 مطلوبين مسجلين اليوم",
                Color.FromArgb(153, 27, 27),
                Color.FromArgb(220, 38, 38),
                Color.FromArgb(254, 242, 242),
                "عدد الأشخاص المطلوبين أمنياً الذين سجلوا حضور اليوم");

            // بطاقة 3: إشعارات مطبوعة (اليسار)
            ConfigureKpiCard(panelKpiPrinted, labelKpiPrintedTitle, labelKpiPrintedVal,
                "🖨️ إشعارات مطبوعة",
                Color.FromArgb(6, 95, 70),
                Color.FromArgb(5, 150, 105),
                Color.FromArgb(236, 253, 245),
                "إجمالي إشعارات الحضور المطبوعة آلياً ويدوياً اليوم");

            tableKpi.Controls.Add(panelKpiTotal, 0, 0);
            tableKpi.Controls.Add(panelKpiWanted, 1, 0);
            tableKpi.Controls.Add(panelKpiPrinted, 2, 0);

            panelKpi.Controls.Add(tableKpi);
            panelKpi.ResumeLayout(true);
        }

        private void ConfigureKpiCard(Panel cardPanel, Label lblTitle, Label lblVal, string titleText, Color titleColor, Color valColor, Color bgColor, string tooltipText)
        {
            cardPanel.SuspendLayout();
            cardPanel.Controls.Clear();
            cardPanel.Dock = DockStyle.Fill;
            cardPanel.Margin = new Padding(6, 2, 6, 2);
            cardPanel.BackColor = bgColor;
            cardPanel.BorderStyle = BorderStyle.FixedSingle;

            var innerTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Padding = new Padding(6, 2, 6, 2),
                Margin = new Padding(0),
                RightToLeft = RightToLeft.Yes
            };
            innerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65f)); // عنوان البطاقة (يمين)
            innerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f)); // الرقم والإحصائية (يسار)
            innerTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            lblTitle.Text = titleText;
            lblTitle.ForeColor = titleColor;
            lblTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            lblTitle.AutoEllipsis = false;

            lblVal.ForeColor = valColor;
            lblVal.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblVal.Dock = DockStyle.Fill;
            lblVal.TextAlign = ContentAlignment.MiddleCenter;

            innerTable.Controls.Add(lblTitle, 0, 0);
            innerTable.Controls.Add(lblVal, 1, 0);

            cardPanel.Controls.Add(innerTable);
            cardPanel.ResumeLayout(true);

            if (toolTipMain != null && !string.IsNullOrEmpty(tooltipText))
            {
                toolTipMain.SetToolTip(cardPanel, tooltipText);
                toolTipMain.SetToolTip(lblTitle, tooltipText);
                toolTipMain.SetToolTip(lblVal, tooltipText);
                toolTipMain.SetToolTip(innerTable, tooltipText);
            }
        }

        private void SetupPersonCardLayout()
        {
            panelCard.SuspendLayout();
            panelCardContent.SuspendLayout();

            // 1. حاوية إطار الصورة الشخصية
            if (panelPhotoContainer == null)
            {
                panelPhotoContainer = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 100,
                    BackColor = Color.Transparent,
                    Padding = new Padding(0)
                };

                panelPhotoFrame.Dock = DockStyle.None;
                panelPhotoFrame.Size = new Size(84, 84);
                panelPhotoFrame.BorderStyle = BorderStyle.FixedSingle;
                panelPhotoFrame.BackColor = Color.FromArgb(241, 245, 249);
                panelPhotoFrame.Location = new Point((panelCard.Width - 84) / 2, 6);
                panelPhotoFrame.Anchor = AnchorStyles.Top;

                panelPhotoContainer.Controls.Add(panelPhotoFrame);
            }

            // 2. ترويسة البطاقة
            labelCardHeader.Height = 36;
            labelCardHeader.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            labelCardHeader.BackColor = Color.FromArgb(15, 23, 42);
            labelCardHeader.ForeColor = Color.White;
            labelCardHeader.TextAlign = ContentAlignment.MiddleCenter;

            // 3. نصوص الاسم والبيانات الأساسية
            labelCardName.Dock = DockStyle.Top;
            labelCardName.Height = 30;
            labelCardName.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            labelCardName.ForeColor = Color.FromArgb(15, 23, 42);
            labelCardName.TextAlign = ContentAlignment.MiddleCenter;
            labelCardName.Padding = new Padding(0);

            labelCardNationalId.Dock = DockStyle.Top;
            labelCardNationalId.Height = 22;
            labelCardNationalId.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            labelCardNationalId.ForeColor = Color.FromArgb(71, 85, 105);
            labelCardNationalId.TextAlign = ContentAlignment.MiddleCenter;

            labelCardJob.Dock = DockStyle.Top;
            labelCardJob.Height = 24;
            labelCardJob.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            labelCardJob.ForeColor = Color.FromArgb(100, 116, 139);
            labelCardJob.TextAlign = ContentAlignment.MiddleCenter;

            // 4. صندوق التنبيهات الإدارية والمطلوبين (ذاتي التمدد دون قص للنص)
            panelWantedAlert.Dock = DockStyle.Top;
            panelWantedAlert.Padding = new Padding(8, 6, 8, 8);
            panelWantedAlert.Margin = new Padding(0, 4, 0, 4);
            panelWantedAlert.BorderStyle = BorderStyle.FixedSingle;
            panelWantedAlert.BackColor = Color.FromArgb(254, 243, 199);

            labelWantedTitle.Dock = DockStyle.Top;
            labelWantedTitle.Height = 24;
            labelWantedTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelWantedTitle.TextAlign = ContentAlignment.MiddleCenter;

            labelWantedReason.Dock = DockStyle.Top;
            labelWantedReason.AutoSize = true;
            labelWantedReason.MaximumSize = new Size(320, 0);
            labelWantedReason.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            labelWantedReason.TextAlign = ContentAlignment.MiddleCenter;
            labelWantedReason.Padding = new Padding(2, 4, 2, 4);

            // 5. موعد المتابعة القادم
            panelNextDate.Dock = DockStyle.Top;
            panelNextDate.Height = 70;
            panelNextDate.Padding = new Padding(6, 4, 6, 4);
            panelNextDate.Margin = new Padding(0, 4, 0, 4);
            panelNextDate.BorderStyle = BorderStyle.FixedSingle;
            panelNextDate.BackColor = Color.FromArgb(240, 253, 244);

            labelNextDateTitle.Dock = DockStyle.Top;
            labelNextDateTitle.Height = 22;
            labelNextDateTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            labelNextDateTitle.ForeColor = Color.FromArgb(5, 150, 105);
            labelNextDateTitle.TextAlign = ContentAlignment.MiddleCenter;

            labelNextDateValue.Dock = DockStyle.Fill;
            labelNextDateValue.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            labelNextDateValue.ForeColor = Color.FromArgb(6, 95, 70);
            labelNextDateValue.TextAlign = ContentAlignment.MiddleCenter;

            // 6. شارة وقت وتأكيد البصمة اللحظية
            if (panelPunchBadge == null)
            {
                panelPunchBadge = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 44,
                    Padding = new Padding(6, 4, 6, 4),
                    Margin = new Padding(0, 4, 0, 4),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(248, 250, 252),
                    Visible = false
                };

                labelPunchStatus = new Label
                {
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(51, 65, 85),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = ""
                };
                panelPunchBadge.Controls.Add(labelPunchStatus);
            }

            // 7. زر إعادة الطباعة
            buttonReprint.Dock = DockStyle.Bottom;
            buttonReprint.Height = 42;
            buttonReprint.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonReprint.BackColor = Color.FromArgb(30, 41, 59);
            buttonReprint.ForeColor = Color.White;
            buttonReprint.FlatStyle = FlatStyle.Flat;
            buttonReprint.FlatAppearance.BorderSize = 0;
            buttonReprint.Cursor = Cursors.Hand;
            toolTipMain?.SetToolTip(buttonReprint, "طباعة أو إعادة طباعة إشعار الحضور الورقي للشخص المحدد حالياً");

            // ترتيب دقيق لعناصر البطاقة لتظهر من الأعلى إلى الأسفل بشكل انسيابي
            panelCardContent.Controls.Clear();
            panelCardContent.Controls.Add(buttonReprint);
            panelCardContent.Controls.Add(panelPunchBadge);
            panelCardContent.Controls.Add(panelNextDate);
            panelCardContent.Controls.Add(panelWantedAlert);
            panelCardContent.Controls.Add(labelCardJob);
            panelCardContent.Controls.Add(labelCardNationalId);
            panelCardContent.Controls.Add(labelCardName);
            panelCardContent.Controls.Add(panelPhotoContainer);

            panelCardContent.ResumeLayout(true);
            panelCard.ResumeLayout(true);
        }

        private void AdjustSplitterDistance()
        {
            if (splitContainerMain == null || splitContainerMain.Width < 500) return;
            int cardWidth = 360;
            int targetSplitter = splitContainerMain.Width - cardWidth;
            if (targetSplitter > 350)
            {
                try
                {
                    splitContainerMain.FixedPanel = FixedPanel.Panel2;
                    splitContainerMain.SplitterDistance = targetSplitter;
                }
                catch { }
            }
        }

        private async void FingerprintAttendanceUserControl_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyUserControlLayout(this);
            SetupTopBarResponsiveLayout();
            SetupKpiBarLayout();
            SetupPersonCardLayout();
            AdjustSplitterDistance();

            await RefreshTodayGridAsync();
            await TryAutoConnectDeviceAsync();

            try
            {
                var settings = await dataHelperPrint.GetAllDataAsync();
                var s = settings?.FirstOrDefault();
                if (s != null)
                {
                    cardDurationSeconds = Math.Max(0, s.ScreenDurationSeconds);
                }
            }
            catch { }
        }

        private async Task TryAutoConnectDeviceAsync()
        {
            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var dev = devices?.FirstOrDefault(x => x.IsEnabled);
                if (dev != null)
                {
                    labelDeviceStatus.Text = $"جاري الاتصال بـ {dev.DeviceName}...";
                    lblStatusDot.ForeColor = Color.FromArgb(245, 158, 11); // Amber
                    bool ok = await zkService.ConnectAsync(dev);
                    UpdateDeviceStatusUI(ok, dev);
                }
                else
                {
                    labelDeviceStatus.Text = "لم يتم تهيئة جهاز بصمة.";
                    lblStatusDot.ForeColor = Color.FromArgb(148, 163, 184);
                }
            }
            catch (Exception ex)
            {
                labelDeviceStatus.Text = $"خطأ: {ex.Message}";
                lblStatusDot.ForeColor = Color.FromArgb(239, 68, 68);
            }
        }

        private void UpdateDeviceStatusUI(bool isConnected, FingerprintDevice dev)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateDeviceStatusUI(isConnected, dev)));
                return;
            }

            if (isConnected)
            {
                labelDeviceStatus.Text = $"متصل: {dev?.DeviceName ?? "جهاز البصمة"}";
                lblStatusDot.ForeColor = Color.FromArgb(16, 185, 129); // Vibrant Green
                buttonConnect.Text = "🛑 قطع الاتصال";
                buttonConnect.BackColor = Color.FromArgb(220, 38, 38);
                toolTipMain?.SetToolTip(buttonConnect, "قطع الاتصال بجهاز البصمة الحالي");
            }
            else
            {
                labelDeviceStatus.Text = "جهاز البصمة: غير متصل";
                lblStatusDot.ForeColor = Color.FromArgb(239, 68, 68); // Red
                buttonConnect.Text = "⚡ اتصال بالبصمة";
                buttonConnect.BackColor = Color.FromArgb(16, 185, 129);
                toolTipMain?.SetToolTip(buttonConnect, "الاتصال بجهاز البصمة الشبكي الرئيسي");
            }
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            if (zkService.IsConnected)
            {
                zkService.Disconnect();
                UpdateDeviceStatusUI(false, zkService.CurrentDevice);
            }
            else
            {
                buttonConnect.Enabled = false;
                await TryAutoConnectDeviceAsync();
                buttonConnect.Enabled = true;
            }
        }

        private async void buttonSyncLogs_Click(object sender, EventArgs e)
        {
            if (!zkService.IsConnected)
            {
                MessageBox.Show("الجهاز غير متصل حالياً. يرجى الضغط على زر الاتصال بالجهاز أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            buttonSyncLogs.Enabled = false;
            try
            {
                var logs = await zkService.ReadNewLogsAsync();
                int processed = 0;
                foreach (var logEvent in logs)
                {
                    await attendanceProcessor.ProcessAttendanceAsync(
                        logEvent.ElementId,
                        logEvent.DeviceId,
                        logEvent.AttendanceTime,
                        logEvent.VerifyMethod,
                        deviceSerialNumber: logEvent.DeviceSerialNumber);
                    processed++;
                }

                await RefreshTodayGridAsync();
                MessageBox.Show($"اكتملت المزامنة بنجاح.\nتم فحص ومعالجة ({processed}) حركة حضور.", "اكتملت المزامنة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء المزامنة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonSyncLogs.Enabled = true;
            }
        }

        private async void buttonSyncTime_Click(object sender, EventArgs e)
        {
            if (!zkService.IsConnected)
            {
                MessageBox.Show("الجهاز غير متصل حالياً. يرجى الضغط على زر الاتصال أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            buttonSyncTime.Enabled = false;
            try
            {
                bool ok = await zkService.SyncDeviceTimeAsync();
                if (ok)
                {
                    var now = DateTime.Now;
                    labelDeviceStatus.Text = $"تمت مزامنة وقت الجهاز بنجاح: {now:yyyy-MM-dd HH:mm:ss}";
                    lblStatusDot.ForeColor = Color.FromArgb(16, 185, 129);
                    MessageBox.Show($"تمت مزامنة ساعة وتاريخ جهاز البصمة مع وقت الحاسوب بنجاح!\n\nالوقت الحالي للجهاز: {now:yyyy-MM-dd HH:mm:ss tt}", "تمت المزامنة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("تعذر ضبط وقت الجهاز. يرجى التحقق من اتصال الشبكة وصلاحيات الجهاز.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء مزامنة الوقت: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonSyncTime.Enabled = true;
            }
        }

        private async void ZkService_OnAttendanceReceived(object sender, AttendanceEventArgs e)
        {
            await attendanceProcessor.ProcessAttendanceAsync(
                e.ElementId,
                e.DeviceId,
                e.AttendanceTime,
                e.VerifyMethod,
                deviceSerialNumber: e.DeviceSerialNumber);
        }

        private void AttendanceProcessor_OnAttendanceProcessed(object sender, AttendanceProcessResult result)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AttendanceProcessor_OnAttendanceProcessed(sender, result)));
                return;
            }

            if (result.Element != null)
            {
                if (result.IsWanted)
                {
                    SystemSounds.Hand.Play();
                }
                else if (result.Success)
                {
                    SystemSounds.Asterisk.Play();
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                }

                DisplayElementOnCard(result.Element, result.WantedStatus, result.AttendanceLog, result.Message, result.Success);

                if (result.Success)
                {
                    _ = RefreshTodayGridAsync();
                }

                // إدارة مدة بقاء الداتا على الشاشة:
                timerCardAutoReset.Stop();
                if (cardDurationSeconds > 0)
                {
                    timerCardAutoReset.Interval = cardDurationSeconds * 1000;
                    timerCardAutoReset.Start();
                }
                // إذا كانت المدة 0: لا يتم تفعيل المؤقت وتبقى البيانات ظاهرة على الشاشة بدون تعديل حتى البصمة التالية

                // المزامنة الفورية مع أي شاشة كشك عرض مفتوحة
                foreach (var kiosk in Application.OpenForms.OfType<AttendanceDisplayKioskForm>())
                {
                    kiosk.DisplayAttendanceReport(
                        result.Element,
                        result.AttendanceLog?.AttendanceDateTime ?? DateTime.Now,
                        result.IsWanted,
                        result.NextFollowDate ?? (result.Element.DateFollowNow > DateTime.MinValue ? result.Element.DateFollowNow : (DateTime?)null),
                        result.Success,
                        result.Message);
                }

                if (zkService.IsConnected)
                {
                    labelDeviceStatus.Text = $"متصل: {zkService.CurrentDevice?.DeviceName ?? "جهاز البصمة"}";
                    lblStatusDot.ForeColor = Color.FromArgb(16, 185, 129);
                }
            }
            else if (!string.IsNullOrEmpty(result.Message))
            {
                SystemSounds.Exclamation.Play();
                DisplayElementOnCard(result.Element, result.WantedStatus, result.AttendanceLog, result.Message, false);
            }
        }

        private void ZkService_OnStatusChanged(object sender, string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ZkService_OnStatusChanged(sender, msg)));
                return;
            }
            labelDeviceStatus.Text = msg;
        }

        private void ZkService_OnErrorOccurred(object sender, string err)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ZkService_OnErrorOccurred(sender, err)));
                return;
            }
            labelDeviceStatus.Text = err;
            lblStatusDot.ForeColor = Color.FromArgb(239, 68, 68);
        }

        public async Task RefreshTodayGridAsync()
        {
            try
            {
                var logs = await dataHelperAttendanceLog.GetFilteredDataAsync();
                todayLogs = logs ?? new List<AttendanceLog>();

                FilterAndBindGrid(resetPagination: true);
                UpdateKpiStats();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing grid: {ex.Message}");
            }
        }

        private void FilterAndBindGrid(bool resetPagination = false)
        {
            dataGridViewToday.Rows.Clear();

            string query = textBoxSearch.Text.Trim();
            int filterMode = comboBoxFilterStatus.SelectedIndex; // 0: All, 1: Normal only, 2: Wanted only

            var list = todayLogs.AsEnumerable();

            if (filterMode == 1)
            {
                list = list.Where(x => !x.IsWantedAtTime && x.Status != "مطلوب");
            }
            else if (filterMode == 2)
            {
                list = list.Where(x => x.IsWantedAtTime || x.Status == "مطلوب");
            }

            if (!string.IsNullOrEmpty(query))
            {
                list = list.Where(x =>
                    x.ElementId.ToString().Contains(query) ||
                    (x.ElementInfo != null && x.ElementInfo.ElementName.Contains(query)) ||
                    (x.ElementInfo != null && x.ElementInfo.NationalId != null && x.ElementInfo.NationalId.Contains(query)) ||
                    (x.Status != null && x.Status.Contains(query))
                );
            }

            var filteredList = list.ToList();
            var pageData = paginationControl != null ? paginationControl.GetPageData(filteredList, resetToFirstPage: resetPagination) : filteredList;

            int startIndex = paginationControl != null && !paginationControl.IsAll 
                ? (paginationControl.CurrentPage - 1) * paginationControl.PageSize 
                : 0;

            for (int i = 0; i < pageData.Count; i++)
            {
                var log = pageData[i];
                int serialNo = startIndex + i + 1;
                string name = log.ElementInfo?.ElementName ?? $"شخص #{log.ElementId}";
                string nationalId = log.ElementInfo?.NationalId ?? "-";
                string timeStr = log.AttendanceDateTime.ToString("hh:mm tt");
                string nextDateStr = log.NextFollowDateAssigned.HasValue ? log.NextFollowDateAssigned.Value.ToString("yyyy/MM/dd") : "-";
                string printedStr = log.IsPrinted ? "تمت ✓" : "لم تطبع";

                int rowIdx = dataGridViewToday.Rows.Add(
                    serialNo,
                    timeStr,
                    name,
                    nationalId,
                    log.Status,
                    nextDateStr,
                    printedStr);

                dataGridViewToday.Rows[rowIdx].Tag = log.Id;

                if (log.IsWantedAtTime || log.Status == "مطلوب")
                {
                    dataGridViewToday.Rows[rowIdx].DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    dataGridViewToday.Rows[rowIdx].DefaultCellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                    dataGridViewToday.Rows[rowIdx].DefaultCellStyle.Font = new Font(dataGridViewToday.Font, FontStyle.Bold);
                }
            }
        }

        private void UpdateKpiStats()
        {
            int total = todayLogs.Count;
            int wanted = todayLogs.Count(x => x.IsWantedAtTime || x.Status == "مطلوب");
            int printed = todayLogs.Count(x => x.IsPrinted);

            labelKpiTotalVal.Text = total.ToString();
            labelKpiWantedVal.Text = wanted.ToString();
            labelKpiPrintedVal.Text = printed.ToString();
        }

        private void DisplayElementOnCard(ElementInfo element, ElementWantedStatus wantedStatus, AttendanceLog log, string alertMessage = null, bool isSuccess = true)
        {
            if (element == null) return;

            currentSelectedElement = element;
            currentSelectedWanted = wantedStatus;
            currentSelectedLog = log;

            labelCardName.Text = element.ElementName;
            string enrollStr = element.DeviceEnrollId.HasValue ? $" | كود الماكينة: #{element.DeviceEnrollId.Value}" : "";
            labelCardNationalId.Text = $"الرقم القومي: {element.NationalId ?? "-"}{enrollStr}";
            labelCardJob.Text = $"المهنة: {element.Job ?? "-"} | هاتف: {element.Mobile ?? element.Phone ?? "-"}";

            if (element.ElementImage != null && element.ElementImage.Length > 0)
            {
                try
                {
                    using var ms = new MemoryStream(element.ElementImage);
                    pictureBoxElement.Image = Image.FromStream(ms);
                }
                catch
                {
                    pictureBoxElement.Image = null;
                }
            }
            else
            {
                pictureBoxElement.Image = null;
            }

            bool isWanted = (wantedStatus != null && wantedStatus.IsWanted) || (log != null && log.IsWantedAtTime);

            if (isWanted)
            {
                panelWantedAlert.Visible = true;
                panelWantedAlert.BackColor = Color.FromArgb(254, 226, 226);
                labelWantedTitle.Text = "🚨 عـنـصـر مـطـلـوب 🚨";
                labelWantedTitle.ForeColor = Color.FromArgb(185, 28, 28);
                labelWantedReason.Text = !string.IsNullOrEmpty(wantedStatus?.WantedReason)
                    ? $"السبب: {wantedStatus.WantedReason}"
                    : "مطلوب للمتابعة الفورية";
                labelWantedReason.ForeColor = Color.FromArgb(153, 27, 27);

                // حساب الارتفاع بدقة لمنع أي قص للنصوص مهما طال السبب
                int maxW = Math.Max(200, panelWantedAlert.Width - 20);
                labelWantedReason.MaximumSize = new Size(maxW, 0);
                panelWantedAlert.Height = labelWantedTitle.Height + labelWantedReason.Height + 18;

                panelNextDate.Visible = false;
                labelNextDateValue.Text = "-";
            }
            else if (!isSuccess && !string.IsNullOrEmpty(alertMessage))
            {
                panelWantedAlert.Visible = true;
                panelWantedAlert.BackColor = Color.FromArgb(254, 243, 199);
                labelWantedTitle.Text = "⚠️ تنبيه إداري";
                labelWantedTitle.ForeColor = Color.FromArgb(180, 83, 9);
                labelWantedReason.Text = alertMessage;
                labelWantedReason.ForeColor = Color.FromArgb(146, 64, 14);

                // حساب الارتفاع بدقة لمنع أي قص لنص التنبيه الإداري
                int maxW = Math.Max(200, panelWantedAlert.Width - 20);
                labelWantedReason.MaximumSize = new Size(maxW, 0);
                panelWantedAlert.Height = labelWantedTitle.Height + labelWantedReason.Height + 18;

                panelNextDate.Visible = true;
                DateTime? nextDate = element.DateFollowNow > DateTime.MinValue ? element.DateFollowNow : (DateTime?)null;
                if (nextDate.HasValue)
                {
                    string dayName = nextDate.Value.ToString("dddd", new System.Globalization.CultureInfo("ar-EG"));
                    labelNextDateValue.Text = $"{dayName}  {nextDate.Value:yyyy/MM/dd}";
                }
                else
                {
                    labelNextDateValue.Text = "-";
                }
            }
            else
            {
                panelWantedAlert.Visible = false;
                panelNextDate.Visible = true;

                DateTime? nextDate = log?.NextFollowDateAssigned ?? (element.DateFollowNow > DateTime.MinValue ? element.DateFollowNow : null);
                if (nextDate.HasValue)
                {
                    string dayName = nextDate.Value.ToString("dddd", new System.Globalization.CultureInfo("ar-EG"));
                    labelNextDateValue.Text = $"{dayName}  {nextDate.Value:yyyy/MM/dd}";
                }
                else
                {
                    labelNextDateValue.Text = "-";
                }
            }

            // تحديث شارة البصمة الآنية
            if (panelPunchBadge != null && labelPunchStatus != null)
            {
                if (log != null)
                {
                    panelPunchBadge.Visible = true;
                    string punchTime = log.AttendanceDateTime.ToString("hh:mm tt", new System.Globalization.CultureInfo("ar-EG"));
                    labelPunchStatus.Text = $"⏰ وقت البصمة: {punchTime}  |  {(isSuccess ? "مسجل بنجاح ✓" : "لم يسجل ✕")}";
                    labelPunchStatus.ForeColor = isSuccess ? Color.FromArgb(5, 150, 105) : Color.FromArgb(220, 38, 38);
                }
                else
                {
                    panelPunchBadge.Visible = false;
                }
            }
        }

        private async void dataGridViewToday_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewToday.SelectedRows.Count == 0) return;

            // إيقاف الإخفاء التلقائي عند التحديد اليدوي من الجدول لفحص البيانات بحرية
            timerCardAutoReset.Stop();

            object tagVal = dataGridViewToday.SelectedRows[0].Tag;
            int logId = tagVal != null ? Convert.ToInt32(tagVal) : 0;
            if (logId <= 0) return;
            var log = todayLogs.FirstOrDefault(x => x.Id == logId);
            if (log == null) return;

            var element = log.ElementInfo ?? await dataHelperElement.FindAsync(log.ElementId);
            var wantedList = await dataHelperWanted.GetAllDataAsync();
            var wanted = wantedList?.FirstOrDefault(x => x.ElementId == log.ElementId);

            DisplayElementOnCard(element, wanted, log);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterAndBindGrid();
        }

        private void comboBoxFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterAndBindGrid();
        }

        private async void buttonRefreshGrid_Click(object sender, EventArgs e)
        {
            await RefreshTodayGridAsync();
        }

        private async void buttonManualAttendance_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "أدخل رقم العنصر (ID) لتسجيل حضوره يدوياً:",
                "تسجيل حضور يدوي",
                "");

            if (string.IsNullOrWhiteSpace(input)) return;

            if (int.TryParse(input.Trim(), out int elementId))
            {
                var res = await attendanceProcessor.ProcessAttendanceAsync(elementId, deviceId: null, attendanceTime: DateTime.Now, verifyType: 9);
                if (!res.Success)
                {
                    MessageBox.Show(res.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("يرجى إدخال رقم صحيح للعنصر", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonReprint_Click(object sender, EventArgs e)
        {
            if (currentSelectedElement == null || currentSelectedLog == null)
            {
                MessageBox.Show("يرجى تحديد حركة حضور من الجدول أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var settings = await dataHelperPrint.GetAllDataAsync();
                var setting = settings?.FirstOrDefault() ?? new PrintSetting();

                printService.PrintAttendanceSlip(currentSelectedLog, currentSelectedElement, currentSelectedWanted, setting, isPreview: false);

                currentSelectedLog.IsPrinted = true;
                currentSelectedLog.PrintedDate = DateTime.Now;
                await dataHelperAttendanceLog.EditAsync(currentSelectedLog);

                await RefreshTodayGridAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء الطباعة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonManageWanted_Click(object sender, EventArgs e)
        {
            using var frm = new ElementWantedManageForm(currentSelectedElement?.Id);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _ = RefreshTodayGridAsync();
            }
        }

        private void buttonEnrollFingerprint_Click(object sender, EventArgs e)
        {
            using var frm = new ElementFingerprintEnrollForm(currentSelectedElement?.Id);
            frm.ShowDialog();
        }

        private void buttonDeviceSettings_Click(object sender, EventArgs e)
        {
            using var frm = new FingerprintDeviceForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _ = TryAutoConnectDeviceAsync();
            }
        }

        private void buttonPrintSettings_Click(object sender, EventArgs e)
        {
            using var frm = new PrintSettingForm();
            frm.ShowDialog();
        }

        private void buttonOpenKiosk_Click(object sender, EventArgs e)
        {
            var kiosk = new AttendanceDisplayKioskForm();
            kiosk.Show();
        }

        public void UpdateScreenDuration(int seconds)
        {
            cardDurationSeconds = Math.Max(0, seconds);
            if (cardDurationSeconds == 0)
            {
                timerCardAutoReset.Stop();
            }
            else
            {
                timerCardAutoReset.Interval = cardDurationSeconds * 1000;
            }
        }

        private void TimerCardAutoReset_Tick(object sender, EventArgs e)
        {
            timerCardAutoReset.Stop();
            ResetCardToIdle();
        }

        public void ResetCardToIdle()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ResetCardToIdle));
                return;
            }

            currentSelectedElement = null;
            currentSelectedLog = null;
            currentSelectedWanted = null;

            labelCardName.Text = "في انتظار تسجيل بصمة...";
            labelCardNationalId.Text = "الرقم القومي: -";
            labelCardJob.Text = "المهنة: -";
            pictureBoxElement.Image = null;
            panelWantedAlert.Visible = false;
            panelWantedAlert.BackColor = Color.FromArgb(254, 226, 226);
            labelWantedTitle.Text = "🚨 شـــخـص مـطـلـوب 🚨";
            labelWantedTitle.ForeColor = Color.FromArgb(185, 28, 28);
            panelNextDate.Visible = false;
            labelNextDateValue.Text = "-";
            if (panelPunchBadge != null) panelPunchBadge.Visible = false;
        }
    }
}
