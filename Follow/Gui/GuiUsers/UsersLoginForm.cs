using Follow.Code;
using Follow.Core;
using Follow.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow.Gui.GuiUsers
{
    public partial class UsersLoginForm : Form
    {
        #region variables 
        private readonly int ID;
        private readonly UsersUserControl userControl;
        private Users users;
        private readonly IDataHelper<Users> dataHelper;
        private readonly IDataHelper<UsersRoles> dataHelperUsersRole;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private Dictionary<string, bool> ListOfRoles = new Dictionary<string, bool>();
        #endregion
        public UsersLoginForm()
        {
            InitializeComponent();
            try
            {
                string iconPath = System.IO.Path.Combine(Application.StartupPath, "شعار_قطاع_الأمن_الوطني_(مصر).ico");
                if (System.IO.File.Exists(iconPath))
                {
                    this.Icon = new System.Drawing.Icon(iconPath);
                }
            }
            catch { }
            dataHelper = (IDataHelper<Users>)ConfigurationObjectManager.GetObject("Users");
            dataHelperUsersRole = (IDataHelper<UsersRoles>)ConfigurationObjectManager.GetObject("UsersRoles");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
        }

        #region Events

        private async void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            // check fields is empty
            if (IsFieldsEmpty())
            {
                MessageCollections.ShowFieldsRequired();
            }
            else
            {
                var UserName = textBoxUserName.Text;
                var Password = textBoxPassword.Text;

                loadingForm.Show();
                var UserLogin = await Task.Run(() => Login(UserName,Password));
                if (UserLogin == 1)
                {
                    // تشغيل النسخ الاحتياطي التلقائي في الخلفية إذا كان مفعلاً لهذا المستخدم اليوم
                    _ = Task.Run(() => AutoBackupService.ExecuteAutoBackupAsync(UserName));

                    Main main = new Main();
                    main.Show();
                    Hide();
                }
                else if (UserLogin == 2)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                    MessageBox.Show("هناك خطأ في معلومات تسجيل الدخول");
                {
                }
                loadingForm.Hide();
            }
        }

        
        #endregion

        #region Methods

        
        private bool IsFieldsEmpty()
        {
            if (
                 textBoxUserName.Text == string.Empty
                || textBoxPassword.Text == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       private int Login(string UserName, string Password)
        {
            try
            {
                // check
                Users users = dataHelper.GetAllData().Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
                if (users != null)
                {
                    Properties.Settings.Default.UserName = users.FullName;
                    Properties.Settings.Default.Save();
                    // Get Roles
                    var ListRoles = dataHelperUsersRole.GetAllData().Where(x => x.UserId == users.Id);
                    // loop into list of roles and set Roles
                    UsersRolesManager.ClearRoles();
                    foreach (var items in ListRoles)
                    {
                        UsersRolesManager.Register(items.Key, items.Value);
                    }

                    // save system records 
                    SystemRecords systemrecords = new SystemRecords
                    {
                        Title = " تسجيل دخول",
                        USerName = Properties.Settings.Default.UserName,
                        Details = "تم تسجيل دخول المستخدم " + users.UserName,
                        AddedDate = DateTime.Now
                    };
                     dataHelperSystemRecords.AddAsync(systemrecords);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch 
            {

                return 2;
            }      
        }

        private void AddCategoryForm_Load(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(515, 465);
            FormLayoutHelper.ApplyDialogLayout(this);

            int margin = 15;
            groupBox1.Left = margin;
            groupBox1.Top = 15;
            groupBox1.Width = this.ClientSize.Width - (margin * 2);
            groupBox1.Height = 370;

            buttonLogin.Left = groupBox1.Left;
            buttonLogin.Width = groupBox1.Width;
            buttonLogin.Top = groupBox1.Bottom + 10;
            buttonLogin.Height = 52;

            pictureBox1.Left = (groupBox1.ClientSize.Width - pictureBox1.Width) / 2;
            label8.Left = (groupBox1.ClientSize.Width - label8.Width) / 2;

            // تثبيت محاذاة وأبعاد الحقول والتسميات لضمان ظهورها بوضوح تام دون أي اقتطاع
            int tbLeft = 15;
            int tbWidth = 310;
            int starLeft = 330;
            int labelLeft = 350;

            textBoxUserName.Left = tbLeft;
            textBoxUserName.Width = tbWidth;
            label3.Left = starLeft;
            label2.Left = labelLeft;

            textBoxPassword.Left = tbLeft;
            textBoxPassword.Width = tbWidth;
            label6.Left = starLeft;
            label4.Left = labelLeft;
        }


        #endregion

        private void UsersLoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

}
