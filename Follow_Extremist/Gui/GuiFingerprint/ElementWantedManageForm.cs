using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Code.Services;
using Follow_Extremist.Core;
using Follow_Extremist.Data;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public partial class ElementWantedManageForm : Form
    {
        private readonly IDataHelper<ElementInfo> dataHelperElement;
        private readonly IDataHelper<ElementWantedStatus> dataHelperWanted;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;

        private List<ElementInfo> allElements = new();
        private ElementInfo selectedElement;
        private ElementWantedStatus currentWantedStatus;
        private int preselectedId = 0;

        public ElementWantedManageForm(int? initialElementId = null)
        {
            InitializeComponent();
            dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperWanted = (IDataHelper<ElementWantedStatus>)ConfigurationObjectManager.GetObject("ElementWantedStatus");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");

            if (initialElementId.HasValue)
            {
                preselectedId = initialElementId.Value;
            }

            searchableElementDropDown.SelectedElementChanged += SearchableElementDropDown_SelectedElementChanged;
        }

        private async void ElementWantedManageForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyDialogLayout(this);

            try
            {
                allElements = await dataHelperElement.GetAllDataAsync() ?? new List<ElementInfo>();
                searchableElementDropDown.SetElements(allElements);

                if (preselectedId > 0)
                {
                    var found = allElements.FirstOrDefault(x => x.Id == preselectedId);
                    if (found != null)
                    {
                        searchableElementDropDown.SelectedElement = found;
                    }
                }

                // إضافة زر تقرير المطلوبين أمنياً
                var buttonPrintTodayWanted = new Button
                {
                    Text = "🖨️ تقرير المطلوبين أمنياً",
                    BackColor = Color.FromArgb(185, 28, 28),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(190, 36),
                    Cursor = Cursors.Hand,
                    Dock = DockStyle.Right,
                    Margin = new Padding(8, 0, 8, 0)
                };
                buttonPrintTodayWanted.FlatAppearance.BorderSize = 0;
                buttonPrintTodayWanted.Click += async (s, ev) =>
                {
                    await WantedDailyReportService.ShowTodayWantedReportAsync();
                };
                panelFooter.Controls.Add(buttonPrintTodayWanted);
                buttonPrintTodayWanted.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل الاشخاص: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SearchableElementDropDown_SelectedElementChanged(object sender, ElementInfo element)
        {
            selectedElement = element;
            if (selectedElement == null)
            {
                labelElementDetails.Text = "بيانات الشخص: لم يتم تحديد شخص بعد";
                checkBoxIsWanted.Checked = false;
                textBoxReason.Text = string.Empty;
                textBoxWantedBy.Text = string.Empty;
                textBoxNotes.Text = string.Empty;
                groupBoxStatus.BackColor = Color.White;
                return;
            }

            labelElementDetails.Text = $"رقم القيد: #{selectedElement.Id} | الرقم القومي: {selectedElement.NationalId ?? "-"} | المهنة: {selectedElement.Job ?? "-"} | هاتف: {selectedElement.Mobile ?? selectedElement.Phone ?? "-"}";

            try
            {
                var wantedList = await dataHelperWanted.GetAllDataAsync();
                currentWantedStatus = wantedList?.FirstOrDefault(x => x.ElementId == selectedElement.Id);

                if (currentWantedStatus != null && currentWantedStatus.IsWanted)
                {
                    checkBoxIsWanted.Checked = true;
                    textBoxReason.Text = currentWantedStatus.WantedReason;
                    textBoxWantedBy.Text = currentWantedStatus.WantedBy;
                    textBoxNotes.Text = currentWantedStatus.Notes;
                    groupBoxStatus.BackColor = Color.FromArgb(254, 242, 242);
                }
                else
                {
                    checkBoxIsWanted.Checked = false;
                    textBoxReason.Text = string.Empty;
                    textBoxWantedBy.Text = string.Empty;
                    textBoxNotes.Text = string.Empty;
                    groupBoxStatus.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في استعلام حالة الطلب: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxIsWanted_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsWanted.Checked)
            {
                groupBoxStatus.BackColor = Color.FromArgb(254, 242, 242);
                textBoxReason.Focus();
            }
            else
            {
                groupBoxStatus.BackColor = Color.White;
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى اختيار اسم أولاً من القائمة المنسدلة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool isNew = (currentWantedStatus == null);
                if (isNew)
                {
                    currentWantedStatus = new ElementWantedStatus
                    {
                        ElementId = selectedElement.Id
                    };
                }

                currentWantedStatus.IsWanted = checkBoxIsWanted.Checked;
                currentWantedStatus.WantedReason = textBoxReason.Text.Trim();
                currentWantedStatus.WantedBy = textBoxWantedBy.Text.Trim();
                currentWantedStatus.Notes = textBoxNotes.Text.Trim();
                currentWantedStatus.WantedDate = checkBoxIsWanted.Checked ? DateTime.Now : null;

                int res = isNew
                    ? await dataHelperWanted.AddAsync(currentWantedStatus)
                    : await dataHelperWanted.EditAsync(currentWantedStatus);

                if (res == 1)
                {
                    if (dataHelperSystemRecords != null)
                    {
                        string action = checkBoxIsWanted.Checked ? "تعيين اسم كمطلوب" : "إلغاء طلب اسم";
                        await dataHelperSystemRecords.AddAsync(new SystemRecords
                        {
                            Title = action,
                            USerName = Properties.Settings.Default.UserName ?? "المسؤول",
                            Details = $"{action}: {selectedElement.ElementName} (رقم: {selectedElement.Id}) - السبب: {currentWantedStatus.WantedReason}",
                            AddedDate = DateTime.Now
                        });
                    }

                    string msg = checkBoxIsWanted.Checked
                        ? $"تم تسجيل الاسم [{selectedElement.ElementName}] كمطلوب بنجاح."
                        : $"تم إلغاء حالة الطلب عن الاسم [{selectedElement.ElementName}] بنجاح.";

                    MessageBox.Show(msg, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("فشل حفظ التعديلات في قاعدة البيانات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"استثناء أثناء الحفظ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxNotes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
