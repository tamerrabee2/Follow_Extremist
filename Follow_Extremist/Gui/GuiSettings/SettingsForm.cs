using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Gui.GuiLoading;
using Follow_Extremist.Code;

namespace Follow_Extremist.Gui.GuiSettings
{
    public partial class SettingsForm : Form
    {
        private LoadingForm loading;
        private readonly bool firstStart;

        public SettingsForm(bool FirstStart)
        {
            InitializeComponent();
            FormLayoutHelper.ApplyDialogLayout(this);
            SetGeneralSettings();
            SetConnectionSettings();
            SetAutoBackupSettings();
            LoadUsersForAutoBackup();
            loading = new LoadingForm();
            firstStart = FirstStart;
        }

        private void SetConnectionSettings()
        {
            // Set the connection settings based on the saved settings
            string server = Properties.Settings.Default.Server;
            string database = Properties.Settings.Default.Database;

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database))
            {
                try
                {
                    string conStr = Properties.Settings.Default.SqServerConString;
                    if (!string.IsNullOrEmpty(conStr))
                    {
                        var builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(conStr);
                        if (string.IsNullOrEmpty(server)) server = builder.DataSource;
                        if (string.IsNullOrEmpty(database)) database = builder.InitialCatalog;
                    }
                }
                catch
                {
                }
            }

            textBoxServer.Text = !string.IsNullOrEmpty(server) ? server : @"DESKTOP-2B87UHT\MSSQLSERVER2019";
            textBoxDatabase.Text = !string.IsNullOrEmpty(database) ? database : "FollowDataBase";

            if (Properties.Settings.Default.SqServerConString.Contains("Trusted_Connection=True", StringComparison.OrdinalIgnoreCase))
            {
                // Local connection
                radioButtonLocalCon.Checked = true;
                textBoxUserName.Enabled = false;
                textBoxPassword.Enabled = false;
                numericUpDownTimeOut.Enabled = false;
            }
            else
            {
                // Network connection
                radioButtonNetworkConn.Checked = true;
                textBoxUserName.Text = Properties.Settings.Default.Userlogin;
                textBoxPassword.Text = Properties.Settings.Default.Password;
                numericUpDownTimeOut.Value = Convert.ToInt32(Properties.Settings.Default.Timeout);
                textBoxUserName.Enabled = true;
                textBoxPassword.Enabled = true;
                numericUpDownTimeOut.Enabled = true;
            }
        }

        private void buttonSaveGeneral_Click(object sender, EventArgs e)
        {
            SaveGeneralSettings();

        }

        private void SaveGeneralSettings()
        {
            Properties.Settings.Default.CompanyName = textBoxCompany.Text;
            Properties.Settings.Default.HideNotification = Convert.ToInt32(numericUpDownNotification.Value);
            Properties.Settings.Default.DataGridViewRowNumber = Convert.ToInt32(numericUpDownDataRow.Value);
            Properties.Settings.Default.breakfollow = colorPickEditbreakfollow.Color;
            Properties.Settings.Default.prison = colorPickEditprison.Color;
            Properties.Settings.Default.outfollow = colorPickEditoutfollow.Color;
        
            // save Picture
            if (pictureBoxLogo.Image != null)
            {
                using (MemoryStream ma = new MemoryStream())
                {
                    pictureBoxLogo.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Png);
                    Properties.Settings.Default.CompanyLogo = Convert.ToBase64String(ma.ToArray());
                }
            }
        
            // save setting 
            Properties.Settings.Default.Save();
            MessageBox.Show("تم حفظ الاعدادات بنجاح");
        }
        private void SetGeneralSettings()
        {
            textBoxCompany.Text= Properties.Settings.Default.CompanyName;
            // تحقق من أن القيمة لا تتجاوز الحد الأقصى لعنصر التحكم
            decimal notificationValue = Convert.ToInt32(Properties.Settings.Default.HideNotification/1000);
            numericUpDownNotification.Value = Math.Min(notificationValue, numericUpDownNotification.Maximum);
            numericUpDownDataRow.Value = Properties.Settings.Default.DataGridViewRowNumber;
            //Check and set default colors if necessary
            colorPickEditoutfollow.Color = Properties.Settings.Default.outfollow != Color.Empty ? Properties.Settings.Default.outfollow : Color.LightGray;
            colorPickEditprison.Color = Properties.Settings.Default.prison != Color.Empty ? Properties.Settings.Default.prison : Color.DeepSkyBlue;
            colorPickEditbreakfollow.Color = Properties.Settings.Default.breakfollow != Color.Empty ? Properties.Settings.Default.breakfollow : Color.DarkGray;
            colorPickEditoutfollow.Color = Properties.Settings.Default.outfollow;
            colorPickEditprison.Color = Properties.Settings.Default.prison;
            colorPickEditbreakfollow.Color = Properties.Settings.Default.breakfollow;
            // set Picture

            if (Properties.Settings.Default.CompanyLogo != string.Empty)
            {
                var ImageAsByte =Convert.FromBase64String (Properties.Settings.Default.CompanyLogo);
                using (MemoryStream ma = new MemoryStream(ImageAsByte))
                {
                        pictureBoxLogo.Image = Image.FromStream(ma);
                }
            }
            
        }

        private async void buttonSaveConnectionString_Click(object sender, EventArgs e)
        {
            var server = textBoxServer.Text.Trim();
            var dataBase = textBoxDatabase.Text.Trim();
            var timeout = numericUpDownTimeOut.Value;
            var userName = textBoxUserName.Text.Trim();
            var password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(dataBase))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("يرجى إدخال اسم السيرفر وقاعدة البيانات أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string conStr;
            if (radioButtonLocalCon.Checked)
            {
                SetLocalCon(server, dataBase);
                conStr = Properties.Settings.Default.SqServerConString;
            }
            else
            {
                SetNetworkCon(server, dataBase, userName, password, timeout);
                conStr = Properties.Settings.Default.SqServerConString;
            }

            Properties.Settings.Default.Server = server;
            Properties.Settings.Default.Database = dataBase;
            Properties.Settings.Default.Userlogin = userName;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.Timeout = (int)timeout;
            Properties.Settings.Default.Save();
            Follow_Extremist.Data.SqlServer.SqlCon.SqlConnection = conStr;

            // Live Connection Testing
            loading.Show();
            bool connected = false;
            string errorDetail = null;

            try
            {
                using (var con = new Microsoft.Data.SqlClient.SqlConnection(conStr))
                {
                    await con.OpenAsync();
                    connected = true;
                }
            }
            catch (Exception ex)
            {
                errorDetail = ex.Message;
            }
            finally
            {
                loading.Hide();
            }

            if (connected)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "تم الاتصال بقاعدة البيانات وحفظ الإعدادات بنجاح!\nسيتم إعادة تشغيل التطبيق لتطبيق الاتصال الجديد.",
                    "نجاح الاتصال",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Application.Restart();
                Environment.Exit(0);
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"تم حفظ نص الاتصال ولكن فشل الاتصال بقاعدة البيانات:\n\n{errorDetail}\n\nيرجى التأكد من اسم السيرفر وقاعدة البيانات وصلاحيات الوصول.",
                    "فشل الاتصال",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void SetNetworkCon(string server, string dataBase, string userName, string password, decimal timeout)
        {
            int timeoutSeconds = (int)Math.Floor(timeout);
            var ConString = string.Format("Server={0};Database={1};User Id={2};Password={3};Timeout={4};TrustServerCertificate=True;",
                                          server, dataBase, userName, password, timeoutSeconds);
            Properties.Settings.Default.SqServerConString = ConString;
            Follow_Extremist.Data.SqlServer.SqlCon.SqlConnection = ConString;
        }

        private void SetLocalCon(string server, string dataBase)
        {
            var ConString = @"Server=" + server + ";Database=" + dataBase + ";Trusted_Connection=True;TrustServerCertificate=True;"; 
            Properties.Settings.Default.SqServerConString = ConString;
            Follow_Extremist.Data.SqlServer.SqlCon.SqlConnection = ConString;
        }

        private void SettingsForm_Activated(object sender, EventArgs e)
        {
        }

        private void radioButtonLocalCon_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLocalCon.Checked)
            {
                textBoxUserName.Enabled = false;
                textBoxPassword.Enabled = false;
                numericUpDownTimeOut.Enabled = false;
            }
        }

        private void radioButtonNetworkConn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNetworkConn.Checked)
            {
                textBoxUserName.Enabled = true;
                textBoxPassword.Enabled = true;
                numericUpDownTimeOut.Enabled = true;
            }
        }

        private void linkLabelImportImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "اختر شعار المؤسسة";
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBoxLogo.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private async void  buttonBackUp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "اختر مسار حفظ النسخة الاحتياطية وتجنب اختيار القرص النظام";
            var result = folderBrowserDialog.ShowDialog();
            if (result==DialogResult.OK)
            {
                Data.SqlServer.BackupRestoreHelper backupRestoreHelper = new Data.SqlServer.BackupRestoreHelper();
                loading.Show();
             string ProcessResult = await Task.Run(()=>  backupRestoreHelper.BackUP(folderBrowserDialog.SelectedPath));
                if (ProcessResult == "1")
                {
                    loading.Hide();
                    MessageBox.Show("تم اجراء النسخ الاحتياطي بنجاح");

                }
                else
                {
                    loading.Hide();
                    MessageBox.Show( ProcessResult+"لم نتمكن من اجراء عملية النسخ الاحتياطي بسبب");

                }
            }
        }

        private async void buttonRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "اختر مسار النسخة الاحتياطية ";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Bak File|*.bak ";
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Data.SqlServer.BackupRestoreHelper backupRestoreHelper = new Data.SqlServer.BackupRestoreHelper();
                loading.Show();
                string ProcessResult = await Task.Run(() => backupRestoreHelper.Restore(openFileDialog.FileName));
                if (ProcessResult == "1")
                {
                    loading.Hide();
                    MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح");

                }
                else
                {
                    loading.Hide();
                    MessageBox.Show(ProcessResult + "لم نتمكن من استعادة النسخة الاحتياطية بسبب");

                }
            }
        }

        private void SetAutoBackupSettings()
        {
            checkBoxAutoBackup.Checked = Properties.Settings.Default.AutoBackupEnabled;
            textBoxAutoBackupPath.Text = Properties.Settings.Default.AutoBackupPath;
            string lastDate = Properties.Settings.Default.LastAutoBackupDate;
            labelLastBackupStatus.Text = !string.IsNullOrEmpty(lastDate)
                ? $"آخر نسخة تم حفظها: {lastDate}"
                : "آخر نسخة تم حفظها: لا يوجد بعد";
        }

        private void LoadUsersForAutoBackup()
        {
            comboBoxAutoBackupUser.Items.Clear();
            comboBoxAutoBackupUser.Items.Add("الكل (أي مستخدم)");

            try
            {
                var usersHelper = (Follow_Extremist.Data.IDataHelper<Follow_Extremist.Core.Users>)ConfigurationObjectManager.GetObject("Users");
                if (usersHelper != null)
                {
                    var userList = usersHelper.GetAllData();
                    if (userList != null)
                    {
                        foreach (var u in userList)
                        {
                            if (!string.IsNullOrWhiteSpace(u.UserName) && !comboBoxAutoBackupUser.Items.Contains(u.UserName))
                            {
                                comboBoxAutoBackupUser.Items.Add(u.UserName);
                            }
                        }
                    }
                }
            }
            catch
            {
                // في حال لم يتم الاتصال بقاعدة البيانات بعد
            }

            string savedUser = Properties.Settings.Default.AutoBackupUserName?.Trim();
            if (string.IsNullOrEmpty(savedUser) || savedUser.StartsWith("الكل") || savedUser.Contains("أي مستخدم"))
            {
                comboBoxAutoBackupUser.SelectedIndex = 0;
            }
            else
            {
                int index = comboBoxAutoBackupUser.FindStringExact(savedUser);
                if (index >= 0)
                {
                    comboBoxAutoBackupUser.SelectedIndex = index;
                }
                else
                {
                    comboBoxAutoBackupUser.Items.Add(savedUser);
                    comboBoxAutoBackupUser.SelectedItem = savedUser;
                }
            }
        }

        private void buttonBrowseAutoBackup_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "اختر مسار حفظ النسخ الاحتياطية التلقائية";
                if (!string.IsNullOrEmpty(textBoxAutoBackupPath.Text) && Directory.Exists(textBoxAutoBackupPath.Text))
                {
                    folderDialog.SelectedPath = textBoxAutoBackupPath.Text;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxAutoBackupPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void buttonSaveAutoBackup_Click(object sender, EventArgs e)
        {
            string path = textBoxAutoBackupPath.Text.Trim();
            if (checkBoxAutoBackup.Checked && string.IsNullOrEmpty(path))
            {
                MessageBox.Show("يرجى تحديد مسار حفظ النسخة الاحتياطية أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("تعذر إنشاء المجلد المحدد: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Properties.Settings.Default.AutoBackupEnabled = checkBoxAutoBackup.Checked;
            Properties.Settings.Default.AutoBackupPath = path;

            string selectedUser = comboBoxAutoBackupUser.SelectedItem?.ToString() ?? comboBoxAutoBackupUser.Text.Trim();
            if (string.IsNullOrEmpty(selectedUser) || selectedUser.StartsWith("الكل") || selectedUser.Contains("أي مستخدم"))
            {
                selectedUser = "الكل";
            }

            Properties.Settings.Default.AutoBackupUserName = selectedUser;
            Properties.Settings.Default.Save();

            MessageBox.Show("تم حفظ إعدادات النسخ الاحتياطي التلقائي بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (firstStart == true)
            {
                Application.Exit();
            }
        }
    }
}
