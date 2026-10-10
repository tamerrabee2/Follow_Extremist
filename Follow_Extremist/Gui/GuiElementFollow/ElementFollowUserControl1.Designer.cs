
namespace Follow_Extremist.Gui.GuiElementFollow
{
    partial class ElementFollowUserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonAdd = new System.Windows.Forms.Button();
            buttonExport = new System.Windows.Forms.Button();
            buttonPrint = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            buttonSearch = new System.Windows.Forms.Button();
            buttonLoad = new System.Windows.Forms.Button();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            elementInfoBindingSource = new System.Windows.Forms.BindingSource(components);
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            comboBoxPageNo = new System.Windows.Forms.ComboBox();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)elementInfoBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            flowLayoutPanel1.Controls.Add(buttonAdd);
            flowLayoutPanel1.Controls.Add(buttonExport);
            flowLayoutPanel1.Controls.Add(buttonPrint);
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Controls.Add(buttonLoad);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            flowLayoutPanel1.Size = new System.Drawing.Size(1280, 71);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Image = Properties.Resources.Add;
            buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAdd.Location = new System.Drawing.Point(1144, 10);
            buttonAdd.Margin = new System.Windows.Forms.Padding(5);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(121, 55);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "اضافة";
            buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonExport
            // 
            buttonExport.BackgroundImage = Properties.Resources.Microsoft_Excel;
            buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonExport.Location = new System.Drawing.Point(1013, 10);
            buttonExport.Margin = new System.Windows.Forms.Padding(5);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(121, 55);
            buttonExport.TabIndex = 6;
            buttonExport.Text = "تصدير";
            buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // buttonPrint
            // 
            buttonPrint.Image = Properties.Resources.Print;
            buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonPrint.Location = new System.Drawing.Point(882, 10);
            buttonPrint.Margin = new System.Windows.Forms.Padding(5);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new System.Drawing.Size(121, 55);
            buttonPrint.TabIndex = 3;
            buttonPrint.Text = "طباعة ";
            buttonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonPrint.UseVisualStyleBackColor = true;
            buttonPrint.Click += buttonPrint_Click;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(buttonSearch);
            panel1.Location = new System.Drawing.Point(567, 18);
            panel1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(307, 38);
            panel1.TabIndex = 5;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            dateTimePicker1.Location = new System.Drawing.Point(89, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size(216, 30);
            dateTimePicker1.TabIndex = 6;
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
            buttonSearch.Size = new System.Drawing.Size(89, 36);
            buttonSearch.TabIndex = 5;
            buttonSearch.Text = "بحث";
            buttonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonSearch.UseVisualStyleBackColor = false;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Image = Properties.Resources.Loader;
            buttonLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonLoad.Location = new System.Drawing.Point(436, 10);
            buttonLoad.Margin = new System.Windows.Forms.Padding(5);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new System.Drawing.Size(121, 55);
            buttonLoad.TabIndex = 5;
            buttonLoad.Text = "تحديث ";
            buttonLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Visible = false;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // gridControl1
            // 
            gridControl1.DataSource = elementInfoBindingSource;
            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl1.Location = new System.Drawing.Point(0, 71);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(1280, 649);
            gridControl1.TabIndex = 2;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // elementInfoBindingSource
            // 
            elementInfoBindingSource.DataSource = typeof(Core.ElementInfo);
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
            gridView1.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.CustomizationFormHint.Options.UseFont = true;
            gridView1.Appearance.CustomizationFormHint.Options.UseTextOptions = true;
            gridView1.Appearance.CustomizationFormHint.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.CustomizationFormHint.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
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
            gridView1.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FilterCloseButton.Options.UseFont = true;
            gridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FilterPanel.Options.UseFont = true;
            gridView1.Appearance.FixedLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FixedLine.Options.UseFont = true;
            gridView1.Appearance.FocusedCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FocusedCell.Options.UseFont = true;
            gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FocusedRow.Options.UseFont = true;
            gridView1.Appearance.FocusedRow.Options.UseTextOptions = true;
            gridView1.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FocusedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FooterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FooterPanel.Options.UseFont = true;
            gridView1.Appearance.GroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupButton.Options.UseFont = true;
            gridView1.Appearance.GroupFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupFooter.Options.UseFont = true;
            gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.GroupPanel.Options.UseFont = true;
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
            gridView1.Appearance.HorzLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.HorzLine.Options.UseFont = true;
            gridView1.Appearance.HotTrackedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.HotTrackedRow.Options.UseFont = true;
            gridView1.Appearance.OddRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.OddRow.Options.UseFont = true;
            gridView1.Appearance.Preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.Preview.Options.UseFont = true;
            gridView1.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.Appearance.Row.Options.UseTextOptions = true;
            gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.RowSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.RowSeparator.Options.UseFont = true;
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
            gridView1.Appearance.ViewCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.ViewCaption.Options.UseFont = true;
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Nothing;
            gridView1.OptionsPrint.AllowMultilineHeaders = true;
            gridView1.OptionsPrint.EnableAppearanceEvenRow = true;
            gridView1.OptionsPrint.EnableAppearanceOddRow = true;
            gridView1.OptionsPrint.ExpandAllDetails = true;
            gridView1.OptionsPrint.PrintDetails = true;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // 
            // comboBoxPageNo
            // 
            comboBoxPageNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            comboBoxPageNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPageNo.FormattingEnabled = true;
            comboBoxPageNo.Location = new System.Drawing.Point(0, 679);
            comboBoxPageNo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            comboBoxPageNo.Name = "comboBoxPageNo";
            comboBoxPageNo.Size = new System.Drawing.Size(121, 33);
            comboBoxPageNo.TabIndex = 4;
            comboBoxPageNo.Visible = false;
            comboBoxPageNo.SelectedIndexChanged += comboBoxPageNo_SelectedIndexChanged;
            // 
            // ElementFollowUserControl1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(comboBoxPageNo);
            Controls.Add(gridControl1);
            Controls.Add(flowLayoutPanel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            Name = "ElementFollowUserControl1";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Size = new System.Drawing.Size(1280, 720);
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)elementInfoBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button buttonSearch;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.ComboBox comboBoxPageNo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.BindingSource elementInfoBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colNationalId;
        private DevExpress.XtraGrid.Columns.GridColumn colMotherName;
        private DevExpress.XtraGrid.Columns.GridColumn colQualification;
        private DevExpress.XtraGrid.Columns.GridColumn colJob;
        private DevExpress.XtraGrid.Columns.GridColumn colBirthPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colElementImage;
        private DevExpress.XtraGrid.Columns.GridColumn colNationalIdImage;
        private DevExpress.XtraGrid.Columns.GridColumn colFollowState;
        private DevExpress.XtraGrid.Columns.GridColumn colReasonEndFollow;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colMobile;
        private DevExpress.XtraGrid.Columns.GridColumn colMobile2;
        private DevExpress.XtraGrid.Columns.GridColumn colMobile3;
        private DevExpress.XtraGrid.Columns.GridColumn colDateFollowStart;
        private DevExpress.XtraGrid.Columns.GridColumn colFollowDaysCount;
        private DevExpress.XtraGrid.Columns.GridColumn colDateFollowNow;
        private DevExpress.XtraGrid.Columns.GridColumn colRegulatoryStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colFacebookAcount;
        private DevExpress.XtraGrid.Columns.GridColumn colFacebookID;
        private DevExpress.XtraGrid.Columns.GridColumn colPrisonedOrnot;
    }
}
