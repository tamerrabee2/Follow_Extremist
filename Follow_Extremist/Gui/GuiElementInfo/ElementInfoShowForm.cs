using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Tile;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Follow_Extremist.Gui.GuiElementInfo
{
    public partial class ElementInfoShowForm : Form
    {
        private readonly int ID;
        private readonly IDataHelper<ElementInfo> dataHelper;
        private readonly ElementInfoControl1 elementInfoControl1;
        private readonly Gui.GuiLoading.LoadingForm loadingForm;
        public ElementInfoShowForm(int id, ElementInfoControl1 elementInfoControl1)
        {
            InitializeComponent();
            dataHelper = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
            this.ID = id;
            this.elementInfoControl1 = elementInfoControl1;
            loadingForm = new GuiLoading.LoadingForm();
            LoadData();
            
        }

        private async void LoadData()
        {
            loadingForm.Show();

            // Assuming ID is a field or property that you use to filter data
            var data = await dataHelper.GetAllDataAsync();
            var dataSource = data.Where(x => x.Id == ID).ToList(); // Ensure to materialize the data with ToList()

            if (dataSource.Count == 0)
            {
                MessageCollections.ShowErrorServer();
            }
            else
            {
                gridControl1.DataSource = dataSource;
                SetColumnsTitleElement1();
            }

            loadingForm.Hide();
        }

        private void SetColumnsTitleElement1()
        {
            // Set column titles and adjust settings
            var view = gridControl1.MainView as CardView;
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
                view.Columns["CaseData"].Caption = "بيانات القضايا";
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            cardView1.ShowPrintPreview();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
