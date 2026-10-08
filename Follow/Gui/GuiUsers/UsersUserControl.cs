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

namespace Follow.Gui.GuiUsers
{
    public partial class UsersUserControl : UserControl
    {
        // variables 
        private readonly IDataHelper<Users> dataHelper;
        //private readonly IDataHelper<Income> dataHelperIncome;
        //private readonly IDataHelper<Outcome> dataHelperOutcome;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static UsersUserControl _UsersUserControl;
        private int RowId;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        private double Amount;

        public UsersUserControl()
        {
            InitializeComponent();
            SetRoles();
            dataHelper = (IDataHelper<Users>)ConfigurationObjectManager.GetObject("Users");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            //dataHelperIncome = (IDataHelper<Income>)ConfigurationObjectManager.GetObject("Income");
            //dataHelperOutcome = (IDataHelper<Outcome>)ConfigurationObjectManager.GetObject("Outcome");
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }

        #region Events
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddUsersForm addUsersForm = new AddUsersForm(0,this,false);
            addUsersForm.Show();
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
        public static UsersUserControl Instance()
        {
            return _UsersUserControl ?? (new UsersUserControl());
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
            dataGridView1.Columns[2].HeaderText = "اسم المستخدم";
            dataGridView1.Columns[3].HeaderText = "كلمة السر";
            dataGridView1.Columns[4].HeaderText = "الايميل";
            dataGridView1.Columns[5].HeaderText = "رقم الهاتف";
            dataGridView1.Columns[6].HeaderText = "تاريخ الاضافة";
            // Set the CellFormatting event handler
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
        }

        private void EditData()
        {
            if (dataGridView1.RowCount > 0)
            {
                // get Id
                RowId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                AddUsersForm addUsersForm = new AddUsersForm(RowId, this,false);
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
            dataTable.Columns["Name"].SetOrdinal(1);
            dataTable.Columns["Name"].ColumnName = "الاسم";
            dataTable.Columns["Type"].SetOrdinal(2);
            dataTable.Columns["Type"].ColumnName = "النوع";
            dataTable.Columns["Details"].SetOrdinal(3);
            dataTable.Columns["Details"].ColumnName = "التفاصيل";
            dataTable.Columns["Balance"].SetOrdinal(4);
            dataTable.Columns["Balance"].ColumnName = "الرصيد";
            dataTable.Columns["AddedDate"].SetOrdinal(5);
            dataTable.Columns["AddedDate"].ColumnName = "تاريخ الاضافة";
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
                }
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Assuming the password column index is 3
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                // Mask the password with asterisks
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
