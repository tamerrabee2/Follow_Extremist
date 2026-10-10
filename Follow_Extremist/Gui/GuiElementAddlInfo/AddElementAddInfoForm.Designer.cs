
namespace Follow_Extremist.Gui.GuiElementAddlInfo
{
    partial class AddElementAddInfoForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxRelationship = new System.Windows.Forms.ComboBox();
            this.searchableElementDropDown = new Follow_Extremist.Gui.GuiFingerprint.SearchableElementDropDown();
            this.textBoxAge = new System.Windows.Forms.TextBox();
            this.textBoxNationalID = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.linkLabelElementID = new System.Windows.Forms.LinkLabel();
            this.label23 = new System.Windows.Forms.Label();
            this.pictureBoxElementID = new System.Windows.Forms.PictureBox();
            this.linkLabelElementImage = new System.Windows.Forms.LinkLabel();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBoxElementImage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxElementID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxElementImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonSaveAndClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 446);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 84);
            this.panel1.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::Follow_Extremist.Properties.Resources.Save;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(18, 16);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(150, 55);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "حفظ ";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveAndClose.Image = global::Follow_Extremist.Properties.Resources.Save_as;
            this.buttonSaveAndClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveAndClose.Location = new System.Drawing.Point(587, 16);
            this.buttonSaveAndClose.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(165, 55);
            this.buttonSaveAndClose.TabIndex = 8;
            this.buttonSaveAndClose.Text = "حفظ وغلق";
            this.buttonSaveAndClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxRelationship);
            this.groupBox1.Controls.Add(this.searchableElementDropDown);
            this.groupBox1.Controls.Add(this.textBoxAge);
            this.groupBox1.Controls.Add(this.textBoxNationalID);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 421);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات العنصر الشخصية ";
            // 
            // comboBoxRelationship
            // 
            this.comboBoxRelationship.FormattingEnabled = true;
            this.comboBoxRelationship.Items.AddRange(new object[] {
            "ابن ",
            "ابنه ",
            "زوجة",
            "زوج ",
            "اب",
            "ام ",
            "حفيد"});
            this.comboBoxRelationship.Location = new System.Drawing.Point(15, 259);
            this.comboBoxRelationship.Name = "comboBoxRelationship";
            this.comboBoxRelationship.Size = new System.Drawing.Size(318, 38);
            this.comboBoxRelationship.TabIndex = 4;
            // 
            // searchableElementDropDown
            // 
            this.searchableElementDropDown.BackColor = System.Drawing.Color.White;
            this.searchableElementDropDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchableElementDropDown.Font = new System.Drawing.Font("Cairo", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchableElementDropDown.Location = new System.Drawing.Point(15, 36);
            this.searchableElementDropDown.Name = "searchableElementDropDown";
            this.searchableElementDropDown.Padding = new System.Windows.Forms.Padding(1);
            this.searchableElementDropDown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.searchableElementDropDown.SelectedElement = null;
            this.searchableElementDropDown.Size = new System.Drawing.Size(318, 38);
            this.searchableElementDropDown.TabIndex = 1;
            // 
            // textBoxAge
            // 
            this.textBoxAge.Location = new System.Drawing.Point(15, 329);
            this.textBoxAge.Name = "textBoxAge";
            this.textBoxAge.Size = new System.Drawing.Size(318, 37);
            this.textBoxAge.TabIndex = 5;
            this.textBoxAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNationalID_KeyPress);
            // 
            // textBoxNationalID
            // 
            this.textBoxNationalID.Location = new System.Drawing.Point(15, 183);
            this.textBoxNationalID.MaxLength = 14;
            this.textBoxNationalID.Name = "textBoxNationalID";
            this.textBoxNationalID.Size = new System.Drawing.Size(318, 37);
            this.textBoxNationalID.TabIndex = 3;
            this.textBoxNationalID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxNationalID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNationalID_KeyPress);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(15, 109);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(318, 37);
            this.textBoxName.TabIndex = 2;
            this.textBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(349, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(349, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(403, 333);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "السن ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(361, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "الرقم القومي";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "صلة القرابة ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(398, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "الاسم ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(349, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 30);
            this.label5.TabIndex = 0;
            this.label5.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "اسم العنصر ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.linkLabelElementID);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.pictureBoxElementID);
            this.groupBox4.Controls.Add(this.linkLabelElementImage);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.pictureBoxElementImage);
            this.groupBox4.Location = new System.Drawing.Point(499, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(259, 409);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "المرفقات";
            // 
            // linkLabelElementID
            // 
            this.linkLabelElementID.AutoSize = true;
            this.linkLabelElementID.Location = new System.Drawing.Point(57, 367);
            this.linkLabelElementID.Name = "linkLabelElementID";
            this.linkLabelElementID.Size = new System.Drawing.Size(151, 30);
            this.linkLabelElementID.TabIndex = 7;
            this.linkLabelElementID.TabStop = true;
            this.linkLabelElementID.Text = "اضغط لتحميل الصورة";
            this.linkLabelElementID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelElementID_LinkClicked);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(57, 208);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(145, 30);
            this.label23.TabIndex = 5;
            this.label23.Text = "صورة الرقم القومي ";
            // 
            // pictureBoxElementID
            // 
            this.pictureBoxElementID.Image = global::Follow_Extremist.Properties.Resources.Customer;
            this.pictureBoxElementID.Location = new System.Drawing.Point(16, 241);
            this.pictureBoxElementID.Name = "pictureBoxElementID";
            this.pictureBoxElementID.Size = new System.Drawing.Size(224, 123);
            this.pictureBoxElementID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxElementID.TabIndex = 4;
            this.pictureBoxElementID.TabStop = false;
            // 
            // linkLabelElementImage
            // 
            this.linkLabelElementImage.AutoSize = true;
            this.linkLabelElementImage.Location = new System.Drawing.Point(57, 178);
            this.linkLabelElementImage.Name = "linkLabelElementImage";
            this.linkLabelElementImage.Size = new System.Drawing.Size(151, 30);
            this.linkLabelElementImage.TabIndex = 6;
            this.linkLabelElementImage.TabStop = true;
            this.linkLabelElementImage.Text = "اضغط لتحميل الصورة";
            this.linkLabelElementImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelElementImage_LinkClicked);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(87, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(97, 30);
            this.label22.TabIndex = 2;
            this.label22.Text = "صورة العنصر ";
            // 
            // pictureBoxElementImage
            // 
            this.pictureBoxElementImage.Image = global::Follow_Extremist.Properties.Resources.Supplier;
            this.pictureBoxElementImage.Location = new System.Drawing.Point(16, 57);
            this.pictureBoxElementImage.Name = "pictureBoxElementImage";
            this.pictureBoxElementImage.Size = new System.Drawing.Size(224, 118);
            this.pictureBoxElementImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxElementImage.TabIndex = 0;
            this.pictureBoxElementImage.TabStop = false;
            // 
            // AddElementAddInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 530);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddElementAddInfoForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اضافة / تعديل عنصر";
            this.Load += new System.EventHandler(this.AddElementForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxElementID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxElementImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAge;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.LinkLabel linkLabelElementID;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.PictureBox pictureBoxElementID;
        private System.Windows.Forms.LinkLabel linkLabelElementImage;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.PictureBox pictureBoxElementImage;
        private System.Windows.Forms.ComboBox comboBoxRelationship;
        private Follow_Extremist.Gui.GuiFingerprint.SearchableElementDropDown searchableElementDropDown;
        private System.Windows.Forms.TextBox textBoxNationalID;
        private System.Windows.Forms.Label label8;
    }
}