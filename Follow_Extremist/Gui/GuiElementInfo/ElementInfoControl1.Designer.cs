
namespace Follow_Extremist.Gui.GuiElementInfo
{
    partial class ElementInfoControl1
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
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonAdd = new System.Windows.Forms.Button();
            buttonEdit = new System.Windows.Forms.Button();
            buttonEditFollowDate = new System.Windows.Forms.Button();
            buttonDelete = new System.Windows.Forms.Button();
            buttonLoad = new System.Windows.Forms.Button();
            buttonPrint = new System.Windows.Forms.Button();
            buttonExport = new System.Windows.Forms.Button();
            buttonElementDetail = new System.Windows.Forms.Button();
            buttonHistoryFollow = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            textBoxSearch = new System.Windows.Forms.TextBox();
            buttonSearch = new System.Windows.Forms.Button();
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            comboBoxPageNo = new System.Windows.Forms.ComboBox();
            colorDialog1 = new System.Windows.Forms.ColorDialog();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemMemoEdit1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            flowLayoutPanel1.Controls.Add(buttonAdd);
            flowLayoutPanel1.Controls.Add(buttonEdit);
            flowLayoutPanel1.Controls.Add(buttonEditFollowDate);
            flowLayoutPanel1.Controls.Add(buttonDelete);
            flowLayoutPanel1.Controls.Add(buttonLoad);
            flowLayoutPanel1.Controls.Add(buttonPrint);
            flowLayoutPanel1.Controls.Add(buttonExport);
            flowLayoutPanel1.Controls.Add(buttonElementDetail);
            flowLayoutPanel1.Controls.Add(buttonHistoryFollow);
            flowLayoutPanel1.Controls.Add(panel1);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            flowLayoutPanel1.Size = new System.Drawing.Size(1682, 72);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // buttonAdd
            // 
            buttonAdd.Image = Properties.Resources.Add;
            buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonAdd.Location = new System.Drawing.Point(1546, 10);
            buttonAdd.Margin = new System.Windows.Forms.Padding(5);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(121, 55);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "اضافة";
            buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.Edit;
            buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonEdit.Location = new System.Drawing.Point(1415, 10);
            buttonEdit.Margin = new System.Windows.Forms.Padding(5);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new System.Drawing.Size(121, 55);
            buttonEdit.TabIndex = 2;
            buttonEdit.Text = "تعديل";
            buttonEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonEditFollowDate
            // 
            buttonEditFollowDate.Image = Properties.Resources.Edit;
            buttonEditFollowDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonEditFollowDate.Location = new System.Drawing.Point(1214, 10);
            buttonEditFollowDate.Margin = new System.Windows.Forms.Padding(5);
            buttonEditFollowDate.Name = "buttonEditFollowDate";
            buttonEditFollowDate.Size = new System.Drawing.Size(191, 55);
            buttonEditFollowDate.TabIndex = 8;
            buttonEditFollowDate.Text = "تعديل ميعاد المتابعة";
            buttonEditFollowDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonEditFollowDate.UseVisualStyleBackColor = true;
            buttonEditFollowDate.Click += buttonEditFollowDate_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.Waste;
            buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonDelete.Location = new System.Drawing.Point(1083, 10);
            buttonDelete.Margin = new System.Windows.Forms.Padding(5);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(121, 55);
            buttonDelete.TabIndex = 3;
            buttonDelete.Text = "حذف ";
            buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Image = Properties.Resources.Loader;
            buttonLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonLoad.Location = new System.Drawing.Point(952, 10);
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
            buttonPrint.Location = new System.Drawing.Point(821, 10);
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
            buttonExport.Location = new System.Drawing.Point(690, 10);
            buttonExport.Margin = new System.Windows.Forms.Padding(5);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(121, 55);
            buttonExport.TabIndex = 6;
            buttonExport.Text = "تصدير";
            buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // buttonElementDetail
            // 
            buttonElementDetail.Image = Properties.Resources.More_Details;
            buttonElementDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonElementDetail.Location = new System.Drawing.Point(524, 10);
            buttonElementDetail.Margin = new System.Windows.Forms.Padding(5);
            buttonElementDetail.Name = "buttonElementDetail";
            buttonElementDetail.Size = new System.Drawing.Size(156, 53);
            buttonElementDetail.TabIndex = 11;
            buttonElementDetail.Text = "تفاصيل العنصر";
            buttonElementDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonElementDetail.UseVisualStyleBackColor = true;
            buttonElementDetail.Click += buttonElementDetail_Click;
            // 
            // buttonHistoryFollow
            // 
            buttonHistoryFollow.Image = Properties.Resources.More_Details;
            buttonHistoryFollow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonHistoryFollow.Location = new System.Drawing.Point(358, 10);
            buttonHistoryFollow.Margin = new System.Windows.Forms.Padding(5);
            buttonHistoryFollow.Name = "buttonHistoryFollow";
            buttonHistoryFollow.Size = new System.Drawing.Size(156, 53);
            buttonHistoryFollow.TabIndex = 12;
            buttonHistoryFollow.Text = "سجل المتابعة";
            buttonHistoryFollow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonHistoryFollow.UseVisualStyleBackColor = true;
            buttonHistoryFollow.Click += buttonHistoryFollow_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.Add(textBoxSearch);
            panel1.Controls.Add(buttonSearch);
            panel1.Location = new System.Drawing.Point(8, 8);
            panel1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(342, 55);
            panel1.TabIndex = 5;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            textBoxSearch.Location = new System.Drawing.Point(89, 0);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new System.Drawing.Size(251, 41);
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
            gridControl1.Location = new System.Drawing.Point(0, 72);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemTextEdit1, repositoryItemMemoEdit1 });
            gridControl1.Size = new System.Drawing.Size(1682, 648);
            gridControl1.TabIndex = 3;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            gridControl1.DoubleClick += gridControl1_DoubleClick;
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
            gridView1.Appearance.EvenRow.Options.UseTextOptions = true;
            gridView1.Appearance.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FilterCloseButton.Options.UseFont = true;
            gridView1.Appearance.FilterCloseButton.Options.UseTextOptions = true;
            gridView1.Appearance.FilterCloseButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FilterPanel.Options.UseFont = true;
            gridView1.Appearance.FilterPanel.Options.UseTextOptions = true;
            gridView1.Appearance.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FixedLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FixedLine.Options.UseFont = true;
            gridView1.Appearance.FixedLine.Options.UseTextOptions = true;
            gridView1.Appearance.FixedLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FixedLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FixedLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FocusedCell.Options.UseTextOptions = true;
            gridView1.Appearance.FocusedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FocusedRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.FocusedRow.Options.UseFont = true;
            gridView1.Appearance.FocusedRow.Options.UseTextOptions = true;
            gridView1.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.FocusedRow.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.FocusedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.FooterPanel.Options.UseTextOptions = true;
            gridView1.Appearance.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupButton.Options.UseTextOptions = true;
            gridView1.Appearance.GroupButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupFooter.Options.UseTextOptions = true;
            gridView1.Appearance.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupPanel.Options.UseTextOptions = true;
            gridView1.Appearance.GroupPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.GroupRow.Options.UseTextOptions = true;
            gridView1.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HideSelectionRow.Options.UseTextOptions = true;
            gridView1.Appearance.HideSelectionRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.HotTrackedRow.Options.UseTextOptions = true;
            gridView1.Appearance.HotTrackedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.OddRow.Options.UseTextOptions = true;
            gridView1.Appearance.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.Preview.Options.UseTextOptions = true;
            gridView1.Appearance.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.Row.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.Appearance.Row.Options.UseTextOptions = true;
            gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            gridView1.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.SelectedRow.Options.UseTextOptions = true;
            gridView1.Appearance.SelectedRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.TopNewRow.Options.UseTextOptions = true;
            gridView1.Appearance.TopNewRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.Appearance.ViewCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            gridView1.Appearance.ViewCaption.Options.UseFont = true;
            gridView1.Appearance.ViewCaption.Options.UseTextOptions = true;
            gridView1.Appearance.ViewCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.EvenRow.Options.UseTextOptions = true;
            gridView1.AppearancePrint.EvenRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.FilterPanel.Options.UseTextOptions = true;
            gridView1.AppearancePrint.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.FooterPanel.Options.UseTextOptions = true;
            gridView1.AppearancePrint.FooterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.GroupFooter.Options.UseTextOptions = true;
            gridView1.AppearancePrint.GroupFooter.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.GroupRow.Options.UseTextOptions = true;
            gridView1.AppearancePrint.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            gridView1.AppearancePrint.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.Lines.Options.UseTextOptions = true;
            gridView1.AppearancePrint.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.OddRow.Options.UseTextOptions = true;
            gridView1.AppearancePrint.OddRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.Preview.Options.UseTextOptions = true;
            gridView1.AppearancePrint.Preview.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.AppearancePrint.Row.Options.UseTextOptions = true;
            gridView1.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            gridView1.ColumnPanelRowHeight = 45;
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowColumnMoving = false;
            gridView1.OptionsDetail.EnableMasterViewMode = false;
            gridView1.OptionsDetail.ShowDetailTabs = false;
            gridView1.OptionsDetail.SmartDetailExpand = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            gridView1.RowStyle += gridView1_RowStyle;
            // 
            // repositoryItemTextEdit1
            // 
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemMemoEdit1
            // 
            repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // comboBoxPageNo
            // 
            comboBoxPageNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            comboBoxPageNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxPageNo.FormattingEnabled = true;
            comboBoxPageNo.Location = new System.Drawing.Point(3, 679);
            comboBoxPageNo.Name = "comboBoxPageNo";
            comboBoxPageNo.Size = new System.Drawing.Size(121, 33);
            comboBoxPageNo.TabIndex = 5;
            comboBoxPageNo.Visible = false;
            comboBoxPageNo.SelectedIndexChanged += comboBoxPageNo_SelectedIndexChanged;
            // 
            // colorDialog1
            // 
            colorDialog1.AnyColor = true;
            // 
            // ElementInfoControl1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(comboBoxPageNo);
            Controls.Add(gridControl1);
            Controls.Add(flowLayoutPanel1);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            Name = "ElementInfoControl1";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            Size = new System.Drawing.Size(1682, 720);
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemTextEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemMemoEdit1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.ComboBox comboBoxPageNo;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonEditFollowDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button buttonElementDetail;
        private System.Windows.Forms.Button buttonHistoryFollow;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}
