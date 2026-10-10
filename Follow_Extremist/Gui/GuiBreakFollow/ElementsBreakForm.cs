using ClosedXML.Excel;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Follow_Extremist.Code;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow_Extremist.Gui.GuiBreakFollow
{
    public partial class ElementsBreakForm : Form
    {
        #region Varaiables
        private readonly IDataHelper<ElementInfo> dataHelperElementInfo;
        private readonly IDataHelper<ElementInfoView> dataHelper;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        private List<int> IdList = new List<int>();
        private string SearchItem;
        private List<ElementInfoView> allElements = new List<ElementInfoView>();
        private GuiCommon.PaginationControl paginationControl;
        #endregion
        public ElementsBreakForm()
        {
            InitializeComponent();
            SetupPagination();
            FormLayoutHelper.ApplyResponsiveLayout(this, startMaximized: true);
            dataHelperElementInfo = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelper = (IDataHelper<ElementInfoView>)ConfigurationObjectManager.GetObject("ElementInfoView");
            loadingForm = new GuiLoading.LoadingForm();
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            SetRoles();
            LoadData();
        }

        private void SetupPagination()
        {
            paginationControl = new GuiCommon.PaginationControl();
            paginationControl.Dock = DockStyle.Bottom;
            paginationControl.PageChanged += (s, e) =>
            {
                var pageData = paginationControl.GetPageData(allElements, resetToFirstPage: false);
                gridControl1.DataSource = pageData;
                SetColumnsTitleElement1();
            };
            this.Controls.Add(paginationControl);
            paginationControl.BringToFront();
        }

        #region Methods
        private async void LoadData()
        {
            loadingForm.Show();

            try
            {
                // Fetch data asynchronously using IDataHelper
                var data = await dataHelper.GetAllDataAsync();
                allElements = data?.ToList() ?? new List<ElementInfoView>();
                var pageData = paginationControl.GetPageData(allElements, resetToFirstPage: true);
                gridControl1.DataSource = pageData;

                if (gridControl1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitleElement1();
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

        private async void PrintGridViewData()
        {
            try
            {
                
                var data = await dataHelper.GetAllDataAsync();
                if (data == null)
                {
                    MessageCollections.ShowErrorServer();
                    return;
                }

                // create an instance of the report 
                var report = new Follow_Extremist.Gui.GuiReport.Report1();

                // bind data to the report 
                report.BindData(data);
                // assign the report to the documentviewer 
                var documentviewer = new Follow_Extremist.Gui.GuiReport.ReportForm();
                documentviewer.documentViewer1.DocumentSource = report;
                report.CreateDocument();
                documentviewer.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            
        }
        private void SetRoles()
        {
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


        #region DatagridcolumnsName
        private void SetColumnsTitleElement1()
        {
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                // Check if the auto-numeric column already exists
                if (view.Columns["AutoNumericColumn"] == null)
                {
                    // Add an auto-numeric column
                    var autoNumericColumn = new DevExpress.XtraGrid.Columns.GridColumn
                    {
                        Name = "AutoNumericColumn",
                        FieldName = "AutoNumericColumn",
                        Caption = "مسلسل",
                        Visible = true,
                        VisibleIndex = 0,
                        Width = 100
                    };
                    view.Columns.Add(autoNumericColumn);
                }

                // Set column titles and adjust settings
                view.Columns["ElementName"].Caption = "اسم العنصر";
                view.Columns["BirthDate"].Caption = "تاريخ الميلاد";
                view.Columns["Address"].Caption = "العنوان";
                view.Columns["Notes"].Caption = "ملاحظات";
                view.Columns["DateFollowNow"].Caption = "تاريخ المتابعة الحالي";
                view.Columns["DateFollowNext"].Caption = "تاريخ المتابعة القادم";

                // Set visibility and order of columns
                view.Columns["ElementName"].VisibleIndex = 2;
                view.Columns["BirthDate"].VisibleIndex = 3;
                view.Columns["Address"].VisibleIndex = 4;
                view.Columns["Notes"].VisibleIndex = 5;
                view.Columns["DateFollowNow"].VisibleIndex = 6;
                view.Columns["DateFollowNext"].VisibleIndex = 7;

                // Set column width
                view.OptionsView.ColumnAutoWidth = false;

                view.Columns["ElementName"].Width = 350;
                view.Columns["BirthDate"].Width = 150;
                view.Columns["Address"].Width = 300;
                view.Columns["Notes"].Width = 300;
                view.Columns["DateFollowNow"].Width = 150;
                view.Columns["DateFollowNext"].Width = 150;
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
        }

        // Event handler to customize cell appearance
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.FieldName == "ElementName" || e.Column.FieldName == "Address" || e.Column.FieldName == "Notes")
            //{
            //    e.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //    e.Appearance.Font = new Font("Cairo", 11); // Set the desired font
            //}
            //e.DefaultDraw();
        }


        // Event handler to customize the page header
        private void printableComponentLink_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string dateday = DateTime.Now.Date.ToShortDateString();
            string headerText = "كشف كسر متابعات اقل من تاريخ يوم" + " " + dateday; // Your custom text here
            Font headerFont = new Font("Cairo", 12, FontStyle.Bold);
            RectangleF headerBounds = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);

            // Draw the custom text in the header area
            e.Graph.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat((DevExpress.Drawing.DXStringAlignment)StringAlignment.Center);
            e.Graph.Font = headerFont;
            e.Graph.DrawString(headerText, Color.Black, headerBounds, DevExpress.XtraPrinting.BorderSide.None);
        }
        // Event handler to initialize print settings
        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            PrintingSystemBase printingSystem = e.PrintingSystem as PrintingSystemBase;
            printingSystem.PageSettings.Landscape = true;
        }
        #endregion

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
                    var allData = await dataHelper.GetAllDataAsync();
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
                            foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
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

        #endregion

        #region Events 
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintGridViewData();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            ExportGridControlToExcel();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private async void Search()
        {
            loadingForm.Show();
            try
            {
                SearchItem = textBoxSearch.Text.Trim();
                var dataSource = await dataHelper.SearchAsync(SearchItem);
                allElements = dataSource?.ToList() ?? new List<ElementInfoView>();
                var pageData = paginationControl.GetPageData(allElements, resetToFirstPage: true);
                gridControl1.DataSource = pageData;

                if (gridControl1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitleElement1();
                }
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

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }


        #endregion

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            
        }
    }
}
