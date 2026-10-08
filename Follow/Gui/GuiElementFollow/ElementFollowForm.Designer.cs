
namespace Follow.Gui.GuiElementFollow
{
    partial class ElementFollowForm
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
            this.comboBoxElement = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerelementFollow = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonSaveAndClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 84);
            this.panel1.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::Follow.Properties.Resources.Save;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(18, 16);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(150, 55);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "حفظ ";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveAndClose.Image = global::Follow.Properties.Resources.Save_as;
            this.buttonSaveAndClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveAndClose.Location = new System.Drawing.Point(330, 16);
            this.buttonSaveAndClose.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(165, 55);
            this.buttonSaveAndClose.TabIndex = 2;
            this.buttonSaveAndClose.Text = "حفظ وغلق";
            this.buttonSaveAndClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerelementFollow);
            this.groupBox1.Controls.Add(this.comboBoxElement);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 181);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "بيانات العنصر الشخصية ";
            // 
            // comboBoxElement
            // 
            this.comboBoxElement.FormattingEnabled = true;
            this.comboBoxElement.Location = new System.Drawing.Point(15, 36);
            this.comboBoxElement.Name = "comboBoxElement";
            this.comboBoxElement.Size = new System.Drawing.Size(293, 38);
            this.comboBoxElement.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(327, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "تاريخ المتابعة الجديد";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(327, 39);
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
            // dateTimePickerelementFollow
            // 
            this.dateTimePickerelementFollow.Location = new System.Drawing.Point(15, 111);
            this.dateTimePickerelementFollow.Name = "dateTimePickerelementFollow";
            this.dateTimePickerelementFollow.Size = new System.Drawing.Size(293, 37);
            this.dateTimePickerelementFollow.TabIndex = 8;
            // 
            // ElementFollowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 290);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ElementFollowForm";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxElement;
        private System.Windows.Forms.DateTimePicker dateTimePickerelementFollow;
    }
}