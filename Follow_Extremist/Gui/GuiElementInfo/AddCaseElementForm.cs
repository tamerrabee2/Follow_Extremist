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
using Follow_Extremist.Gui.GuiElementFollow;
using DevExpress.XtraGrid.Views.Grid;

namespace Follow_Extremist.Gui.GuiElementInfo
{
    public partial class AddCaseElementForm : Form
    {
        #region variables 
        private readonly int ID;
        private readonly string elementName;
      //  private readonly AddElementForm userControl;
        private readonly ElementInfoControl1 userControl;
        private ElementCases table;
        private readonly IDataHelper<ElementCases> dataHelper;
        private readonly IDataHelper<ElementInfo> dataHelperElementInfo;
        private readonly IDataHelper<SystemRecords> dataHelperSystemRecords;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        #endregion
        public AddCaseElementForm(int Id, ElementInfoControl1 userControl, string ElementName)
        {
            InitializeComponent();
            dataHelper = (IDataHelper <ElementCases>) ConfigurationObjectManager.GetObject("ElementCases");
            dataHelperElementInfo = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            dataHelperSystemRecords = (IDataHelper<SystemRecords>)ConfigurationObjectManager.GetObject("SystemRecords");
            loadingForm = new GuiLoading.LoadingForm();
            this.ID = Id;
            this.elementName = ElementName;
            this.userControl = userControl;
            LoadData();
        }

        #region Events

        private  void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            
        }

       

        private  void buttonSave_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Methods

        public async void LoadData()
        {
            loadingForm.Show();
            try
            {

                var data1 = await dataHelper.GetAllDataAsync();
                var data = data1.Where(x => x.ElementInfoId == ID).ToList();
                gridControl1.DataSource = data;
                
                if (gridControl1.DataSource == null)
                {
                    MessageCollections.ShowErrorServer();
                }
                else
                {
                    SetColumnsTitle();
                }
                loadingForm.Hide();
             
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void SetColumnsTitle()
        {
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                // Set column titles and adjust settings
                view.Columns["Id"].Caption = "المعرف";
                view.Columns["ElementName"].Caption = "اسم العنصر";
                view.Columns["ElementDateJail"].Caption = "تاريخ الحبس";
                view.Columns["CasesData"].Caption = "بيانات القضية";
                view.Columns["ElementDateRelease"].Caption = "تاريخ الافراج";
                view.Columns["ElementStateJailOrNot"].Caption = "حالة العنصر";
                view.Columns["ElementFollowNew"].Caption = "تاريخ المتابعة الجديد";

                // Set visibility and order of columns
                view.Columns["Id"].VisibleIndex = 1;
                view.Columns["ElementName"].VisibleIndex = 2;
                view.Columns["ElementDateJail"].VisibleIndex = 3;
                view.Columns["CasesData"].VisibleIndex = 4;
                view.Columns["ElementDateRelease"].VisibleIndex = 5;
                view.Columns["ElementStateJailOrNot"].VisibleIndex = 6;
                view.Columns["ElementFollowNew"].VisibleIndex = 7;

                // Hide other columns
                HideColumns(view, new string[] {
            "ElementInfoId", "ElementInfo"
        });
                // Set column width
                view.OptionsView.ColumnAutoWidth = false;
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in view.Columns)
                {
                    column.Width = 250;
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


        #endregion


        private void SetElementInfoId(string ElementInfoName)
        {
            
        }
        private async void AddElementForm_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.ApplyResponsiveLayout(this, startMaximized: true);
            loadingForm.Show();
            await SetFieldTData();
            loadingForm.Hide();
        }

        private async void gridView1_Click(object sender, EventArgs e)
        {
            var data = (await dataHelper.GetAllDataAsync()).Where(x => x.ElementInfoId == ID).FirstOrDefault();
            dateTimePickerElementJail.Value = data.ElementDateJail.Date;
            if (data.ElementDateRelease != null)
            {
                //DateTime selectedDate = dateTimePickerElementRelease.Value;
                //string dateString = selectedDate.ToString("yyyy-MM-dd HH:mm:ss");
                var result = DateTime.TryParse(data.ElementDateRelease.ToString(), out DateTime dateTimerelease);
                dateTimePickerElementRelease.Value = dateTimerelease;
            }
            textBoxCaseData.Text = data.CasesData;
        }


        private async Task SetFieldTData()
        {
            if (ID>0)
            {
                table = await dataHelper.FindAsync(ID);
                if (table !=null)
                {
                    dateTimePickerElementJail.Value = table.ElementDateJail;
                    if (table.ElementStateJailOrNot == radioButtonPrisoned.Text)
                    {
                        radioButtonreleased.Checked = false;
                        radioButtonPrisoned.Checked = true;
                        dateTimePickerElementRelease.Enabled = false;
                        dateTimePickerFollownew.Enabled = false;
                    }
                    else if (table.ElementStateJailOrNot == radioButtonreleased.Text)
                    {
                        radioButtonreleased.Checked = true;
                        radioButtonPrisoned.Checked = false;
                        
                        if (table.ElementDateRelease !=null|| table.ElementFollowNew !=null)
                        {
                            var result = DateTime.TryParse(table.ElementDateRelease.ToString(), out DateTime dateTimerelease);
                            dateTimePickerElementRelease.Value = dateTimerelease;
                            var result1 = DateTime.TryParse(table.ElementDateRelease.ToString(), out DateTime dateTimefollownow);
                            dateTimePickerFollownew.Value = dateTimefollownow;
                        }
                        else
                        {
                            dateTimePickerElementRelease.Enabled = true;
                            dateTimePickerFollownew.Enabled = true;
                        }
                    }
                    else
                    {
                        radioButtonreleased.Checked = false;
                        radioButtonPrisoned.Checked = false;
                        dateTimePickerElementRelease.Enabled = false;
                        dateTimePickerFollownew.Enabled = false;
                    }
                }
                else
                {
                    MessageCollections.ShowErrorServer();
                }
            }
        }
    }

}
