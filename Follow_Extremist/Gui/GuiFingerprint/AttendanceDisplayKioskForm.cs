using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Code.Services;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using Follow_Extremist.Data.SqlServer;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public partial class AttendanceDisplayKioskForm : Form
    {
        private readonly IDataHelper<AttendanceLog> dataHelperAttendanceLog;
        private readonly IDataHelper<ElementInfo> dataHelperElement;
        private readonly IDataHelper<ElementWantedStatus> dataHelperWanted;
        private readonly IDataHelper<FingerprintDevice> dataHelperDevice;
        private readonly IDataHelper<PrintSetting> dataHelperPrintSetting;
        private readonly ZKDeviceService zkService;
        private readonly AttendanceProcessor attendanceProcessor;

        private int lastSeenLogId = 0;
        private int maxCountdownSeconds = 12;
        private int countdownSeconds = 12;
        private string lastDisplayedKey = string.Empty;
        private bool isFullScreen = true;
        private readonly CultureInfo arabicCulture = new CultureInfo("ar-EG");
        private bool isPulseOn = false;

        public AttendanceDisplayKioskForm()
        {
            InitializeComponent();

            dataHelperAttendanceLog = (IDataHelper<AttendanceLog>)ConfigurationObjectManager.GetObject("AttendanceLog");
            dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperWanted = (IDataHelper<ElementWantedStatus>)ConfigurationObjectManager.GetObject("ElementWantedStatus");
            dataHelperDevice = (IDataHelper<FingerprintDevice>)ConfigurationObjectManager.GetObject("FingerprintDevice");
            dataHelperPrintSetting = (IDataHelper<PrintSetting>)ConfigurationObjectManager.GetObject("PrintSetting");
            attendanceProcessor = new AttendanceProcessor();

            zkService = new ZKDeviceService();
            zkService.OnAttendanceReceived += ZkService_OnAttendanceReceived;
            zkService.OnStatusChanged += ZkService_OnStatusChanged;
            zkService.OnErrorOccurred += ZkService_OnErrorOccurred;
        }

        private async void AttendanceDisplayKioskForm_Load(object sender, EventArgs e)
        {
            UpdateClock();
            SetFullScreen(true);

            // Ensure start state is Idle screen
            panelReport.Visible = false;
            panelIdle.Visible = true;
            panelIdle.BringToFront();

            // Initialize lastSeenLogId to current latest log so we only catch NEW check-ins
            lastSeenLogId = await GetMaxLogIdAsync();

            // Load configured screen display duration
            try
            {
                var settings = await dataHelperPrintSetting.GetAllDataAsync();
                var s = settings?.FirstOrDefault();
                if (s != null)
                {
                    maxCountdownSeconds = Math.Max(0, s.ScreenDurationSeconds);
                }
            }
            catch { }

            await RefreshTodayStatsAsync();
            await TryConnectDeviceAsync();

            timerDbPoll.Start();
        }

        private static string GetConnectionString()
        {
            if (!string.IsNullOrEmpty(SqlCon.SqlConnection))
                return SqlCon.SqlConnection;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.SqServerConString))
                return Properties.Settings.Default.SqServerConString;

            return @"Server=DESKTOP-2B87UHT\MSSQLSERVER2019;Database=FollowExtremistDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        private async Task<int> GetMaxLogIdAsync()
        {
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ISNULL(MAX(Id), 0) FROM AttendanceLog";
                var res = await cmd.ExecuteScalarAsync();
                return (res != null && int.TryParse(res.ToString(), out int id)) ? id : 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task RefreshTodayStatsAsync()
        {
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(1) FROM AttendanceLog WHERE CAST(AttendanceDateTime AS DATE) = CAST(GETDATE() AS DATE)";
                var res = await cmd.ExecuteScalarAsync();
                int todayCount = (res != null && int.TryParse(res.ToString(), out int cnt)) ? cnt : 0;
                lblStatCount.Text = $"{todayCount} عدد الحضور";
            }
            catch { }
        }

        private async Task TryConnectDeviceAsync()
        {
            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var dev = devices?.FirstOrDefault(x => x.IsEnabled);
                if (dev != null)
                {
                    lblStatusPill.Text = $"جاري الاتصال بـ {dev.DeviceName}...";
                    lblStatusPill.ForeColor = Color.FromArgb(245, 158, 11);
                    bool ok = await zkService.ConnectAsync(dev);
                    if (ok)
                    {
                        lblStatusPill.Text = $"● متصل: {dev.DeviceName} ({dev.IpAddress})";
                        lblStatusPill.ForeColor = Color.FromArgb(16, 185, 129);
                    }
                    else
                    {
                        lblStatusPill.Text = "● البصمة عبر الشبكة المركزية (جاهز)";
                        lblStatusPill.ForeColor = Color.FromArgb(56, 189, 248);
                    }
                }
                else
                {
                    lblStatusPill.Text = "● البصمة عبر الشبكة المركزية (جاهز)";
                    lblStatusPill.ForeColor = Color.FromArgb(56, 189, 248);
                }
            }
            catch
            {
                lblStatusPill.Text = "● مراقبة قاعدة البيانات المركزية";
                lblStatusPill.ForeColor = Color.FromArgb(56, 189, 248);
            }
        }

        private void ZkService_OnAttendanceReceived(object sender, AttendanceEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ZkService_OnAttendanceReceived(sender, e)));
                return;
            }

            _ = HandleElementAttendanceAsync(e.ElementId, e.AttendanceTime);
        }

        private void ZkService_OnStatusChanged(object sender, string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ZkService_OnStatusChanged(sender, msg)));
                return;
            }
            lblStatusPill.Text = $"● {msg}";
        }

        private void ZkService_OnErrorOccurred(object sender, string err)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ZkService_OnErrorOccurred(sender, err)));
                return;
            }
            lblStatusPill.Text = "● مراقبة الحضور عبر الشبكة المركزية";
            lblStatusPill.ForeColor = Color.FromArgb(56, 189, 248);
        }

        private async void timerDbPoll_Tick(object sender, EventArgs e)
        {
            try
            {
                int maxId = await GetMaxLogIdAsync();
                if (maxId <= 0 || maxId <= lastSeenLogId) return;

                if (lastSeenLogId <= 0)
                {
                    lastSeenLogId = maxId;
                    return;
                }

                // قراءة الحركات الجديدة فقط بدلاً من تحميل قاعدة البيانات بالكامل
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(GetConnectionString());
                await conn.OpenAsync();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT TOP 1 a.Id, a.ElementId, a.AttendanceDateTime, a.IsWantedAtTime, a.NextFollowDateAssigned,
             e.ElementName, e.NationalId, e.Job
FROM AttendanceLog a
INNER JOIN ElementInfo e ON a.ElementId = e.Id
WHERE a.Id > @lastSeenLogId
ORDER BY a.Id DESC;";
                cmd.Parameters.AddWithValue("@lastSeenLogId", lastSeenLogId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    lastSeenLogId = reader.GetInt32(0);
                    int elementId = reader.GetInt32(1);
                    DateTime attDate = reader.GetDateTime(2);
                    bool isWanted = reader.GetBoolean(3);
                    DateTime? nextDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                    var element = new ElementInfo
                    {
                        Id = elementId,
                        ElementName = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        NationalId = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        Job = reader.IsDBNull(7) ? "" : reader.GetString(7)
                    };

                    DisplayAttendanceReport(element, attDate, isWanted, nextDate);
                    await RefreshTodayStatsAsync();
                }
                else
                {
                    lastSeenLogId = maxId;
                }
            }
            catch { }
        }

        private async Task HandleElementAttendanceAsync(int elementId, DateTime attTime)
        {
            try
            {
                var res = await attendanceProcessor.ProcessAttendanceAsync(elementId, deviceId: null, attendanceTime: attTime);
                if (res.Element != null)
                {
                    DisplayAttendanceReport(
                        res.Element,
                        res.AttendanceLog?.AttendanceDateTime ?? attTime,
                        res.IsWanted,
                        res.NextFollowDate ?? (res.Element.DateFollowNow > DateTime.MinValue ? res.Element.DateFollowNow : (DateTime?)null),
                        res.Success,
                        res.Message);

                    if (res.Success)
                    {
                        await RefreshTodayStatsAsync();
                    }
                }
                else if (!string.IsNullOrEmpty(res.Message))
                {
                    lblStatusPill.Text = $"● {res.Message}";
                    lblStatusPill.ForeColor = Color.OrangeRed;
                    try { SystemSounds.Exclamation.Play(); } catch { }
                }
            }
            catch { }
        }

        public void DisplayAttendanceReport(ElementInfo element, DateTime attTime, bool isWanted, DateTime? nextFollowDate, bool isSuccess = true, string alertMessage = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => DisplayAttendanceReport(element, attTime, isWanted, nextFollowDate, isSuccess, alertMessage)));
                return;
            }

            string recordKey = $"{element.Id}_{attTime:yyyyMMddHHmmss}";
            if (recordKey == lastDisplayedKey && timerCountdown.Enabled)
            {
                // Exact same check-in event already active, keep countdown running
                return;
            }
            lastDisplayedKey = recordKey;

            lblElementName.Text = element.ElementName ?? "الأسم";
            lblElementMeta.Text = $"رقم القيد: #{element.Id}  |  الرقم القومي: {element.NationalId ?? "-"}  |  المهنة: {element.Job ?? "-"}";
            lblAttendanceTime.Text = $"⏱ وقت الحضور: {attTime:hh:mm:ss tt} - {attTime.ToString("dddd dd MMMM yyyy", arabicCulture)}";

            // Load element photo
            LoadElementPhoto(element);

            if (isWanted)
            {
                // === WANTED ALERT VIEW ===
                panelNextDateBox.Visible = false;
                panelWantedAlertBox.Visible = true;
                panelWantedAlertBox.BringToFront();

                lblReportBadge.BackColor = Color.FromArgb(185, 28, 28);
                lblReportBadge.Text = "⚠️ تقريـر حضـور متـابعـة:  مطـلـوب ⚠️";
                lblWantedTitle.Text = "🚨  مطـلـوب 🚨";
                lblWantedInstruction.Text = "يرجى اخطار الشخص المختص فوراً";

                try
                {
                    SystemSounds.Exclamation.Play();
                }
                catch { }
            }
            else if (!isSuccess && !string.IsNullOrEmpty(alertMessage))
            {
                // === WARNING / NOT TIME YET VIEW ===
                panelWantedAlertBox.Visible = true;
                panelWantedAlertBox.BringToFront();

                lblReportBadge.BackColor = Color.FromArgb(217, 119, 6);
                lblReportBadge.Text = "⚠️ تنبيـه حضـور متـابعـة ⚠️";
                lblWantedTitle.Text = "⚠️ تنبيه إداري";
                lblWantedInstruction.Text = alertMessage;

                if (nextFollowDate.HasValue)
                {
                    panelNextDateBox.Visible = true;
                    lblNextDateValue.Text = nextFollowDate.Value.ToString("dddd  dd  MMMM  yyyy", arabicCulture);
                    lblNextDateDays.Text = "موعد المتابعة المحدد القادم";
                }
                else
                {
                    panelNextDateBox.Visible = false;
                }

                try
                {
                    SystemSounds.Exclamation.Play();
                }
                catch { }
            }
            else
            {
                // === NORMAL SUCCESS VIEW ===
                panelWantedAlertBox.Visible = false;
                panelNextDateBox.Visible = true;
                panelNextDateBox.BringToFront();

                lblReportBadge.BackColor = Color.FromArgb(5, 150, 105);
                lblReportBadge.Text = "📋 تقـريـر حضـور متـابعـة";

                if (nextFollowDate.HasValue)
                {
                    lblNextDateValue.Text = nextFollowDate.Value.ToString("dddd  dd  MMMM  yyyy", arabicCulture);
                    int days = element.FollowDaysCount > 0 ? element.FollowDaysCount : 7;
                    lblNextDateDays.Text = $"✅ تم تسجيل حضورك بنجاح | دورية المتابعة القادمة: كل {days} أيام";
                }
                else
                {
                    lblNextDateValue.Text = "محدد وفقاً لجدول المتابعة";
                    lblNextDateDays.Text = "✅ تم تسجيل حضورك بنجاح";
                }
            }

            // Switch to Report view and reset countdown
            panelIdle.Visible = false;
            panelReport.Visible = true;
            panelReport.BringToFront();

            if (maxCountdownSeconds > 0)
            {
                countdownSeconds = maxCountdownSeconds;
                progressBarCountdown.Visible = true;
                progressBarCountdown.Maximum = maxCountdownSeconds;
                progressBarCountdown.Value = maxCountdownSeconds;
                lblCountdownText.Visible = true;
                lblCountdownText.Text = $"العودة لشاشة الانتظار خلال {countdownSeconds} ثانية...";
                timerCountdown.Stop();
                timerCountdown.Start();
            }
            else
            {
                // إذا كانت المدة 0: لا يتم تعديل الشاشة نهائياً وتفضل زي ما هي حتى البصمة التالية
                timerCountdown.Stop();
                progressBarCountdown.Visible = false;
                lblCountdownText.Visible = true;
                lblCountdownText.Text = "● شاشة العرض نشطة (بقاء دائم بدون إخفاء)";
            }
        }

        public void UpdateScreenDuration(int seconds)
        {
            maxCountdownSeconds = Math.Max(0, seconds);
            if (maxCountdownSeconds == 0)
            {
                timerCountdown.Stop();
                progressBarCountdown.Visible = false;
                lblCountdownText.Text = "● شاشة العرض نشطة (بقاء دائم بدون إخفاء)";
            }
            else if (timerCountdown.Enabled)
            {
                countdownSeconds = Math.Min(countdownSeconds, maxCountdownSeconds);
                progressBarCountdown.Visible = true;
                progressBarCountdown.Maximum = maxCountdownSeconds;
                progressBarCountdown.Value = Math.Max(0, Math.Min(maxCountdownSeconds, countdownSeconds));
                lblCountdownText.Text = $"العودة لشاشة الانتظار خلال {countdownSeconds} ثانية...";
            }
        }

        private void LoadElementPhoto(ElementInfo element)
        {
            try
            {
                if (element.ElementImage != null && element.ElementImage.Length > 0)
                {
                    using var ms = new MemoryStream(element.ElementImage);
                    pictureBoxPhoto.Image = Image.FromStream(ms);
                    pictureBoxPhoto.Visible = true;
                }
                else
                {
                    pictureBoxPhoto.Image = null;
                    pictureBoxPhoto.Visible = false;
                }
            }
            catch
            {
                pictureBoxPhoto.Image = null;
                pictureBoxPhoto.Visible = false;
            }
        }

        private void timerCountdown_Tick(object sender, EventArgs e)
        {
            countdownSeconds--;
            if (countdownSeconds <= 0)
            {
                timerCountdown.Stop();
                panelReport.Visible = false;
                panelIdle.Visible = true;
                panelIdle.BringToFront();
                lastDisplayedKey = string.Empty;
            }
            else
            {
                progressBarCountdown.Value = Math.Max(0, Math.Min(maxCountdownSeconds, countdownSeconds));
                lblCountdownText.Text = $"العودة لشاشة الانتظار خلال {countdownSeconds} ثوانٍ...";
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            var now = DateTime.Now;
            lblClock.Text = now.ToString("hh:mm:ss tt", arabicCulture);
            lblDate.Text = now.ToString("dddd  dd  MMMM  yyyy", arabicCulture);
        }

        private void timerPulse_Tick(object sender, EventArgs e)
        {
            isPulseOn = !isPulseOn;
            if (panelIdle.Visible)
            {
                lblIdleIcon.ForeColor = isPulseOn 
                    ? Color.FromArgb(16, 185, 129) 
                    : Color.FromArgb(5, 150, 105);
            }
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            SetFullScreen(!isFullScreen);
        }

        private void SetFullScreen(bool full)
        {
            isFullScreen = full;
            if (isFullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                btnFullScreen.Text = "⛶ عادي";
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.Size = new Size(1280, 800);
                this.CenterToScreen();
                btnFullScreen.Text = "⛶ F11";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            PromptClose();
        }

        private void AttendanceDisplayKioskForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                SetFullScreen(!isFullScreen);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PromptClose();
            }
        }

        private void PromptClose()
        {
            var confirm = MessageBox.Show(
                "هل تريد الخروج من شاشة العرض اللحظي والعودة إلى شاشة تسجيل الدخول؟",
                "تأكيد الخروج",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.FormClosing -= AttendanceDisplayKioskForm_FormClosing;
                timerDbPoll.Stop();
                timerCountdown.Stop();
                zkService.Dispose();

                var login = new GuiUsers.UsersLoginForm();
                login.Show();
                this.Close();
            }
        }

        private void AttendanceDisplayKioskForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerDbPoll.Stop();
            timerCountdown.Stop();
            zkService.Dispose();
        }
    }
}
