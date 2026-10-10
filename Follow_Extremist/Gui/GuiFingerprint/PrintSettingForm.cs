using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Code.Services;
using Follow_Extremist.Core;
using Follow_Extremist.Data;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public partial class PrintSettingForm : Form
    {
        private readonly IDataHelper<PrintSetting> dataHelperPrint;
        private PrintSetting currentSetting;

        public PrintSettingForm()
        {
            InitializeComponent();
            dataHelperPrint = (IDataHelper<PrintSetting>)ConfigurationObjectManager.GetObject("PrintSetting");

            // ربط أحداث النقر المباشر لأزرار الاختيار لضمان تفاعلها الفوري
            radioButtonBoth.Click += (s, e) => { radioButtonBoth.Checked = true; OutputMode_CheckedChanged(s, e); };
            radioButtonScreenOnly.Click += (s, e) => { radioButtonScreenOnly.Checked = true; OutputMode_CheckedChanged(s, e); };
            radioButtonPrintOnly.Click += (s, e) => { radioButtonPrintOnly.Checked = true; OutputMode_CheckedChanged(s, e); };
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadSettingsAsync();
        }

        private void PrintSettingForm_Load(object sender, EventArgs e)
        {
            // تم تنفيذ التحميل داخل OnLoad لتجنب التكرار والتضارب غير المتزامن
        }

        private async Task LoadSettingsAsync()
        {
            FormLayoutHelper.ApplyDialogLayout(this);

            radioButtonBoth.BringToFront();
            radioButtonScreenOnly.BringToFront();
            radioButtonPrintOnly.BringToFront();

            // Populate installed printers
            comboBoxPrinterName.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBoxPrinterName.Items.Add(printer);
            }

            try
            {
                var settings = await dataHelperPrint.GetAllDataAsync();
                currentSetting = settings?.FirstOrDefault();

                if (currentSetting != null)
                {
                    // Output Mode
                    string mode = currentSetting.OutputMode?.Trim();
                    if (string.Equals(mode, "ScreenOnly", StringComparison.OrdinalIgnoreCase))
                    {
                        radioButtonScreenOnly.Checked = true;
                        radioButtonBoth.Checked = false;
                        radioButtonPrintOnly.Checked = false;
                    }
                    else if (string.Equals(mode, "PrintOnly", StringComparison.OrdinalIgnoreCase))
                    {
                        radioButtonPrintOnly.Checked = true;
                        radioButtonBoth.Checked = false;
                        radioButtonScreenOnly.Checked = false;
                    }
                    else
                    {
                        radioButtonBoth.Checked = true;
                        radioButtonScreenOnly.Checked = false;
                        radioButtonPrintOnly.Checked = false;
                    }

                    numericScreenDuration.Minimum = 0;
                    numericScreenDuration.Maximum = 300;
                    numericScreenDuration.Value = Math.Max(0, Math.Min(300, currentSetting.ScreenDurationSeconds));

                    comboBoxPrinterType.SelectedIndex = currentSetting.PrinterType == "Thermal" ? 0 : 1;
                    if (!string.IsNullOrEmpty(currentSetting.PrinterName) && comboBoxPrinterName.Items.Contains(currentSetting.PrinterName))
                    {
                        comboBoxPrinterName.SelectedItem = currentSetting.PrinterName;
                    }
                    else if (comboBoxPrinterName.Items.Count > 0)
                    {
                        comboBoxPrinterName.SelectedIndex = 0;
                    }

                    comboBoxPaperWidth.SelectedIndex = currentSetting.PaperWidthMm == 58 ? 1 : 0;
                    numericCopies.Value = Math.Max(1, currentSetting.PrintCopies);
                    checkBoxAutoPrint.Checked = currentSetting.AutoPrintOnAttendance;
                    textBoxHeader.Text = currentSetting.HeaderText ?? "";
                    textBoxFooter.Text = currentSetting.FooterText ?? "";
                    checkBoxBarcode.Checked = currentSetting.ShowBarcode;
                    checkBoxNationalId.Checked = currentSetting.ShowNationalId;
                    checkBoxNextDate.Checked = currentSetting.ShowNextFollowDate;
                }
                else
                {
                    radioButtonBoth.Checked = true;
                    numericScreenDuration.Value = 12;
                    comboBoxPrinterType.SelectedIndex = 0;
                    comboBoxPaperWidth.SelectedIndex = 0;
                    if (comboBoxPrinterName.Items.Count > 0) comboBoxPrinterName.SelectedIndex = 0;
                }

                OutputMode_CheckedChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في قراءة إعدادات الطباعة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OutputMode_CheckedChanged(object sender, EventArgs e)
        {
            bool showScreen = !radioButtonPrintOnly.Checked;
            labelScreenDuration.Visible = showScreen;
            numericScreenDuration.Visible = showScreen;
            buttonDurationPlus.Visible = showScreen;
            buttonDurationMinus.Visible = showScreen;
            labelDurationUnit.Visible = showScreen;
            labelPresets.Visible = showScreen;
            buttonPreset5.Visible = showScreen;
            buttonPreset10.Visible = showScreen;
            buttonPreset12.Visible = showScreen;
            buttonPreset15.Visible = showScreen;
            buttonPreset30.Visible = showScreen;

            bool showHardware = !radioButtonScreenOnly.Checked;
            groupBoxHardware.Visible = showHardware;
            buttonTestPrint.Visible = showHardware;
        }

        private void buttonDurationPlus_Click(object sender, EventArgs e)
        {
            numericScreenDuration.Value = Math.Min(numericScreenDuration.Maximum, numericScreenDuration.Value + 1);
        }

        private void buttonDurationMinus_Click(object sender, EventArgs e)
        {
            numericScreenDuration.Value = Math.Max(numericScreenDuration.Minimum, numericScreenDuration.Value - 1);
        }

        private void buttonPreset_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag != null && int.TryParse(btn.Tag.ToString(), out int sec))
            {
                numericScreenDuration.Value = Math.Max(numericScreenDuration.Minimum, Math.Min(numericScreenDuration.Maximum, sec));
            }
        }

        private void comboBoxPrinterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isThermal = comboBoxPrinterType.SelectedIndex == 0;
            comboBoxPaperWidth.Enabled = isThermal;
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isNew = (currentSetting == null || currentSetting.Id == 0);
                if (currentSetting == null) currentSetting = new PrintSetting();

                if (radioButtonScreenOnly.Checked)
                {
                    currentSetting.OutputMode = "ScreenOnly";
                }
                else if (radioButtonPrintOnly.Checked)
                {
                    currentSetting.OutputMode = "PrintOnly";
                }
                else
                {
                    currentSetting.OutputMode = "Both";
                }

                currentSetting.ScreenDurationSeconds = (int)Math.Max(0, numericScreenDuration.Value);
                currentSetting.PrinterType = comboBoxPrinterType.SelectedIndex == 0 ? "Thermal" : "LaserA4";
                currentSetting.PrinterName = comboBoxPrinterName.SelectedItem?.ToString() ?? "";
                currentSetting.PaperWidthMm = comboBoxPaperWidth.SelectedIndex == 1 ? 58 : 80;
                currentSetting.PrintCopies = (int)numericCopies.Value;
                currentSetting.AutoPrintOnAttendance = checkBoxAutoPrint.Checked;
                currentSetting.HeaderText = textBoxHeader.Text.Trim();
                currentSetting.FooterText = textBoxFooter.Text.Trim();
                currentSetting.ShowBarcode = checkBoxBarcode.Checked;
                currentSetting.ShowNationalId = checkBoxNationalId.Checked;
                currentSetting.ShowNextFollowDate = checkBoxNextDate.Checked;

                int res = isNew ? await dataHelperPrint.AddAsync(currentSetting) : await dataHelperPrint.EditAsync(currentSetting);
                if (res == 1)
                {
                    AttendanceProcessor.InvalidatePrintSettingCache(currentSetting);

                    // تطبيق المدة فوراً على شاشة الكشك وشاشة الحضور بالبصمة
                    foreach (var kiosk in Application.OpenForms.OfType<AttendanceDisplayKioskForm>())
                    {
                        kiosk.UpdateScreenDuration(currentSetting.ScreenDurationSeconds);
                    }

                    FingerprintAttendanceUserControl.Instance()?.UpdateScreenDuration(currentSetting.ScreenDurationSeconds);

                    MessageBox.Show("تم حفظ إعدادات النظام بنجاح", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("حدث خطأ أثناء حفظ الإعدادات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"استثناء أثناء الحفظ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTestPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var setting = new PrintSetting
                {
                    PrinterType = comboBoxPrinterType.SelectedIndex == 0 ? "Thermal" : "LaserA4",
                    PrinterName = comboBoxPrinterName.SelectedItem?.ToString() ?? "",
                    PaperWidthMm = comboBoxPaperWidth.SelectedIndex == 1 ? 58 : 80,
                    PrintCopies = (int)numericCopies.Value,
                    HeaderText = textBoxHeader.Text.Trim(),
                    FooterText = textBoxFooter.Text.Trim(),
                    ShowBarcode = checkBoxBarcode.Checked,
                    ShowNationalId = checkBoxNationalId.Checked,
                    ShowNextFollowDate = checkBoxNextDate.Checked
                };

                var sampleElement = new ElementInfo
                {
                    Id = 1001,
                    ElementName = "محمد أحمد عبد الله (تجريبي)",
                    NationalId = "29001011234567",
                    Job = "أعمال حرة"
                };

                var sampleLog = new AttendanceLog
                {
                    Id = 1,
                    ElementId = 1001,
                    AttendanceDateTime = DateTime.Now,
                    VerifyType = 1,
                    NextFollowDateAssigned = DateTime.Now.AddDays(15),
                    Status = "تم بنجاح"
                };

                ThermalPrintService printer = new ThermalPrintService();
                printer.PrintAttendanceSlip(sampleLog, sampleElement, null, setting, isPreview: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء اختبار الطباعة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
