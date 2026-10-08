using Follow.Code;
using Follow.Core;
using Follow.Data;
using System;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Columns;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace Follow.Gui.GuiElementFollow
{
    public partial class ElementFollowUserControl1 : UserControl
    {
        // Variables 
        private readonly IDataHelper<ElementFollowAdd> dataHelper;
        private readonly IDataHelper<ElementInfo> dataHelperElementInfo;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private static ElementFollowUserControl1 elementFollowUserControl1;
        private int RowId;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;

        public ElementFollowUserControl1()
        {
            InitializeComponent();
            dataHelper = (IDataHelper<ElementFollowAdd>)ConfigurationObjectManager.GetObject("ElementFollowAdd");
            dataHelperElementInfo = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            SetRoles();
            _ = LoadDataDate(DateTime.Now.Date);
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            buttonSearch.Click += buttonSearch_Click;
        }
        #region lodadData in gridview 
        public async void LoadData()
        {
            loadingForm.Show();

            try
            {
                // Fetch data asynchronously using IDataHelper
                var data = await dataHelperElementInfo.GetAllDataAsync();
                gridControl1.DataSource = data.ToList();

                if (gridControl1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitleElement1();
                }
                loadingForm.Hide();
                data.Clear();
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public async Task LoadDataDate(DateTime dateTime)
        {
            loadingForm.Show();
            try
            {
                // Format the date as a string to use with the search method
                SearchItem = dateTime.ToString("yyyy-MM-dd");
                string search = "مفرج عنه";
                string followstate = "داخل المتابعة ";
                // Fetch data asynchronously using IDataHelper
                var data = (await dataHelperElementInfo.SearchAsync(SearchItem)).Where(x => x.DateFollowNow.Date == dateTime.Date).ToList();
                var data1 = data.Where(x => x.PrisonedOrnot == search || x.PrisonedOrnot == null).ToList();
                var data2 = data1.Where(x => x.FollowState == followstate).ToList();

                if (data2 == null || data2.Count == 0)
                {
                    loadingForm.Hide();
                    SetColumnsTitleElement1();
                    MessageBox.Show("لا يوجد بيانات لاظهارها بتاريخ اليوم", "تنويه ", MessageBoxButtons.OK);
                    return;
                }
                //Debug.WriteLine($"Fetched {data.Count} records from dataHelperElementInfo.");

                // Clear existing columns
                var view = gridControl1.MainView as GridView;
                if (view != null)
                {
                    view.Columns.Clear();
                }




                // Set the binding list as the data source for the grid control
                gridControl1.DataSource = data2.ToList();

                if (gridControl1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitleElement1();
                }
                data2.Clear();
                data1.Clear();
                data.Clear();
            }
            catch
            {
                // Handle exceptions if needed
                // MessageBox.Show("حدث خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingForm.Hide();
            }
        }
        #region Methods gridview
        private void SetColumnsTitleElement()
        {
            // Set column titles and adjust settings
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.Columns["Id"].Caption = "المعرف";
                view.Columns["ElementName"].Caption = "اسم العنصر";
                view.Columns["NationalId"].Caption = "الرقم القومي";
                view.Columns["MotherName"].Caption = "اسم الام";
                view.Columns["Qualification"].Caption = "المؤهل";
                view.Columns["Job"].Caption = "الوظيفة";
                view.Columns["BirthDate"].Caption = "تاريخ الميلاد";
                view.Columns["BirthPlace"].Caption = "محل الميلاد";
                view.Columns["ElementImage"].Caption = "صورة العنصر";
                view.Columns["NationalIdImage"].Caption = "صورة الرقم القومي";
                view.Columns["FollowState"].Caption = "حالة المتابعة";
                view.Columns["ReasonEndFollow"].Caption = "سبب انهاء المتابعة";
                view.Columns["Notes"].Caption = "ملاحظات";
                view.Columns["Phone"].Caption = "تليفون";
                view.Columns["Mobile"].Caption = "محمول";
                view.Columns["Mobile2"].Caption = "محمول 2";
                view.Columns["Mobile3"].Caption = "محمول 3";
                view.Columns["DateFollowStart"].Caption = "تاريخ بدء المتابعة";
                view.Columns["FollowDaysCount"].Caption = "عدد ايام المتابعة";
                view.Columns["DateFollowNow"].Caption = "تاريخ المتابعة الجديد";
                view.Columns["Address"].Caption = "العنوان";
                view.Columns["DateFollowNext"].Caption = "تاريخ المتابعة القادم";
                view.Columns["RegulatoryStatus"].Caption = "الوضع التنظيمي";
                view.Columns["FacebookAcount"].Caption = "الفيسبوك";
                view.Columns["FacebookID"].Caption = "معرف الفيسبوك";
                view.Columns["PrisonedOrnot"].Caption = "حالة العنصر";



                // Hide specific columns
                view.Columns["MotherName"].Visible = false;
                view.Columns["Qualification"].Visible = false;
                view.Columns["Job"].Visible = false;
                view.Columns["BirthPlace"].Visible = false;
                view.Columns["ElementImage"].Visible = false;
                view.Columns["NationalIdImage"].Visible = false;
                view.Columns["FollowState"].Visible = false;
                view.Columns["ReasonEndFollow"].Visible = false;
                view.Columns["Phone"].Visible = false;
                view.Columns["Mobile"].Visible = false;
                view.Columns["Mobile2"].Visible = false;
                view.Columns["Mobile3"].Visible = false;
                view.Columns["RegulatoryStatus"].Visible = false;
                view.Columns["FacebookAcount"].Visible = false;
                view.Columns["FacebookID"].Visible = false;
                view.Columns["PrisonedOrnot"].Visible = false;
                view.Columns["NationalId"].Visible = false;
                view.Columns["DateFollowStart"].Visible = false;
                view.Columns["FollowDaysCount"].Visible = false;



                // Set column width
                view.OptionsView.ColumnAutoWidth = false;
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
                {
                    column.Width = 250;
                }
            }
        }
        #endregion

        #endregion
        public static ElementFollowUserControl1 Instance()
        {
            return elementFollowUserControl1 ?? (new ElementFollowUserControl1());
        }

        #region Methods AddData ElementFollow
        private List<ElementInfo> GetSelectedRowsData()
        {
            var view = gridControl1.MainView as GridView;
            if (view == null) return new List<ElementInfo>();

            var SelectedRows = view.GetSelectedRows();
            var SelectedData = new List<ElementInfo>();

            foreach (var rowHandle in SelectedRows)
            {
                if (view.IsDataRow(rowHandle))
                {
                    var rowData = view.GetRow(rowHandle) as ElementInfo;
                    if (rowData != null)
                    {
                        SelectedData.Add(rowData);
                    }
                }
            }
            return SelectedData;
        }

        private async Task AddOrUpdateDataAsync(List<ElementInfo> SelectedData, DateTime dateTime)
        {
            loadingForm.Show();
            try
            {
                foreach (var elementinfo in SelectedData)
                {
                    var existingElementInfo = await dataHelperElementInfo.FindAsync(elementinfo.Id);
                    if (existingElementInfo != null)
                    {
                        // Check if DateFollowNow is today's date in ElementInfo
                        if (existingElementInfo.DateFollowNow.Date == DateTime.Now.Date)
                        {
                            // Now check if there's already a follow record for today's date in ElementFollowAdd
                            var existingFollowAdd = (await dataHelper.GetAllDataAsync())
                                                    .FirstOrDefault(x => x.DateFollow == DateTime.Now.Date && x.ElementInfoId == elementinfo.Id);
                            if (existingFollowAdd != null)
                            {
                                MessageBox.Show("تم ادخال المتابعة من قبل");
                                continue;
                            }

                            // If no existing follow record, add a new follow record
                            var newFollowRecord = new ElementFollowAdd
                            {
                                ElementName = elementinfo.ElementName,
                                DateFollow = DateTime.Now.Date,
                                ElementInfoId = elementinfo.Id
                            };
                            var result = await dataHelper.AddAsync(newFollowRecord);
                            if (result == 1)
                            {
                                // Save system records
                                SystemRecords systemRecords = new SystemRecords
                                {
                                    Title = "اضافة متابعة لعنصر",
                                    USerName = Properties.Settings.Default.UserName,
                                    Details = "تمت اضافة متابعة لعنصر" + " " + newFollowRecord.ElementName,
                                    AddedDate = DateTime.Now
                                };
                                await dataHelperSystemRecords.AddAsync(systemRecords);
                            }
                            // Update existing element info
                            existingElementInfo.DateFollowNow = DateTime.Now.Date.AddDays(existingElementInfo.FollowDaysCount);
                            existingElementInfo.DateFollowNext = DateTime.Now.Date.AddDays((existingElementInfo.FollowDaysCount) * 2);
                            var result2 = await dataHelperElementInfo.EditAsync(existingElementInfo);
                            if (result2 == 1)
                            {
                                // Save system records
                                SystemRecords systemRecords = new SystemRecords
                                {
                                    Title = "تعديل تاريخ متابعة لعنصر",
                                    USerName = Properties.Settings.Default.UserName,
                                    Details = "تمت تعديل تاريخ متابعة لعنصر" + " " + newFollowRecord.ElementName,
                                    AddedDate = DateTime.Now
                                };
                                await dataHelperSystemRecords.AddAsync(systemRecords);
                            }
                        }
                        else
                        {
                            MessageBox.Show("لا يمكن ادخال متابعة لا تساوي تاريخ اليوم");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                loadingForm.Hide();
            }
        }
        #endregion

        private void SetRoles()
        {
            if (!UsersRolesManager.GetRole("checkBoxAdd"))
            {
                buttonAdd.Enabled = false;
            }

            if (!UsersRolesManager.GetRole("checkBoxExport"))
            {
                buttonExport.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxprint"))
            {
                buttonPrint.Enabled = false;
            }

            if (!UsersRolesManager.GetRole("checkBoxSearch"))
            {
                buttonSearch.Enabled = false;
            }

        }


        #region Methods export to excel 

        private async void ExportGridControlToExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "تصدير الملف على شكل اكسيل ",
                DefaultExt = "xlsx",
                AddExtension = true,
                Filter = "Excel File (.xlsx)|*.xlsx",
                RestoreDirectory = true
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    var allData = await dataHelperElementInfo.GetAllDataAsync();
                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Data");
                        
                        var view = gridControl1.MainView as GridView;
                        if (view == null)
                        {
                            MessageBox.Show("Failed to retrieve the grid view.");
                            return;
                        }
                        // set the headers 
                        int colIndex = 1;
                        foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
                        {
                            if (column.Visible)
                            {
                                worksheet.Cell(1, colIndex).Value = column.Caption;
                                colIndex++;
                            }
                        }

                        // set the data 
                        for (int i = 0; i < allData.Count; i++)
                        {
                            colIndex = 1;
                            foreach (DevExpress.XtraGrid.Columns.GridColumn column  in view.Columns)
                            {
                                if (column.Visible)
                                {
                                    var value = allData[i].GetType().GetProperty(column.FieldName).GetValue(allData[i], null);
                                    worksheet.Cell(i + 2, colIndex).Value = value;
                                    colIndex++;
                                }
                            }
                        }

                        workbook.SaveAs(saveFileDialog.FileName);
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

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"يوجد خطا ما : {ex.Message }");
                }
            }
        }
        
        #endregion

        #region events buttons
        private async void  comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadingForm.Show();
            try
            {
                int index = comboBoxPageNo.SelectedIndex;
                int indexNoOfRow = index * Properties.Settings.Default.DataGridViewRowNumber;
                List<ElementInfo> data;
                if (!string.IsNullOrEmpty(SearchItem)) // Assuming isSearching is a flag indicating if a search is active
                {
                    data = await dataHelperElementInfo.SearchAsync(SearchItem);
                }
                else
                {
                    data = await dataHelperElementInfo.GetAllDataAsync();
                }

                int noOfPage = (int)Math.Ceiling((double)data.Count / Properties.Settings.Default.DataGridViewRowNumber);
                // Load data for the current page
                var pageData = data.Skip(indexNoOfRow).Take(Properties.Settings.Default.DataGridViewRowNumber).ToList();
                gridControl1.DataSource = pageData;
                if (gridControl1.DataSource == null || pageData.Count == 0)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitleElement();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                loadingForm.Hide();
            }
            
        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now.Date;
            var selectedData = GetSelectedRowsData();
           
            if (selectedData.Count > 0)
            {
                await AddOrUpdateDataAsync(selectedData, date);
                MessageBox.Show($"تم اضافة عدد متابعات بنجاح: {selectedData.Count}");
                gridControl1.DataSource = null;
                await LoadDataDate(date);
                SetColumnsTitleElement1();

            }
            else
            {
                MessageCollections.ShowErrorServer();
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private  void buttonExport_Click(object sender, EventArgs e)
        {
            ExportGridControlToExcel();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            var gridView = gridControl1.MainView as GridView;
            // Ensure gridView is not null and has rows
            if (gridView != null && gridView.RowCount > 0)
            {
                var data = (IEnumerable<ElementInfo>)gridControl1.DataSource;
                var dateTime = dateTimePicker1.Value.Date;
                var arabicCulture = new CultureInfo("ar-SA");
                var date = dateTime.ToString("dddd", arabicCulture);
                ShowReport(data, dateTime, date);
            }
            else
            {
                MessageBox.Show("لا يوجد بيانات متاحة لاظهارها في التقرير.", "لا يوجد بيانات", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // gridView1.ShowPrintPreview();
        }
        private void ShowReport(IEnumerable<ElementInfo> data, DateTime reportDate, string date)
        {
            var report = new Gui.GuiReport.GuiReportElementFollow.ReportElementFollow();

            // Set the report parameter 
            report.Parameters["ReportDate"].Value = reportDate;
            report.Parameters["ReportDate"].Visible = false; // Hide the parameter if you don't want it to be visible
            report.Parameters["Dayofweek"].Value = date;
            report.Parameters["Dayofweek"].Visible = false;
            // Bind data to the report
            report.BindData(data);
            
            // assign the report to the documentviewer 
            var documentviewer = new Follow.Gui.GuiReport.ReportForm();
            documentviewer.documentViewer1.DocumentSource = report;
            report.CreateDocument();
            documentviewer.Show();
        }
        #endregion

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;
            gridControl1.DataSource = null;
           // Debug.WriteLine($"Searching for records with DateFollowNow matching {selectedDate.ToShortDateString()}.");
            _=LoadDataDate(selectedDate);
        }

        private void SetColumnsTitleElement1()
        {
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                // Clear existing columns to avoid duplicates
                view.Columns.Clear();

                // Create columns manually (Unbound Mode)
                // 1. Auto-numeric column
                var autoNumericColumn = new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Name = "AutoNumericColumn",
                    FieldName = "AutoNumericColumn",
                    Caption = "مسلسل",
                    Visible = true,
                    VisibleIndex = 0,
                    Width = 100,
                    UnboundDataType = typeof(int)
                };
                view.Columns.Add(autoNumericColumn);

                // 2. Element Name column
                var elementNameColumn = new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Name = "ElementName",
                    FieldName = "ElementName",
                    Caption = "اسم العنصر",
                    Visible = true,
                    VisibleIndex = 1,
                    Width = 400,
                    UnboundDataType = typeof(string)
                };
                view.Columns.Add(elementNameColumn);

                // 3. Birth Date column
                var birthDateColumn = new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Name = "BirthDate",
                    FieldName = "BirthDate",
                    Caption = "تاريخ الميلاد",
                    Visible = true,
                    VisibleIndex = 2,
                    Width = 200,
                    UnboundDataType = typeof(DateTime)
                };
                view.Columns.Add(birthDateColumn);

                // 4. Address column
                var addressColumn = new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Name = "Address",
                    FieldName = "Address",
                    Caption = "العنوان",
                    Visible = true,
                    VisibleIndex = 3,
                    Width = 400,
                    UnboundDataType = typeof(string)
                };
                view.Columns.Add(addressColumn);

                // 5. Notes column
                var notesColumn = new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Name = "Notes",
                    FieldName = "Notes",
                    Caption = "ملاحظات",
                    Visible = true,
                    VisibleIndex = 4,
                    Width = 400,
                    UnboundDataType = typeof(string)
                };
                view.Columns.Add(notesColumn);

                // 6. Date Follow Next column
                var dateFollowNextColumn = new DevExpress.XtraGrid.Columns.GridColumn
                {
                    Name = "DateFollowNext",
                    FieldName = "DateFollowNext",
                    Caption = "تاريخ المتابعة القادم",
                    Visible = true,
                    VisibleIndex = 5,
                    Width = 200,
                    UnboundDataType = typeof(DateTime)
                };
                view.Columns.Add(dateFollowNextColumn);

                // Add hidden columns if needed
                CreateHiddenColumn(view, "Id", typeof(int));
                CreateHiddenColumn(view, "NationalId", typeof(string));
                CreateHiddenColumn(view, "MotherName", typeof(string));
                // Add other hidden columns as needed...

                view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.ShowColumnHeaders = true;
            }
        }

        private void CreateHiddenColumn(GridView view, string fieldName, Type dataType)
        {
            var column = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = fieldName,
                FieldName = fieldName,
                Visible = false,
                UnboundDataType = dataType
            };
            view.Columns.Add(column);
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "AutoNumericColumn")
            {
                e.DisplayText = (e.ListSourceRowIndex + 1).ToString();
            }
        }
        private void AddColumn(GridView view, string fieldName, string caption, int visibleIndex)
        {
            var column = view.Columns.AddField(fieldName);
            column.VisibleIndex = visibleIndex;
            column.Caption = caption;
            column.Visible = true;
        }
        private void HideColumns(GridView view, string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (view.Columns[columnName] != null)
                {
                    view.Columns[columnName].Visible = false;
                }
            }
        }
    }
}
