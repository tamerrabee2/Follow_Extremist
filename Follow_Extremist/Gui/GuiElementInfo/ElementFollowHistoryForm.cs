using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Follow_Extremist.Data;
using Follow_Extremist.Core;
using Follow_Extremist.Code;
using DevExpress.XtraGrid.Views.Grid;

namespace Follow_Extremist.Gui.GuiElementInfo
{
    public partial class ElementFollowHistoryForm : Form
    {
        private IDataHelper<ElementFollowAdd> dataHelper;
        private int ID;
        private readonly ElementInfoControl1 elementInfoControl1;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        public ElementFollowHistoryForm(int id, ElementInfoControl1 elementInfoControl1)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<ElementFollowAdd>)ConfigurationObjectManager.GetObject("ElementFollowAdd");
            this.ID = id;
            this.elementInfoControl1 = elementInfoControl1;
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
        }

        private async void LoadData()
        {
            loadingForm.Show();
            var data = await dataHelper.GetAllDataAsync();
            var dataSource = data.Where(x => x.ElementInfoId == ID);
            gridControl1.DataSource = dataSource;

            if (dataSource == null)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
                SetColumnsTitleElement1();
            }
            loadingForm.Hide();

        }

        private void SetColumnsTitleElement1()
        {
            var view = gridControl1.MainView as GridView;
            if (view != null)
            {
                //Set column titles and adjust settings
                view.Columns["Id"].Caption = "المعرف";
                view.Columns["ElementName"].Caption = "اسم العنصر";
                view.Columns["DateFollow"].Caption = "تاريخ المتابعة";


                // Set visibility and order of columns
                view.Columns["Id"].VisibleIndex = 1;
                view.Columns["ElementName"].VisibleIndex = 2;
                view.Columns["DateFollow"].VisibleIndex = 3;
               

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

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            gridView1.ShowPrintPreview();
        }
    }
}
