using Follow.Gui.GuiElementInfo;
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
using System.IO;

namespace Follow.Gui.GuiElementFollow
{
    public partial class ElementFollowForm : Form
    {
        #region variables 
        private readonly int ID;
        private int ElementInfoId;
        private readonly ElementFollowUserControl1 userControl;
        private ElementFollowAdd table;
        private readonly IDataHelper<ElementFollowAdd> dataHelper;
        private readonly IDataHelper<ElementInfo> dataHelperElementInfo;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        #endregion
        public ElementFollowForm(int Id, ElementFollowUserControl1 userControl)
        {
            InitializeComponent();
            dataHelper = (IDataHelper < ElementFollowAdd >) ConfigurationObjectManager.GetObject("ElementFollowAdd");
            dataHelperElementInfo = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.userControl = userControl;
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
                    Close();
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
            // check fields is empty 
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
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
                loadingForm.Hide();
            }
        }
        #endregion

        #region Methods

        
        private bool IsFieldsEmpty()
        {
            if (comboBoxElement.SelectedItem== null)
                
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private async Task<bool> SaveData()
        {
            if (ID == 0) // add
            {
                var ElementInfoName = comboBoxElement.SelectedItem.ToString();
                await Task.Run(() => SetElementInfoId(ElementInfoName));
                return await AddData();
            }
            else // edit
            {
                var ElementInfoName = comboBoxElement.SelectedItem.ToString();
                await Task.Run(() => SetElementInfoId(ElementInfoName));
                return await EditData();
            }
        }

        private async Task<bool> AddData()
        {
            // set data 
            table = new ElementFollowAdd
            {
                ElementName = comboBoxElement.SelectedItem.ToString(),
                DateFollow = dateTimePickerelementFollow.Value,
                ElementInfoId = ElementInfoId,
            };
            // submit 
            var result = await dataHelper.AddAsync(table);
            if (result == 1)
            {
                // save system records 
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = "اضافة متابعة عنصر",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تم اضافة  متابعة عنصر اسمه" + table.ElementName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);
                userControl.LoadData();
                return true;
            }
            else
            {
                return false;
            }
        }


        private async Task<bool> EditData()
        {
            // set data 
            table = new ElementFollowAdd
            {
                Id = ID,
                ElementName = comboBoxElement.SelectedItem.ToString(),
                DateFollow = dateTimePickerelementFollow.Value,
                ElementInfoId = ElementInfoId,
            };
            // submit 
            var result = await dataHelper.EditAsync(table);
            if (result == 1)
            {
                // save system records 
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = "تعديل عنصر",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل عنصر اسمه" + table.ElementName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);
                userControl.LoadData();
                return true;
            }
            else
            {
                return false;
            }
        }


        
       


        #endregion

       

        

        private async void SetFieldTData()
        {
            // Get List of Element 
            var ListElement = await dataHelperElementInfo.GetAllDataAsync();
            comboBoxElement.DataSource = ListElement.Select(x => x.ElementName).ToList();// fill
            // Auto complete 
            AutoCompleteStringCollection autoCompleteString = new AutoCompleteStringCollection();
            autoCompleteString.AddRange(ListElement.Select(x => x.ElementName).ToArray());
            comboBoxElement.AutoCompleteCustomSource = autoCompleteString;
            ListElement.Clear();
            if (ID >0)
            {
                // set field
                table = await dataHelper.FindAsync(ID);
                if (table!= null)
                {
                    
                    comboBoxElement.SelectedItem = table.ElementName;
                    dateTimePickerelementFollow.Value = table.DateFollow;
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
            }
        }

        private void SetElementInfoId(string ElementInfoName)
        {
            ElementInfoId = dataHelperElementInfo.GetAllData().Where(x => x.ElementName == ElementInfoName)
                .Select(x => x.Id).First();
        }
        private void AddElementForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyDialogLayout(this);
            loadingForm.Show();
            SetFieldTData();
            loadingForm.Hide();
        }
    }

}
