
namespace Follow_Extremist.Gui.GuiBreakFollow
{
    partial class ElementsBreakForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonLoad = new System.Windows.Forms.Button();
            buttonPrint = new System.Windows.Forms.Button();
            buttonExport = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            textBoxSearch = new System.Windows.Forms.TextBox();
            buttonSearch = new System.Windows.Forms.Button();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            flowLayoutPanel1.Controls.Add(buttonLoad);
            flowLayoutPanel1.Controls.Add(buttonPrint);
            flowLayoutPanel1.Controls.Add(buttonExport);
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            flowLayoutPanel1.Size = new System.Drawing.Size(1555, 74);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // buttonLoad
            // 
            buttonLoad.Image = Properties.Resources.Loader;
            buttonLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonLoad.Location = new System.Drawing.Point(1419, 10);
            buttonLoad.Margin = new System.Windows.Forms.Padding(5);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new System.Drawing.Size(121, 55);
            buttonLoad.TabIndex = 5;
            buttonLoad.Text = "تحديث ";
            buttonLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonPrint
            // 
            buttonPrint.Image = Properties.Resources.Print;
            buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonPrint.Location = new System.Drawing.Point(1288, 10);
            buttonPrint.Margin = new System.Windows.Forms.Padding(5);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new System.Drawing.Size(121, 55);
            buttonPrint.TabIndex = 7;
            buttonPrint.Text = "طباعة ";
            buttonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonPrint.UseVisualStyleBackColor = true;
            buttonPrint.Click += buttonPrint_Click;
            // 
            // buttonExport
            // 
            buttonExport.BackgroundImage = Properties.Resources.Microsoft_Excel;
            buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonExport.Location = new System.Drawing.Point(1157, 10);
            buttonExport.Margin = new System.Windows.Forms.Padding(5);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(121, 55);
            buttonExport.TabIndex = 6;
            buttonExport.Text = "تصدير";
            buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(textBoxSearch);
            panel1.Controls.Add(buttonSearch);
            panel1.Location = new System.Drawing.Point(731, 8);
            panel1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(418, 55);
            panel1.TabIndex = 5;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            textBoxSearch.Location = new System.Drawing.Point(89, 0);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new System.Drawing.Size(327, 41);
            textBoxSearch.TabIndex = 6;
            textBoxSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // buttonSearch
            // 
            buttonSearch.BackColor = System.Drawing.Color.SteelBlue;
            buttonSearch.Dock = System.Windows.Forms.DockStyle.Left;
            buttonSearch.FlatAppearance.BorderSize = 0;
            buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            buttonSearch.ForeColor = System.Drawing.Color.White;
            buttonSearch.Image = Properties.Resources.Search;
            buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonSearch.Location = new System.Drawing.Point(0, 0);
            buttonSearch.Margin = new System.Windows.Forms.Padding(5);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new System.Drawing.Size(89, 53);
            buttonSearch.TabIndex = 5;
            buttonSearch.Text = "بحث";
            buttonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonSearch.UseVisualStyleBackColor = false;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl1.Location = new System.Drawing.Point(0, 74);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(1555, 607);
            gridControl1.TabIndex = 3;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.ColumnFilterButton.Options.UseFont = true;
            gridView1.Appearance.ColumnFilterButton.Options.UseTextOptions = true;
            gridView1.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.ColumnFilterButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.ColumnFilterButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            gridView1.Appearance.ColumnFilterButtonActive.Options.UseTextOptions = true;
            gridView1.Appearance.ColumnFilterButtonActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.ColumnFilterButtonActive.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.ColumnFilterButtonActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.CustomizationFormHint.Options.UseFont = true;
            gridView1.Appearance.CustomizationFormHint.Options.UseTextOptions = true;
            gridView1.Appearance.CustomizationFormHint.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.CustomizationFormHint.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.CustomizationFormHint.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.DetailTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.DetailTip.Options.UseFont = true;
            gridView1.Appearance.DetailTip.Options.UseTextOptions = true;
            gridView1.Appearance.DetailTip.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.DetailTip.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.DetailTip.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.Empty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.Empty.Options.UseFont = true;
            gridView1.Appearance.Empty.Options.UseTextOptions = true;
            gridView1.Appearance.Empty.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.Empty.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.EvenRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.EvenRow.Options.UseFont = true;
            gridView1.Appearance.EvenRow.Options.UseTextOptions = true;
            gridView1.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FilterCloseButton.Options.UseFont = true;
            gridView1.Appearance.FilterCloseButton.Options.UseTextOptions = true;
            gridView1.Appearance.FilterCloseButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FilterCloseButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FilterCloseButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FilterPanel.Options.UseFont = true;
            gridView1.Appearance.FilterPanel.Options.UseTextOptions = true;
            gridView1.Appearance.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FixedLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FixedLine.Options.UseFont = true;
            gridView1.Appearance.FixedLine.Options.UseTextOptions = true;
            gridView1.Appearance.FixedLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FixedLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FixedLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FocusedCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FocusedCell.Options.UseFont = true;
            gridView1.Appearance.FocusedCell.Options.UseTextOptions = true;
            gridView1.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FocusedCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FocusedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FocusedRow.Options.UseFont = true;
            gridView1.Appearance.FocusedRow.Options.UseTextOptions = true;
            gridView1.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FocusedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FooterPanel.Options.UseFont = true;
            gridView1.Appearance.FooterPanel.Options.UseTextOptions = true;
            gridView1.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupButton.Options.UseFont = true;
            gridView1.Appearance.GroupButton.Options.UseTextOptions = true;
            gridView1.Appearance.GroupButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.GroupButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.GroupButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupFooter.Options.UseFont = true;
            gridView1.Appearance.GroupFooter.Options.UseTextOptions = true;
            gridView1.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupPanel.Options.UseFont = true;
            gridView1.Appearance.GroupPanel.Options.UseTextOptions = true;
            gridView1.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.GroupPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupRow.Options.UseFont = true;
            gridView1.Appearance.GroupRow.Options.UseTextOptions = true;
            gridView1.Appearance.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.HideSelectionRow.Options.UseFont = true;
            gridView1.Appearance.HideSelectionRow.Options.UseTextOptions = true;
            gridView1.Appearance.HideSelectionRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HideSelectionRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.HideSelectionRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HorzLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.HorzLine.Options.UseFont = true;
            gridView1.Appearance.HorzLine.Options.UseTextOptions = true;
            gridView1.Appearance.HorzLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HorzLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.HorzLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HotTrackedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.HotTrackedRow.Options.UseFont = true;
            gridView1.Appearance.HotTrackedRow.Options.UseTextOptions = true;
            gridView1.Appearance.HotTrackedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HotTrackedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.HotTrackedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.OddRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.OddRow.Options.UseFont = true;
            gridView1.Appearance.OddRow.Options.UseTextOptions = true;
            gridView1.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.Preview.Options.UseFont = true;
            gridView1.Appearance.Preview.Options.UseTextOptions = true;
            gridView1.Appearance.Preview.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.Appearance.Row.Options.UseTextOptions = true;
            gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.RowSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.RowSeparator.Options.UseFont = true;
            gridView1.Appearance.RowSeparator.Options.UseTextOptions = true;
            gridView1.Appearance.RowSeparator.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.RowSeparator.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.RowSeparator.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.SelectedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.SelectedRow.Options.UseFont = true;
            gridView1.Appearance.SelectedRow.Options.UseTextOptions = true;
            gridView1.Appearance.SelectedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.SelectedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.SelectedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.TopNewRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.TopNewRow.Options.UseFont = true;
            gridView1.Appearance.TopNewRow.Options.UseTextOptions = true;
            gridView1.Appearance.TopNewRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.TopNewRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.TopNewRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.VertLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.VertLine.Options.UseFont = true;
            gridView1.Appearance.VertLine.Options.UseTextOptions = true;
            gridView1.Appearance.VertLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.VertLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.VertLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.ViewCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.ViewCaption.Options.UseFont = true;
            gridView1.Appearance.ViewCaption.Options.UseTextOptions = true;
            gridView1.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.EvenRow.Options.UseTextOptions = true;
            gridView1.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.EvenRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.FilterPanel.Options.UseTextOptions = true;
            gridView1.AppearancePrint.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.FooterPanel.Options.UseTextOptions = true;
            gridView1.AppearancePrint.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.FooterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.GroupFooter.Options.UseTextOptions = true;
            gridView1.AppearancePrint.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.GroupFooter.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.GroupRow.Options.UseTextOptions = true;
            gridView1.AppearancePrint.GroupRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.GroupRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            gridView1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.Lines.Options.UseTextOptions = true;
            gridView1.AppearancePrint.Lines.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.Lines.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.OddRow.Options.UseTextOptions = true;
            gridView1.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.OddRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.Preview.Options.UseTextOptions = true;
            gridView1.AppearancePrint.Preview.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.Preview.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.Row.Options.UseTextOptions = true;
            gridView1.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.AppearancePrint.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.GridControl = gridControl1;
            gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Nothing;
            gridView1.OptionsPrint.AllowMultilineHeaders = true;
            gridView1.OptionsPrint.EnableAppearanceEvenRow = true;
            gridView1.OptionsPrint.EnableAppearanceOddRow = true;
            gridView1.OptionsPrint.ExpandAllDetails = true;
            gridView1.OptionsPrint.PrintDetails = true;
            gridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            gridView1.CustomRowCellEdit += gridView1_CustomRowCellEdit;
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            gridView1.PrintInitialize += gridView1_PrintInitialize;
            // 
            // ElementsBreakForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1555, 681);
            Controls.Add(gridControl1);
            Controls.Add(flowLayoutPanel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            MaximizeBox = true;
            MinimizeBox = true;
            Name = "ElementsBreakForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "كسر المتابعة ";
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}