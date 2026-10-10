using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Follow_Extremist.Core;

namespace Follow_Extremist.Gui.GuiFingerprint
{
    public class SearchableElementDropDown : UserControl
    {
        private TextBox txtDisplay;
        private Button btnDropDown;
        private Button btnClear;
        private ToolStripDropDown popup;
        private ToolStripControlHost popupHost;
        private Panel popupPanel;
        private TextBox txtSearch;
        private ListBox lstItems;
        private Label lblCount;

        private List<ElementInfo> allElements = new();
        private List<ElementInfo> filteredElements = new();
        private ElementInfo selectedElement;

        public event EventHandler<ElementInfo> SelectedElementChanged;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ElementInfo SelectedElement
        {
            get => selectedElement;
            set
            {
                selectedElement = value;
                UpdateDisplayText();
                SelectedElementChanged?.Invoke(this, selectedElement);
            }
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int? SelectedId => selectedElement?.Id;

        public SearchableElementDropDown()
        {
            InitializeComponent();
            SetupPopup();
        }

        private void InitializeComponent()
        {
            this.txtDisplay = new TextBox();
            this.btnDropDown = new Button();
            this.btnClear = new Button();
            this.SuspendLayout();

            // Root UserControl
            this.RightToLeft = RightToLeft.Yes;
            this.Size = new Size(380, 36);
            this.BackColor = Color.White;
            this.Padding = new Padding(1);
            this.BorderStyle = BorderStyle.FixedSingle;

            // Clear Button (Left in RTL)
            this.btnClear.Dock = DockStyle.Left;
            this.btnClear.Width = 28;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Text = "✕";
            this.btnClear.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnClear.ForeColor = Color.FromArgb(160, 160, 160);
            this.btnClear.Cursor = Cursors.Hand;
            this.btnClear.Click += (s, e) => { SelectedElement = null; };

            // DropDown Button (Right in RTL)
            this.btnDropDown.Dock = DockStyle.Right;
            this.btnDropDown.Width = 32;
            this.btnDropDown.FlatStyle = FlatStyle.Flat;
            this.btnDropDown.FlatAppearance.BorderSize = 0;
            this.btnDropDown.BackColor = Color.FromArgb(240, 244, 248);
            this.btnDropDown.Text = "▼";
            this.btnDropDown.Font = new Font("Arial", 8F);
            this.btnDropDown.ForeColor = Color.FromArgb(70, 80, 95);
            this.btnDropDown.Cursor = Cursors.Hand;
            this.btnDropDown.Click += (s, e) => TogglePopup();

            // Text Display (Middle)
            this.txtDisplay.Dock = DockStyle.Fill;
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.BackColor = Color.White;
            this.txtDisplay.BorderStyle = BorderStyle.None;
            this.txtDisplay.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            this.txtDisplay.ForeColor = Color.FromArgb(30, 41, 59);
            this.txtDisplay.Cursor = Cursors.Hand;
            this.txtDisplay.Click += (s, e) => TogglePopup();

            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDropDown);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetupPopup()
        {
            popupPanel = new Panel
            {
                Size = new Size(420, 260),
                RightToLeft = RightToLeft.Yes,
                BackColor = Color.White,
                Padding = new Padding(6),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Search Header Box
            var searchContainer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(0, 0, 0, 5)
            };

            txtSearch = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "🔍 اكتب اسم العنصر، الرقم القومي، أو رقم القيد للبحث..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.KeyDown += TxtSearch_KeyDown;

            searchContainer.Controls.Add(txtSearch);

            // List of items
            lstItems = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 28,
                BorderStyle = BorderStyle.FixedSingle,
                IntegralHeight = false
            };
            lstItems.DrawItem += LstItems_DrawItem;
            lstItems.Click += (s, e) => SelectCurrentItem();
            lstItems.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) SelectCurrentItem();
                else if (e.KeyCode == Keys.Escape) popup?.Close();
            };

            // Count label footer
            lblCount = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 22,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "0 عنصر"
            };

            popupPanel.Controls.Add(lstItems);
            popupPanel.Controls.Add(lblCount);
            popupPanel.Controls.Add(searchContainer);

            popupHost = new ToolStripControlHost(popupPanel)
            {
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                AutoSize = false
            };

            popup = new ToolStripDropDown
            {
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                AutoClose = true,
                DropShadowEnabled = true
            };
            popup.Items.Add(popupHost);
        }

        public void SetElements(IEnumerable<ElementInfo> elements)
        {
            allElements = elements?.ToList() ?? new List<ElementInfo>();
            FilterElements(string.Empty);
        }

        private void FilterElements(string query)
        {
            query = query?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(query))
            {
                filteredElements = allElements.Take(200).ToList();
            }
            else
            {
                // Normalize Arabic
                string normQ = NormalizeArabic(query);

                filteredElements = allElements.Where(x =>
                    x.Id.ToString() == query ||
                    (!string.IsNullOrEmpty(x.NationalId) && x.NationalId.Contains(query)) ||
                    (!string.IsNullOrEmpty(x.ElementName) && NormalizeArabic(x.ElementName).Contains(normQ))
                ).Take(150).ToList();
            }

            lstItems.Items.Clear();
            foreach (var el in filteredElements)
            {
                lstItems.Items.Add(el);
            }

            lblCount.Text = $"عدد النتائج المعروضة: {filteredElements.Count} من أصل {allElements.Count}";
            if (filteredElements.Count > 0)
            {
                lstItems.SelectedIndex = 0;
            }
        }

        private string NormalizeArabic(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return text.Replace("أ", "ا")
                       .Replace("إ", "ا")
                       .Replace("آ", "ا")
                       .Replace("ة", "ه")
                       .Replace("ى", "ي")
                       .Replace("ـ", "");
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterElements(txtSearch.Text);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstItems.Items.Count > 0)
                {
                    lstItems.Focus();
                    if (lstItems.SelectedIndex < lstItems.Items.Count - 1)
                        lstItems.SelectedIndex++;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SelectCurrentItem();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                popup?.Close();
                e.Handled = true;
            }
        }

        private void LstItems_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= filteredElements.Count) return;

            var item = filteredElements[e.Index];
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color bg = isSelected ? Color.FromArgb(230, 240, 255) : Color.White;
            Color fg = isSelected ? Color.FromArgb(20, 70, 160) : Color.FromArgb(30, 41, 59);

            using var brushBg = new SolidBrush(bg);
            e.Graphics.FillRectangle(brushBg, e.Bounds);

            // Draw ID Badge
            string idBadge = $"[{item.Id}]";
            using var fontBold = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            using var fontRegular = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            using var fontSmall = new Font("Segoe UI", 8F, FontStyle.Regular);
            using var brushFg = new SolidBrush(fg);
            using var brushMuted = new SolidBrush(Color.Gray);

            int x = e.Bounds.Right - 8;
            int y = e.Bounds.Y + 4;

            var sfRtl = new StringFormat { Alignment = StringAlignment.Far, FormatFlags = StringFormatFlags.DirectionRightToLeft };

            string displayText = $"{item.ElementName}   (قيد: #{item.Id})";
            if (!string.IsNullOrEmpty(item.NationalId))
            {
                displayText += $" - ق/ {item.NationalId}";
            }

            e.Graphics.DrawString(displayText, isSelected ? fontBold : fontRegular, brushFg, new RectangleF(e.Bounds.X + 5, y, e.Bounds.Width - 10, e.Bounds.Height), sfRtl);

            // Divider line
            using var penLine = new Pen(Color.FromArgb(240, 243, 246));
            e.Graphics.DrawLine(penLine, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
        }

        private void SelectCurrentItem()
        {
            if (lstItems.SelectedItem is ElementInfo el)
            {
                SelectedElement = el;
                popup?.Close();
            }
        }

        private void TogglePopup()
        {
            if (popup.Visible)
            {
                popup.Close();
                return;
            }

            popupPanel.Width = Math.Max(this.Width, 420);
            popupHost.Size = popupPanel.Size;
            txtSearch.Text = string.Empty;
            FilterElements(string.Empty);

            popup.Show(this, new Point(0, this.Height + 2), ToolStripDropDownDirection.Default);
            txtSearch.Focus();
        }

        private void UpdateDisplayText()
        {
            if (selectedElement != null)
            {
                txtDisplay.Text = $"[#{selectedElement.Id}]  {selectedElement.ElementName}  (قومي: {selectedElement.NationalId ?? "-"})";
                txtDisplay.ForeColor = Color.FromArgb(15, 23, 42);
            }
            else
            {
                txtDisplay.Text = "--- اضغط هنا للبحث واختيار العنصر ---";
                txtDisplay.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }
    }
}
