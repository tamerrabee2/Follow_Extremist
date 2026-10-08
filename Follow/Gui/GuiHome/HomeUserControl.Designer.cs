
namespace Follow.Gui.GuiHome
{
    partial class HomeUserControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddSupplier = new System.Windows.Forms.Button();
            this.buttonAddUser = new System.Windows.Forms.Button();
            this.buttonFollowBroken = new System.Windows.Forms.Button();
            this.buttonAddCustomer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelCompany = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 183);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::Follow.Properties.Resources.Smart;
            this.pictureBox1.Location = new System.Drawing.Point(489, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.buttonAddSupplier);
            this.groupBox1.Controls.Add(this.buttonAddUser);
            this.groupBox1.Controls.Add(this.buttonFollowBroken);
            this.groupBox1.Controls.Add(this.buttonAddCustomer);
            this.groupBox1.Location = new System.Drawing.Point(74, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1106, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "اضافة";
            // 
            // buttonAddSupplier
            // 
            this.buttonAddSupplier.Image = global::Follow.Properties.Resources.Supplier;
            this.buttonAddSupplier.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddSupplier.Location = new System.Drawing.Point(573, 29);
            this.buttonAddSupplier.Margin = new System.Windows.Forms.Padding(5);
            this.buttonAddSupplier.Name = "buttonAddSupplier";
            this.buttonAddSupplier.Size = new System.Drawing.Size(143, 55);
            this.buttonAddSupplier.TabIndex = 7;
            this.buttonAddSupplier.Text = "بيان اضافي";
            this.buttonAddSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddSupplier.UseVisualStyleBackColor = true;
            this.buttonAddSupplier.Click += new System.EventHandler(this.buttonAddSupplier_Click);
            // 
            // buttonAddUser
            // 
            this.buttonAddUser.Image = global::Follow.Properties.Resources.Users;
            this.buttonAddUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddUser.Location = new System.Drawing.Point(404, 29);
            this.buttonAddUser.Margin = new System.Windows.Forms.Padding(5);
            this.buttonAddUser.Name = "buttonAddUser";
            this.buttonAddUser.Size = new System.Drawing.Size(127, 55);
            this.buttonAddUser.TabIndex = 4;
            this.buttonAddUser.Text = "مستخدم";
            this.buttonAddUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddUser.UseVisualStyleBackColor = true;
            this.buttonAddUser.Click += new System.EventHandler(this.buttonAddUser_Click);
            // 
            // buttonFollowBroken
            // 
            this.buttonFollowBroken.Image = global::Follow.Properties.Resources.Microsoft_Project;
            this.buttonFollowBroken.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFollowBroken.Location = new System.Drawing.Point(227, 29);
            this.buttonFollowBroken.Margin = new System.Windows.Forms.Padding(5);
            this.buttonFollowBroken.Name = "buttonFollowBroken";
            this.buttonFollowBroken.Size = new System.Drawing.Size(151, 55);
            this.buttonFollowBroken.TabIndex = 3;
            this.buttonFollowBroken.Text = "كسر المتابعة";
            this.buttonFollowBroken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonFollowBroken.UseVisualStyleBackColor = true;
            this.buttonFollowBroken.Click += new System.EventHandler(this.buttonFollowBroken_Click);
            // 
            // buttonAddCustomer
            // 
            this.buttonAddCustomer.Image = global::Follow.Properties.Resources.Supplier;
            this.buttonAddCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddCustomer.Location = new System.Drawing.Point(760, 29);
            this.buttonAddCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.buttonAddCustomer.Name = "buttonAddCustomer";
            this.buttonAddCustomer.Size = new System.Drawing.Size(136, 55);
            this.buttonAddCustomer.TabIndex = 2;
            this.buttonAddCustomer.Text = "عنصر";
            this.buttonAddCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddCustomer.UseVisualStyleBackColor = true;
            this.buttonAddCustomer.Click += new System.EventHandler(this.buttonAddCustomer_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(555, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "الوصول السريع";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.labelCompany);
            this.panel2.Controls.Add(this.pictureBoxLogo);
            this.panel2.Location = new System.Drawing.Point(787, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 100);
            this.panel2.TabIndex = 1;
            // 
            // labelCompany
            // 
            this.labelCompany.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCompany.Location = new System.Drawing.Point(7, 12);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(347, 76);
            this.labelCompany.TabIndex = 4;
            this.labelCompany.Text = "برنامج متابعة العناصر ";
            this.labelCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxLogo.Image = global::Follow.Properties.Resources.Smart;
            this.pictureBoxLogo.Location = new System.Drawing.Point(365, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(97, 100);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelWelcome
            // 
            this.labelWelcome.Font = new System.Drawing.Font("Cairo", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelWelcome.Location = new System.Drawing.Point(0, 0);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(347, 100);
            this.labelWelcome.TabIndex = 5;
            this.labelWelcome.Text = "مرحبا بك تامر ربيع ";
            this.labelWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox2.Image = global::Follow.Properties.Resources.wPRmPIeh_400x400;
            this.pictureBox2.Location = new System.Drawing.Point(410, 141);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(442, 314);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // HomeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cairo", 11.89565F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "HomeUserControl";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1262, 597);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAddUser;
        private System.Windows.Forms.Button buttonFollowBroken;
        private System.Windows.Forms.Button buttonAddCustomer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Button buttonAddSupplier;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
