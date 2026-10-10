using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Code.Services;
using Follow_Extremist.Core;
using Follow_Extremist.Data;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public partial class FingerprintDeviceForm : Form
    {
        private readonly IDataHelper<FingerprintDevice> dataHelperDevice;
        private FingerprintDevice currentDevice;

        public FingerprintDeviceForm()
        {
            InitializeComponent();
            dataHelperDevice = (IDataHelper<FingerprintDevice>)ConfigurationObjectManager.GetObject("FingerprintDevice");
        }

        private async void FingerprintDeviceForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyDialogLayout(this);

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                currentDevice = devices?.FirstOrDefault();

                if (currentDevice != null)
                {
                    textBoxName.Text = currentDevice.DeviceName;
                    textBoxIp.Text = currentDevice.IpAddress;
                    textBoxPort.Text = currentDevice.Port.ToString();
                    textBoxMachineNum.Text = currentDevice.MachineNumber.ToString();
                    textBoxPassword.Text = currentDevice.CommPassword;
                    textBoxLocation.Text = currentDevice.Location;
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ في تحميل البيانات: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private async void buttonTestConnect_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "جاري محاولة الاتصال بالجهاز عبر الشبكة...";
            labelStatus.ForeColor = Color.FromArgb(37, 99, 235);
            buttonTestConnect.Enabled = false;

            try
            {
                var testDev = BuildDeviceFromInputs();
                using var zkService = new ZKDeviceService();

                bool ok = await zkService.ConnectAsync(testDev);
                if (ok)
                {
                    labelStatus.Text = "✓ تم الاتصال بجهاز البصمة بنجاح عبر الشبكة!";
                    labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                    zkService.Disconnect();
                }
                else
                {
                    labelStatus.Text = "✗ فشل الاتصال. تحقق من عنوان الـ IP والكيبل وتشغيل zkemkeeper.";
                    labelStatus.ForeColor = Color.FromArgb(220, 38, 38);
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ أثناء الاختبار: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonTestConnect.Enabled = true;
            }
        }

        private async void buttonSyncTime_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "جاري مزامنة وقت وتاريخ جهاز البصمة مع وقت الحاسوب...";
            labelStatus.ForeColor = Color.FromArgb(2, 132, 199);
            buttonSyncTime.Enabled = false;

            try
            {
                var testDev = BuildDeviceFromInputs();
                using var zkService = new ZKDeviceService();
                string detailedError = string.Empty;
                zkService.OnErrorOccurred += (s, err) => detailedError = err;

                if (await zkService.ConnectAsync(testDev))
                {
                    bool ok = await zkService.SyncDeviceTimeAsync();
                    var devTime = await zkService.GetDeviceTimeAsync();
                    zkService.Disconnect();
                    if (ok)
                    {
                        var timeStr = devTime.HasValue ? devTime.Value.ToString("yyyy/MM/dd  hh:mm:ss tt") : DateTime.Now.ToString("yyyy/MM/dd  hh:mm:ss tt");
                        labelStatus.Text = $"تمت المزامنة بنجاح! وقت الجهاز المحدث: {timeStr}";
                        labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                        MessageBox.Show($"تم ضبط وتحديث ساعة وتاريخ جهاز البصمة مع وقت الحاسوب بنجاح!\n\nالوقت الحالي: {timeStr}", "تمت المزامنة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        labelStatus.Text = "تم الاتصال ولكن تعذر ضبط وقت الجهاز.";
                        labelStatus.ForeColor = Color.OrangeRed;
                    }
                }
                else
                {
                    labelStatus.Text = string.IsNullOrEmpty(detailedError) ? "فشل الاتصال بالجهاز. تأكد من عنوان الـ IP والمنفذ." : detailedError;
                    labelStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonSyncTime.Enabled = true;
            }
        }

        private async void buttonClearLogs_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "هل أنت متأكد من مسح جميع سجلات الحضور المخزنة داخل ذاكرة جهاز البصمة؟\n(يُفضل إجراء مزامنة لجميع البصمات قبل المسح)",
                "تأكيد مسح الذاكرة",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            labelStatus.Text = "جاري مسح السجلات من الجهاز...";
            labelStatus.ForeColor = Color.FromArgb(220, 38, 38);

            try
            {
                var testDev = BuildDeviceFromInputs();
                using var zkService = new ZKDeviceService();

                if (await zkService.ConnectAsync(testDev))
                {
                    bool ok = await zkService.ClearLogsAsync();
                    zkService.Disconnect();
                    if (ok)
                    {
                        labelStatus.Text = "تم مسح سجلات الحضور من ذاكرة الجهاز بنجاح.";
                        labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                    }
                    else
                    {
                        labelStatus.Text = "فشل مسح السجلات من الجهاز.";
                        labelStatus.ForeColor = Color.Red;
                    }
                }
                else
                {
                    labelStatus.Text = "فشل الاتصال بالجهاز.";
                    labelStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIp.Text))
            {
                MessageBox.Show("يرجى إدخال عنوان IP الخاص بجهاز البصمة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool isNew = (currentDevice == null);
                if (isNew) currentDevice = new FingerprintDevice();

                currentDevice.DeviceName = textBoxName.Text.Trim();
                currentDevice.IpAddress = textBoxIp.Text.Trim();
                currentDevice.Port = int.TryParse(textBoxPort.Text.Trim(), out int port) ? port : 4370;
                currentDevice.MachineNumber = int.TryParse(textBoxMachineNum.Text.Trim(), out int mach) ? mach : 1;
                currentDevice.CommPassword = textBoxPassword.Text.Trim();
                currentDevice.Location = textBoxLocation.Text.Trim();
                currentDevice.IsEnabled = true;

                int res = isNew ? await dataHelperDevice.AddAsync(currentDevice) : await dataHelperDevice.EditAsync(currentDevice);
                if (res == 1)
                {
                    MessageBox.Show("تم حفظ إعدادات جهاز البصمة بنجاح", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء حفظ الإعدادات في قاعدة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"استثناء أثناء الحفظ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private FingerprintDevice BuildDeviceFromInputs()
        {
            return new FingerprintDevice
            {
                DeviceName = textBoxName.Text.Trim(),
                IpAddress = textBoxIp.Text.Trim(),
                Port = int.TryParse(textBoxPort.Text.Trim(), out int port) ? port : 4370,
                MachineNumber = int.TryParse(textBoxMachineNum.Text.Trim(), out int mach) ? mach : 1,
                CommPassword = textBoxPassword.Text.Trim(),
                Location = textBoxLocation.Text.Trim()
            };
        }
    }
}
