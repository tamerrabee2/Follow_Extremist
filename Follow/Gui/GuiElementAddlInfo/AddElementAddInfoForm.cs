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
using System.Text.RegularExpressions;

namespace Follow.Gui.GuiElementAddlInfo
{
    public partial class AddElementAddInfoForm : Form
    {
        #region variables 
        private readonly int ID;
        private int ElementInfoId;
        private readonly ElementAddInfoControl userControl;
        private ElementAddInfo table;
        private readonly IDataHelper<ElementAddInfo> dataHelper;
        private readonly IDataHelper<ElementInfo> dataHelperElementInfo;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        #endregion
        public AddElementAddInfoForm(int Id, ElementAddInfoControl userControl)
        {
            InitializeComponent();
            dataHelper = (IDataHelper < ElementAddInfo >) ConfigurationObjectManager.GetObject("ElementAddInfo");
            dataHelperElementInfo = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.userControl = userControl;
            textBoxName.KeyPress += textBoxName_KeyPress;
            textBoxNationalID.KeyPress += textBoxNationalID_KeyPress;
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
            if (comboBoxElement.SelectedItem== null|| textBoxName.Text == string.Empty
                ||comboBoxRelationship.SelectedItem == null)
                
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
            table = new ElementAddInfo
            {
                ElementName = comboBoxElement.SelectedItem.ToString(),
                Relationship = comboBoxRelationship.SelectedItem.ToString(),
                NameRelationElement = textBoxName.Text,//
                ElementRelationNationalID = textBoxNationalID.Text,//
                Age = textBoxAge.Text,//
                ElemmentRelationImage = ConvertTobyteImageElement(),//
                ElementRelationNationalIDImage = convertTobyteImagenationalID(),
                ElementInfoId = ElementInfoId,
            };
            // submit 
            var result = await dataHelper.AddAsync(table);
            if (result == 1)
            {
                // save system records 
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = "اضافة بيانات اضافية",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تم اضافة عنصر اسمه" + table.NameRelationElement,
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
            table = new ElementAddInfo
            {
                Id = ID,
                ElementName = comboBoxElement.SelectedItem.ToString(),
                Relationship = comboBoxRelationship.SelectedItem.ToString(),
                NameRelationElement = textBoxName.Text,//
                ElementRelationNationalID = textBoxNationalID.Text,//
                Age = textBoxAge.Text,//
                ElemmentRelationImage = ConvertTobyteImageElement(),//
                ElementRelationNationalIDImage = convertTobyteImagenationalID(),
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
                    Details = "تم تعديل عنصر اسمه" + table.NameRelationElement,
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


        private byte[] ConvertTobyteImageElement()
        {
            MemoryStream ma = new MemoryStream();
            pictureBoxElementImage.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Png);
            return ma.ToArray();
        }
        private byte[] convertTobyteImagenationalID()
        {
            MemoryStream ma = new MemoryStream();
            pictureBoxElementID.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Png);
            return ma.ToArray();
        }


        #endregion

        private void linkLabelElementImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "اختر صورة العنصر";
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBoxElementImage.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void linkLabelElementID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "اختر صورة الرقم القومي";
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pictureBoxElementID.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

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
                    MemoryStream ma = new MemoryStream(table.ElemmentRelationImage);
                    MemoryStream ma2 = new MemoryStream(table.ElementRelationNationalIDImage);
                    comboBoxElement.SelectedItem = table.ElementName;
                    comboBoxRelationship.SelectedItem = table.Relationship;
                    textBoxName.Text = table.NameRelationElement;
                    textBoxNationalID.Text = table.ElementRelationNationalID;
                    textBoxAge.Text = table.Age;
                    pictureBoxElementImage.Image = Image.FromStream(ma);
                    pictureBoxElementID.Image = Image.FromStream(ma2);
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

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsWhiteSpace(e.KeyChar) || e.KeyChar == '\b') &&
                !Regex.IsMatch(e.KeyChar.ToString(), @"\p{IsArabic}"))
            {
                e.Handled = true;
            }
        }

        private void textBoxNationalID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

}
