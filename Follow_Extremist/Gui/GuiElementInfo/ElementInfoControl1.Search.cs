using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Core;
using Follow_Extremist.Data.SqlServer;

namespace Follow_Extremist.Gui.GuiElementInfo
{
    [System.ComponentModel.DesignerCategory("Code")]
    public partial class ElementInfoControl1
    {
        #region Search Controls
        private Button buttonAdvancedSearch;
        private Panel panelAdvancedSearch;
        private TextBox textBoxSearchName;
        private TextBox textBoxSearchNationalId;
        private TextBox textBoxSearchMotherName;
        private TextBox textBoxSearchBirthPlace;
        private TextBox textBoxSearchJob;
        private TextBox textBoxSearchQualification;
        private TextBox textBoxSearchPhone;
        private TextBox textBoxSearchAddress;
        private TextBox textBoxSearchRegulatory;
        private TextBox textBoxSearchCase;
        private ComboBox comboBoxSearchPrison;
        private ComboBox comboBoxSearchFollowState;
        private TextBox textBoxSearchNotes;
        private CheckBox checkBoxSearchBirthDate;
        private DateTimePicker dateTimePickerBirthFrom;
        private DateTimePicker dateTimePickerBirthTo;
        private CheckBox checkBoxSearchDate;
        private DateTimePicker dateTimePickerSearchFrom;
        private DateTimePicker dateTimePickerSearchTo;
        private Button buttonExecuteAdvancedSearch;
        private Button buttonResetAdvancedSearch;
        private Button buttonCloseAdvancedSearch;
        private Label labelSearchResultsCount;
        #endregion

        private void InitializeAdvancedSearchPanel()
        {
            // 1. زر "بحث متقدم" في الشريط العلوي flowLayoutPanel1
            buttonAdvancedSearch = new Button
            {
                Name = "buttonAdvancedSearch",
                Text = "بحث متقدم",
                Size = new Size(160, 55),
                Margin = new Padding(5),
                Font = new Font("Cairo", 11F, FontStyle.Bold, GraphicsUnit.Point),
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.FromArgb(30, 41, 59),
                Image = Properties.Resources.Search,
                ImageAlign = ContentAlignment.MiddleLeft,
                TextAlign = ContentAlignment.MiddleRight,
                UseVisualStyleBackColor = true,
                Cursor = Cursors.Hand
            };
            buttonAdvancedSearch.Click += buttonAdvancedSearch_Click;

            // إضافة الزر إلى flowLayoutPanel1 بجانب panel1
            flowLayoutPanel1.Controls.Add(buttonAdvancedSearch);

            // 2. لوحة البحث المتقدم panelAdvancedSearch
            panelAdvancedSearch = new Panel
            {
                Name = "panelAdvancedSearch",
                Dock = DockStyle.Top,
                Height = 245,
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15, 8, 15, 8),
                Visible = false,
                RightToLeft = RightToLeft.Yes
            };

            var tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 8,
                RowCount = 5,
                RightToLeft = RightToLeft.Yes
            };

            // توزيع الأعمدة (تسمية 9% + حقل إدخال 16%) x 4 = 100%
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9F));  // 0
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F)); // 1
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9F));  // 2
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F)); // 3
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9F));  // 4
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F)); // 5
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9F));  // 6
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F)); // 7

            // توزيع الصفوف
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F)); // Row 0
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F)); // Row 1
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F)); // Row 2
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F)); // Row 3
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F)); // Row 4 (Action Bar)

            var fontLabel = new Font("Cairo", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
            var fontInput = new Font("Cairo", 9.5F, FontStyle.Regular, GraphicsUnit.Point);

            Label CreateLabel(string text)
            {
                return new Label
                {
                    Text = text,
                    Font = fontLabel,
                    ForeColor = Color.FromArgb(30, 41, 59),
                    TextAlign = ContentAlignment.MiddleRight,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(2, 2, 2, 2)
                };
            }

            TextBox CreateTextBox()
            {
                var tb = new TextBox
                {
                    Font = fontInput,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(2, 2, 6, 2)
                };
                tb.KeyDown += SearchTextBox_KeyDown;
                return tb;
            }

            // ================== الصف الأول ==================
            // 1. اسم العنصر
            tableLayout.Controls.Add(CreateLabel("اسم العنصر:"), 0, 0);
            textBoxSearchName = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchName, 1, 0);

            // 2. الرقم القومي
            tableLayout.Controls.Add(CreateLabel("الرقم القومي:"), 2, 0);
            textBoxSearchNationalId = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchNationalId, 3, 0);

            // 3. اسم الأم
            tableLayout.Controls.Add(CreateLabel("اسم الأم:"), 4, 0);
            textBoxSearchMotherName = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchMotherName, 5, 0);

            // 4. محل الميلاد
            tableLayout.Controls.Add(CreateLabel("محل الميلاد:"), 6, 0);
            textBoxSearchBirthPlace = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchBirthPlace, 7, 0);

            // ================== الصف الثاني ==================
            // 1. الوظيفة
            tableLayout.Controls.Add(CreateLabel("الوظيفة:"), 0, 1);
            textBoxSearchJob = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchJob, 1, 1);

            // 2. المؤهل
            tableLayout.Controls.Add(CreateLabel("المؤهل:"), 2, 1);
            textBoxSearchQualification = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchQualification, 3, 1);

            // 3. تليفون / محمول
            tableLayout.Controls.Add(CreateLabel("تليفون / محمول:"), 4, 1);
            textBoxSearchPhone = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchPhone, 5, 1);

            // 4. العنوان
            tableLayout.Controls.Add(CreateLabel("العنوان:"), 6, 1);
            textBoxSearchAddress = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchAddress, 7, 1);

            // ================== الصف الثالث ==================
            // 1. الوضع التنظيمي
            tableLayout.Controls.Add(CreateLabel("الوضع التنظيمي:"), 0, 2);
            textBoxSearchRegulatory = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchRegulatory, 1, 2);

            // 2. بيانات القضية
            tableLayout.Controls.Add(CreateLabel("بيانات القضية:"), 2, 2);
            textBoxSearchCase = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchCase, 3, 2);

            // 3. حالة العنصر
            tableLayout.Controls.Add(CreateLabel("حالة العنصر:"), 4, 2);
            comboBoxSearchPrison = new ComboBox
            {
                Font = fontInput,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Margin = new Padding(2, 2, 6, 2)
            };
            comboBoxSearchPrison.Items.AddRange(new object[] { "الكل", "محبوس", "مفرج عنه" });
            comboBoxSearchPrison.SelectedIndex = 0;
            comboBoxSearchPrison.KeyDown += SearchTextBox_KeyDown;
            tableLayout.Controls.Add(comboBoxSearchPrison, 5, 2);

            // 4. حالة المتابعة
            tableLayout.Controls.Add(CreateLabel("حالة المتابعة:"), 6, 2);
            comboBoxSearchFollowState = new ComboBox
            {
                Font = fontInput,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill,
                Margin = new Padding(2, 2, 6, 2)
            };
            comboBoxSearchFollowState.Items.AddRange(new object[] { "الكل", "داخل المتابعة", "خارج المتابعة" });
            comboBoxSearchFollowState.SelectedIndex = 0;
            comboBoxSearchFollowState.KeyDown += SearchTextBox_KeyDown;
            tableLayout.Controls.Add(comboBoxSearchFollowState, 7, 2);

            // ================== الصف الرابع ==================
            // 1. ملاحظات
            tableLayout.Controls.Add(CreateLabel("ملاحظات:"), 0, 3);
            textBoxSearchNotes = CreateTextBox();
            tableLayout.Controls.Add(textBoxSearchNotes, 1, 3);

            // 2. فلترة تاريخ الميلاد من - إلى (يمتد على الأعمدة 2 و 3 و 4)
            var panelBirthDate = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 5,
                RightToLeft = RightToLeft.Yes,
                Margin = new Padding(0)
            };
            panelBirthDate.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // [ ] تاريخ الميلاد:
            panelBirthDate.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // من:
            panelBirthDate.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F)); // تاريخ من
            panelBirthDate.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // إلى:
            panelBirthDate.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F)); // تاريخ إلى
            panelBirthDate.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            checkBoxSearchBirthDate = new CheckBox
            {
                Text = "تاريخ الميلاد:",
                Font = fontLabel,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 3)
            };

            var labelBirthFrom = new Label
            {
                Text = "من:",
                Font = fontLabel,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 3)
            };

            dateTimePickerBirthFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = fontInput,
                Width = 105,
                Value = new DateTime(1960, 1, 1),
                Enabled = false,
                Margin = new Padding(2, 4, 2, 3)
            };

            var labelBirthTo = new Label
            {
                Text = "إلى:",
                Font = fontLabel,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 3)
            };

            dateTimePickerBirthTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = fontInput,
                Width = 105,
                Value = DateTime.Today,
                Enabled = false,
                Margin = new Padding(2, 4, 2, 3)
            };

            checkBoxSearchBirthDate.CheckedChanged += (s, e) =>
            {
                dateTimePickerBirthFrom.Enabled = checkBoxSearchBirthDate.Checked;
                dateTimePickerBirthTo.Enabled = checkBoxSearchBirthDate.Checked;
            };

            panelBirthDate.Controls.Add(checkBoxSearchBirthDate, 0, 0);
            panelBirthDate.Controls.Add(labelBirthFrom, 1, 0);
            panelBirthDate.Controls.Add(dateTimePickerBirthFrom, 2, 0);
            panelBirthDate.Controls.Add(labelBirthTo, 3, 0);
            panelBirthDate.Controls.Add(dateTimePickerBirthTo, 4, 0);
            tableLayout.SetColumnSpan(panelBirthDate, 3);
            tableLayout.Controls.Add(panelBirthDate, 2, 3);

            // 3. فلترة تاريخ المتابعة من - إلى (يمتد على الأعمدة 5 و 6 و 7)
            var panelFollowDate = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 5,
                RightToLeft = RightToLeft.Yes,
                Margin = new Padding(0)
            };
            panelFollowDate.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // [ ] تاريخ المتابعة:
            panelFollowDate.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // من:
            panelFollowDate.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F)); // تاريخ من
            panelFollowDate.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));       // إلى:
            panelFollowDate.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F)); // تاريخ إلى
            panelFollowDate.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            checkBoxSearchDate = new CheckBox
            {
                Text = "تاريخ المتابعة:",
                Font = fontLabel,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 3)
            };

            var labelFollowFrom = new Label
            {
                Text = "من:",
                Font = fontLabel,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 3)
            };

            dateTimePickerSearchFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = fontInput,
                Width = 105,
                Value = DateTime.Today,
                Enabled = false,
                Margin = new Padding(2, 4, 2, 3)
            };

            var labelFollowTo = new Label
            {
                Text = "إلى:",
                Font = fontLabel,
                AutoSize = true,
                Margin = new Padding(2, 6, 2, 3)
            };

            dateTimePickerSearchTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = fontInput,
                Width = 105,
                Value = DateTime.Today,
                Enabled = false,
                Margin = new Padding(2, 4, 2, 3)
            };

            checkBoxSearchDate.CheckedChanged += (s, e) =>
            {
                dateTimePickerSearchFrom.Enabled = checkBoxSearchDate.Checked;
                dateTimePickerSearchTo.Enabled = checkBoxSearchDate.Checked;
            };

            panelFollowDate.Controls.Add(checkBoxSearchDate, 0, 0);
            panelFollowDate.Controls.Add(labelFollowFrom, 1, 0);
            panelFollowDate.Controls.Add(dateTimePickerSearchFrom, 2, 0);
            panelFollowDate.Controls.Add(labelFollowTo, 3, 0);
            panelFollowDate.Controls.Add(dateTimePickerSearchTo, 4, 0);
            tableLayout.SetColumnSpan(panelFollowDate, 3);
            tableLayout.Controls.Add(panelFollowDate, 5, 3);

            // ================== الصف الخامس: شريط الإجراءات والنتائج ==================
            var panelActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Margin = new Padding(0, 4, 0, 0)
            };

            buttonExecuteAdvancedSearch = new Button
            {
                Text = "بحث 🔍",
                Font = new Font("Cairo", 10F, FontStyle.Bold, GraphicsUnit.Point),
                BackColor = Color.FromArgb(37, 99, 235), // Primary Blue
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 36),
                Margin = new Padding(4, 2, 4, 2),
                Cursor = Cursors.Hand
            };
            buttonExecuteAdvancedSearch.FlatAppearance.BorderSize = 0;
            buttonExecuteAdvancedSearch.Click += buttonExecuteAdvancedSearch_Click;

            buttonResetAdvancedSearch = new Button
            {
                Text = "تفريغ الفلاتر 🔄",
                Font = new Font("Cairo", 10F, FontStyle.Bold, GraphicsUnit.Point),
                BackColor = Color.FromArgb(100, 116, 139), // Slate
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 36),
                Margin = new Padding(4, 2, 4, 2),
                Cursor = Cursors.Hand
            };
            buttonResetAdvancedSearch.FlatAppearance.BorderSize = 0;
            buttonResetAdvancedSearch.Click += buttonResetAdvancedSearch_Click;

            buttonCloseAdvancedSearch = new Button
            {
                Text = "إغلاق ✖",
                Font = new Font("Cairo", 10F, FontStyle.Bold, GraphicsUnit.Point),
                BackColor = Color.FromArgb(226, 232, 240),
                ForeColor = Color.FromArgb(71, 85, 105),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 36),
                Margin = new Padding(4, 2, 4, 2),
                Cursor = Cursors.Hand
            };
            buttonCloseAdvancedSearch.FlatAppearance.BorderSize = 0;
            buttonCloseAdvancedSearch.Click += (s, e) =>
            {
                panelAdvancedSearch.Visible = false;
                buttonAdvancedSearch.BackColor = Color.WhiteSmoke;
                buttonAdvancedSearch.ForeColor = Color.FromArgb(30, 41, 59);
            };

            labelSearchResultsCount = new Label
            {
                Text = "",
                Font = new Font("Cairo", 10.5F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(15, 23, 42),
                AutoSize = true,
                Margin = new Padding(15, 6, 5, 2)
            };

            panelActions.Controls.Add(buttonExecuteAdvancedSearch);
            panelActions.Controls.Add(buttonResetAdvancedSearch);
            panelActions.Controls.Add(buttonCloseAdvancedSearch);
            panelActions.Controls.Add(labelSearchResultsCount);

            tableLayout.SetColumnSpan(panelActions, 8);
            tableLayout.Controls.Add(panelActions, 0, 4);

            panelAdvancedSearch.Controls.Add(tableLayout);

            // إضافة لوحة البحث المتقدم إلى Control
            Controls.Add(panelAdvancedSearch);

            // ترتيب الـ Docking الصحيح
            flowLayoutPanel1.SendToBack();
            panelAdvancedSearch.BringToFront();
            gridControl1.BringToFront();
        }

        private void buttonAdvancedSearch_Click(object sender, EventArgs e)
        {
            panelAdvancedSearch.Visible = !panelAdvancedSearch.Visible;
            if (panelAdvancedSearch.Visible)
            {
                buttonAdvancedSearch.BackColor = Color.FromArgb(224, 231, 255);
                buttonAdvancedSearch.ForeColor = Color.FromArgb(30, 58, 138);
                textBoxSearchName.Focus();
            }
            else
            {
                buttonAdvancedSearch.BackColor = Color.WhiteSmoke;
                buttonAdvancedSearch.ForeColor = Color.FromArgb(30, 41, 59);
            }

            // تأكيد ترتيب الـ Docking
            flowLayoutPanel1.SendToBack();
            gridControl1.BringToFront();
        }

        private async void buttonExecuteAdvancedSearch_Click(object sender, EventArgs e)
        {
            await ExecuteAdvancedSearch();
        }

        private void buttonResetAdvancedSearch_Click(object sender, EventArgs e)
        {
            ResetAdvancedSearchFields();
            LoadData();
        }

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await ExecuteAdvancedSearch();
            }
        }

        public async Task ExecuteAdvancedSearch()
        {
            loadingForm.Show();
            try
            {
                var criteria = new ElementSearchCriteria
                {
                    ElementName = textBoxSearchName?.Text,
                    NationalId = textBoxSearchNationalId?.Text,
                    MotherName = textBoxSearchMotherName?.Text,
                    BirthPlace = textBoxSearchBirthPlace?.Text,
                    Job = textBoxSearchJob?.Text,
                    Qualification = textBoxSearchQualification?.Text,
                    PhoneOrMobile = textBoxSearchPhone?.Text,
                    Address = textBoxSearchAddress?.Text,
                    RegulatoryStatus = textBoxSearchRegulatory?.Text,
                    CaseData = textBoxSearchCase?.Text,
                    PrisonedOrnot = comboBoxSearchPrison?.SelectedItem?.ToString(),
                    FollowState = comboBoxSearchFollowState?.SelectedItem?.ToString(),
                    Notes = textBoxSearchNotes?.Text,
                    UseBirthDateFilter = checkBoxSearchBirthDate?.Checked ?? false,
                    BirthDateFrom = checkBoxSearchBirthDate?.Checked == true ? dateTimePickerBirthFrom?.Value : (DateTime?)null,
                    BirthDateTo = checkBoxSearchBirthDate?.Checked == true ? dateTimePickerBirthTo?.Value : (DateTime?)null,
                    UseDateFilter = checkBoxSearchDate?.Checked ?? false,
                    DateFollowFrom = checkBoxSearchDate?.Checked == true ? dateTimePickerSearchFrom?.Value : (DateTime?)null,
                    DateFollowTo = checkBoxSearchDate?.Checked == true ? dateTimePickerSearchTo?.Value : (DateTime?)null
                };

                var entity = (dataHelper as ElementInfoEntity) ?? new ElementInfoEntity();
                var results = await entity.AdvancedSearchAsync(criteria);
                allElements = results?.Where(x => x.FollowState == null || x.FollowState.Trim() != "خارج المتابعة").ToList() ?? new List<ElementInfo>();

                var pageData = paginationControl.GetPageData(allElements, resetToFirstPage: true);
                gridControl1.DataSource = pageData;
                SetColumnsTitleElement1();
                gridControl1.BringToFront();
                if (labelSearchResultsCount != null)
                {
                    labelSearchResultsCount.Text = $"عدد النتائج المطابقة: {allElements.Count}";
                }
                else
                {
                    if (labelSearchResultsCount != null)
                    {
                        labelSearchResultsCount.Text = "عدد النتائج: 0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء البحث: {ex.Message}");
            }
            finally
            {
                loadingForm.Hide();
            }
        }

        public void ResetAdvancedSearchFields()
        {
            if (textBoxSearchName != null) textBoxSearchName.Text = string.Empty;
            if (textBoxSearchNationalId != null) textBoxSearchNationalId.Text = string.Empty;
            if (textBoxSearchMotherName != null) textBoxSearchMotherName.Text = string.Empty;
            if (textBoxSearchBirthPlace != null) textBoxSearchBirthPlace.Text = string.Empty;
            if (textBoxSearchJob != null) textBoxSearchJob.Text = string.Empty;
            if (textBoxSearchQualification != null) textBoxSearchQualification.Text = string.Empty;
            if (textBoxSearchPhone != null) textBoxSearchPhone.Text = string.Empty;
            if (textBoxSearchAddress != null) textBoxSearchAddress.Text = string.Empty;
            if (textBoxSearchRegulatory != null) textBoxSearchRegulatory.Text = string.Empty;
            if (textBoxSearchCase != null) textBoxSearchCase.Text = string.Empty;
            if (comboBoxSearchPrison != null) comboBoxSearchPrison.SelectedIndex = 0;
            if (comboBoxSearchFollowState != null) comboBoxSearchFollowState.SelectedIndex = 0;
            if (textBoxSearchNotes != null) textBoxSearchNotes.Text = string.Empty;
            if (checkBoxSearchBirthDate != null)
            {
                checkBoxSearchBirthDate.Checked = false;
                dateTimePickerBirthFrom.Value = new DateTime(1960, 1, 1);
                dateTimePickerBirthTo.Value = DateTime.Today;
            }
            if (checkBoxSearchDate != null)
            {
                checkBoxSearchDate.Checked = false;
                dateTimePickerSearchFrom.Value = DateTime.Today;
                dateTimePickerSearchTo.Value = DateTime.Today;
            }
            if (labelSearchResultsCount != null) labelSearchResultsCount.Text = string.Empty;
        }
    }
}
