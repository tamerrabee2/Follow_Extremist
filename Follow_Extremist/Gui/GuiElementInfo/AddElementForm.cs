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

namespace Follow_Extremist.Gui.GuiElementInfo
{
    public partial class AddElementForm : Form
    {
        #region variables 
        private readonly int ID;
        bool state;
        private readonly ElementInfoControl1 userControl;
        private bool isEditingFollowDateOnly;
        //private readonly ElementInfoControl userControl;
        private ElementInfo table;
        private ElementInfo existingData;
        private bool isEditMode;
        private readonly IDataHelper<ElementInfo> dataHelper;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        #endregion
        public AddElementForm(int Id, ElementInfoControl1 userControl, bool editFollowDateOnly, bool isEditMode)
        {
            InitializeComponent();
            dataHelper = (IDataHelper < ElementInfo >) ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.userControl = userControl;
            isEditingFollowDateOnly = editFollowDateOnly;
            textBoxName.KeyPress += textBoxName_KeyPress;
            textBoxNationalId.KeyPress += textBoxNationalId_KeyPress;
            textBoxNationalId.Leave += textBoxNationalId_Leave;
            dateTimePickerFollowNew.ValueChanged += dateTimePickerFollowNew_ValueChanged;
            this.isEditMode = isEditMode;
            groupBox4.Visible = false;
            if (isEditMode)
            {
                labelelementstatus.Enabled = true;
                checkBoxPrisoned.Enabled = true;
                checkBoxPrisoned.Visible = true;
                // Load existing data for editing
            }
            else
            {
                checkBoxPrisoned.Visible = false;
                labelelementstatus.Visible = false;
                labelelementstatus.Enabled = false;
                checkBoxPrisoned.Enabled = false;
            }
        }

        private void DisableAllControlsExceptDatePicker()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox || control is ComboBox || control is DateTimePicker || control is PictureBox || control is LinkLabel || control is RadioButton || control is RichTextBox)
                {
                    control.Enabled = false;
                }
                if (control is Panel || control is GroupBox || control is TabControl)
                {
                    DisableAllControlsExceptDatePicker(control);
                }
            }
            dateTimePickerFollowNew.Enabled = true;
            dateTimePickerFollowNew.ShowUpDown = false;
            textBoxDaysCount.Enabled = true;
            dateTimePickerFollowStartDate.Enabled = false;
        }

        private void DisableAllControlsExceptDatePicker(Control container)
        {
            foreach (Control control in container.Controls)
            {
                if (control is TextBox || control is ComboBox || control is DateTimePicker || control is PictureBox|| control is LinkLabel || control is RadioButton || control is RichTextBox)
                {
                    control.Enabled = false;
                }

                // Recursively disable controls in containers
                if (control is Panel || control is GroupBox || control is TabControl)
                {
                    DisableAllControlsExceptDatePicker(control);
                }
            }
            dateTimePickerFollowNew.Enabled = true;
            dateTimePickerFollowNew.ShowUpDown = false;
            textBoxDaysCount.Enabled = true;
            dateTimePickerFollowStartDate.Enabled = false;
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
                    clear();
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
            if (isEditingFollowDateOnly)
            {
                return string.IsNullOrWhiteSpace(textBoxDaysCount.Text);
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                string.IsNullOrWhiteSpace(textBoxNationalId.Text) ||
                string.IsNullOrWhiteSpace(textBoxDaysCount.Text) ||
                comboBoxStatusCase.SelectedItem == null)
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
            table = new ElementInfo
            { 
                ElementName = textBoxName.Text,//
                NationalId = textBoxNationalId.Text,//
                MotherName = textBoxMotherName.Text,//
                Qualification = textBoxQualification.Text,//
                Job = textBoxJob.Text,//
                BirthDate = dateTimePickerBirthDate.Value,//
                BirthPlace = textBoxBirthPlace.Text,//
                ElementImage = ConvertTobyteImageElement().Length > 0 ? ConvertTobyteImageElement() : null,//
                NationalIdImage = convertTobyteImagenationalID().Length > 0 ? convertTobyteImagenationalID() : null,
                FollowState = comboBoxStatusCase.SelectedItem.ToString(),//
                ReasonEndFollow = textBoxEndreason.Text,//
                Notes = textBoxNotes.Text,
                Phone = textBoxPhone.Text,
                Mobile = textBoxMobile.Text,
                Mobile2 = textBoxMobile2.Text,
                Mobile3 = textBoxMobile3.Text,
                DateFollowStart = dateTimePickerFollowStartDate.Value,
                FollowDaysCount = Convert.ToInt32(textBoxDaysCount.Text),
                DateFollowNow = dateTimePickerFollowStartDate.Value.AddDays(Convert.ToInt32(textBoxDaysCount.Text)),
                DateFollowNext = dateTimePickerFollowStartDate.Value.AddDays(Convert.ToInt32(textBoxDaysCount.Text) * 2),
                Address = textBoxAddress.Text,
                RegulatoryStatus = textBoxRegulatoryStatus.Text,
                FacebookAcount = textBoxFacebookAcount.Text,
                FacebookID = textBoxFacebookAcountId.Text,
                PrisonedOrnot = checkBoxPrisoned.Checked ? "محبوس" : "مفرج عنه",
                CaseData = richTextBoxCaseData.Text


                // PrisonedOrnot = radioButtonreleased.Checked ? radioButtonreleased.Text : radioButtonPrisoned.Text

            };
            // submit 
            var result = await dataHelper.AddAsync(table);
            if (result == 1)
            {
                // save system records 
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = "اضافة عنصر",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تم اضافة عنصر اسمه" + " " + table.ElementName + " معرف رقم" + " " + table.Id,
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
            if (existingData == null)
            {
                existingData = await dataHelper.FindAsync(ID);
            }

            if (isEditingFollowDateOnly)
            {
                int daysCount = int.TryParse(textBoxDaysCount.Text, out int dc) ? dc : (existingData?.FollowDaysCount ?? 15);
                table = new ElementInfo
                {
                    Id = ID,
                    ElementName = existingData.ElementName,//
                    NationalId = existingData.NationalId,//
                    BirthDate = existingData.BirthDate,//
                    BirthPlace = existingData.BirthPlace,//
                    Address = existingData.Address,
                    MotherName = existingData.MotherName,//
                    Phone = existingData.Phone,
                    Mobile = existingData.Mobile,
                    Mobile2 = existingData.Mobile2,
                    Mobile3 = existingData.Mobile3,
                    Qualification = existingData.Qualification,
                    Job = existingData.Job,
                    FollowState = existingData.FollowState,
                    ReasonEndFollow = existingData.ReasonEndFollow,
                    DateFollowStart = dateTimePickerFollowStartDate.Value.Date,
                    FollowDaysCount = daysCount,
                    DateFollowNow = dateTimePickerFollowNew.Value.Date,
                    ElementImage = existingData.ElementImage,
                    NationalIdImage = existingData.NationalIdImage,
                    Notes = existingData.Notes,
                    DateFollowNext = dateTimePickerFollowNew.Value.Date.AddDays(daysCount),
                    RegulatoryStatus = existingData.RegulatoryStatus,
                    FacebookAcount = existingData.FacebookAcount,
                    FacebookID = existingData.FacebookID,
                    PrisonedOrnot = existingData.PrisonedOrnot, 
                    CaseData = existingData.CaseData
                };
            }
            else
            {
                // Update DateFollowNow based on the state of checkBoxPrisoned
                DateTime newFollowNowDate;
                DateTime newFollowNext;
                if (existingData.PrisonedOrnot == "محبوس")
                {
                    if (!checkBoxPrisoned.Checked)
                    {
                        newFollowNowDate = DateTime.Now.AddDays(Convert.ToInt32(textBoxDaysCount.Text));
                        newFollowNext = newFollowNowDate.AddDays(Convert.ToInt32(textBoxDaysCount.Text));
                    }
                    else
                    {
                        newFollowNowDate = existingData.DateFollowNow;
                        newFollowNext = existingData.DateFollowNext;
                    }

                    // set data 
                    table = new ElementInfo
                    {
                        Id = ID,
                        ElementName = textBoxName.Text,//
                        NationalId = textBoxNationalId.Text,//
                        BirthDate = dateTimePickerBirthDate.Value,//
                        BirthPlace = textBoxBirthPlace.Text,//
                        Address = textBoxAddress.Text,
                        MotherName = textBoxMotherName.Text,//
                        Phone = textBoxPhone.Text,
                        Mobile = textBoxMobile.Text,
                        Mobile2 = textBoxMobile2.Text,
                        Mobile3 = textBoxMobile3.Text,
                        Qualification = textBoxQualification.Text,//
                        Job = textBoxJob.Text,//
                        FollowState = comboBoxStatusCase.SelectedItem.ToString(),//
                        ReasonEndFollow = textBoxEndreason.Text,//
                        DateFollowStart = existingData.DateFollowStart,
                        FollowDaysCount = existingData.FollowDaysCount,
                        DateFollowNow = newFollowNowDate,
                        ElementImage = ConvertTobyteImageElement().Length > 0 ? ConvertTobyteImageElement() : existingData.ElementImage,//
                        NationalIdImage = convertTobyteImagenationalID().Length > 0 ? convertTobyteImagenationalID() : existingData.NationalIdImage,
                        Notes = textBoxNotes.Text,
                        DateFollowNext = newFollowNext,
                        RegulatoryStatus = textBoxRegulatoryStatus.Text,
                        FacebookAcount = textBoxFacebookAcount.Text,
                        FacebookID = textBoxFacebookAcountId.Text,
                        PrisonedOrnot = checkBoxPrisoned.Checked ? "محبوس" : "مفرج عنه",
                        CaseData = richTextBoxCaseData.Text

                        //PrisonedOrnot = radioButtonreleased.Checked ? radioButtonreleased.Text : radioButtonPrisoned.Text
                    };
                }
                else
                {
                    // set data 
                    table = new ElementInfo
                    {
                        Id = ID,
                        ElementName = textBoxName.Text,//
                        NationalId = textBoxNationalId.Text,//
                        BirthDate = dateTimePickerBirthDate.Value,//
                        BirthPlace = textBoxBirthPlace.Text,//
                        Address = textBoxAddress.Text,
                        MotherName = textBoxMotherName.Text,//
                        Phone = textBoxPhone.Text,
                        Mobile = textBoxMobile.Text,
                        Mobile2 = textBoxMobile2.Text,
                        Mobile3 = textBoxMobile3.Text,
                        Qualification = textBoxQualification.Text,//
                        Job = textBoxJob.Text,//
                        FollowState = comboBoxStatusCase.SelectedItem.ToString(),//
                        ReasonEndFollow = textBoxEndreason.Text,//
                        DateFollowStart = existingData.DateFollowStart,
                        FollowDaysCount = existingData.FollowDaysCount,
                        DateFollowNow = existingData.DateFollowNow,
                        ElementImage = ConvertTobyteImageElement().Length > 0 ? ConvertTobyteImageElement() : existingData.ElementImage,//
                        NationalIdImage = convertTobyteImagenationalID().Length > 0 ? convertTobyteImagenationalID() : existingData.NationalIdImage,
                        Notes = textBoxNotes.Text,
                        DateFollowNext = existingData.DateFollowNext,
                        RegulatoryStatus = textBoxRegulatoryStatus.Text,
                        FacebookAcount = textBoxFacebookAcount.Text,
                        FacebookID = textBoxFacebookAcountId.Text,
                        PrisonedOrnot = checkBoxPrisoned.Checked ? "محبوس" : "مفرج عنه",
                        CaseData = richTextBoxCaseData.Text
                    };
                }
            }
            // submit 
            var result = await dataHelper.EditAsync(table);
            if (result == 1)
            {
                // save system records 
                SystemRecords systemRecords = new SystemRecords
                {
                    Title = "تعديل عنصر",
                    USerName = Properties.Settings.Default.UserName,
                    Details = "تم تعديل عنصر اسمه" + " "+ table.ElementName + " بمعرف رقم " + " " + table.Id,
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
            if (pictureBoxElementImage.Image != null)
            {
                using (MemoryStream ma = new MemoryStream())
                {
                    pictureBoxElementImage.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Png);
                    return ma.ToArray();
                }
            }
            else
            {
                return new byte[0];
            }
            
            
        }
        private byte[] convertTobyteImagenationalID()
        {
            if (pictureBoxElementID.Image != null)
            {
                using (MemoryStream ma = new MemoryStream())
                {
                    pictureBoxElementID.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Png);
                    return ma.ToArray();
                }
            }
            else
            {
                return new byte[0];
            }
            
           
        }

        private void clear()
        {
            textBoxName.Clear();
            textBoxNationalId.Clear();
            dateTimePickerBirthDate.Value = DateTime.Now;
            textBoxBirthPlace.Clear();
            textBoxAddress.Clear();
            textBoxQualification.Clear();
            textBoxJob.Clear();
            textBoxMotherName.Clear();
            textBoxNotes.Clear();
            textBoxPhone.Clear();
            textBoxMobile.Clear();
            textBoxMobile2.Clear();
            textBoxMobile3.Clear();
            comboBoxStatusCase.ResetText();
            comboBoxStatusCase.SelectedIndex = -1;
            textBoxEndreason.Clear();
            dateTimePickerFollowStartDate.Value = DateTime.Now;
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

        private async Task SetFieldTData()
        {
            if (ID >0)
            {
                // set field
                existingData = await dataHelper.FindAsync(ID);
                if (existingData != null)
                {
                   
                    textBoxName.Text = existingData.ElementName;
                    textBoxNationalId.Text = existingData.NationalId;
                    dateTimePickerBirthDate.Value = existingData.BirthDate;
                    textBoxBirthPlace.Text = existingData.BirthPlace;
                    textBoxAddress.Text = existingData.Address;
                    textBoxMotherName.Text = existingData.MotherName;
                    textBoxPhone.Text = existingData.Phone;
                    textBoxMobile.Text = existingData.Mobile;
                    textBoxMobile2.Text = existingData.Mobile2;
                    textBoxMobile3.Text = existingData.Mobile3;
                    textBoxQualification.Text = existingData.Qualification;
                    textBoxJob.Text = existingData.Job;
                    comboBoxStatusCase.SelectedItem = existingData.FollowState;
                    if (comboBoxStatusCase.SelectedItem == null && !string.IsNullOrEmpty(existingData.FollowState))
                    {
                        var trimmed = existingData.FollowState.Trim();
                        for (int i = 0; i < comboBoxStatusCase.Items.Count; i++)
                        {
                            if (comboBoxStatusCase.Items[i]?.ToString()?.Trim() == trimmed)
                            {
                                comboBoxStatusCase.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    textBoxEndreason.Text = existingData.ReasonEndFollow;
                    dateTimePickerFollowStartDate.Value = existingData.DateFollowStart;
                    textBoxDaysCount.Text = existingData.FollowDaysCount.ToString();
                    dateTimePickerFollowNew.Value = existingData.DateFollowNow;
                    // Handle the ElementImage
                    if (existingData.ElementImage != null && existingData.ElementImage.Length > 0)
                    {
                        using (MemoryStream ma = new MemoryStream(existingData.ElementImage))
                        {
                            pictureBoxElementImage.Image = Image.FromStream(ma);
                        }
                    }
                    else
                    {
                        pictureBoxElementImage.Image = null; // or set a default image
                    }

                    // Handle the NationalIdImage
                    if (existingData.NationalIdImage != null && existingData.NationalIdImage.Length > 0)
                    {
                        using (MemoryStream ma2 = new MemoryStream(existingData.NationalIdImage))
                        {
                            pictureBoxElementID.Image = Image.FromStream(ma2);
                        }
                    }
                    else
                    {
                        pictureBoxElementID.Image = null; // or set a default image
                    }

                    textBoxNotes.Text = existingData.Notes;
                    textBoxFacebookAcount.Text = existingData.FacebookAcount;
                    textBoxFacebookAcountId.Text = existingData.FacebookID;
                    textBoxRegulatoryStatus.Text = existingData.RegulatoryStatus;
                    checkBoxPrisoned.Checked = existingData.PrisonedOrnot == "محبوس";
                    richTextBoxCaseData.Text = existingData.CaseData;
                    //if (existingData.PrisonedOrnot == radioButtonreleased.Text)
                    //{
                    //    radioButtonreleased.Checked = true;
                    //    radioButtonPrisoned.Checked = false;
                    //}
                    //else if (existingData.PrisonedOrnot == radioButtonPrisoned.Text)
                    //{
                    //    radioButtonreleased.Checked = false;
                    //    radioButtonPrisoned.Checked = true;
                    //}
                    //else
                    //{
                    //    radioButtonreleased.Checked = false;
                    //    radioButtonPrisoned.Checked = false;
                    //}
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
            }
        }

        private async void AddElementForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyDialogLayout(this);
            loadingForm.Show();
            await SetFieldTData();
            if (isEditingFollowDateOnly)
            {
                DisableAllControlsExceptDatePicker();
            }
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

        private void textBoxNationalId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxStatusCase_SelectedValueChanged(object sender, EventArgs e)
        {
            var combobox = "داخل المتابعة ";
            if (comboBoxStatusCase.SelectedItem == combobox)
            {
                textBoxEndreason.Enabled = false;
            }
            else
            {
                textBoxEndreason.Enabled = true;
            }
        }

        private void linkLabelDataCases_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // AddCaseElementForm addCaseElementForm = new AddCaseElementForm(0,this);
           // addCaseElementForm.Show();
        }

        private bool isSyncingFollowDates = false;

        private void textBoxDaysCount_TextChanged(object sender, EventArgs e)
        {
            if (isSyncingFollowDates) return;
            if (int.TryParse(textBoxDaysCount.Text, out int days))
            {
                try
                {
                    isSyncingFollowDates = true;
                    dateTimePickerFollowNew.Value = dateTimePickerFollowStartDate.Value.AddDays(days);
                }
                finally
                {
                    isSyncingFollowDates = false;
                }
            }
        }

        private void dateTimePickerFollowNew_ValueChanged(object sender, EventArgs e)
        {
            if (isSyncingFollowDates) return;
            if (isEditingFollowDateOnly)
            {
                int diff = (dateTimePickerFollowNew.Value.Date - dateTimePickerFollowStartDate.Value.Date).Days;
                if (diff > 0)
                {
                    try
                    {
                        isSyncingFollowDates = true;
                        textBoxDaysCount.Text = diff.ToString();
                    }
                    finally
                    {
                        isSyncingFollowDates = false;
                    }
                }
            }
        }

        private bool CheckDuplicateData()
        {
            try
            {
                var data = dataHelper.GetAllData();
                var user = data.Where(x => x.NationalId == textBoxNationalId.Text).FirstOrDefault();
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
                //MessageBox.Show("خطأ , من فضلك تحقق من الرقم القومي");
            }
            return state;
        }

        private void textBoxNationalId_Leave(object sender, EventArgs e)
        {
            var checkduplicate = CheckDuplicateData();
            if (checkduplicate == true)
            {
                MessageBox.Show("يوجد رقم قومي مسجل من قبل. من فضلك ادخل رقم قومي اخر .");
                textBoxNationalId.Focus(); // Optionally, set focus back to the TextBox
            }
        }
    }

}
