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
using System.Drawing;
using System.Windows.Forms;

namespace Follow_Extremist.Gui.GuiUsers
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
        private List<Users> allUsers = new List<Users>();
        private GuiCommon.PaginationControl paginationControl;

        public UsersUserControl()
        {
            InitializeComponent();
            SetupPagination();
            SetRoles();
            dataHelper = (IDataHelper<Users>)ConfigurationObjectManager.GetObject("Users");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            //dataHelperIncome = (IDataHelper<Income>)ConfigurationObjectManager.GetObject("Income");
            //dataHelperOutcome = (IDataHelper<Outcome>)ConfigurationObjectManager.GetObject("Outcome");
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }

        private void SetupPagination()
        {
            if (comboBoxPageNo != null)
            {
                comboBoxPageNo.Visible = false;
                this.Controls.Remove(comboBoxPageNo);
            }
            paginationControl = new GuiCommon.PaginationControl();
            paginationControl.Dock = DockStyle.Bottom;
            paginationControl.PageChanged += (s, e) => BindCurrentPage();
            this.Controls.Add(paginationControl);
            paginationControl.BringToFront();
            dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int startIndex = paginationControl != null && !paginationControl.IsAll 
                ? (paginationControl.CurrentPage - 1) * paginationControl.PageSize 
                : 0;
            string rowIdx = (startIndex + e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, dataGridView1.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void BindCurrentPage()
        {
            var pageData = paginationControl.GetPageData(allUsers);
            dataGridView1.DataSource = pageData;
            SetColumnsTitle();
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

        private void comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        #endregion


        #region Methods 
        public static UsersUserControl Instance()
        {
            return _UsersUserControl ?? (new UsersUserControl());
        }

        public async void LoadData()
        {
            loadingForm.Show();
            try
            {
                var data = await dataHelper.GetAllDataAsync();
                allUsers = data ?? new List<Users>();
                var pageData = paginationControl.GetPageData(allUsers, resetToFirstPage: true);
                dataGridView1.DataSource = pageData;
                SetColumnsTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل المستخدمين: {ex.Message}");
            }
            finally
            {
                loadingForm.Hide();
            }
        }

        private void SetColumnsTitle()
        {
            if (dataGridView1.Columns == null || dataGridView1.Columns.Count == 0) return;

            if (dataGridView1.Columns.Contains("Id"))
                dataGridView1.Columns["Id"].HeaderText = "المعرف";

            if (dataGridView1.Columns.Contains("Name"))
                dataGridView1.Columns["Name"].HeaderText = "الاسم";

            if (dataGridView1.Columns.Contains("UserName"))
                dataGridView1.Columns["UserName"].HeaderText = "اسم المستخدم";

            if (dataGridView1.Columns.Contains("Password"))
                dataGridView1.Columns["Password"].HeaderText = "كلمة السر";

            if (dataGridView1.Columns.Contains("Email"))
                dataGridView1.Columns["Email"].HeaderText = "الايميل";

            if (dataGridView1.Columns.Contains("Phone"))
                dataGridView1.Columns["Phone"].HeaderText = "رقم الهاتف";

            if (dataGridView1.Columns.Contains("AddedDate"))
                dataGridView1.Columns["AddedDate"].HeaderText = "تاريخ الاضافة";

            dataGridView1.CellFormatting -= dataGridView1_CellFormatting;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
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
            try
            {
                SearchItem = textBoxSearch.Text.Trim();
                var data = await dataHelper.SearchAsync(SearchItem);
                allUsers = data ?? new List<Users>();
                var pageData = paginationControl.GetPageData(allUsers, resetToFirstPage: true);
                dataGridView1.DataSource = pageData;
                SetColumnsTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء البحث: {ex.Message}");
            }
            finally
            {
                loadingForm.Hide();
            }
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
