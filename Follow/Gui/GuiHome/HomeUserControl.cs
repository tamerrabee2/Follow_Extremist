using Follow.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Follow.Gui.GuiHome
{
    public partial class HomeUserControl : UserControl
    {
        // Variables 
        private static HomeUserControl _HomeUserControl;
        public HomeUserControl()
        {
            InitializeComponent();
            SetRoles();
            SetGeneralSettings();
            SetHello();
        }

        public static HomeUserControl Instance()
        {
            return _HomeUserControl ?? (new HomeUserControl());
        }

        private void SetRoles()
        {
            
            if (!UsersRolesManager.GetRole("checkBoxAccessElement"))
            {
                buttonAddCustomer.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxAccessElementAddInfo"))
            {
                buttonAddSupplier.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxAccessUser"))
            {
                buttonAddUser.Visible = false;
            }

            if (!UsersRolesManager.GetRole("checkBoxAccessElementFollowbroken"))
            {
                buttonFollowBroken.Visible = false;
            }
        }

        private void SetGeneralSettings()
        {
            labelCompany.Text = Properties.Settings.Default.CompanyName;

            // set Picture

            if (Properties.Settings.Default.CompanyLogo != string.Empty)
            {
                var ImageAsByte = Convert.FromBase64String(Properties.Settings.Default.CompanyLogo);
                using (MemoryStream ma = new MemoryStream(ImageAsByte))
                {
                    pictureBoxLogo.Image = Image.FromStream(ma);
                }
            }

        }

        private void SetHello()
        {
            labelWelcome.Text = "مرحبا بك " + Properties.Settings.Default.UserName;
        }

        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonAddCustomer_Click(object sender, EventArgs e)
        {
            Gui.GuiElementInfo.AddElementForm addElementForm = new GuiElementInfo.AddElementForm(0,new GuiElementInfo.ElementInfoControl1(),false,false);
            addElementForm.Show();
        }

        private void buttonAddSupplier_Click(object sender, EventArgs e)
        {
            Gui.GuiElementAddlInfo.AddElementAddInfoForm addElementAddInfoForm = new GuiElementAddlInfo.AddElementAddInfoForm(0, new Gui.GuiElementAddlInfo.ElementAddInfoControl());
            addElementAddInfoForm.Show();
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            Gui.GuiUsers.AddUsersForm addUsersForm = new GuiUsers.AddUsersForm(0, new GuiUsers.UsersUserControl(), false);
            addUsersForm.Show();
        }

        private void buttonFollowBroken_Click(object sender, EventArgs e)
        {
            Gui.GuiBreakFollow.ElementsBreakForm elementsBreakForm = new GuiBreakFollow.ElementsBreakForm();
            elementsBreakForm.Show();
        }
    }
}
