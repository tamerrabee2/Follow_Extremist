namespace Follow_Extremist.Gui.GuiFingerprint
{
    partial class ElementWantedManageForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new System.Windows.Forms.Panel();
            labelTitle = new System.Windows.Forms.Label();
            panelFooter = new System.Windows.Forms.Panel();
            buttonSave = new System.Windows.Forms.Button();
            buttonClose = new System.Windows.Forms.Button();
            panelContent = new System.Windows.Forms.Panel();
            groupBoxStatus = new System.Windows.Forms.GroupBox();
            textBoxNotes = new System.Windows.Forms.TextBox();
            labelNotes = new System.Windows.Forms.Label();
            textBoxWantedBy = new System.Windows.Forms.TextBox();
            labelWantedBy = new System.Windows.Forms.Label();
            textBoxReason = new System.Windows.Forms.TextBox();
            labelReason = new System.Windows.Forms.Label();
            checkBoxIsWanted = new System.Windows.Forms.CheckBox();
            labelElementDetails = new System.Windows.Forms.Label();
            searchableElementDropDown = new SearchableElementDropDown();
            panelHeader.SuspendLayout();
            panelFooter.SuspendLayout();
            panelContent.SuspendLayout();
            groupBoxStatus.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            panelHeader.Location = new System.Drawing.Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new System.Drawing.Size(580, 58);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            labelTitle.ForeColor = System.Drawing.Color.White;
            labelTitle.Location = new System.Drawing.Point(0, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(580, 58);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "🚨 إدارة حالة طلب الاشخاص (مطلوب للمتابعة)";
            labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            panelFooter.Controls.Add(buttonSave);
            panelFooter.Controls.Add(buttonClose);
            panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelFooter.Location = new System.Drawing.Point(0, 480);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            panelFooter.Size = new System.Drawing.Size(580, 60);
            panelFooter.TabIndex = 1;
            // 
            // buttonSave
            // 
            buttonSave.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonSave.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            buttonSave.ForeColor = System.Drawing.Color.White;
            buttonSave.Location = new System.Drawing.Point(394, 12);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(170, 36);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "💾 حفظ وتحديث الحالة";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = System.Drawing.Color.FromArgb(148, 163, 184);
            buttonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            buttonClose.Dock = System.Windows.Forms.DockStyle.Left;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonClose.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            buttonClose.ForeColor = System.Drawing.Color.White;
            buttonClose.Location = new System.Drawing.Point(16, 12);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(120, 36);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "إغلاق";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(groupBoxStatus);
            panelContent.Controls.Add(labelElementDetails);
            panelContent.Controls.Add(searchableElementDropDown);
            panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(0, 58);
            panelContent.Name = "panelContent";
            panelContent.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            panelContent.Size = new System.Drawing.Size(580, 422);
            panelContent.TabIndex = 2;
            // 
            // groupBoxStatus
            // 
            groupBoxStatus.BackColor = System.Drawing.Color.White;
            groupBoxStatus.Controls.Add(textBoxNotes);
            groupBoxStatus.Controls.Add(labelNotes);
            groupBoxStatus.Controls.Add(textBoxWantedBy);
            groupBoxStatus.Controls.Add(labelWantedBy);
            groupBoxStatus.Controls.Add(textBoxReason);
            groupBoxStatus.Controls.Add(labelReason);
            groupBoxStatus.Controls.Add(checkBoxIsWanted);
            groupBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxStatus.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            groupBoxStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            groupBoxStatus.Location = new System.Drawing.Point(20, 90);
            groupBoxStatus.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            groupBoxStatus.Name = "groupBoxStatus";
            groupBoxStatus.Padding = new System.Windows.Forms.Padding(12);
            groupBoxStatus.Size = new System.Drawing.Size(540, 316);
            groupBoxStatus.TabIndex = 3;
            groupBoxStatus.TabStop = false;
            groupBoxStatus.Text = "تفاصيل حالة الطلب";
            // 
            // textBoxNotes
            // 
            textBoxNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBoxNotes.Location = new System.Drawing.Point(16, 208);
            textBoxNotes.Multiline = true;
            textBoxNotes.Name = "textBoxNotes";
            textBoxNotes.Size = new System.Drawing.Size(500, 52);
            textBoxNotes.TabIndex = 6;
            textBoxNotes.TextChanged += textBoxNotes_TextChanged;
            // 
            // labelNotes
            // 
            labelNotes.AutoSize = true;
            labelNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelNotes.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelNotes.Location = new System.Drawing.Point(455, 184);
            labelNotes.Name = "labelNotes";
            labelNotes.Size = new System.Drawing.Size(79, 23);
            labelNotes.TabIndex = 5;
            labelNotes.Text = "ملاحظات:";
            // 
            // textBoxWantedBy
            // 
            textBoxWantedBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBoxWantedBy.Location = new System.Drawing.Point(16, 150);
            textBoxWantedBy.Name = "textBoxWantedBy";
            textBoxWantedBy.Size = new System.Drawing.Size(500, 30);
            textBoxWantedBy.TabIndex = 4;
            // 
            // labelWantedBy
            // 
            labelWantedBy.AutoSize = true;
            labelWantedBy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelWantedBy.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelWantedBy.Location = new System.Drawing.Point(430, 126);
            labelWantedBy.Name = "labelWantedBy";
            labelWantedBy.Size = new System.Drawing.Size(107, 23);
            labelWantedBy.TabIndex = 3;
            labelWantedBy.Text = "الجهة الطالبة:";
            // 
            // textBoxReason
            // 
            textBoxReason.Font = new System.Drawing.Font("Segoe UI", 10F);
            textBoxReason.Location = new System.Drawing.Point(16, 92);
            textBoxReason.Name = "textBoxReason";
            textBoxReason.PlaceholderText = "اكتب سبب طلب الشخص للمتابعة...";
            textBoxReason.Size = new System.Drawing.Size(500, 30);
            textBoxReason.TabIndex = 2;
            // 
            // labelReason
            // 
            labelReason.AutoSize = true;
            labelReason.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            labelReason.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelReason.Location = new System.Drawing.Point(440, 68);
            labelReason.Name = "labelReason";
            labelReason.Size = new System.Drawing.Size(102, 23);
            labelReason.TabIndex = 1;
            labelReason.Text = "سبب الطلب:";
            // 
            // checkBoxIsWanted
            // 
            checkBoxIsWanted.AutoSize = true;
            checkBoxIsWanted.Cursor = System.Windows.Forms.Cursors.Hand;
            checkBoxIsWanted.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            checkBoxIsWanted.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            checkBoxIsWanted.Location = new System.Drawing.Point(220, 31);
            checkBoxIsWanted.Name = "checkBoxIsWanted";
            checkBoxIsWanted.Size = new System.Drawing.Size(316, 34);
            checkBoxIsWanted.TabIndex = 0;
            checkBoxIsWanted.Text = "🚨 هذا الشخص مـطـلـوب حـالـيـاً";
            checkBoxIsWanted.UseVisualStyleBackColor = true;
            checkBoxIsWanted.CheckedChanged += checkBoxIsWanted_CheckedChanged;
            // 
            // labelElementDetails
            // 
            labelElementDetails.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            labelElementDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelElementDetails.Dock = System.Windows.Forms.DockStyle.Top;
            labelElementDetails.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            labelElementDetails.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            labelElementDetails.Location = new System.Drawing.Point(20, 52);
            labelElementDetails.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            labelElementDetails.Name = "labelElementDetails";
            labelElementDetails.Padding = new System.Windows.Forms.Padding(8);
            labelElementDetails.Size = new System.Drawing.Size(540, 38);
            labelElementDetails.TabIndex = 2;
            labelElementDetails.Text = "بيانات الشخص: لم يتم تحديد اسم بعد";
            labelElementDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // searchableElementDropDown
            // 
            searchableElementDropDown.BackColor = System.Drawing.Color.White;
            searchableElementDropDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            searchableElementDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            searchableElementDropDown.Location = new System.Drawing.Point(20, 16);
            searchableElementDropDown.Name = "searchableElementDropDown";
            searchableElementDropDown.Padding = new System.Windows.Forms.Padding(1);
            searchableElementDropDown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            searchableElementDropDown.Size = new System.Drawing.Size(540, 36);
            searchableElementDropDown.TabIndex = 1;
            // 
            // ElementWantedManageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            ClientSize = new System.Drawing.Size(580, 540);
            Controls.Add(panelContent);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ElementWantedManageForm";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "إدارة حالة طلب العناصر";
            Load += ElementWantedManageForm_Load;
            panelHeader.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            groupBoxStatus.ResumeLayout(false);
            groupBoxStatus.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panelContent;
        private Follow_Extremist.Gui.GuiFingerprint.SearchableElementDropDown searchableElementDropDown;
        private System.Windows.Forms.Label labelElementDetails;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.CheckBox checkBoxIsWanted;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.Label labelWantedBy;
        private System.Windows.Forms.TextBox textBoxWantedBy;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.TextBox textBoxNotes;
    }
}
