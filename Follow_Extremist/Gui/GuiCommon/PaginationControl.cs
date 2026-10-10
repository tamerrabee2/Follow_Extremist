using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Follow_Extremist.Gui.GuiCommon
{
    public class PageChangedEventArgs : EventArgs
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
        public bool IsAll { get; set; } = false;
        public int Skip => IsAll ? 0 : (PageNumber - 1) * PageSize;
        public int Take => IsAll ? int.MaxValue : PageSize;
    }

    /// <summary>
    /// شريط ترقيم صفحات عصري وموحد يدعم تحديد عدد الصفوف (10، 20، 30، 50، 100، الكل) والتنقل بين الصفحات
    /// </summary>
    public class PaginationControl : UserControl
    {
        public event EventHandler<PageChangedEventArgs> PageChanged;

        private Label lblPageSizeTitle;
        private ComboBox comboPageSize;
        private Label lblSummary;

        private Button btnFirst;
        private Button btnPrev;
        private ComboBox comboPageNumber;
        private Label lblFromWord;
        private Label lblTotalPages;
        private Button btnNext;
        private Button btnLast;

        private bool isUpdatingUI = false;

        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 30;
        public bool IsAll { get; private set; } = false;
        public int TotalRecords { get; private set; } = 0;
        public int TotalPages { get; private set; } = 1;

        public PaginationControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Height = 44;
            this.Dock = DockStyle.Bottom;
            this.BackColor = Color.FromArgb(248, 250, 252); // Modern Slate-50
            this.Font = new Font("Cairo", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
            this.Padding = new Padding(12, 4, 12, 4);

            // 1. Right Side Controls
            lblPageSizeTitle = new Label
            {
                Text = "عرض الصفوف:",
                AutoSize = true,
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Cairo", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };

            comboPageSize = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 85,
                Height = 30,
                Font = new Font("Cairo", 9F, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42)
            };
            comboPageSize.Items.AddRange(new object[] { "10", "20", "30", "50", "100", "الكل" });
            comboPageSize.SelectedItem = "30";
            comboPageSize.SelectedIndexChanged += ComboPageSize_SelectedIndexChanged;

            lblSummary = new Label
            {
                Text = "إجمالي السجلات: 0",
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Cairo", 9F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleRight
            };

            // 2. Left Side Controls
            btnFirst = CreateNavButton("الأولى", BtnFirst_Click);
            btnPrev = CreateNavButton("السابق", BtnPrev_Click);

            comboPageNumber = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 65,
                Height = 30,
                Font = new Font("Cairo", 9F, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42)
            };
            comboPageNumber.SelectedIndexChanged += ComboPageNumber_SelectedIndexChanged;

            lblFromWord = new Label
            {
                Text = "من",
                AutoSize = true,
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Cairo", 9F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblTotalPages = new Label
            {
                Text = "1",
                AutoSize = true,
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Cairo", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnNext = CreateNavButton("التالي", BtnNext_Click);
            btnLast = CreateNavButton("الأخيرة", BtnLast_Click);

            this.Controls.Add(lblPageSizeTitle);
            this.Controls.Add(comboPageSize);
            this.Controls.Add(lblSummary);

            this.Controls.Add(btnFirst);
            this.Controls.Add(btnPrev);
            this.Controls.Add(comboPageNumber);
            this.Controls.Add(lblFromWord);
            this.Controls.Add(lblTotalPages);
            this.Controls.Add(btnNext);
            this.Controls.Add(btnLast);

            this.ResumeLayout(false);
        }

        private Button CreateNavButton(string text, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Width = 62,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Cairo", 8.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btn.Click += onClick;
            return btn;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            LayoutAllControls();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutAllControls();
        }

        private void LayoutAllControls()
        {
            if (comboPageSize == null || btnFirst == null) return;

            int h = this.ClientSize.Height;
            int w = this.ClientSize.Width;

            // 1. Right Side (أسفل يمين الصفحة):
            // الترتيب من اليمين لليسار: "عرض الصفوف:" ثم الكومبوبوكس ثم ملخص السجلات
            int rightEdge = w - 16;

            lblPageSizeTitle.Location = new Point(rightEdge - lblPageSizeTitle.Width, (h - lblPageSizeTitle.Height) / 2);
            rightEdge = lblPageSizeTitle.Left - 8;

            comboPageSize.Location = new Point(rightEdge - comboPageSize.Width, (h - comboPageSize.Height) / 2);
            rightEdge = comboPageSize.Left - 16;

            lblSummary.Location = new Point(rightEdge - lblSummary.Width, (h - lblSummary.Height) / 2);

            // 2. Left Side (يسار الصفحة):
            // الترتيب: الأولى ثم السابق ثم كومبوبوكس الصفحة ثم "من" ثم إجمالي الصفحات ثم التالي ثم الأخيرة
            int leftEdge = 16;

            btnFirst.Location = new Point(leftEdge, (h - btnFirst.Height) / 2);
            leftEdge += btnFirst.Width + 6;

            btnPrev.Location = new Point(leftEdge, (h - btnPrev.Height) / 2);
            leftEdge += btnPrev.Width + 8;

            comboPageNumber.Location = new Point(leftEdge, (h - comboPageNumber.Height) / 2);
            leftEdge += comboPageNumber.Width + 6;

            lblFromWord.Location = new Point(leftEdge, (h - lblFromWord.Height) / 2);
            leftEdge += lblFromWord.Width + 6;

            lblTotalPages.Location = new Point(leftEdge, (h - lblTotalPages.Height) / 2);
            leftEdge += lblTotalPages.Width + 8;

            btnNext.Location = new Point(leftEdge, (h - btnNext.Height) / 2);
            leftEdge += btnNext.Width + 6;

            btnLast.Location = new Point(leftEdge, (h - btnLast.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw sleek subtle top separator line
            using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
            e.Graphics.DrawLine(pen, 0, 0, this.Width, 0);
        }

        private void ComboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI) return;

            string selected = comboPageSize.SelectedItem?.ToString() ?? "30";
            if (selected == "الكل")
            {
                IsAll = true;
                PageSize = int.MaxValue;
            }
            else
            {
                IsAll = false;
                if (int.TryParse(selected, out int sz))
                {
                    PageSize = sz;
                }
                else
                {
                    PageSize = 30;
                }
            }

            CurrentPage = 1;
            CalculatePages();
            RaisePageChanged();
        }

        private void ComboPageNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI) return;

            if (int.TryParse(comboPageNumber.SelectedItem?.ToString(), out int p))
            {
                if (p != CurrentPage && p >= 1 && p <= TotalPages)
                {
                    CurrentPage = p;
                    UpdateNavButtonsState();
                    UpdateSummaryText();
                    RaisePageChanged();
                }
            }
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage = 1;
                SyncPageCombo();
                UpdateNavButtonsState();
                UpdateSummaryText();
                RaisePageChanged();
            }
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                SyncPageCombo();
                UpdateNavButtonsState();
                UpdateSummaryText();
                RaisePageChanged();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                SyncPageCombo();
                UpdateNavButtonsState();
                UpdateSummaryText();
                RaisePageChanged();
            }
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage = TotalPages;
                SyncPageCombo();
                UpdateNavButtonsState();
                UpdateSummaryText();
                RaisePageChanged();
            }
        }

        private void RaisePageChanged()
        {
            PageChanged?.Invoke(this, new PageChangedEventArgs
            {
                PageNumber = CurrentPage,
                PageSize = PageSize,
                IsAll = IsAll
            });
        }

        /// <summary>
        /// تحديث إجمالي السجلات وإعادة احتساب عدد الصفحات وضبط عناصر التحكم
        /// </summary>
        public void SetTotalRecords(int totalRecords, int? pageNumber = null)
        {
            TotalRecords = Math.Max(0, totalRecords);
            if (pageNumber.HasValue)
            {
                CurrentPage = pageNumber.Value;
            }
            CalculatePages();
        }

        private void CalculatePages()
        {
            isUpdatingUI = true;
            try
            {
                if (IsAll || PageSize >= TotalRecords || PageSize <= 0)
                {
                    TotalPages = 1;
                    CurrentPage = 1;
                }
                else
                {
                    TotalPages = (int)Math.Ceiling((double)TotalRecords / PageSize);
                    if (TotalPages < 1) TotalPages = 1;
                    if (CurrentPage > TotalPages) CurrentPage = TotalPages;
                    if (CurrentPage < 1) CurrentPage = 1;
                }

                // Populate comboPageNumber
                comboPageNumber.Items.Clear();
                for (int i = 1; i <= TotalPages; i++)
                {
                    comboPageNumber.Items.Add(i.ToString());
                }

                comboPageNumber.SelectedItem = CurrentPage.ToString();
                lblTotalPages.Text = $"{TotalPages}";

                UpdateNavButtonsState();
                UpdateSummaryText();
                LayoutAllControls();
            }
            finally
            {
                isUpdatingUI = false;
            }
        }

        private void SyncPageCombo()
        {
            isUpdatingUI = true;
            try
            {
                comboPageNumber.SelectedItem = CurrentPage.ToString();
            }
            finally
            {
                isUpdatingUI = false;
            }
        }

        private void UpdateNavButtonsState()
        {
            bool canGoBack = CurrentPage > 1 && !IsAll;
            bool canGoForward = CurrentPage < TotalPages && !IsAll;

            btnFirst.Enabled = canGoBack;
            btnPrev.Enabled = canGoBack;
            btnNext.Enabled = canGoForward;
            btnLast.Enabled = canGoForward;

            comboPageNumber.Enabled = !IsAll && TotalPages > 1;

            ApplyBtnVisualState(btnFirst, canGoBack);
            ApplyBtnVisualState(btnPrev, canGoBack);
            ApplyBtnVisualState(btnNext, canGoForward);
            ApplyBtnVisualState(btnLast, canGoForward);
        }

        private void ApplyBtnVisualState(Button btn, bool enabled)
        {
            if (enabled)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.FromArgb(15, 23, 42);
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            }
            else
            {
                btn.BackColor = Color.FromArgb(241, 245, 249);
                btn.ForeColor = Color.FromArgb(148, 163, 184);
                btn.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            }
        }

        private void UpdateSummaryText()
        {
            if (TotalRecords == 0)
            {
                lblSummary.Text = "لا توجد سجلات";
                LayoutAllControls();
                return;
            }

            if (IsAll)
            {
                lblSummary.Text = $"إجمالي السجلات: {TotalRecords} (عرض الكل)";
                LayoutAllControls();
                return;
            }

            int fromRecord = ((CurrentPage - 1) * PageSize) + 1;
            int toRecord = Math.Min(CurrentPage * PageSize, TotalRecords);
            lblSummary.Text = $"عرض ({fromRecord} - {toRecord}) من أصل {TotalRecords} سجل";
            LayoutAllControls();
        }

        /// <summary>
        /// تقطيع البيانات وتسليم الصفحة المطلوبة مباشرة وإدارة السجلات تلقائياً
        /// </summary>
        public List<T> GetPageData<T>(IEnumerable<T> source, bool resetToFirstPage = false)
        {
            if (source == null)
            {
                SetTotalRecords(0);
                return new List<T>();
            }

            var list = source as IList<T> ?? source.ToList();

            if (resetToFirstPage)
            {
                CurrentPage = 1;
            }

            SetTotalRecords(list.Count);

            if (IsAll || PageSize >= list.Count)
            {
                return list.ToList();
            }

            int skip = Math.Max(0, (CurrentPage - 1) * PageSize);
            return list.Skip(skip).Take(PageSize).ToList();
        }
    }
}
