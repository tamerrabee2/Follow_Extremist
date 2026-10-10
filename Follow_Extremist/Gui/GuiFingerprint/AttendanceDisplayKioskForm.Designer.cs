namespace Follow_Extremist.Gui.GuiFingerprint
{
    partial class AttendanceDisplayKioskForm
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
            components = new System.ComponentModel.Container();
            panelTopBar = new System.Windows.Forms.Panel();
            panelTopCenter = new System.Windows.Forms.Panel();
            lblStatusPill = new System.Windows.Forms.Label();
            panelTopLeft = new System.Windows.Forms.Panel();
            lblHeaderTitle = new System.Windows.Forms.Label();
            lblHeaderSub = new System.Windows.Forms.Label();
            panelTopRight = new System.Windows.Forms.Panel();
            panelClockContainer = new System.Windows.Forms.Panel();
            lblClock = new System.Windows.Forms.Label();
            lblDate = new System.Windows.Forms.Label();
            btnFullScreen = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            panelMainContainer = new System.Windows.Forms.Panel();
            panelReport = new System.Windows.Forms.Panel();
            panelReportCard = new System.Windows.Forms.Panel();
            lblCountdownText = new System.Windows.Forms.Label();
            progressBarCountdown = new System.Windows.Forms.ProgressBar();
            panelWantedAlertBox = new System.Windows.Forms.Panel();
            lblWantedInstruction = new System.Windows.Forms.Label();
            lblWantedTitle = new System.Windows.Forms.Label();
            panelNextDateBox = new System.Windows.Forms.Panel();
            lblNextDateDays = new System.Windows.Forms.Label();
            lblNextDateValue = new System.Windows.Forms.Label();
            lblNextDateHeader = new System.Windows.Forms.Label();
            lblAttendanceTime = new System.Windows.Forms.Label();
            lblElementMeta = new System.Windows.Forms.Label();
            lblElementName = new System.Windows.Forms.Label();
            pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            lblReportBadge = new System.Windows.Forms.Label();
            panelIdle = new System.Windows.Forms.Panel();
            panelIdleStats = new System.Windows.Forms.Panel();
            lblStatCount = new System.Windows.Forms.Label();
            lblStatTitle = new System.Windows.Forms.Label();
            lblIdleSubtitle = new System.Windows.Forms.Label();
            lblIdleTitle = new System.Windows.Forms.Label();
            lblIdleIcon = new System.Windows.Forms.Label();
            timerClock = new System.Windows.Forms.Timer(components);
            timerCountdown = new System.Windows.Forms.Timer(components);
            timerDbPoll = new System.Windows.Forms.Timer(components);
            timerPulse = new System.Windows.Forms.Timer(components);
            panelTopBar.SuspendLayout();
            panelTopCenter.SuspendLayout();
            panelTopLeft.SuspendLayout();
            panelTopRight.SuspendLayout();
            panelClockContainer.SuspendLayout();
            panelMainContainer.SuspendLayout();
            panelReport.SuspendLayout();
            panelReportCard.SuspendLayout();
            panelWantedAlertBox.SuspendLayout();
            panelNextDateBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
            panelIdle.SuspendLayout();
            panelIdleStats.SuspendLayout();
            SuspendLayout();
            // 
            // panelTopBar
            // 
            panelTopBar.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            panelTopBar.Controls.Add(panelTopCenter);
            panelTopBar.Controls.Add(panelTopLeft);
            panelTopBar.Controls.Add(panelTopRight);
            panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            panelTopBar.Location = new System.Drawing.Point(0, 0);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            panelTopBar.Size = new System.Drawing.Size(1280, 80);
            panelTopBar.TabIndex = 0;
            // 
            // panelTopCenter
            // 
            panelTopCenter.BackColor = System.Drawing.Color.Transparent;
            panelTopCenter.Controls.Add(lblStatusPill);
            panelTopCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            panelTopCenter.Location = new System.Drawing.Point(335, 8);
            panelTopCenter.Name = "panelTopCenter";
            panelTopCenter.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            panelTopCenter.Size = new System.Drawing.Size(485, 64);
            panelTopCenter.TabIndex = 2;
            // 
            // lblStatusPill
            // 
            lblStatusPill.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblStatusPill.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatusPill.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblStatusPill.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblStatusPill.Location = new System.Drawing.Point(20, 10);
            lblStatusPill.Name = "lblStatusPill";
            lblStatusPill.Size = new System.Drawing.Size(445, 44);
            lblStatusPill.TabIndex = 0;
            lblStatusPill.Text = "● جاهز لاستقبال البصمة";
            lblStatusPill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTopLeft
            // 
            panelTopLeft.BackColor = System.Drawing.Color.Transparent;
            panelTopLeft.Controls.Add(lblHeaderTitle);
            panelTopLeft.Controls.Add(lblHeaderSub);
            panelTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
            panelTopLeft.Location = new System.Drawing.Point(15, 8);
            panelTopLeft.Name = "panelTopLeft";
            panelTopLeft.Size = new System.Drawing.Size(320, 64);
            panelTopLeft.TabIndex = 1;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new System.Drawing.Size(320, 34);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "نظام المتابعة الإلكتروني";
            lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHeaderSub
            // 
            lblHeaderSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblHeaderSub.Location = new System.Drawing.Point(0, 36);
            lblHeaderSub.Name = "lblHeaderSub";
            lblHeaderSub.Size = new System.Drawing.Size(320, 28);
            lblHeaderSub.TabIndex = 1;
            lblHeaderSub.Text = "شاشة عرض الحضور اللحظي";
            lblHeaderSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelTopRight
            // 
            panelTopRight.BackColor = System.Drawing.Color.Transparent;
            panelTopRight.Controls.Add(panelClockContainer);
            panelTopRight.Controls.Add(btnFullScreen);
            panelTopRight.Controls.Add(btnClose);
            panelTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            panelTopRight.Location = new System.Drawing.Point(820, 8);
            panelTopRight.Name = "panelTopRight";
            panelTopRight.Size = new System.Drawing.Size(445, 64);
            panelTopRight.TabIndex = 0;
            // 
            // panelClockContainer
            // 
            panelClockContainer.Controls.Add(lblClock);
            panelClockContainer.Controls.Add(lblDate);
            panelClockContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelClockContainer.Location = new System.Drawing.Point(0, 0);
            panelClockContainer.Name = "panelClockContainer";
            panelClockContainer.Padding = new System.Windows.Forms.Padding(0, 4, 15, 4);
            panelClockContainer.Size = new System.Drawing.Size(325, 64);
            panelClockContainer.TabIndex = 0;
            // 
            // lblClock
            // 
            lblClock.Dock = System.Windows.Forms.DockStyle.Top;
            lblClock.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblClock.ForeColor = System.Drawing.Color.FromArgb(248, 250, 252);
            lblClock.Location = new System.Drawing.Point(0, 4);
            lblClock.Name = "lblClock";
            lblClock.Size = new System.Drawing.Size(310, 30);
            lblClock.TabIndex = 0;
            lblClock.Text = "12:00:00 م";
            lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            lblDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblDate.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblDate.Location = new System.Drawing.Point(0, 36);
            lblDate.Name = "lblDate";
            lblDate.Size = new System.Drawing.Size(310, 24);
            lblDate.TabIndex = 1;
            lblDate.Text = "الأربعاء 08 أكتوبر 2026";
            lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFullScreen
            // 
            btnFullScreen.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnFullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFullScreen.Dock = System.Windows.Forms.DockStyle.Right;
            btnFullScreen.FlatAppearance.BorderSize = 0;
            btnFullScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFullScreen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnFullScreen.ForeColor = System.Drawing.Color.FromArgb(203, 213, 225);
            btnFullScreen.Location = new System.Drawing.Point(325, 0);
            btnFullScreen.Margin = new System.Windows.Forms.Padding(6);
            btnFullScreen.Name = "btnFullScreen";
            btnFullScreen.Size = new System.Drawing.Size(65, 64);
            btnFullScreen.TabIndex = 1;
            btnFullScreen.Text = "⛶ F11";
            btnFullScreen.UseVisualStyleBackColor = false;
            btnFullScreen.Click += btnFullScreen_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnClose.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
            btnClose.Location = new System.Drawing.Point(390, 0);
            btnClose.Margin = new System.Windows.Forms.Padding(6);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(55, 64);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // panelMainContainer
            // 
            panelMainContainer.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            panelMainContainer.Controls.Add(panelReport);
            panelMainContainer.Controls.Add(panelIdle);
            panelMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMainContainer.Location = new System.Drawing.Point(0, 80);
            panelMainContainer.Name = "panelMainContainer";
            panelMainContainer.Padding = new System.Windows.Forms.Padding(30);
            panelMainContainer.Size = new System.Drawing.Size(1280, 720);
            panelMainContainer.TabIndex = 1;
            // 
            // panelReport
            // 
            panelReport.Anchor = System.Windows.Forms.AnchorStyles.None;
            panelReport.BackColor = System.Drawing.Color.Transparent;
            panelReport.Controls.Add(panelReportCard);
            panelReport.Location = new System.Drawing.Point(190, 20);
            panelReport.Name = "panelReport";
            panelReport.Size = new System.Drawing.Size(900, 660);
            panelReport.TabIndex = 1;
            panelReport.Visible = false;
            // 
            // panelReportCard
            // 
            panelReportCard.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            panelReportCard.Controls.Add(lblCountdownText);
            panelReportCard.Controls.Add(progressBarCountdown);
            panelReportCard.Controls.Add(panelWantedAlertBox);
            panelReportCard.Controls.Add(panelNextDateBox);
            panelReportCard.Controls.Add(lblAttendanceTime);
            panelReportCard.Controls.Add(lblElementMeta);
            panelReportCard.Controls.Add(lblElementName);
            panelReportCard.Controls.Add(pictureBoxPhoto);
            panelReportCard.Controls.Add(lblReportBadge);
            panelReportCard.Dock = System.Windows.Forms.DockStyle.Fill;
            panelReportCard.Location = new System.Drawing.Point(0, 0);
            panelReportCard.Name = "panelReportCard";
            panelReportCard.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            panelReportCard.Size = new System.Drawing.Size(900, 660);
            panelReportCard.TabIndex = 0;
            // 
            // lblCountdownText
            // 
            lblCountdownText.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblCountdownText.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblCountdownText.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblCountdownText.Location = new System.Drawing.Point(40, 565);
            lblCountdownText.Name = "lblCountdownText";
            lblCountdownText.Size = new System.Drawing.Size(820, 30);
            lblCountdownText.TabIndex = 8;
            lblCountdownText.Text = "العودة لشاشة الانتظار خلال 12 ثانية...";
            lblCountdownText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarCountdown
            // 
            progressBarCountdown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            progressBarCountdown.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            progressBarCountdown.Location = new System.Drawing.Point(40, 600);
            progressBarCountdown.Maximum = 12;
            progressBarCountdown.Name = "progressBarCountdown";
            progressBarCountdown.Size = new System.Drawing.Size(820, 10);
            progressBarCountdown.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBarCountdown.TabIndex = 7;
            progressBarCountdown.Value = 12;
            // 
            // panelWantedAlertBox
            // 
            panelWantedAlertBox.BackColor = System.Drawing.Color.FromArgb(127, 29, 29);
            panelWantedAlertBox.Controls.Add(lblWantedInstruction);
            panelWantedAlertBox.Controls.Add(lblWantedTitle);
            panelWantedAlertBox.Location = new System.Drawing.Point(40, 250);
            panelWantedAlertBox.Name = "panelWantedAlertBox";
            panelWantedAlertBox.Padding = new System.Windows.Forms.Padding(20);
            panelWantedAlertBox.Size = new System.Drawing.Size(820, 230);
            panelWantedAlertBox.TabIndex = 6;
            panelWantedAlertBox.Visible = false;
            // 
            // lblWantedInstruction
            // 
            lblWantedInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            lblWantedInstruction.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            lblWantedInstruction.ForeColor = System.Drawing.Color.White;
            lblWantedInstruction.Location = new System.Drawing.Point(20, 100);
            lblWantedInstruction.Name = "lblWantedInstruction";
            lblWantedInstruction.Size = new System.Drawing.Size(780, 110);
            lblWantedInstruction.TabIndex = 1;
            lblWantedInstruction.Text = "يـرجـى المـراجعـة ";
            lblWantedInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWantedTitle
            // 
            lblWantedTitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblWantedTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            lblWantedTitle.ForeColor = System.Drawing.Color.FromArgb(254, 202, 202);
            lblWantedTitle.Location = new System.Drawing.Point(20, 20);
            lblWantedTitle.Name = "lblWantedTitle";
            lblWantedTitle.Size = new System.Drawing.Size(780, 80);
            lblWantedTitle.TabIndex = 0;
            lblWantedTitle.Text = "⚠️ تنبيــــه هــــام ⚠️";
            lblWantedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelNextDateBox
            // 
            panelNextDateBox.BackColor = System.Drawing.Color.FromArgb(6, 78, 59);
            panelNextDateBox.Controls.Add(lblNextDateDays);
            panelNextDateBox.Controls.Add(lblNextDateValue);
            panelNextDateBox.Controls.Add(lblNextDateHeader);
            panelNextDateBox.Location = new System.Drawing.Point(40, 250);
            panelNextDateBox.Name = "panelNextDateBox";
            panelNextDateBox.Padding = new System.Windows.Forms.Padding(20);
            panelNextDateBox.Size = new System.Drawing.Size(820, 230);
            panelNextDateBox.TabIndex = 5;
            // 
            // lblNextDateDays
            // 
            lblNextDateDays.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblNextDateDays.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            lblNextDateDays.ForeColor = System.Drawing.Color.FromArgb(110, 231, 183);
            lblNextDateDays.Location = new System.Drawing.Point(20, 170);
            lblNextDateDays.Name = "lblNextDateDays";
            lblNextDateDays.Size = new System.Drawing.Size(780, 40);
            lblNextDateDays.TabIndex = 2;
            lblNextDateDays.Text = "✅ تم تسجيل حضورك بنجاح | دورية المتابعة: كل 7 أيام";
            lblNextDateDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNextDateValue
            // 
            lblNextDateValue.Dock = System.Windows.Forms.DockStyle.Top;
            lblNextDateValue.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold);
            lblNextDateValue.ForeColor = System.Drawing.Color.White;
            lblNextDateValue.Location = new System.Drawing.Point(20, 60);
            lblNextDateValue.Name = "lblNextDateValue";
            lblNextDateValue.Size = new System.Drawing.Size(780, 100);
            lblNextDateValue.TabIndex = 1;
            lblNextDateValue.Text = "الأحـد 18 أكتـوبـر 2026";
            lblNextDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNextDateHeader
            // 
            lblNextDateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            lblNextDateHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblNextDateHeader.ForeColor = System.Drawing.Color.FromArgb(167, 243, 208);
            lblNextDateHeader.Location = new System.Drawing.Point(20, 20);
            lblNextDateHeader.Name = "lblNextDateHeader";
            lblNextDateHeader.Size = new System.Drawing.Size(780, 40);
            lblNextDateHeader.TabIndex = 0;
            lblNextDateHeader.Text = "📅 مـوعـد المتـابعـة القـادم المقـرّر:";
            lblNextDateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAttendanceTime
            // 
            lblAttendanceTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblAttendanceTime.ForeColor = System.Drawing.Color.FromArgb(56, 189, 248);
            lblAttendanceTime.Location = new System.Drawing.Point(40, 185);
            lblAttendanceTime.Name = "lblAttendanceTime";
            lblAttendanceTime.Size = new System.Drawing.Size(660, 35);
            lblAttendanceTime.TabIndex = 4;
            lblAttendanceTime.Text = "⏱ وقت الحضور المسجل: 10:45:12 ص  (اليوم)";
            lblAttendanceTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblElementMeta
            // 
            lblElementMeta.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblElementMeta.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblElementMeta.Location = new System.Drawing.Point(40, 140);
            lblElementMeta.Name = "lblElementMeta";
            lblElementMeta.Size = new System.Drawing.Size(660, 35);
            lblElementMeta.TabIndex = 3;
            lblElementMeta.Text = "رقم القيد: #1024  |  الرقم القومي: 28901010000000  |  المهنة: موظف";
            lblElementMeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblElementName
            // 
            lblElementName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            lblElementName.ForeColor = System.Drawing.Color.White;
            lblElementName.Location = new System.Drawing.Point(40, 80);
            lblElementName.Name = "lblElementName";
            lblElementName.Size = new System.Drawing.Size(660, 55);
            lblElementName.TabIndex = 2;
            lblElementName.Text = "الاسم الكامل هنا";
            lblElementName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBoxPhoto
            // 
            pictureBoxPhoto.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBoxPhoto.Location = new System.Drawing.Point(720, 80);
            pictureBoxPhoto.Name = "pictureBoxPhoto";
            pictureBoxPhoto.Size = new System.Drawing.Size(140, 150);
            pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxPhoto.TabIndex = 1;
            pictureBoxPhoto.TabStop = false;
            // 
            // lblReportBadge
            // 
            lblReportBadge.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            lblReportBadge.Dock = System.Windows.Forms.DockStyle.Top;
            lblReportBadge.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblReportBadge.ForeColor = System.Drawing.Color.FromArgb(248, 250, 252);
            lblReportBadge.Location = new System.Drawing.Point(30, 20);
            lblReportBadge.Name = "lblReportBadge";
            lblReportBadge.Size = new System.Drawing.Size(840, 46);
            lblReportBadge.TabIndex = 0;
            lblReportBadge.Text = "📋 تقـريـر حضـور متـابعـة";
            lblReportBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelIdle
            // 
            panelIdle.Anchor = System.Windows.Forms.AnchorStyles.None;
            panelIdle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            panelIdle.Controls.Add(panelIdleStats);
            panelIdle.Controls.Add(lblIdleSubtitle);
            panelIdle.Controls.Add(lblIdleTitle);
            panelIdle.Controls.Add(lblIdleIcon);
            panelIdle.Location = new System.Drawing.Point(240, 60);
            panelIdle.Name = "panelIdle";
            panelIdle.Padding = new System.Windows.Forms.Padding(40);
            panelIdle.Size = new System.Drawing.Size(800, 520);
            panelIdle.TabIndex = 0;
            // 
            // panelIdleStats
            // 
            panelIdleStats.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            panelIdleStats.Controls.Add(lblStatCount);
            panelIdleStats.Controls.Add(lblStatTitle);
            panelIdleStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelIdleStats.Location = new System.Drawing.Point(40, 410);
            panelIdleStats.Name = "panelIdleStats";
            panelIdleStats.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            panelIdleStats.Size = new System.Drawing.Size(720, 70);
            panelIdleStats.TabIndex = 3;
            // 
            // lblStatCount
            // 
            lblStatCount.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatCount.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblStatCount.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblStatCount.Location = new System.Drawing.Point(20, 10);
            lblStatCount.Name = "lblStatCount";
            lblStatCount.Size = new System.Drawing.Size(440, 50);
            lblStatCount.TabIndex = 1;
            lblStatCount.Text = "0 عنصر";
            lblStatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTitle
            // 
            lblStatTitle.Dock = System.Windows.Forms.DockStyle.Right;
            lblStatTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblStatTitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblStatTitle.Location = new System.Drawing.Point(460, 10);
            lblStatTitle.Name = "lblStatTitle";
            lblStatTitle.Size = new System.Drawing.Size(240, 50);
            lblStatTitle.TabIndex = 0;
            lblStatTitle.Text = "إجمالي حضور اليوم:";
            lblStatTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdleSubtitle
            // 
            lblIdleSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblIdleSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F);
            lblIdleSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            lblIdleSubtitle.Location = new System.Drawing.Point(40, 230);
            lblIdleSubtitle.Name = "lblIdleSubtitle";
            lblIdleSubtitle.Size = new System.Drawing.Size(720, 70);
            lblIdleSubtitle.TabIndex = 2;
            lblIdleSubtitle.Text = "يرجى وضع إصبعك على جهاز البصمة لتسجيل الحضور\nواستعراض موعد المتابعة القادم تلقائياً";
            lblIdleSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdleTitle
            // 
            lblIdleTitle.Dock = System.Windows.Forms.DockStyle.Top;
            lblIdleTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            lblIdleTitle.ForeColor = System.Drawing.Color.White;
            lblIdleTitle.Location = new System.Drawing.Point(40, 170);
            lblIdleTitle.Name = "lblIdleTitle";
            lblIdleTitle.Size = new System.Drawing.Size(720, 60);
            lblIdleTitle.TabIndex = 1;
            lblIdleTitle.Text = "بانتظـار تسجيـل البصمـة";
            lblIdleTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIdleIcon
            // 
            lblIdleIcon.Dock = System.Windows.Forms.DockStyle.Top;
            lblIdleIcon.Font = new System.Drawing.Font("Segoe UI", 60F);
            lblIdleIcon.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblIdleIcon.Location = new System.Drawing.Point(40, 40);
            lblIdleIcon.Name = "lblIdleIcon";
            lblIdleIcon.Size = new System.Drawing.Size(720, 130);
            lblIdleIcon.TabIndex = 0;
            lblIdleIcon.Text = "👆";
            lblIdleIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerClock
            // 
            timerClock.Enabled = true;
            timerClock.Interval = 1000;
            timerClock.Tick += timerClock_Tick;
            // 
            // timerCountdown
            // 
            timerCountdown.Interval = 1000;
            timerCountdown.Tick += timerCountdown_Tick;
            // 
            // timerDbPoll
            // 
            timerDbPoll.Interval = 1500;
            timerDbPoll.Tick += timerDbPoll_Tick;
            // 
            // timerPulse
            // 
            timerPulse.Enabled = true;
            timerPulse.Interval = 1200;
            timerPulse.Tick += timerPulse_Tick;
            // 
            // AttendanceDisplayKioskForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            ClientSize = new System.Drawing.Size(1280, 800);
            Controls.Add(panelMainContainer);
            Controls.Add(panelTopBar);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            KeyPreview = true;
            Name = "AttendanceDisplayKioskForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "شاشة عرض الحضور اللحظي";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormClosing += AttendanceDisplayKioskForm_FormClosing;
            Load += AttendanceDisplayKioskForm_Load;
            KeyDown += AttendanceDisplayKioskForm_KeyDown;
            panelTopBar.ResumeLayout(false);
            panelTopCenter.ResumeLayout(false);
            panelTopLeft.ResumeLayout(false);
            panelTopRight.ResumeLayout(false);
            panelClockContainer.ResumeLayout(false);
            panelMainContainer.ResumeLayout(false);
            panelReport.ResumeLayout(false);
            panelReportCard.ResumeLayout(false);
            panelWantedAlertBox.ResumeLayout(false);
            panelNextDateBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
            panelIdle.ResumeLayout(false);
            panelIdleStats.ResumeLayout(false);
            ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTopBar;
        private System.Windows.Forms.Panel panelTopLeft;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Panel panelTopCenter;
        private System.Windows.Forms.Label lblStatusPill;
        private System.Windows.Forms.Panel panelTopRight;
        private System.Windows.Forms.Panel panelClockContainer;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnFullScreen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelMainContainer;
        private System.Windows.Forms.Panel panelIdle;
        private System.Windows.Forms.Label lblIdleIcon;
        private System.Windows.Forms.Label lblIdleTitle;
        private System.Windows.Forms.Label lblIdleSubtitle;
        private System.Windows.Forms.Panel panelIdleStats;
        private System.Windows.Forms.Label lblStatTitle;
        private System.Windows.Forms.Label lblStatCount;
        private System.Windows.Forms.Panel panelReport;
        private System.Windows.Forms.Panel panelReportCard;
        private System.Windows.Forms.Label lblReportBadge;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.Label lblElementName;
        private System.Windows.Forms.Label lblElementMeta;
        private System.Windows.Forms.Label lblAttendanceTime;
        private System.Windows.Forms.Panel panelNextDateBox;
        private System.Windows.Forms.Label lblNextDateHeader;
        private System.Windows.Forms.Label lblNextDateValue;
        private System.Windows.Forms.Label lblNextDateDays;
        private System.Windows.Forms.Panel panelWantedAlertBox;
        private System.Windows.Forms.Label lblWantedTitle;
        private System.Windows.Forms.Label lblWantedInstruction;
        private System.Windows.Forms.ProgressBar progressBarCountdown;
        private System.Windows.Forms.Label lblCountdownText;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Timer timerCountdown;
        private System.Windows.Forms.Timer timerDbPoll;
        private System.Windows.Forms.Timer timerPulse;
    }
}
