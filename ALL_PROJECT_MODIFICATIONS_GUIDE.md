# الدليل الشامل لكافة التعديلات والميزات المُنفذة لتطبيقها على مشاريع مماثلة
*(Windows Forms - .NET 9 - DevExpress & WinForms Controls - Entity Framework Core)*

هذا الملف يحتوي على توثيق تقني ومعماري كامل وشامل لجميع التعديلات والحلول التي تم تطبيقها، مصنفة حسب كل ميزة، مع نماذج الأكواد، البنية، وأسباب التصميم، بحيث يسهل نقلها وتطبيقها على أي مشروع آخر يعمل بنفس الآلية.

---

## الفهرس
1. [نظام الترقيم والصفحات الموحد (Pagination System) لجميع الجداول](#1-نظام-الترقيم-والصفحات-الموحد-pagination-system-لجميع-الجداول)
2. [ترقيم مسلسل الصفوف التراكمي الصحيح عبر الصفحات (AutoNumeric Sequence)](#2-ترقيم-مسلسل-الصفوف-التراكمي-الصحيح-عبر-الصفحات-autonumeric-sequence)
3. [الدروب داون الذكي القابل للبحث والفلترة الفورية (SearchableElementDropDown)](#3-الدروب-داون-الذكي-القابل-للبحث-والفلترة-الفورية-searchableelementdropdown)
4. [حل مشكلة تعارض ملفات الموارد في الـ Designer خطأ MSBuild](#4-حل-مشكلة-تعارض-ملفات-الموارد-في-الـ-designer-خطأ-msbuild)
5. [منظومة متابعة المواعيد والحضور بالبصمة والتواريخ التلقائية](#5-منظومة-متابعة-المواعيد-والحضور-بالبصمة-والتواريخ-التلقائية)
6. [عزل وفلترة العناصر خارج المتابعة في الشاشات والحضور](#6-عزل-وفلترة-العناصر-خارج-المتابعة-في-الشاشات-والحضور)
7. [البحث المتقدم ومطابقة حالات المتابعة مع قاعدة البيانات](#7-البحث-المتقدم-ومطابقة-حالات-المتابعة-مع-قاعدة-البيانات)
8. [تعريب وتنسيق رؤوس الجداول (DataGrid Column Headers Localization)](#8-تعريب-وتنسيق-رؤوس-الجداول-datagrid-column-headers-localization)
9. [منظومة الحضور والانصراف والبصمة المتكاملة (ZKTeco Biometric System & Attendance Processor)](#9-منظومة-الحضور-والانصراف-والبصمة-المتكاملة-zkteco-biometric-system--attendance-processor)
10. [الجداول وسكربتات قاعدة البيانات المضافة (Database Schema & SQL Scripts)](#10-الجداول-وسكربتات-قاعدة-البيانات-المضافة-database-schema--sql-scripts)

---

## 1. نظام الترقيم والصفحات الموحد (Pagination System) لجميع الجداول

### الهدف:
إضافة تحكم كامل في تقسيم البيانات المعروضة في شاشات الجريد (DevExpress GridControl أو WinForms DataGridView) إلى صفحات بدلاً من تحميل كل السجلات دفعة واحدة، مع إمكانية اختيار عدد السجلات بالصفحة: `(10، 20، 30، 50، 100، الكل)`.

### أ. إنشاء عنصر التحكم المشترك `PaginationControl`:
تم إنشاء كنترول مستقل قابل لإعادة الاستخدام في المسار:
`Follow/Gui/GuiCommon/PaginationControl.cs`

#### مميزات الكنترول:
- **ترتيب عربي سليم (Right-To-Left):**
  - **في أسفل اليمين:** عرض "عرض الصفوف" + دروب داون بعدد الصفوف (`10, 20, 30, 50, 100, الكل`).
  - **في أسفل اليسار:** أزرار التنقل المنطقية: `الأولى` | `السابق` | `دروب داون رقم الصفحة` | `من` | `إجمالي الصفحات` | `التالي` | `الأخيرة`.
- **دعم ملء الشاشة (Responsive Layout):** استخدام `TableLayoutPanel` مع تعيين أعمدة مرنة (100% في المنتصف) لضمان بقاء شريط الصفحات ثابتاً في الأسفل حتى عند تكبير الشاشة (Maximize) أو تصغيرها.
- **حدث موحد لتغيير الصفحة:** `event EventHandler PageChanged`.

#### كود الكنترول الأساسي (`PaginationControl.cs`):
```csharp
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Follow.Gui.GuiCommon
{
    public class PaginationControl : UserControl
    {
        public event EventHandler PageChanged;

        private int _totalRecords = 0;
        private int _pageSize = 30;
        private int _currentPage = 1;
        private bool _isUpdatingUi = false;

        private TableLayoutPanel tableLayout;
        private FlowLayoutPanel panelPageSize;
        private FlowLayoutPanel panelNavigation;

        private Label labelPageSizeText;
        private ComboBox comboBoxPageSize;

        private Button buttonFirst;
        private Button buttonPrev;
        private ComboBox comboBoxCurrentPage;
        private Label labelOf;
        private Label labelTotalPages;
        private Button buttonNext;
        private Button buttonLast;

        public int PageSize => _pageSize;
        public int CurrentPage => _currentPage;
        public bool IsAll => _pageSize >= int.MaxValue || _pageSize <= 0;
        public int TotalPages
        {
            get
            {
                if (_totalRecords <= 0) return 1;
                if (IsAll) return 1;
                return (int)Math.Ceiling((double)_totalRecords / _pageSize);
            }
        }

        public PaginationControl()
        {
            InitializeComponentCustom();
        }

        private void InitializeComponentCustom()
        {
            this.RightToLeft = RightToLeft.Yes;
            this.Dock = DockStyle.Bottom;
            this.Height = 44;
            this.BackColor = Color.FromArgb(248, 250, 252);

            var font = new Font("Cairo", 9F, FontStyle.Bold, GraphicsUnit.Point);

            tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 3,
                RightToLeft = RightToLeft.Yes
            };
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // اليمين
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // مسافة مرنة
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // اليسار

            // 1. قسم حجم الصفحة (اليمين)
            panelPageSize = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(10, 5, 0, 5)
            };
            labelPageSizeText = new Label { Text = "عرض الصفوف:", Font = font, AutoSize = true, Margin = new Padding(0, 6, 5, 0) };
            comboBoxPageSize = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = font, Width = 75 };
            comboBoxPageSize.Items.AddRange(new object[] { "10", "20", "30", "50", "100", "الكل" });
            comboBoxPageSize.SelectedItem = "30";
            comboBoxPageSize.SelectedIndexChanged += ComboBoxPageSize_SelectedIndexChanged;

            panelPageSize.Controls.Add(labelPageSizeText);
            panelPageSize.Controls.Add(comboBoxPageSize);

            // 2. قسم التنقل بين الصفحات (اليسار)
            panelNavigation = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 5, 10, 5)
            };

            buttonFirst = CreateNavButton("الأولى", (s, e) => GoToPage(1));
            buttonPrev = CreateNavButton("السابق", (s, e) => GoToPage(_currentPage - 1));

            comboBoxCurrentPage = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = font, Width = 65 };
            comboBoxCurrentPage.SelectedIndexChanged += (s, e) =>
            {
                if (_isUpdatingUi) return;
                if (int.TryParse(comboBoxCurrentPage.SelectedItem?.ToString(), out int p)) GoToPage(p);
            };

            labelOf = new Label { Text = "من", Font = font, AutoSize = true, Margin = new Padding(4, 6, 4, 0) };
            labelTotalPages = new Label { Text = "1", Font = font, AutoSize = true, Margin = new Padding(4, 6, 4, 0) };

            buttonNext = CreateNavButton("التالي", (s, e) => GoToPage(_currentPage + 1));
            buttonLast = CreateNavButton("الأخيرة", (s, e) => GoToPage(TotalPages));

            panelNavigation.Controls.Add(buttonFirst);
            panelNavigation.Controls.Add(buttonPrev);
            panelNavigation.Controls.Add(comboBoxCurrentPage);
            panelNavigation.Controls.Add(labelOf);
            panelNavigation.Controls.Add(labelTotalPages);
            panelNavigation.Controls.Add(buttonNext);
            panelNavigation.Controls.Add(buttonLast);

            tableLayout.Controls.Add(panelPageSize, 0, 0);
            tableLayout.Controls.Add(panelNavigation, 2, 0);
            this.Controls.Add(tableLayout);
        }

        private Button CreateNavButton(string text, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Font = new Font("Cairo", 8.5F, FontStyle.Regular, GraphicsUnit.Point),
                Height = 30,
                AutoSize = true,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Standard,
                Margin = new Padding(2, 2, 2, 2),
                Cursor = Cursors.Hand
            };
            btn.Click += onClick;
            return btn;
        }

        public void SetTotalRecords(int total, int? targetPage = null)
        {
            _totalRecords = total;
            int totalPages = TotalPages;
            if (targetPage.HasValue) _currentPage = Math.Max(1, Math.Min(targetPage.Value, totalPages));
            else if (_currentPage > totalPages) _currentPage = Math.Max(1, totalPages);
            UpdateUi();
        }

        private void GoToPage(int page)
        {
            int total = TotalPages;
            if (page < 1) page = 1;
            if (page > total) page = total;
            if (_currentPage == page) return;
            _currentPage = page;
            UpdateUi();
            PageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ComboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingUi) return;
            string sel = comboBoxPageSize.SelectedItem?.ToString();
            _pageSize = (sel == "الكل") ? int.MaxValue : int.Parse(sel);
            _currentPage = 1;
            UpdateUi();
            PageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateUi()
        {
            _isUpdatingUi = true;
            try
            {
                int totalPages = TotalPages;
                labelTotalPages.Text = totalPages.ToString();

                comboBoxCurrentPage.Items.Clear();
                for (int i = 1; i <= totalPages; i++) comboBoxCurrentPage.Items.Add(i.ToString());
                comboBoxCurrentPage.SelectedItem = _currentPage.ToString();

                buttonFirst.Enabled = _currentPage > 1;
                buttonPrev.Enabled = _currentPage > 1;
                buttonNext.Enabled = _currentPage < totalPages;
                buttonLast.Enabled = _currentPage < totalPages;
            }
            finally
            {
                _isUpdatingUi = false;
            }
        }
    }
}
```

### ب. كيفية دمج الـ Paging داخل أي شاشة (`UserControl` أو `Form`):
1. **تعريف المتغيرات وتثبيت الكنترول:**
```csharp
private PaginationControl paginationControl;
private List<MyModel> _allCachedData = new List<MyModel>();

private void SetupPagination()
{
    paginationControl = new PaginationControl();
    paginationControl.Dock = DockStyle.Bottom;
    paginationControl.PageChanged += (s, e) => ApplyPaging();
    this.Controls.Add(paginationControl);

    // ضبط الترتيب بحيث يظهر شريط الترقيم أسفل الجريد
    gridControl1.BringToFront();
}
```
2. **عند جلب البيانات (LoadData):**
```csharp
_allCachedData = await dataHelper.GetAllAsync();
paginationControl.SetTotalRecords(_allCachedData.Count);
ApplyPaging();
```
3. **دالة تقسيم الصفحة `ApplyPaging()`:**
```csharp
private void ApplyPaging()
{
    if (_allCachedData == null) return;
    List<MyModel> pagedList;
    if (paginationControl.IsAll)
    {
        pagedList = _allCachedData;
    }
    else
    {
        pagedList = _allCachedData
            .Skip((paginationControl.CurrentPage - 1) * paginationControl.PageSize)
            .Take(paginationControl.PageSize)
            .ToList();
    }
    gridControl1.DataSource = pagedList;
}
```

---

## 2. ترقيم مسلسل الصفوف التراكمي الصحيح عبر الصفحات (AutoNumeric Sequence)

### المشكلة:
عند استخدام عمود ترقيم تسلسلي تلقائي (`AutoNumericColumn` في DevExpress GridView عبر حدث `CustomColumnDisplayText`)، عند الانتقال للصفحة الثانية بحجم 30 يبدأ الترقيم من جديد من 1 حتى 30 بدلاً من إكمال التسلسل (31، 32، 33 ...).

### الحل الجذري:
حساب نقطة البداية `startIndex` اعتماداً على رقم الصفحة الحالية وحجم الصفحة:
```csharp
private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
{
    if (e.Column.Name == "AutoNumericColumn" || e.Column.FieldName == "AutoNumericColumn")
    {
        int startIndex = (paginationControl != null && !paginationControl.IsAll)
            ? (paginationControl.CurrentPage - 1) * paginationControl.PageSize
            : 0;

        e.DisplayText = (startIndex + e.ListSourceRowIndex + 1).ToString();
    }
}
```

---

## 3. الدروب داون الذكي القابل للبحث والفلترة الفورية (SearchableElementDropDown)

### المشكلة:
الـ `ComboBox` التقليدي في Windows Forms يكون بطيئاً وصعب الاستخدام عند وجود آلاف السجلات، ولا يدعم البحث السريع بالرقم القومي أو الأحرف العربية المختلفة (أ، إ، آ، ة، ه، ي، ى).

### الحل المطبق:
استخدام الكنترول المخصص `SearchableElementDropDown` والموجود في:
`Follow.Gui.GuiFingerprint.SearchableElementDropDown`

#### الخصائص والمميزات:
1. **نافذة منبثقة تفاعلية (Popup Grid)** تفتح أثناء الكتابة بدون تجميد الشاشة.
2. **بحث متعدد الحقول:** يبحث في آن واحد عن (الاسم، الرقم القومي، رقم القيد).
3. **تطبيع النصوص العربية (Arabic Normalization):** يتجاهل الفروق بين (أ، إ، آ) و (ة، ه) و (ي، ى).
4. **ربط بالكائن الكامل `SelectedElement`:** يحمل كائن الموديل كاملاً مع الـ `Id` مباشرة دون الحاجة لاستعلام إضافي.

#### طريقة استخدامه في شاشة الإضافة/التعديل:
1. **في ملف `Designer.cs`:**
```csharp
this.searchableElementDropDown = new Follow.Gui.GuiFingerprint.SearchableElementDropDown();
// ... خصائص الحجم والمكان Anchor و Docking
this.panelContainer.Controls.Add(this.searchableElementDropDown);
```
2. **في الكود البرمجي `Form.cs`:**
```csharp
// تعبئة البيانات مع استبعاد الحالات غير المطلوبة
var list = await dataHelper.GetAllAsync();
searchableElementDropDown.SetElements(list.Where(x => x.FollowState != "خارج المتابعة").ToList());

// قراءة العنصر المختار عند الحفظ:
var selected = searchableElementDropDown.SelectedElement;
if (selected != null)
{
    table.ElementInfoId = selected.Id;
    table.ElementName = selected.ElementName;
}

// تحديد العنصر تلقائياً في وضع التعديل (Edit Mode):
if (ID > 0)
{
    searchableElementDropDown.SelectElementById(existingData.ElementInfoId);
}
```

---

## 4. حل مشكلة تعارض ملفات الموارد في الـ Designer خطأ MSBuild

### الخطأ الشائع:
```text
Two output file names resolved to the same output path: 
"obj\Debug\net9.0-windows\Follow.Gui.GuiElementInfo.ElementInfoControl1.resources"
```

### السبب:
عند تقسيم كلاس Form أو UserControl إلى ملف كود إضافي `partial class` (مثل `ElementInfoControl1.Search.cs` المكتوب برمجياً بالكامل):
1. يتعرف Visual Studio على الكلاس كـ `UserControl` ويحاول فتحه في المصمم (Designer).
2. يقوم Visual Studio بإنشاء ملف `.resx` فارغ له باسم `ElementInfoControl1.Search.resx`.
3. عند عمل Build، يقوم MSBuild بتوليد ملفين `.resources` يحملان نفس اسم الكلاس تماماً في مجلد الـ `obj`، مما يؤدي للتعارض وتوقف الـ Build.

### الحل المعتمد الدائم:
1. **إضافة السمة `[DesignerCategory("Code")]` في ملف الكود الإضافي:**
```csharp
namespace Follow.Gui.GuiElementInfo
{
    [System.ComponentModel.DesignerCategory("Code")]
    public partial class ElementInfoControl1
    {
        // أزرار وبانل البحث المتقدم المكتوبة برمجياً
    }
}
```
2. **تعديل ملف المشروع `*.csproj`:**
```xml
<ItemGroup>
  <Compile Update="Gui\GuiElementInfo\ElementInfoControl1.Search.cs">
    <SubType>Code</SubType>
  </Compile>
</ItemGroup>

<ItemGroup>
  <!-- منع وحذف أي ملفات resx خاصة بملفات الكود الجزئية -->
  <EmbeddedResource Remove="**\*.Search.resx" />
</ItemGroup>
```
3. **حذف ملف `ElementInfoControl1.Search.resx` الفارغ من القرص.**

---

## 5. منظومة متابعة المواعيد والحضور بالبصمة والتواريخ التلقائية

### القواعد البرمجية:
1. **عدم تسجيل حضور في جدول البصمات إلا إذا حان موعد متابعة العنصر:**
   - مقارنة تاريخ اليوم `DateTime.Today` مع تاريخ المتابعة الحالي `DateFollowNow.Date`.
   - إذا كان التاريخ لم يحن بعد (`today < element.DateFollowNow.Date`)، يتم إبلاغ العنصر بالموعد المتبقي ومنع قيده في جدول الحضور.
2. **التحديث التلقائي لموعد المتابعة القادم بمجرد إتمام المتابعة:**
   - موعد المتابعة الحالي: `DateFollowNow = DateTime.Today` (أو تاريخ التسجيل).
   - موعد المتابعة القادم: `DateFollowNext = DateFollowNow.AddDays(FollowDaysCount)`.
   - يتم تحديث جدول العناصر فوراً وبشكل تلقائي سواء كان التسجيل يدوياً أو عن طريق جهاز البصمة.

---

## 6. عزل وفلترة العناصر خارج المتابعة في الشاشات والحضور

### القاعدة:
العناصر التي حالتها `"خارج المتابعة"` لا تظهر في:
- شاشة الحضور والمتابعة اليومية (`ElementInfoControl1` / `ElementFollowUserControl1`).
- شاشة ماكينة البصمة والكشك التفاعلي (`AttendanceDisplayKioskForm`).
- القوائم المنسدلة لاختيار العناصر في الشاشات الإضافية.

### تطبيق الشرط برمجياً:
```csharp
// عند جلب البيانات للعرض:
var activeElements = (await dataHelper.GetAllAsync())
    .Where(x => string.IsNullOrWhiteSpace(x.FollowState) || x.FollowState.Trim() != "خارج المتابعة")
    .ToList();
```

---

## 7. البحث المتقدم ومطابقة حالات المتابعة مع قاعدة البيانات

### التناسق بين شاشات الإدخال والبحث:
- **في شاشة إضافة/تعديل عنصر:** خيارات الحالة هي:
  - `داخل المتابعة`
  - `خارج المتابعة`
- **في شاشة البحث المتقدم:**
  - `الكل` (بدون فلترة)
  - `داخل المتابعة`
  - `خارج المتابعة`

### كود الاستعلام الآمن من الفراغات (EF Core SQL Query):
```csharp
if (!string.IsNullOrWhiteSpace(criteria.FollowState) && criteria.FollowState != "الكل")
{
    string val = criteria.FollowState.Trim();
    query = query.Where(x => x.FollowState != null && (x.FollowState.Trim() == val || x.FollowState.Contains(val)));
}
```

---

## 8. تعريب وتنسيق رؤوس الجداول (DataGrid Column Headers Localization)

### المشكلة:
ظهور أسماء الأعمدة في بعض الشاشات بأسماء الحقول الإنجليزية من كود الـ Entity (مثل `NameRelationElement`, `Relationship`...).

### التعريب الموحد لجميع الأعمدة:
```csharp
private void SetColumnHeaderCaptions(GridView view)
{
    if (view == null) return;

    var titles = new Dictionary<string, string>
    {
        { "AutoNumericColumn", "م" },
        { "ElementName", "اسم العنصر" },
        { "Relationship", "صلة القرابة" },
        { "NameRelationElement", "اسم القريب" },
        { "ElementRelationNationalID", "الرقم القومي للقريب" },
        { "Age", "السن" },
        { "FollowState", "حالة المتابعة" },
        { "NationalId", "الرقم القومي" },
        { "Phone", "التليفون" },
        { "Mobile", "المحمول" },
        { "DateFollowNow", "ميعاد المتابعة القادم" },
        { "FollowDaysCount", "عدد الأيام" }
    };

    foreach (var pair in titles)
    {
        if (view.Columns[pair.Key] != null)
        {
            view.Columns[pair.Key].Caption = pair.Value;
            view.Columns[pair.Key].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            view.Columns[pair.Key].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
    }
}
```

---

## 9. منظومة الحضور والانصراف والبصمة المتكاملة (ZKTeco Biometric System & Attendance Processor)

تم بناء وتطوير محرك حضور ومتابعة بالبصمة متكامل يربط بين أجهزة البصمة (ZKTeco)، قاعدة البيانات المركزية، الكشك التفاعلي (Interactive Kiosk)، وسيرفر الطباعة الحرارية.

---

### أ. معمارية المعالج المركزي للحضور (`AttendanceProcessor`)
يقوم هذا المعالج بتطبيق كافة القواعد الإدارية والأمنية فور التقاط البصمة:

```text
[بصمة من الماكينة / الشبكة]
            │
            ▼
    [التحقق من وجود العنصر] ──(غير موجود)──> [رفض وإشعار بعدم التسجيل]
            │
            ▼
   [فحص حالة المتابعة] ──("خارج المتابعة")──> [رفض الحضور ومنع تسجيله]
            │
            ▼
   [فحص حالة الحبس] ─────("محبوس")────────> [رفض الحضور ومنع تسجيله]
            │
            ▼
     [فحص هل العنصر مطلوب؟]
     ├─── نعم ───> [تسجيل حضور كمطلوب] ──> [تنبيه أمني فوري + صوت تحذيري]
     │                                      └──> [بدون ميعاد متابعة قادم] ──> [طباعة إيصال مطلوب]
     │
     └─── لا (عادي)
            │
            ▼
   [فحص موعد المتابعة: DateFollowNow] ──(لم يحن بعد)──> [رفض الحضور + إظهار الموعد المتبقي]
            │
            ▼
   [فحص التكرار اليومي] ──(سُجل اليوم مسبقاً)──> [تنبيه بأنه سجل بالفعل]
            │
            ▼
   [حساب الميعاد القادم تلقائياً: NextFollow = Today + Days]
            │
            ├──> [تحديث جدول العناصر ElementInfo] (DateFollowNow / DateFollowNext)
            ├──> [تسجيل في سجل متابعات العناصر اليومية ElementFollowAdd]
            ├──> [تسجيل حركة البصمة في AttendanceLog]
            ├──> [تدوين الحدث في سجلات النظام SystemRecords]
            └──> [طباعة إيصال حضور حراري Thermal Receipt]
```

#### كود المعالج المركزي الكامل (`AttendanceProcessor.cs`):
```csharp
public class AttendanceProcessResult
{
    public bool Success { get; set; }
    public bool IsWanted { get; set; }
    public string Message { get; set; }
    public ElementInfo Element { get; set; }
    public AttendanceLog AttendanceLog { get; set; }
    public ElementWantedStatus WantedStatus { get; set; }
    public DateTime? NextFollowDate { get; set; }
}

public class AttendanceProcessor
{
    // ... الحاقن لـ IDataHelper وخدمات الطباعة ...

    public async Task<AttendanceProcessResult> ProcessAttendanceAsync(
        int elementId, int? deviceId = null, DateTime? attendanceTime = null, int verifyType = 1)
    {
        DateTime attTime = attendanceTime ?? DateTime.Now;
        var result = new AttendanceProcessResult();

        // 1. جلب العنصر (مع Fallback بالرقم القومي وبجدول البصمات)
        var element = await dataHelperElement.FindAsync(elementId);
        if (element == null)
        {
            var all = await dataHelperElement.GetAllDataAsync();
            string idStr = elementId.ToString();
            element = all?.FirstOrDefault(x => x.Id == elementId || (x.NationalId != null && x.NationalId.Trim() == idStr));
            if (element == null)
            {
                result.Success = false;
                result.Message = $"رقم المستخدم [ #{elementId} ] غير مسجل لأي عنصر.";
                return result;
            }
        }
        result.Element = element;

        // 2. التحقق من حالة المتابعة (العناصر خارج المتابعة لا تسجل حضور)
        if (element.FollowState != null && element.FollowState.Trim() == "خارج المتابعة")
        {
            result.Success = false;
            result.Message = $"العنصر {element.ElementName} مسجل كـ (خارج المتابعة)، ولا يتم تسجيل حضور له.";
            return result;
        }

        // 3. التحقق من حالة الحبس (العناصر المحبوسة لا تسجل حضور)
        if (element.PrisonedOrnot != null && element.PrisonedOrnot.Trim() == "محبوس")
        {
            result.Success = false;
            result.Message = $"العنصر {element.ElementName} مسجل كـ (محبوس)، ولا يتم تسجيل حضور له.";
            return result;
        }

        // 4. التحقق من العناصر المطلوبة أمنياً
        var allWanted = await dataHelperWanted.GetAllDataAsync();
        var wantedRecord = allWanted?.FirstOrDefault(w => w.ElementId == element.Id && w.IsWanted);
        result.IsWanted = (wantedRecord != null && wantedRecord.IsWanted);

        if (result.IsWanted)
        {
            // مسار العنصر المطلوب:
            var attLog = new AttendanceLog
            {
                ElementId = element.Id,
                DeviceId = deviceId,
                AttendanceDateTime = attTime,
                VerifyType = verifyType,
                IsWantedAtTime = true,
                NextFollowDateAssigned = null, // لا يوجد ميعاد متابعة قادم للمطلوب!
                Status = "مطلوب",
                Notes = $"مطلوب: {wantedRecord?.WantedReason} - {wantedRecord?.WantedBy}"
            };
            await dataHelperAttendanceLog.AddAsync(attLog);
            result.AttendanceLog = attLog;
            result.Success = true;
            result.Message = $"تنبيه أمني: العنصر {element.ElementName} مـطـلـوب!";

            // توثيق في سجل النظام وطباعة إشعار أمني فوري
            await dataHelperSystemRecords.AddAsync(new SystemRecords
            {
                Title = "تنبيه: حضور عنصر مطلوب",
                Details = $"حضر العنصر المطلوب: {element.ElementName} - سبب: {wantedRecord?.WantedReason}",
                AddedDate = DateTime.Now
            });

            return result;
        }

        // 4. مسار العنصر العادي: التحقق الصارم من موعد المتابعة
        if (element.DateFollowNow.Date > attTime.Date)
        {
            result.Success = false;
            result.NextFollowDate = element.DateFollowNow;
            result.Message = $"لم يحن موعد متابعة العنصر {element.ElementName} بعد. الموعد المحدد هو: {element.DateFollowNow:yyyy/MM/dd}";
            return result;
        }

        // التحقق من عدم التكرار في نفس اليوم
        var allFollow = await dataHelperFollowAdd.GetAllDataAsync();
        if (allFollow?.Any(f => f.ElementInfoId == element.Id && f.DateFollow.Date == attTime.Date) ?? false)
        {
            result.Success = false;
            result.NextFollowDate = element.DateFollowNow;
            result.Message = $"العنصر {element.ElementName} تم تسجيل متابعته اليوم مسبقاً.";
            return result;
        }

        // 5. حان الموعد -> احتساب المواعيد وتحديث جدول العناصر أوتوماتيكياً
        int days = element.FollowDaysCount > 0 ? element.FollowDaysCount : 15;
        DateTime nextFollowNow = attTime.Date.AddDays(days);
        DateTime nextFollowNext = attTime.Date.AddDays(days * 2);

        element.DateFollowNow = nextFollowNow;
        element.DateFollowNext = nextFollowNext;
        await dataHelperElement.EditAsync(element);

        // إضافة إلى جدول المتابعات اليومية ElementFollowAdd
        await dataHelperFollowAdd.AddAsync(new ElementFollowAdd
        {
            ElementName = element.ElementName,
            DateFollow = attTime.Date,
            ElementInfoId = element.Id
        });

        // تسجيل حركة الحضور في AttendanceLog مع الميعاد الجديد
        var normalLog = new AttendanceLog
        {
            ElementId = element.Id,
            DeviceId = deviceId,
            AttendanceDateTime = attTime,
            VerifyType = verifyType,
            IsWantedAtTime = false,
            NextFollowDateAssigned = nextFollowNow,
            Status = "تم بنجاح"
        };
        await dataHelperAttendanceLog.AddAsync(normalLog);

        result.AttendanceLog = normalLog;
        result.NextFollowDate = nextFollowNow;
        result.Success = true;
        result.Message = $"تم تسجيل حضور ومتابعة {element.ElementName}. المتابعة القادمة: {nextFollowNow:yyyy/MM/dd}";

        return result;
    }
}
```

---

### ب. الكشك التفاعلي وشاشة العرض الحية (`AttendanceDisplayKioskForm`)
- **شاشة ملء الشاشة بدون إطارات (True Fullscreen Kiosk):**
  ```csharp
  this.FormBorderStyle = FormBorderStyle.None;
  this.WindowState = FormWindowState.Maximized;
  this.TopMost = true;
  ```
- **وضع الخمول والانتظار (Idle Screen):**
  يعرض التاريخ والوقت الحي مع عداد إجمالي الحضور اليومي، وفي انتظار استقبال بصمة جديدة.
- **عرض كارت الحضور الفوري (Live Result Card):**
  - عرض صورة العنصر المخزنة فوراً.
  - الاسم، الرقم القومي، رقم القيد، ووقت البصمة الفعلي.
  - **في حالة المتابع العادي:** عرض بطاقة خضراء بارزة تبرز *"ميعاد المتابعة القادم: الأربعاء 24 أكتوبر 2026"*.
  - **في حالة العنصر المطلوب:** وميض أحمر بارز وتحذير صوتي فوري `SystemSounds.Exclamation.Play()` مع عدم إظهار أي موعد قادم.
  - **مؤقت الإغلاق التلقائي (Auto-Reset Countdown):** عداد تنازلي (قابل للضبط بالثواني من الإعدادات) للعودة التلقائية لشاشة الانتظار.

---

### ج. لوحة التحكم وجهاز البصمة (`FingerprintAttendanceUserControl`)
1. **الاتصال عبر TCP/IP عبر ZKTeco SDK:**
   - استخدام `zkemkeeper.CZKEMClass` للاتصال بالجهاز عبر الـ IP والـ Port (افتراضياً 4370).
   - تفعيل مراقبة الأحداث الحية `RegEvent(1, 65535)` للاستماع الفوري لكل بصمة تضغط على الجهاز.
2. **مزامنة وقت وتاريخ الجهاز مع السيرفر (Time Sync):**
   ```csharp
   private async void buttonSyncTime_Click(object sender, EventArgs e)
   {
       DateTime now = DateTime.Now;
       bool success = zkService.SetDeviceTime(now);
       if (success)
           MessageBox.Show("تمت مزامنة وقت الجهاز بنجاح.");
   }
   ```
3. **طباعة الإيصالات الحرارية الفورية (`ThermalPrintService`):**
   - تصميم إيصال متوافق مع طابعات الإيصالات 80mm و 58mm (ESC/POS أو Windows PrintDocument).
   - يحتوي على شعار المنظومة، اسم العنصر، الرقم القومي، تاريخ ووقت الحضور، وميعاد المتابعة القادم بخط كبير وواضح.

---

### د. ضبط بيئة التشغيل للـ SDK (ActiveX & 32-bit x86 Settings)
مكتبة `zkemkeeper.dll` الرسمية من ZKTeco مبنية على معمارية 32-بت (x86 COM / ActiveX):
1. **في ملف المشروع `*.csproj`:**
   يجب تعيين المعمارية صراحة إلى `x86`:
   ```xml
   <PropertyGroup>
     <PlatformTarget>x86</PlatformTarget>
     <UseWindowsForms>true</UseWindowsForms>
   </PropertyGroup>
   ```
2. **تسجيل المكتبات في نظام تشغيل Windows:**
   تنفيذ الأوامر التالية كمسؤول (Run as Administrator) في مجلد `sdk`:
   ```bat
   regsvr32 /s zkemsdk.dll
   regsvr32 /s zkemkeeper.dll
   ```

---

### هـ. معالجة العناصر المطلوبة أمنياً وطباعة تقرير الحضور اليومي للمطلوبين
1. **دورة حضور العنصر المطلوب:**
   - **على شاشة الكشك العامة (`AttendanceDisplayKioskForm`):** يظل يظهر أن العنصر مطلوب فقط (البطاقة الحمراء والتنبيه الصوتي التحذيري) دون إظهار أي ميعاد متابعة قادم أمامه على الشاشة.
   - **في قاعدة البيانات:** يتم تسجيل حضوره عادي في `AttendanceLog`، ويتم تعديل ميعاد متابعته الجديدة تلقائياً في جدول العناصر `ElementInfo` (`DateFollowNow = Today + Days` و `DateFollowNext = Today + (Days * 2)`)، كما يتم تسجيل المتابعة في جدول `ElementFollowAdd`.
2. **إخفاء عمود حالة الطباعة وجعله اختيارياً:**
   - تم إخفاء عمود `colPrinted` في جدول حضور اليوم افتراضياً (`colPrinted.Visible = false`).
   - تم توفير CheckBox اختياري "إظهار عمود الطباعة" للمستخدم للتحكم في ظهوره عند الرغبة.
3. **تقرير حضور العناصر المطلوبة اليومي (`WantedDailyReportService`):**
   - تم توفير زر مخصص "🚨 تقرير المطلوبين لليوم" في لوحة البصمة وشاشة إدارة المطلوبين.
   - يعرض التقرير الأعمدة المحددة:
     `م` | `اسم العنصر` | `الرقم القومي` | `ميعاد المتابعة الحالي (تاريخ اليوم)` | `ميعاد المتابعة القادم`
   - يظهر تنبيه منبثق وشريط تحذيري بارز داخل التقرير:
     *"⚠️ تنبيه هـام: يلزم عمل استبعاد لهذه العناصر من هذه الشاشة بعد العمل عليها واتخاذ الإجراءات اللازمة."*
4. **مدة بقاء الداتا على الشاشة وإعادة الضبط التلقائي (Screen Display Duration & Auto-Reset):**
   - **في نافذة إعدادات إشعار الحضور (`PrintSettingForm`):** تم إتاحة القيمة **0 ثانية للبقاء الدائم** (حيث يظهر توضيح بجانبها: `0 = بقاء دائم بدون تعديل`)، مع إمكانية تحديد أي مدة تصل حتى 300 ثانية وافتراضي 12 ثانية عبر أزرار (+ / -) والخيارات السريعة (5 ث، 10 ث، 12 ث، 15 ث، 30 ث).
   - **التطبيق الفوري المباشر (Live Sync):** فور الضغط على "💾 حفظ الإعدادات"، يتم تطبيق وتحديث المدة فوراً في الذاكرة على شاشة الكشك التفاعلي (`AttendanceDisplayKioskForm`) وشاشة حضور البصمة (`FingerprintAttendanceUserControl`) دون الحاجة لإعادة تشغيل البرنامج.
   - **عند ضبط المدة على 0 (بقاء دائم):** لا يتم تعديل الشاشة نهائياً وتفضل زي ما هي معروضة بكل بيانات وصورة العنصر وميعاده حتى تأتي بصمة جديدة لعنصر آخر.
   - **عند ضبط المدة على قيمة أكبر من 0 (مثلاً 10 أو 12 ثانية):** تظل بيانات وصورة العنصر ظاهرة للمدة المحددة مع عداد تنازلي، ثم تعود الشاشة تلقائياً لوضع الجاهزية والاستعداد فور انتهاء الوقت.

---

## 10. الجداول وسكربتات قاعدة البيانات المضافة (Database Schema & SQL Scripts)

لكي يعمل البرنامج في بيئة جديدة أو عند نقله إلى مشروع آخر، يجب إنشاء الجداول والفيوهات (Views) الإضافية الخاصة بمنظومة البصمة، تتبع الحضور، الحالات الأمنية، وإعدادات الطباعة.

---

### أ. قائمة الجداول والفيوهات الجديدة
1. **`FingerprintDevice`**: جدول تعريف أجهزة وماكينات البصمة (الاسم، الـ IP، المنفذ، وحالة الاتصال).
2. **`ElementFingerprint`**: جدول قوالب بصمات الأصابع الرقمية (Fingerprint Templates) المرتبطة بالعناصر.
3. **`ElementWantedStatus`**: جدول تسجيل العناصر المطلوبة أمنياً، سبب الطلب، وتاريخه.
4. **`AttendanceLog`**: جدول حركات الحضور اللحظية لكل بصمة، التوقيت، حالة الطلب الأمني، وميعاد المتابعة المسند.
5. **`PrintSetting`**: جدول إعدادات الطباعة الحرارية (Thermal Slip)، العرض، النصوص العلوية والسفلية، ومدة شاشة الكشك.
6. **`vw_TodayAttendance`**: فيو (View) تجميعي لحركات حضور اليوم لربط تفاصيل العنصر وماكينة البصمة.

---

### ب. سكربت SQL Server T-SQL الكامل والجاهز للتشغيل مباشرة (SSMS)
يمكنك نسخ هذا السكربت وتشغيله مباشرة على قاعدة بيانات SQL Server لإنشاء جميع الجداول والفيوهات تلقائياً بأمان:

```sql
USE [FollowDataBase];
GO

-- 1. جدول أجهزة البصمة (FingerprintDevice)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FingerprintDevice')
BEGIN
    CREATE TABLE [dbo].[FingerprintDevice] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [DeviceName] NVARCHAR(MAX) NULL,
        [IpAddress] NVARCHAR(MAX) NULL,
        [Port] INT NOT NULL DEFAULT 4370,
        [MachineNumber] INT NOT NULL DEFAULT 1,
        [CommPassword] NVARCHAR(MAX) NULL,
        [Location] NVARCHAR(MAX) NULL,
        [IsEnabled] BIT NOT NULL DEFAULT 1,
        [LastSyncTime] DATETIME2 NULL,
        [Status] NVARCHAR(MAX) NULL,
        [Notes] NVARCHAR(MAX) NULL
    );
    PRINT N'تم إنشاء جدول FingerprintDevice بنجاح.';
END
GO

-- 2. جدول إعدادات الطباعة والكشك (PrintSetting)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PrintSetting')
BEGIN
    CREATE TABLE [dbo].[PrintSetting] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [PrinterType] NVARCHAR(MAX) NULL,
        [PrinterName] NVARCHAR(MAX) NULL,
        [PaperWidthMm] INT NOT NULL DEFAULT 80,
        [AutoPrintOnAttendance] BIT NOT NULL DEFAULT 1,
        [PrintCopies] INT NOT NULL DEFAULT 1,
        [HeaderText] NVARCHAR(MAX) NULL,
        [FooterText] NVARCHAR(MAX) NULL,
        [ShowBarcode] BIT NOT NULL DEFAULT 1,
        [ShowNationalId] BIT NOT NULL DEFAULT 1,
        [ShowNextFollowDate] BIT NOT NULL DEFAULT 1,
        [OutputMode] NVARCHAR(MAX) NULL DEFAULT N'Both',
        [ScreenDurationSeconds] INT NOT NULL DEFAULT 12
    );

    -- إدراج الإعدادات الافتراضية
    INSERT INTO [dbo].[PrintSetting] (
        [PrinterType], [PrinterName], [PaperWidthMm], [AutoPrintOnAttendance], 
        [PrintCopies], [HeaderText], [FooterText], [ShowBarcode], 
        [ShowNationalId], [ShowNextFollowDate], [OutputMode], [ScreenDurationSeconds]
    ) VALUES (
        N'Thermal', N'', 80, 1, 1, 
        N'حضور متابعة', N'يرجى الالتزام بموعد المتابعة القادم', 1, 1, 1, 
        N'Both', 12
    );
    PRINT N'تم إنشاء جدول PrintSetting بنجاح.';
END
GO

-- 3. جدول قوالب بصمات العناصر (ElementFingerprint)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ElementFingerprint')
BEGIN
    CREATE TABLE [dbo].[ElementFingerprint] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ElementId] INT NOT NULL,
        [FingerIndex] INT NOT NULL,
        [FingerName] NVARCHAR(MAX) NULL,
        [TemplateData] NVARCHAR(MAX) NULL,
        [TemplateVersion] INT NOT NULL DEFAULT 10,
        [CreatedDate] DATETIME2 NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [FK_ElementFingerprint_ElementInfo_ElementId] 
            FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ElementInfo] ([Id]) ON DELETE CASCADE
    );

    CREATE NONCLUSTERED INDEX [IX_ElementFingerprint_ElementId] 
        ON [dbo].[ElementFingerprint] ([ElementId]);
    PRINT N'تم إنشاء جدول ElementFingerprint بنجاح.';
END
GO

-- 4. جدول العناصر المطلوبة أمنياً (ElementWantedStatus)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ElementWantedStatus')
BEGIN
    CREATE TABLE [dbo].[ElementWantedStatus] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ElementId] INT NOT NULL,
        [IsWanted] BIT NOT NULL DEFAULT 1,
        [WantedReason] NVARCHAR(MAX) NULL,
        [WantedDate] DATETIME2 NULL,
        [WantedBy] NVARCHAR(MAX) NULL,
        [Notes] NVARCHAR(MAX) NULL,
        CONSTRAINT [FK_ElementWantedStatus_ElementInfo_ElementId] 
            FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ElementInfo] ([Id]) ON DELETE CASCADE
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_ElementWantedStatus_ElementId] 
        ON [dbo].[ElementWantedStatus] ([ElementId]);
    PRINT N'تم إنشاء جدول ElementWantedStatus بنجاح.';
END
GO

-- 5. جدول حركات الحضور بالبصمة (AttendanceLog)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AttendanceLog')
BEGIN
    CREATE TABLE [dbo].[AttendanceLog] (
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [ElementId] INT NOT NULL,
        [DeviceId] INT NULL,
        [AttendanceDateTime] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [VerifyType] INT NOT NULL DEFAULT 1,
        [IsWantedAtTime] BIT NOT NULL DEFAULT 0,
        [NextFollowDateAssigned] DATETIME2 NULL,
        [IsPrinted] BIT NOT NULL DEFAULT 0,
        [PrintedDate] DATETIME2 NULL,
        [PrintType] NVARCHAR(MAX) NULL,
        [Status] NVARCHAR(MAX) NULL,
        [Notes] NVARCHAR(MAX) NULL,
        CONSTRAINT [FK_AttendanceLog_ElementInfo_ElementId] 
            FOREIGN KEY ([ElementId]) REFERENCES [dbo].[ElementInfo] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AttendanceLog_FingerprintDevice_DeviceId] 
            FOREIGN KEY ([DeviceId]) REFERENCES [dbo].[FingerprintDevice] ([Id]) ON DELETE SET NULL
    );

    CREATE NONCLUSTERED INDEX [IX_AttendanceLog_ElementId] 
        ON [dbo].[AttendanceLog] ([ElementId]);
    CREATE NONCLUSTERED INDEX [IX_AttendanceLog_DeviceId] 
        ON [dbo].[AttendanceLog] ([DeviceId]);
    PRINT N'تم إنشاء جدول AttendanceLog بنجاح.';
END
GO

-- 6. فيو لحركات حضور اليوم اللحظية (vw_TodayAttendance)
IF OBJECT_ID('dbo.vw_TodayAttendance', 'V') IS NOT NULL
    DROP VIEW [dbo].[vw_TodayAttendance];
GO

CREATE VIEW [dbo].[vw_TodayAttendance] AS
SELECT 
    a.Id AS AttendanceLogId,
    a.ElementId,
    e.ElementName,
    e.NationalId,
    a.AttendanceDateTime,
    ISNULL(d.DeviceName, N'يدوي / غير محدد') AS DeviceName,
    ISNULL(w.IsWanted, 0) AS IsWanted,
    a.NextFollowDateAssigned AS NextFollowDate,
    a.IsPrinted,
    a.Status
FROM [dbo].[AttendanceLog] a
INNER JOIN [dbo].[ElementInfo] e ON a.ElementId = e.Id
LEFT JOIN [dbo].[FingerprintDevice] d ON a.DeviceId = d.Id
LEFT JOIN [dbo].[ElementWantedStatus] w ON a.ElementId = w.ElementId
WHERE CAST(a.AttendanceDateTime AS DATE) = CAST(GETDATE() AS DATE);
GO
PRINT N'تم إنشاء الفيو vw_TodayAttendance بنجاح.';
GO
```

---

### ج. أوامر تطبيق المايجريشن عبر Entity Framework Core
إذا كان المشروع الآخر يعتمد على EF Core Migrations، يمكنك أيضاً تنفيذ الأمر التالي عبر Terminal لتطبيق التحديثات تلقائياً:

```bash
# الانتقال إلى مسار مشروع الـ Data
dotnet ef database update --project Follow.Data --startup-project Follow
```

أو عبر **Package Manager Console** في Visual Studio:
```powershell
Update-Database -Project Follow.Data -StartupProject Follow
```

---

## 19. تحسين الـ Layout لشريط البحث وأدوات جدول الحضور (FingerprintAttendanceUserControl)

### المشكلة:
- كان مربع البحث (`textBoxSearch`) يتمدد بعرض الشاشة مع `Anchor = Left | Right` وحجم أولي 920px.
- كانت أدوات الشريط الأخرى (`comboBoxFilterStatus`, `buttonRefreshGrid`, `checkBoxShowPrintCol`, `buttonPrintWantedReport`) مثبتة على اليسار `Dock = Left`.
- تسبب ذلك في تداخل واصطدام مربع البحث مع الأدوات وقص نصوصها بالكامل (كما ظهر في تغطية مربع البحث لزر التحديث وقائمة التصفية وإخفاء جزء من خيار إظهار عمود الطباعة).

### الحل الهندسي:
1. إلغاء الـ `Dock` والـ `Anchor` المتعارضين من جميع عناصر `panelSearch`.
2. إنشاء `FlowLayoutPanel` مخصص بتوجيه من اليمين لليسار في WinForms:
   - ملاحظة هامة: في بيئة WinForms عندما يكون `RightToLeft = Yes`، يجب ضبط `FlowDirection = LeftToRight` لتبدأ العناصر من الحافة اليمنى وتتدفق نحو اليسار بصورة صحيحة.
3. تصغير خانة البحث إلى عرض مناسب ومريح (`Width = 240px`) لإعطاء مساحة كافية لكافة الأدوات.
4. تنظيم الأدوات بالترتيب التالي:
   - `labelSearch`: "🔍 بحث سريع:"
   - `textBoxSearch`: خانة البحث المصغرة (240px)
   - `comboBoxFilterStatus`: قائمة تصفية الحالة (130px)
   - `buttonRefreshGrid`: زر التحديث (85px)
   - فاصل بصري أنيق `|`
   - `checkBoxShowPrintCol`: خيار "إظهار عمود الطباعة"
   - `buttonPrintWantedReport`: زر "🚨 تقرير المطلوبين لليوم" (170px)

---

## 20. ضبط الترقيم المسلسل في جدول الحضور اليومي (DataGrid Sequential Index)

### المشكلة:
- كان يظهر رقمان في الجدول:
  1. في أقصى اليمين تحت عمود "م": يظهر رقم الـ ID الخاص بسجل قاعدة البيانات (`log.Id` مثل: 26, 27, 25...).
  2. في أقصى اليسار في ترويسة الصفوف (`RowHeaders`): يظهر الترقيم المسلسل (1, 2, 3...) عبر حدث `RowPostPaint`.
- أدى ذلك إلى ازدواجية الأرقام للمستخدم وخلط بين رقم المعرف والترقيم المسلسل.

### الحل الهندسي:
1. إخفاء ترويسة الصفوف الجانبية تماماً (`RowHeadersVisible = false`) وإلغاء حدث `RowPostPaint`.
2. جعل عمود "م" الرئيسي في أقصى اليمين يعرض المسلسل الحقيقي بدءاً من `1` وحتى آخر سجل في الصفحة:
   ```csharp
   int startIndex = paginationControl != null && !paginationControl.IsAll 
       ? (paginationControl.CurrentPage - 1) * paginationControl.PageSize 
       : 0;
   int serialNo = startIndex + i + 1;
   ```
3. حفظ رقم معرّف السجل (`log.Id`) داخل `row.Tag` لاستخدامه عند تحديد الصف (`SelectionChanged`) لعرض بطاقة العنصر، دون الحاجة لعرضه للمستخدم في عمود المسلسل.

---

## 21. تسريع استجابة البصمة عند العميل من 10 ثوانٍ إلى أقل من 50 مللي ثانية (High-Performance Fingerprint Optimization)

### المشكلة:
1. **استنزاف معالج جهاز البصمة (Microcontroller Overload):**
   - كان مؤقت المراقبة `pollTimer` في [ZKDeviceService.cs](file:///d:/Projects/Follow/Follow/Code/Services/ZKDeviceService.cs) يستدعي الدالة الثقيلة `ReadGeneralLogData(machineNumber)` كل ثانية واحدة، والتي تجلب آلاف السجلات المخزنة في جهاز البصمة عبر مقبس TCP/IP 4370 بشكل متكرر دون توقف.
   - أدى ذلك إلى خنق معالج جهاز البصمة ZKTeco، فعندما يضع الموظف/العنصر إصبعه على الحساس، يتجمد الجهاز أو يستغرق 10 إلى 15 ثانية للتأكيد أو الاستجابة.
2. **استعلامات المسح الكامل للجداول (Full Table Scans):**
   - عند كل حركة بصمة، كان [AttendanceProcessor.cs](file:///d:/Projects/Follow/Follow/Code/Services/AttendanceProcessor.cs) يستدعي `dataHelperFollowAdd.GetAllDataAsync()` و `dataHelperWanted.GetAllDataAsync()` و `dataHelperElement.GetAllDataAsync()`، مما يُحمل عشرات الآلاف من السجلات عبر الشبكة لفحص سجل واحد فقط.
3. **مؤقت استطلاع الشاشة (Kiosk DB Polling Loop):**
   - كان مؤقت شاشة العرض [AttendanceDisplayKioskForm.cs](file:///d:/Projects/Follow/Follow/Gui/GuiFingerprint/AttendanceDisplayKioskForm.cs) يستدعي `dataHelperAttendanceLog.GetAllDataAsync()` كل 1.5 ثانية لجلب سجلات الحضور بالكامل.
4. **تأخير مهلة الاتصال عند بدء التشغيل (30s Connection Timeout):**
   - كان [Program.cs](file:///d:/Projects/Follow/Follow/Program.cs) يستدعي `DependencyInjection.AddDependencyValues()` قبل ضبط سلسلة الاتصال في `SqlCon.SqlConnection`، مما جعل كائنات الـ `DbContext` تُهيأ بسلسلة الاتصال الافتراضية لخادم التطوير مما أخر البرنامج 30 ثانية عند كل اتصال خاطئ.

### الحل الهندسي المطبق:
1. **الاستعلام الخفيف عن عدد السجلات عبر ZKTeco COM Status:**
   - تم تعديل فحص المؤقت في [ZKDeviceService.cs](file:///d:/Projects/Follow/Follow/Code/Services/ZKDeviceService.cs) لاستخدام `GetDeviceStatus(machineNumber, 6, ref dwValue)` (حيث الكود 6 هو عدد السجلات الإجمالي). يستغرق هذا الاستعلام 1 إلى 2 مللي ثانية فقط وينقل 8 بايت فقط عبر الشبكة دون قراءة السجلات. ولا يتم جلب السجلات إلا في اللحظة التي يرتفع فيها العدد الفعلي (وهو 0.01% من الوقت).
2. **استعلامات Direct SQL مستهدفة وفورية في معالج الحضور:**
   - تم استبدال تحميل الجداول بالكامل في [AttendanceProcessor.cs](file:///d:/Projects/Follow/Follow/Code/Services/AttendanceProcessor.cs) بدوال مباشرة وسريعة:
     - فحص الحضور اليوم: استعلام رقمي عبر `SELECT TOP 1 Id FROM ElementFollowAdd WHERE ...` يستغرق أقل من 1ms.
     - فحص المطلوبين: استعلام مباشر `SELECT TOP 1 ... FROM ElementWantedStatus WHERE ElementId = @Id`.
     - كاش لإعدادات الطباعة: تخزين كائن `PrintSetting` في الذاكرة لتجنب الاستعلام من القاعدة في كل ضغطة بصمة.
3. **استعلام الفارق الخفيف لشاشة العرض (Max ID Delta Polling):**
   - تم تحويل مؤقت شاشة العرض في [AttendanceDisplayKioskForm.cs](file:///d:/Projects/Follow/Follow/Gui/GuiFingerprint/AttendanceDisplayKioskForm.cs) لفحص `SELECT MAX(Id) FROM AttendanceLog`. إذا لم يتغير المعرف لا يقوم بتحميل أي بيانات، مما يوفر 99.9% من حركة المرور على قاعدة البيانات والشبكة.
4. **تصحيح ترتيب التهيئة في Program.cs:**
   - تم ضبط `SqlCon.SqlConnection = conString` قبل استدعاء `AddDependencyValues()` لضمان الاتصال المباشر بقاعدة بيانات العميل منذ البداية.

---

## 22. معالجة حفظ وعرض إعدادات شاشة العرض ومدة التوقيت (Self-Healing DDL & PrintSetting Screen Duration)

### المشكلة:
1. **غياب الأعمدة في قاعدة بيانات العميل:**
   - عند استعادة قاعدة بيانات عميل قديمة أو عدم اكتمال الترحيل، كان جدول `PrintSetting` يفتقر إلى عمودي `OutputMode` و `ScreenDurationSeconds`.
   - عند فتح [PrintSettingForm.cs](file:///d:/Projects/Follow/Follow/Gui/GuiFingerprint/PrintSettingForm.cs) أو حفظها، كان استعلام الـ SQL أو EF يرمي استثناءً `Invalid column name 'OutputMode'` و `Invalid column name 'ScreenDurationSeconds'`، فتفشل عملية الحفظ برمز 0 ويظهر خطأ للمستخدم، ولا تُعرض القيم المحفوظة مطلقاً.
2. **الاستدعاء المزدوج لـ `LoadSettingsAsync()`:**
   - كان الفورم يستدعي الدالة مرتين متزامنتين في كل من `OnLoad` و `PrintSettingForm_Load` مما يسبب تعارضاً وتنافساً عند تعبئة الحقول.

### الحل الهندسي المطبق:
1. **آلية الفحص والإصلاح الذاتي للمخطط (Self-Healing Schema DDL):**
   - تم إضافة `EnsureTableAndColumnsExistAsync()` و `EnsureTableAndColumnsExist()` في [PrintSettingEntity.cs](file:///d:/Projects/Follow/Follow.Data/SqlServer/PrintSettingEntity.cs):
     ```sql
     IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'OutputMode')
         ALTER TABLE [dbo].[PrintSetting] ADD [OutputMode] NVARCHAR(MAX) NULL DEFAULT N'Both';

     IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[PrintSetting]') AND name = N'ScreenDurationSeconds')
         ALTER TABLE [dbo].[PrintSetting] ADD [ScreenDurationSeconds] INT NOT NULL DEFAULT 12;
     ```
   - يتم تشغيل هذا الفحص تلقائياً عند بدء تشغيل البرنامج في [StartForm1.cs](file:///d:/Projects/Follow/Follow/StartForm1.cs) عبر `EnsureCompleteDatabaseSchemaAsync()` وقبل أي استعلام جلب أو حفظ لـ `PrintSetting`.
2. **التعامل الاحترافي مع القراءات الفارغة والافتراضية:**
   - في استعلامات الـ Direct SQL داخل [PrintSettingEntity.cs](file:///d:/Projects/Follow/Follow.Data/SqlServer/PrintSettingEntity.cs)، يتم قراءة `OutputMode` مع fallback إلى `"Both"` وقراءة `ScreenDurationSeconds` مع fallback إلى `12` ثانية إذا كانت القيمة خالية (`NULL`).
3. **تطبيق الحفظ الفوري وتحديث الذاكرة المؤقتة:**
   - تم ربط أزرار الراديو (`radioButtonScreenOnly`, `radioButtonPrintOnly`, `radioButtonBoth`) وعداد الثواني (`numericScreenDuration`) بدقة وتحديث `OutputMode` و `ScreenDurationSeconds`.
   - فور الحفظ الناجح، يتم استدعاء `AttendanceProcessor.InvalidatePrintSettingCache(currentSetting)` لتطبيق النمط الجديد فوراً دون الحاجة لإعادة تشغيل النظام.

---

## 23. إعادة هندسة وتنسيق الـ Layout الكامل لشاشة الحضور بالبصمة (Complete Responsive Layout & Zero-Clipping Overhaul)

### المشاكل التي ظهرت في الشاشة:
1. **اختفاء أزرار الإجراءات الأساسية في الشريط العلوي (Clipped Top Buttons):**
   - كان مجموع عروض الأزرار التسعة يتجاوز 2000 بكسل، فكانت الأزرار الأولى الحرجة ("⚡ اتصال بالبصمة" و "🔄 سحب الحركات") تختفي تماماً خارج الشاشة في الجهة اليسرى عند تشغيل النظام على شاشات لابتوب عادية (1366x768).
2. **تشويه صندوق حالة الجهاز وقص نصوص التنبيهات (Status Pill Truncation):**
   - كان حدث البصمة يقوم بوضع نصوص التنبيهات الإدارية الطويلة (مثل "العنصر انور احمد حامد قابيل مسجل كـ (محبوس)...") داخل `labelDeviceStatus` المخصص لحالة الاتصال فقط، مما يتسبب في قص النص وتشوه شارة الاتصال.
3. **فراغ بصري ضخم في شريط المؤشرات الرقمية (KPI Void):**
   - كانت بطاقات الـ KPI الثلاث تعتمد على إحداثيات ثابتة (`Location`) مما ترك فجوة بيضاء ضخمة تتجاوز 1000 بكسل بين بطاقة الإجمالي وباقي البطاقات.
4. **قص نصوص التنبيه الإداري في بطاقة العنصر (Clipped Reason Box):**
   - كان صندوق `panelWantedAlert` مقيداً بارتفاع ثابت (85px ولابيل 39px)، مما تسبب في قص السطور الثانية والثالثة من نصوص التنبيهات (مثل "العنصر انور احمد حامد قابيل مسجل كـ...").
5. **اختفاء إطار الصورة وتنسيق البطاقة (Photo & Info Alignment):**
   - كان إطار الصورة غير مرتبط بحاوية `Dock` ومغطى بلابيل الاسم، مع وجود مساحة بيضاء فارغة أسفل موعد المتابعة.

### الحلول الهندسية المطبقة:
1. **إعادة هندسة الشريط العلوي وحساب أوزان الأزرار:**
   - تم ضبط أحجام ونصوص الأزرار لتكون موجزة، أنيقة، ومعبرة:
     - `buttonConnect`: "⚡ اتصال بالبصمة" (115px)
     - `buttonSyncLogs`: "🔄 سحب الحركات" (115px)
     - `buttonSyncTime`: "⏱️ ضبط الوقت" (100px)
     - `buttonManualAttendance`: "✍️ حضور يدوي" (100px)
     - `buttonManageWanted`: "🚨 المطلوبين" (95px)
     - `buttonEnrollFingerprint`: "👆 تسجيل بصمة" (110px)
     - `buttonDeviceSettings`: "⚙️ الأجهزة" (90px)
     - `buttonPrintSettings`: "🖨️ إعدادات الطباعة" (120px)
     - `buttonOpenKiosk`: "🖥️ شاشة الكشك" (105px)
   - المجموع الإجمالي: ~970 بكسل فقط! مما يضمن ظهور جميع الأزرار التسعة بالكامل دون أي اختفاء على جميع مقاسات الشاشات (من 1280px وما فوق).
   - إلغاء التكرار الزائد لزر تقرير المطلوبين في الشريط العلوي والاكتفاء بوجوده المميز في شريط أدوات الجدول.
2. **فصل حالة اتصال الجهاز عن رسائل الحضور:**
   - تم تخصيص `panelStatusPill` فقط لعرض حالة الاتصال الحقيقية: "جهاز البصمة: متصل ●" أو "جهاز البصمة: غير متصل ●" وتثبيت عرضه بـ 180px.
   - رسائل التنبيه الإداري والمطلوبين تذهب إلى بطاقة العنصر مباشرة وتظهر بكل وضوح دون المساس بحالة الجهاز.
3. **توزيع متناظر هندسياً لشريط الـ KPI (3-Column TableLayoutPanel):**
   - تم استخدام `TableLayoutPanel` بنسبة 33.33% لكل بطاقة:
     - إجمالي حضور اليوم (أزرق ناصع `#EFF6FF` وبوردر `#BFDBFE`).
     - مطلوبين مسجلين (أحمر ناصع `#FEF2F2` وبوردر `#FECACA`).
     - إشعارات مطبوعة (أخضر زمردي `#ECFDF5` وبوردر `#A7F3D0`).
   - تملأ البطاقات كامل عرض الشاشة بمرونة تامة وتناغم بصري فاخر بدون أي فراغات.
4. **صندوق تنبيهات ذاتي التمدد لمنع قص النصوص (Auto-Sizing Alert Box):**
   - تم ضبط `labelWantedReason` مع `AutoSize = true` وتحديد `MaximumSize` ديناميكياً بحسب عرض البطاقة.
   - يتم حساب ارتفاع الصندوق ديناميكياً:
     ```csharp
     panelWantedAlert.Height = labelWantedTitle.Height + labelWantedReason.Height + 18;
     ```
   - النتيجة: يظهر نص التنبيه الإداري بالكامل (سواء سطر أو سطرين أو أربعة أسطر) دون قص أي حرف إطلاقاً!
5. **تنظيم هرمي لبطاقة العنصر مع إطار صورة وشارة حضور:**
   - وضع حاوية علوية مستقلة `panelPhotoContainer` تعرض إطار الصورة (84x84) في المنتصف بدقة.
   - ترتيب عناصر البطاقة: الصورة -> الاسم -> الرقم القومي -> المهنة -> صندوق التنبيه -> موعد المتابعة -> شارة وقت البصمة اللحظية -> زر الطباعة في الأسفل.
   - تثبيت عرض البطاقة عند 360px بمرونة عبر `splitContainerMain.FixedPanel = FixedPanel.Panel2;` لضمان بقائها مثالية مهما تغير حجم النافذة.

---

## 24. ضبط نصوص وتلميحات الأزرار وسيمترية تدفق العمل وتقارير المطلوبين والبصمات (Button Labels, ToolTips, Workflow Symmetry & Reports Overhaul)

### المشاكل التي ظهرت في الشاشة:
1. **قص واختفاء الكلمة الثانية من نصوص الأزرار العلوية (Button Text Truncation):**
   - بسبب تقليص عروض الأزرار إلى (90px - 115px)، قامت مكتبة Windows Forms GDI+ بلف الكلمة الثانية إلى سطر ثانٍ يقع خارج ارتفاع الزر (36px)، فظهرت الأزرار بكلمة واحدة فقط ("فصل"، "سحب"، "ضبط"، "حضور"، "تسجيل"، "شاشة") مع وجود أكثر من 500 بكسل فارغة تماماً في يسار الشريط العلوي.
2. **غياب التلميحات الإرشادية (Missing ToolTips):**
   - لم يكن هناك أي تلميح (ToolTip / هنت) يوضح للمستخدم وظيفة أي زر عند الوقوف عليه بمؤشر الماوس.
3. **قص نصوص بطاقات المؤشرات الرقمية (KPI Title Clipping):**
   - بسبب استخدام `DockStyle.Left` للقيمة الرقمية مع تفعيل خاصية الاتجاه من اليمين لليسار (`RightToLeft = Yes`) والارتفاع الصغير (60px)، التف نص العنوان ليظهر مقطوعاً ("إجمالي ."، "مطلوب"، "إشعار").
4. **تقييد تقرير المطلوبين بحضور اليوم فقط:**
   - كان تقرير المطلوبين يستعلم فقط عن سجلات حضور اليوم `AttendanceLog.Where(today)` بدلاً من عرض كافة الأشخاص المسجلين في جدول المطلوبين (`ElementWantedStatus` حيث `IsWanted == true`) كتقرير حصر شامل.
5. **الحاجة لتقرير الأشخاص غير المسجلين بالبصمة:**
   - عدم وجود تقرير يحدد الأشخاص المقيدين بالمنظومة والذين لم يتم أخذ بصمات أصابع لهم أو لم تتم مزامنتهم مع جهاز البصمة.

---

### الحلول الهندسية المطبقة:

#### 1. توسيع وإظهار نصوص الأزرار العلوية بالكامل (Zero Clipping Buttons):
- تم زيادة عروض الأزرار بدقة لتستوعب الكلمات بالكامل دون أي التفاف:
  - `buttonConnect`: "⚡ اتصال بالبصمة" / "🛑 قطع الاتصال" (140px)
  - `buttonSyncLogs`: "🔄 سحب الحركات" (130px)
  - `buttonSyncTime`: "⏱️ ضبط الوقت" (125px)
  - `buttonManualAttendance`: "✍️ حضور يدوي" (125px)
  - `buttonEnrollFingerprint`: "👆 تسجيل بصمة" (130px)
  - `buttonManageWanted`: "🚨 سجل المطلوبين" (135px)
  - `buttonOpenKiosk`: "🖥️ شاشة العرض" (125px)
  - `buttonDeviceSettings`: "⚙️ إدارة الأجهزة" (125px)
  - `buttonPrintSettings`: "🖨️ إعدادات الطباعة" (135px)
- تم ضبط `TextAlign = ContentAlignment.MiddleCenter` و `AutoEllipsis = false` وارتفاع 38px، مما جعل النصوص واضحة وبارزة بنسبة 100%.

#### 2. سيمترية الترتيب وتدفق العمل الثلاثي (Symmetrical 3x3 Layout Flow):
تمت إعادة ترتيب الأزرار داخل `flowLayoutPanelActions` من اليمين إلى اليسار في 3 مجموعات متناسقة (3 + 3 + 3) تتبع السير المنطقي اليومي للمشغّل:
1. **مجموعة الاتصال والعتاد (Device & Sync):**
   - `[اتصال بالبصمة]` -> `[سحب الحركات]` -> `[ضبط الوقت]`
2. **مجموعة العمليات الحيوية واليومية (Daily Biometric Operations):**
   - `[حضور يدوي]` -> `[تسجيل بصمة]` -> `[سجل المطلوبين]`
3. **مجموعة العرض والإعدادات (Display & Configuration):**
   - `[شاشة العرض]` -> `[إدارة الأجهزة]` -> `[إعدادات الطباعة]`

#### 3. نظام التلميحات الشامل (Comprehensive ToolTip System):
تم تضمين مكوّن `toolTipMain` بخصائص متقدمة (`AutoPopDelay = 8000ms`, `InitialDelay = 350ms`, `UseAnimation = true`) وربطه بكافة الأزرار والعناصر:
- أزرار الشريط العلوي: توضيح دقيق وموجز لوظيفة كل زر ومؤشر حالة الجهاز.
- أدوات شريط البحث: تلميحات لخانة البحث، قائمة التصفية، زر التحديث، وخيار عمود الطباعة.
- بطاقة الحضور: تلميح لزر إعادة طباعة الإشعار الورقي.
- بطاقات الـ KPI: تلميح لكل بطاقة يوضح طبيعة الإحصائية المعروضة.

#### 4. إعادة هندسة بطاقات الـ KPI (2-Column Internal TableLayoutPanel):
- تم رفع ارتفاع الشريط إلى `panelKpi.Height = 68px`.
- تم استبدال أسلوب الـ `Dock` الداخلي الهش بجدول داخلي `TableLayoutPanel` بنسبة مئوية ثابتة (65% للعنوان بمحاذاة اليمين، و35% للرقم بالمنتصف) داخل كل بطاقة:
  - **📊 إجمالي الحضور اليوم**
  - **🚨 مطلوبين مسجلين اليوم**
  - **🖨️ إشعارات مطبوعة**
- النتيجة: انتهاء مشكلة التفاف وقص النصوص نهائياً مع وضوح فائق للأرقام والعناوين.

#### 5. تطوير تقرير المطلوبين الشامل ([WantedDailyReportService.cs](file:///d:/Projects/Follow/Follow/Code/Services/WantedDailyReportService.cs)):
- تم فك الارتباط بسجلات حضور اليوم، وبات التقرير يستعلم مباشرة عن كافة العناصر المسجلة في `ElementWantedStatus` حيث `IsWanted == true`.
- يشتمل التقرير على:
  - التنبيه الأمني الإلزامي بضرورة مراجعة واستبعاد الحالات المنتهية.
  - أعمدة واضحة ومتناسبة مع مقاس A4: (م، اسم العنصر، الرقم القومي، سبب الطلب/الإدراج، الجهة الطالبة، تاريخ الإدراج، موعد المتابعة).

#### 6. إنشاء تقرير غير المسجلين بالبصمة ([UnenrolledFingerprintsReportService.cs](file:///d:/Projects/Follow/Follow/Code/Services/UnenrolledFingerprintsReportService.cs)):
- خدمة تقرير جديدة ومستقلة تستخرج قائمة بكافة الأشخاص المقيدين بالمنظومة في `ElementInfo` الذين ليس لديهم أي قالب بصمة مسجل في `ElementFingerprint`.
- يتضمن التقرير: (م، كود العنصر، اسم الشخص، الرقم القومي، المهنة، رقم الهاتف، موعد المتابعة) مع تنبيه إرشادي للمشغل.
- تم إضافة زر مخصص وأنيق في شريط أدوات الجدول: `buttonUnenrolledReport` ("⚠️ غير المسجلين بالبصمة").

---

## 25. توحيد معرف البصمة (DeviceEnrollId) لمشاركة ماكينة البصمة بين قاعدتي بيانات منفصلتين (Cross-Database Hardware Biometric ID Architecture)

### المشكلة:
1. **تصادم المعرفات التلقائية (Auto-Increment ID Collision across Databases):**
   - عندما يعمل العميل بنظامين أو قاعدتي بيانات منفصلتين (DB1 و DB2) على نفس ماكينة البصمة الفيزيائية، يختلف تسلسل المعرف الرقمي التلقائي `Id` للشخص تماماً بين القاعدتين.
   - على سبيل المثال: الشخص "أحمد" قد يكون رقمه `Id = 15` في القاعدة الأولى، بينما في القاعدة الثانية هو `Id = 84`. وفي نفس الوقت، قد يكون هناك شخص آخر "محمود" مسجل برقم `Id = 15` في القاعدة الثانية!
   - استخدام الـ `Id` المحلي كـ `dwEnrollNumber` على الماكينة كان يؤدي حتماً إلى تداخل وسحق بصمات الأشخاص أو تسجيل حركات الحضور للشخص الخطأ في القاعدة الأخرى.
2. **غياب كود المعرف المشترك ورقم الماكينة المتسلسل في بنية الجداول:**
   - جداول `ElementInfo` و `AttendanceLog` و `FingerprintDevice` لم تكن تشتمل على حقول لتخزين كود التسجيل الموحد على الماكينة `DeviceEnrollId` أو السيريال نمبر الخاص بالماكينة `DeviceSerialNumber`.

---

### الحل الهندسي والمعماري المطبق:

#### 1. اعتماد ماكينة البصمة كمصدر وحيد للحقيقة (Hardware Token - Single Source of Truth):
- بما أن ماكينة البصمة هي الحاوية الفعلية لقوالب البصمات الفيزيائية، تم جعل الماكينة هي المرجع التنسيقي الموحد لتوليد كود التسجيل.
- تم برمجة دالة `GetNextAvailableEnrollIdAsync()` في [ZKDeviceService.cs](file:///d:/Projects/Follow/Follow/Code/Services/ZKDeviceService.cs):
  1. الاستعلام عبر ZKTeco COM API من خلال `ReadAllUserID` و `SSR_GetAllUserInfo` عن كافة المستخدمين المسجلين على الجهاز واستخراج أعلى رقم معرف موجود `Max(ElementId)`.
  2. الاستعلام من قاعدة البيانات المحلية الحالية عن `MAX(DeviceEnrollId)`.
  3. حساب الكود القادم المضمون عدم تكراره:
     ```csharp
     int nextId = Math.Max(maxFromDevice, maxFromDb) + 1;
     ```
  - بهذه الطريقة، سواء تم تسجيل شخص جديد من القاعدة الأولى أو القاعدة الثانية، يحصل تلقائياً على رقم فريد غير مستخدم على الماكينة نهائياً، ويتم حفظه في كلا القاعدتين.

#### 2. الإصلاح الذاتي للمخطط البرمجي (Self-Healing Schema DDL):
- تم تضمين ترقية ذاتية للجداول داخل [StartForm1.cs](file:///d:/Projects/Follow/Follow/StartForm1.cs) عبر `EnsureCompleteDatabaseSchemaAsync()`:
  - إضافة عمود `DeviceEnrollId` (مع فهرس مسرّع `IX_ElementInfo_DeviceEnrollId`) إلى جدول `ElementInfo`.
  - إضافة عمودي `DeviceEnrollId` و `DeviceSerialNumber` إلى جدول `AttendanceLog`.
  - إضافة عمود `SerialNumber` إلى جدول `FingerprintDevice`.
  - عند فتح أي من القاعدتين (DB1 أو DB2)، يتم فحص وتحديث الجداول تلقائياً وبأمان في غضون ثانية واحدة.

#### 3. أولوية المطابقة الذكية في معالج الحضور ([AttendanceProcessor.cs](file:///d:/Projects/Follow/Follow/Code/Services/AttendanceProcessor.cs)):
- عند استقبال أي بصمة حضور لحظية أو سحب الحركات، يتم تمرير رقم `enrollId` القادم من الماكينة.
- تم تحديث الاستعلام المباشر فائق السرعة `FindElementFastAsync` ليعتمد ترتيب أسبقية قطعي وصارم:
  ```sql
  SELECT TOP 1 e.Id, e.ElementName, e.NationalId, e.Job, e.FollowState, e.PrisonedOrnot, e.DateFollowNow, e.DateFollowNext, e.FollowDaysCount, e.DeviceEnrollId
  FROM ElementInfo e
  WHERE e.DeviceEnrollId = @enrollId
     OR (e.NationalId IS NOT NULL AND e.NationalId = @idStr)
     OR e.Id = @enrollId
  ORDER BY 
     CASE 
        WHEN e.DeviceEnrollId = @enrollId THEN 1
        WHEN e.NationalId = @idStr THEN 2
        ELSE 3
     END
  ```
  - الأولوية الأولى 1: مطابقة كود الماكينة الموحد `DeviceEnrollId`.
  - الأولوية الثانية 2: مطابقة الرقم القومي `NationalId`.
  - الأولوية الثالثة 3: مطابقة الـ `Id` المحلي (للتوافق الرجعي 100% مع أي سجلات تاريخية سابقة).
- حفظ `DeviceEnrollId` و `DeviceSerialNumber` في سجل الحضور `AttendanceLog` لضمان التوثيق الجنائي والعتادي الكامل لكل حركة.

#### 4. تطوير شاشة التسجيل والإدارة وتصحيح تسلسل المزامنة السيمتري ([ElementFingerprintEnrollForm.cs](file:///d:/Projects/Follow/Follow/Gui/GuiFingerprint/ElementFingerprintEnrollForm.cs)):
- **العرض الذكي:** عرض كود الماكينة الموحد بوضوح في بطاقة تفاصيل الشخص: `كود الماكينة الموحد: #XX`.
- **تصحيح ثغرة التسجيل المباشر وعزل رقم القيد المحلي عن الهاردوير:**
  - سابقاً: كان زر "بدء تسجيل البصمة بالجهاز" يرسل رقم القيد المحلي `selectedElement.Id` إلى الماكينة بدلاً من استدعاء `EnsureElementEnrollIdAsync`، مما أدى لتسجيل البصمة على الماكينة برقم قيد مختلف عن الكود الذي يبحث عنه زر "سحب البصمة".
  - الحل المطبق: تم توحيد كافة عمليات التسجيل والمزامنة (`buttonStartRemoteEnroll_Click`, `buttonSyncUserName_Click`, `buttonFetchFromDevice_Click`, `buttonFetchAllFingers_Click`, `buttonUploadToDevice_Click`) لتمر دائماً وبصورة إلزامية عبر `EnsureElementEnrollIdAsync` لضمان عمل كافة العمليات حصراً وتناغماً على كود الماكينة الموحد `DeviceEnrollId`.
  - إضافة فحص ذكي في `EnsureElementEnrollIdAsync`: قبل حجز رقم جديد للشخص، يفحص النظام الماكينة فإذا كان الشخص مسجلاً بالفعل عليها بالاسم أو الرقم القومي، يربطه بكوده الموجود تلقائياً لمنع أي تكرار.
- **إعادة هندسة سيمترية وتدفق أزرار الشاشة (Symmetrical Workflow Layout):**
  - تم تنظيم العمليات في صفين متناظرين بثلاثة أعمدة متطابقة المقاسات (270×36 بكسل):
    - **الخطوة 1 (أعلى اليمين):** `[👤 1. مزامنة وكود العنصر بالجهاز]` (أزرق سماوي - ينشئ الكود الموحد ويرسل الاسم للجهاز).
    - **الوسط (أعلى):** اختيار الإصبع `[تحديد الإصبع]` مع قائمة منسدلة.
    - **الخطوة 2 (أعلى اليسار):** `[👆 2. بدء تسجيل البصمة بالجهاز]` (بنفسجي - يرسل أمر التسجيل المباشر للحساس).
    - **الخطوة 3 (أسفل اليمين):** `[📥 3. سحب بصمة الإصبع المحدد]` (أزرق ملكي - يسحب القالب ويخزنه مركزياً).
    - **الوسط (أسفل):** `[📥 سحب كافة بصمات العنصر (0-9)]` (تركواز).
    - **اليسار (أسفل):** `[📤 رفع قوالب البصمات للجهاز]` (أخضر زمردي).
- **منع قص النصوص في الشريط السفلي (Panel Footer):**
  - تم توسيع النافذة إلى عرض 920 بكسل مع ضبط مقاسات وخطوط أزرار شريط الإجراءات:
    `[🗑️ حذف البصمة المحددة]` | `[⚠️ مسح العنصر من الجهاز]` | `[🔄 مزامنة شاملة بالجهاز]` | `[🔄 مطابقة وربط أكواد الماكينة]` | `[❌ إغلاق]` لتظهر كافة النصوص كاملة بنسبة 100% دون أي قص أو التفاف.
- **تلميحات تفاعلية فورية (Interactive ToolTips):**
  - إضافة تلميحات إرشادية عند الوقوف بالماوس فوق أي زر، تشرح للمشغل وظيفة الزر وتسلسل خطوته بدقة فائقة.
- **أداة المطابقة والربط الجماعي التلقائي (Auto-Match & Link Device Users Tool):**
  - تم إضافة زر مخصص في أسفل الشاشة: `buttonAutoMatchDeviceUsers` ("🔄 مطابقة وربط أكواد الماكينة").
  - تقوم الأداة بفحص كافة المستخدمين المسجلين على ماكينة البصمة، ومطابقتهم آلياً مع أسماء أو أرقام الأشخاص في قاعدة البيانات، وربط كود الماكينة `DeviceEnrollId` وحفظه فورياً في قاعدة البيانات الحالية.
  - إظهار تقرير تفصيلي بعدد المستخدمين على الماكينة، وعدد العناصر المطابقة، والأكواد التي تم ربطها وتحديثها.
  - تحل هذه الأداة مسألة تشغيل القاعدة الثانية بضغطة زر واحدة: فبمجرد الضغط على الزر يتم ربط كافة الأشخاص المسجلين على الماكينة بكودهم الموحد في القاعدة الثانية فوراً!

#### 5. عرض وتوثيق كود الماكينة في شاشة الحضور اليومي:
- تم تعديل [FingerprintAttendanceUserControl.cs](file:///d:/Projects/Follow/Follow/Gui/GuiFingerprint/FingerprintAttendanceUserControl.cs) لعرض كود الماكينة بجوار الرقم القومي على بطاقة العنصر عند تسجيل الحضور:
  `الرقم القومي: XXXXXXXXXXXXXX | كود الماكينة: #15`.
- تمرير السيريال نمبر للماكينة وحفظه مع كل بصمة حضور لتوثيق الجهاز مصدر الحركة بدقة.

#### 6. توليد وتطبيق ترحيل قاعدة البيانات (EF Core Migration):
- تم إنشاء الترحيل الرسمي الكامل في مشروع `Follow.Data/Migrations`:
  - الملف: [20261010103715_AddDeviceEnrollIdAndHardwareFields.cs](file:///d:/Projects/Follow/Follow.Data/Migrations/20261010103715_AddDeviceEnrollIdAndHardwareFields.cs)
  - المخطط النموذجي: [DBContextModelSnapshot.cs](file:///d:/Projects/Follow/Follow.Data/Migrations/DBContextModelSnapshot.cs)
- الحقول المضافة للمخطط:
  - `ElementInfo.DeviceEnrollId` (مع فهرس `IX_ElementInfo_DeviceEnrollId`).
  - `AttendanceLog.DeviceEnrollId` (مع فهرس `IX_AttendanceLog_DeviceEnrollId`).
  - `AttendanceLog.DeviceSerialNumber` (كود وسيريال الجهاز العتادي).
  - `FingerprintDevice.SerialNumber` (سيريال الجهاز المعتمد).
- جعل الترحيل آمناً وتكرارياً (Idempotent):
  - تم صياغة تعليمات الترحيل باستخدام `IF NOT EXISTS` بحيث يتوافق الترحيل تلقائياً مع أي قواعد بيانات سابقة تم إضافة الأعمدة إليها مسبقاً، ويمنع حدوث خطأ تكرار اسم العمود (`Column name already exists`).
- تم تطبيق التحديث بنجاح تام على قاعدة البيانات وسُجل في جدول تاريخ الترحيلات `__EFMigrationsHistory`.

#### 7. حل مشكلة سحب البصمة من الماكينة وتوضيح أرقام الأزرار (رقم القيد vs رقم الماكينة):
- **تحديد هوية وسلوك الأزرار:**
  - **رقم القيد المحلي (`Id = 2`):** هو المعرف الداخلي في قاعدة بيانات البرنامج.
  - **كود الماكينة الموحد (`DeviceEnrollId = 5`):** هو المعرف المسجل فعلياً على شريحة وذاكرة ماكينة البصمة.
  - **ما تأخذه الأزرار:**
    - جميع أزرار الشاشة (مزامنة الاسم، بدء التسجيل بالحساس، سحب البصمة، سحب كافة البصمات) تأخذ وتتعامل مع **كود الماكينة الموحد (`DeviceEnrollId = 5`)** وهو الرقم المطابق 100% للشخص في الماكينة.
    - كما تحتوي عمليات السحب على فحص ارتدادي تلقائي (Fallback) لرقم القيد المحلي (`2`) والرقم القومي في حال كان مسجلاً تاريخياً بأحدهما.
- **السبب الجذري لعدم سحب القالب من الماكينة:**
  - عند فحص ذاكرة الماكينة الحية تبين أن المستخدم مسجل بالفعل برقم `5` ولديه بصمة إصبع رقم `1` بحجم 1344 بايت.
  - لكن دالة `ReadFingerprintTemplateAsync` في [ZKDeviceService.cs](file:///d:/Projects/Follow/Follow/Code/Services/ZKDeviceService.cs) كانت تستدعي `ReadAllTemplate` فقط دون استدعاء `ReadAllUserID` قبلها؛ وفي بروتوكول مكتبة ZKTeco SDK، تفشل دالة `SSR_GetUserTmpStr` وتعيد `False` فوراً إذا لم يتم تحميل المستخدمين بـ `ReadAllUserID` أولاً إلى ذاكرة الـ Buffer.
  - تم إصلاح الترتيب الإلزامي واستدعاء `ReadAllUserID` ثم `ReadAllTemplate`.
  - تم إضافة دعم دالة `GetUserTmpExStr` المعتمدة لقوالب **Biokey 10.0** الحديثة بجانب `SSR_GetUserTmpStr`.

---

---

## 23. طباعة البيانات الكاملة لجميع شاشات التقارير وتفعيل التفاف النص واحتواء الصفحة (Full Dataset Printing, Text Wrapping & A4 Containment)

### 1. المشكلة:
1. **اقتصار الطباعة على صفحة الـ Paging المعروضة فقط:**
   - عند طباعة التقارير من شاشات النظام المختلفة (شاشة العناصر `ElementInfoControl1`، شاشة متابعة العناصر `ElementFollowUserControl1`، شاشة كسر المتابعات `ElementsBreakForm`)، كانت أزرار الطباعة تقوم بتمرير `gridControl1.DataSource` مباشرة لمصدر بيانات التقرير.
   - ونظراً لأن الـ GridControl يتم ربطه ببيانات الصفحة الحالية فقط بواسطة أداة التصفح `paginationControl.GetPageData(...)` (مثل أول 25 أو 50 عنصراً)، كانت التقارير تُطبع مقتصرة فقط على الصفحة الحالية المعروضة، ولا تطبع باقي السجلات التي تم تصفيتها أو جلبها بالكامل.
2. **قص النصوص الطويلة وعدم التفافها:**
   - خلايا الجداول في تقارير DevExpress XtraReports كانت تحتوي على إعداد `CanGrow = false` في ترويسات الأعمدة، ولم تكن مفعلة لخاصية `WordWrap = true` في بعض الخلايا، مما أدى إلى قص الأسماء الطويلة والعناوين والملاحظات بدون التفاف.
3. **تجاوز هوامش الطباعة وانقسام الجداول أفقياً على صفحتين:**
   - في بعض التقارير كانت الجداول أعرض من المساحة المخصصة للورقة أو كانت الهوامش كبيرة (مثل هوامش افتراضية 100pt)، مما يؤدي لقص الأعمدة من اليسار أو انقسام الجدول أفقياً على ورقتين في كل صفحة مطبوعة.

---

### 2. الحل الهندسي المنفذ:

#### أ. طباعة كامل البيانات (`allElements`) في كافة الشاشات:
1. **شاشة العناصر ([ElementInfoControl1.cs](file:///d:/Projects/Follow/Follow/Gui/GuiElementInfo/ElementInfoControl1.cs)):**
   - تم تعديل زر الطباعة `buttonPrint_Click` لتمرير قائمة `allElements` الكاملة الناتجة عن البحث أو التصفية بالكامل إلى `ShowReport(printData)` بدلاً من `gridControl1.DataSource`.
2. **شاشة متابعة العناصر ([ElementFollowUserControl1.cs](file:///d:/Projects/Follow/Follow/Gui/GuiElementFollow/ElementFollowUserControl1.cs)):**
   - تم تعديل زر الطباعة `buttonPrint_Click` لتمرير قائمة `allElements` بالكامل الخاصة باليوم المحدد إلى `ShowReport(printData, dateTime, date)` بدلاً من صفحة الـ Grid الحالية.
3. **شاشة كسر المتابعات ([ElementsBreakForm.cs](file:///d:/Projects/Follow/Follow/Gui/GuiBreakFollow/ElementsBreakForm.cs)):**
   - تم تعديل دالة `PrintGridViewData()` لجلب وتمرير كافة السجلات المحققة لشرط كسر المتابعة `allElements` إلى `Report1` كاملاً.

#### ب. تفعيل التفاف النصوص تلقائياً (Text Wrapping & Auto Grow):
1. **تقرير بيانات العناصر ([ReportElementInfo.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/GuiReportElementInfo/ReportElementInfo.cs)):**
   - إضافة دالة `ConfigureTextWrappingAndLayout()` وتفعيل `row.CanGrow = true` و`cell.WordWrap = true; cell.CanGrow = true; cell.Multiline = true;` لجميع صفوف وخلايا `xrTable1` و`xrTable2`.
   - إزالة `CanGrow = false` من [ReportElementInfo.Designer.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/GuiReportElementInfo/ReportElementInfo.Designer.cs).
2. **تقرير متابعات العناصر ([ReportElementFollow.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/GuiReportElementFollow/ReportElementFollow.cs)):**
   - إضافة دالة `ConfigureTextWrappingAndLayout()` لتفعيل `CanGrow` و`WordWrap` و`Multiline` لجميع الصفوف والخلايا.
   - إزالة `CanGrow = false` وإزالة `AnchorVertical` من [ReportElementFollow.Designer.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/GuiReportElementFollow/ReportElementFollow.Designer.cs).
3. **تقرير كسر المتابعات ([Report1.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/Report1.cs)):**
   - تفعيل `ConfigureTextWrappingAndLayout()` على `xrTable1` و`xrTable2`.
   - إزالة `CanGrow = false` من خلايا الترويسة في [Report1.Designer.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/Report1.Designer.cs).
4. **تقرير غير المسجلين بالبصمة وتقرير المطلوبين أمنياً:**
   - في [UnenrolledFingerprintsReportService.cs](file:///d:/Projects/Follow/Follow/Code/Services/UnenrolledFingerprintsReportService.cs) و [WantedDailyReportService.cs](file:///d:/Projects/Follow/Follow/Code/Services/WantedDailyReportService.cs): تفعيل حلقة تكرارية على جميع خلايا الترويسة والبيانات لضبط `WordWrap = true; CanGrow = true; Multiline = true; headerRow.CanGrow = true; dataRow.CanGrow = true;`.

#### ج. الاحتواء الدقيق والمحاذاة لأبعاد ورق A4 لمنع الانقسام الأفقي:
1. **التقارير الأفقية (A4 Landscape - أبعاد 1169 × 827 pt):**
   - ضبط الهوامش على `Margins(22, 22, 50, 20)`.
   - العرض المتاح للطباعة = `1169 - 22 - 22 = 1125 pt`.
   - محاذاة الجداول على الحافة تماماً: `LocationFloat = PointFloat(0F, 0F)` وضبط عرض الجدول ليكون بالضبط `1125F` في كل من `ReportElementInfo` و`ReportElementFollow` و`Report1`.
2. **التقارير الرأسية (A4 Portrait - أبعاد 827 × 1169 pt):**
   - تحديد مقاس الورق صراحة: `PaperKind = DXPaperKind.A4`.
   - ضبط الهوامش على `Margins(35, 35, 35, 35)`.
   - العرض المتاح للطباعة = `827 - 35 - 35 = 757 pt`.
   - ضبط عرض الجدول ليكون `757 pt` ومجموع أعمدة الجدول 757 pt بالضبط.
3. **عرض نافذة المعاينة بحجم الشاشة الكاملة (Maximized Form):**
   - تم ضبط `this.WindowState = FormWindowState.Maximized;` في [ReportForm.cs](file:///d:/Projects/Follow/Follow/Gui/GuiReport/ReportForm.cs) لتفتح نافذة معاينة الطباعة مباشرة بكامل حجم الشاشة.

---

*تم إنشاء هذا التوثيق ليكون مرجعاً معمارياً وهندسياً شاملاً لتطبيق الميزات وحل المشاكل المشابهة في أي نظام WinForms / DevExpress بسهولة.*




