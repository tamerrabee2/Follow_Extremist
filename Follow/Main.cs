using Follow.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow.Gui;

namespace Follow
{
    public partial class Main : Form
    {
        private readonly PageManager pageManager;

        private Button currentActiveButton = null;
        

        public Main()
        {
            InitializeComponent();
            pageManager = new PageManager(this);
           // Load Home Page;
            pageManager.LoadPage(Gui.GuiHome.HomeUserControl.Instance());
            SetRoles();
        }

        #region Events
        private void buttonHome_Click(object sender, EventArgs e)
        {
            ChangeButtonColor(sender as Button);
            // Load Home Page
            pageManager.LoadPage(Gui.GuiHome.HomeUserControl.Instance());
        }

        private void buttonElement_Click(object sender, EventArgs e)
        {
            // Load Element Info
            pageManager.LoadPage(Gui.GuiElementInfo.ElementInfoControl1.Instance());
            ChangeButtonColor(sender as Button);

        }

        private void buttonElementAddInfo_Click(object sender, EventArgs e)
        {
            pageManager.LoadPage(Gui.GuiElementAddlInfo.ElementAddInfoControl.Instance());
            ChangeButtonColor(sender as Button);
        }

        private void buttonFollow_Click(object sender, EventArgs e)
        {
            pageManager.LoadPage(Gui.GuiElementFollow.ElementFollowUserControl1.Instance());
            ChangeButtonColor(sender as Button);
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            pageManager.LoadPage(Gui.GuiUsers.UsersUserControl.Instance());
            ChangeButtonColor(sender as Button);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Gui.GuiSettings.SettingsForm settingsForm = new Gui.GuiSettings.SettingsForm(false);
            settingsForm.Show();
            ChangeButtonColor(sender as Button);
        }

        private void buttonSystemRecord_Click(object sender, EventArgs e)
        {
            pageManager.LoadPage(Gui.GuiSystemRecords.RecordUserControl.Instance());
            ChangeButtonColor(sender as Button);
        }

        #endregion

        private void SetRoles()
        {
            if (!UsersRolesManager.GetRole("checkBoxHome"))
            {
                buttonHome.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxElementInfo"))
            {
                buttonElement.Enabled = false;

            }
            if (!UsersRolesManager.GetRole("checkBoxElementAddInfo"))
            {
                buttonElementAddInfo.Enabled = false;

            }
            if (!UsersRolesManager.GetRole("checkBoxFollowElement"))
            {
                buttonFollow.Enabled = false;
            }
            
            if (!UsersRolesManager.GetRole("checkBoxSetting"))
            {
                buttonSettings.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxUser"))
            {
                buttonUsers.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxSystemRecord"))
            {
                buttonSystemRecord.Enabled = false;
            }
            
        }

        
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Gui.GuiUsers.UsersLoginForm loginForm = new Gui.GuiUsers.UsersLoginForm();
            loginForm.Show();
            Hide();
        }

       
        private void buttonAbout_Click(object sender, EventArgs e)
        {
            //Gui.GuiAbout.About about = new Gui.GuiAbout.About();
            //about.Show();
        }
        
        private void ChangeButtonColor(Button clickedButton)
        {
            // Reset the color of the previously active button
            if (currentActiveButton != null)
            {
                currentActiveButton.BackColor = SystemColors.Control;
            }

            // Change the color of the clicked button
            clickedButton.BackColor = Color.LightBlue; // Example color

            // Update the current active button
            currentActiveButton = clickedButton;
        }
    }
}
