using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Follow_Extremist.Core;
using Follow_Extremist.Data;
using Follow_Extremist.Gui.GuiReport;

namespace Follow_Extremist.Code.Services
{
    public class WantedDailyReportItem
    {
        public int Sequence { get; set; }
        public string ElementName { get; set; }
        public string NationalId { get; set; }
        public string WantedReason { get; set; }
        public string WantedBy { get; set; }
        public string WantedDate { get; set; }
        public string NextFollowDate { get; set; }
        public string CurrentFollowDate { get; set; }
    }

    public static class WantedDailyReportService
    {
        public static async Task ShowTodayWantedReportAsync()
        {
            await ShowWantedReportAsync();
        }

        public static async Task ShowWantedReportAsync()
        {
            try
            {
                var dataHelperWanted = (IDataHelper<ElementWantedStatus>)ConfigurationObjectManager.GetObject("ElementWantedStatus");
                var dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");

                var allWanted = await dataHelperWanted.GetAllDataAsync() ?? new List<ElementWantedStatus>();
                var activeWanted = allWanted.Where(w => w.IsWanted).OrderBy(w => w.Id).ToList();

                if (activeWanted.Count == 0)
                {
                    MessageBox.Show(
                        "لا توجد عناصر مسجلة في قائمة المطلوبين أمنياً حالياً.",
                        "تقرير المطلوبين أمنياً",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // عرض التنبيه الإلزامي للمستخدم
                MessageBox.Show(
                    "⚠️ تنبيه هـام للمستخدم:\n\nيرجى مراجعة واستبعاد العناصر التي تم اتخاذ الإجراءات اللازمة حيالها من شاشة المطلوبين بعد العمل عليها.",
                    "تنبيه أمني هام",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                var allElements = await dataHelperElement.GetAllDataAsync() ?? new List<ElementInfo>();
                var elementDict = allElements.ToDictionary(e => e.Id, e => e);

                var reportItems = new List<WantedDailyReportItem>();
                int seq = 1;
                foreach (var wanted in activeWanted)
                {
                    elementDict.TryGetValue(wanted.ElementId, out var element);

                    string nextDateStr = "-";
                    if (element != null && element.DateFollowNow != default)
                    {
                        nextDateStr = element.DateFollowNow.ToString("yyyy/MM/dd");
                    }
                    else if (element != null && element.DateFollowNext != default)
                    {
                        nextDateStr = element.DateFollowNext.ToString("yyyy/MM/dd");
                    }

                    string wantedDateStr = wanted.WantedDate.HasValue
                        ? wanted.WantedDate.Value.ToString("yyyy/MM/dd")
                        : "-";

                    reportItems.Add(new WantedDailyReportItem
                    {
                        Sequence = seq++,
                        ElementName = element?.ElementName ?? $"عنصر #{wanted.ElementId}",
                        NationalId = element?.NationalId ?? "-",
                        WantedReason = string.IsNullOrWhiteSpace(wanted.WantedReason) ? "-" : wanted.WantedReason,
                        WantedBy = string.IsNullOrWhiteSpace(wanted.WantedBy) ? "-" : wanted.WantedBy,
                        WantedDate = wantedDateStr,
                        NextFollowDate = nextDateStr,
                        CurrentFollowDate = DateTime.Today.ToString("yyyy/MM/dd")
                    });
                }

                var report = CreateReport(reportItems);
                var documentViewer = new ReportForm();
                documentViewer.documentViewer1.DocumentSource = report;
                report.CreateDocument();
                documentViewer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء إنشاء تقرير المطلوبين: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static XtraReport CreateReport(List<WantedDailyReportItem> items)
        {
            var report = new XtraReport
            {
                PaperKind = (DevExpress.Drawing.Printing.DXPaperKind)System.Drawing.Printing.PaperKind.A4,
                PageWidth = 827,
                PageHeight = 1169,
                Margins = new System.Drawing.Printing.Margins(35, 35, 35, 35),
                RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes,
                RightToLeftLayout = DevExpress.XtraReports.UI.RightToLeftLayout.Yes,
                DisplayName = $"تقرير_العناصر_المطلوبة_{DateTime.Now:yyyy_MM_dd}"
            };

            // Margins
            report.Bands.Add(new TopMarginBand { HeightF = 35 });
            report.Bands.Add(new BottomMarginBand { HeightF = 35 });

            // Report Header Band
            var reportHeader = new ReportHeaderBand { HeightF = 135 };

            // عنوان التقرير الرئيسي
            var titleLabel = new XRLabel
            {
                Text = "سجل العناصر المطلوبة أمنياً",
                Font = new Font("Cairo", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(185, 28, 28), // Red-700
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(0, 5),
                SizeF = new SizeF(757, 36),
                WordWrap = true,
                CanGrow = true,
                Multiline = true
            };
            reportHeader.Controls.Add(titleLabel);

            // تاريخ التقرير وعدد الحالات
            var dateLabel = new XRLabel
            {
                Text = $"إجمالي المطلوبين المسجلين: {items.Count} عنصر  |  تاريخ استخراج التقرير: {DateTime.Now:yyyy/MM/dd HH:mm}",
                Font = new Font("Cairo", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(0, 42),
                SizeF = new SizeF(757, 26),
                WordWrap = true,
                CanGrow = true,
                Multiline = true
            };
            reportHeader.Controls.Add(dateLabel);

            // شريط التنبيه الأمني
            var warningLabel = new XRLabel
            {
                Text = "⚠️ تنبيه هـام: يلزم عمل استبعاد لهذه العناصر من شاشة المطلوبين بعد العمل عليها واتخاذ الإجراءات اللازمة حيالها.",
                Font = new Font("Cairo", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(254, 242, 242),
                ForeColor = Color.FromArgb(185, 28, 28),
                BorderColor = Color.FromArgb(248, 113, 113),
                Borders = BorderSide.All,
                BorderWidth = 1,
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(10, 75),
                SizeF = new SizeF(737, 45),
                WordWrap = true,
                CanGrow = true,
                Multiline = true
            };
            reportHeader.Controls.Add(warningLabel);

            report.Bands.Add(reportHeader);

            // Page Header Band (Table Columns Header)
            var pageHeader = new PageHeaderBand { HeightF = 40 };
            var tableHeader = new XRTable
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0, 0),
                SizeF = new SizeF(757, 40),
                BackColor = Color.FromArgb(30, 41, 59), // Slate 800
                ForeColor = Color.White,
                Font = new Font("Cairo", 9.5F, FontStyle.Bold),
                TextAlignment = TextAlignment.MiddleCenter,
                Borders = BorderSide.All,
                BorderColor = Color.FromArgb(203, 213, 225)
            };
            var headerRow = new XRTableRow { HeightF = 40, CanGrow = true };

            headerRow.Cells.Add(new XRTableCell { Text = "م", WidthF = 35 });
            headerRow.Cells.Add(new XRTableCell { Text = "اسم العنصر", WidthF = 180 });
            headerRow.Cells.Add(new XRTableCell { Text = "الرقم القومي", WidthF = 122 });
            headerRow.Cells.Add(new XRTableCell { Text = "سبب الطلب / الإدراج", WidthF = 145 });
            headerRow.Cells.Add(new XRTableCell { Text = "الجهة الطالبة", WidthF = 100 });
            headerRow.Cells.Add(new XRTableCell { Text = "تاريخ الإدراج", WidthF = 85 });
            headerRow.Cells.Add(new XRTableCell { Text = "موعد المتابعة", WidthF = 90 });

            foreach (XRTableCell cell in headerRow.Cells)
            {
                cell.WordWrap = true;
                cell.CanGrow = true;
                cell.Multiline = true;
            }

            tableHeader.Rows.Add(headerRow);
            pageHeader.Controls.Add(tableHeader);
            report.Bands.Add(pageHeader);

            // Detail Band (Table Rows)
            var detail = new DetailBand { HeightF = 34 };
            var tableData = new XRTable
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0, 0),
                SizeF = new SizeF(757, 34),
                Font = new Font("Cairo", 9F, FontStyle.Regular),
                TextAlignment = TextAlignment.MiddleCenter,
                Borders = BorderSide.All,
                BorderColor = Color.FromArgb(226, 232, 240)
            };

            var dataRow = new XRTableRow { HeightF = 34, CanGrow = true };

            var cellSeq = new XRTableCell { WidthF = 35, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[Sequence]") } };
            var cellName = new XRTableCell { WidthF = 180, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[ElementName]") } };
            var cellNatId = new XRTableCell { WidthF = 122, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[NationalId]") } };
            var cellReason = new XRTableCell { WidthF = 145, ForeColor = Color.FromArgb(185, 28, 28), ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[WantedReason]") } };
            var cellBy = new XRTableCell { WidthF = 100, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[WantedBy]") } };
            var cellDate = new XRTableCell { WidthF = 85, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[WantedDate]") } };
            var cellNextDate = new XRTableCell { WidthF = 90, ForeColor = Color.FromArgb(5, 150, 105), ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[NextFollowDate]") } };

            dataRow.Cells.Add(cellSeq);
            dataRow.Cells.Add(cellName);
            dataRow.Cells.Add(cellNatId);
            dataRow.Cells.Add(cellReason);
            dataRow.Cells.Add(cellBy);
            dataRow.Cells.Add(cellDate);
            dataRow.Cells.Add(cellNextDate);

            foreach (XRTableCell cell in dataRow.Cells)
            {
                cell.WordWrap = true;
                cell.CanGrow = true;
                cell.Multiline = true;
            }

            tableData.Rows.Add(dataRow);
            detail.Controls.Add(tableData);
            report.Bands.Add(detail);

            // Page Footer Band
            var pageFooter = new PageFooterBand { HeightF = 30 };
            var pageInfo = new XRPageInfo
            {
                PageInfo = PageInfo.NumberOfTotal,
                TextFormatString = "صفحة {0} من {1}",
                Font = new Font("Cairo", 8.5F, FontStyle.Regular),
                ForeColor = Color.Gray,
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(0, 5),
                SizeF = new SizeF(757, 22)
            };
            pageFooter.Controls.Add(pageInfo);
            report.Bands.Add(pageFooter);

            // Bind Data
            report.DataSource = items;

            return report;
        }
    }
}
