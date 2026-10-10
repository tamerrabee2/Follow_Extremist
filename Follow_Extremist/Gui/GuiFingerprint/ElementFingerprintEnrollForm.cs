using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Code;
using Follow_Extremist.Code.Services;
using Follow_Extremist.Core;
using Follow_Extremist.Data;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public partial class ElementFingerprintEnrollForm : Form
    {
        private readonly IDataHelper<ElementInfo> dataHelperElement;
        private readonly IDataHelper<ElementFingerprint> dataHelperFingerprint;
        private readonly IDataHelper<FingerprintDevice> dataHelperDevice;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;

        private List<ElementInfo> allElements = new();
        private ElementInfo selectedElement;
        private int preselectedId = 0;

        public ElementFingerprintEnrollForm(int? initialElementId = null)
        {
            InitializeComponent();
            dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperFingerprint = (IDataHelper<ElementFingerprint>)ConfigurationObjectManager.GetObject("ElementFingerprint");
            dataHelperDevice = (IDataHelper<FingerprintDevice>)ConfigurationObjectManager.GetObject("FingerprintDevice");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");

            if (initialElementId.HasValue)
            {
                preselectedId = initialElementId.Value;
            }

            searchableElementDropDown.SelectedElementChanged += SearchableElementDropDown_SelectedElementChanged;
            SetupGridStyling();
        }

        private void SetupGridStyling()
        {
            dataGridViewFingerprints.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dataGridViewFingerprints.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewFingerprints.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dataGridViewFingerprints.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewFingerprints.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dataGridViewFingerprints.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewFingerprints.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewFingerprints.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
            dataGridViewFingerprints.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        private async void ElementFingerprintEnrollForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyDialogLayout(this);
            SetupToolTips();
            comboBoxFinger.SelectedIndex = 1; // Default: سبابة يمنى

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل العناصر: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupToolTips()
        {
            var toolTip = new ToolTip { ShowAlways = true, AutoPopDelay = 9000, InitialDelay = 350, ReshowDelay = 200 };
            toolTip.SetToolTip(buttonSyncUserName, "الخطوة 1: إنشاء كود الماكينة الموحد وإرسال اسم وبيانات الشخص إلى جهاز البصمة");
            toolTip.SetToolTip(buttonStartRemoteEnroll, "الخطوة 2: تشغيل وضع التسجيل المباشر على حساس الجهاز للإصبع المحدد (3 مرات متتالية)");
            toolTip.SetToolTip(buttonFetchFromDevice, "الخطوة 3: سحب قالب بصمة الإصبع المسجل من الماكينة وحفظه في قاعدة البيانات المركزية");
            toolTip.SetToolTip(buttonFetchAllFingers, "فحص وسحب كافة البصمات (0-9) المسجلة لهذا الشخص من الماكينة وحفظها معاً");
            toolTip.SetToolTip(buttonUploadToDevice, "رفع قوالب البصمات المسجلة للشخص من قاعدة البيانات إلى جهاز البصمة");
            toolTip.SetToolTip(buttonAutoMatchDeviceUsers, "فحص ومطابقة جميع مستخدمي الماكينة تلقائياً وربطهم بالأشخاص في قاعدة البيانات");
            toolTip.SetToolTip(buttonBulkSyncAll, "مزامنة ورفع جميع الأشخاص وبصماتهم من قاعدة البيانات إلى الماكينة دفعة واحدة");
            toolTip.SetToolTip(buttonDeleteFingerprint, "حذف قالب البصمة المحددة في الجدول من قاعدة البيانات والجهاز");
            toolTip.SetToolTip(buttonDeleteUserFromDevice, "مسح المستخدم وكافة بصماته نهائياً من ذاكرة جهاز البصمة");
            toolTip.SetToolTip(buttonClose, "إغلاق هذه النافذة");
        }

        private async void SearchableElementDropDown_SelectedElementChanged(object sender, ElementInfo element)
        {
            selectedElement = element;
            if (selectedElement == null)
            {
                labelElementDetails.Text = "بيانات الشخص: لم يتم تحديد الأسم بعد";
                dataGridViewFingerprints.Rows.Clear();
                return;
            }

            UpdateElementDetailsLabel();
            await RefreshFingerprintsGrid();
        }

        private void UpdateElementDetailsLabel()
        {
            if (selectedElement == null) return;
            string enrollStr = selectedElement.DeviceEnrollId.HasValue && selectedElement.DeviceEnrollId.Value > 0
                ? $"كود الماكينة الموحد: #{selectedElement.DeviceEnrollId.Value}"
                : "كود الماكينة: (لم يحدد - سيتم تعيين كود تلقائي من الماكينة)";

            labelElementDetails.Text = $"رقم القيد المحلي: #{selectedElement.Id} | {enrollStr} | الرقم القومي: {selectedElement.NationalId ?? "-"} | المهنة: {selectedElement.Job ?? "-"}";
        }

        private async Task<int> EnsureElementEnrollIdAsync(ZKDeviceService zkService)
        {
            if (selectedElement == null) return 0;
            if (selectedElement.DeviceEnrollId.HasValue && selectedElement.DeviceEnrollId.Value > 0)
            {
                return selectedElement.DeviceEnrollId.Value;
            }

            // فحص ذكي: هل الشخص مسجل بالفعل على جهاز البصمة مسبقاً؟
            try
            {
                var deviceUsers = await zkService.GetAllDeviceUsersWithFingerprintsAsync();
                if (deviceUsers != null && deviceUsers.Count > 0)
                {
                    // 1. مطابقة بالاسم
                    var match = deviceUsers.FirstOrDefault(u =>
                        !string.IsNullOrEmpty(u.Name) &&
                        (string.Equals(u.Name.Trim(), selectedElement.ElementName?.Trim(), StringComparison.OrdinalIgnoreCase) ||
                         (selectedElement.ElementName != null && selectedElement.ElementName.Contains(u.Name.Trim()))));

                    // 2. مطابقة بالرقم القومي
                    if (string.IsNullOrEmpty(match.EnrollNum) && !string.IsNullOrEmpty(selectedElement.NationalId))
                    {
                        match = deviceUsers.FirstOrDefault(u => u.EnrollNum == selectedElement.NationalId.Trim());
                    }

                    if (!string.IsNullOrEmpty(match.EnrollNum) && int.TryParse(match.EnrollNum, out int matchedId) && matchedId > 0)
                    {
                        selectedElement.DeviceEnrollId = matchedId;
                        await dataHelperElement.EditAsync(selectedElement);
                        var cached = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
                        if (cached != null) cached.DeviceEnrollId = matchedId;
                        UpdateElementDetailsLabel();
                        return matchedId;
                    }
                }
            }
            catch { }

            // الاستعلام عن أعلى كود مسجل على الماكينة + 1 مع حماية قاعدة البيانات
            int nextId = await zkService.GetNextAvailableEnrollIdAsync();
            selectedElement.DeviceEnrollId = nextId;
            await dataHelperElement.EditAsync(selectedElement);

            var cachedElem = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
            if (cachedElem != null) cachedElem.DeviceEnrollId = nextId;

            UpdateElementDetailsLabel();
            return nextId;
        }

        private async Task RefreshFingerprintsGrid()
        {
            if (selectedElement == null) return;

            try
            {
                var allFps = await dataHelperFingerprint.GetAllDataAsync();
                var elementFps = allFps?.Where(x => x.ElementId == selectedElement.Id).ToList() ?? new List<ElementFingerprint>();

                dataGridViewFingerprints.Rows.Clear();
                foreach (var fp in elementFps)
                {
                    dataGridViewFingerprints.Rows.Add(fp.Id, fp.FingerName, fp.CreatedDate.ToString("yyyy/MM/dd  hh:mm tt"));
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ في تحميل البصمات: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private async void buttonStartRemoteEnroll_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى اختيار اسم أولاً من القائمة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int fingerIdx = comboBoxFinger.SelectedIndex;
            string fingerName = comboBoxFinger.SelectedItem?.ToString() ?? "سبابة يمنى";

            labelStatus.Text = "جاري إعداد الشخص وإرسال أمر التسجيل المباشر لجهاز البصمة...";
            labelStatus.ForeColor = Color.FromArgb(124, 58, 237);
            buttonStartRemoteEnroll.Enabled = false;

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم تهيئة جهاز بصمة نشط في الإعدادات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                // 1. ضمان كود الماكينة الموحد DeviceEnrollId
                int enrollId = await EnsureElementEnrollIdAsync(zkService);

                // 2. مزامنة اسم وكود العنصر الموحد إلى الماكينة أولاً
                await zkService.UploadUserAsync(enrollId, selectedElement.ElementName);

                // 3. إرسال أمر التسجيل المباشر لجهاز البصمة بكود الماكينة الموحد
                bool ok = await zkService.StartEnrollAsync(enrollId, fingerIdx);
                zkService.Disconnect();

                if (ok)
                {
                    labelStatus.Text = $"جهاز البصمة في وضع التسجيل المباشر للإصبع [{fingerName}] بكود الماكينة (#{enrollId}). ضع الإصبع 3 مرات.";
                    labelStatus.ForeColor = Color.FromArgb(5, 150, 105);

                    MessageBox.Show(
                        $"تم إرسال أمر التسجيل لجهاز البصمة بنجاح!\n\n" +
                        $"الاسم: {selectedElement.ElementName}\n" +
                        $"كود الماكينة الموحد: #{enrollId}\n" +
                        $"رقم القيد المحلي: #{selectedElement.Id}\n" +
                        $"الإصبع المطلوب: {fingerName}\n\n" +
                        "يرجى التوجه للجهاز ووضع الإصبع (3) مرات متتالية حتى يؤكد الجهاز نجاح التسجيل،\n" +
                        "ثم اضغط على زر '📥 3. سحب بصمة الإصبع المحدد' لحفظ القالب المركزي بقاعدة البيانات.",
                        "جهاز البصمة في وضع التسجيل المباشر", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    labelStatus.Text = "لم يستجب الجهاز لأمر بدء التسجيل المباشر.";
                    labelStatus.ForeColor = Color.OrangeRed;
                    MessageBox.Show(
                        "تعذر تشغيل وضع التسجيل المباشر على الجهاز عن بعد.\n" +
                        $"يمكنك تسجيل البصمة يدوياً من قائمة المستخدمين على الماكينة مباشرة بكود الماكينة الموحد (#{enrollId})، ثم الضغط على 'سحب بصمة الإصبع المحدد'.",
                        "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonStartRemoteEnroll.Enabled = true;
            }
        }

        private async void buttonSyncUserName_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى اختيار اسم أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            labelStatus.Text = "جاري مزامنة اسم وبيانات الشخص إلى الماكينة...";
            labelStatus.ForeColor = Color.FromArgb(2, 132, 199);
            buttonSyncUserName.Enabled = false;

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم تهيئة جهاز بصمة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                int enrollId = await EnsureElementEnrollIdAsync(zkService);
                bool ok = await zkService.UploadUserAsync(enrollId, selectedElement.ElementName);
                zkService.Disconnect();

                if (ok)
                {
                    labelStatus.Text = $"تمت مزامنة بيانات الشخص [{selectedElement.ElementName}] بكود ماكينة (#{enrollId}) بنجاح!";
                    labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                    MessageBox.Show(
                        $"تم تسجيل وتحديث بيانات الشخص في الماكينة بنجاح:\n\n" +
                        $"الاسم: {selectedElement.ElementName}\n" +
                        $"كود الماكينة الموحد: #{enrollId}\n" +
                        $"رقم القيد المحلي: #{selectedElement.Id}\n\n" +
                        "سيظهر اسم الشخص الآن على شاشة الجهاز عند الحضور.",
                        "تمت المزامنة بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    labelStatus.Text = "فشلت المزامنة مع الماكينة.";
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
                buttonSyncUserName.Enabled = true;
            }
        }

        private async void buttonFetchAllFingers_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى اختيار عنصر أولاً من القائمة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            labelStatus.Text = "جاري سحب وفحص كافة البصمات (0-9) من جهاز البصمة...";
            labelStatus.ForeColor = Color.FromArgb(8, 145, 178);
            buttonFetchAllFingers.Enabled = false;

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم العثور على جهاز بصمة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                int enrollId = await EnsureElementEnrollIdAsync(zkService);
                int localId = selectedElement.Id;
                string natId = selectedElement.NationalId?.Trim();
                var templates = await zkService.ReadAllUserTemplatesAsync(enrollId, natId);

                // فحص رقم القيد المحلي إذا لم توجد بصمات مسجلة بكود enrollId
                if (templates.Count == 0 && localId > 0 && localId != enrollId)
                {
                    templates = await zkService.ReadAllUserTemplatesAsync(localId);
                    if (templates.Count > 0)
                    {
                        selectedElement.DeviceEnrollId = localId;
                        await dataHelperElement.EditAsync(selectedElement);
                        var cached = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
                        if (cached != null) cached.DeviceEnrollId = localId;
                        UpdateElementDetailsLabel();
                        enrollId = localId;
                    }
                }

                if (templates.Count == 0)
                {
                    // Check if other users on the device have fingerprints (e.g. if registered with a different user ID on the machine)
                    var deviceUsers = await zkService.GetAllDeviceUsersWithFingerprintsAsync();
                    var usersWithFp = deviceUsers?.Where(u => u.FingerCount > 0).ToList() ?? new List<(string, string, int)>();

                    if (usersWithFp.Count > 0)
                    {
                        string listStr = string.Join("\n", usersWithFp.Select(u => $"• كود بالماكينة [ {u.EnrollNum} ] : {(string.IsNullOrEmpty(u.Name) ? "بدون اسم" : u.Name)} - لديه ({u.FingerCount}) بصمة مسجلة"));

                        var candidate = usersWithFp.FirstOrDefault(u => !string.IsNullOrEmpty(u.Name) && selectedElement.ElementName.Contains(u.Name));
                        if (string.IsNullOrEmpty(candidate.EnrollNum))
                        {
                            candidate = usersWithFp.FirstOrDefault(u => u.EnrollNum == enrollId.ToString());
                            if (string.IsNullOrEmpty(candidate.EnrollNum))
                            {
                                candidate = usersWithFp.FirstOrDefault(u => u.EnrollNum == selectedElement.Id.ToString());
                                if (string.IsNullOrEmpty(candidate.EnrollNum))
                                {
                                    candidate = usersWithFp[0];
                                }
                            }
                        }

                        var ask = MessageBox.Show(
                            $"لم يتم العثور على بصمات مسجلة بكود الماكينة الحالي (#{enrollId}) على الماكينة.\n\n" +
                            $"ولكن تم اكتشاف بصمات للمستخدمين التاليين بالماكينة:\n" +
                            $"{listStr}\n\n" +
                            $"هل تم تسجيل بصمة هذا الشخص على الماكينة تحت كود [ {candidate.EnrollNum} ] وتريد ربطه وتحديث كوده وسحب بصماته الآن؟",
                            "اكتشاف بصمات على الماكينة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (ask == DialogResult.Yes)
                        {
                            int cId = int.TryParse(candidate.EnrollNum, out int parsed) ? parsed : 0;
                            if (cId > 0 && selectedElement.DeviceEnrollId != cId)
                            {
                                selectedElement.DeviceEnrollId = cId;
                                await dataHelperElement.EditAsync(selectedElement);
                                var cached = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
                                if (cached != null) cached.DeviceEnrollId = cId;
                                UpdateElementDetailsLabel();
                            }
                            templates = await zkService.ReadAllUserTemplatesAsync(cId, candidate.EnrollNum);
                        }
                    }
                }

                zkService.Disconnect();

                if (templates.Count == 0)
                {
                    labelStatus.Text = $"لا توجد أي بصمات مسجلة للشخص #{enrollId} على الماكينة.";
                    labelStatus.ForeColor = Color.OrangeRed;
                    MessageBox.Show(
                        $"لم يتم العثور على أي بصمات مسجلة للشخص [{selectedElement.ElementName}] بكود الماكينة (#{enrollId}) في الماكينة.\n\n" +
                        "💡 تسلسل العمل المتناسق:\n" +
                        $"1. اضغط '👤 1. مزامنة وكود العنصر بالجهاز' لإنشاء المستخدم بالماكينة.\n" +
                        "2. أو اضغط '👆 2. بدء تسجيل البصمة بالجهاز' ليقوم البرنامج بتوجيه الماكينة للتسجيل فوراً.\n" +
                        "3. بعد وضع الإصبع 3 مرات، اضغط '📥 3. سحب بصمة الإصبع المحدد'.",
                        "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var allFps = await dataHelperFingerprint.GetAllDataAsync() ?? new List<ElementFingerprint>();
                string[] fingerNames = {
                    "0 - إبهام يمين", "1 - سبابة يمنى", "2 - وسطى يمنى", "3 - بنصر أيمن", "4 - خنصر أيمن",
                    "5 - إبهام يسار", "6 - سبابة يسرى", "7 - وسطى يسرى", "8 - بنصر أيسر", "9 - خنصر أيسر"
                };

                int savedCount = 0;
                foreach (var kvp in templates)
                {
                    int fingerIdx = kvp.Key;
                    string templateData = kvp.Value;
                    string fName = fingerIdx >= 0 && fingerIdx < fingerNames.Length ? fingerNames[fingerIdx] : $"إصبع {fingerIdx}";

                    var existing = allFps.FirstOrDefault(x => x.ElementId == selectedElement.Id && x.FingerIndex == fingerIdx);
                    if (existing != null)
                    {
                        existing.TemplateData = templateData;
                        existing.CreatedDate = DateTime.Now;
                        await dataHelperFingerprint.EditAsync(existing);
                    }
                    else
                    {
                        var newFp = new ElementFingerprint
                        {
                            ElementId = selectedElement.Id,
                            FingerIndex = fingerIdx,
                            FingerName = fName,
                            TemplateData = templateData,
                            TemplateVersion = 10,
                            CreatedDate = DateTime.Now
                        };
                        await dataHelperFingerprint.AddAsync(newFp);
                    }
                    savedCount++;
                }

                labelStatus.Text = $"تم سحب وحفظ ({savedCount}) بصمات للعنصر بنجاح!";
                labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                await RefreshFingerprintsGrid();
                MessageBox.Show($"تم سحب وتحديث ({savedCount}) بصمة للعنصر [{selectedElement.ElementName}] في قاعدة البيانات المركزية بنجاح.", "تم السحب بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonFetchAllFingers.Enabled = true;
            }
        }

        private async void buttonFetchFromDevice_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى اختيار عنصر أولاً من القائمة المنسدلة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int fingerIdx = comboBoxFinger.SelectedIndex;
            string fingerName = comboBoxFinger.SelectedItem?.ToString() ?? "سبابة يمنى";

            labelStatus.Text = "جاري الاتصال بالماكينة وسحب القالب...";
            labelStatus.ForeColor = Color.FromArgb(37, 99, 235);
            buttonFetchFromDevice.Enabled = false;

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم العثور على جهاز بصمة مهيأ. يرجى ضبط إعدادات الجهاز أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                int enrollId = await EnsureElementEnrollIdAsync(zkService);
                int localId = selectedElement.Id;
                string natId = selectedElement.NationalId?.Trim();

                // 1. فحص كود الماكينة الموحد (enrollId) للإصبع المحدد
                string templateData = await zkService.ReadFingerprintTemplateAsync(enrollId, fingerIdx, natId);

                // 2. إذا لم يتم العثور، فحص رقم القيد المحلي (localId) إذا كان مختلفاً عن كود الماكينة
                if (string.IsNullOrEmpty(templateData) && localId > 0 && localId != enrollId)
                {
                    templateData = await zkService.ReadFingerprintTemplateAsync(localId, fingerIdx);
                    if (!string.IsNullOrEmpty(templateData))
                    {
                        selectedElement.DeviceEnrollId = localId;
                        await dataHelperElement.EditAsync(selectedElement);
                        var cached = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
                        if (cached != null) cached.DeviceEnrollId = localId;
                        UpdateElementDetailsLabel();
                        enrollId = localId;
                    }
                }

                // 3. إذا لم يتم العثور، فحص ما إذا كان للشخص أي بصمة مسجلة على أي إصبع آخر (0 إلى 9)
                if (string.IsNullOrEmpty(templateData))
                {
                    var userTemplates = await zkService.ReadAllUserTemplatesAsync(enrollId);
                    if (userTemplates.Count == 0 && localId > 0 && localId != enrollId)
                    {
                        userTemplates = await zkService.ReadAllUserTemplatesAsync(localId);
                        if (userTemplates.Count > 0)
                        {
                            selectedElement.DeviceEnrollId = localId;
                            await dataHelperElement.EditAsync(selectedElement);
                            var cached = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
                            if (cached != null) cached.DeviceEnrollId = localId;
                            UpdateElementDetailsLabel();
                            enrollId = localId;
                        }
                    }

                    if (userTemplates.Count > 0)
                    {
                        int foundFinger = userTemplates.Keys.First();
                        templateData = userTemplates[foundFinger];
                        fingerIdx = foundFinger;
                        if (comboBoxFinger.Items.Count > fingerIdx)
                        {
                            comboBoxFinger.SelectedIndex = fingerIdx;
                            fingerName = comboBoxFinger.SelectedItem?.ToString() ?? $"إصبع #{fingerIdx}";
                        }
                    }
                }

                if (string.IsNullOrEmpty(templateData))
                {
                    // Check if other users on the device have fingerprints
                    var deviceUsers = await zkService.GetAllDeviceUsersWithFingerprintsAsync();
                    var usersWithFp = deviceUsers?.Where(u => u.FingerCount > 0).ToList() ?? new List<(string, string, int)>();

                    if (usersWithFp.Count > 0)
                    {
                        string listStr = string.Join("\n", usersWithFp.Select(u => $"• كود بالماكينة [ {u.EnrollNum} ] : {(string.IsNullOrEmpty(u.Name) ? "بدون اسم" : u.Name)} - لديه ({u.FingerCount}) بصمة مسجلة"));

                        var candidate = usersWithFp.FirstOrDefault(u => !string.IsNullOrEmpty(u.Name) && selectedElement.ElementName.Contains(u.Name));
                        if (string.IsNullOrEmpty(candidate.EnrollNum))
                        {
                            candidate = usersWithFp.FirstOrDefault(u => u.EnrollNum == enrollId.ToString());
                            if (string.IsNullOrEmpty(candidate.EnrollNum))
                            {
                                candidate = usersWithFp[0];
                            }
                        }

                        var ask = MessageBox.Show(
                            $"لم يتم العثور على بصمة للإصبع #{fingerIdx} بكود الماكينة الحالي (#{enrollId}) على الماكينة.\n\n" +
                            $"ولكن تم اكتشاف بصمات للمستخدمين التاليين بالماكينة:\n" +
                            $"{listStr}\n\n" +
                            $"هل تم تسجيل بصمة هذا الشخص على الماكينة تحت كود [ {candidate.EnrollNum} ] وتريد ربطه وتحديث كوده وسحب بصمته الآن؟",
                            "اكتشاف بصمات على الماكينة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (ask == DialogResult.Yes)
                        {
                            int cId = int.TryParse(candidate.EnrollNum, out int parsed) ? parsed : 0;
                            if (cId > 0 && selectedElement.DeviceEnrollId != cId)
                            {
                                selectedElement.DeviceEnrollId = cId;
                                await dataHelperElement.EditAsync(selectedElement);
                                var cached = allElements.FirstOrDefault(x => x.Id == selectedElement.Id);
                                if (cached != null) cached.DeviceEnrollId = cId;
                                UpdateElementDetailsLabel();
                            }
                            templateData = await zkService.ReadFingerprintTemplateAsync(cId, fingerIdx, candidate.EnrollNum);
                            if (string.IsNullOrEmpty(templateData))
                            {
                                var candTemplates = await zkService.ReadAllUserTemplatesAsync(cId, candidate.EnrollNum);
                                if (candTemplates.Count > 0)
                                {
                                    fingerIdx = candTemplates.Keys.First();
                                    templateData = candTemplates[fingerIdx];
                                    fingerName = comboBoxFinger.Items.Count > fingerIdx ? comboBoxFinger.Items[fingerIdx].ToString() : $"إصبع {fingerIdx}";
                                }
                            }
                        }
                    }
                }

                zkService.Disconnect();

                if (!string.IsNullOrEmpty(templateData))
                {
                    var allFps = await dataHelperFingerprint.GetAllDataAsync();
                    var existing = allFps?.FirstOrDefault(x => x.ElementId == selectedElement.Id && x.FingerIndex == fingerIdx);

                    if (existing != null)
                    {
                        existing.TemplateData = templateData;
                        existing.CreatedDate = DateTime.Now;
                        await dataHelperFingerprint.EditAsync(existing);
                    }
                    else
                    {
                        var newFp = new ElementFingerprint
                        {
                            ElementId = selectedElement.Id,
                            FingerIndex = fingerIdx,
                            FingerName = fingerName,
                            TemplateData = templateData,
                            TemplateVersion = 10,
                            CreatedDate = DateTime.Now
                        };
                        await dataHelperFingerprint.AddAsync(newFp);
                    }

                    labelStatus.Text = $"تم سحب وحفظ بصمة [{fingerName}] بنجاح في قاعدة البيانات!";
                    labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                    await RefreshFingerprintsGrid();
                }
                else
                {
                    labelStatus.Text = $"لم يتم العثور على بصمة مسجلة للإصبع #{fingerIdx} في الماكينة.";
                    labelStatus.ForeColor = Color.OrangeRed;
                    MessageBox.Show(
                        $"لم يتم العثور على قالب بصمة مسجل في الماكينة للعنصر (#{enrollId}) للإصبع المحدد ({fingerName}).\n\n" +
                        "يرجى الضغط على زر '👆 بدء تسجيل البصمة بالجهاز' لتسجيل البصمة أولاً على حساس الجهاز ثم الضغط على سحب البصمة.",
                        "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"استثناء: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonFetchFromDevice.Enabled = true;
            }
        }

        private async void buttonUploadToDevice_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى اختيار اسم أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            labelStatus.Text = "جاري رفع بيانات الاسم وبصماته للماكينة...";
            labelStatus.ForeColor = Color.FromArgb(37, 99, 235);
            buttonUploadToDevice.Enabled = false;

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم تهيئة جهاز بصمة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                int enrollId = await EnsureElementEnrollIdAsync(zkService);
                bool userOk = await zkService.UploadUserAsync(enrollId, selectedElement.ElementName);

                var allFps = await dataHelperFingerprint.GetAllDataAsync();
                var elementFps = allFps?.Where(x => x.ElementId == selectedElement.Id).ToList() ?? new List<ElementFingerprint>();

                int uploadCount = 0;
                foreach (var fp in elementFps)
                {
                    if (!string.IsNullOrEmpty(fp.TemplateData))
                    {
                        bool fpOk = await zkService.UploadFingerprintTemplateAsync(enrollId, fp.FingerIndex, fp.TemplateData);
                        if (fpOk) uploadCount++;
                    }
                }

                zkService.Disconnect();

                labelStatus.Text = $"تم رفع الاسم [{selectedElement.ElementName}] بكود #{enrollId} و ({uploadCount}) بصمات إلى الجهاز بنجاح!";
                labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                MessageBox.Show($"تم رفع الاسم إلى جهاز البصمة بنجاح.\nاسم الشخص: {selectedElement.ElementName}\nكود الماكينة الموحد: #{enrollId}\nعدد قوالب البصمات المرفوعة: {uploadCount}", "تم الرفع بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonUploadToDevice.Enabled = true;
            }
        }

        private async void buttonDeleteFingerprint_Click(object sender, EventArgs e)
        {
            if (dataGridViewFingerprints.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد بصمة من الجدول لحذفها", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int fpId = Convert.ToInt32(dataGridViewFingerprints.SelectedRows[0].Cells[0].Value);
            var confirm = MessageBox.Show(
                "هل أنت متأكد من حذف هذه البصمة؟\nسيتم حذفها من قاعدة البيانات ومن جهاز البصمة أيضاً إذا كان متصلاً.",
                "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var allFps = await dataHelperFingerprint.GetAllDataAsync();
                var targetFp = allFps?.FirstOrDefault(x => x.Id == fpId);

                // Delete from device if connected
                if (targetFp != null && selectedElement != null)
                {
                    try
                    {
                        var devices = await dataHelperDevice.GetAllDataAsync();
                        var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                        if (device != null)
                        {
                            using var zkService = new ZKDeviceService();
                            if (await zkService.ConnectAsync(device))
                            {
                                int enrollId = selectedElement.DeviceEnrollId ?? selectedElement.Id;
                                await zkService.DeleteFingerprintAsync(enrollId, targetFp.FingerIndex);
                                zkService.Disconnect();
                            }
                        }
                    }
                    catch { }
                }

                int res = await dataHelperFingerprint.DeleteAsync(fpId);
                if (res == 1)
                {
                    labelStatus.Text = "تم حذف البصمة بنجاح من قاعدة البيانات والجهاز.";
                    labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                    await RefreshFingerprintsGrid();
                }
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ في الحذف: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private async void buttonDeleteUserFromDevice_Click(object sender, EventArgs e)
        {
            if (selectedElement == null)
            {
                MessageBox.Show("يرجى تحديد عنصر أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل أنت متأكد تماماً من مسح الشخص [{selectedElement.ElementName}] وكافة بصماته من جهاز البصمة؟\n(ملاحظة: لن يتم حذف بيانات الشخص من البرنامج الرئيسي)",
                "تحذير مسح من الجهاز", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            buttonDeleteUserFromDevice.Enabled = false;
            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم تهيئة جهاز بصمة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بالجهاز.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                int enrollId = selectedElement.DeviceEnrollId ?? selectedElement.Id;
                bool ok = await zkService.DeleteUserAsync(enrollId);
                zkService.Disconnect();

                if (ok)
                {
                    labelStatus.Text = $"تم مسح الشخص #{enrollId} وبصماته بالكامل من جهاز البصمة بنجاح.";
                    labelStatus.ForeColor = Color.FromArgb(5, 150, 105);
                    MessageBox.Show($"تم مسح الشخص [{selectedElement.ElementName}] من جهاز البصمة بنجاح.", "تم المسح من الجهاز", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    labelStatus.Text = "فشل مسح الشخص من جهاز البصمة.";
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
                buttonDeleteUserFromDevice.Enabled = true;
            }
        }

        private async void buttonBulkSyncAll_Click(object sender, EventArgs e)
        {
            if (allElements == null || allElements.Count == 0)
            {
                MessageBox.Show("لا توجد عناصر مسجلة للمزامنة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل تريد رفع ومزامنة جميع الأشخاص ({allElements.Count} شخص) وبصماتهم المسجلة إلى جهاز البصمة؟\nسيتم تحديث أسماء الأشخاص وقوالب البصمات في ذاكرة الجهاز.",
                "تأكيد المزامنة الشاملة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            buttonBulkSyncAll.Enabled = false;
            labelStatus.Text = "جاري الاتصال بالجهاز للمزامنة الشاملة...";
            labelStatus.ForeColor = Color.FromArgb(79, 70, 229);

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم تهيئة جهاز بصمة نشط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                var allFps = await dataHelperFingerprint.GetAllDataAsync() ?? new List<ElementFingerprint>();
                var fpsByElement = allFps.GroupBy(x => x.ElementId).ToDictionary(g => g.Key, g => g.ToList());

                int syncedUsers = 0;
                int syncedTemplates = 0;

                for (int i = 0; i < allElements.Count; i++)
                {
                    var elem = allElements[i];
                    int enrollId = elem.DeviceEnrollId ?? elem.Id;
                    labelStatus.Text = $"جاري المزامنة ({i + 1}/{allElements.Count}): {elem.ElementName} [#{enrollId}]...";

                    bool uOk = await zkService.UploadUserAsync(enrollId, elem.ElementName);
                    if (uOk) syncedUsers++;

                    if (fpsByElement.TryGetValue(elem.Id, out var elemFps))
                    {
                        foreach (var fp in elemFps)
                        {
                            if (!string.IsNullOrEmpty(fp.TemplateData))
                            {
                                bool fOk = await zkService.UploadFingerprintTemplateAsync(enrollId, fp.FingerIndex, fp.TemplateData);
                                if (fOk) syncedTemplates++;
                            }
                        }
                    }
                }

                zkService.Disconnect();

                labelStatus.Text = $"اكتملت المزامنة الشاملة! تم رفع {syncedUsers} عنصر و {syncedTemplates} قالب بصمة.";
                labelStatus.ForeColor = Color.FromArgb(5, 150, 105);

                MessageBox.Show(
                    $"اكتملت المزامنة الجماعية مع جهاز البصمة بنجاح!\n\n" +
                    $"إجمالي العناصر المرفوعة: {syncedUsers}\n" +
                    $"إجمالي قوالب البصمات المرفوعة: {syncedTemplates}",
                    "نجاح المزامنة الجماعية", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ في المزامنة: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonBulkSyncAll.Enabled = true;
            }
        }

        private async void buttonAutoMatchDeviceUsers_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "تقوم هذه العملية بفحص جميع المستخدمين والبصمات المسجلة على ماكينة البصمة، " +
                "ومطابقتهم تلقائياً مع أسماء وأرقام الأشخاص في قاعدة البيانات الحالية لتوحيد أكواد الماكينة (DeviceEnrollId).\n\n" +
                "مفيد جداً عند تشغيل قاعدتي بيانات منفصلتين على نفس الماكينة لضمان ربط كل شخص بكوده الموحد على الماكينة.\n\n" +
                "هل ترغب في بدء المطابقة والربط الآن؟",
                "مطابقة وربط أكواد الماكينة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            buttonAutoMatchDeviceUsers.Enabled = false;
            labelStatus.Text = "جاري الاتصال بجهاز البصمة وقراءة المستخدمين المسجلين...";
            labelStatus.ForeColor = Color.FromArgb(13, 148, 136);

            try
            {
                var devices = await dataHelperDevice.GetAllDataAsync();
                var device = devices?.FirstOrDefault(x => x.IsEnabled) ?? devices?.FirstOrDefault();
                if (device == null)
                {
                    MessageBox.Show("لم يتم تهيئة جهاز بصمة نشط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var zkService = new ZKDeviceService();
                if (!await zkService.ConnectAsync(device))
                {
                    labelStatus.Text = "فشل الاتصال بجهاز البصمة.";
                    labelStatus.ForeColor = Color.Red;
                    return;
                }

                var deviceUsers = await zkService.GetAllDeviceUsersWithFingerprintsAsync();
                zkService.Disconnect();

                if (deviceUsers == null || deviceUsers.Count == 0)
                {
                    labelStatus.Text = "لم يتم العثور على أي مستخدمين مسجلين في الماكينة.";
                    labelStatus.ForeColor = Color.OrangeRed;
                    MessageBox.Show("لم يتم العثور على مستخدمين مسجلين في الماكينة حالياً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int matchedCount = 0;
                int updatedCount = 0;
                var updatedElements = new List<string>();

                allElements = await dataHelperElement.GetAllDataAsync() ?? new List<ElementInfo>();

                foreach (var du in deviceUsers)
                {
                    if (!int.TryParse(du.EnrollNum, out int enrollId) || enrollId <= 0)
                        continue;

                    string dName = du.Name?.Trim();

                    ElementInfo match = null;

                    // 1. المطابقة بالاسم
                    if (!string.IsNullOrEmpty(dName))
                    {
                        match = allElements.FirstOrDefault(x =>
                            string.Equals(x.ElementName?.Trim(), dName, StringComparison.OrdinalIgnoreCase) ||
                            (x.ElementName != null && (x.ElementName.Contains(dName) || dName.Contains(x.ElementName))));
                    }

                    // 2. المطابقة بالرقم القومي
                    if (match == null)
                    {
                        match = allElements.FirstOrDefault(x => x.NationalId?.Trim() == du.EnrollNum);
                    }

                    // 3. المطابقة برقم المعرف المحلي إذا لم يكن مربوطاً بكود ماكينة آخر
                    if (match == null)
                    {
                        match = allElements.FirstOrDefault(x => x.Id == enrollId && !x.DeviceEnrollId.HasValue);
                    }

                    if (match != null)
                    {
                        matchedCount++;
                        if (match.DeviceEnrollId != enrollId)
                        {
                            match.DeviceEnrollId = enrollId;
                            await dataHelperElement.EditAsync(match);
                            updatedCount++;
                            updatedElements.Add($"• {match.ElementName} ➔ كود الماكينة: #{enrollId}");
                        }
                    }
                }

                // تحديث القائمة المحلية والشاشة
                allElements = await dataHelperElement.GetAllDataAsync() ?? new List<ElementInfo>();
                searchableElementDropDown.SetElements(allElements);

                if (selectedElement != null)
                {
                    selectedElement = allElements.FirstOrDefault(x => x.Id == selectedElement.Id) ?? selectedElement;
                    UpdateElementDetailsLabel();
                }

                labelStatus.Text = $"اكتملت المطابقة! تم مطابقة ({matchedCount}) وربط ({updatedCount}) كود بنجاح.";
                labelStatus.ForeColor = Color.FromArgb(5, 150, 105);

                string details = updatedElements.Count > 0
                    ? string.Join("\n", updatedElements.Take(15)) + (updatedElements.Count > 15 ? $"\n... و ({updatedElements.Count - 15}) آخرين" : "")
                    : "جميع العناصر المطابقة كانت مربوطة ومحدثة مسبقاً.";

                MessageBox.Show(
                    $"اكتملت عملية المطابقة والربط مع جهاز البصمة بنجاح!\n\n" +
                    $"عدد المستخدمين على الماكينة: {deviceUsers.Count}\n" +
                    $"إجمالي العناصر المطابقة: {matchedCount}\n" +
                    $"الأكواد الجديدة التي تم ربطها وتحديثها: {updatedCount}\n\n" +
                    $"{details}",
                    "نجاح مطابقة أكواد الماكينة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                labelStatus.Text = $"خطأ: {ex.Message}";
                labelStatus.ForeColor = Color.Red;
            }
            finally
            {
                buttonAutoMatchDeviceUsers.Enabled = true;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
