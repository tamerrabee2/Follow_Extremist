using Follow.Code;
using Follow.Core;
using Follow.Data;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow.Gui.GuiElementInfo
{
    public partial class ElementInfoControl : UserControl
    {
        // variables 
        private readonly IDataHelper<ElementInfo> dataHelper;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static ElementInfoControl _ElementInfoControl;
        private int RowId;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        

        public ElementInfoControl()
        {
            InitializeComponent();
            SetRoles();
            dataHelper = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }

        #region Events
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //AddElementForm addelementForm = new AddElementForm(0,this);
           // addelementForm.Show();
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
                                    Details = "تم حذف مستخدم رقم" + RowId.ToString(),
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
        public static ElementInfoControl Instance()
        {
            return _ElementInfoControl ?? (new ElementInfoControl());
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

            // Set header texts
            dataGridView1.Columns[0].HeaderText = "المعرف";
            dataGridView1.Columns[1].HeaderText = "اسم العنصر";
            dataGridView1.Columns[2].HeaderText = "الرقم القومي";
            dataGridView1.Columns[3].HeaderText = "اسم الام";
            dataGridView1.Columns[4].HeaderText = "المؤهل";
            dataGridView1.Columns[5].HeaderText = "الوظيفة";
            dataGridView1.Columns[6].HeaderText = "تاريخ الميلاد";
            dataGridView1.Columns[7].HeaderText = "محل الميلاد";
            dataGridView1.Columns[8].HeaderText = "صورة العنصر";
            dataGridView1.Columns[9].HeaderText = "صورة الرقم القومي";
            dataGridView1.Columns[10].HeaderText = "حالة المتابعة";
            dataGridView1.Columns[11].HeaderText = "سبب انهاء المتابعة";
            dataGridView1.Columns[12].HeaderText = "ملاحظات";
            dataGridView1.Columns[13].HeaderText = "تليفون";
            dataGridView1.Columns[14].HeaderText = "محمول";
            dataGridView1.Columns[15].HeaderText = "محمول 2";
            dataGridView1.Columns[16].HeaderText = "محمول 3";
            dataGridView1.Columns[17].HeaderText = "تاريخ بدء المتابعة";
            dataGridView1.Columns[18].HeaderText = "عدد ايام المتابعة";
            dataGridView1.Columns[19].HeaderText = "تاريخ المتابعة الجديد";
            dataGridView1.Columns[20].HeaderText = "العنوان";
            dataGridView1.Columns[21].HeaderText = "تاريخ المتابعة القادم";

            

            // Rearrange columns (example: moving columns[20] to be next to columns[1])
            dataGridView1.Columns[20].DisplayIndex = 2; // Position it right after columns[1]

            // Enable text wrapping for all cells
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }



            // Ensure vertical scrolling is enabled

            dataGridView1.ScrollBars = ScrollBars.Both;

            // Optionally, you may want to set the row height to fit the content
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (dataGridView1.Columns != null)
            {
                if (dataGridView1.Columns != null && dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column != null)
                        {
                            dataGridView1.Columns[0].Width = 50;
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
                //AddElementForm addUsersForm = new AddElementForm(RowId, this);
                //addUsersForm.Show();
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
            dataTable.Columns["ElementName"].SetOrdinal(1);
            dataTable.Columns["ElementName"].ColumnName = "اسم العنصر";
            dataTable.Columns["NationalId"].SetOrdinal(2);
            dataTable.Columns["NationalId"].ColumnName = "الرقم القومي";
            dataTable.Columns["MotherName"].SetOrdinal(3);
            dataTable.Columns["MotherName"].ColumnName = "اسم الام";
            dataTable.Columns["Qualification"].SetOrdinal(4);
            dataTable.Columns["Qualification"].ColumnName = "المؤهل";
            dataTable.Columns["Job"].SetOrdinal(5);
            dataTable.Columns["Job"].ColumnName = "الوظيفة";
            dataTable.Columns["BirthDate"].SetOrdinal(6);
            dataTable.Columns["BirthDate"].ColumnName = "تاريخ الميلاد";
            dataTable.Columns["BirthPlace"].SetOrdinal(7);
            dataTable.Columns["BirthPlace"].ColumnName = "محل الميلاد";
            dataTable.Columns["ElementImage"].SetOrdinal(8);
            dataTable.Columns["ElementImage"].ColumnName = "صورة العنصر";
            dataTable.Columns["NationalIdImage"].SetOrdinal(9);
            dataTable.Columns["NationalIdImage"].ColumnName = "صورة الرقم القومي";
            dataTable.Columns["FollowState"].SetOrdinal(10);
            dataTable.Columns["FollowState"].ColumnName = "حالة المتابعة";
            dataTable.Columns["ReasonEndFollow"].SetOrdinal(11);
            dataTable.Columns["ReasonEndFollow"].ColumnName = "سبب انهاء المتابعة";
            dataTable.Columns["Notes"].SetOrdinal(12);
            dataTable.Columns["Notes"].ColumnName = "ملاحظات";
            dataTable.Columns["Phone"].SetOrdinal(13);
            dataTable.Columns["Phone"].ColumnName = "رقم التليفون";
            dataTable.Columns["Mobile"].SetOrdinal(14);
            dataTable.Columns["Mobile"].ColumnName = "محمول";
            dataTable.Columns["Mobile2"].SetOrdinal(15);
            dataTable.Columns["Mobile2"].ColumnName = "محمول 2";
            dataTable.Columns["Mobile3"].SetOrdinal(16);
            dataTable.Columns["Mobile3"].ColumnName = "محمول 3";
            dataTable.Columns["DateFollowStart"].SetOrdinal(17);
            dataTable.Columns["DateFollowStart"].ColumnName = "تاريخ بداية المتابعة";
            dataTable.Columns["FollowDaysCount"].SetOrdinal(18);
            dataTable.Columns["FollowDaysCount"].ColumnName = "عدد الايام";
            dataTable.Columns["DateFollowNew"].SetOrdinal(19);
            dataTable.Columns["DateFollowNew"].ColumnName = "تاريخ المتابعة الجديد";
            dataTable.Columns["Address"].SetOrdinal(20);
            dataTable.Columns["Address"].ColumnName = "العنوان";
            dataTable.Columns["DateFollowNext"].SetOrdinal(21);
            dataTable.Columns["DateFollowNext"].ColumnName = "تاريخ المتابعة القادم";

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
             /*   try
                {
                    using (XLWorkbook xLWorkbook = new XLWorkbook())
                    {
                        xLWorkbook.AddWorksheet(dataTableArranged, "Data");
                        using (MemoryStream ma = new MemoryStream())
                        {
                            xLWorkbook.SaveAs(ma);
                            File.WriteAllBytes(saveFileDialog.FileName, ma.ToArray());
                        }
                    }
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }*/
            }
        }

        private void SetRoles()
        {
            if (!UsersRolesManager.GetRole("checkBoxAdd"))
            {
                buttonAdd.Visible = false;
            }

            if (!UsersRolesManager.GetRole("checkBoxEdit"))
            {
                buttonEdit.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxDelete"))
            {
                buttonDelete.Visible = false;
            }
           
            if (!UsersRolesManager.GetRole("checkBoxSearch"))
            {
                buttonSearch.Visible = false;
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
