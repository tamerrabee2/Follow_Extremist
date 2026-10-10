using Follow_Extremist.Gui.GuiElementInfo;
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
using System.IO;

namespace Follow_Extremist.Gui.GuiElementFollow
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
            // التحقق من حلول موعد المتابعة للعنصر
            var element = await dataHelperElementInfo.FindAsync(ElementInfoId);
            if (element != null)
            {
                if (element.DateFollowNow.Date > dateTimePickerelementFollow.Value.Date)
                {
                    MessageBox.Show($"لم يحن موعد متابعة العنصر ({element.ElementName}) بعد.\nموعد المتابعة المحدد هو: {element.DateFollowNow:yyyy/MM/dd}", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // التحقق من عدم تكرار المتابعة لنفس اليوم
            var allFollows = await dataHelper.GetAllDataAsync();
            var alreadyFollowed = allFollows?.Any(f => f.ElementInfoId == ElementInfoId && f.DateFollow.Date == dateTimePickerelementFollow.Value.Date) ?? false;
            if (alreadyFollowed)
            {
                MessageBox.Show("تم إدخال متابعة لهذا التاريخ من قبل لهذا العنصر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            // set data 
            table = new ElementFollowAdd
            {
                ElementName = comboBoxElement.SelectedItem.ToString(),
                DateFollow = dateTimePickerelementFollow.Value.Date,
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
                    Details = "تم اضافة متابعة عنصر اسمه " + table.ElementName,
                    AddedDate = DateTime.Now
                };
                await dataHelperSystemRecords.AddAsync(systemRecords);

                // تعديل ميعاد المتابعة القادم في جدول العناصر اتوماتيكياً
                if (element != null)
                {
                    int daysInterval = element.FollowDaysCount > 0 ? element.FollowDaysCount : 7;
                    element.DateFollowNow = dateTimePickerelementFollow.Value.Date.AddDays(daysInterval);
                    element.DateFollowNext = dateTimePickerelementFollow.Value.Date.AddDays(daysInterval * 2);
                    await dataHelperElementInfo.EditAsync(element);

                    await dataHelperSystemRecords.AddAsync(new SystemRecords
                    {
                        Title = "تعديل تاريخ متابعة لعنصر",
                        USerName = Properties.Settings.Default.UserName,
                        Details = $"تم تعديل ميعاد المتابعة القادم للعنصر {element.ElementName} إلى {element.DateFollowNow:yyyy/MM/dd}",
                        AddedDate = DateTime.Now
                    });
                }

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
            var allElements = await dataHelperElementInfo.GetAllDataAsync();
            var ListElement = allElements.Where(x => x.FollowState == null || x.FollowState.Trim() != "خارج المتابعة").ToList();
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
