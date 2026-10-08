using Follow_Extremist.Gui.GuiUsers;
using Follow_Extremist.Code;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow_Extremist.Gui.GuiUsers
{
    public partial class AddUsersForm : Form
    {
        #region variables 
        private readonly int ID;
        bool state;
        private readonly UsersUserControl userControl;
        private readonly bool firstStart;
        private Users users;
        private readonly IDataHelper<Users> dataHelper;
        private readonly IDataHelper<UsersRoles> dataHelperUsersRole;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private Dictionary<string, bool> ListOfRoles = new Dictionary<string, bool>();
        #endregion
        public AddUsersForm(int Id, UsersUserControl userControl, bool FirstStart)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<Users>)ConfigurationObjectManager.GetObject("Users");
            dataHelperUsersRole = (IDataHelper<UsersRoles>)ConfigurationObjectManager.GetObject("UsersRoles");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.userControl = userControl;
            firstStart = FirstStart;
        }

        #region Events

        private async void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            // chexk fields is empty
            if (IsFieldsEmpty())
            {
                MessageCollections.ShowFieldsRequired();
            }
            else
            {
                loadingForm.Show();
                if (await SaveData())
                {
                    if (ID == 0)
                    {
                        this.DialogResult = DialogResult.OK;
                        MessageCollections.ShoAddNotification();
                    }
                    else
                    {
                        MessageCollections.ShowUpdateNotification();
                    }
                    if (firstStart == true)
                    {
                        MessageBox.Show("أعد تشغيل البرنامج ");
                        Application.Exit();
                    }
                    else
                    {
                        Close();
                    }

                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
                loadingForm.Hide();
            }
        }


        private async void buttonSave_Click(object sender, EventArgs e)
        {
            // chexk fields is empty
            if (IsFieldsEmpty())
            {
                MessageCollections.ShowFieldsRequired();
            }
            else
            {
                var checkduplicate = CheckDuplicateData();
                if (checkduplicate == true)
                {
                    MessageBox.Show("البيانات مكررة");
                }
                else
                {
                    loadingForm.Show();
                    if (await SaveData())
                    {
                        if (ID == 0)
                        {
                            this.DialogResult = DialogResult.OK;
                            MessageCollections.ShoAddNotification();
                        }
                        else
                        {
                            MessageCollections.ShowUpdateNotification();
                        }
                    }
                    else
                    {
                        MessageCollections.ShowErrorServer();
                    }
                    loadingForm.Hide();
                }
            }
        }

        private bool CheckDuplicateData()
        {
            try
            {
                var data = dataHelper.GetAllData();
                var user = data.Where(x => x.FullName == textBoxName.Text).FirstOrDefault();
                if (user == null)
                {
                    state = false;
                }
                else
                {
                    state = true;
                }

            }
            catch
            {

                state = false;
                MessageBox.Show("خطأ , من فضلك تحقق من الاسم");
            }
            return state;
        }
        #endregion

        #region Methods
        private async Task <bool> SaveData()
        {
          if (ID == 0) //add
          {

                var checkduplicate = CheckDuplicateData();
                if (checkduplicate == true)
                {
                    MessageBox.Show("البيانات مكررة");
                    return textBoxName.Focused;
                }
                else
                {
                    return await AddData();

                }

          }
          else   //edit
           {
                  return await EditData();
           }
        }
        private bool IsFieldsEmpty()
        {
            if (textBoxName.Text== string.Empty
                || textBoxUserName.Text == string.Empty
                || textBoxPassword.Text == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       private async Task<bool> AddData()
        {
            // set data 
            users = new Users
            {
                FullName = textBoxName.Text,
                UserName = textBoxUserName.Text,
                Password = textBoxPassword.Text,
                Email = textBoxEmail.Text,
                Phone = textBoxPhoneNumber.Text,
                AddedDate = DateTime.Now,

            };
            // submit
            var result = await dataHelper.AddAsync(users);
            if (result == 1)
            {
                // Add Roles
                var data = await dataHelper.GetAllDataAsync();
                var userid = data.Select(x => x.Id).LastOrDefault();
                SetRoles();
                // loap into list of roles
                for (int i = 0; i < ListOfRoles.Count; i++)
                {
                    UsersRoles usersRoles = new UsersRoles
                    {
                        UserId = userid,
                        Key = ListOfRoles.Keys.ToList()[i],
                        Value = ListOfRoles.Values.ToList()[i],
                    };
                    await dataHelperUsersRole.AddAsync(usersRoles);
                }

                // save system records 
                SystemRecords systemrecords = new SystemRecords
                {
                    Title = " اضافة مستخدم",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تمت اضافة مستخدم " + users.UserName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemrecords);
                userControl.LoadData();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetRoles()
        {
            ListOfRoles.Clear();
            ListOfRoles.Add(checkBoxHome.Name, checkBoxHome.Checked);
            ListOfRoles.Add(checkBoxElementInfo.Name, checkBoxElementInfo.Checked);
            ListOfRoles.Add(checkBoxElementAddInfo.Name, checkBoxElementAddInfo.Checked);
            ListOfRoles.Add(checkBoxFollowElement.Name, checkBoxFollowElement.Checked);
            ListOfRoles.Add(checkBoxUser.Name, checkBoxUser.Checked);
            ListOfRoles.Add(checkBoxSetting.Name, checkBoxSetting.Checked);
            ListOfRoles.Add(checkBoxSystemRecord.Name, checkBoxSystemRecord.Checked);

            //
            ListOfRoles.Add(checkBoxAccessElement.Name, checkBoxAccessElement.Checked);
            ListOfRoles.Add(checkBoxAccessElementAddInfo.Name, checkBoxAccessElementAddInfo.Checked);
            ListOfRoles.Add(checkBoxAccessUser.Name, checkBoxAccessUser.Checked);
            ListOfRoles.Add(checkBoxAccessElementFollowbroken.Name, checkBoxAccessElementFollowbroken.Checked);

            //
            ListOfRoles.Add(checkBoxAdd.Name, checkBoxAdd.Checked);
            ListOfRoles.Add(checkBoxEdit.Name, checkBoxEdit.Checked);
            ListOfRoles.Add(checkBoxDelete.Name, checkBoxDelete.Checked);
            ListOfRoles.Add(checkBoxExport.Name, checkBoxExport.Checked);
            ListOfRoles.Add(checkBoxSearch.Name, checkBoxSearch.Checked);
            ListOfRoles.Add(checkBoxExplore.Name, checkBoxExplore.Checked);
            ListOfRoles.Add(checkBoxprint.Name, checkBoxprint.Checked);
            ListOfRoles.Add(checkBoxEditFollowDate.Name, checkBoxEditFollowDate.Checked);
        }

        private async Task <bool> EditData()
        {

            // set data 
            users = new Users
            {
                Id = ID,
                FullName = textBoxName.Text,
                UserName = textBoxUserName.Text,
                Password = textBoxPassword.Text,
                Email = textBoxEmail.Text,
                Phone = textBoxPhoneNumber.Text,
                AddedDate = DateTime.Now,

            };
            // submit
            var result = await dataHelper.EditAsync(users);
            if (result == 1)
            {
                // Add Role
                var rolesData = await dataHelperUsersRole.GetAllDataAsync();
                var ListOfRolesId = rolesData.Where(x => x.UserId == ID).Select(x => x.Id).ToList();
                // loop into listofrolesid ==> Delete
                for (int j = 0; j < ListOfRolesId.Count; j++)
                {
                    var userid = ListOfRolesId[j];
                    await dataHelperUsersRole.DeleteAsync(userid);
                }

                SetRoles();
                // loap into list of roles
                for (int i = 0; i < ListOfRoles.Count; i++)
                {
                    UsersRoles usersRoles = new UsersRoles
                    {
                        UserId = ID,
                        Key = ListOfRoles.Keys.ToList()[i],
                        Value = ListOfRoles.Values.ToList()[i],
                    };
                    await dataHelperUsersRole.AddAsync(usersRoles);
                }
                // save system records 
                SystemRecords systemrecords = new SystemRecords
                {
                    Title = " تعديل مستخدم",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تمت تعديل مستخدم " + users.UserName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemrecords);
                //Toast 
                userControl.LoadData();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private  void AddCategoryForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyDialogLayout(this);
            loadingForm.Show();
            SetFieldsToData();
            loadingForm.Hide();
            if (firstStart == true)
            {
                buttonSave.Visible = false;
            }
        }

        private async void SetFieldsToData()
        {
            if (ID > 0)
            {
                // set field 
                users = await dataHelper.FindAsync(ID);
                var rolesData = await dataHelperUsersRole.GetAllDataAsync();
                var ListOfRoles = rolesData.Where(x => x.UserId == ID).Select(x => x.Value).ToList();

                if (users != null)
                {
                    textBoxName.Text = users.FullName;
                    textBoxUserName.Text = users.UserName;
                    textBoxPassword.Text = users.Password;
                    textBoxEmail.Text = users.Email;
                    textBoxPhoneNumber.Text = users.Phone;

                    // set Current roles
                    checkBoxHome.Checked= ListOfRoles[0];
                    checkBoxElementInfo.Checked = ListOfRoles[1];
                    checkBoxElementAddInfo.Checked = ListOfRoles[2];
                    checkBoxFollowElement.Checked = ListOfRoles[3];
                    checkBoxUser.Checked = ListOfRoles[4];
                    checkBoxSetting.Checked = ListOfRoles[5];
                    checkBoxSystemRecord.Checked = ListOfRoles[6];

                    checkBoxAccessElement.Checked = ListOfRoles[7];
                    checkBoxAccessElementAddInfo.Checked = ListOfRoles[8];
                    checkBoxAccessUser.Checked = ListOfRoles[9];
                    checkBoxAccessElementFollowbroken.Checked = ListOfRoles[10];

                    checkBoxAdd.Checked = ListOfRoles[11];
                    checkBoxEdit.Checked = ListOfRoles[12];
                    checkBoxDelete.Checked = ListOfRoles[13];
                    checkBoxExport.Checked = ListOfRoles[14];
                    checkBoxSearch.Checked = ListOfRoles[15];
                    checkBoxExplore.Checked = ListOfRoles[16];
                    checkBoxprint.Checked = ListOfRoles[17];
                    checkBoxEditFollowDate.Checked = ListOfRoles[18];

                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
            }
        }

        #endregion

        private void AddUsersForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (firstStart == true)
            {
                
                Application.Exit();
            }
        }
    }

}
