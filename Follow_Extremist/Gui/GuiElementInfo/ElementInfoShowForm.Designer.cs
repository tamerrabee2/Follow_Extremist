
namespace Follow_Extremist.Gui.GuiElementInfo
{
    partial class ElementInfoShowForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.buttonLoad);
            this.flowLayoutPanel1.Controls.Add(this.buttonPrint);
            this.flowLayoutPanel1.Controls.Add(this.buttonExport);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1343, 74);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Image = global::Follow_Extremist.Properties.Resources.Loader;
            this.buttonLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoad.Location = new System.Drawing.Point(1207, 10);
            this.buttonLoad.Margin = new System.Windows.Forms.Padding(5);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(121, 55);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "تحديث ";
            this.buttonLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Image = global::Follow_Extremist.Properties.Resources.Print;
            this.buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPrint.Location = new System.Drawing.Point(1076, 10);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(5);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(121, 55);
            this.buttonPrint.TabIndex = 7;
            this.buttonPrint.Text = "طباعة ";
            this.buttonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.BackgroundImage = global::Follow_Extremist.Properties.Resources.Microsoft_Excel;
            this.buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExport.Location = new System.Drawing.Point(945, 10);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(5);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(121, 55);
            this.buttonExport.TabIndex = 6;
            this.buttonExport.Text = "تصدير";
            this.buttonExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExport.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxSearch);
            this.panel1.Controls.Add(this.buttonSearch);
            this.panel1.Location = new System.Drawing.Point(519, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 55);
            this.panel1.TabIndex = 5;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSearch.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxSearch.Location = new System.Drawing.Point(89, 0);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(327, 57);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Image = global::Follow_Extremist.Properties.Resources.Search;
            this.buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSearch.Location = new System.Drawing.Point(0, 0);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(89, 53);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "بحث";
            this.buttonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSearch.UseVisualStyleBackColor = false;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = typeof(Follow_Extremist.Core.ElementInfo);
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 74);
            this.gridControl1.MainView = this.cardView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1343, 624);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.cardView1});
            // 
            // cardView1
            // 
            this.cardView1.Appearance.Card.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.Card.Options.UseFont = true;
            this.cardView1.Appearance.Card.Options.UseTextOptions = true;
            this.cardView1.Appearance.Card.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.Appearance.Card.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.Appearance.CardButton.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.CardButton.Options.UseFont = true;
            this.cardView1.Appearance.CardButton.Options.UseTextOptions = true;
            this.cardView1.Appearance.CardButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.Appearance.CardButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.Appearance.CardCaption.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.CardCaption.Options.UseFont = true;
            this.cardView1.Appearance.CardCaption.Options.UseTextOptions = true;
            this.cardView1.Appearance.CardCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.Appearance.CardCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.Appearance.CardExpandButton.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.CardExpandButton.Options.UseFont = true;
            this.cardView1.Appearance.EmptySpace.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.EmptySpace.Options.UseFont = true;
            this.cardView1.Appearance.FieldCaption.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.FieldCaption.Options.UseFont = true;
            this.cardView1.Appearance.FieldValue.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.FieldValue.Options.UseFont = true;
            this.cardView1.Appearance.FieldValue.Options.UseTextOptions = true;
            this.cardView1.Appearance.FieldValue.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.Appearance.FieldValue.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.cardView1.Appearance.FilterPanel.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.FilterPanel.Options.UseFont = true;
            this.cardView1.Appearance.FocusedCardCaption.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.FocusedCardCaption.Options.UseFont = true;
            this.cardView1.Appearance.SeparatorLine.BackColor = System.Drawing.Color.Black;
            this.cardView1.Appearance.SeparatorLine.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cardView1.Appearance.SeparatorLine.BorderColor = System.Drawing.Color.Black;
            this.cardView1.Appearance.SeparatorLine.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.SeparatorLine.Options.UseBackColor = true;
            this.cardView1.Appearance.SeparatorLine.Options.UseBorderColor = true;
            this.cardView1.Appearance.SeparatorLine.Options.UseFont = true;
            this.cardView1.Appearance.SeparatorLine.Options.UseTextOptions = true;
            this.cardView1.Appearance.SeparatorLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.Appearance.SeparatorLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.Appearance.ViewCaption.BorderColor = System.Drawing.Color.Black;
            this.cardView1.Appearance.ViewCaption.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.Appearance.ViewCaption.Options.UseBorderColor = true;
            this.cardView1.Appearance.ViewCaption.Options.UseFont = true;
            this.cardView1.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.cardView1.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.AppearancePrint.Card.Font = new System.Drawing.Font("Cairo", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.AppearancePrint.Card.Options.UseFont = true;
            this.cardView1.AppearancePrint.Card.Options.UseTextOptions = true;
            this.cardView1.AppearancePrint.Card.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.AppearancePrint.Card.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.AppearancePrint.CardCaption.Font = new System.Drawing.Font("Cairo", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.AppearancePrint.CardCaption.Options.UseFont = true;
            this.cardView1.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cardView1.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.cardView1.AppearancePrint.FilterPanel.Options.UseTextOptions = true;
            this.cardView1.AppearancePrint.FilterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cardView1.AppearancePrint.FilterPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.cardView1.AppearancePrint.FilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.cardView1.CardWidth = 700;
            this.cardView1.DetailHeight = 600;
            this.cardView1.GridControl = this.gridControl1;
            this.cardView1.Name = "cardView1";
            this.cardView1.OptionsBehavior.AllowExpandCollapse = false;
            this.cardView1.OptionsBehavior.Editable = false;
            this.cardView1.OptionsBehavior.FieldAutoHeight = true;
            this.cardView1.OptionsPrint.AutoHorzWidth = true;
            this.cardView1.OptionsView.ShowCardCaption = false;
            this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            // 
            // ElementInfoShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 698);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ElementInfoShowForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "عرض تفاصيل العنصر ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            this.ResumeLayout(false);

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
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
    }
}