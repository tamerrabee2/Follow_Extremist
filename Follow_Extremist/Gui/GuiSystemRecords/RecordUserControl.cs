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

namespace Follow_Extremist.Gui.GuiSystemRecords
{
    public partial class RecordUserControl : UserControl
    {
        // variables 
        private readonly IDataHelper<SystemRecords> dataHelper;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static RecordUserControl _CategoryUserControl;
        private int RowId;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        private double Amount;
        private List<SystemRecords> allRecords = new List<SystemRecords>();
        private GuiCommon.PaginationControl paginationControl;

        public RecordUserControl()
        {
            InitializeComponent();
            SetupPagination();
            SetRoles();
            dataHelper = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
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
            var pageData = paginationControl.GetPageData(allRecords);
            dataGridView1.DataSource = pageData;
            SetColumnsTitle();
        }

        #region Events
    
        

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

        

        private void comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        #endregion


        #region Methods 
        public static RecordUserControl Instance()
        {
            return _CategoryUserControl ?? (new RecordUserControl());
        }

        public async void LoadData()
        {
            loadingForm.Show();
            try
            {
                var data = await dataHelper.GetAllDataAsync();
                allRecords = data?.OrderByDescending(x => x.Id).ToList() ?? new List<SystemRecords>();
                var pageData = paginationControl.GetPageData(allRecords, resetToFirstPage: true);
                dataGridView1.DataSource = pageData;
                SetColumnsTitle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل السجلات: {ex.Message}");
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

            if (dataGridView1.Columns.Contains("UserName"))
                dataGridView1.Columns["UserName"].HeaderText = "اسم المستخدم";

            if (dataGridView1.Columns.Contains("Title"))
                dataGridView1.Columns["Title"].HeaderText = "العنوان";

            if (dataGridView1.Columns.Contains("Details"))
                dataGridView1.Columns["Details"].HeaderText = "التفاصيل";

            if (dataGridView1.Columns.Contains("AddedDate"))
                dataGridView1.Columns["AddedDate"].HeaderText = "تاريخ الاضافة";
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
                allRecords = data?.OrderByDescending(x => x.Id).ToList() ?? new List<SystemRecords>();
                var pageData = paginationControl.GetPageData(allRecords, resetToFirstPage: true);
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
            dataTable.Columns["UserName"].SetOrdinal(1);
            dataTable.Columns["UserName"].ColumnName = "اسم المستخدم";
            dataTable.Columns["Title"].SetOrdinal(2);
            dataTable.Columns["Title"].ColumnName = "العنوان";
            dataTable.Columns["Details"].SetOrdinal(3);
            dataTable.Columns["Details"].ColumnName = "التفاصيل";
            dataTable.Columns["AddedDate"].SetOrdinal(4);
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

        #endregion

        private async void CategoryUserControl_Leave(object sender, EventArgs e)
        {
        }

        private void SetRoles()
        {
            
            if (!UsersRolesManager.GetRole("checkBoxDelete"))
            {
                buttonDelete.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxExport"))
            {
                buttonExport.Visible = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxSearch"))
            {
                buttonSearch.Visible = false;
            }
            
        }

    }
}
