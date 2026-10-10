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
    public class UnenrolledReportItem
    {
        public int Sequence { get; set; }
        public int ElementId { get; set; }
        public string ElementName { get; set; }
        public string NationalId { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
        public string NextFollowDate { get; set; }
    }

    public static class UnenrolledFingerprintsReportService
    {
        public static async Task ShowUnenrolledReportAsync()
        {
            try
            {
                var dataHelperElement = (IDataHelper<ElementInfo>)ConfigurationObjectManager.GetObject("ElementInfo");
                var dataHelperFingerprint = (IDataHelper<ElementFingerprint>)ConfigurationObjectManager.GetObject("ElementFingerprint");

                var allElements = await dataHelperElement.GetAllDataAsync() ?? new List<ElementInfo>();
                var allFingerprints = await dataHelperFingerprint.GetAllDataAsync() ?? new List<ElementFingerprint>();

                // العناصر التي لديها بصمات مسجلة
                var enrolledElementIds = new HashSet<int>(allFingerprints.Select(f => f.ElementId));

                // العناصر التي ليس لها أي بصمة مسجلة في المنظومة
                var unenrolledElements = allElements
                    .Where(e => !enrolledElementIds.Contains(e.Id))
                    .OrderBy(e => e.ElementName)
                    .ToList();

                if (unenrolledElements.Count == 0)
                {
                    MessageBox.Show(
                        "جميع الأشخاص المسجلين بالمنظومة لديهم بصمات أصابع مسجلة بنجاح.",
                        "تقرير البصمات",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var reportItems = new List<UnenrolledReportItem>();
                int seq = 1;
                foreach (var el in unenrolledElements)
                {
                    string followDateStr = el.DateFollowNow != default
                        ? el.DateFollowNow.ToString("yyyy/MM/dd")
                        : (el.DateFollowNext != default ? el.DateFollowNext.ToString("yyyy/MM/dd") : "-");

                    string phoneStr = !string.IsNullOrWhiteSpace(el.Mobile) 
                        ? el.Mobile 
                        : (!string.IsNullOrWhiteSpace(el.Phone) ? el.Phone : "-");

                    reportItems.Add(new UnenrolledReportItem
                    {
                        Sequence = seq++,
                        ElementId = el.Id,
                        ElementName = el.ElementName ?? $"عنصر #{el.Id}",
                        NationalId = string.IsNullOrWhiteSpace(el.NationalId) ? "-" : el.NationalId,
                        Job = string.IsNullOrWhiteSpace(el.Job) ? "-" : el.Job,
                        Phone = phoneStr,
                        NextFollowDate = followDateStr
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
                MessageBox.Show($"خطأ أثناء استخراج تقرير الأشخاص غير المسجلين بالبصمة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static XtraReport CreateReport(List<UnenrolledReportItem> items)
        {
            var report = new XtraReport
            {
                PaperKind = (DevExpress.Drawing.Printing.DXPaperKind)System.Drawing.Printing.PaperKind.A4,
                PageWidth = 827,
                PageHeight = 1169,
                Margins = new System.Drawing.Printing.Margins(35, 35, 35, 35),
                RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes,
                RightToLeftLayout = DevExpress.XtraReports.UI.RightToLeftLayout.Yes,
                DisplayName = $"تقرير_غير_المسجلين_بالبصمة_{DateTime.Now:yyyy_MM_dd}"
            };

            // Margins
            report.Bands.Add(new TopMarginBand { HeightF = 35 });
            report.Bands.Add(new BottomMarginBand { HeightF = 35 });

            // Report Header Band
            var reportHeader = new ReportHeaderBand { HeightF = 135 };

            var titleLabel = new XRLabel
            {
                Text = "تقرير الأشخاص غير المسجلين بجهاز البصمة",
                Font = new Font("Cairo", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(194, 65, 12), // Amber-700
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(0, 5),
                SizeF = new SizeF(757, 36),
                WordWrap = true,
                CanGrow = true,
                Multiline = true
            };
            reportHeader.Controls.Add(titleLabel);

            var subTitleLabel = new XRLabel
            {
                Text = $"إجمالي غير المسجلين بالبصمة: {items.Count} شخص  |  تاريخ التقرير: {DateTime.Now:yyyy/MM/dd HH:mm}",
                Font = new Font("Cairo", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(0, 42),
                SizeF = new SizeF(757, 26),
                WordWrap = true,
                CanGrow = true,
                Multiline = true
            };
            reportHeader.Controls.Add(subTitleLabel);

            var noteLabel = new XRLabel
            {
                Text = "ℹ️ تنبيه: هؤلاء الأشخاص مقيدون بالمنظومة ولم تؤخذ بصماتهم أو لم تتم مزامنتهم مع جهاز البصمة، يلزم تسجيل بصماتهم من شاشة إدارة البصمات.",
                Font = new Font("Cairo", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(254, 243, 199),
                ForeColor = Color.FromArgb(146, 64, 14),
                BorderColor = Color.FromArgb(245, 158, 11),
                Borders = BorderSide.All,
                BorderWidth = 1,
                TextAlignment = TextAlignment.MiddleCenter,
                LocationFloat = new DevExpress.Utils.PointFloat(10, 75),
                SizeF = new SizeF(737, 45),
                WordWrap = true,
                CanGrow = true,
                Multiline = true
            };
            reportHeader.Controls.Add(noteLabel);
            report.Bands.Add(reportHeader);

            // Page Header (Table columns)
            var pageHeader = new PageHeaderBand { HeightF = 40 };
            var tableHeader = new XRTable
            {
                LocationFloat = new DevExpress.Utils.PointFloat(0, 0),
                SizeF = new SizeF(757, 40),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Cairo", 9.5F, FontStyle.Bold),
                TextAlignment = TextAlignment.MiddleCenter,
                Borders = BorderSide.All,
                BorderColor = Color.FromArgb(203, 213, 225)
            };
            var headerRow = new XRTableRow { HeightF = 40, CanGrow = true };
            headerRow.Cells.Add(new XRTableCell { Text = "م", WidthF = 35 });
            headerRow.Cells.Add(new XRTableCell { Text = "كود", WidthF = 55 });
            headerRow.Cells.Add(new XRTableCell { Text = "اسم الشخص", WidthF = 212 });
            headerRow.Cells.Add(new XRTableCell { Text = "الرقم القومي", WidthF = 130 });
            headerRow.Cells.Add(new XRTableCell { Text = "المهنة / الوظيفة", WidthF = 125 });
            headerRow.Cells.Add(new XRTableCell { Text = "الهاتف", WidthF = 100 });
            headerRow.Cells.Add(new XRTableCell { Text = "موعد المتابعة", WidthF = 100 });

            foreach (XRTableCell cell in headerRow.Cells)
            {
                cell.WordWrap = true;
                cell.CanGrow = true;
                cell.Multiline = true;
            }

            tableHeader.Rows.Add(headerRow);
            pageHeader.Controls.Add(tableHeader);
            report.Bands.Add(pageHeader);

            // Detail Band
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
            dataRow.Cells.Add(new XRTableCell { WidthF = 35, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[Sequence]") } });
            dataRow.Cells.Add(new XRTableCell { WidthF = 55, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[ElementId]") } });
            dataRow.Cells.Add(new XRTableCell { WidthF = 212, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[ElementName]") } });
            dataRow.Cells.Add(new XRTableCell { WidthF = 130, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[NationalId]") } });
            dataRow.Cells.Add(new XRTableCell { WidthF = 125, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[Job]") } });
            dataRow.Cells.Add(new XRTableCell { WidthF = 100, ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[Phone]") } });
            dataRow.Cells.Add(new XRTableCell { WidthF = 100, ForeColor = Color.FromArgb(30, 64, 175), ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "[NextFollowDate]") } });

            foreach (XRTableCell cell in dataRow.Cells)
            {
                cell.WordWrap = true;
                cell.CanGrow = true;
                cell.Multiline = true;
            }

            tableData.Rows.Add(dataRow);
            detail.Controls.Add(tableData);
            report.Bands.Add(detail);

            // Page Footer
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

            report.DataSource = items;
            return report;
        }
    }
}
