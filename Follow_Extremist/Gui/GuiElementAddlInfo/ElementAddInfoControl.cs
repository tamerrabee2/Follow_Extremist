using Follow_Extremist.Code;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow_Extremist.Gui.GuiElementAddlInfo
{
    public partial class ElementAddInfoControl : UserControl
    {
        // variables 
        private readonly IDataHelper<ElementAddInfo> dataHelper;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static  ElementAddInfoControl _ElementInfoControl;
        private int RowId;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        

        public ElementAddInfoControl()
        {
            InitializeComponent();
            SetRoles();
            dataHelper = (IDataHelper<ElementAddInfo>)ConfigurationObjectManager.GetObject("ElementAddInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }

        #region Events
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddElementAddInfoForm addelementForm = new AddElementAddInfoForm(0,this);
            addelementForm.Show();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        private async void  buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                var Deleteresult = MessageCollections.ShowDeletDialog();
                if (Deleteresult)
                {
                    IdList.Clear();
                    SetIdRowForDelete();
                    loadingForm.Show();
                    if (IdList.Count>0)
                    {
                        
                        for (int i = 0; i < IdList.Count; i++)
                        {
                            RowId = IdList[i];
                            var result = await dataHelper.DeleteAsync(RowId);
                            if (result == 1)
                            {
                                // save system records 
                                SystemRecords systemrecords = new SystemRecords
                                {
                                    Title = "عملية حذف",
                                    USerName = Properties.Settings.Default.UserName,
                                    Details = "تم حذف عنصر رقم" + RowId.ToString(),
                                    AddedDate = DateTime.Now
                                };
                                await dataHelperSystemRecords.AddAsync(systemrecords);
                                MessageCollections.ShowDeleteNotification();
                            }
                            else
                            {
                                MessageCollections.ShowErrorServer();
                            }
                        }
                        LoadData();
                    }
                    else
                    {
                        MessageCollections.ShowRequiredDeleteRow();

                    }
                    loadingForm.Hide();
                } 
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();

            }
            
        }
 
        private async void buttonExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            // Convert List of Data to DataTable
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            using(var reader =FastMember.ObjectReader.Create(data))
            {
                dataTable.Load(reader);
            }
            loadingForm.Hide();
            // Re-Set Columns 
            DataTable dataTableArranged = SetDataTableColumns(dataTable);
            // Export Data as Sheet Excel 
            ExportAsXlsxFile(dataTableArranged);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditData();
        }

        private async void comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            var dataId = data.Select(x => x.Id).ToArray();
            int index = comboBoxPageNo.SelectedIndex;
            int indexNoOfRow = index * Properties.Settings.Default.DataGridViewRowNumber;
            dataGridView1.DataSource = data.Where(x => x.Id >= dataId[indexNoOfRow]).Take(Properties.Settings.Default.DataGridViewRowNumber).ToList();

            if (dataGridView1.DataSource == null)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
                SetColumnsTitle();
            }
            loadingForm.Hide();
        }
        #endregion


        #region Methods 
        public static ElementAddInfoControl Instance()
        {
            return _ElementInfoControl ?? (new ElementAddInfoControl());
        }

        public async void  LoadData()
        {
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            dataGridView1.DataSource = data.Take(Properties.Settings.Default.DataGridViewRowNumber).ToList();

            // Add No of page into combo box
            comboBoxPageNo.Items.Clear();
            double value = (Convert.ToDouble(data.Count) /Convert.ToDouble (Properties.Settings.Default.DataGridViewRowNumber));
            int NoOfPage =(int) Math.Round(value, MidpointRounding.AwayFromZero);
            for (int i = 0; i <NoOfPage;i++)
            {
                comboBoxPageNo.Items.Add(i);
            }
            if (dataGridView1.DataSource == null)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
                SetColumnsTitle();
            }
            loadingForm.Hide();
            data.Clear();
        }

        private void SetColumnsTitle()
        {

            dataGridView1.Columns[0].HeaderText = "المعرف";
            dataGridView1.Columns[1].HeaderText = "الاسم";
            dataGridView1.Columns[2].HeaderText = "اسم العنصر";
            dataGridView1.Columns[3].HeaderText = "الرقم القومي";
            dataGridView1.Columns[4].HeaderText = "صلة القرابة";
            dataGridView1.Columns[5].HeaderText = "السن";
            dataGridView1.Columns[6].HeaderText = "صورة";
            dataGridView1.Columns[7].HeaderText = "صورة الرقم القومي";

            //hide
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

            if (dataGridView1.Columns != null)
            {
                if (dataGridView1.Columns != null && dataGridView1.Columns.Count>0)
                {
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column != null)
                        {
                            column.Width = 200;
                        }
                    }
                }
            }
        }

        private void EditData()
        {
            if (dataGridView1.RowCount > 0)
            {
                // get Id
                RowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                AddElementAddInfoForm addUsersForm = new AddElementAddInfoForm(RowId, this);
                addUsersForm.Show();
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
        }

        private void SetIdRowForDelete()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    IdList.Add(Convert.ToInt32(row.Cells[0].Value));
                }
            }
        }

        public async void Search()
        {
            loadingForm.Show();
            SearchItem = textBoxSearch.Text;
            dataGridView1.DataSource = await dataHelper.SearchAsync(SearchItem);
            if (dataGridView1.DataSource == null)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
                SetColumnsTitle();
            }
            loadingForm.Hide();
        }

        private DataTable SetDataTableColumns(DataTable dataTable)
        {
            dataTable.Columns["Id"].SetOrdinal(0);
            dataTable.Columns["Id"].ColumnName = "المعرف";
            dataTable.Columns["NameRelationElement"].SetOrdinal(1);
            dataTable.Columns["NameRelationElement"].ColumnName = "الاسم";
            dataTable.Columns["ElementName"].SetOrdinal(2);
            dataTable.Columns["ElementName"].ColumnName = "اسم العنصر";
            dataTable.Columns["ElementRelationNationalID"].SetOrdinal(3);
            dataTable.Columns["ElementRelationNationalID"].ColumnName = "الرقم القومي";
            dataTable.Columns["Relationship"].SetOrdinal(4);
            dataTable.Columns["Relationship"].ColumnName = "صلة القرابة";
            dataTable.Columns["Age"].SetOrdinal(5);
            dataTable.Columns["Age"].ColumnName = "السن";
            dataTable.Columns["ElemmentRelationImage"].SetOrdinal(6);
            dataTable.Columns["ElemmentRelationImage"].ColumnName = "صورة العنصر";
            dataTable.Columns["ElementRelationNationalIDImage"].SetOrdinal(7);
            dataTable.Columns["ElementRelationNationalIDImage"].ColumnName = "صورة الرقم القومي";
            dataTable.Columns["ElementInfoId"].SetOrdinal(8);
            dataTable.Columns["ElementInfoId"].ColumnName = "ElementInfoId";

            // hide 
            dataTable.Columns.Remove("ElementInfo");
            dataTable.Columns.Remove("ElementInfoId");
            dataTable.AcceptChanges();
            return dataTable;
        }

        private void ExportAsXlsxFile(DataTable dataTableArranged)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "تصدير الملف على شكل اكسيل";
            saveFileDialog.DefaultExt = "Xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "Excel File (.Xlsx)|*.xlsx";
            saveFileDialog.RestoreDirectory = true;
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook xLWorkbook = new XLWorkbook())
                    {
                        var worksheet = xLWorkbook.Worksheets.Add("Data");
                        // add headers 
                        for (int i = 0; i < dataTableArranged.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = dataTableArranged.Columns[i].ColumnName;
                        }

                        // add data 
                        for (int i = 0; i < dataTableArranged.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataTableArranged.Columns.Count; j++)
                            {
                                if (dataTableArranged.Columns[j].DataType == typeof(byte[]))
                                {
                                    // add image to worksheet
                                    byte[] imageData = (byte[])dataTableArranged.Rows[i][j];
                                    if (imageData != null && imageData.Length>0)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageData))
                                        {
                                            var img = worksheet.AddPicture(ms).MoveTo(worksheet.Cell(i + 2, j + 1));
                                            img.Height = 50;
                                            img.Width = 50;
                                        }
                                    }
                                }
                                else
                                {
                                    worksheet.Cell(i + 2, j + 1).Value = dataTableArranged.Rows[i][j];
                                }
                            }
                        }

                        using (MemoryStream ma = new MemoryStream())
                        {
                            xLWorkbook.SaveAs(ma);
                            File.WriteAllBytes(saveFileDialog.FileName, ma.ToArray());
                        }
                    }
                    // Verify if the file was created successfully before attempting to open it
                    if (File.Exists(saveFileDialog.FileName))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", saveFileDialog.FileName);

                    }
                    else
                    {
                        MessageBox.Show("File creation failed.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
             
            }
        }

        private void SetRoles()
        {
            if (!UsersRolesManager.GetRole("checkBoxAdd"))
            {
                buttonAdd.Enabled = false;
            }

            if (!UsersRolesManager.GetRole("checkBoxEdit"))
            {
                buttonEdit.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxDelete"))
            {
                buttonDelete.Enabled = false;
            }
           
            if (!UsersRolesManager.GetRole("checkBoxSearch"))
            {
                buttonSearch.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxExport"))
            {
                buttonExport.Enabled = false;
            }
           
        }
        #endregion

        private async void CategoryUserControl_Leave(object sender, EventArgs e)
        {
        }

        private async void buttonExport_Click_1(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            // convert List of Data to DataTable
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            using (var reader = FastMember.ObjectReader.Create(data))
            {
                dataTable.Load(reader);
            }
            loadingForm.Hide();
            // Re-set columns 
            DataTable dataTableArranged = SetDataTableColumns(dataTable);
            //Export data as Sheet Excel 
            ExportAsXlsxFile(dataTableArranged);
        }
    }
}
