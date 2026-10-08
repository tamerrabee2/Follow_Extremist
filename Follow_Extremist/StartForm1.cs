using DevExpress.XtraSplashScreen;
using Follow_Extremist.Code;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist
{
    public partial class StartForm1 : SplashScreen
    {
        private IDataHelper<Users> dataHelper;

        public StartForm1()
        {
            InitializeComponent();
            AppIconHelper.ApplyFormIcon(this);
            this.Text = "برنامج متابعة العناصر المتطرفة";
            this.labelCopyright.Text = "Copyright © 2023-" + DateTime.Now.Year.ToString();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private async void StartForm1_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            await Task.Run(() => Thread.Sleep(2000));
            Checkcon();
        }

        private async void Checkcon()
        {
            labelStatus.Text = "جاري الاتصال بقاعدة البيانات...";
            if (dataHelper == null)
            {
                dataHelper = (IDataHelper<Users>)ConfigurationObjectManager.GetObject("Users");
            }
            if (dataHelper == null) return;

            bool canConnect = false;
            string errorDetail = null;

            try
            {
                using (var dB = new Follow_Extremist.Data.SqlServer.DBContext())
                {
                    canConnect = await dB.Database.CanConnectAsync();
                    if (!canConnect)
                    {
                        await dB.Database.OpenConnectionAsync();
                        canConnect = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorDetail = ex.Message;
                canConnect = false;
            }

            if (canConnect)
            {
                var data = await dataHelper.GetAllDataAsync();
                if (data != null && data.Count > 0)
                {
                    // login form 
                    Gui.GuiUsers.UsersLoginForm loginForm = new Gui.GuiUsers.UsersLoginForm();
                    loginForm.Show();
                    Hide();
                }
                else
                {
                    // add user form 
                    Gui.GuiUsers.AddUsersForm addUsersForm = new Gui.GuiUsers.AddUsersForm(0, new Gui.GuiUsers.UsersUserControl(), true);
                    addUsersForm.Show();
                    Hide();
                }
            }
            else
            {
                Hide();
                string message = "هناك خطأ في الاتصال بقاعدة البيانات.";
                if (!string.IsNullOrEmpty(errorDetail))
                {
                    message += $"\n\nسبب الخطأ:\n{errorDetail}";
                }
                message += "\n\nاضغط 'نعم' لضبط الاتصال، أو 'لا' للخروج من البرنامج.";

                var result = DevExpress.XtraEditors.XtraMessageBox.Show(
                    message,
                    "خطأ في الاتصال",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Gui.GuiSettings.SettingsForm settingsForm = new Gui.GuiSettings.SettingsForm(true);
                    settingsForm.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

    }
}