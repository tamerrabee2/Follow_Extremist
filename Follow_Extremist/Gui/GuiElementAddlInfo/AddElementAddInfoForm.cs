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
using System.Text.RegularExpressions;

namespace Follow_Extremist.Gui.GuiElementAddlInfo
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
            if (searchableElementDropDown.SelectedElement == null 
                || string.IsNullOrWhiteSpace(textBoxName.Text)
                || comboBoxRelationship.SelectedItem == null)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> SaveData()
        {
            if (searchableElementDropDown.SelectedElement != null)
            {
                ElementInfoId = searchableElementDropDown.SelectedElement.Id;
            }

            if (ID == 0) // add
            {
                return await AddData();
            }
            else // edit
            {
                return await EditData();
            }
        }

        private async Task<bool> AddData()
        {
            // set data 
            table = new ElementAddInfo
            {
                ElementName = searchableElementDropDown.SelectedElement?.ElementName ?? string.Empty,
                Relationship = comboBoxRelationship.SelectedItem?.ToString(),
                NameRelationElement = textBoxName.Text.Trim(),
                ElementRelationNationalID = textBoxNationalID.Text.Trim(),
                Age = textBoxAge.Text.Trim(),
                ElemmentRelationImage = ConvertTobyteImageElement(),
                ElementRelationNationalIDImage = convertTobyteImagenationalID(),
                ElementInfoId = searchableElementDropDown.SelectedElement?.Id ?? ElementInfoId,
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
                ElementName = searchableElementDropDown.SelectedElement?.ElementName ?? string.Empty,
                Relationship = comboBoxRelationship.SelectedItem?.ToString(),
                NameRelationElement = textBoxName.Text.Trim(),
                ElementRelationNationalID = textBoxNationalID.Text.Trim(),
                Age = textBoxAge.Text.Trim(),
                ElemmentRelationImage = ConvertTobyteImageElement(),
                ElementRelationNationalIDImage = convertTobyteImagenationalID(),
                ElementInfoId = searchableElementDropDown.SelectedElement?.Id ?? ElementInfoId,
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
            var allElements = await dataHelperElementInfo.GetAllDataAsync();
            var listElement = allElements?.Where(x => x.FollowState == null || x.FollowState.Trim() != "خارج المتابعة").ToList() ?? new List<ElementInfo>();
            searchableElementDropDown.SetElements(listElement);

            if (ID > 0)
            {
                // set field
                table = await dataHelper.FindAsync(ID);
                if (table != null)
                {
                    if (table.ElemmentRelationImage != null && table.ElemmentRelationImage.Length > 0)
                    {
                        using var ma = new MemoryStream(table.ElemmentRelationImage);
                        pictureBoxElementImage.Image = Image.FromStream(ma);
                    }
                    if (table.ElementRelationNationalIDImage != null && table.ElementRelationNationalIDImage.Length > 0)
                    {
                        using var ma2 = new MemoryStream(table.ElementRelationNationalIDImage);
                        pictureBoxElementID.Image = Image.FromStream(ma2);
                    }

                    var selected = listElement.FirstOrDefault(x => x.Id == table.ElementInfoId || x.ElementName == table.ElementName);
                    if (selected != null)
                    {
                        searchableElementDropDown.SelectedElement = selected;
                    }

                    comboBoxRelationship.SelectedItem = table.Relationship;
                    textBoxName.Text = table.NameRelationElement;
                    textBoxNationalID.Text = table.ElementRelationNationalID;
                    textBoxAge.Text = table.Age;
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
            }
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
