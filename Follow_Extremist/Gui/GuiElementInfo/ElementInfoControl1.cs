using System;
using Follow_Extremist.Code;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Tile;
using System.IO;
using DevExpress.XtraGrid.Views.Layout;

namespace Follow_Extremist.Gui.GuiElementInfo
{
    public partial class ElementInfoControl1 : UserControl
    {
        // variables 
        private readonly IDataHelper<ElementInfo> dataHelper;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly IDataHelper<ElementCases> dataHelperElementCases;
        private ElementCases elementCases;
        private ElementInfo elementInfo;
        private static ElementInfoControl1 _elementInfoControl;
        private int rowId;
        private string elementName;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> idList = new List<int>();
        private string SearchItem;
        private List<ElementInfo> allElements = new List<ElementInfo>();
        private GuiCommon.PaginationControl paginationControl;

        public ElementInfoControl1()
        {
            InitializeComponent();
            SetupPagination();
            InitializeAdvancedSearchPanel();
            SetRoles();
            dataHelper = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            dataHelperElementCases = (IDataHelper<ElementCases>)ConfigurationObjectManager.GetObject("ElementCases");
            loadingForm = new GuiLoading.LoadingForm();
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
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
        }

        public void BindCurrentPage()
        {
            var pageData = paginationControl.GetPageData(allElements);
            gridControl1.DataSource = pageData;
            SetColumnsTitleElement1();
            gridControl1.BringToFront();
        }

        #region Methods

        public static ElementInfoControl1 Instance()
        {
            return _elementInfoControl ?? (new ElementInfoControl1());
        }

        public async void LoadData()
        {
            loadingForm.Show();
            try
            {
                var data = await dataHelper.GetAllDataAsync();
                // استبعاد العناصر التي خارج المتابعة بحيث لا تظهر في جدول العناصر
                allElements = data?.Where(x => x.FollowState == null || x.FollowState.Trim() != "خارج المتابعة").ToList() ?? new List<ElementInfo>();
                var pageData = paginationControl.GetPageData(allElements, resetToFirstPage: true);
                gridControl1.DataSource = pageData;

                if (gridControl1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitleElement1();
                    gridControl1.BringToFront();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                loadingForm.Hide();
            }
        }

        private void ShowReport(IEnumerable<ElementInfo> data)
        {
            var report = new Gui.GuiReport.GuiReportElementInfo.ReportElementInfo();

            // Bind data to the report
            report.BindData(data);
            // assign the report to the documentviewer 
            var documentviewer = new Follow_Extremist.Gui.GuiReport.ReportForm();
            documentviewer.documentViewer1.DocumentSource = report;
            report.CreateDocument();
            documentviewer.Show();
        }
        private void SetColumnsTitle()
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


                //Set column width
                view.OptionsView.ColumnAutoWidth = false;
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
                {
                    column.Width = 250;
                }
            }
        }

        
        private void EditData()
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.RowCount > 0)
            {
                int selectedRowHandle = view.FocusedRowHandle;
                if (selectedRowHandle >= 0)
                {
                    int rowId = 0;
                    if (view.GetRow(selectedRowHandle) is ElementInfo elem)
                    {
                        rowId = elem.Id;
                    }
                    else
                    {
                        object value = view.GetRowCellValue(selectedRowHandle, "Id");
                        if (value != null) int.TryParse(value.ToString(), out rowId);
                    }

                    if (rowId > 0)
                    {
                        var addElementForm = new AddElementForm(rowId, this, false, true);
                        addElementForm.Show();
                    }
                    else
                    {
                        MessageCollections.ShowEmptyDataMessage();
                    }
                }
                else
                {
                    MessageCollections.ShowEmptyDataMessage();
                }
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }

        }
       

        private void SetIdRowForDelete(GridView view)
        {   
                foreach (int rowHandle in view.GetSelectedRows())
                {
                    if (view.GetRow(rowHandle) is ElementInfo elem)
                    {
                        idList.Add(elem.Id);
                    }
                    else
                    {
                        object value = view.GetRowCellValue(rowHandle, "Id");
                        if (value != null && int.TryParse(value.ToString(), out int id)) 
                        {
                            idList.Add(id);
                        }
                    }
                }
        }

        public async void Search()
        {
            loadingForm.Show();
            try
            {
                SearchItem = textBoxSearch.Text.Trim();
                var dataSource = await dataHelper.SearchAsync(SearchItem);
                allElements = dataSource?.Where(x => x.FollowState == null || x.FollowState.Trim() != "خارج المتابعة").ToList() ?? new List<ElementInfo>();
                var pageData = paginationControl.GetPageData(allElements, resetToFirstPage: true);
                gridControl1.DataSource = pageData;
                SetColumnsTitleElement1();
                gridControl1.BringToFront();
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
                    using (var xLWorkbook = new XLWorkbook())
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
                                    if (imageData != null && imageData.Length > 0)
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

                        using (var ma = new MemoryStream())
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
            buttonAdd.Visible = UsersRolesManager.GetRole("checkBoxAdd");
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
                if (buttonAdvancedSearch != null) buttonAdvancedSearch.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxprint"))
            {
                buttonPrint.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxExport"))
            {
                buttonExport.Enabled = false;
            }
            if (!UsersRolesManager.GetRole("checkBoxEditFollowDate"))
            {
                buttonEditFollowDate.Enabled = false;
            }
        }

        private void SetColumnsTitleElement1()
        {
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                view.OptionsDetail.EnableMasterViewMode = false;
                view.OptionsDetail.ShowDetailTabs = false;
                view.OptionsDetail.SmartDetailExpand = false;
                view.OptionsView.ShowColumnHeaders = true;
                view.OptionsView.ShowGroupPanel = false;
                view.ColumnPanelRowHeight = 45;
                view.Appearance.HeaderPanel.Font = new System.Drawing.Font("Cairo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                view.Appearance.HeaderPanel.Options.UseFont = true;
                view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                view.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                view.Appearance.HeaderPanel.Options.UseTextOptions = true;

                // 1. Hide unwanted columns first
                HideColumns(view, new string[] {
                    "NationalId", "MotherName", "Qualification", "Job",
                    "BirthPlace", "ElementImage", "NationalIdImage", "FollowState",
                    "ReasonEndFollow", "Phone", "Mobile", "Mobile2", "Mobile3",
                    "RegulatoryStatus", "FacebookAcount", "FacebookID", "PrisonedOrnot",
                    "DateFollowNext", "CaseData", "Id",
                    "ElementAddInfo", "ElementFollowAdd", "ElementCases", "ElementFingerprints", "AttendanceLogs"
                });

                // 2. Ensure the auto-numeric column exists
                if (view.Columns["AutoNumericColumn"] == null)
                {
                    var autoNumericColumn = new DevExpress.XtraGrid.Columns.GridColumn
                    {
                        Name = "AutoNumericColumn",
                        FieldName = "AutoNumericColumn",
                        Caption = "مسلسل",
                        Visible = true,
                        Width = 80,
                        UnboundDataType = typeof(int)
                    };
                    view.Columns.Add(autoNumericColumn);
                }
                else
                {
                    view.Columns["AutoNumericColumn"].Visible = true;
                    view.Columns["AutoNumericColumn"].Caption = "مسلسل";
                    view.Columns["AutoNumericColumn"].Width = 80;
                }

                // 3. Set captions and widths
                if (view.Columns["ElementName"] != null) { view.Columns["ElementName"].Caption = "اسم العنصر"; view.Columns["ElementName"].Width = 300; }
                if (view.Columns["BirthDate"] != null) { view.Columns["BirthDate"].Caption = "تاريخ الميلاد"; view.Columns["BirthDate"].Width = 140; }
                if (view.Columns["Address"] != null) { view.Columns["Address"].Caption = "العنوان"; view.Columns["Address"].Width = 280; }
                if (view.Columns["Notes"] != null) { view.Columns["Notes"].Caption = "ملاحظات"; view.Columns["Notes"].Width = 260; }
                if (view.Columns["DateFollowStart"] != null) { view.Columns["DateFollowStart"].Caption = "تاريخ بدء المتابعة"; view.Columns["DateFollowStart"].Width = 140; }
                if (view.Columns["FollowDaysCount"] != null) { view.Columns["FollowDaysCount"].Caption = "عدد ايام المتابعة"; view.Columns["FollowDaysCount"].Width = 130; }
                if (view.Columns["DateFollowNow"] != null) { view.Columns["DateFollowNow"].Caption = "تاريخ المتابعة القادم"; view.Columns["DateFollowNow"].Width = 140; }
                if (view.Columns["ElementWantedStatus"] != null)
                {
                    view.Columns["ElementWantedStatus"].Caption = "حالة العنصر";
                    view.Columns["ElementWantedStatus"].Width = 130;
                    view.Columns["ElementWantedStatus"].Visible = true;
                }

                // 4. Set column display order (VisibleIndex) strictly in order
                int vIndex = 0;
                if (view.Columns["AutoNumericColumn"] != null) view.Columns["AutoNumericColumn"].VisibleIndex = vIndex++;
                if (view.Columns["ElementName"] != null) view.Columns["ElementName"].VisibleIndex = vIndex++;
                if (view.Columns["BirthDate"] != null) view.Columns["BirthDate"].VisibleIndex = vIndex++;
                if (view.Columns["Address"] != null) view.Columns["Address"].VisibleIndex = vIndex++;
                if (view.Columns["Notes"] != null) view.Columns["Notes"].VisibleIndex = vIndex++;
                if (view.Columns["DateFollowStart"] != null) view.Columns["DateFollowStart"].VisibleIndex = vIndex++;
                if (view.Columns["FollowDaysCount"] != null) view.Columns["FollowDaysCount"].VisibleIndex = vIndex++;
                if (view.Columns["DateFollowNow"] != null) view.Columns["DateFollowNow"].VisibleIndex = vIndex++;
                if (view.Columns["ElementWantedStatus"] != null) view.Columns["ElementWantedStatus"].VisibleIndex = vIndex++;

                // Ensure AutoNumericColumn stays at the first position (0)
                if (view.Columns["AutoNumericColumn"] != null) view.Columns["AutoNumericColumn"].VisibleIndex = 0;

                view.OptionsView.ColumnAutoWidth = false;
                gridControl1.ForceInitialize();
                view.LayoutChanged();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "AutoNumericColumn" || e.Column.FieldName == "AutoNumericColumn")
            {
                int startIndex = paginationControl != null && !paginationControl.IsAll 
                    ? (paginationControl.CurrentPage - 1) * paginationControl.PageSize 
                    : 0;
                e.DisplayText = (startIndex + e.ListSourceRowIndex + 1).ToString();
            }
            else if (e.Column.FieldName == "ElementWantedStatus" || e.Column.Name == "ElementWantedStatus")
            {
                if (e.Value is ElementWantedStatus status)
                {
                    e.DisplayText = status.IsWanted ? "مطلوب" : "غير مطلوب";
                }
                else
                {
                    e.DisplayText = "غير مطلوب";
                }
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
            dataTable.Columns["DateFollowNow"].SetOrdinal(19);
            dataTable.Columns["DateFollowNow"].ColumnName = "تاريخ المتابعة الجديد";
            dataTable.Columns["Address"].SetOrdinal(20);
            dataTable.Columns["Address"].ColumnName = "العنوان";
            dataTable.Columns["DateFollowNext"].SetOrdinal(21);
            dataTable.Columns["DateFollowNext"].ColumnName = "تاريخ المتابعة القادم";
            dataTable.Columns["RegulatoryStatus"].SetOrdinal(22);
            dataTable.Columns["RegulatoryStatus"].ColumnName = "الوضع التنظيمي";
            dataTable.Columns["FacebookAcount"].SetOrdinal(23);
            dataTable.Columns["FacebookAcount"].ColumnName = "الفيسبوك";
            dataTable.Columns["FacebookID"].SetOrdinal(24);
            dataTable.Columns["FacebookID"].ColumnName = " معرف الفيسبوك";
            dataTable.Columns["PrisonedOrnot"].SetOrdinal(25);
            dataTable.Columns["PrisonedOrnot"].ColumnName = " حالة العنصر";

            return dataTable;
        }
        #endregion

        #region events
        private void comboBoxPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddElementForm addelementForm = new AddElementForm(0, this,false,false);
            addelementForm.labelelementstatus.Enabled = false;
            addelementForm.checkBoxPrisoned.Enabled = false;
            addelementForm.Show();
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            var view = gridControl1.MainView as GridView;
            if (view != null && view.RowCount>0)
            {
                var deleteResult = MessageCollections.ShowDeletDialog();
                if (deleteResult)
                {
                    idList.Clear();
                    SetIdRowForDelete(view);
                    loadingForm.Show();
                    if (idList.Count>0)
                    {
                        for (int i = 0; i < idList.Count; i++)
                        {
                            rowId = idList[i];
                            var result = await dataHelper.DeleteAsync(rowId);
                            if (result == 1)
                            {
                                SystemRecords systemRecords = new SystemRecords
                                {
                                    Title = "عملية حذف على بيانات العناصر",
                                    USerName = Properties.Settings.Default.UserName,
                                    Details = "تم حذف عنصر رقم" + rowId.ToString(),
                                    AddedDate = DateTime.Now
                                };
                                await dataHelperSystemRecords.AddAsync(systemRecords);
                                MessageCollections.ShowDeleteNotification();
                            }
                            else
                            {
                                MessageCollections.ShowErrorServer();
                            }
                        }
                        idList.Clear();
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

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ShowReport((IEnumerable<ElementInfo>)gridControl1.DataSource);
          //  gridView1.ShowPrintPreview();
        }

        private async void buttonExport_Click(object sender, EventArgs e)
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

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        #endregion

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void buttonEditFollowDate_Click(object sender, EventArgs e)
        {
            EditFollowDate();
        }
        private void EditFollowDate()
        {
            GridView cardView = gridControl1.MainView as GridView;
            if (cardView != null && cardView.RowCount > 0)
            {
                int selectedRowHandle = cardView.FocusedRowHandle;

                if (selectedRowHandle >= 0)
                {
                    int rowId = 0;
                    if (cardView.GetRow(selectedRowHandle) is ElementInfo element)
                    {
                        rowId = element.Id;
                    }
                    else
                    {
                        object value = cardView.GetRowCellValue(selectedRowHandle, "Id");
                        if (value != null) int.TryParse(value.ToString(), out rowId);
                    }

                    if (rowId > 0)
                    {
                        AddElementForm addUsersForm = new AddElementForm(rowId, this, true, false);
                        addUsersForm.Show();
                    }
                    else
                    {
                        MessageCollections.ShowEmptyDataMessage();
                    }
                }
                else
                {
                    MessageCollections.ShowEmptyDataMessage();
                }
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
        }

        private async void AddjailDate()
        {
            loadingForm.Show();
            GridView cardView = gridControl1.MainView as GridView;
            if (cardView != null && cardView.RowCount > 0)
            {
                int selectedRowHandle = cardView.FocusedRowHandle;

                if (selectedRowHandle >= 0)
                {
                    int RowId = 0;
                    string elemName = null;
                    if (cardView.GetRow(selectedRowHandle) is ElementInfo elem)
                    {
                        RowId = elem.Id;
                        elemName = elem.ElementName;
                    }
                    else
                    {
                        object value = cardView.GetRowCellValue(selectedRowHandle, "Id");
                        object valueelement = cardView.GetRowCellValue(selectedRowHandle, "ElementName");
                        if (value != null) int.TryParse(value.ToString(), out RowId);
                        elemName = valueelement?.ToString();
                    }

                    if (RowId > 0)
                    {
                        AddElementForm addElementForm = new AddElementForm(0, new ElementInfoControl1(), false,false);
                        elementCases = new ElementCases
                        {
                            ElementName = elemName,
                            ElementInfoId = RowId,
                           // ElementDateJail = dateTimePickerjail.Value,
                            CasesData = null,
                            ElementDateRelease = null,
                            ElementStateJailOrNot = addElementForm.radioButtonPrisoned.Text,
                            ElementFollowNew = null
                    };
                        var result = await dataHelperElementCases.AddAsync(elementCases);
                        if (result == 1)
                        {
                            // Save system records
                            SystemRecords systemRecords = new SystemRecords
                            {
                                Title = "اضافة تاريخ حبس لعنصر",
                                USerName = Properties.Settings.Default.UserName,
                                Details = "تمت اضافة تاريخ حبس للعنصر" + " " + elementCases.ElementName,
                                AddedDate = DateTime.Now
                            };
                            await dataHelperSystemRecords.AddAsync(systemRecords);
                        }
                        var elementinfoedit = await dataHelper.FindAsync(RowId);
                        elementinfoedit.PrisonedOrnot = addElementForm.radioButtonPrisoned.Text;
                        var result2 = await dataHelper.EditAsync(elementinfoedit);
                        if (result2 == 1)
                        {
                            // Save system records
                            SystemRecords systemRecords = new SystemRecords
                            {
                                Title = "تعديل حالة العنصر",
                                USerName = Properties.Settings.Default.UserName,
                                Details = "تم تعديل  حالة العنصر" + " " + elementCases.ElementName,
                                AddedDate = DateTime.Now
                            };
                            await dataHelperSystemRecords.AddAsync(systemRecords);
                        }
                        MessageCollections.ShoAddNotification();
                    }
                }
                else
                {
                    MessageCollections.ShowEmptyDataMessage();
                }
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
            loadingForm.Hide();
        }
        private void buttonaddjail_Click(object sender, EventArgs e)
        {
            AddjailDate();
        }

        private  void buttonaddcases_Click(object sender, EventArgs e)
        {
            loadingForm.Show();
            GridView cardView = gridControl1.MainView as GridView;
            if (cardView != null && cardView.RowCount > 0)
            {
                int selectedRowHandle = cardView.FocusedRowHandle;

                if (selectedRowHandle >= 0)
                {
                    int RowId = 0;
                    string elemName = null;
                    if (cardView.GetRow(selectedRowHandle) is ElementInfo elem)
                    {
                        RowId = elem.Id;
                        elemName = elem.ElementName;
                    }
                    else
                    {
                        object value = cardView.GetRowCellValue(selectedRowHandle, "Id");
                        object valueelement = cardView.GetRowCellValue(selectedRowHandle, "ElementName");
                        if (value != null) int.TryParse(value.ToString(), out RowId);
                        elemName = valueelement?.ToString();
                    }

                    if (RowId > 0)
                    {
                        elementName = elemName;
                        AddCaseElementForm addCaseElementForm = new AddCaseElementForm(RowId, this, elementName);
                        addCaseElementForm.Show();
                        addCaseElementForm.textBoxElement.Text = elementName;
                    }
                }
                else
                {
                    MessageCollections.ShowEmptyDataMessage();
                }
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
            loadingForm.Hide();
        }

        private void buttonElementDetail_Click(object sender, EventArgs e)
        {
            loadingForm.Show();
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView !=null && gridView.RowCount>0)
            {
                int selectedRowHandle = gridView.FocusedRowHandle;
                if (selectedRowHandle>=0)
                {
                    int RowId = 0;
                    if (gridView.GetRow(selectedRowHandle) is ElementInfo elem)
                    {
                        RowId = elem.Id;
                    }
                    else
                    {
                        object value = gridView.GetRowCellValue(selectedRowHandle, "Id");
                        if (value != null) int.TryParse(value.ToString(), out RowId);
                    }

                    if (RowId > 0)
                    {
                        // show form details 
                        ElementInfoShowForm elementInfoShowForm = new ElementInfoShowForm(RowId, this);
                        elementInfoShowForm.Show();
                    }

                }
                else
                {
                    MessageCollections.ShowEmptyDataMessage();
                }
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
            loadingForm.Hide();
        }

        private void buttonHistoryFollow_Click(object sender, EventArgs e)
        {
            loadingForm.Show();
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView != null && gridView.RowCount > 0)
            {
                int selectedRowHandle = gridView.FocusedRowHandle;
                if (selectedRowHandle >= 0)
                {
                    int RowId = 0;
                    if (gridView.GetRow(selectedRowHandle) is ElementInfo elem)
                    {
                        RowId = elem.Id;
                    }
                    else
                    {
                        object value = gridView.GetRowCellValue(selectedRowHandle, "Id");
                        if (value != null) int.TryParse(value.ToString(), out RowId);
                    }

                    if (RowId > 0)
                    {
                        // show form details 
                        ElementFollowHistoryForm elementFollowHistoryForm = new ElementFollowHistoryForm(RowId, this);
                        elementFollowHistoryForm.Show();
                    }

                }
                else
                {
                    MessageCollections.ShowEmptyDataMessage();
                }
            }
            else
            {
                MessageCollections.ShowEmptyDataMessage();
            }
            loadingForm.Hide();
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view != null && e.RowHandle >= 0)
            {
                var prisonornot = view.GetRowCellValue(e.RowHandle, "PrisonedOrnot")?.ToString()?.Trim();
                var followState = view.GetRowCellValue(e.RowHandle, "FollowState")?.ToString()?.Trim();
                var dateFollowNow = view.GetRowCellValue(e.RowHandle, "DateFollowNow");

                if (followState == "خارج المتابعة")
                {
                    e.Appearance.BackColor = Properties.Settings.Default.outfollow;
                }
                else if (prisonornot == "محبوس")
                {
                    e.Appearance.BackColor = Properties.Settings.Default.prison;
                }
                else if (dateFollowNow != null && DateTime.TryParse(dateFollowNow.ToString(), out DateTime dateFollowNow12))
                {
                    if (dateFollowNow12.Date < DateTime.Now.Date)
                    {
                        e.Appearance.BackColor = Properties.Settings.Default.breakfollow;
                    }
                }
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Notes")
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }
    }
}

